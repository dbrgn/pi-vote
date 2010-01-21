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
  public class Certificate : Serializable
  {
    public Guid Id { get; private set; }

    public string FullName { get; private set; }

    private byte[] Key { get; set; }

    public Certificate(string fullName)
    {
      Id = Guid.NewGuid();
      FullName = fullName;

      RSACryptoServiceProvider rsaProvider = new RSACryptoServiceProvider();
      rsaProvider.KeySize = 2048;
      Key = rsaProvider.ExportCspBlob(true);
    }

    public Certificate(DeserializeContext context)
      : base(context)
    { }

    public Certificate(Certificate original, bool onlyPublicPart)
    {
      Id = original.Id;
      FullName = original.FullName;

      if (onlyPublicPart)
      {
        Key = original.GetRsaProvider().ExportCspBlob(false);
      }
      else
      {
        Key = Key;
      }
    }

    private RSACryptoServiceProvider GetRsaProvider()
    {
      var rsaProvider = new RSACryptoServiceProvider();
      rsaProvider.ImportCspBlob(Key);
      return rsaProvider;
    }

    public byte[] Sign(byte[] data)
    {
      var rsaProvider = GetRsaProvider();
      return rsaProvider.SignData(data, "SHA256");
    }

    public bool Verify(byte[] data, byte[] signature)
    {
      var rsaProvider = GetRsaProvider();
      return rsaProvider.VerifyData(data, "SHA256", signature);
    }

    public bool IsIdentic(Certificate other)
    {
      return Id == other.Id;
    }

    public byte[] Encrypt(byte[] data)
    {
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

    public byte[] Decrypt(byte[] data)
    {
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

    public bool HasPrivateKey
    {
      get { return !GetRsaProvider().PublicOnly; }
    }

    public Certificate OnlyPublicPart
    {
      get { return new Certificate(this, true); }
    }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Id.ToByteArray());
      context.Write(FullName);
      context.Write(Key);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Id = new Guid(context.ReadBytes());
      FullName = context.ReadString();
      Key = context.ReadBytes();
    }
  }
}
