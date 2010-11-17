﻿

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
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Rpc
{
  public class ListVotingIdsResponse : RpcResponse
  {
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

      if (Exception == null)
      {
        context.Write(VotingIds.Count);

        foreach (Guid votingId in VotingIds)
        {
          context.Write(votingId);
        }
      }
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);

      if (Exception == null)
      {
        int count = context.ReadInt32();
        VotingIds = new List<Guid>();

        for (int index = 0; index < count; index++)
        {
          VotingIds.Add(context.ReadGuid());
        }
      }
    }
  }
}
