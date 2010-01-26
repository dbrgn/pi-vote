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
    /// Certificate of the signer.
    /// </summary>
    public Certificate Signer { get; private set; }

    /// <summary>
    /// Signature data.
    /// </summary>
    public byte[] Data { get; private set; }

    /// <summary>
    /// Date of creation of this signature.
    /// </summary>
    public DateTime CreationDate { get; private set; }

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
    {
      Signer = signer.OnlyPublicPart;
      CreationDate = DateTime.Now;
      ValidUntil = validUntil;
      Data = signer.Sign(AssmblySigningData(objectData));
    }

    /// <summary>
    /// Creates a copy of a signature.
    /// </summary>
    /// <param name="original">Original signature to copy from.</param>
    public Signature(Signature original)
    {
      Signer = original.Signer.OnlyPublicPart;
      Data = (byte[])original.Data.Clone();
      CreationDate = original.CreationDate;
      ValidUntil = original.ValidUntil;
    }

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
    /// <returns>Is the signature valid.</returns>
    public bool Verify(byte[] objectData)
    {
      return
        Signer.Verify(AssmblySigningData(objectData), Data) &&
        Signer.Valid &&
        ValidUntil >= DateTime.Now;
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
      streamWriter.Write(CreationDate.Ticks);
      streamWriter.Write(ValidUntil.Ticks);
      streamWriter.Close();
      memoryStream.Close();

      return memoryStream.ToArray();
    }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Signer);
      context.Write(Data);
      context.Write(CreationDate);
      context.Write(ValidUntil);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Signer = context.ReadObject<Certificate>();
      Data = context.ReadBytes();
      CreationDate = context.ReadDateTime();
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
