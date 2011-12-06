using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Prover
{
  public class CertificateProof : Serializable
  {
    public DateTime CreatedDate { get; private set; }

    public string Text { get; private set; }

    public CertificateProof(string text)
    {
      CreatedDate = DateTime.Now;
      Text = text;
    }

    public CertificateProof(DeserializeContext context, byte version)
      : base(context, version)
    { 
    }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(CreatedDate);
      context.Write(Text);
    }

    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      CreatedDate = context.ReadDateTime();
      Text = context.ReadString();
    }
  }
}
