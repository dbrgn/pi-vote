﻿/*
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
  /// RPC response to a request to fetch the list of authorities.
  /// </summary>
  [SerializeObject("RPC response to a request to fetch the list of authorities.")]
  public class FetchAuthorityListResponse : RpcResponse
  {
    /// <summary>
    /// List of authorities for the voting.
    /// </summary>
    [SerializeField(0, "List of authorities for the voting.")]
    public AuthorityList AuthorityList { get; private set; }

    /// <summary>
    /// Create a response to a request to fetch the list of authorities.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="authorityList">List of authorities for the voting.</param>
    public FetchAuthorityListResponse(Guid requestId, AuthorityList authorityList)
      : base(requestId)
    {
      AuthorityList = authorityList;
    }

    /// <summary>
    /// Create a failure response to request.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="exception">Exception that occured when executing the request.</param>
    public FetchAuthorityListResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchAuthorityListResponse(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(AuthorityList);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      AuthorityList = context.ReadObject<AuthorityList>();
    }
  }
}
