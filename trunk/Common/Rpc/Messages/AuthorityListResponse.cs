
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
  public class AuthorityListResponse : RpcResponse
  {
    public AuthorityList AuthorityList { get; private set; }

    public AuthorityListResponse(Guid requestId, AuthorityList authorityList)
      : base(requestId)
    {
      AuthorityList = authorityList;
    }

    public AuthorityListResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    public AuthorityListResponse(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(AuthorityList);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      AuthorityList = context.ReadObject<AuthorityList>();
    }
  }
}
