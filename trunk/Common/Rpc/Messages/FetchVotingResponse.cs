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
  /// RPC response delivering voting material.
  /// </summary>
  [SerializeObject("RPC response delivering voting containers.")]
  public class FetchVotingResponse : RpcResponse
  {
    [SerializeField(0, "List of voting containers.")]
    private List<VotingContainer> votings;

    /// <summary>
    /// List of tuples of voting material, status, and authorities.
    /// </summary>
    public IEnumerable<VotingContainer> Votings { get { return this.votings; } }

    public FetchVotingResponse(Guid requestId, IEnumerable<VotingContainer> votings)
      : base(requestId)
    {
      this.votings = new List<VotingContainer>(votings);
    }

    /// <summary>
    /// Create a failure response to request.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="exception">Exception that occured when executing the request.</param>
    public FetchVotingResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchVotingResponse(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);

      if (Exception == null)
      {
        context.WriteList(this.votings);
      }
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);

      if (Exception == null)
      {
        this.votings = context.ReadObjectList<VotingContainer>();
      }
    }
  }
}
