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
  public class FetchSignatureRequestListResponse : RpcResponse
  {
    public List<Guid> SignatureRequestList { get; private set; }

    public FetchSignatureRequestListResponse(Guid requestId, List<Guid> signatureRequestList)
      : base(requestId)
    {
      SignatureRequestList = signatureRequestList;
    }

    /// <summary>
    /// Create a failure response to request.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="exception">Exception that occured when executing the request.</param>
    public FetchSignatureRequestListResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchSignatureRequestListResponse(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(SignatureRequestList.Count);
      foreach (Guid id in SignatureRequestList)
      {
        context.Write(id);
      }
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      SignatureRequestList = new List<Guid>();
      int count = context.ReadInt32();
      for (int index = 0; index < count; index++)
      {
        SignatureRequestList.Add(context.ReadGuid());
      }
    }
  }
}