

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
  /// RPC response delivering voting material.
  /// </summary>
  [SerializeObject("RPC response delivering voting material.")]
  public class FetchVotingMaterialVoterResponse : RpcResponse
  {
    [SerializeField(0, "List of tuples of voting material, status, and authorities.")]
    private List<Tuple<VotingMaterial, VotingStatus, List<Guid>>> votingMaterials;

    /// <summary>
    /// List of tuples of voting material, status, and authorities.
    /// </summary>
    public IEnumerable<Tuple<VotingMaterial, VotingStatus, List<Guid>>> VotingMaterials { get { return this.votingMaterials; } }

    public FetchVotingMaterialVoterResponse(Guid requestId, IEnumerable<Tuple<VotingMaterial, VotingStatus, List<Guid>>> votingMaterials)
      : base(requestId)
    {
      this.votingMaterials = new List<Tuple<VotingMaterial, VotingStatus, List<Guid>>>(votingMaterials);
    }

    /// <summary>
    /// Create a failure response to request.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="exception">Exception that occured when executing the request.</param>
    public FetchVotingMaterialVoterResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchVotingMaterialVoterResponse(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);

      if (Exception == null)
      {
        context.Write(this.votingMaterials.Count);

        foreach (var votingMaterial in this.votingMaterials)
        {
          context.Write(votingMaterial.First);
          context.Write((int)votingMaterial.Second);
          context.WriteList(votingMaterial.Third);
        }
      }
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);

      if (Exception == null)
      {
        this.votingMaterials = new List<Tuple<VotingMaterial, VotingStatus, List<Guid>>>();
        int count = context.ReadInt32();

        for (int index = 0; index < count; index++)
        {
          this.votingMaterials.Add(
            new Tuple<VotingMaterial, VotingStatus, List<Guid>>(
              context.ReadObject<VotingMaterial>(),
              (VotingStatus)context.ReadInt32(),
              context.ReadGuidList()));
        }
      }
    }
  }
}
