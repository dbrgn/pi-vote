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
  /// RPC response delivering a sign check cookie.
  /// </summary>
  [SerializeObject("RPC response delivering a sign check cookie.")]
  public class FetchSignCheckCookieResponse : RpcResponse
  {
    /// <summary>
    /// Signed sign check cookie.
    /// </summary>
    [SerializeField(0, "Signed sign check cookie.")]
    public Signed<SignCheckCookie> Cookie { get; private set; }

    public FetchSignCheckCookieResponse(
      Guid requestId,
      Signed<SignCheckCookie> cookie)
      : base(requestId)
    {
      Cookie = cookie;
    }

    /// <summary>
    /// Create a failure response to request.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="exception">Exception that occured when executing the request.</param>
    public FetchSignCheckCookieResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchSignCheckCookieResponse(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Cookie);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      Cookie = context.ReadObject<Signed<SignCheckCookie>>();
    }
  }
}
