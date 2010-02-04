
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
  public class PushSharesAuthorityRequest : RpcRequest<VotingRpcServer, PushSharesAuthorityResponse>
  {
    private int votingId;

    private Signed<SharePart> signedSharePart;

    public PushSharesAuthorityRequest(
      Guid requestId,
      int votingId,
      Signed<SharePart> signedSharePart)
      : base(requestId)
    {
      this.votingId = votingId;
      this.signedSharePart = signedSharePart;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public PushSharesAuthorityRequest(DeserializeContext context)
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
      context.Write(this.signedSharePart);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      this.votingId = context.ReadInt32();
      this.signedSharePart = context.ReadObject<Signed<SharePart>>();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override PushSharesAuthorityResponse Execute(VotingRpcServer server)
    {
      var voting = server.GetVoting(this.votingId);
      voting.DepositShares(this.signedSharePart);

      return new PushSharesAuthorityResponse(RequestId);
    }
  }
}
