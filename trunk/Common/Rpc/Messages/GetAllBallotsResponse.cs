

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
  public class GetAllBallotsResponse : RpcResponse
  {
    public AuthorityEnvelopeList AuthorityEnvelopeList { get; private set; }

    public GetAllBallotsResponse(Guid requestId, AuthorityEnvelopeList authorityEnvelopeList)
      : base(requestId)
    {
      AuthorityEnvelopeList = authorityEnvelopeList;
    }

    public GetAllBallotsResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    public GetAllBallotsResponse(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(AuthorityEnvelopeList);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      AuthorityEnvelopeList = context.ReadObject<AuthorityEnvelopeList>();
    }
  }
}
