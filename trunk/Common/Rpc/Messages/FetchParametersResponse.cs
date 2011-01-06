
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
  /// RPC response to the request to fetch voting parameters.
  /// </summary>
  [SerializeObject("RPC response to the request to fetch voting parameters.")]
  public class FetchParametersResponse : RpcResponse
  {
    /// <summary>
    /// Index of the authority.
    /// </summary>
    [SerializeField(0, "Index of the authority.")]
    public int AuthorityIndex { get; private set; }

    /// <summary>
    /// Parameters of the voting.
    /// </summary>
    [SerializeField(1, "Parameters of the voting.")]
    public Signed<VotingParameters> VotingParameters { get; private set; }

    /// <summary>
    /// Create a new response to the request to fetch voting parameters.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="authorityIndex">Index of the authority.</param>
    /// <param name="votingParameters">Parameters of the voting.</param>
    public FetchParametersResponse(Guid requestId, int authorityIndex, Signed<VotingParameters> votingParameters)
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
    public FetchParametersResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchParametersResponse(DeserializeContext context)
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
      VotingParameters = context.ReadObject<Signed<VotingParameters>>();
    }
  }
}
