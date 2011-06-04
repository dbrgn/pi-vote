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
  public class FetchSignCheckListResponse : RpcResponse
  {
    public List<Signed<SignatureRequestSignCheck>> SignChecks { get; private set; }

    public FetchSignCheckListResponse(Guid requestId, IEnumerable<Signed<SignatureRequestSignCheck>> signChecks)
      : base(requestId)
    {
      SignChecks = new List<Signed<SignatureRequestSignCheck>>(signChecks);
    }

    public FetchSignCheckListResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    public FetchSignCheckListResponse(DeserializeContext context, byte version)
      : base(context, version)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.WriteList(SignChecks);
    }

    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      SignChecks = context.ReadObjectList<Signed<SignatureRequestSignCheck>>();
    }
  }
}
