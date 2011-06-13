using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  public class SignCheckCookie : Serializable
  {
    public byte[] Randomness { get; private set; }

    public DateTime CreationDate { get; private set; }

    public SignCheckCookie()
    {
      Randomness = new byte[32];
      RandomNumberGenerator.Create().GetBytes(Randomness);
      CreationDate = DateTime.Now;
    }

    public SignCheckCookie(DeserializeContext context, byte version)
      : base(context, version)
    {
    }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(CreationDate);
      context.Write(Randomness);
    }

    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      CreationDate = context.ReadDateTime();
      Randomness = context.ReadBytes();
    }
  }
}
