
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
  public class PushEnvelopeVoterRequest : RpcRequest<VotingRpcServer, PushEnvelopeVoterResponse>
  {
    private Guid votingId;

    private Signed<Envelope> signedEnvelope;

    public PushEnvelopeVoterRequest(
      Guid requestId,
      Guid votingId,
      Signed<Envelope> signedEnvelope)
      : base(requestId)
    {
      this.votingId = votingId;
      this.signedEnvelope = signedEnvelope;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public PushEnvelopeVoterRequest(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(this.votingId);
      context.Write(this.signedEnvelope);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      this.votingId = context.ReadGuid();
      this.signedEnvelope = context.ReadObject<Signed<Envelope>>();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override PushEnvelopeVoterResponse Execute(VotingRpcServer server)
    {
      var voting = server.GetVoting(this.votingId);
      voting.Vote(this.signedEnvelope);

      return new PushEnvelopeVoterResponse(RequestId);
    }
  }
}
