
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
  /// RPC request to end a voting procedure.
  /// </summary>
  public class EndVotingAdminRequest : RpcRequest<VotingRpcServer, EndVotingAdminResponse>
  {
    /// <summary>
    /// Id of the voting.
    /// </summary>
    private int votingId;

    /// <summary>
    /// Creates a request to end voting.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="votingId">Id of the voting.</param>
    public EndVotingAdminRequest(
      Guid requestId,
      int votingId)
      : base(requestId)
    {
      this.votingId = votingId;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public EndVotingAdminRequest(DeserializeContext context)
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
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      this.votingId = context.ReadInt32();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override EndVotingAdminResponse Execute(VotingRpcServer server)
    {
      var voting = server.GetVoting(this.votingId);
      voting.EndVote();

      return new EndVotingAdminResponse(RequestId);
    }
  }
}
