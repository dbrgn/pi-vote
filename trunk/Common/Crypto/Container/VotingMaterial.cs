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
  public class VotingMaterial : Serializable
  {
    public int VotingId { get; private set; }

    public ParameterContainer ParameterContainer { get; private set; }

    public List<SignedContainer<ShareResponse>> PublicKeyParts { get; private set; }

    public VotingMaterial(int votingId, ParameterContainer parameterContainer, IEnumerable<SignedContainer<ShareResponse>> publicKeyParts)
    {
      VotingId = votingId;
      ParameterContainer = parameterContainer;
      PublicKeyParts = new List<SignedContainer<ShareResponse>>(publicKeyParts);
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
      ParameterContainer = context.ReadObject<ParameterContainer>();
      PublicKeyParts = context.ReadObjectList<SignedContainer<ShareResponse>>();
    }
  }
}
