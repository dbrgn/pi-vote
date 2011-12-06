using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Notary/authority sign check on a signature request.
  /// </summary>
  [SerializeObject("Notary/authority sign check on a signature request.")]
  public class SignatureRequestSignCheck : Serializable
  {
    /// <summary>
    /// Signed sign check cookie from the notary/authory.
    /// </summary>
    [SerializeField(0, "Signed sign check cookie from the notary/authory.")]
    public Signed<SignCheckCookie> Cookie { get; private set; }

    /// <summary>
    /// Certificate which was signed by the notary/authority.
    /// </summary>
    [SerializeField(1, "Certificate which was signed by the notary/authority.")]
    public Certificate Certificate { get; private set; }
    
    /// <summary>
    /// Date and time at which the sign check was created.
    /// </summary>
    [SerializeField(2, "Date and time at which the sign check was created.")]
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
