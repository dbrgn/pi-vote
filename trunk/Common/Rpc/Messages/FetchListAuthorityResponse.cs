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
  /// <summary>
  /// Response to a request to fetch the list of authorities.
  /// </summary>
  public class FetchListAuthorityResponse : RpcResponse
  {
    /// <summary>
    /// List of authorities for the voting.
    /// </summary>
    public AuthorityList AuthorityList { get; private set; }

    /// <summary>
    /// Create a response to a request to fetch the list of authorities.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="authorityList">List of authorities for the voting.</param>
    public FetchListAuthorityResponse(Guid requestId, AuthorityList authorityList)
      : base(requestId)
    {
      AuthorityList = authorityList;
    }

    /// <summary>
    /// Create a failure response to request.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="exception">Exception that occured when executing the request.</param>
    public FetchListAuthorityResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchListAuthorityResponse(DeserializeContext context)
      : base(context)
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
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      AuthorityList = context.ReadObject<AuthorityList>();
    }
  }
}