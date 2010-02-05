
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

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// Voting RPC server.
  /// </summary>
  public class VotingRpcServer : RpcServer
  {
    /// <summary>
    /// List of voting procedures.
    /// </summary>
    private Dictionary<int, VotingServerEntity> votings;

    /// <summary>
    /// Storage for certificates.
    /// </summary>
    private CertificateStorage certificateStorage;

    /// <summary>
    /// Create the voting server.
    /// </summary>
    /// <param name="certificateStorage">Storage for certificates.</param>
    public VotingRpcServer(CertificateStorage certificateStorage)
    {
      this.votings = new Dictionary<int, VotingServerEntity>();
      this.certificateStorage = certificateStorage;
    }

    /// <summary>
    /// Excecutes a RPC request.
    /// </summary>
    /// <param name="requestData">Serialized request data.</param>
    /// <returns>Serialized response data</returns>
    public byte[] Execute(byte[] requestData)
    {
      var signedRequest = Serializable.FromBinary<Signed<RpcRequest<VotingRpcServer>>>(requestData);

      var request = signedRequest.Value;
      var valid = signedRequest.Verify(this.certificateStorage);
      
      var response = request.TryExecute(this, valid ? signedRequest.Certificate : null);

      return response.ToBinary();
    }

    /// <summary>
    /// Creates a new voting.
    /// </summary>
    /// <param name="votingParameters">Parameters for the voting.</param>
    /// <param name="authorities">List of authorities to oversee the voting.</param>
    /// <returns>Id of the voting.</returns>
    public int CreateVoting(VotingParameters votingParameters, IEnumerable<AuthorityCertificate> authorities)
    {
      if (votingParameters == null)
        throw new PiArgumentException(ExceptionCode.ArgumentNull, "Voting parameters cannot be null.");
      if (authorities == null)
        throw new PiArgumentException(ExceptionCode.ArgumentNull, "Authority list cannot be null.");

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

      votingParameters.SetId(votings.Keys.MaxOrDefault(0) + 1);

      VotingServerEntity voting = new VotingServerEntity(votingParameters, this.certificateStorage);
      authorities.Foreach(authority => voting.AddAuthority(authority));
      this.votings.Add(voting.Id, voting);

      return voting.Id;
    }

    /// <summary>
    /// Fetches the ids of all votings.
    /// </summary>
    /// <returns>List of voting ids.</returns>
    public IEnumerable<int> FetchVotingIds()
    {
      return this.votings.Keys;
    }

    /// <summary>
    /// Gets a voting from an id.
    /// </summary>
    /// <param name="id">Id of the voting.</param>
    /// <returns>Voting procedure entity.</returns>
    public VotingServerEntity GetVoting(int id)
    {
      if (!this.votings.ContainsKey(id))
        throw new PiArgumentException(ExceptionCode.NoVotingWithId, "No voting with that id.");

      return this.votings[id];
    }
  }
}
