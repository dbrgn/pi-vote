
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
  public class VotingStatusResponse : RpcResponse
  {
    public VotingStatus VotingStatus { get; private set; }

    /// <summary>
    /// List of authorities that have done the current step.
    /// Null if not appliable.
    /// </summary>
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
    public VotingStatusResponse(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write((int)VotingStatus);

      if (AuthoritiesDone == null)
      {
        context.Write(-1);
      }
      else
      {
        context.Write(AuthoritiesDone.Count);
        AuthoritiesDone.ForEach(authorityId => context.Write(authorityId));
      }
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingStatus = (VotingStatus)context.ReadInt32();

      int count = context.ReadInt32();

      if (count < 0)
      {
        AuthoritiesDone = null;
      }
      else
      {
        AuthoritiesDone = new List<Guid>();
        count.Times(() => AuthoritiesDone.Add(context.ReadGuid()));
      }
    }
  }
}
