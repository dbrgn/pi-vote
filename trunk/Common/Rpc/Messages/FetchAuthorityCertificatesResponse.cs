
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
  /// Response to the request to fetch all valid authority certificates.
  /// </summary>
  public class FetchAuthorityCertificatesResponse : RpcResponse
  {
    /// <summary>
    /// List of all valid authority certificates.
    /// </summary>
    public List<AuthorityCertificate> AuthorityCertificates { get; private set; }

    /// <summary>
    /// Create a new response to the request to fetch all valid authority certificates.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="authorityCertificates">List of all valid authority certificates.</param>
    public FetchAuthorityCertificatesResponse(Guid requestId, List<AuthorityCertificate> authorityCertificates)
      : base(requestId)
    {
      AuthorityCertificates = authorityCertificates;
    }

    /// <summary>
    /// Create a failure response to request.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="exception">Exception that occured when executing the request.</param>
    public FetchAuthorityCertificatesResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchAuthorityCertificatesResponse(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.WriteList(AuthorityCertificates);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      AuthorityCertificates = context.ReadObjectList<AuthorityCertificate>();
    }
  }
}
