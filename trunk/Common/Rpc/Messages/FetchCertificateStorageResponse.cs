

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
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// RPC response delivering the certificate storage.
  /// </summary>
  [SerializeObject("RPC response delivering the certificate storage.")]
  public class FetchCertificateStorageResponse : RpcResponse
  {
    /// <summary>
    /// Certificate storage from server.
    /// </summary>
    [SerializeField(0, "Certificate storage from server.")]
    public CertificateStorage CertificateStorage { get; private set; }

    /// <summary>
    /// Certificate of the server.
    /// </summary>
    [SerializeField(1, "Certificate of the server.")]
    public Certificate ServerCertificate { get; private set; }

    public FetchCertificateStorageResponse(Guid requestId, CertificateStorage certificateStorage, Certificate serverCertificate)
      : base(requestId)
    {
      CertificateStorage = certificateStorage;
      ServerCertificate = serverCertificate;
    }

    /// <summary>
    /// Create a failure response to request.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="exception">Exception that occured when executing the request.</param>
    public FetchCertificateStorageResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchCertificateStorageResponse(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(CertificateStorage);
      context.Write(ServerCertificate);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      CertificateStorage = context.ReadObject<CertificateStorage>();
      ServerCertificate = context.ReadObject<ServerCertificate>();
    }
  }
}
