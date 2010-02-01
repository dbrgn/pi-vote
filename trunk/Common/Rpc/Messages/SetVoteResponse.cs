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
  public class SetVoteResponse : RpcResponse
  {
    public SetVoteResponse(Guid requestId)
      : base(requestId)
    { }

    public SetVoteResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    public SetVoteResponse(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
    }
  }
}
