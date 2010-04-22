
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
  public class BadShareProof : Serializable
  {
    public int ComplainingAuthorityIndex { get; private set; }

    public Dictionary<int, Certificate> Authorities { get; private set; }

    public CertificateStorage CertificateStorage { get; private set; }

    public Signed<VotingParameters> SignedParameters { get; private set; }

    public VotingParameters Parameters { get { return SignedParameters.Value; } }

    public AllShareParts AllShareParts { get; private set; }

    public Dictionary<int, TrapDoor> TrapDoors { get; private set; }

    public string FileName(Guid authorityId)
    {
      return string.Format("{0}-{1}.pi-shareproof", Parameters.VotingId.ToString(), authorityId.ToString());
    }

    public BadShareProof(int complainingAuthorityIndex, CertificateStorage certificateStorage, Signed<VotingParameters> signedParameters, AllShareParts allShareParts, IDictionary<int, TrapDoor> trapDoors, IDictionary<int, Certificate> authorities)
    {
      ComplainingAuthorityIndex = complainingAuthorityIndex;
      CertificateStorage = certificateStorage;
      SignedParameters = signedParameters;
      AllShareParts = allShareParts;
      TrapDoors = new Dictionary<int, TrapDoor>(trapDoors);
      Authorities = new Dictionary<int, Certificate>(authorities);
    }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(ComplainingAuthorityIndex);
      context.Write(CertificateStorage);
      context.Write(SignedParameters);
      context.Write(AllShareParts);
      context.WriteDictionary(TrapDoors);
      context.WriteDictionary(Authorities);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      ComplainingAuthorityIndex = context.ReadInt32();
      CertificateStorage = context.ReadObject<CertificateStorage>();
      SignedParameters = context.ReadObject<Signed<VotingParameters>>();
      AllShareParts = context.ReadObject<AllShareParts>();
      TrapDoors = context.ReadObjectDictionary<TrapDoor>();
      Authorities = context.ReadObjectDictionary<Certificate>();
    }

    public BadShareProof(DeserializeContext context)
      : base(context)
    { }
  }
}
