

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
  /// Response to a request to fetch all envelopes.
  /// </summary>
  public class FetchEnvelopesAuthorityResponse : RpcResponse
  {
    /// <summary>
    /// List of all envelopes in the voting.
    /// </summary>
    public AuthorityEnvelopeList AuthorityEnvelopeList { get; private set; }

    /// <summary>
    /// Creates a new response to a request to fetch all envelopes
    /// </summary>
    /// <param name="requestId">Id of thr request.</param>
    /// <param name="authorityEnvelopeList">List of all envelopes.</param>
    public FetchEnvelopesAuthorityResponse(Guid requestId, AuthorityEnvelopeList authorityEnvelopeList)
      : base(requestId)
    {
      AuthorityEnvelopeList = authorityEnvelopeList;
    }

    /// <summary>
    /// Create a failure response to request.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="exception">Exception that occured when executing the request.</param>
    public FetchEnvelopesAuthorityResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchEnvelopesAuthorityResponse(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(AuthorityEnvelopeList);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      AuthorityEnvelopeList = context.ReadObject<AuthorityEnvelopeList>();
    }
  }
}
