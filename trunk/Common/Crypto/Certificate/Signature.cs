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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// A signature to be fixed at a certificate.
  /// </summary>
  public class Signature : Serializable
  {
    private static byte[] MagicBytes = Encoding.UTF8.GetBytes("Signature");

    /// <summary>
    /// Certificate id of the signer.
    /// </summary>
    public Guid SignerId { get; private set; }

    /// <summary>
    /// Signature data.
    /// </summary>
    public byte[] Data { get; private set; }

    /// <summary>
    /// This signature is valid from then on.
    /// </summary>
    public DateTime ValidFrom { get; private set; }

    /// <summary>
    /// This signature is valid until then.
    /// </summary>
    public DateTime ValidUntil { get; private set; }

    /// <summary>
    /// Create a new signature.
    /// </summary>
    /// <param name="signer">Certificate of the signer.</param>
    /// <param name="objectData">Object data to be signed.</param>
    /// <param name="validUntil">This signature is valid until then.</param>
    public Signature(Certificate signer, byte[] objectData, DateTime validUntil)
      : this(signer, objectData, DateTime.Now, validUntil)
    { }

    /// <summary>
    /// Create a new signature.
    /// </summary>
    /// <param name="signer">Certificate of the signer.</param>
    /// <param name="objectData">Object data to be signed.</param>
    /// <param name="validFrom">This signature is not valid before then.</param>
    /// <param name="validUntil">This signature is valid until then.</param>
    public Signature(Certificate signer, byte[] objectData, DateTime validFrom, DateTime validUntil)
    {
      SignerId = signer.Id;
      ValidFrom = validFrom;
      ValidUntil = validUntil;
      Data = signer.Sign(AssmblySigningData(objectData));
    }

    /// <summary>
    /// Creates a copy of a signature.
    /// </summary>
    /// <param name="original">Original signature to copy from.</param>
    public Signature(Signature original)
    {
      SignerId = original.SignerId;
      Data = (byte[])original.Data.Clone();
      ValidFrom = original.ValidFrom;
      ValidUntil = original.ValidUntil;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public Signature(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Verifies a signature.
    /// </summary>
    /// <remarks>
    /// Also check the validity of the signer's certificate.
    /// </remarks>
    /// <param name="objectData">Data to check against.</param>
    /// <param name="certificateStorage">Storage of certificates.</param>
    /// <param name="date">Date at which the signature must be valid.</param>
    /// <returns>Is the signature valid.</returns>
    public CertificateValidationResult Verify(byte[] objectData, ICertificateStorage certificateStorage, DateTime date)
    {
      if (ValidFrom.Date <= date.Date)
      {
        if (ValidUntil.Date >= date.Date)
        {
          if (certificateStorage.Has(SignerId))
          {
            Certificate signer = certificateStorage.Get(SignerId);

            if (signer.VerifySimple(AssmblySigningData(objectData), Data))
            {
              if (signer.Validate(certificateStorage, date) == CertificateValidationResult.Valid)
              {
                return CertificateValidationResult.Valid;
              }
              else
              {
                return CertificateValidationResult.SignerInvalid;
              }
            }
            else
            {
              return CertificateValidationResult.SignatureDataInvalid;
            }
          }
          else
          {
            return CertificateValidationResult.UnknownSigner;
          }
        }
        else
        {
          return CertificateValidationResult.Outdated;
        }
      }
      else
      {
        return CertificateValidationResult.NotYetValid;
      }
    }

    /// <summary>
    /// Determines until when the signature will be valid if ever.
    /// The date may lay in the past if it is not valid any more.
    /// </summary>
    /// <param name="objectData">Data to check against.</param>
    /// <param name="certificateStorage">Storage of certificates.</param>
    /// <param name="date">Date at which the signers certificate must be valid.</param>
    /// <returns>Date after which the signature expires.</returns>
    public DateTime ExpectedValidUntil(byte[] objectData, ICertificateStorage certificateStorage, DateTime date)
    {
      if (certificateStorage.Has(SignerId))
      {
        Certificate signer = certificateStorage.Get(SignerId);

        if (signer.VerifySimple(AssmblySigningData(objectData), Data))
        {
          if (signer.Validate(certificateStorage, date) == CertificateValidationResult.Valid)
          {
            return ValidUntil;
          }
          else
          {
            return DateTime.MinValue;
          }
        }
        else
        {
          return DateTime.MinValue;
        }
      }
      else
      {
        return DateTime.MinValue;
      }
    }

    /// <summary>
    /// Determines until when the signature will become valid.
    /// </summary>
    /// <param name="certificateStorage">Storage of certificates.</param>
    /// <param name="date">Date at which the signers certificate must be valid.</param>
    /// <returns>Date after which the signature expires.</returns>
    public DateTime ExpectedValidFrom(byte[] objectData, ICertificateStorage certificateStorage)
    {
      if (certificateStorage.Has(SignerId))
      {
        Certificate signer = certificateStorage.Get(SignerId);

        if (signer.VerifySimple(AssmblySigningData(objectData), Data))
        {
          return ValidFrom;
        }
        else
        {
          return DateTime.MaxValue;
        }
      }
      else
      {
        return DateTime.MaxValue;
      }
    }

    /// <summary>
    /// Assembles the data to be signed.
    /// </summary>
    /// <param name="objectData">Data of the object to be signed.</param>
    /// <returns>Bytes of data.</returns>
    private byte[] AssmblySigningData(byte[] objectData)
    {
      MemoryStream memoryStream = new MemoryStream();
      BinaryWriter streamWriter = new BinaryWriter(memoryStream);
      streamWriter.Write(MagicBytes);
      streamWriter.Write(objectData);
      streamWriter.Write(SignerId.ToByteArray());
      streamWriter.Write(ValidFrom.Ticks);
      streamWriter.Write(ValidUntil.Ticks);
      streamWriter.Close();
      memoryStream.Close();

      return memoryStream.ToArray();
    }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(SignerId);
      context.Write(Data);
      context.Write(ValidFrom);
      context.Write(ValidUntil);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      SignerId = context.ReadGuid();
      Data = context.ReadBytes();
      ValidFrom = context.ReadDateTime();
      ValidUntil = context.ReadDateTime();
    }

    /// <summary>
    /// Clones the signature.
    /// </summary>
    public Signature Clone
    {
      get { return new Signature(this); }
    }
  }
}
