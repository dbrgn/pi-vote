
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
    /// Create the voting server.
    /// </summary>
    public VotingRpcServer()
    {
      this.dbConnection = new MySqlConnection("Server=localhost;Database=PiVote;Uid=pivote;Pwd=alpha123.;");
      this.dbConnection.Open();

      CertificateStorage = new DatabaseCertificateStorage(this.dbConnection);
      CertificateStorage.ImportCaIfNeed();

      LoadVotings();
    }

    /// <summary>
    /// Load all votings from the database.
    /// </summary>
    private void LoadVotings()
    {
      this.votings = new Dictionary<Guid, VotingServerEntity>();

      MySqlCommand selectCommand = new MySqlCommand("SELECT Id, Parameters, Status FROM voting", this.dbConnection);
      MySqlDataReader reader = selectCommand.ExecuteReader();

      while (reader.Read())
      {
        Guid id = reader.GetGuid(0);
        byte[] signedParametersData = reader.GetBlob(1);
        Signed<VotingParameters> signedParameters = Serializable.FromBinary<Signed<VotingParameters>>(signedParametersData);
        VotingStatus status = (VotingStatus)reader.GetInt32(2);
        VotingServerEntity entity = new VotingServerEntity(this.dbConnection, signedParameters, this.CertificateStorage, status);
        this.votings.Add(id, entity);
      }

      reader.Close();
    }

    /// <summary>
    /// Excecutes a RPC request.
    /// </summary>
    /// <param name="requestData">Serialized request data.</param>
    /// <returns>Serialized response data</returns>
    public byte[] Execute(byte[] requestData)
    {
      var request = Serializable.FromBinary<RpcRequest<VotingRpcServer>>(requestData);
      var response = request.TryExecute(this);

      return response.ToBinary();
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
      if (!Prime.IsPrime(votingParameters.P))
        throw new PiArgumentException(ExceptionCode.PIsNoPrime, "P is not prime.");
      if (!Prime.IsPrime((votingParameters.P - 1) / 2))
        throw new PiArgumentException(ExceptionCode.PIsNoSafePrime, "P is no safe prime.");
      if (!Prime.IsPrime(votingParameters.Q))
        throw new PiArgumentException(ExceptionCode.QIsNoPrime, "Q is not prime.");

      if (!votingParameters.AuthorityCount.InRange(3, 23))
        throw new PiArgumentException(ExceptionCode.AuthorityCountOutOfRange, "Authority count out of range.");
      if (!votingParameters.Thereshold.InRange(1, votingParameters.AuthorityCount - 1))
        throw new PiArgumentException(ExceptionCode.TheresholdOutOfRange, "Thereshold out of range.");
      if (votingParameters.OptionCount < 2)
        throw new PiArgumentException(ExceptionCode.OptionCountOutOfRange, "Option count out of range.");
      if (!votingParameters.MaxVota.InRange(1, votingParameters.OptionCount))
        throw new PiArgumentException(ExceptionCode.MaxVotaOutOfRange, "Maximum vota out of range.");
      if (votingParameters.Options.Count() != votingParameters.OptionCount)
        throw new PiArgumentException(ExceptionCode.OptionCountMismatch, "Option count does not match number of options.");
      if (votingParameters.AuthorityCount != authorities.Count())
        throw new PiArgumentException(ExceptionCode.AuthorityCountMismatch, "Authority count does not match number of provided authorities.");
      if (!authorities.All(authority => authority.Valid(CertificateStorage)))
        throw new PiArgumentException(ExceptionCode.AuthorityInvalid, "Authority certificate invalid or not recognized.");

      MySqlCommand insertCommand = new MySqlCommand("INSERT INTO voting (Id, Parameters, Status) VALUES (@Id, @Parameters, @Status)", this.dbConnection);
      insertCommand.Parameters.AddWithValue("@Id", votingParameters.VotingId.ToByteArray());
      insertCommand.Parameters.AddWithValue("@Parameters", signedVotingParameters.ToBinary());
      insertCommand.Parameters.AddWithValue("@Status", (int)VotingStatus.New);
      insertCommand.ExecuteNonQuery();

      VotingServerEntity voting = new VotingServerEntity(this.dbConnection, signedVotingParameters, CertificateStorage);
      authorities.Foreach(authority => voting.AddAuthority(authority));
      this.votings.Add(voting.Id, voting);
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
    public void SetSignatureRequest(Signed<SignatureRequest> signatureRequest)
    {
      Guid id = signatureRequest.Certificate.Id;

      if (!signatureRequest.VerifySimple())
        throw new PiArgumentException(ExceptionCode.SignatureRequestInvalid, "Signature request invalid.");

      MySqlCommand replaceCommand = new MySqlCommand("REPLACE INTO signaturerequest (Id, Value) VALUES (@Id, @Value)", this.dbConnection);
      replaceCommand.Parameters.AddWithValue("@Id", id.ToByteArray());
      replaceCommand.Parameters.AddWithValue("@Value", signatureRequest.ToBinary());
      replaceCommand.ExecuteNonQuery();

      if (signatureRequest.Certificate is AuthorityCertificate)
      {
        CertificateStorage.Add(signatureRequest.Certificate);
      }
    }

    /// <summary>
    /// Get the list of open signature requests.
    /// </summary>
    /// <returns>List of signature request ids.</returns>
    public List<Guid> GetSignatureRequestList()
    {
      MySqlDataReader reader = this.dbConnection
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
    public Signed<SignatureRequest> GetSignatureRequest(Guid id)
    {
      MySqlDataReader reader = this.dbConnection
        .ExecuteReader("SELECT Value FROM signaturerequest WHERE Id = @Id",
        "@Id", id.ToByteArray());

      if (reader.Read())
      {
        byte[] signatureRequestData = reader.GetBlob(0);
        reader.Close();
        return Serializable.FromBinary<Signed<SignatureRequest>>(signatureRequestData);
      }
      else
      {
        reader.Close();
        throw new PiArgumentException(ExceptionCode.SignatureRequestNotFound, "Signature request not found.");
      }
    }

    /// <summary>
    /// Get the status and perhaps response regarding as signature request.
    /// </summary>
    /// <param name="certificateId">Id of the certificate.</param>
    /// <param name="signatureResponse">Signed signature response.</param>
    /// <returns>Status of the signature response.</returns>
    public SignatureResponseStatus GetSignatureResponseStatus(Guid certificateId, out Signed<SignatureResponse> signatureResponse)
    {
      MySqlCommand selectResponseCommand = new MySqlCommand("SELECT Value FROM signatureresponse WHERE Id = @Id", this.dbConnection);
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

        MySqlCommand selectRequestCommand = new MySqlCommand("SELECT count(*) FROM signaturerequest WHERE Id = @Id", this.dbConnection);
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

      MySqlCommand replaceCommand = new MySqlCommand("REPLACE INTO signatureresponse (Id, Value) VALUES (@Id, @Value)", this.dbConnection);
      replaceCommand.Parameters.AddWithValue("@Id", signatureResponse.SubjectId.ToByteArray());
      replaceCommand.Parameters.AddWithValue("@Value", signedSignatureResponse.ToBinary());
      replaceCommand.ExecuteNonQuery();

      if (CertificateStorage.Has(signatureResponse.SubjectId))
      {
        Certificate certificate = CertificateStorage.Get(signatureResponse.SubjectId);
        certificate.AddSignature(signatureResponse.Signature);
        CertificateStorage.Add(certificate);
      }
    }

    /// <summary>
    /// Get all valid authority certificates in storage.
    /// </summary>
    /// <returns>List of authority certificates.</returns>
    public List<AuthorityCertificate> GetValidAuthorityCertificates()
    {
      List<AuthorityCertificate> authorityCertificates = new List<AuthorityCertificate>();

      foreach (Certificate certificate in CertificateStorage.Certificates)
      {
        if (certificate is AuthorityCertificate && certificate.Valid(CertificateStorage))
        {
          authorityCertificates.Add((AuthorityCertificate)certificate);
        }
      }

      return authorityCertificates;
    }
  }
}
