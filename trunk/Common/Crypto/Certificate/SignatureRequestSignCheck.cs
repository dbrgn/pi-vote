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
    private byte[] signerX509certificate;

    public Certificate Certificate { get; private set; }
    
    public DateTime SignDateTime { get; private set; }

    public X509Certificate2 Signer
    {
      get { return new X509Certificate2(this.signerX509certificate); }
    }

    public SignatureRequestSignCheck(
      X509Certificate2 signer, 
      Certificate certificate)
    {
      this.signerX509certificate = signer.GetRawCertData();
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
      context.Write(this.signerX509certificate);
      context.Write(Certificate);
      context.Write(SignDateTime);
    }

    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      this.signerX509certificate = context.ReadBytes();
      Certificate = context.ReadObject<Certificate>();
      SignDateTime = context.ReadDateTime();
    }
  }
}
