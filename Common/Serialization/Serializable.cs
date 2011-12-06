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
using System.Text.RegularExpressions;
using Emil.GMP;

namespace Pirate.PiVote.Serialization
{
  [SerializeObject("Base object of all serializable objects.")]
  [SerializeAdditionalField(typeof(string), "FullName", 0, "Full name of the object type. The type name may be preceeded by @xx where xx is the hex representation of the version of this object.")]
  public abstract class Serializable
  {
    public Serializable()
    { 
    }

    public Serializable(DeserializeContext context, byte version)
    {
      Deserialize(context, version);
    }

    /// <summary>
    /// Version of the serialized data.
    /// </summary>
    /// <remarks>
    /// Version 0 means not using versionning.
    /// </remarks>
    public virtual byte Version
    {
      get { return 0; }
    }

    public virtual void Serialize(SerializeContext context)
    {
      string fullName = Regex.Replace(GetType().FullName, @"(\[[^,]*?,\s*[^,]*?)(,[^\]]*?)(\])", "$1$3");

      if (Version == 0)
      {
        context.Write(fullName);
      }
      else
      {
        string versionString = string.Format("@{0:x2}", Version);
        context.Write(versionString + fullName);
      }
    }

    protected virtual void Deserialize(DeserializeContext context, byte version)
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

    public static byte[] ToBinary(Serializable first, Serializable second, Serializable third)
    {
      MemoryStream memoyrStream = new MemoryStream();
      SerializeContext context = new SerializeContext(memoyrStream);

      first.Serialize(context);
      second.Serialize(context);
      third.Serialize(context);

      context.Close();
      memoyrStream.Close();

      return memoyrStream.ToArray();
    }

    public static byte[] ToBinary(Serializable first, Serializable second)
    {
      MemoryStream memoyrStream = new MemoryStream();
      SerializeContext context = new SerializeContext(memoyrStream);

      first.Serialize(context);
      second.Serialize(context);

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
      catch (Exception exception)
      {
        throw new PiFormatException(ExceptionCode.BadSerializableFormat, "Bad serializable format:" + exception.ToString());
      }
    }

    public static Tuple<TValue1, TValue2> FromBinary<TValue1, TValue2>(byte[] data)
      where TValue1 : Serializable
      where TValue2 : Serializable
    {
      try
      {
        MemoryStream memoryStream = new MemoryStream(data);
        DeserializeContext context = new DeserializeContext(memoryStream);

        TValue1 obj1 = context.ReadObject<TValue1>();
        TValue2 obj2 = context.ReadObject<TValue2>();

        context.Close();
        memoryStream.Close();

        return new Tuple<TValue1, TValue2>(obj1, obj2);
      }
      catch (Exception exception)
      {
        throw new PiFormatException(ExceptionCode.BadSerializableFormat, "Bad serializable format:" + exception.ToString());
      }
    }
    
    public static Tuple<TValue1, TValue2, TValue3> FromBinary<TValue1, TValue2, TValue3>(byte[] data)
      where TValue1 : Serializable
      where TValue2 : Serializable
      where TValue3 : Serializable
    {
      try
      {
        MemoryStream memoryStream = new MemoryStream(data);
        DeserializeContext context = new DeserializeContext(memoryStream);

        TValue1 obj1 = context.ReadObject<TValue1>();
        TValue2 obj2 = context.ReadObject<TValue2>();
        TValue3 obj3 = context.ReadObject<TValue3>();

        context.Close();
        memoryStream.Close();

        return new Tuple<TValue1, TValue2, TValue3>(obj1, obj2, obj3);
      }
      catch (Exception exception)
      {
        throw new PiFormatException(ExceptionCode.BadSerializableFormat, "Bad serializable format:" + exception.ToString());
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
