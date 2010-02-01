

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
  public class AuthorityGetAllSharesResponse : RpcResponse
  {
    public AllShareParts AllShareParts { get; private set; }

    public AuthorityGetAllSharesResponse(Guid requestId, AllShareParts allShareParts)
      : base(requestId)
    {
      AllShareParts = allShareParts;
    }

    public AuthorityGetAllSharesResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    public AuthorityGetAllSharesResponse(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(AllShareParts);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      AllShareParts = context.ReadObject<AllShareParts>();
    }
  }
}
