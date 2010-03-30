
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
    public CertificateStorage CertificateStorage { get; private set; }

    public VotingParameters Parameters { get; private set; }

    public AllShareParts AllShareParts { get; private set; }

    public string FileName(Guid authorityId)
    {
      return string.Format("{0}-{1}.pi-badshareproof", Parameters.VotingId.ToString(), authorityId.ToString());
    }

    public BadShareProof(CertificateStorage certificateStorage, VotingParameters parameters, AllShareParts allShareParts)
    {
      CertificateStorage = certificateStorage;
      Parameters = parameters;
      AllShareParts = allShareParts;
    }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(CertificateStorage);
      context.Write(Parameters);
      context.Write(AllShareParts);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      CertificateStorage = context.ReadObject<CertificateStorage>();
      Parameters = context.ReadObject<VotingParameters>();
      AllShareParts = context.ReadObject<AllShareParts>();
    }

    public BadShareProof(DeserializeContext context)
      : base(context)
    { }
  }
}
