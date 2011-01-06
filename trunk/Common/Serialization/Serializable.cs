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
using System.Text.RegularExpressions;
using Emil.GMP;

namespace Pirate.PiVote.Serialization
{
  [SerializeObject("Base object of all serializable objects.")]
  [SerializeAdditionalField(typeof(string), "FullName", 0, "Full name of the object type.")]
  public class Serializable
  {
    public Serializable()
    { 
    }

    public Serializable(DeserializeContext context)
    {
      Deserialize(context);
    }

    public virtual void Serialize(SerializeContext context)
    {
      string fullName = Regex.Replace(GetType().FullName, @"(\[[^,]*?,\s*[^,]*?)(,[^\]]*?)(\])", "$1$3");
      context.Write(fullName);
    }

    protected virtual void Deserialize(DeserializeContext context)
    { 
    }

    public byte[] ToBinary()
    {
      MemoryStream memoyrStream = new MemoryStream();
      SerializeContext context = new SerializeContext(memoyrStream);

      Serialize(context);

      context.Close();
      memoyrStream.Close();

      return memoyrStream.ToArray();
    }

    public static TValue FromBinary<TValue>(byte[] data)
      where TValue : Serializable
    {
      try
      {
        MemoryStream memoryStream = new MemoryStream(data);
        DeserializeContext context = new DeserializeContext(memoryStream);

        TValue obj = context.ReadObject<TValue>();

        context.Close();
        memoryStream.Close();

        return obj;
      }
      catch
      {
        throw new PiFormatException(ExceptionCode.BadSerializableFormat, "Bad serializable format.");
      }
    }

    /// <summary>
    /// Save object to file.
    /// </summary>
    /// <param name="fileName">Filename for saving.</param>
    public void Save(string fileName)
    {
      if (fileName == null)
        throw new ArgumentNullException("fileName");

      File.WriteAllBytes(fileName, ToBinary());
    }

    /// <summary>
    /// Load object from file.
    /// </summary>
    /// <param name="fileName">Filename pointing to a certificate</param>
    /// <returns>Deserialized object.</returns>
    public static TSerializable Load<TSerializable>(string fileName)
      where TSerializable : Serializable
    {
      if (fileName == null)
        throw new ArgumentNullException("fileName");
      if (!File.Exists(fileName))
        throw new ArgumentException("Certificate file not found.");

      return Serializable.FromBinary<TSerializable>(File.ReadAllBytes(fileName));
    }
  }
}
