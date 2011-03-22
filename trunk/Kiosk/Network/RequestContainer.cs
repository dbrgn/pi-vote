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

namespace Pirate.PiVote.Kiosk
{
  public class RequestContainer : Serializable
  {
    public SignatureRequest Request { get; private set; }

    public Secure<SignatureRequest> SecureRequest { get; private set; }

    public Secure<SignatureRequestInfo> SecureRequestInfo { get; private set; }

    public RequestContainer(SignatureRequest request,
                            Secure<SignatureRequest> secureRequest,
                            Secure<SignatureRequestInfo> secureRequestInfo)
    {
      Request = request;
      SecureRequest = secureRequest;
      SecureRequestInfo = secureRequestInfo;
    }

    public RequestContainer(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Request);
      context.Write(SecureRequest);
      context.Write(SecureRequestInfo);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Request = context.ReadObject<SignatureRequest>();
      SecureRequest = context.ReadObject<Secure<SignatureRequest>>();
      SecureRequestInfo = context.ReadObject<Secure<SignatureRequestInfo>>();
    }
  }
}
