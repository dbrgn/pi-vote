using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;
using System.IO;

namespace Pirate.PiVote.Kiosk
{
  public class KioskServer
  {
    public  SignatureRequest UserData { get; set; }

    public Secure<SignatureRequest> Request { get; set; }

    public Secure<SignatureRequestInfo> RequestInfo { get; set; }

    public CertificateStorage CertificateStorage { get; set; }

    public Certificate ServerCertificate { get; set; }

    public byte[] FetchUserData()
    {
      return UserData.ToBinary();
    }

    public byte[] FetchCertificateStorage()
    {
      return CertificateStorage.ToBinary();
    }

    public byte[] FetchServerCertificate()
    {
      return ServerCertificate.ToBinary();
    }

    public void PushSignatureRequest(byte[] data)
    {
      MemoryStream memoryStream = new MemoryStream(data);
      DeserializeContext context = new DeserializeContext(memoryStream);

      Request = context.ReadObject<Secure<SignatureRequest>>();
      RequestInfo = context.ReadObject<Secure<SignatureRequestInfo>>();

      context.Close();
      memoryStream.Close();
    }
  }
}
