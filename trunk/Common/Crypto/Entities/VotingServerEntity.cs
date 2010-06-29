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
using MySql.Data.MySqlClient;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Rpc;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Entity of the voting server.
  /// </summary>
  public class VotingServerEntity
  {
    /// <summary>
    /// Voting RPC server.
    /// </summary>
    private VotingRpcServer Server { get; set; }

    /// <summary>
    /// Logs information to file.
    /// </summary>
    private Logger Logger { get { return Server.Logger; } }

    /// <summary>
    /// Configuration of the server.
    /// </summary>
    private ServerConfig ServerConfig { get { return Server.ServerConfig; } }

    /// <summary>
    /// Sends mails to users;
    /// </summary>
    private Mailer Mailer { get { return Server.Mailer; } }

    /// <summary>
    /// Voting parameters.
    /// </summary>
    private VotingParameters parameters;

    /// <summary>
    /// Signed voting parameters.
    /// </summary>
    private Signed<VotingParameters> signedParameters;

    /// <summary>
    /// Storage of certificates.
    /// </summary>
    private ICertificateStorage certificateStorage;

    /// <summary>
    /// The server's own certificate.
    /// </summary>
    private ServerCertificate serverCertificate;

    /// <summary>
    /// Status of the voting procedures.
    /// </summary>
    private VotingStatus status;

    /// <summary>
    /// Status of the voting procedures.
    /// </summary>
    public VotingStatus Status
    {
      get
      {
        if (this.status == VotingStatus.Ready && DateTime.Now.Date >= this.parameters.VotingBeginDate.Date)
        {
          Status = VotingStatus.Voting;
        }

        if (this.status == VotingStatus.Voting && DateTime.Now.Date > this.parameters.VotingEndDate.Date)
        {
         Status = VotingStatus.Deciphering;
        }

        return this.status;
      }
      private set
      {
        this.status = value;
        MySqlCommand updateCommand = new MySqlCommand("UPDATE voting SET Status = @Status WHERE Id = @Id", DbConnection);
        updateCommand.Add("@Id", Id.ToByteArray());
        updateCommand.Add("@Status", (int)this.status);
        updateCommand.ExecuteNonQuery();

        Logger.Log(LogLevel.Info, "Status of voting {0} changed to {1}.", Id.ToString(), this.status.ToString());

        string body = string.Format(
          ServerConfig.MailAdminVotingStatusBody,
          Id.ToString(),
          Parameters.Title.Text,
          this.status.ToString());
        Mailer.TrySend(ServerConfig.MailAdminAddress, ServerConfig.MailAdminVotingStatusSubject, body);

        switch (this.status)
        {
          case VotingStatus.Sharing:
          case VotingStatus.Deciphering:
            SendAuthorityActionRequiredMail();
            break;
        }
      }
    }

    /// <summary>
    /// Checked database connections.
    /// </summary>
    private MySqlConnection DbConnection
    {
      get { return Server.DbConnection; }
    }

    /// <summary>
    /// List of authoritites that have performed the current step.
    /// Null if not applicable.
    /// </summary>
    public List<Guid> AuthoritiesDone
    {
      get
      {
        List<Guid> authorityIds = new List<Guid>();
        MySqlDataReader reader = null;

        switch (Status)
        {
          case VotingStatus.New:
            reader = DbConnection.ExecuteReader(
              "SELECT authority.AuthorityId FROM authority, sharepart WHERE authority.VotingId = @VotingId AND sharepart.VotingId = @VotingId AND authority.AuthorityIndex = sharepart.AuthorityIndex",
              "@VotingId", Id.ToByteArray());

            while (reader.Read())
            {
              authorityIds.Add(reader.GetGuid(0));
            }

            reader.Close();

            return authorityIds;
          case VotingStatus.Sharing:
            reader = DbConnection.ExecuteReader(
              "SELECT authority.AuthorityId FROM authority, shareresponse WHERE authority.VotingId = @VotingId AND shareresponse.VotingId = @VotingId AND authority.AuthorityIndex = shareresponse.AuthorityIndex",
              "@VotingId", Id.ToByteArray());

            while (reader.Read())
            {
              authorityIds.Add(reader.GetGuid(0));
            }

            reader.Close();

            return authorityIds;
          case VotingStatus.Deciphering:
            reader = DbConnection.ExecuteReader(
              "SELECT authority.AuthorityId FROM authority, deciphers WHERE authority.VotingId = @VotingId AND deciphers.VotingId = @VotingId AND authority.AuthorityIndex = deciphers.AuthorityIndex",
              "@VotingId", Id.ToByteArray());

            while (reader.Read())
            {
              authorityIds.Add(reader.GetGuid(0));
            }

            reader.Close();

            return authorityIds;
          default:
            return null;
        }
      }
    }

    /// <summary>
    /// Id of the voting procedures.
    /// </summary>
    public Guid Id
    {
      get { return this.parameters.VotingId; }
    }

    /// <summary>
    /// Create a new voting procedure.
    /// </summary>
    /// <param name="logger">Logs messages to file.</param>
    /// <param name="dbConnection">Connection to the database.</param>
    /// <param name="parameters">Voting parameters.</param>
    /// <param name="rootCertificate">Certificate storage.</param>
    public VotingServerEntity(
      VotingRpcServer server,
      Signed<VotingParameters> signedParameters,
      ICertificateStorage certificateStorage,
      ServerCertificate serverCertificate)
      : this(server, signedParameters, certificateStorage, serverCertificate, VotingStatus.New)
    {
      SendAuthorityActionRequiredMail();
    }

    /// <summary>
    /// Send action required mail to all authorities
    /// </summary>
    private void SendAuthorityActionRequiredMail()
    {
      List<string> authorities = new List<string>(
        Authorities
        .Where(authority => Server.HasSignatureRequest(authority.Id))
        .Select(authority => Server.GetSignatureRequest(authority.Id).Value.EmailAddress));
      string authorityBody = string.Format(
        ServerConfig.MailAuthorityActionRequiredBody,
        Id.ToString(),
        Parameters.Title.Text,
        this.status.ToString());
      Mailer.TrySend(authorities, ServerConfig.MailAuthorityActionRequiredSubject, authorityBody);
    }

    /// <summary>
    /// Create a new voting procedure.
    /// </summary>
    /// <param name="logger">Logs messages to file.</param>
    /// <param name="dbConnection">Connection to the database.</param>
    /// <param name="parameters">Voting parameters.</param>
    /// <param name="rootCertificate">Certificate storage.</param>
    /// <param name="status">Voting status.</param>
    public VotingServerEntity(
      VotingRpcServer server,
      Signed<VotingParameters> signedParameters,
      ICertificateStorage certificateStorage,
      ServerCertificate serverCertificate,
      VotingStatus status)
    {
      if (server == null)
        throw new ArgumentNullException("server");
      if (signedParameters == null)
        throw new ArgumentNullException("signedParameters");
      if (serverCertificate == null)
        throw new ArgumentNullException("serverCertificate");

      Server = server;
      this.certificateStorage = certificateStorage;
      this.serverCertificate = serverCertificate;
      this.signedParameters = signedParameters;
      this.parameters = this.signedParameters.Value;
      this.status = status;
    }

    /// <summary>
    /// Voting parameters.
    /// </summary>
    public VotingParameters Parameters { get { return this.parameters; } }

    /// <summary>
    /// Signed version of voting parameters;
    /// </summary>
    public Signed<VotingParameters> SignedParameters { get { return this.signedParameters; } }

    /// <summary>
    /// Get the index of an authority from certificate.
    /// </summary>
    /// <param name="certificate">Certificate of the authority.</param>
    /// <returns>Index of the authority.</returns>
    public int GetAuthorityIndex(AuthorityCertificate certificate)
    {
      if (certificate == null)
        throw new PiArgumentException(ExceptionCode.ArgumentNull, "Certificate is null.");
      if (!certificate.Valid(this.certificateStorage))
        throw new PiSecurityException(ExceptionCode.InvalidCertificate, "Authority certificate invalid.");

      MySqlCommand command = new MySqlCommand("SELECT AuthorityIndex FROM authority WHERE VotingId = @VotingId AND AuthorityId = @AuthorityId", DbConnection);
      command.Parameters.AddWithValue("@VotingId", this.parameters.VotingId.ToByteArray());
      command.Parameters.AddWithValue("@AuthorityId", certificate.Id.ToByteArray());
      MySqlDataReader reader = command.ExecuteReader();

      if (reader.Read())
      {
        int authorityIndex = reader.GetInt32(0);
        reader.Close();
        return authorityIndex;
      }
      else
      {
        reader.Close();
        throw new PiArgumentException(ExceptionCode.NoAuthorityWithCertificate, "No authority with that certificate.");
      }
    }

    /// <summary>
    /// Add an authority.
    /// </summary>
    /// <param name="certificate">Authority to be added.</param>
    /// <returns>Index of the authority.</returns>
    public int AddAuthority(Certificate certificate)
    {
      if (certificate == null)
        throw new ArgumentNullException("certificate");
      if (!certificate.Valid(this.certificateStorage))
        throw new PiSecurityException(ExceptionCode.InvalidCertificate, "Authority certificate not valid.");
      if (!(certificate is AuthorityCertificate))
        throw new PiSecurityException(ExceptionCode.NoAuthorizedAuthority, "No an authority certificate.");

      MySqlTransaction transaction = DbConnection.BeginTransaction();

      MySqlCommand countCommand = new MySqlCommand("SELECT count(*) FROM authority WHERE VotingId = @VotingId", DbConnection, transaction);
      countCommand.Add("@VotingId", this.parameters.VotingId.ToByteArray());
      if ((long)countCommand.ExecuteScalar() >= this.parameters.AuthorityCount)
        throw new PiArgumentException(ExceptionCode.AlreadyEnoughAuthorities, "Already enough authorities.");

      MySqlCommand addedCommand = new MySqlCommand("SELECT count(*) FROM authority WHERE VotingId = @VotingId AND AuthorityId = @AuthorityId", DbConnection, transaction);
      addedCommand.Add("@VotingId", this.parameters.VotingId.ToByteArray());
      addedCommand.Add("@AuthorityId", certificate.Id.ToByteArray());
      if (addedCommand.ExecuteHasRows())
        throw new PiArgumentException(ExceptionCode.AuthorityAlreadyInVoting, "Already an authority of the voting.");

      MySqlCommand indexCommand = new MySqlCommand("SELECT max(AuthorityIndex) + 1 FROM authority WHERE VotingId = @VotingId", DbConnection, transaction);
      indexCommand.Add("@VotingId", this.parameters.VotingId.ToByteArray());
      object authorityIndexNull = indexCommand.ExecuteScalar();
      int authorityIndex = authorityIndexNull == DBNull.Value ? 1 : Convert.ToInt32((long)authorityIndexNull);

      MySqlCommand insertCommand = new MySqlCommand("INSERT INTO authority (VotingId, AuthorityIndex, AuthorityId, Certificate) VALUES (@VotingId, @AuthorityIndex, @AuthorityId, @Certificate)", DbConnection, transaction);
      insertCommand.Parameters.AddWithValue("@VotingId", this.parameters.VotingId.ToByteArray());
      insertCommand.Parameters.AddWithValue("@AuthorityIndex", authorityIndex);
      insertCommand.Parameters.AddWithValue("@AuthorityId", certificate.Id.ToByteArray());
      insertCommand.Parameters.AddWithValue("@Certificate", certificate.ToBinary());
      insertCommand.ExecuteNonQuery();

      transaction.Commit();

      return authorityIndex;
    }

    /// <summary>
    /// List of authorities.
    /// </summary>
    public IEnumerable<Certificate> Authorities
    {
      get
      {
        List<Certificate> authorities = new List<Certificate>();
        MySqlDataReader reader = DbConnection.ExecuteReader(
          "SELECT Certificate FROM authority WHERE VotingId = @VotingId",
          "@VotingId", Id.ToByteArray());

        while (reader.Read())
        {
          byte[] certificateData = reader.GetBlob(0);
          authorities.Add(Serializable.FromBinary<Certificate>(certificateData));
        }

        reader.Close();

        return authorities;
      }
    }

    /// <summary>
    /// List of authorities.
    /// </summary>
    public AuthorityList AuthorityList
    {
      get
      {
        return new AuthorityList(
          Id,
          Authorities,
          this.certificateStorage.Certificates,
          this.certificateStorage.SignedRevocationLists);
      }
    }

    private Certificate GetAuthority(int authorityIndex)
    {
      MySqlDataReader reader = DbConnection.ExecuteReader(
        "SELECT Certificate FROM authority WHERE VotingId = @VotingId AND AuthorityIndex = @AuthorityIndex",
        "@VotingId", this.parameters.VotingId.ToByteArray(),
        "@AuthorityIndex", authorityIndex);

      if (reader.Read())
      {
        byte[] certificateData = reader.GetBlob(0);
        reader.Close();
        return Serializable.FromBinary<Certificate>(certificateData);
      }
      else
      {
        reader.Close();
        throw new PiArgumentException(ExceptionCode.NoAuthorizedAuthority, "Bad authority index.");
      }
    }

    /// <summary>
    /// Deposit a share part from authorities.
    /// </summary>
    /// <param name="signedSharePart">Share part.</param>
    public void DepositShares(Signed<SharePart> signedSharePart)
    {
      if (signedSharePart == null)
        throw new ArgumentNullException("shares");
      if (Status != VotingStatus.New)
        throw new PiArgumentException(ExceptionCode.WrongStatusForOperation, "Wrong status for operation.");

      SharePart sharePart = signedSharePart.Value;

      Certificate certificate = GetAuthority(sharePart.AuthorityIndex);

      if (!signedSharePart.Verify(this.certificateStorage))
        throw new PiSecurityException(ExceptionCode.InvalidSignature, "Bad signature.");
      if (!signedSharePart.Certificate.IsIdentic(certificate))
        throw new PiSecurityException(ExceptionCode.NoAuthorizedAuthority, "Not signed by proper authority.");

      bool exists = DbConnection.ExecuteHasRows(
        "SELECT count(*) FROM sharepart WHERE VotingId = @VotingId AND AuthorityIndex = @AuthorityIndex",
        "@VotingId", Id.ToByteArray(),
        "@AuthorityIndex", sharePart.AuthorityIndex);
      if (exists)
        throw new PiArgumentException(ExceptionCode.AuthorityHasAlreadyDeposited, "Authority has already deposited shares.");

      MySqlCommand insertCommand = new MySqlCommand("INSERT INTO sharepart (VotingId, AuthorityIndex, Value) VALUES (@VotingId, @AuthorityIndex, @Value)", DbConnection);
      insertCommand.Add("@VotingId", Id.ToByteArray());
      insertCommand.Add("@AuthorityIndex", sharePart.AuthorityIndex);
      insertCommand.Add("@Value", signedSharePart.ToBinary());
      insertCommand.ExecuteNonQuery();

      long depositedSharePartCount = (long)DbConnection.ExecuteScalar(
        "SELECT count(*) FROM sharepart WHERE VotingId = @VotingId",
        "@VotingId", Id.ToByteArray());

      SendAdminAuthorityActivityMail(certificate, "deposited shares");

      if (depositedSharePartCount == this.parameters.AuthorityCount)
      {
        Status = VotingStatus.Sharing;
      }
    }

    private void SendAdminAuthorityActivityMail(Certificate authority, string activity)
    {
      string body = string.Format(
        ServerConfig.MailAdminAuthorityActivityBody,
        Id.ToString(),
        Parameters.Title.Text,
        authority.FullName,
        activity);
      Mailer.TrySend(ServerConfig.MailAdminAddress, ServerConfig.MailAdminAuthorityActivitySubject, body);
    }

    /// <summary>
    /// Get all shares from all authorities.
    /// </summary>
    /// <returns>All shares list.</returns>
    public AllShareParts GetAllShares()
    {
      if (Status != VotingStatus.Sharing)
        throw new InvalidOperationException("Wrong status for operation.");

      return new AllShareParts(Id, SignedShareParts);
    }

    /// <summary>
    /// List all share parts.
    /// </summary>
    private IEnumerable<Signed<SharePart>> SignedShareParts
    {
      get
      {
        List<Signed<SharePart>> signedShareParts = new List<Signed<SharePart>>();
        MySqlDataReader reader = DbConnection.ExecuteReader(
          "SELECT Value FROM sharepart WHERE VotingId = @VotingId ORDER BY AuthorityIndex DESC",
          "@VotingId", Id.ToByteArray());

        while (reader.Read())
        {
          byte[] signedSharePartDate = reader.GetBlob(0);
          signedShareParts.Add(Serializable.FromBinary<Signed<SharePart>>(signedSharePartDate));
        }

        reader.Close();

        return signedShareParts;
      }
    }

    /// <summary>
    /// Deposit share responses from authorities.
    /// </summary>
    /// <param name="signedShareResponse">Signed share response.</param>
    public void DepositShareResponse(Signed<ShareResponse> signedShareResponse)
    {
      if (signedShareResponse == null)
        throw new ArgumentNullException("shares");
      if (Status != VotingStatus.Sharing)
        throw new PiArgumentException(ExceptionCode.WrongStatusForOperation, "Wrong status for operation.");

      ShareResponse shareResponse = signedShareResponse.Value;

      Certificate certificate = GetAuthority(shareResponse.AuthorityIndex);

      if (!signedShareResponse.Verify(this.certificateStorage))
        throw new PiSecurityException(ExceptionCode.InvalidSignature, "Bad signature.");
      if (!signedShareResponse.Certificate.IsIdentic(certificate))
        throw new PiSecurityException(ExceptionCode.NoAuthorizedAuthority, "Not signed by proper authority.");

      bool exists = DbConnection.ExecuteHasRows(
        "SELECT count(*) FROM shareresponse WHERE VotingId = @VotingId AND AuthorityIndex = @AuthorityIndex",
        "@VotingId", Id.ToByteArray(),
        "@AuthorityIndex", shareResponse.AuthorityIndex);
      if (exists)
        throw new PiArgumentException(ExceptionCode.AuthorityHasAlreadyDeposited, "Authority has already deposited share responses.");

      MySqlCommand insertCommand = new MySqlCommand("INSERT INTO shareresponse (VotingId, AuthorityIndex, Value) VALUES (@VotingId, @AuthorityIndex, @Value)", DbConnection);
      insertCommand.Add("@VotingId", Id.ToByteArray());
      insertCommand.Add("@AuthorityIndex", shareResponse.AuthorityIndex);
      insertCommand.Add("@Value", signedShareResponse.ToBinary());
      insertCommand.ExecuteNonQuery();

      long depositedShareResponseCount = (long)DbConnection.ExecuteScalar(
        "SELECT count(*) FROM shareresponse WHERE VotingId = @VotingId",
        "@VotingId", Id.ToByteArray());

      SendAdminAuthorityActivityMail(certificate, "deposited share response");

      if (depositedShareResponseCount == this.parameters.AuthorityCount)
      {
        Status = VotingStatus.Ready;
      }
    }

    /// <summary>
    /// List all share responses.
    /// </summary>
    private IEnumerable<Signed<ShareResponse>> SignedShareResponses
    {
      get
      {
        List<Signed<ShareResponse>> signedShareResponses = new List<Signed<ShareResponse>>();
        MySqlDataReader reader = DbConnection.ExecuteReader(
          "SELECT Value FROM shareresponse WHERE VotingId = @VotingId ORDER BY AuthorityIndex DESC",
          "@VotingId", Id.ToByteArray());

        while (reader.Read())
        {
          byte[] signedShareResponse = reader.GetBlob(0);
          signedShareResponses.Add(Serializable.FromBinary<Signed<ShareResponse>>(signedShareResponse));
        }

        reader.Close();

        return signedShareResponses;
      }
    }

    /// <summary>
    /// Get material for a voter.
    /// </summary>
    /// <returns>Material to vote.</returns>
    public VotingMaterial GetVotingMaterial()
    {
      return new VotingMaterial(
        this.signedParameters,
        SignedShareResponses,
        GetEnvelopeCount());
    }

    /// <summary>
    /// Deposit a ballot.
    /// </summary>
    /// <param name="ballot">Ballot in signed envleope.</param>
    /// <returns>Vote receipt signed by the server.</returns>
    public Signed<VoteReceipt> Vote(Signed<Envelope> signedEnvelope)
    {
      if (signedEnvelope == null)
        throw new ArgumentNullException("ballot");
      if (Status != VotingStatus.Voting)
        throw new PiArgumentException(ExceptionCode.WrongStatusForOperation, "Wrong status for operation.");
      if (!signedEnvelope.Verify(this.certificateStorage))
        throw new PiArgumentException(ExceptionCode.VoteSignatureNotValid, "Vote signature not valid.");
      if (!(signedEnvelope.Certificate is VoterCertificate))
        throw new PiArgumentException(ExceptionCode.NoVoterCertificate, "Not a voter certificate.");
      if (Parameters.Canton != Canton.None && Parameters.Canton != ((VoterCertificate)signedEnvelope.Certificate).Canton)
        throw new PiArgumentException(ExceptionCode.BadCantonInCertificate, "Wrong canton in certificate.");

      bool hasVoted = DbConnection.ExecuteHasRows(
        "SELECT count(*) FROM envelope WHERE VotingId = @VotingId AND VoterId = @VoterId",
        "@VotingId", Id.ToByteArray(),
        "@VoterId", signedEnvelope.Certificate.Id.ToByteArray());
      if (hasVoted)
        throw new PiArgumentException(ExceptionCode.AlreadyVoted, "Voter has already voted.");

      MySqlTransaction transaction = DbConnection.BeginTransaction();

      MySqlCommand indexCommand = new MySqlCommand(
        "SELECT max(EnvelopeIndex) + 1 FROM envelope WHERE VotingId = @VotingId",
        DbConnection,
        transaction);
      indexCommand.Add("@VotingId", Id.ToByteArray());
      object indexObject = indexCommand.ExecuteScalar();
      int envelopeIndex = indexObject == DBNull.Value ? 1 : Convert.ToInt32(indexObject);

      MySqlCommand insertCommand = new MySqlCommand(
        "INSERT INTO envelope (VotingId, EnvelopeIndex, VoterId, Value) VALUES (@VotingId, @EnvelopeIndex, @VoterId, @Value)",
        DbConnection,
        transaction);
      insertCommand.Add("@VotingId", Id.ToByteArray());
      insertCommand.Add("@VoterId", signedEnvelope.Certificate.Id.ToByteArray());
      insertCommand.Add("@Value", signedEnvelope.ToBinary());
      insertCommand.Add("@EnvelopeIndex", envelopeIndex);
      insertCommand.ExecuteNonQuery();

      transaction.Commit();

      VoteReceipt voteReceipt = new VoteReceipt(Parameters, signedEnvelope);

      return new Signed<VoteReceipt>(voteReceipt, this.serverCertificate);
    }

    /// <summary>
    /// End the voting procedure.
    /// </summary>
    public void EndVote()
    {
      if (Status != VotingStatus.Voting)
        throw new InvalidOperationException("Wrong status for operation.");

      Status = VotingStatus.Deciphering;
    }

    /// <summary>
    /// Deposit partial deciphers from an authority.
    /// </summary>
    /// <param name="signedPartialDecipherList">Partial decipher list.</param>
    public void DepositPartialDecipher(Signed<PartialDecipherList> signedPartialDecipherList)
    {
      if (signedPartialDecipherList == null)
        throw new ArgumentNullException("partialDecipherContainer");
      if (Status != VotingStatus.Deciphering)
        throw new InvalidOperationException("Wrong status for operation.");

      PartialDecipherList partialDecipherList = signedPartialDecipherList.Value;

      Certificate certificate = GetAuthority(partialDecipherList.AuthorityIndex);

      if (!signedPartialDecipherList.Verify(this.certificateStorage))
        throw new ArgumentException("Bad signature.");
      if (!signedPartialDecipherList.Certificate.IsIdentic(certificate))
        throw new ArgumentException("Not signed by proper authority.");

      bool exists = DbConnection.ExecuteHasRows(
        "SELECT count(*) FROM deciphers WHERE VotingId = @VotingId AND AuthorityIndex = @AuthorityIndex",
        "@VotingId", Id.ToByteArray(),
        "@AuthorityIndex", partialDecipherList.AuthorityIndex);
      if (exists)
        throw new ArgumentException("Authority has already deposited shares.");

      MySqlCommand insertCommand = new MySqlCommand("INSERT INTO deciphers (VotingId, AuthorityIndex, Value) VALUES (@VotingId, @AuthorityIndex, @Value)", DbConnection);
      insertCommand.Add("@VotingId", Id.ToByteArray());
      insertCommand.Add("@AuthorityIndex", partialDecipherList.AuthorityIndex);
      insertCommand.Add("@Value", signedPartialDecipherList.ToBinary());
      insertCommand.ExecuteNonQuery();

      long depositedShareResponseCount = (long)DbConnection.ExecuteScalar(
        "SELECT count(*) FROM deciphers WHERE VotingId = @VotingId",
        "@VotingId", Id.ToByteArray());

      SendAdminAuthorityActivityMail(certificate, "deposited partial deciphers");

      if (depositedShareResponseCount == this.parameters.Thereshold + 1)
      {
        Status = VotingStatus.Finished;
      }
    }

    /// <summary>
    /// Get signed envelope.
    /// </summary>
    /// <param name="envelopeIndex">Index of envelope.</param>
    /// <returns>Signed envelope.</returns>
    public Signed<Envelope> GetEnvelope(int envelopeIndex)
    {
      MySqlDataReader reader = DbConnection.ExecuteReader(
        "SELECT Value FROM envelope WHERE VotingId = @VotingId AND EnvelopeIndex = @EnvelopeIndex",
        "@VotingId", Id.ToByteArray(),
        "@EnvelopeIndex", envelopeIndex + 1);

      if (reader.Read())
      {
        byte[] signedEnvelopeData = reader.GetBlob(0);
        reader.Close();
        return Serializable.FromBinary<Signed<Envelope>>(signedEnvelopeData);
      }
      else
      {
        reader.Close();
        throw new PiArgumentException(ExceptionCode.ArgumentOutOfRange, "Envelope index out of range.");
      }
    }

    /// <summary>
    /// Get partial decipher list.
    /// </summary>
    /// <param name="authorityIndex">Index of authority.</param>
    /// <returns>Partial decipher list.</returns>
    public Signed<PartialDecipherList> GetPartialDecipher(int authorityIndex)
    {
      MySqlDataReader reader = DbConnection.ExecuteReader(
        "SELECT Value FROM deciphers WHERE VotingId = @VotingId AND AuthorityIndex = @AuthorityIndex",
        "@VotingId", Id.ToByteArray(),
        "@AuthorityIndex", authorityIndex);

      if (reader.Read())
      {
        byte[] signedEnvelopeData = reader.GetBlob(0);
        reader.Close();
        return Serializable.FromBinary<Signed<PartialDecipherList>>(signedEnvelopeData);
      }
      else
      {
        reader.Close();
        return null;
      }
    }

    /// <summary>
    /// Get number of envelopes.
    /// </summary>
    /// <returns>Envelope count.</returns>
    public int GetEnvelopeCount()
    {
      return Convert.ToInt32((long)DbConnection.ExecuteScalar(
        "SELECT count(*) FROM envelope WHERE VotingId = @VotingId",
        "@VotingId", Id.ToByteArray()));
    }
  }
}
