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
  /// RPC response delivering the list of voting ids.
  /// </summary>
  [SerializeObject("RPC response delivering the list of voting ids.")]
  public class ListVotingIdsResponse : RpcResponse
  {
    /// <summary>
    /// List of voting ids.
    /// </summary>
    [SerializeField(0, "List of voting ids.")]
    public List<Guid> VotingIds { get; private set; }

    public ListVotingIdsResponse(Guid requestId, IEnumerable<Guid> votingIds)
      : base(requestId)
    {
      VotingIds = new List<Guid>(votingIds);
    }

    /// <summary>
    /// Create a failure response to request.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="exception">Exception that occured when executing the request.</param>
    public ListVotingIdsResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public ListVotingIdsResponse(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.WriteList(VotingIds);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingIds = context.ReadGuidList();
    }
  }
}
