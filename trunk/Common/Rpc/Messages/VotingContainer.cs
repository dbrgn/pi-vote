using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// Container that hold all information pertaining to a voting.
  /// </summary>
  [SerializeObject("Container that hold all information pertaining to a voting.")]
  public class VotingContainer : Serializable
  {
        /// <summary>
    /// Id of the voting.
    /// </summary>
    [SerializeField(0, "Id of the voting.")]
    public VotingMaterial Material { get; private set; }

    /// <summary>
    /// All authorities on that voting.
    /// </summary>
    [SerializeField(1, "All authorities on that voting.")]
    public List<Certificate> Authorities { get; private set; }

    /// <summary>
    /// Authorities that are done with the current step of this voting.
    /// </summary>
    [SerializeField(2, "Authorities that are done with the current step of this voting.")]
    public List<Guid> AuthoritiesDone { get; private set; }

    /// <summary>
    /// Status of the voting.
    /// </summary>
    [SerializeField(3, "Status of the voting.")]
    public VotingStatus Status { get; private set; }

    /// <summary>
    /// Number of votes cast.
    /// </summary>
    [SerializeField(4, "Number of votes cast.")]
    public int EnvelopeCount { get; private set; }

    public VotingContainer(VotingMaterial material, List<Certificate> authorities, List<Guid> authoritiesDone, VotingStatus status, int envelopeCount)
    {
      Material = material;
      Authorities = authorities;
      AuthoritiesDone = authoritiesDone;
      Status = status;
      EnvelopeCount = envelopeCount;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public VotingContainer(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Material);
      context.WriteList(Authorities);
      context.WriteList(AuthoritiesDone);
      context.Write((int)Status);
      context.Write(EnvelopeCount);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      Material = context.ReadObject<VotingMaterial>();
      Authorities = context.ReadObjectList<Certificate>();
      AuthoritiesDone = context.ReadGuidList();
      Status = (VotingStatus)context.ReadInt32();
      EnvelopeCount = context.ReadInt32();
    }
  }
}
