/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
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
  [SerializeObject("Certificate of identity.")]
  public abstract class Certificate : Serializable
  {
    /// <summary>
    /// Id of the certificate.
    /// </summary>
    [SerializeField(1, "Id of the certificate.")]
    public Guid Id { get; private set; }

    /// <summary>
    /// Public key of the certificate.
    /// </summary>
    [SerializeField(3, "Public key of the certificate.")]
    private byte[] PublicKey { get; set; }

    /// <summary>
    /// Status of the private key.
    /// </summary>
    [SerializeField(7, "Status of the private key. Saved as Encrypted even when Decrypted.")]
    public PrivateKeyStatus PrivateKeyStatus { get; private set; }

    /// <summary>
    /// Data of the private key, either encrypted or unencrypted.
    /// </summary>
    [SerializeField(8, "Data of the private key, either encrypted or unencrypted.", "PrivateKeyStatus is not Unavailable.", null)]
    private byte[] privateKeyData;

    /// <summary>
    /// Salt used in encryption of the private key.
    /// </summary>
    [SerializeField(9, "Salt used in encryption of the private key.", "PrivateKeyStatus is Encrypted or Decrypted.", null)]
    private byte[] privateKeySalt;

    /// <summary>
    /// Salt used to strengthen the passphrase.
    /// </summary>
    [SerializeField(10, "Salt used to strengthen the passphrase.", "PrivateKeyStatus is Encrypted or Decrypted.", null)]
    private byte[] passphraseSalt;

    /// <summary>
    /// Decrypted private key.
    /// </summary>
    private byte[] privateKeyDecrypted;

    /// <summary>
    /// Private key corresponding to the certificate.
    /// </summary>
    private byte[] PrivateKey
    {
      get
      {
        switch (PrivateKeyStatus)
        {
          case PrivateKeyStatus.Unavailable:
            throw new InvalidOperationException("Private key unavailable.");
          case PrivateKeyStatus.Unencrypted:
            return this.privateKeyData;
          case Crypto.PrivateKeyStatus.Encrypted:
            throw new InvalidOperationException("Private key is encrypted.");
          case Crypto.PrivateKeyStatus.Decrypted:
            return this.privateKeyDecrypted;
          default:
            throw new InvalidOperationException("Unknown private key status.");
        }
      }
    }

    /// <summary>
    /// Signatures affixed to the certificate.
    /// </summary>
    [SerializeField(6, "Signatures affixed to the certificate.")]
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
    [SerializeField(4, "Signature from the certificate itself.")]
    private byte[] SelfSignature { get; set; }

    /// <summary>
    /// Date of creation of this certificate.
    /// </summary>
    [SerializeField(2, "Date of creation of this certificate.")]
    public DateTime CreationDate { get; private set; }

    /// <summary>
    /// Attributes of the certificate.
    /// </summary>
    [SerializeField(5, "Attributes of the certificate.")]
    private List<CertificateAttribute> attributes;

    /// <summary>
    /// SHA 256 fingerprint of the certificate.
    /// </summary>
    public string Fingerprint
    {
      get
      {
        SHA256Managed sha256 = new SHA256Managed();
        byte[] hash = sha256.ComputeHash(GetSignatureContent());
        return string.Join(" ", hash.Select(b => string.Format("{0:x2}", b)).ToArray());
      }
    }

    /// <summary>
    /// Create a new certificate.
    /// </summary>
    /// <param name="language">Language preferred by the certificate holder.</param>
    /// <param name="passphrase">Passphrase to encrypt the key with or null for no encryption.</param>
    public Certificate(Language language, string passphrase)
    {
      CreationDate = DateTime.Now;
      Id = Guid.NewGuid();

      RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
      rsaProvider.KeySize = 4096;
      PublicKey = rsaProvider.ExportCspBlob(false);

      PrivateKeyStatus = PrivateKeyStatus.Unencrypted;

      if (passphrase == null)
      {
        this.privateKeyData = rsaProvider.ExportCspBlob(true);
      }
      else
      {
        EncryptPrivateKey(passphrase, rsaProvider);
        Unlock(passphrase);
      }

      this.signatures = new List<Signature>();
      this.attributes = new List<CertificateAttribute>();

      AddAttribute(new Int32CertificateAttribute(CertificateAttributeName.Language, (int)language));
    }

    /// <summary>
    /// Encrypts the private key of RSA using a key derived from the passphrase.
    /// </summary>
    /// <param name="passphrase">Passphrase to protect the private key.</param>
    /// <param name="rsaProvider">RSA provider containing a private key.</param>
    private void EncryptPrivateKey(string passphrase, RSACryptoServiceProvider rsaProvider)
    {
      RandomNumberGenerator rng = RandomNumberGenerator.Create();
      this.passphraseSalt = new byte[32];
      rng.GetBytes(this.passphraseSalt);

      Rfc2898DeriveBytes derive = new Rfc2898DeriveBytes(passphrase, this.passphraseSalt, 1024);
      byte[] key = derive.GetBytes(32);

      this.privateKeySalt = new byte[16];
      rng.GetBytes(this.privateKeySalt);

      byte[] data = rsaProvider.ExportCspBlob(true);
      SHA256Managed sha256 = new SHA256Managed();
      data = data.Concat(sha256.ComputeHash(data));

      this.privateKeyData = Aes.Encrypt(data, key, this.privateKeySalt);
      PrivateKeyStatus = PrivateKeyStatus.Encrypted;
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
      PublicKey = original.PublicKey;

      if (onlyPublicPart)
      {
        PrivateKeyStatus = PrivateKeyStatus.Unavailable;
      }
      else
      {
        PrivateKeyStatus =
          original.PrivateKeyStatus == PrivateKeyStatus.Decrypted ?
          PrivateKeyStatus.Encrypted :
          original.PrivateKeyStatus;
        this.privateKeyData = original.privateKeyData;
        this.privateKeySalt = original.privateKeySalt;
      }

      SelfSignature = original.SelfSignature;

      this.signatures = new List<Signature>();
      original.signatures.ForEach(signature => this.signatures.Add(signature.Clone));

      this.attributes = new List<CertificateAttribute>();
      original.attributes.ForEach(attribute => this.attributes.Add(attribute.Clone));
    }

    /// <summary>
    /// Gets the RSA provider for the saved public and possibly private key.
    /// </summary>
    /// <param name="usingPrivateKey">Include the private key?</param>
    /// <returns>An RSA provider.</returns>
    private RSACryptoServiceProvider GetRsaProvider(bool usingPrivateKey)
    {
      var rsaProvider = new RSACryptoServiceProvider();

      if (usingPrivateKey)
      {
        rsaProvider.ImportCspBlob(PrivateKey);
      }
      else
      {
        rsaProvider.ImportCspBlob(PublicKey);
      }

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

      var rsaProvider = GetRsaProvider(true);
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

      var rsaProvider = GetRsaProvider(false);
      return rsaProvider.VerifyData(data, "SHA256", signature) &&
        Validate(certificateStorage) == CertificateValidationResult.Valid;
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

      var rsaProvider = GetRsaProvider(false);
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

      var rsaProvider = GetRsaProvider(false);
      return rsaProvider.VerifyData(data, "SHA256", signature) &&
        Validate(certificateStorage, date) == CertificateValidationResult.Valid;
    }

    /// <summary>
    /// Is the certificate valid?
    /// </summary>
    /// <param name="certificateStorage">Storage of certificates.</param>
    /// <returns>Is certificate valid.</returns>
    public CertificateValidationResult Validate(ICertificateStorage certificateStorage)
    {
      return Validate(certificateStorage, DateTime.Now);
    }

    /// <summary>
    /// Is the certificate valid?
    /// </summary>
    /// <param name="certificateStorage">Storage of certificates.</param>
    /// <param name="date">Date on which it must be valid.</param>
    /// <returns>Is certificate valid.</returns>
    public CertificateValidationResult Validate(ICertificateStorage certificateStorage, DateTime date)
    {
      if (SelfSignatureValid)
      {
        if (certificateStorage.IsRootCertificate(this))
        {
          return CertificateValidationResult.Valid;
        }
        else
        {
          CertificateValidationResult result = CertificateValidationResult.NoSignature;

          foreach (Signature signature in this.signatures)
          {
            result = signature.Verify(GetSignatureContent(), certificateStorage, date);

            if (result == CertificateValidationResult.Valid)
            {
              return certificateStorage.IsRevoked(signature.SignerId, Id, date);
            }
          }

          return result;
        }
      }
      else
      {
        return CertificateValidationResult.SelfsignatureInvalid;
      }
    }

    /// <summary>
    /// Determines until when the certificate will stay valid, provided it isn't revoked until then.
    /// The date may lay in the past if the certificate is not valid now.
    /// </summary>
    /// <param name="certificateStorage">Storage of certificates.</param>
    /// <param name="date">Date on which signers certificates must be valid.</param>
    /// <returns>Date the certificate will expire after.</returns>
    public DateTime ExpectedValidUntil(ICertificateStorage certificateStorage, DateTime date)
    {
      if (SelfSignatureValid)
      {
        if (certificateStorage.IsRootCertificate(this))
        {
          return DateTime.MaxValue;
        }
        else
        {
          return this.signatures.Max(signature => signature.ExpectedValidUntil(GetSignatureContent(), certificateStorage, date));
        }
      }
      else
      {
        return DateTime.MinValue;
      }
    }

    /// <summary>
    /// Determines until when the certificate will become valid, provided it isn't revoked until then.
    /// </summary>
    /// <param name="certificateStorage">Storage of certificates.</param>
    /// <returns>Date the certificate will become valid.</returns>
    public DateTime ExpectedValidFrom(ICertificateStorage certificateStorage)
    {
      if (SelfSignatureValid)
      {
        if (certificateStorage.IsRootCertificate(this))
        {
          return DateTime.MinValue;
        }
        else
        {
          return this.signatures.Min(signature => signature.ExpectedValidFrom(GetSignatureContent(), certificateStorage));
        }
      }
      else
      {
        return DateTime.MinValue;
      }
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

      var rsaProvider = GetRsaProvider(false);

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
        throw new InvalidProgramException("No private key.");

      var rsaProvider = GetRsaProvider(true);

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
    /// Creates a trapdoor.
    /// </summary>
    /// <param name="data">Encrypted data.</param>
    /// <returns>Created trapdoor.</returns>
    public TrapDoor CreateTrapDoor(byte[] data)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      if (!HasPrivateKey)
        throw new InvalidProgramException("No private key.");

      var rsaProvider = GetRsaProvider(true);

      byte[] encryptedKey = data.Part(0, 128);

      return new TrapDoor(Id, rsaProvider.Decrypt(encryptedKey, true));
    }

    /// <summary>
    /// Decrypts data using a trapdoor.
    /// </summary>
    /// <param name="trapDoor">Trapdoor to the data.</param>
    /// <param name="data">Encrypted data.</param>
    /// <returns>Decrypted data.</returns>
    public byte[] DeryptWithTrapDoor(TrapDoor trapDoor, byte[] data)
    {
      if (data == null)
        throw new ArgumentNullException("data");

      byte[] encryptedKey = data.Part(0, 128);
      byte[] cipherText = data.Part(144);

      RijndaelManaged rijndael = new RijndaelManaged();
      rijndael.BlockSize = 128;
      rijndael.KeySize = 256;
      rijndael.IV = data.Part(128, 16);
      rijndael.Key = trapDoor.SymmetricKey;
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
      get
      {
        return PrivateKeyStatus == Crypto.PrivateKeyStatus.Decrypted ||
               PrivateKeyStatus == Crypto.PrivateKeyStatus.Unencrypted;
      }
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
      context.Write(PublicKey);
      context.Write(SelfSignature);
      context.WriteList(this.attributes);
      context.WriteList(this.signatures);

      switch (PrivateKeyStatus)
      {
        case PrivateKeyStatus.Unavailable:
          context.Write((int)PrivateKeyStatus.Unavailable);
          break;
        case PrivateKeyStatus.Unencrypted:
          context.Write((int)PrivateKeyStatus.Unencrypted);
          context.Write(this.privateKeyData);
          break;
        case PrivateKeyStatus.Encrypted:
        case PrivateKeyStatus.Decrypted:
          context.Write((int)PrivateKeyStatus.Encrypted);
          context.Write(this.privateKeyData);
          context.Write(this.privateKeySalt);
          context.Write(this.passphraseSalt);
          break;
        default:
          throw new InvalidOperationException("Unknown private key status.");
      }
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
      PublicKey = context.ReadBytes();
      SelfSignature = context.ReadBytes();
      this.attributes = context.ReadObjectList<CertificateAttribute>();
      this.signatures = context.ReadObjectList<Signature>();

      PrivateKeyStatus = (PrivateKeyStatus)context.ReadInt32();

      switch (PrivateKeyStatus)
      {
        case PrivateKeyStatus.Unavailable:
          break;
        case Crypto.PrivateKeyStatus.Unencrypted:
          this.privateKeyData = context.ReadBytes();
          break;
        case Crypto.PrivateKeyStatus.Encrypted:
          this.privateKeyData = context.ReadBytes();
          this.privateKeySalt = context.ReadBytes();
          this.passphraseSalt = context.ReadBytes();
          break;
        default:
          throw new InvalidOperationException("Unknown or bad private key status.");
      }
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
      writer.Write(PublicKey);

      this.attributes.ForEach(attribute => writer.Write(attribute.ToBinary()));
    }

    /// <summary>
    /// Assembles the data to be signed for the certificate.
    /// </summary>
    /// <returns>Data to be signed.</returns>
    public byte[] GetSignatureContent()
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
    /// <param name="validUntil">This signature is valid until then.</param>
    /// <returns>New signature.</returns>
    public Signature AddSignature(Certificate signer, DateTime validUntil)
    {
      return AddSignature(signer, DateTime.Now, validUntil);
    }

    /// <summary>
    /// Add a signature to the certificate.
    /// </summary>
    /// <param name="signer">Certificate used to sign this one.</param>
    /// <param name="validFrom">This signature is not valid before then.</param>
    /// <param name="validUntil">This signature is valid until then.</param>
    /// <returns>New signature.</returns>
    public Signature AddSignature(Certificate signer, DateTime validFrom, DateTime validUntil)
    {
      if (signer == null)
        throw new ArgumentNullException("signer");

      Signature signature = new Signature(signer, GetSignatureContent(), validFrom, validUntil);
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
        var rsaProvider = GetRsaProvider(false);
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
    [SerializeField(0, "The magic certificate type.")]
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

    /// <summary>
    /// Has the certificate this attribute?
    /// </summary>
    /// <param name="name">Name of the attribute.</param>
    /// <returns>Is it there?</returns>
    protected bool HasAttribute(CertificateAttributeName name)
    {
      return this.attributes.Where(attribute => attribute.Name == name).Count() > 0;
    }

    /// <summary>
    /// Get attribute if available.
    /// </summary>
    /// <param name="name">Name of the attribute.</param>
    /// <returns>Attribute.</returns>
    protected CertificateAttribute GetAttribute(CertificateAttributeName name)
    {
      return this.attributes.Where(attribute => attribute.Name == name).FirstOrDefault();
    }

    /// <summary>
    /// Get a string attribute value.
    /// </summary>
    /// <param name="name">Name of the attribute.</param>
    /// <returns>Value of the attribute or null.</returns>
    protected string GetAttributeValueString(CertificateAttributeName name)
    {
      CertificateAttribute attribute = GetAttribute(name);

      if (attribute is StringCertificateAttribute)
      {
        return ((StringCertificateAttribute)attribute).Value;
      }
      else
      {
        return null;
      }
    }

    /// <summary>
    /// Get a string attribute value.
    /// </summary>
    /// <param name="name">Name of the attribute.</param>
    /// <returns>Value of the attribute or null.</returns>
    protected int? GetAttributeValueInt32(CertificateAttributeName name)
    {
      CertificateAttribute attribute = GetAttribute(name);

      if (attribute is Int32CertificateAttribute)
      {
        return ((Int32CertificateAttribute)attribute).Value;
      }
      else
      {
        return null;
      }
    }

    /// <summary>
    /// Get a string attribute value.
    /// </summary>
    /// <param name="name">Name of the attribute.</param>
    /// <returns>Value of the attribute or null.</returns>
    protected bool? GetAttributeValueBoolean(CertificateAttributeName name)
    {
      CertificateAttribute attribute = GetAttribute(name);

      if (attribute is BooleanCertificateAttribute)
      {
        return ((BooleanCertificateAttribute)attribute).Value;
      }
      else
      {
        return null;
      }
    }

    /// <summary>
    /// Add an attribute to the certificate;
    /// </summary>
    /// <param name="attribute"></param>
    protected void AddAttribute(CertificateAttribute attribute)
    {
      if (SelfSignature == null)
      {
        this.attributes.Add(attribute);
      }
      else
      {
        throw new InvalidOperationException("Cannot add attribute after self signature.");
      }
    }

    /// <summary>
    /// Language preferred by the certificate holder.
    /// </summary>
    public Language Language
    {
      get
      {
        int? value = GetAttributeValueInt32(CertificateAttributeName.Language);

        if (value == null)
        {
          return Language.English;
        }
        else
        {
          return (Language)value;
        }
      }
    }

    /// <summary>
    /// Decrypts the private key.
    /// </summary>
    /// <param name="oldPassphrase">Passphrase protecting the private key.</param>
    public void Unlock(string passphrase)
    {
      if (passphrase.IsNullOrEmpty())
        throw new ArgumentException("Passphrase must not be null or empty.");

      if (PrivateKeyStatus == PrivateKeyStatus.Encrypted)
      {
        Rfc2898DeriveBytes derive = new Rfc2898DeriveBytes(passphrase, this.passphraseSalt, 1024);
        byte[] key = derive.GetBytes(32);

        try
        {
          byte[] data = Aes.Decrypt(this.privateKeyData, key, this.privateKeySalt);
          SHA256Managed sha256 = new SHA256Managed();
          int hashLength = sha256.HashSize / 8;

          if (data.Length > hashLength &&
            sha256.ComputeHash(data.Part(0, data.Length - hashLength)).Equal(data.Part(data.Length - hashLength, hashLength)))
          {
            this.privateKeyDecrypted = data.Part(0, data.Length - hashLength);
            PrivateKeyStatus = PrivateKeyStatus.Decrypted;
          }
          else
          {
            throw new CryptographicException("Passphrase wrong.");
          }
        }
        catch (CryptographicException)
        {
          throw new CryptographicException("Passphrase wrong.");
        }
      }
      else if (PrivateKeyStatus == PrivateKeyStatus.Decrypted)
      {
        //Do nothing at all.
      }
      else
      {
        throw new InvalidOperationException("Private key is not encrypted.");
      }
    }

    /// <summary>
    /// Decrypts the private key permanently.
    /// </summary>
    /// <param name="oldPassphrase">Passphrase protecting the private key.</param>
    public void DecryptPrivateKey(string passphrase)
    {
      if (passphrase.IsNullOrEmpty())
        throw new ArgumentException("Passphrase must not be null or empty.");

      if (PrivateKeyStatus == PrivateKeyStatus.Encrypted)
      {
        Rfc2898DeriveBytes derive = new Rfc2898DeriveBytes(passphrase, this.passphraseSalt, 1024);
        byte[] key = derive.GetBytes(32);

        try
        {
          byte[] data = Aes.Decrypt(this.privateKeyData, key, this.privateKeySalt);
          SHA256Managed sha256 = new SHA256Managed();
          int hashLength = sha256.HashSize / 8;

          if (data.Length > hashLength &&
            sha256.ComputeHash(data.Part(0, data.Length - hashLength)).Equal(data.Part(data.Length - hashLength, hashLength)))
          {
            this.privateKeyData = data.Part(0, data.Length - hashLength);
            PrivateKeyStatus = PrivateKeyStatus.Unencrypted;
          }
          else
          {
            throw new CryptographicException("Passphrase wrong.");
          }
        }
        catch (CryptographicException)
        {
          throw new CryptographicException("Passphrase wrong.");
        }
      }
      else if (PrivateKeyStatus == PrivateKeyStatus.Decrypted)
      {
        //Do nothing at all.
      }
      else
      {
        throw new InvalidOperationException("Private key is not encrypted.");
      }
    }

    /// <summary>
    /// Encrypt the private key with a passphrase.
    /// </summary>
    /// <param name="passphrase">Passphrase for encryption.</param>
    public void EncryptPrivateKey(string passphrase)
    {
      if (passphrase.IsNullOrEmpty())
        throw new ArgumentException("Passphrase must not be null or empty.");

      if (PrivateKeyStatus != PrivateKeyStatus.Unencrypted)
        throw new InvalidOperationException("Already encrypted.");

      var rsaProvider = GetRsaProvider(true);
      EncryptPrivateKey(passphrase, rsaProvider);
    }

    /// <summary>
    /// Changes the passphrase.
    /// </summary>
    /// <param name="oldPassphrase">Passphrase currently protecting the private key.</param>
    /// <param name="newPassphrase">New passphrase to be set.</param>
    public void ChangePassphrase(string oldPassphrase, string newPassphrase)
    {
      if (oldPassphrase.IsNullOrEmpty())
        throw new ArgumentException("Passphrase must not be null or empty.");

      if (PrivateKeyStatus == PrivateKeyStatus.Decrypted)
      {
        Lock();
      }

      if (PrivateKeyStatus == PrivateKeyStatus.Encrypted)
      {
        Rfc2898DeriveBytes oldDerive = new Rfc2898DeriveBytes(oldPassphrase, this.passphraseSalt, 1024);
        byte[] oldKey = oldDerive.GetBytes(32);

        try
        {
          byte[] data = Aes.Decrypt(this.privateKeyData, oldKey, this.privateKeySalt);
          SHA256Managed sha256 = new SHA256Managed();
          int hashLength = sha256.HashSize / 8;

          if (data.Length > hashLength &&
            sha256.ComputeHash(data.Part(0, data.Length - hashLength)).Equal(data.Part(data.Length - hashLength, hashLength)))
          {
            this.privateKeyDecrypted = data.Part(0, data.Length - hashLength);
            PrivateKeyStatus = PrivateKeyStatus.Decrypted;

            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            this.passphraseSalt = new byte[32];
            rng.GetBytes(this.passphraseSalt);

            Rfc2898DeriveBytes newDerive = new Rfc2898DeriveBytes(newPassphrase, this.passphraseSalt, 1024);
            byte[] newKey = newDerive.GetBytes(32);

            this.privateKeySalt = new byte[16];
            rng.GetBytes(this.privateKeySalt);

            this.privateKeyData = Aes.Encrypt(data, newKey, this.privateKeySalt);
            PrivateKeyStatus = PrivateKeyStatus.Encrypted;
          }
          else
          {
            throw new CryptographicException("Passphrase wrong.");
          }
        }
        catch (CryptographicException)
        {
          throw new CryptographicException("Passphrase wrong.");
        }
      }
      else
      {
        throw new InvalidOperationException("Private key is not encrypted.");
      }
    }

    /// <summary>
    /// Remove the decrypted private key.
    /// </summary>
    /// <remarks>
    /// Altought is zeros out the key, this might be unreliable due to managed memory.
    /// </remarks>
    public void Lock()
    {
      if (this.privateKeyDecrypted != null)
      {
        this.privateKeyDecrypted.Clear();
        this.privateKeyDecrypted = null;
      }

      if (PrivateKeyStatus == PrivateKeyStatus.Decrypted)
      {
        PrivateKeyStatus = PrivateKeyStatus.Encrypted;
      }
    }
  }
}
