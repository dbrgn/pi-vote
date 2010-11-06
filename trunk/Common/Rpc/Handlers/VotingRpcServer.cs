/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Crypto;
using MySql.Data;
using MySql.Data.MySqlClient;

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
    /// MySQL database connection.
    /// </summary>
    private MySqlConnection dbConnection;
    
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
      this.logger = new Logger(Pirate.PiVote.Logger.ServerLogFileName, LogLevel.Debug);
      Logger.Log(LogLevel.Info, "Voting RPC server starting...");

      this.serverConfig = new ServerConfig(ServerConfigFileName);
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
    /// <param name="requestData">Serialized request data.</param>
    /// <returns>Serialized response data</returns>
    public override byte[] Execute(byte[] requestData)
    {
      Logger.Log(LogLevel.Debug, "Receiving request of {0} bytes.", requestData.Length);

      var request = Serializable.FromBinary<RpcRequest<VotingRpcServer>>(requestData);
      var response = request.TryExecute(this);

      byte[] responseData = response.ToBinary();
      Logger.Log(LogLevel.Debug, "Sending response of {0} bytes.", responseData.Length);

      return responseData;
    }

    /// <summary>
    /// Creates a new voting.
    /// </summary>
    /// <param name="votingParameters">Parameters for the voting.</param>
    /// <param name="authorities">List of authorities to oversee the voting.</param>
    public void CreateVoting(Signed<VotingParameters> signedVotingParameters, IEnumerable<AuthorityCertificate> authorities)
    {
      if (signedVotingParameters == null)
        throw new PiArgumentException(ExceptionCode.ArgumentNull, "Voting parameters cannot be null.");
      if (authorities == null)
        throw new PiArgumentException(ExceptionCode.ArgumentNull, "Authority list cannot be null.");

      if (!signedVotingParameters.Verify(CertificateStorage))
        throw new PiSecurityException(ExceptionCode.InvalidSignature, "Invalid signature.");
      if (!(signedVotingParameters.Certificate is AdminCertificate))
        throw new PiSecurityException(ExceptionCode.NoAuthorizedAdmin, "No authorized admin.");

      VotingParameters votingParameters = signedVotingParameters.Value;

      if (votingParameters.P == null)
        throw new PiArgumentException(ExceptionCode.ArgumentNull, "P cannot be null.");
      if (votingParameters.Q == null)
        throw new PiArgumentException(ExceptionCode.ArgumentNull, "Q cannot be null.");
      if (votingParameters.F == null)
        throw new PiArgumentException(ExceptionCode.ArgumentNull, "F cannot be null.");
      if (votingParameters.G == null)
        throw new PiArgumentException(ExceptionCode.ArgumentNull, "G cannot be null.");
      //if (!Prime.IsPrime(votingParameters.P))
      //  throw new PiArgumentException(ExceptionCode.PIsNoPrime, "P is not prime.");
      //if (!Prime.IsPrime((votingParameters.P - 1) / 2))
      //  throw new PiArgumentException(ExceptionCode.PIsNoSafePrime, "P is no safe prime.");
      //if (!Prime.IsPrime(votingParameters.Q))
      //  throw new PiArgumentException(ExceptionCode.QIsNoPrime, "Q is not prime.");

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
      if (!authorities.All(authority => authority.Validate(CertificateStorage) == CertificateValidationResult.Valid))
        throw new PiArgumentException(ExceptionCode.AuthorityInvalid, "Authority certificate invalid or not recognized.");

      MySqlCommand insertCommand = new MySqlCommand("INSERT INTO voting (Id, Parameters, Status) VALUES (@Id, @Parameters, @Status)", DbConnection);
      insertCommand.Parameters.AddWithValue("@Id", votingParameters.VotingId.ToByteArray());
      insertCommand.Parameters.AddWithValue("@Parameters", signedVotingParameters.ToBinary());
      insertCommand.Parameters.AddWithValue("@Status", (int)VotingStatus.New);
      insertCommand.ExecuteNonQuery();

      VotingServerEntity voting = new VotingServerEntity(this, signedVotingParameters, CertificateStorage, this.serverCertificate);
      authorities.Foreach(authority => voting.AddAuthority(authority));
      this.votings.Add(voting.Id, voting);
      voting.SendAuthorityActionRequiredMail();
    }

    /// <summary>
    /// Fetches the ids of all votings.
    /// </summary>
    /// <returns>List of voting ids.</returns>
    public IEnumerable<Guid> FetchVotingIds()
    {
      return this.votings.Keys;
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
    public void SetSignatureRequest(Secure<SignatureRequest> signatureRequest, Secure<SignatureRequestInfo> signatureRequestInfo)
    {
      Guid id = signatureRequest.Certificate.Id;

      if (signatureRequest.Certificate.Id != signatureRequestInfo.Certificate.Id)
        throw new PiArgumentException(ExceptionCode.SignatureRequestInvalid, "Signature request invalid.");
      if (!signatureRequest.VerifySimple())
        throw new PiArgumentException(ExceptionCode.SignatureRequestInvalid, "Signature request invalid.");
      if (!signatureRequestInfo.VerifySimple())
        throw new PiArgumentException(ExceptionCode.SignatureRequestInvalid, "Signature request invalid.");

      SignatureRequestInfo requestInfo = signatureRequestInfo.Value.Decrypt(this.serverCertificate);
      if (!requestInfo.Valid)
        throw new PiArgumentException(ExceptionCode.InvalidSignatureRequest, "Signature request data not valid.");

      MySqlCommand replaceCommand = new MySqlCommand("REPLACE INTO signaturerequest (Id, Value, Info) VALUES (@Id, @Value, @Info)", DbConnection);
      replaceCommand.Parameters.AddWithValue("@Id", id.ToByteArray());
      replaceCommand.Parameters.AddWithValue("@Value", signatureRequest.ToBinary());
      replaceCommand.Parameters.AddWithValue("@Info", signatureRequestInfo.ToBinary());
      replaceCommand.ExecuteNonQuery();

      MySqlCommand deleteCommand = new MySqlCommand("DELETE FROM signatureresponse WHERE Id = @Id", DbConnection);
      deleteCommand.Parameters.AddWithValue("@Id", id.ToByteArray());
      deleteCommand.ExecuteNonQuery();

      if (signatureRequest.Certificate is AuthorityCertificate)
      {
        CertificateStorage.Add(signatureRequest.Certificate);
      }

      if (!requestInfo.EmailAddress.IsNullOrEmpty())
      {
        string userBody = string.Format(
          this.serverConfig.MailRequestDepositedBody,
          requestInfo.EmailAddress,
          signatureRequest.Certificate.Id.ToString(),
          signatureRequest.Certificate.TypeText);
        Mailer.TrySend(requestInfo.EmailAddress, this.serverConfig.MailRequestSubject, userBody);
      }

      string adminBody = string.Format(
        this.serverConfig.MailAdminNewRequestBody,
        requestInfo.EmailAddress.IsNullOrEmpty() ? "?@?.?" : requestInfo.EmailAddress,
        signatureRequest.Certificate.Id.ToString(),
        signatureRequest.Certificate.TypeText);
      Mailer.TrySend(this.serverConfig.MailAdminAddress, this.serverConfig.MailAdminNewRequestSubject, adminBody);
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

    /// <summary>
    /// Sets a signature response.
    /// </summary>
    /// <param name="signatureRequestId">Id of the corresponding request.</param>
    /// <param name="signedSignatureResponse">Signed signature response.</param>
    public void SetSignatureResponse(Signed<SignatureResponse> signedSignatureResponse)
    {
      if (!signedSignatureResponse.Verify(CertificateStorage))
        throw new PiSecurityException(ExceptionCode.InvalidSignature, "Signature response has invalid signature.");
      if (!(signedSignatureResponse.Certificate is CACertificate))
        throw new PiSecurityException(ExceptionCode.SignatureResponseNotFromCA, "Signature response not from proper CA.");
      
      SignatureResponse signatureResponse = signedSignatureResponse.Value;

      MySqlCommand replaceCommand = new MySqlCommand("REPLACE INTO signatureresponse (Id, Value) VALUES (@Id, @Value)", DbConnection);
      replaceCommand.Parameters.AddWithValue("@Id", signatureResponse.SubjectId.ToByteArray());
      replaceCommand.Parameters.AddWithValue("@Value", signedSignatureResponse.ToBinary());
      replaceCommand.ExecuteNonQuery();

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
          string userBody = string.Format(
            this.serverConfig.MailRequestApprovedBody,
            signatureRequestInfo.EmailAddress,
            secureSignatureRequestInfo.Certificate.Id.ToString(),
            secureSignatureRequestInfo.Certificate.TypeText);
          Mailer.TrySend(signatureRequestInfo.EmailAddress, this.serverConfig.MailRequestSubject, userBody);
        }
      }
      else
      {
        Secure<SignatureRequestInfo> secureSignatureRequestInfo = GetSignatureRequestInfo(signatureResponse.SubjectId);
        SignatureRequestInfo signatureRequestInfo = secureSignatureRequestInfo.Value.Decrypt(this.serverCertificate);
        if (!signatureRequestInfo.EmailAddress.IsNullOrEmpty())
        {
          string userBody = string.Format(
            this.serverConfig.MailRequestDeclinedBody,
            signatureRequestInfo.EmailAddress,
            secureSignatureRequestInfo.Certificate.Id.ToString(),
            secureSignatureRequestInfo.Certificate.TypeText,
            signatureResponse.Reason);
          Mailer.TrySend(signatureRequestInfo.EmailAddress, this.serverConfig.MailRequestSubject, userBody);
        }
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
        Console.WriteLine("{0} from {1} is {2}", certificate.Id.ToString(), certificate.FullName, certificate.Validate(CertificateStorage));

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
    public void AddCertificateStorage(CertificateStorage certificateStorage)
    {
      if (!certificateStorage.SignedRevocationLists.All(crl => crl.Verify(CertificateStorage)))
        throw new PiSecurityException(ExceptionCode.InvalidSignature, "Signature on CRL not valid.");
      if (!certificateStorage.Certificates.All(certificate => certificate.Validate(CertificateStorage) == CertificateValidationResult.Valid))
        throw new PiSecurityException(ExceptionCode.InvalidCertificate, "Certificate not valid.");

      CertificateStorage.Add(certificateStorage);
    }

    /// <summary>
    /// Called at regular intervals.
    /// Has to keep the DB connection alive.
    /// </summary>
    public override void Process()
    {
      int certificateCount = CertificateStorage.Certificates.Count();
      Logger.Log(LogLevel.Debug, "SQL keep alive.", certificateCount);
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
  }
}
