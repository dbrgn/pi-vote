using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  public class SignatureRequestSignCheck : Serializable
  {
    public Signed<SignCheckCookie> Cookie { get; private set; }

    public Certificate Certificate { get; private set; }
    
    public DateTime SignDateTime { get; private set; }

    public SignatureRequestSignCheck(
      Certificate certificate,
      Signed<SignCheckCookie> cookie)
    {
      Cookie = cookie;
      Certificate = certificate;
      SignDateTime = DateTime.Now;
    }

    public SignatureRequestSignCheck(DeserializeContext context, byte version)
      : base(context, version)
    { 
    }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Cookie);
      context.Write(Certificate);
      context.Write(SignDateTime);
    }

    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      Cookie = context.ReadObject<Signed<SignCheckCookie>>();
      Certificate = context.ReadObject<Certificate>();
      SignDateTime = context.ReadDateTime();
    }
  }
}
