
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
  public class PushPartialDecipherAuthorityRequest : RpcRequest<VotingRpcServer, PushPartialDecipherAuthorityResponse>
  {
    private int votingId;

    private Signed<PartialDecipherList> signedPartialDecipherList;

    public PushPartialDecipherAuthorityRequest(
      Guid requestId,
      int votingId,
      Signed<PartialDecipherList> signedPartialDecipherList)
      : base(requestId)
    {
      this.votingId = votingId;
      this.signedPartialDecipherList = signedPartialDecipherList;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public PushPartialDecipherAuthorityRequest(DeserializeContext context)
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
      context.Write(this.signedPartialDecipherList);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      this.votingId = context.ReadInt32();
      this.signedPartialDecipherList = context.ReadObject<Signed<PartialDecipherList>>();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override PushPartialDecipherAuthorityResponse Execute(VotingRpcServer server, Certificate signer)
    {
      var voting = server.GetVoting(this.votingId);
      voting.DepositPartialDecipher(this.signedPartialDecipherList);

      return new PushPartialDecipherAuthorityResponse(RequestId);
    }
  }
}
