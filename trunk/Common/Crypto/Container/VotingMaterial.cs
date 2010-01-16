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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// All things a voter needs to cast his vote.
  /// </summary>
  public class VotingMaterial : Serializable
  {
    /// <summary>
    /// Id of the voting procedure.
    /// </summary>
    public int VotingId { get; private set; }

    /// <summary>
    /// Defines voting procedure.
    /// </summary>
    public VotingParameters ParameterContainer { get; private set; }

    /// <summary>
    /// Responses that can be combined to a public key.
    /// </summary>
    public List<Signed<ShareResponse>> PublicKeyParts { get; private set; }

    public VotingMaterial(int votingId, VotingParameters parameterContainer, IEnumerable<Signed<ShareResponse>> publicKeyParts)
    {
      VotingId = votingId;
      ParameterContainer = parameterContainer;
      PublicKeyParts = new List<Signed<ShareResponse>>(publicKeyParts);
    }

    public VotingMaterial(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(ParameterContainer);
      context.WriteList(PublicKeyParts);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      ParameterContainer = context.ReadObject<VotingParameters>();
      PublicKeyParts = context.ReadObjectList<Signed<ShareResponse>>();
    }
  }
}
