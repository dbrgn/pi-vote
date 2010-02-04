
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
  /// Response to the request to fetch voting parameters.
  /// </summary>
  public class FetchParametersAuthorityResponse : RpcResponse
  {
    /// <summary>
    /// Index of the authority.
    /// </summary>
    public int AuthorityIndex { get; private set; }

    /// <summary>
    /// Parameters of the voting.
    /// </summary>
    public VotingParameters VotingParameters { get; private set; }

    /// <summary>
    /// Create a new response to the request to fetch voting parameters.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="authorityIndex">Index of the authority.</param>
    /// <param name="votingParameters">Parameters of the voting.</param>
    public FetchParametersAuthorityResponse(Guid requestId, int authorityIndex, VotingParameters votingParameters)
      : base(requestId)
    {
      AuthorityIndex = authorityIndex;
      VotingParameters = votingParameters;
    }

    /// <summary>
    /// Create a failure response to request.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="exception">Exception that occured when executing the request.</param>
    public FetchParametersAuthorityResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchParametersAuthorityResponse(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(AuthorityIndex);
      context.Write(VotingParameters);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      AuthorityIndex = context.ReadInt32();
      VotingParameters = context.ReadObject<VotingParameters>();
    }
  }
}
