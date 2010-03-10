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
  /// Certificate of identity.
  /// </summary>
  public abstract class Certificate : Serializable
  {
    /// <summary>
    /// Id of the certificate.
    /// </summary>
    public Guid Id { get; private set; }

    /// <summary>
    /// Public and possibly private key of the certificate.
    /// </summary>
    private byte[] Key { get; set; }

    /// <summary>
    /// Signatures affixed to the certificate.
    /// </summary>
    private List<Signature> signatures;

    /// <summary>
    /// Signatures affixed to the certificate.
    /// </summary>
    public IEnumerable<Signature> Signatures { get { return this.signatures; } }

    /// <summary>
    /// Signature from the certificate itself.
    /// </summary>
    /// <remarks>
    /// This is necessary to hold together the parts securely.
    /// </remarks>
    private byte[] SelfSignature { get; set; }

    /// <summary>
    /// Date of creation of this certificate.
    /// </summary>
    public DateTime CreationDate { get; private set; }

    /// <summary>
    /// Create a new certificate.
    /// </summary>
    public Certificate()
    {
      CreationDate = DateTime.Now;
      Id = Guid.NewGuid();

      RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
      rsaProvider.KeySize = 4096;
      Key = rsaProvider.ExportCspBlob(true);

      this.signatures = new List<Signature>();
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public Certificate(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Copies a certificate.
    /// </summary>
    /// <param name="original">Original to be copied.</param>
    /// <param name="onlyPublicPart">Copy only public key?</param>
    protected Certificate(Certificate original, bool onlyPublicPart)
    {
      if (original == null)
        throw new ArgumentNullException("original");

      CreationDate = original.CreationDate;
      Id = original.Id;

      if (onlyPublicPart)
      {
        Key = original.GetRsaProvider().ExportCspBlob(false);
      }
      else
      {
        Key = Key;
      }

      SelfSignature = original.SelfSignature;

      this.signatures = new List<Signature>();
      original.signatures.ForEach(signature => signatures.Add(signature.Clone));
    }

    /// <summary>
    /// Gets the RSA provider for the saved public and possibly private key.
    /// </summary>
    /// <returns>An RSA provider.</returns>
    private RSACryptoServiceProvider GetRsaProvider()
    {
      var rsaProvider = new RSACryptoServiceProvider();
      rsaProvider.ImportCspBlob(Key);
      return rsaProvider;
    }

    /// <summary>
    /// Sign some data with the certificate.
    /// </summary>
    /// <param name="data">Data to be signed.</param>
    /// <returns>Signature data.</returns>
    public byte[] Sign(byte[] data)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      if (!HasPrivateKey)
        throw new InvalidOperationException("No private key.");

      var rsaProvider = GetRsaProvider();
      return rsaProvider.SignData(data, "SHA256");
    }

    /// <summary>
    /// Verifies a signature made with this certificate.
    /// </summary>
    /// <remarks>
    /// Also check the validity of the certificate.
    /// </remarks>
    /// <param name="data">Data which was signed.</param>
    /// <param name="signature">Signature data.</param>
    /// <returns>Is the signature valid?</returns>
    public bool Verify(byte[] data, byte[] signature, ICertificateStorage certificateStorage)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      if (signature == null)
        throw new ArgumentNullException("signature");

      var rsaProvider = GetRsaProvider();
      return rsaProvider.VerifyData(data, "SHA256", signature) && Valid(certificateStorage);
    }

    /// <summary>
    /// Verifies a signature without verifying the certificate itself.
    /// </summary>
    /// <param name="data">Data which was signed.</param>
    /// <param name="signature">Signature data.</param>
    /// <returns>Is the signature valid?</returns>
    public bool VerifySimple(byte[] data, byte[] signature)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      if (signature == null)
        throw new ArgumentNullException("signature");

      var rsaProvider = GetRsaProvider();
      return rsaProvider.VerifyData(data, "SHA256", signature);
    }
    
    /// <summary>
    /// Verifies a signature made with this certificate.
    /// </summary>
    /// <remarks>
    /// Also check the validity of the certificate.
    /// </remarks>
    /// <param name="data">Data which was signed.</param>
    /// <param name="signature">Signature data.</param>
    /// <param name="date">Date at which the signature must be valid.</param>
    /// <returns>Is the signature valid?</returns>
    public bool Verify(byte[] data, byte[] signature, ICertificateStorage certificateStorage, DateTime date)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      if (signature == null)
        throw new ArgumentNullException("signature");

      var rsaProvider = GetRsaProvider();
      return rsaProvider.VerifyData(data, "SHA256", signature) && Valid(certificateStorage, date);
    }

    /// <summary>
    /// Is the certificate valid?
    /// </summary>
    /// <param name="certificateStorage">Storage of certificates.</param>
    /// <returns>Is certificate valid.</returns>
    public bool Valid(ICertificateStorage certificateStorage)
    {
      return Valid(certificateStorage, DateTime.Now);
    }

    /// <summary>
    /// Is the certificate valid?
    /// </summary>
    /// <param name="certificateStorage">Storage of certificates.</param>
    /// <param name="date">Date on which it must be valid.</param>
    /// <returns>Is certificate valid.</returns>
    public bool Valid(ICertificateStorage certificateStorage, DateTime date)
    {
      return SelfSignatureValid &&
        (signatures.Any(signature => signature.Verify(GetSignatureContent(), certificateStorage, date) && !certificateStorage.IsRevoked(signature.SignerId, Id, date)) ||
        certificateStorage.IsRootCertificate(this));
    }

    /// <summary>
    /// This this certificate identic to another one?
    /// </summary>
    /// <param name="other">Other certificate to compare.</param>
    /// <returns>Is it identic?</returns>
    public bool IsIdentic(Certificate other)
    {
      if (other == null)
        throw new ArgumentNullException("other");

      return Id == other.Id;
    }

    /// <summary>
    /// Encrypt some data for the holder of the private key
    /// associated with this certificate.
    /// </summary>
    /// <param name="data">Data to encrypt.</param>
    /// <returns>Encrypted data.</returns>
    public byte[] Encrypt(byte[] data)
    {
      if (data == null)
        throw new ArgumentNullException("data");

      var rsaProvider = GetRsaProvider();

      RijndaelManaged rijndael = new RijndaelManaged();
      rijndael.BlockSize = 128;
      rijndael.KeySize = 256;
      rijndael.GenerateIV();
      rijndael.GenerateKey();
      rijndael.Mode = CipherMode.CBC;
      rijndael.Padding = PaddingMode.ISO10126;
      
      MemoryStream memoryStream = new MemoryStream();
      CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
      cryptoStream.Write(data, 0, data.Length);
      cryptoStream.Close();
      memoryStream.Close();

      byte[] encryptedKey = rsaProvider.Encrypt(rijndael.Key, true);

      return encryptedKey.Concat(rijndael.IV, memoryStream.ToArray());
    }

    /// <summary>
    /// Decrypt data encrypted for this certificate.
    /// </summary>
    /// <param name="data">Encrypted data.</param>
    /// <returns>Decrypted data.</returns>
    public byte[] Decrypt(byte[] data)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      if (!HasPrivateKey)
        throw new InvalidProgramException("No public key.");

      var rsaProvider = GetRsaProvider();

      byte[] encryptedKey = data.Part(0, 128);
      byte[] cipherText = data.Part(144);

      RijndaelManaged rijndael = new RijndaelManaged();
      rijndael.BlockSize = 128;
      rijndael.KeySize = 256;
      rijndael.IV = data.Part(128, 16);
      rijndael.Key = rsaProvider.Decrypt(encryptedKey, true);
      rijndael.Mode = CipherMode.CBC;
      rijndael.Padding = PaddingMode.ISO10126;

      MemoryStream memoryStream = new MemoryStream();
      CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
      cryptoStream.Write(cipherText, 0, cipherText.Length);
      cryptoStream.Close();
      memoryStream.Close();

      return memoryStream.ToArray();
    }

    /// <summary>
    /// Does this certificate contain a private key?
    /// </summary>
    public bool HasPrivateKey
    {
      get { return !GetRsaProvider().PublicOnly; }
    }

    /// <summary>
    /// Copy only the public key part of this certificate.
    /// </summary>
    public abstract Certificate OnlyPublicPart { get; }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(MagicTypeConstant);
      context.Write(Id.ToByteArray());
      context.Write(CreationDate);
      context.Write(Key);
      context.Write(SelfSignature);
      context.WriteList(this.signatures);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);

      if (!context.ReadBytes().Equal(MagicTypeConstant))
        throw new InvalidOperationException("Magic type contant wrong.");

      Id = new Guid(context.ReadBytes());
      CreationDate = context.ReadDateTime();
      Key = context.ReadBytes();
      SelfSignature = context.ReadBytes();
      this.signatures = context.ReadObjectList<Signature>();
    }

    /// <summary>
    /// Adds data to be signed to the stream.
    /// </summary>
    /// <remarks>
    /// Be sure to call the base when overriding.
    /// </remarks>
    /// <param name="writer">Stream writer.</param>
    protected virtual void AddSignatureContent(BinaryWriter writer)
    {
      writer.Write(MagicTypeConstant);
      writer.Write(CreationDate.Ticks);
      writer.Write(Id.ToByteArray());

      //Obviously the private key must not be signed as it is not given out.
      writer.Write(HasPrivateKey ? GetRsaProvider().ExportCspBlob(false) : Key);
    }

    /// <summary>
    /// Assembles the data to be signed for the certificate.
    /// </summary>
    /// <returns>Data to be signed.</returns>
    private byte[] GetSignatureContent()
    {
      MemoryStream signatureContent = new MemoryStream();
      BinaryWriter contentWriter = new BinaryWriter(signatureContent);
      AddSignatureContent(contentWriter);
      contentWriter.Close();
      signatureContent.Close();

      return signatureContent.ToArray();
    }

    /// <summary>
    /// Add a signature to the certificate.
    /// </summary>
    /// <param name="signer">Certificate used to sign this one.</param>
    /// <returns>New signature.</returns>
    public Signature AddSignature(Certificate signer, DateTime validUntil)
    {
      if (signer == null)
        throw new ArgumentNullException("signer");

      Signature signature = new Signature(signer, GetSignatureContent(), validUntil);
      AddSignature(signature);

      return signature;
    }

    /// <summary>
    /// Add a signature to the certificate.
    /// </summary>
    /// <param name="signature">Signature to add.</param>
    public void AddSignature(Signature signature)
    {
      if (signature == null)
        throw new ArgumentNullException("signature");

      if (!this.signatures.Any(other => other.SignerId == signature.SignerId))
      {
        this.signatures.Add(signature);
      }
    }

    /// <summary>
    /// Load certificate from file.
    /// </summary>
    /// <param name="fileName">Filename pointing to a certificate</param>
    /// <returns></returns>
    public static Certificate Load(string fileName)
    {
      if (fileName == null)
        throw new ArgumentNullException("fileName");
      if (!File.Exists(fileName))
        throw new ArgumentException("Certificate file not found.");

      return Serializable.FromBinary<Certificate>(File.ReadAllBytes(fileName));
    }

    /// <summary>
    /// Create the self signature.
    /// </summary>
    /// <remarks>
    /// Must always be called after creation of a new certificate.
    /// </remarks>
    public void CreateSelfSignature()
    {
      SelfSignature = Sign(GetSignatureContent());
    }

    /// <summary>
    /// Is the selfsignature valid?
    /// </summary>
    public bool SelfSignatureValid
    {
      get
      {
        var rsaProvider = GetRsaProvider();
        return rsaProvider.VerifyData(GetSignatureContent(), "SHA256", SelfSignature);
      }
    }

    /// <summary>
    /// Adds signatures from certificate. 
    /// </summary>
    /// <param name="certificate">Certificate in question.</param>
    public void Merge(Certificate certificate)
    {
      if (!IsIdentic(certificate))
        throw new ArgumentException("Certificate not identic");

      certificate.Signatures.Foreach(signature => AddSignature(signature));
    }

    /// <summary>
    /// Can this certificate sign other certificates.
    /// </summary>
    public abstract bool CanSignCertificates { get; }

    /// <summary>
    /// The magic certificate type constant makes sure
    /// a certificate is never mistaken for another type
    /// of certificate.
    /// </summary>
    public abstract byte[] MagicTypeConstant { get; }

    /// <summary>
    /// Type of the certificate in multilingual text.
    /// </summary>
    public virtual string TypeText
    {
      get { return LibraryResources.CertificateTypeUnknown; }
    }

    /// <summary>
    /// Full name of the subject.
    /// N/A in case of voter certificate.
    /// </summary>
    public virtual string FullName
    {
      get { return LibraryResources.CertificateFullNameNotAvailable; }
    }
  }
}
