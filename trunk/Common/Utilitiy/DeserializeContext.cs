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
using Emil.GMP;

namespace Pirate.PiVote.Serialization
{
  public class DeserializeContext
  {
    private static Dictionary<string, Type> Types = new Dictionary<string, Type>();

    private BinaryReader reader;

    public DeserializeContext(Stream stream)
    {
      this.reader = new BinaryReader(stream);
    }

    public int ReadInt32()
    {
      return this.reader.ReadInt32();
    }

    public uint ReadUInt32()
    {
      return this.reader.ReadUInt32();
    }

    public long ReadInt64()
    {
      return this.reader.ReadInt64();
    }

    public ulong ReadUInt64()
    {
      return this.reader.ReadUInt64();
    }

    public float ReadSingle()
    {
      return this.reader.ReadSingle();
    }

    public double ReadDouble()
    {
      return this.reader.ReadDouble();
    }

    public bool ReadBoolean()
    {
      return this.reader.ReadBoolean();
    }

    public string ReadString()
    {
      return this.reader.ReadString();
    }

    public byte[] ReadBytes()
    {
      int count = ReadInt32();
      return this.reader.ReadBytes(count);
    }

    public DateTime ReadDateTime()
    {
      return new DateTime(ReadInt64());
    }

    public BigInt ReadBigInt()
    {
      return new BigInt(ReadBytes());
    }

    public TValue ReadObject<TValue>()
      where TValue : Serializable
    {
      string typeName = ReadString();
      Type type = null;

      if (Types.ContainsKey(typeName))
      {
        type = Types[typeName];
      }
      else
      {
        type = Type.GetType(typeName);
        Types.Add(typeName, type);
      }

      return (TValue)Activator.CreateInstance(type, new object[] { this }, new object[] { });
    }

    public List<TValue> ReadObjectList<TValue>()
      where TValue : Serializable
    {
      int count = ReadInt32();
      List<TValue> list = new List<TValue>();

      for (int index = 0; index < count; index++)
      {
        list.Add(ReadObject<TValue>());
      }

      return list;
    }

    public List<byte[]> ReadBytesList()
    {
      int count = ReadInt32();
      List<byte[]> list = new List<byte[]>();

      for (int index = 0; index < count; index++)
      {
        list.Add(ReadBytes());
      }

      return list;
    }

    public void Close()
    {
      this.reader.Close();
    }
  }
}
