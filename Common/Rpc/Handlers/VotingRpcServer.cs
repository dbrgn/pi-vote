﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// Voting RPC server.
  /// </summary>
  public class VotingRpcServer : RpcServer
  {
    /// <summary>
    /// Config file for the configuration of the server.
    /// </summary>
    private const string ServerConfigFileName = "pi-vote-server.cfg";

    /// <summary>
    /// Config file for the remote configuration of the clients.
    /// </summary>
    private const string RemoteConfigFileName = "pi-vote-remote.cfg";
    
    /// <summary>
    /// List of voting procedures.
    /// </summary>
    private Dictionary<Guid, VotingServerEntity> votings;

    /// <summary>
    /// Storage for certificates.
    /// </summary>
    public DatabaseCertificateStorage CertificateStorage { get; private set; }

    /// <summary>
    /// The server's own certificate.
    /// </summary>
    private ServerCertificate serverCertificate;

    /// <summary>
    /// Server config file.
    /// </summary>
    private ServerConfig serverConfig;

    /// <summary>
    /// MySQL database connection.
    /// </summary>
    private MySqlConnection dbConnection;

    /// <summary>
    /// Time of last processing.
    /// </summary>
    private DateTime lastProcess;

    /// <summary>
    /// Server config file.
    /// </summary>
    public override IServerConfig ServerConfig
    {
      get { return this.serverConfig; }
    }

    /// <summary>
    /// Configures the client remotly.
    /// </summary>
    public IRemoteConfig RemoteConfig { get; private set; }

    /// <summary>
    /// Sends emails to users and admins.
    /// </summary>
    public Mailer Mailer { get; private set; }

    /// <summary>
    /// Logs messages to file.
    /// </summary>
    private Logger logger;

    /// <summary>
    /// Logs messages to file.
    /// </summary>
    public override ILogger Logger { get { return this.logger; } }

    /// <summary>
    /// Checked database connections.
    /// </summary>
    public MySqlConnection DbConnection
    {
      get { return this.dbConnection; }
    }
    
    /// <summary>
    /// Server certificate without private key.
    /// </summary>
    public Certificate ServerCertificate
    {
      get { return this.serverCertificate.OnlyPublicPart; }
    }

    /// <summary>
    /// Create the voting server.
    /// </summary>
    public VotingRpcServer()
    {
      this.lastProcess = DateTime.Now;

      this.logger = new Logger(Pirate.PiVote.Logger.ServerLogFileName, LogLevel.Info);
      Logger.Log(LogLevel.Info, "Voting RPC server starting...");

      this.serverConfig = new ServerConfig(ServerConfigFileName);
      this.serverConfig.ValidateMail(Logger);
      Logger.Log(LogLevel.Info, "Config file is read.");

      RemoteConfig = new RemoteStoredConfig(RemoteConfigFileName);
      Logger.Log(LogLevel.Info, "Remote config file is read.");

      Mailer = new Mailer(this.serverConfig, this.logger);
      Logger.Log(LogLevel.Info, "Mailer is set up.");

      this.dbConnection = new MySqlConnection(this.serverConfig.MySqlConnectionString);
      this.dbConnection.Open();
      Logger.Log(LogLevel.Info, "Database connection is open.");

      CertificateStorage = new DatabaseCertificateStorage(this.dbConnection);

      if (!CertificateStorage.TryLoadRoot())
      {
        Logger.Log(LogLevel.Error, "Root certificate file not found.");
        this.dbConnection.Close();
        throw new InvalidOperationException("Root certificate file not found.");
      }

      CertificateStorage.ImportStorageIfNeed();
      Logger.Log(LogLevel.Info, "Certificate storage is loaded.");

      this.serverCertificate = CertificateStorage.LoadServerCertificate();
      Logger.Log(LogLevel.Info, "Server certificate is loaded.");

      LoadVotings();
      Logger.Log(LogLevel.Info, "Votings are loaded.");

      ////OutputReport();
    }

    /// <summary>
    /// Load all votings from the database.
    /// </summary>
    private void LoadVotings()
    {
      this.votings = new Dictionary<Guid, VotingServerEntity>();

      MySqlCommand selectCommand = new MySqlCommand("SELECT Id, Parameters, Status FROM voting", DbConnection);
      MySqlDataReader reader = selectCommand.ExecuteReader();

      while (reader.Read())
      {
        Guid id = reader.GetGuid(0);
        byte[] signedParametersData = reader.GetBlob(1);
        Signed<VotingParameters> signedParameters = Serializable.FromBinary<Signed<VotingParameters>>(signedParametersData);
        VotingStatus status = (VotingStatus)reader.GetInt32(2);
        VotingServerEntity entity = new VotingServerEntity(this, signedParameters, this.CertificateStorage, this.serverCertificate, status);
        this.votings.Add(id, entity);
      }

      reader.Close();
    }

    /// <summary>
    /// Excecutes a RPC request.
    /// </summary>
    /// <param name="connection">Connection that made the request.</param>
    /// <param name="requestData">Serialized request data.</param>
    /// <returns>Serialized response data</returns>
    public override byte[] Execute(TcpRpcConnection connection, byte[] requestData)
    {
      Logger.Log(LogLevel.Debug, "Connection {0}: Receiving request of {1} bytes.", connection.Id, requestData.Length);

      var request = Serializable.FromBinary<RpcRequest<VotingRpcServer>>(requestData);
      var response = request.TryExecute(connection, this);

      byte[] responseData = response.ToBinary();
      Logger.Log(LogLevel.Debug, "Connection {0}: Sending response of {1} bytes.", connection.Id, responseData.Length);

      return responseData;
    }

    /// <summary>
    /// Delete a voting.
    /// </summary>
    /// <param name="votingId">Id of voting to delete.</param>
    public void DeleteVoting(IRpcConnection connection, Guid votingId)
    {
      VotingServerEntity voting = GetVoting(votingId);

      switch (voting.Status)
      {
        case VotingStatus.New:
        case VotingStatus.Sharing:
        case VotingStatus.Ready:
          voting.Delete();
          Logger.Log(LogLevel.Info, "Connection {0}: Voting id {1} title {2} is deleted.", connection.Id, voting.Parameters.VotingId.ToString(), voting.Parameters.Title.Text);
          this.votings.Remove(votingId);
          break;
        default:
          Logger.Log(LogLevel.Warning, "Connection {0}: Tried to delete voting id {1} title {2} but it was in state {3}.", connection.Id, voting.Parameters.VotingId.ToString(), voting.Parameters.Title.Text, voting.Status.ToString());
          throw new PiException(ExceptionCode.CommandNotAllowedInStatus, "Deletion of voting is not allowed in current status.");
      }
    }

    /// <summary>
    /// Creates a new voting.
    /// </summary>
    /// <param name="votingParameters">Parameters for the voting.</param>
    /// <param name="authorities">List of authorities to oversee the voting.</param>
    public void CreateVoting(
      IRpcConnection connection, 
      Signed<VotingParameters> signedVotingParameters, 
      IEnumerable<AuthorityCertificate> authorities)
    {
      if (signedVotingParameters == null)
        throw new PiArgumentException(ExceptionCode.ArgumentNull, "Voting parameters cannot be null.");
      if (authorities == null)
        throw new PiArgumentException(ExceptionCode.ArgumentNull, "Authority list cannot be null.");

      VotingParameters votingParameters = signedVotingParameters.Value;

      if (!signedVotingParameters.Verify(CertificateStorage, votingParameters.VotingBeginDate))
      {
        if (!signedVotingParameters.VerifySimple())
        {
          Logger.Log(LogLevel.Warning,
            "Connection {0}: Admin {1} (unverified) tried to create voting {2} title {3} but the signature was wrong.",
            connection.Id,
            signedVotingParameters.Certificate.Id.ToString(),
            votingParameters.VotingId.ToString(),
            votingParameters.Title.Text);
        }
        else if (signedVotingParameters.Certificate.Validate(CertificateStorage, votingParameters.VotingBeginDate) != CertificateValidationResult.Valid)
        {
          Logger.Log(LogLevel.Warning,
            "Connection {0}: Admin {1} (unverified) tried to create voting {2} title {3} but his certificate status was {4}.",
            connection.Id,
            signedVotingParameters.Certificate.Id.ToString(),
            votingParameters.VotingId.ToString(),
            votingParameters.Title.Text,
            signedVotingParameters.Certificate.Validate(CertificateStorage, votingParameters.VotingBeginDate).ToString());
        }
        else
        {
          Logger.Log(LogLevel.Warning,
            "Connection {0}: Admin {1} (unverified) tried to create voting {2} title {3} but his signature was invalid.",
            connection.Id,
            signedVotingParameters.Certificate.Id.ToString(),
            votingParameters.VotingId.ToString(),
            votingParameters.Title.Text);
        }

        throw new PiSecurityException(ExceptionCode.InvalidSignature, "Invalid signature of voting authority.");
      }

      if (!(signedVotingParameters.Certificate is AdminCertificate))
      {
        Logger.Log(LogLevel.Warning,
          "Connection {0}: Admin {1} (verified) tried to create voting {2} title {3} but he is a {4}.",
          connection.Id,
          signedVotingParameters.Certificate.Id.ToString(),
          votingParameters.VotingId.ToString(),
          votingParameters.Title.Text,
          signedVotingParameters.Certificate.TypeText);
        throw new PiSecurityException(ExceptionCode.NoAuthorizedAdmin, "No authorized admin.");
      }

      if (!CertificateStorage.SignedRevocationLists
        .Any(signedCrl => signedCrl.Verify(CertificateStorage) &&
                          votingParameters.VotingBeginDate >= signedCrl.Value.ValidFrom &&
                          votingParameters.VotingBeginDate <= signedCrl.Value.ValidUntil))
      {
        Logger.Log(LogLevel.Info, 
          "Connection {0}: Admin id {1} name {2} tried to create voting {3} title {4} but the voting begin date {5} was not covered by any CRL.", 
          connection.Id, 
          signedVotingParameters.Certificate.Id.ToString(),
          ((AdminCertificate)signedVotingParameters.Certificate).FullName,
          votingParameters.VotingId.ToString(), 
          votingParameters.Title.Text,
          votingParameters.VotingBeginDate.ToString());
        throw new PiSecurityException(ExceptionCode.InvalidSignature, "Voting begin date not covered by CRL.");
      }

      if (votingParameters.P == null)
        throw new PiArgumentException(ExceptionCode.ArgumentNull, "P cannot be null.");
      if (votingParameters.Q == null)
        throw new PiArgumentException(ExceptionCode.ArgumentNull, "Q cannot be null.");
      if (votingParameters.F == null)
        throw new PiArgumentException(ExceptionCode.ArgumentNull, "F cannot be null.");
      if (votingParameters.G == null)
        throw new PiArgumentException(ExceptionCode.ArgumentNull, "G cannot be null.");
      if (!Prime.HasSufficientLength(votingParameters.P, BaseParameters.PrimeBits))
        throw new PiException(ExceptionCode.ArgumentNull, "P is not long enough");
      if (!Prime.HasSufficientLength(votingParameters.Q, BaseParameters.PrimeBits))
        throw new PiException(ExceptionCode.ArgumentNull, "Q is not long enough");
      if (!Prime.IsPrimeUnsure(votingParameters.P))
        throw new PiArgumentException(ExceptionCode.PIsNoPrime, "P is not prime.");
      if (!Prime.IsPrimeUnsure((votingParameters.P - 1) / 2))
        throw new PiArgumentException(ExceptionCode.PIsNoSafePrime, "P is no safe prime.");
      if (!Prime.IsPrimeUnsure(votingParameters.Q))
        throw new PiArgumentException(ExceptionCode.QIsNoPrime, "Q is not prime.");

      if (!votingParameters.AuthorityCount.InRange(3, 23))
        throw new PiArgumentException(ExceptionCode.AuthorityCountOutOfRange, "Authority count out of range.");
      if (!votingParameters.Thereshold.InRange(1, votingParameters.AuthorityCount - 1))
        throw new PiArgumentException(ExceptionCode.TheresholdOutOfRange, "Thereshold out of range.");
      
      foreach (Question question in votingParameters.Questions)
      {
        if (question.Options.Count() < 2)
          throw new PiArgumentException(ExceptionCode.OptionCountOutOfRange, "Option count out of range.");
        if (!question.MaxVota.InRange(1, question.Options.Count()))
          throw new PiArgumentException(ExceptionCode.MaxVotaOutOfRange, "Maximum vota out of range.");
      }

      if (votingParameters.AuthorityCount != authorities.Count())
        throw new PiArgumentException(ExceptionCode.AuthorityCountMismatch, "Authority count does not match number of provided authorities.");
      if (!authorities.All(authority => authority.Validate(CertificateStorage, votingParameters.VotingBeginDate) == CertificateValidationResult.Valid))
        throw new PiArgumentException(ExceptionCode.AuthorityInvalid, "Authority certificate invalid or not recognized.");

      MySqlCommand insertCommand = new MySqlCommand("INSERT INTO voting (Id, Parameters, Status) VALUES (@Id, @Parameters, @Status)", DbConnection);
      insertCommand.Parameters.AddWithValue("@Id", votingParameters.VotingId.ToByteArray());
      insertCommand.Parameters.AddWithValue("@Parameters", signedVotingParameters.ToBinary());
      insertCommand.Parameters.AddWithValue("@Status", (int)VotingStatus.New);
      insertCommand.ExecuteNonQuery();

      Logger.Log(LogLevel.Info, "Connection {0}: Voting id {1} title {2} is created.", connection.Id, votingParameters.VotingId.ToString(), votingParameters.Title.Text);

      VotingServerEntity voting = new VotingServerEntity(this, signedVotingParameters, CertificateStorage, this.serverCertificate);
      authorities.Foreach(authority => voting.AddAuthority(connection, authority));
      this.votings.Add(voting.Id, voting);
      voting.SendAuthorityActionRequiredMail(MailType.AuthorityCreateSharesGreen);
    }

    /// <summary>
    /// Fetches the ids of all votings.
    /// </summary>
    /// <returns>List of voting ids.</returns>
    public IEnumerable<Guid> FetchVotingIds()
    {
      return this.votings.Keys;
    }

    private void AddRow(StringBuilder data, params object[] values)
    {
      data.AppendLine(string.Join(";", values.Select(v => v == null ? "\"\"" : "\"" + v.ToString() + "\"").ToArray()));
    }

    private string GetSignatureRequestStatistics()
    {
      var data = new StringBuilder();

      AddRow(
        data,
        "Type",
        "Group",
        "Id",
        "FullName",
        "Language",
        "CreationData",
        "Fingerprint",
        "SelfSignatureValid",
        "Valid",
        "ResponseStatus",
        "ResponseSignerId",
        "ResponseSignerName",
        "ResponseValid",
        "ResponseReason",
        "Used");

      foreach (var secureRequest in GetSignatureRequests())
      {
        var certificate = secureRequest.Certificate;
        Signed<SignatureResponse> signedResponse = null;
        var responseStatus = GetSignatureResponseStatus(secureRequest.Certificate.Id, out signedResponse);

        if (signedResponse != null &&
            signedResponse.Value.Signature != null)
        {
          certificate.AddSignature(signedResponse.Value.Signature);
        }

        var responseSignerId = signedResponse != null ? (object)signedResponse.Certificate.Id : null;
        var responseSignerName = signedResponse != null ? (object)signedResponse.Certificate.FullName : null;
        var responseValid = signedResponse != null ? (object)signedResponse.Verify(CertificateStorage) : null;
        var responseReason = signedResponse != null ? (object)signedResponse.Value.Reason : null;
        var group = (certificate as VoterCertificate) == null ? string.Empty : GetGroupName((certificate as VoterCertificate).GroupId);

        var used = this.votings.Values
          .SelectMany(v => v.GetAllEnvelopes())
          .Where(e => e.Certificate.Id.Equals(certificate.Id))
          .Count();

        AddRow(data,
          certificate.TypeText,
          group,
          certificate.Id,
          certificate.FullName,
          certificate.Language,
          certificate.CreationDate,
          certificate.Fingerprint,
          certificate.SelfSignatureValid,
          certificate.Validate(CertificateStorage, DateTime.Now),
          responseStatus,
          responseSignerId,
          responseSignerName,
          responseValid,
          responseReason,
          used);
      }

      return data.ToString();
    }

    private string GetVotingsStatistics()
    {
      var data = new StringBuilder();

      var certificates = new List<VoterCertificate>();

      foreach (var secureRequest in GetSignatureRequests())
      {
        var certificate = secureRequest.Certificate;
        Signed<SignatureResponse> signedResponse = null;
        var responseStatus = GetSignatureResponseStatus(secureRequest.Certificate.Id, out signedResponse);

        if (signedResponse != null &&
            signedResponse.Value.Signature != null)
        {
          certificate.AddSignature(signedResponse.Value.Signature);
        }

        if (certificate is VoterCertificate)
        {
          certificates.Add(certificate as VoterCertificate);
        }
      }

      AddRow(
          data,
          "Id",
          "Title",
          "Status",
          "VotingBeginDate",
          "VotingEndDate",
          "Group",
          "Questions",
          "Authority 1",
          "Authority 2",
          "Authority 3",
          "Authority 4",
          "Authority 5",
          "Valid Certificates",
          "Envelopes");

      foreach (var voting in this.votings.Values)
      {
        var validCertificates = certificates
          .Where(c => c.GroupId == voting.Parameters.GroupId && c.Validate(CertificateStorage, voting.Parameters.VotingBeginDate) == CertificateValidationResult.Valid)
          .Count();

        AddRow(
          data,
          voting.Id,
          voting.Parameters.Title.Text,
          voting.Status,
          voting.Parameters.VotingBeginDate,
          voting.Parameters.VotingEndDate,
          GetGroups().Where(g => g.Id == voting.Parameters.GroupId).Single().Name.Text,
          voting.Parameters.Questions.Count(),
          voting.Authorities.First().Id,
          voting.Authorities.Skip(1).First().Id,
          voting.Authorities.Skip(2).First().Id,
          voting.Authorities.Skip(3).First().Id,
          voting.Authorities.Skip(4).First().Id,
          validCertificates,
          voting.GetAllEnvelopes().Count());
      }

      return data.ToString();
    }

    private string GetSignatureRequestFullStatistics()
    {
      var data = new StringBuilder();

      AddRow(
        data,
        "Id",
        "Value");

      foreach (var secureRequest in GetSignatureRequests())
      {
        AddRow(
          data,
          secureRequest.Certificate.Id,
          secureRequest.ToBinary().ToHexString());
      }

      return data.ToString();
    }

    private string GetSignatureResponseFullStatistics()
    {
      var data = new StringBuilder();

      AddRow(
        data,
        "Id",
        "Value");

      foreach (var signedResponse in GetSignatureResponses())
      {
        AddRow(
          data,
          signedResponse.Certificate.Id,
          signedResponse.ToBinary().ToHexString());
      }

      return data.ToString();
    }

    private string GetBallotsStatistics()
    {
      var data = new StringBuilder();

      AddRow(
            data,
            "VotingId",
            "CertificateId",
            "CertificateFingerprint",
            "Date",
            "SignatureValid");

      foreach (var voting in this.votings.Values)
      {
        foreach (var envelope in voting.GetAllEnvelopes())
        {
          AddRow(
            data,
            voting.Id,
            envelope.Certificate.Id,
            envelope.Certificate.Fingerprint,
            envelope.Value.Date,
            envelope.Verify(CertificateStorage, envelope.Value.Date));
        }
      }

      return data.ToString();
    }

    private string GetTimelineStatistics()
    {
      var data = new StringBuilder();

      var from = new DateTime(2010, 9, 1, 0, 0, 0).Date;
      var until = DateTime.Now.Date;
      var voterCertificates = new List<VoterCertificate>();

      foreach (var secureRequest in GetSignatureRequests())
      {
        var certificate = secureRequest.Certificate;
        Signed<SignatureResponse> signedResponse = null;
        var responseStatus = GetSignatureResponseStatus(secureRequest.Certificate.Id, out signedResponse);

        if (signedResponse != null &&
            signedResponse.Value.Signature != null)
        {
          certificate.AddSignature(signedResponse.Value.Signature);
        }

        if (certificate is VoterCertificate)
        {
          voterCertificates.Add(certificate as VoterCertificate);
        }
      }

      AddRow(
        data,
        new string[] { "Day", "ActiveVotings" }
        .Concat(GetGroups().OrderBy(g => g.Id)
        .Select(g => new string[] { "Total in " + g.Name.Text, "Valid in " + g.Name.Text })
        .SelectMany(x => x)).ToArray());

      for (DateTime day = from; day <= until; day = day.AddDays(1))
      {
        var activeVotings = this.votings
          .Values.Where(v => v.Parameters.VotingBeginDate.Date >= day.Date && v.Parameters.VotingEndDate.Date <= day.Date)
          .Count();

        List<object> items = new List<object>();
        items.Add(day.ToShortDateString());
        items.Add(activeVotings);

        foreach (var group in GetGroups().OrderBy(g => g.Id))
        {
          items.Add(voterCertificates
            .Where(c => c.GroupId == group.Id && c.CreationDate.Date <= day)
            .Count());
          items.Add(voterCertificates
            .Where(c => c.GroupId == group.Id && c.Validate(CertificateStorage, day) == CertificateValidationResult.Valid)
            .Count());
        }

        AddRow(
          data,
          items.ToArray());
      }

      return data.ToString();
    }

    public string GetStatistics(StatisticsDataType type)
    {
      switch (type)
      {
        case StatisticsDataType.SignatureRequests:
          return GetSignatureRequestStatistics();
        case StatisticsDataType.SignatureRequestsFull:
          return GetSignatureRequestFullStatistics();
        case StatisticsDataType.SignatureResponsesFull:
          return GetSignatureResponseFullStatistics();
        case StatisticsDataType.Votings:
          return GetVotingsStatistics();
        case StatisticsDataType.Ballots:
          return GetBallotsStatistics();
        case StatisticsDataType.Timeline:
          return GetTimelineStatistics();
        default:
          return "Unknown statistics type.";
      }
    }

    /// <summary>
    /// Gets a voting from an id.
    /// </summary>
    /// <param name="id">Id of the voting.</param>
    /// <returns>Voting procedure entity.</returns>
    public VotingServerEntity GetVoting(Guid id)
    {
      if (!this.votings.ContainsKey(id))
        throw new PiArgumentException(ExceptionCode.NoVotingWithId, "No voting with that id.");

      return this.votings[id];
    }

    /// <summary>
    /// Set a signature request.
    /// </summary>
    /// <remarks>
    /// Add or replaces a signature request.
    /// </remarks>
    /// <param name="signatureRequest">Signed signature request.</param>
    public void SetSignatureRequest(
      IRpcConnection connection, 
      Secure<SignatureRequest> signatureRequest, 
      Secure<SignatureRequestInfo> signatureRequestInfo)
    {
      Guid id = signatureRequest.Certificate.Id;

      if (signatureRequest.Certificate.Id != signatureRequestInfo.Certificate.Id)
      {
        Logger.Log(LogLevel.Warning,
          "Connection {0}: Certificate id {1} (unverified) tried to set a signature request, but the certificate id on the request info was {2}.",
          connection.Id,
          signatureRequest.Certificate.Id.ToString(),
          signatureRequestInfo.Certificate.Id);
        throw new PiArgumentException(ExceptionCode.SignatureRequestInvalid, "Signature request invalid.");
      }

      if (!signatureRequest.VerifySimple())
      {
        Logger.Log(LogLevel.Warning,
          "Connection {0}: Certificate id {1} (unverified) tried to set a signature request, but the signature on the request was invalid.",
          connection.Id,
          signatureRequest.Certificate.Id.ToString());
        throw new PiArgumentException(ExceptionCode.SignatureRequestInvalid, "Signature request invalid.");
      }

      if (!signatureRequestInfo.VerifySimple())
      {
        Logger.Log(LogLevel.Warning,
          "Connection {0}: Certificate id {1} (unverified) tried to set a signature request, but the signature on the request info was invalid.",
          connection.Id,
          signatureRequest.Certificate.Id.ToString());
        throw new PiArgumentException(ExceptionCode.SignatureRequestInvalid, "Signature request invalid.");
      }

      SignatureRequestInfo requestInfo = signatureRequestInfo.Value.Decrypt(this.serverCertificate);

      if (!requestInfo.Valid)
      {
        Logger.Log(LogLevel.Warning,
          "Connection {0}: Certificate id {1} (unverified) tried to set a signature request, but the request data was invalid.",
          connection.Id,
          signatureRequest.Certificate.Id.ToString());
        throw new PiArgumentException(ExceptionCode.InvalidSignatureRequest, "Signature request data not valid.");
      }

      MySqlCommand replaceCommand = new MySqlCommand("REPLACE INTO signaturerequest (Id, Value, Info) VALUES (@Id, @Value, @Info)", DbConnection);
      replaceCommand.Parameters.AddWithValue("@Id", id.ToByteArray());
      replaceCommand.Parameters.AddWithValue("@Value", signatureRequest.ToBinary());
      replaceCommand.Parameters.AddWithValue("@Info", signatureRequestInfo.ToBinary());
      replaceCommand.ExecuteNonQuery();

      Logger.Log(LogLevel.Info, "Connection {0}: Signature request for certificate id {1} stored.", connection.Id, signatureRequest.Certificate.Id.ToString());

      MySqlCommand deleteCommand = new MySqlCommand("DELETE FROM signatureresponse WHERE Id = @Id", DbConnection);
      deleteCommand.Parameters.AddWithValue("@Id", id.ToByteArray());
      deleteCommand.ExecuteNonQuery();

      if (signatureRequest.Certificate is AuthorityCertificate)
      {
        CertificateStorage.Add(signatureRequest.Certificate);
      }

      if (!requestInfo.EmailAddress.IsNullOrEmpty())
      {
        SendMail(
          requestInfo.EmailAddress,
          MailType.VoterRequestDeposited,
          requestInfo.EmailAddress,
          signatureRequest.Certificate.Id.ToString(),
          CertificateTypeText(signatureRequest.Certificate, Language.English),
          CertificateTypeText(signatureRequest.Certificate, Language.German),
          CertificateTypeText(signatureRequest.Certificate, Language.French));
      }

      SendMail(
        this.serverConfig.MailAdminAddress,
        MailType.AdminNewRequest,
        requestInfo.EmailAddress.IsNullOrEmpty() ? "?@?.?" : requestInfo.EmailAddress,
        signatureRequest.Certificate.Id.ToString(),
        signatureRequest.Certificate.TypeText);
    }

    /// <summary>
    /// Send a mail.
    /// </summary>
    /// <param name="address">Address to send to.</param>
    /// <param name="mailType">Type of mail to send.</param>
    /// <param name="arguments">Arguments to add.</param>
    public void SendMail(string address, MailType mailType, params object[] arguments)
    {
      var texts = ServerConfig.GetMailText(mailType, Logger);
      string body = string.Format(texts.Second, arguments);
      Mailer.TrySend(address, texts.First, body);
      Logger.Log(LogLevel.Info, "Sending message of type {0} to {1}.", mailType, address);
    }

    /// <summary>
    /// Get the list of open signature requests.
    /// </summary>
    /// <returns>List of signature request ids.</returns>
    public List<Guid> GetSignatureRequestList()
    {
      MySqlDataReader reader = DbConnection
        .ExecuteReader("SELECT Id FROM signaturerequest WHERE NOT Id IN (SELECT Id FROM signatureresponse)");
      List<Guid> signatureRequestList = new List<Guid>();

      while (reader.Read())
      {
        signatureRequestList.Add(reader.GetGuid(0));
      }

      reader.Close();

      return signatureRequestList;
    }

    /// <summary>
    /// Get a signature request.
    /// </summary>
    /// <param name="id">Id of the signature request.</param>
    /// <returns>Signed signature request.</returns>
    public Secure<SignatureRequest> GetSignatureRequest(Guid id)
    {
      MySqlDataReader reader = DbConnection
        .ExecuteReader("SELECT Value FROM signaturerequest WHERE Id = @Id",
        "@Id", id.ToByteArray());

      if (reader.Read())
      {
        byte[] signatureRequestData = reader.GetBlob(0);
        reader.Close();
        return Serializable.FromBinary<Secure<SignatureRequest>>(signatureRequestData);
      }
      else
      {
        reader.Close();
        throw new PiArgumentException(ExceptionCode.SignatureRequestNotFound, "Signature request not found.");
      }
    }

    /// <summary>
    /// Gets all signature requests.
    /// </summary>
    /// <returns>List of signed signature request.</returns>
    public IEnumerable<Secure<SignatureRequest>> GetSignatureRequests()
    {
      MySqlDataReader reader = DbConnection
        .ExecuteReader("SELECT Value FROM signaturerequest");

      List<Secure<SignatureRequest>> requests = new List<Secure<SignatureRequest>>();
        
      while (reader.Read())
      {
        byte[] signatureRequestData = reader.GetBlob(0);
        requests.Add( Serializable.FromBinary<Secure<SignatureRequest>>(signatureRequestData));
      }

      reader.Close();

      return requests;
    }

    /// <summary>
    /// Gets all signature responses.
    /// </summary>
    /// <returns>List of signed signature responses.</returns>
    public IEnumerable<Signed<SignatureResponse>> GetSignatureResponses()
    {
      MySqlDataReader reader = DbConnection
        .ExecuteReader("SELECT Value FROM signatureresponse");

      List<Signed<SignatureResponse>> requests = new List<Signed<SignatureResponse>>();

      while (reader.Read())
      {
        byte[] signatureRequestData = reader.GetBlob(0);
        requests.Add(Serializable.FromBinary<Signed<SignatureResponse>>(signatureRequestData));
      }

      reader.Close();

      return requests;
    }
    
    /// <summary>
    /// Get a signature request info.
    /// </summary>
    /// <param name="id">Id of the signature request.</param>
    /// <returns>Signed signature request.</returns>
    public Secure<SignatureRequestInfo> GetSignatureRequestInfo(Guid id)
    {
      MySqlDataReader reader = DbConnection
        .ExecuteReader("SELECT Info FROM signaturerequest WHERE Id = @Id",
        "@Id", id.ToByteArray());

      if (reader.Read())
      {
        byte[] signatureRequestData = reader.GetBlob(0);
        reader.Close();
        return Serializable.FromBinary<Secure<SignatureRequestInfo>>(signatureRequestData);
      }
      else
      {
        reader.Close();
        throw new PiArgumentException(ExceptionCode.SignatureRequestNotFound, "Signature request not found.");
      }
    }

    /// <summary>
    /// Get an encrypted signature request
    /// </summary>
    /// <param name="id">Id of the signature request.</param>
    /// <returns>Encrypted signature request data.</returns>
    public byte[] GetEncryptedSignatureRequest(Guid id)
    {
      var secureRequest = GetSignatureRequestInfo(id);

      return secureRequest.Value.Decrypt(this.serverCertificate).EncryptedSignatureRequest;
    }

    /// <summary>
    /// Get a signature request.
    /// </summary>
    /// <param name="id">Id of the signature request.</param>
    /// <returns>Is there a signature request.</returns>
    public bool HasSignatureRequest(Guid id)
    {
      return DbConnection
        .ExecuteHasRows("SELECT count(*) FROM signaturerequest WHERE Id = @Id",
        "@Id", id.ToByteArray());
    }

    /// <summary>
    /// Get the status and perhaps response regarding as signature request.
    /// </summary>
    /// <param name="certificateId">Id of the certificate.</param>
    /// <param name="signatureResponse">Signed signature response.</param>
    /// <returns>Status of the signature response.</returns>
    public SignatureResponseStatus GetSignatureResponseStatus(Guid certificateId, out Signed<SignatureResponse> signatureResponse)
    {
      MySqlCommand selectResponseCommand = new MySqlCommand("SELECT Value FROM signatureresponse WHERE Id = @Id", DbConnection);
      selectResponseCommand.Parameters.AddWithValue("@Id", certificateId.ToByteArray());
      MySqlDataReader selectResponseReader = selectResponseCommand.ExecuteReader();

      if (selectResponseReader.Read())
      {
        byte[] signatureResponseData = selectResponseReader.GetBlob(0);
        selectResponseReader.Close();
        signatureResponse = Serializable.FromBinary<Signed<SignatureResponse>>(signatureResponseData);
        return signatureResponse.Value.Status;
      }
      else
      {
        selectResponseReader.Close();

        MySqlCommand selectRequestCommand = new MySqlCommand("SELECT count(*) FROM signaturerequest WHERE Id = @Id", DbConnection);
        selectRequestCommand.Parameters.AddWithValue("@Id", certificateId.ToByteArray());

        if (selectRequestCommand.ExecuteHasRows())
        {
          signatureResponse = null;
          return SignatureResponseStatus.Pending;
        }
        else
        {
          signatureResponse = null;
          return SignatureResponseStatus.Unknown;
        }
      }
    }

    private void OutputReport()
    {
      Console.Write("Building report");

      StringBuilder report = new StringBuilder();
      Dictionary<Guid, int> voteCounts = new Dictionary<Guid,int>();
      var certs = GetCertificates();

      Console.Write(".");

      foreach (var item in certs)
      {
        voteCounts.Add(item.First.Id, 0);
      }

      Console.Write(".");

      foreach (var voting in this.votings.Values)
      {
        for (int envelopeIndex = 0; envelopeIndex < voting.GetEnvelopeCount(); envelopeIndex++)
        {
          var envelope = voting.GetEnvelope(envelopeIndex);
          voteCounts[envelope.Certificate.Id]++;
        }
      }

      Console.Write(".");

      report.AppendLine("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10}",
        "Type",
        "Id",
        "Name",
        "Fingerprint",
        "SelfSignatureValid",
        "Validate",
        "CreationDate",
        "Signatures",
        "ExpectedValidFrom",
        "ExpectedValidUntil",
        "VoteCount");

      foreach (var item in certs)
      {
        report.AppendLine("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10}",
          item.First.TypeText,
          item.First.Id,
          item.First.FullName,
          item.First.Fingerprint,
          item.First.SelfSignatureValid,
          item.First.Validate(CertificateStorage),
          item.First.CreationDate.ToString("yyyy-MM-dd"),
          item.First.Signatures.Count(),
          item.First.ExpectedValidFrom(CertificateStorage).ToString("yyyy-MM-dd"),
          item.First.ExpectedValidUntil(CertificateStorage, DateTime.Now).ToString("yyyy-MM-dd"),
          voteCounts[item.First.Id]);
      }

      Console.Write(".");

      File.WriteAllText(Path.Combine(System.Windows.Forms.Application.StartupPath, "report.csv"), report.ToString());

      Console.WriteLine("Done");

    }

    /// <summary>
    /// Get all certificates.
    /// </summary>
    /// <returns>Tuples of certificate, validation result and email address.</returns>
    public IEnumerable<Tuple<Certificate, CertificateValidationResult, string>> GetCertificates()
    {
      var signatureResponse = new Dictionary<Guid, SignatureResponse>();
      foreach (var signedSignatureResponse in QuerySignatureResponses())
      {
        if (signedSignatureResponse.Verify(CertificateStorage))
        {
          signatureResponse.Add(signedSignatureResponse.Value.SubjectId, signedSignatureResponse.Value);
        }
      }

      foreach (var secureSignatureRequestInfo in QuerySignatureRequests())
      {
        var certificate = secureSignatureRequestInfo.Certificate;
        var signatureRequestInfo = secureSignatureRequestInfo.Value.Decrypt(this.serverCertificate);
        var emailAddress = string.IsNullOrEmpty(signatureRequestInfo.EmailAddress) ? null : signatureRequestInfo.EmailAddress;

        if (signatureResponse.ContainsKey(certificate.Id))
        {
          var response = signatureResponse[certificate.Id];

          if (response.Status == SignatureResponseStatus.Accepted &&
              response.Signature != null)
          {
            certificate.AddSignature(response.Signature);
          }
        }

        yield return new Tuple<Certificate, CertificateValidationResult, string>(certificate, certificate.Validate(CertificateStorage), emailAddress);
      }
    }

    /// <summary>
    /// Query signature responses.
    /// </summary>
    /// <returns>Signed signature responses</returns>
    private IEnumerable<Signed<SignatureResponse>> QuerySignatureResponses()
    {
      MySqlCommand selectCommand = new MySqlCommand("SELECT Value FROM signatureresponse", DbConnection);
      MySqlDataReader selectReader = selectCommand.ExecuteReader();
      List<Signed<SignatureResponse>> list = new List<Signed<SignatureResponse>>();

      while (selectReader.Read())
      {
        var requestInfo = Serializable.FromBinary<Signed<SignatureResponse>>(selectReader.GetBlob(0));
        list.Add(requestInfo);
      }

      selectReader.Close();
      return list;
    }

    /// <summary>
    /// Query signature requests.
    /// </summary>
    /// <returns>Secure signature requests.</returns>
    private IEnumerable<Secure<SignatureRequestInfo>> QuerySignatureRequests()
    {
      MySqlCommand selectCommand = new MySqlCommand("SELECT Info FROM signaturerequest", DbConnection);
      MySqlDataReader selectReader = selectCommand.ExecuteReader();
      List<Secure<SignatureRequestInfo>> list = new List<Secure<SignatureRequestInfo>>();

      while (selectReader.Read())
      {
        var requestInfo = Serializable.FromBinary<Secure<SignatureRequestInfo>>(selectReader.GetBlob(0));
        list.Add(requestInfo);
      }

      selectReader.Close();
      return list;
    }

    /// <summary>
    /// Sets a signature response.
    /// </summary>
    /// <param name="signatureRequestId">Id of the corresponding request.</param>
    /// <param name="signedSignatureResponse">Signed signature response.</param>
    public void SetSignatureResponse(
      IRpcConnection connection,
      Signed<SignatureResponse> signedSignatureResponse)
    {
      if (!signedSignatureResponse.Verify(CertificateStorage))
      {
        if (signedSignatureResponse.VerifySimple())
        {
          Logger.Log(LogLevel.Warning,
            "Connection {0}: CA id {1} (unverified) tried to set a signature response, but his certificate status was {2}.",
            connection.Id,
            signedSignatureResponse.Certificate.Id.ToString(),
            signedSignatureResponse.Certificate.Validate(CertificateStorage).Text());
        }
        else
        {
          Logger.Log(LogLevel.Warning,
            "Connection {0}: CA id {1} (unverified) tried to set a signature response, but the signature was invalid.",
            connection.Id,
            signedSignatureResponse.Certificate.Id.ToString());
        }

        throw new PiSecurityException(ExceptionCode.InvalidSignature, "Signature response has invalid signature.");
      }

      if (!(signedSignatureResponse.Certificate is CACertificate))
      {
        Logger.Log(LogLevel.Warning,
          "Connection {0}: CA id {1} (unverified) tried to set a signature response, but it's not a CA.",
          connection.Id,
          signedSignatureResponse.Certificate.Id.ToString());
        throw new PiSecurityException(ExceptionCode.SignatureResponseNotFromCA, "Signature response not from proper CA.");
      }
      
      SignatureResponse signatureResponse = signedSignatureResponse.Value;

      MySqlCommand replaceCommand = new MySqlCommand("REPLACE INTO signatureresponse (Id, Value) VALUES (@Id, @Value)", DbConnection);
      replaceCommand.Parameters.AddWithValue("@Id", signatureResponse.SubjectId.ToByteArray());
      replaceCommand.Parameters.AddWithValue("@Value", signedSignatureResponse.ToBinary());
      replaceCommand.ExecuteNonQuery();

      Logger.Log(LogLevel.Info, "Connection {0}: Signature response for certificate id {1} stored on behalf of CA id {2}.", connection.Id, signatureResponse.SubjectId.ToString(), signedSignatureResponse.Certificate.Id.ToString());

      if (signatureResponse.Status == SignatureResponseStatus.Accepted)
      {
        if (CertificateStorage.Has(signatureResponse.SubjectId))
        {
          Certificate certificate = CertificateStorage.Get(signatureResponse.SubjectId);
          certificate.AddSignature(signatureResponse.Signature);
          CertificateStorage.Add(certificate);
        }

        Secure<SignatureRequestInfo> secureSignatureRequestInfo = GetSignatureRequestInfo(signatureResponse.SubjectId);
        SignatureRequestInfo signatureRequestInfo = secureSignatureRequestInfo.Value.Decrypt(this.serverCertificate);
        if (!signatureRequestInfo.EmailAddress.IsNullOrEmpty())
        {
          SendMail(
            signatureRequestInfo.EmailAddress,
            MailType.VoterRequestApproved,
            signatureRequestInfo.EmailAddress,
            secureSignatureRequestInfo.Certificate.Id.ToString(),
            CertificateTypeText(secureSignatureRequestInfo.Certificate, Language.English),
            CertificateTypeText(secureSignatureRequestInfo.Certificate, Language.German),
            CertificateTypeText(secureSignatureRequestInfo.Certificate, Language.French));
        }
      }
      else
      {
        Secure<SignatureRequestInfo> secureSignatureRequestInfo = GetSignatureRequestInfo(signatureResponse.SubjectId);
        SignatureRequestInfo signatureRequestInfo = secureSignatureRequestInfo.Value.Decrypt(this.serverCertificate);
        if (!signatureRequestInfo.EmailAddress.IsNullOrEmpty())
        {
          SendMail(
            signatureRequestInfo.EmailAddress,
            MailType.VoterRequestDeclined,
            signatureRequestInfo.EmailAddress,
            secureSignatureRequestInfo.Certificate.Id.ToString(),
            CertificateTypeText(secureSignatureRequestInfo.Certificate, Language.English),
            CertificateTypeText(secureSignatureRequestInfo.Certificate, Language.German),
            CertificateTypeText(secureSignatureRequestInfo.Certificate, Language.French),
            signatureResponse.Reason);
        }
      }
    }

    /// <summary>
    /// Get certificate type text in the selected language.
    /// </summary>
    /// <param name="certificate">Certificate in question.</param>
    /// <param name="language">Desired language.</param>
    /// <returns>Type text.</returns>
    private string CertificateTypeText(Certificate certificate, Language language)
    {
      if (certificate is VoterCertificate)
      {
        return LibraryResources.ResourceManager.GetString("CertificateTypeVoter", language.ToCulture());
      }
      else if (certificate is AdminCertificate)
      {
        return LibraryResources.ResourceManager.GetString("CertificateTypeAdmin", language.ToCulture());
      }
      else if (certificate is AuthorityCertificate)
      {
        return LibraryResources.ResourceManager.GetString("CertificateTypeAuthority", language.ToCulture());
      }
      else if (certificate is NotaryCertificate)
      {
        return LibraryResources.ResourceManager.GetString("CertificateTypeNotary", language.ToCulture());
      }
      else if (certificate is ServerCertificate)
      {
        return LibraryResources.ResourceManager.GetString("CertificateTypeServer", language.ToCulture());
      }
      else if (certificate is CACertificate)
      {
        return LibraryResources.ResourceManager.GetString("CertificateTypeCA", language.ToCulture());
      }
      else
      {
        return LibraryResources.ResourceManager.GetString("CertificateTypeUnknown", language.ToCulture());
      }
    }

    /// <summary>
    /// Get all valid authority certificates in storage.
    /// </summary>
    /// <returns>List of authority certificates.</returns>
    public List<AuthorityCertificate> GetValidAuthorityCertificates()
    {
      List<AuthorityCertificate> authorityCertificates = new List<AuthorityCertificate>();

      Console.WriteLine();
      Console.WriteLine();

      foreach (Certificate certificate in CertificateStorage.Certificates)
      {
        if (certificate is AuthorityCertificate &&
          certificate.Validate(CertificateStorage) == CertificateValidationResult.Valid)
        {
          authorityCertificates.Add((AuthorityCertificate)certificate);
        }
      }

      Console.WriteLine();

      return authorityCertificates;
    }

    /// <summary>
    /// Add a certificate storage to the server's data.
    /// </summary>
    /// <remarks>
    /// Used to add new CRLs.
    /// </remarks>
    /// <param name="certificateStorage">Certificate storage to add.</param>
    public void AddCertificateStorage(
      IRpcConnection connection,
      CertificateStorage certificateStorage)
    {
      if (!certificateStorage.SignedRevocationLists.All(crl => crl.Certificate is CACertificate &&
                                                               crl.Value.IssuerId.Equals(crl.Certificate.Id) &&
                                                               crl.Verify(CertificateStorage)))
      {
        Logger.Log(LogLevel.Warning,
          "Connection {0}: Add certificate storage failed; Signature on CRL or issuer not valid.",
          connection.Id);
        throw new PiSecurityException(ExceptionCode.InvalidSignature, "Signature on CRL or issuer not valid.");
      }

      CertificateStorage.Add(certificateStorage.SignedRevocationLists);

      var validCertificates = certificateStorage.Certificates
        .Where(certificate => certificate.Validate(CertificateStorage) == CertificateValidationResult.Valid &&
                              certificate.AllSignaturesValid(CertificateStorage));
      CertificateStorage.Add(validCertificates);

      Logger.Log(LogLevel.Info,
        "Connection {0}: Certificate storage added.",
        connection.Id);
    }

    /// <summary>
    /// Called at regular intervals.
    /// Has to keep the DB connection alive.
    /// </summary>
    public override void Process()
    {
      int certificateCount = CertificateStorage.Certificates.Count();
      this.votings.Values.Foreach(voting => voting.Process());
      RemindAdmin();
      CheckSendStatusReport();
      this.lastProcess = DateTime.Now;
      Logger.Log(LogLevel.Debug, "Processing.", certificateCount);
    }

    /// <summary>
    /// Check if one should send status report now.
    /// </summary>
    private void CheckSendStatusReport()
    {
      if (DateTime.Now.Hour > this.lastProcess.Hour &&
      DateTime.Now.Hour == 20 &&
      (DateTime.Now.DayOfWeek == DayOfWeek.Saturday ||
      this.votings.Values.Any(voting => voting.Status != VotingStatus.Finished ||
                              voting.Parameters.VotingEndDate.AddDays(7) >= DateTime.Now)))
      {
        SendStatusReport();
      }
    }

    /// <summary>
    /// Send the status report.
    /// </summary>
    private void SendStatusReport()
    {
      try
      {
        StringBuilder report = new StringBuilder();

        var certificateList = GetCertificates();

        var authorityCertificateList = certificateList
          .Where(item => item.First is AuthorityCertificate);
        report.AppendLine("Authorities");

        foreach (var item in authorityCertificateList.GroupBy(x => x.Second))
        {
          report.AppendLine("{0}: {1}", item.Key.ToString(), item.Count());
        }

        report.AppendLine();

        var notaryCertificateList = certificateList
          .Where(item => item.First is NotaryCertificate);
        report.AppendLine("Notaries");

        foreach (var item in notaryCertificateList.GroupBy(x => x.Second))
        {
          report.AppendLine("{0}: {1}", item.Key.ToString(), item.Count());
        }

        report.AppendLine();

        var voterCertificateList = certificateList
          .Where(item => item.First is VoterCertificate);

        foreach (var item2 in voterCertificateList.GroupBy(item2 => ((VoterCertificate)item2.First).GroupId))
        {
          report.AppendLine(GetGroupName(item2.Key));

          foreach (var item in item2.GroupBy(x => x.Second))
          {
            report.AppendLine("{0}: {1}", item.Key.ToString(), item.Count());
          }

          report.AppendLine();
        }

        foreach (var voting in this.votings.Values)
        {
          if (voting.Status != VotingStatus.Finished ||
              voting.Parameters.VotingEndDate.AddDays(7) >= DateTime.Now)
          {
            voting.AddStatusReport(report);
            report.AppendLine();
          }
        }

        SendMail(ServerConfig.MailAdminAddress, MailType.AdminStatusReport, report.ToString());
      }
      catch (Exception exception)
      {
        Logger.Log(LogLevel.Error, "Sending status report failed:\n{0}", exception.ToString());
      }
    }

    /// <summary>
    /// Remind authorities to do their duties.
    /// </summary>
    private void RemindAdmin()
    {
      if (DateTime.Now.Hour > this.lastProcess.Hour)
      {
        var maxValid = CertificateStorage.SignedRevocationLists
          .Where(crl => crl.Verify(CertificateStorage))
          .Select(crl => crl.Value.ValidUntil.Date)
          .Max();
        var today = DateTime.Now.Date;

        if (today > maxValid &&
            DateTime.Now.Hour % 3 == 0)
        {
          SendMail(
            ServerConfig.MailAdminAddress,
            MailType.AdminCrlRed,
            maxValid.ToLongDateString());
        }
        else if (today > maxValid.AddDays(-2) &&
                 DateTime.Now.Hour % 12 == 6)
        {
          SendMail(
            ServerConfig.MailAdminAddress,
            MailType.AdminCrlOrange,
            maxValid.ToLongDateString());
        }
        else if (today > maxValid.AddDays(-5) &&
                 DateTime.Now.Hour == 18)
        {
          SendMail(
            ServerConfig.MailAdminAddress,
            MailType.AdminCrlGreen,
            maxValid.ToLongDateString());
        }
      }
    }

    /// <summary>
    /// Called from time to time when idle.
    /// </summary>
    public override void Idle()
    {
    }

    /// <summary>
    /// List all groups in database.
    /// </summary>
    /// <returns>List of groups.</returns>
    public List<Group> GetGroups()
    {
      MySqlDataReader reader = DbConnection
        .ExecuteReader("SELECT Id, NameEnglish, NameGerman, NameFrench, NameItalien FROM votinggroup");
      List<Group> groupList = new List<Group>();

      while (reader.Read())
      {
        MultiLanguageString name = new MultiLanguageString();
        name.Set(Language.English, reader.GetString(1));
        name.Set(Language.German, reader.GetString(2));
        name.Set(Language.French, reader.GetString(3));
        name.Set(Language.Italien, reader.GetString(4));
        groupList.Add(new Group(reader.GetInt32(0), name));
      }

      reader.Close();

      return groupList;
    }

    /// <summary>
    /// Get group name.
    /// </summary>
    /// <returns>Name of the group.</returns>
    public string GetGroupName(int groupId)
    {
      MySqlDataReader reader = DbConnection
        .ExecuteReader("SELECT NameEnglish FROM votinggroup WHERE Id = @Id",
        "@Id", groupId);

      if (reader.Read())
      {
        var groupName = reader.GetString(0);
        reader.Close();
        return groupName;
      }
      else
      {
        return "Unknown Group";
      }
    }

    /// <summary>
    /// Add a sign check.
    /// </summary>
    /// <param name="signedSignCheck">Signed sign check.</param>
    public void AddSignatureRequestSignCheck(
      IRpcConnection connection,
      Signed<SignatureRequestSignCheck> signedSignCheck)
    {
      if (!signedSignCheck.Verify(CertificateStorage))
      {
        if (signedSignCheck.VerifySimple())
        {
          Logger.Log(LogLevel.Warning, 
            "Connection {0}: Server id {1} (unverified) tried to add a sign check, but his certificate status was {2}.", 
            connection.Id, 
            signedSignCheck.Certificate.Id.ToString(), 
            signedSignCheck.Certificate.Validate(CertificateStorage).Text());
        }
        else
        {
          Logger.Log(LogLevel.Warning,
            "Connection {0}: Server id {1} (unverified) tried to add a sign check, but the signature was invalid.", 
            connection.Id, 
            signedSignCheck.Certificate.Id.ToString());
        }

        throw new PiSecurityException(ExceptionCode.InvalidSignature, "Signature on sign check invalid.");
      }

      if (!(signedSignCheck.Certificate is ServerCertificate))
      {
        Logger.Log(LogLevel.Warning,
          "Connection {0}: Server id {1} (verified) tried to add a sign check, but he is not a server.",
          connection.Id, 
          signedSignCheck.Certificate.Id.ToString());
        throw new PiSecurityException(ExceptionCode.SignCheckNotFromServer, "Signature on sign check not from server.");
      }

      var signCheck = signedSignCheck.Value;
      Signed<SignatureResponse> signatureResponse = null;
      var status = GetSignatureResponseStatus(signCheck.Certificate.Id, out signatureResponse);

      if (status != SignatureResponseStatus.Pending)
      {
        Logger.Log(LogLevel.Warning, 
          "Connection {0}: Notary id {1} (unverified) tried to a sign check to id {2}, but its status was {3}.", 
          connection.Id,
          signCheck.Cookie.Certificate.Id.ToString(), 
          signCheck.Certificate.Id.ToString(),
          status.ToString());
        throw new PiException(ExceptionCode.SignCheckResponseStateMismatch, "Signature response status mismatch.");
      }

      if (!signCheck.Cookie.Verify(CertificateStorage))
      {
        if (signCheck.Cookie.VerifySimple())
        {
          Logger.Log(LogLevel.Warning, 
            "Connection {0}: Notary id {1} (unverified) tried to a sign check to id {2}, but his certificate status was {3}.",
            connection.Id, 
            signCheck.Cookie.Certificate.Id.ToString(), 
            signCheck.Certificate.Id.ToString(), 
            signCheck.Cookie.Certificate.Validate(CertificateStorage).Text());
        }
        else
        {
          Logger.Log(LogLevel.Warning,
            "Connection {0}: Notary id {1} (unverified) tried to a sign check to id {2}, but the signature was invalid.",
            connection.Id,
            signCheck.Cookie.Certificate.Id.ToString(),
            signCheck.Certificate.Id.ToString());
        }

        throw new PiSecurityException(ExceptionCode.SignCheckCookieSignatureInvalid, "Signature on sign check cookie invalid.");
      }

      if (!(signCheck.Cookie.Certificate is AuthorityCertificate ||
            signCheck.Cookie.Certificate is NotaryCertificate))
      {
        Logger.Log(LogLevel.Warning,
          "Connection {0}: Notary id {1} (unverified) tried to a sign check to id {2}, but he is not an authority or notary.", 
          connection.Id,
          signCheck.Cookie.Certificate.Id.ToString(),
          signCheck.Certificate.Id.ToString());
        throw new PiSecurityException(ExceptionCode.SignCheckCookieNotFromNotary, "Signature on sign check cookie not from notary.");
      }

      Signed<SignCheckCookie> dbCookie = null;

      var reader = DbConnection.ExecuteReader(
        "SELECT Cookie FROM signcheckcookie WHERE NotaryId = @NotaryId",
        "@NotaryId",
        signCheck.Cookie.Certificate.Id.ToByteArray());

      if (reader.Read())
      {
        dbCookie = Serializable.FromBinary<Signed<SignCheckCookie>>(reader.GetBlob(0));
        reader.Close();
      }
      else
      {
        reader.Close();
        throw new PiSecurityException(ExceptionCode.SignCheckCookieNotFound, "Sign check cookie not found.");
      }

      if (dbCookie.Certificate.Fingerprint != signCheck.Cookie.Certificate.Fingerprint)
        throw new PiSecurityException(ExceptionCode.SignCheckCookieFingerprintMismatch, "Fingerprint on sign check cookie does not match.");
      if (!dbCookie.Value.Randomness.Equal(signCheck.Cookie.Value.Randomness))
        throw new PiSecurityException(ExceptionCode.SignCheckCookieRandomnessMismatch, "Randomness of sign check cookie does not match.");

      MySqlCommand insertCommand = new MySqlCommand("INSERT INTO signcheck (CertificateId, Value) VALUES (@CertificateId, @Value)", DbConnection);
      insertCommand.Parameters.AddWithValue("@CertificateId", signedSignCheck.Value.Certificate.Id.ToByteArray());
      insertCommand.Parameters.AddWithValue("@Value", signedSignCheck.ToBinary());
      insertCommand.ExecuteNonQuery();

      Logger.Log(LogLevel.Info,
        "Connection {0}: Notary {1}, {2} has signed signature request {3}", 
        connection.Id,
        signCheck.Cookie.Certificate.Id.ToString(), 
        signCheck.Cookie.Certificate.FullName, 
        signCheck.Certificate.Id.ToString());
    }

    /// <summary>
    /// List all sign checks.
    /// </summary>
    /// <param name="certificateId">Id of certificate in question.</param>
    /// <returns>List of sign checks.</returns>
    public IEnumerable<Signed<SignatureRequestSignCheck>> GetSignatureRequestSignChecks(Guid certificateId)
    {
      List<Signed<SignatureRequestSignCheck>> signChecks = new List<Signed<SignatureRequestSignCheck>>();

      MySqlDataReader reader = DbConnection.ExecuteReader(
        "SELECT Value FROM signcheck WHERE CertificateId = @CertificateId",
        "@CertificateId", certificateId.ToByteArray());

      while (reader.Read())
      {
        byte[] signCheckData = reader.GetBlob(0);
        signChecks.Add(Serializable.FromBinary<Signed<SignatureRequestSignCheck>>(signCheckData));
      }

      reader.Close();

      return signChecks;
    }

    public Signed<SignCheckCookie> GetSignCheckCookie(
      IRpcConnection connection,
      Guid notaryId, 
      byte[] code)
    {
      var reader = this.DbConnection.ExecuteReader(
        "SELECT Cookie, Code, Expires FROM signcheckcookie WHERE NotaryId = @NotaryId",
        "@NotaryId",
        notaryId.ToByteArray());

      if (reader.Read())
      {
        if (DateTime.Now <= reader.GetDateTime(2))
        {
          if (reader.GetBlob(1).Equal(code))
          {
            var signedCookie = Serializable.FromBinary<Signed<SignCheckCookie>>(reader.GetBlob(0));
            reader.Close();
            Logger.Log(LogLevel.Info,
              "Connection {0}: Notary id {1} (unverified) got his sign check cookie.",
              connection.Id,
              notaryId.ToString());
            return signedCookie;
          }
          else
          {
            Logger.Log(LogLevel.Warning,
              "Connection {0}: Notary id {1} (unverified) tried to get his sign check cookie, but the code was invalid.",
              connection.Id,
              notaryId.ToString());
            reader.Close();
            throw new PiSecurityException(ExceptionCode.SignCheckCookieCodeWrong, "Sign check cookie code wrong.");
          }
        }
        else
        {
          Logger.Log(LogLevel.Warning,
            "Connection {0}: Notary id {1} (unverified) tried to get his sign check cookie, the this had already run out.",
            connection.Id,
            notaryId.ToString());
          reader.Close();
          throw new PiSecurityException(ExceptionCode.SignCheckCookieCodeExpired, "Sign check cookie code has expired.");
        }
      }
      else
      {
        Logger.Log(LogLevel.Warning,
          "Connection {0}: Notary id {1} (unverified) tried to get his sign check cookie, but it was not found.",
          connection.Id,
          notaryId.ToString());
        throw new PiException(ExceptionCode.SignCheckCookieNotFound, "Sign check cookie not found.");
      }
    }

    public byte[] SetSignCheckCookie(
      IRpcConnection connection,
      Signed<SignCheckCookie> signedCookie)
    {
      if (!signedCookie.Verify(CertificateStorage))
      {
        if (signedCookie.VerifySimple())
        {
          Logger.Log(LogLevel.Warning,
            "Connection {0}: Notary id {1} (unverified) tried to set his sign check cookie, but his certificate status was {2}.",
            connection.Id,
            signedCookie.Certificate.Id.ToString(),
            signedCookie.Certificate.Validate(CertificateStorage).Text());
        }
        else
        {
          Logger.Log(LogLevel.Warning,
            "Connection {0}: Notary id {1} (unverified) tried to set his sign check cookie, but the signature was invalid.",
            connection.Id,
            signedCookie.Certificate.Id.ToString());
        }

        throw new PiSecurityException(ExceptionCode.InvalidSignature, "Invalid signature.");
      }

      if (!(signedCookie.Certificate is AuthorityCertificate ||
            signedCookie.Certificate is NotaryCertificate))
      {
        Logger.Log(LogLevel.Warning,
          "Connection {0}: Notary id {0} (verified) tried to set his sign check cookie, but he is no authority or notary.", 
          connection.Id,
          signedCookie.Certificate.Id.ToString());
        throw new PiSecurityException(ExceptionCode.SignCheckCookieNotFromNotary, "Not from proper authority or notary.");
      }

      this.DbConnection.ExecuteNonQuery(
        "DELETE FROM signcheckcookie WHERE NotaryId = @NotaryId",
        "@NotaryId", 
        signedCookie.Certificate.Id.ToByteArray());

      var rng = RandomNumberGenerator.Create();
      byte[] code = new byte[32];
      rng.GetBytes(code);
      byte[] encryptedCode = signedCookie.Certificate.Encrypt(code);

      MySqlCommand insertCommand = new MySqlCommand(
        "INSERT INTO signcheckcookie (NotaryId, Cookie, Code, Expires) VALUES (@NotaryId, @Cookie, @Code, @Expires)", 
        this.DbConnection);
      insertCommand.Parameters.AddWithValue("@NotaryId", signedCookie.Certificate.Id.ToByteArray());
      insertCommand.Parameters.AddWithValue("@Cookie", signedCookie.ToBinary());
      insertCommand.Parameters.AddWithValue("@Code", code);
      insertCommand.Parameters.AddWithValue("@Expires", DateTime.Now.AddMinutes(15).ToString("yyyy-MM-dd HH:mm:ss"));
      insertCommand.ExecuteNonQuery();

      Logger.Log(LogLevel.Info,
        "Connection {0}: Notary id {1} (verified) has set his sign check cookie with code {2}.",
        connection.Id,
        signedCookie.Certificate.Id.ToString(),
        code.ToHexString());

      return encryptedCode;
    }
  }
}
