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
using System.IO;
using System.Security.Cryptography;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Certificate entry at a CA.
  /// </summary>
  public class CertificateAuthorityEntry : Serializable
  {
    /// <summary>
    /// Certificate in question.
    /// </summary>
    public Certificate Certificate { get { return Request.Certificate; } }

    /// <summary>
    /// Request for signature.
    /// </summary>
    public Signed<SignatureRequest> Request { get; private set; }

    /// <summary>
    /// Response to signature request.
    /// </summary>
    public Signed<SignatureResponse> Response { get; private set; }

    /// <summary>
    /// Is this certificate revoked?
    /// </summary>
    public bool Revoked { get; private set; }

    /// <summary>
    /// Creates a new certificate entry.
    /// </summary>
    /// <param name="request">Request to create from.</param>
    public CertificateAuthorityEntry(Signed<SignatureRequest> request)
    {
      Request = request;
      Response = null;
      Revoked = false;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public CertificateAuthorityEntry(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Request);
      context.Write(Response);
      context.Write(Revoked);
    }

    /// <summary>
    /// /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Request = context.ReadObject<Signed<SignatureRequest>>();
      Response = context.ReadObject<Signed<SignatureResponse>>();
      Revoked = context.ReadBoolean();
    }

    /// <summary>
    /// Signs the certificates and create response
    /// </summary>
    /// <param name="caCertificate">Certificate of the CA.</param>
    /// <param name="validUntil">Signature valid until.</param>
    public void Sign(CACertificate caCertificate, DateTime validUntil)
    {
      SignatureRequest request = Request.Value;
      Signature signature = Certificate.AddSignature(caCertificate, validUntil);
      SignatureResponse response = new SignatureResponse(signature);
      Response = new Signed<SignatureResponse>(response, caCertificate);
    }

    /// <summary>
    /// Refuse signature and create response.
    /// </summary>
    /// <param name="caCertificate">Certificate of the CA.</param>
    /// <param name="reason">Reason for refusing to sign.</param>
    public void Refuse(CACertificate caCertificate, string reason)
    {
      SignatureRequest request = Request.Value;
      SignatureResponse response = new SignatureResponse(reason);
      Response = new Signed<SignatureResponse>(response, caCertificate);
    }

    /// <summary>
    /// Revoke this certificate.
    /// </summary>
    public void Revoke()
    {
      Revoked = true;
    }
  }
}
