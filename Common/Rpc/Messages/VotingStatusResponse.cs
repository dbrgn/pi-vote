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
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// RPC response delivering the voting status.
  /// </summary>
  [SerializeObject("RPC response delivering the voting status.")]
  public class VotingStatusResponse : RpcResponse
  {
    /// <summary>
    /// Status of the voting.
    /// </summary>
    [SerializeField(0, "Status of the voting.")]
    public VotingStatus VotingStatus { get; private set; }

    /// <summary>
    /// List of authorities that have done the current step.
    /// </summary>
    [SerializeField(1, "List of authorities that have done the current step.")]
    public List<Guid> AuthoritiesDone { get; private set; }

    public VotingStatusResponse(Guid requestId, VotingStatus votingStatus, List<Guid> authoritiesDone)
      : base(requestId)
    {
      VotingStatus = votingStatus;
      AuthoritiesDone = authoritiesDone;
    }

    /// <summary>
    /// Create a failure response to request.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="exception">Exception that occured when executing the request.</param>
    public VotingStatusResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public VotingStatusResponse(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write((int)VotingStatus);
      context.WriteList(this.AuthoritiesDone);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      VotingStatus = (VotingStatus)context.ReadInt32();
      this.AuthoritiesDone = context.ReadGuidList();
    }
  }
}
