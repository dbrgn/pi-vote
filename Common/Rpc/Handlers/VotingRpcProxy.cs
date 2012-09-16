/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// Proxy for RPC function calling.
  /// </summary>
  public partial class VotingRpcProxy
  {
    /// <summary>
    /// Binary RPC proxy.
    /// </summary>
    private IBinaryRpcProxy binaryProxy;

    /// <summary>
    /// When was the last message sent.
    /// Used to determine when to send an keepalive.
    /// </summary>
    private DateTime lastMessageSent;

    /// <summary>
    /// Continue to run the process thread?
    /// </summary>
    private bool run;

    /// <summary>
    /// Thread that does continues processing.
    /// Sends keepalive if necessary.
    /// </summary>
    private Thread processThread;

    /// <summary>
    /// Creates a new voting proxy.
    /// </summary>
    /// <param name="binaryProxy">Binary RPC proxy.</param>
    public VotingRpcProxy(IBinaryRpcProxy binaryProxy)
    {
      this.binaryProxy = binaryProxy;
      this.lastMessageSent = DateTime.Now;
    }

    /// <summary>
    /// Fetches the ids of all voting procedures.
    /// </summary>
    /// <returns>List of ids of voting procedures.</returns>
    public IEnumerable<Guid> FetchVotingIds()
    {
      var request = new ListVotingIdsRequest(Guid.NewGuid());
      var response = Execute<ListVotingIdsResponse>(request);

      return response.VotingIds;
    }

    /// <summary>
    /// Fetches the status of a voting.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <param name="authoritiesDone">List of authorities that have completed the current step if applicable.</param>
    /// <returns>Status of the voting.</returns>
    public VotingStatus FetchVotingStatus(Guid votingId, out List<Guid> authoritiesDone)
    {
      var request = new VotingStatusRequest(Guid.NewGuid(), votingId);
      var response = Execute<VotingStatusResponse>(request);
      authoritiesDone = response.AuthoritiesDone;

      return response.VotingStatus;
    }

    /// <summary>
    /// Executes a PRC request on a remote server.
    /// </summary>
    /// <typeparam name="TResponse">Type of the response.</typeparam>
    /// <param name="request">RPC request to be executed.</param>
    /// <returns>RPC response from the server.</returns>
    protected TResponse Execute<TResponse>(RpcRequest<VotingRpcServer> request)
      where TResponse : RpcResponse
    {
      lock (this.binaryProxy)
      {
        this.lastMessageSent = DateTime.Now;

        var responseData = this.binaryProxy.Execute(request.ToBinary());
        var response = Serializable.FromBinary<TResponse>(responseData);

        if (response.Exception != null)
          throw response.Exception;

        return response;
      }
    }

    /// <summary>
    /// Fetches an envelope.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <param name="envelopeIndex">Index of the envelope.</param>
    /// <returns>Signed envelope.</returns>
    public Signed<Envelope> FetchEnvelope(Guid votingId, int envelopeIndex)
    {
      var request = new FetchEnvelopeRequest(Guid.NewGuid(), votingId, envelopeIndex);
      var response = Execute<FetchEnvelopeResponse>(request);

      return response.Envelope;
    }

    /// <summary>
    /// Fetches an envelope.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>Statistics data.</returns>
    public string FetchStats(StatisticsDataType type)
    {
      var request = new FetchStatsRequest(Guid.NewGuid(), type);
      var response = Execute<FetchStatsResponse>(request);

      return response.Data;
    }

    /// <summary>
    /// Fetches a partial decipher list.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <param name="authorityIndex">Index of the authority.</param>
    /// <returns>Singed list of partial deciphers.</returns>
    public Signed<PartialDecipherList> FetchPartialDecipher(Guid votingId, int authorityIndex)
    {
      var request = new FetchPartialDecipherRequest(Guid.NewGuid(), votingId, authorityIndex);
      var response = Execute<FetchPartialDecipherResponse>(request);

      return response.PartialDecipherList;
    }

    /// <summary>
    /// Fetches the envelope count.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <returns>Number of envelopes.</returns>
    public int FetchEnvelopeCount(Guid votingId)
    {
      var request = new FetchEnvelopeCountRequest(Guid.NewGuid(), votingId);
      var response = Execute<FetchEnvelopeCountResponse>(request);

      return response.EnvelopeCount;
    }

    /// <summary>
    /// Fetches the voting material.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <returns>Complete voting material.</returns>
    public VotingMaterial FetchVotingMaterial(Guid votingId)
    {
      return FetchVotingMaterial(new Guid[] { votingId }).First().First;
    }

    /// <summary>
    /// Fetches the voting material.
    /// </summary>
    /// <param name="votingIds">Ids of the votings.</param>
    /// <returns>Complete voting material.</returns>
    public IEnumerable<Tuple<VotingMaterial, VotingStatus, List<Guid>>> FetchVotingMaterial(IEnumerable<Guid> votingIds)
    {
      var request = new FetchVotingMaterialVoterRequest(Guid.NewGuid(), votingIds);
      var response = Execute<FetchVotingMaterialVoterResponse>(request);

      return response.VotingMaterials;
    }

    /// <summary>
    /// Fetches the voting material.
    /// </summary>
    /// <param name="votingIds">Ids of the votings.</param>
    /// <returns>Complete voting material.</returns>
    public IEnumerable<VotingContainer> FetchVotings(IEnumerable<Guid> votingIds)
    {
      var request = new FetchVotingRequest(Guid.NewGuid(), votingIds);
      var response = Execute<FetchVotingResponse>(request);

      return response.Votings;
    }

    /// <summary>
    /// Fetches a signature responses or at least it's status.
    /// </summary>
    /// <param name="certificateId">Id of the certificate.</param>
    /// <param name="signatureResponse">Signed signature reponse.</param>
    /// <returns>Status of the signature response.</returns>
    public SignatureResponseStatus FetchSignatureResponse(Guid certificateId, out Signed<SignatureResponse> signatureResponse)
    {
      var request = new FetchSignatureResponseRequest(Guid.NewGuid(), certificateId);
      var response = Execute<FetchSignatureResponseResponse>(request);

      signatureResponse = response.SignatureResponse;
      return response.Status;
    }

    /// <summary>
    /// Pushes a signature request to the server.
    /// </summary>
    /// <param name="signatureRequest">Signature Request signed and encrypted for the CA.</param>
    /// <param name="signatureRequestInfo">Signature Request Info signed and encrypted for the Server.</param>
    public void PushSignatureRequest(Secure<SignatureRequest> signatureRequest, Secure<SignatureRequestInfo> signatureRequestInfo)
    {
      var request = new PushSignatureRequestRequest(Guid.NewGuid(), signatureRequest, signatureRequestInfo);
      var response = Execute<PushSignatureRequestResponse>(request);
    }

    /// <summary>
    /// Pushes a signature request to the server.
    /// </summary>
    /// <param name="signatureRequest">Signed signature request.</param>
    public Tuple< CertificateStorage, Certificate> FetchCertificateStorage()
    {
      var request = new FetchCertificateStorageRequest(Guid.NewGuid());
      var response = Execute<FetchCertificateStorageResponse>(request);

      return new Tuple<CertificateStorage, Certificate>(response.CertificateStorage, response.ServerCertificate);
    }

    /// <summary>
    /// Pushes a remote request to the server.
    /// </summary>
    /// <param name="clientName">Name of the client.</param>
    /// <param name="clientVersion">Version of the client.</param>
    public Tuple<IRemoteConfig, IEnumerable<Group>> FetchConfig(string clientName, string clientVersion)
    {
      var request = new FetchConfigRequest(Guid.NewGuid(), clientName, clientVersion);
      var response = Execute<FetchConfigResponse>(request);

      return new Tuple<IRemoteConfig, IEnumerable<Group>>(response.Config, response.Groups);
    }

    /// <summary>
    /// Starts the processing thread.
    /// </summary>
    public void Start()
    {
      this.run = true;
      this.processThread = new Thread(Process);
      this.processThread.Start();
    }

    /// <summary>
    /// Stops the processing thread.
    /// </summary>
    public void Stop()
    {
      this.run = false;
      this.processThread.Join();
    }

    private void Process()
    {
      while (this.run)
      {
        if (DateTime.Now.Subtract(this.lastMessageSent).TotalSeconds > 20d)
        {
          SendKeepAlive();
        }

        Thread.Sleep(100);
      }
    }

    /// <summary>
    /// Keeps the connection alive.
    /// </summary>
    private void SendKeepAlive()
    {
      var request = new KeepAliveRequest(Guid.NewGuid());

      try
      {
        var response = Execute<KeepAliveResponse>(request);
      }
      catch
      { }
    }
  }
}
