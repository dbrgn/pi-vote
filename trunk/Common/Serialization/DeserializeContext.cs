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
using System.Reflection;
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

    public Guid ReadGuid()
    {
      return new Guid(ReadBytes());
    }

    public BigInt ReadBigInt()
    {
      return new BigInt(ReadBytes());
    }

    public MultiLanguageString ReadMultiLanguageString()
    {
      return MultiLanguageString.Deserialize(this);
    }

    public TValue ReadObject<TValue>()
      where TValue : Serializable
    {
      string typeName = ReadString();
      Type type = null;

      if (typeName == string.Empty)
      {
        return null;
      }
      else
      {
        lock (Types)
        {
          if (Types.ContainsKey(typeName))
          {
            type = Types[typeName];
          }
          else
          {
            type = Type.GetType(typeName);

            if (type != null)
            {
              Types.Add(typeName, type);
            }
            else
            {
              type = Assembly.GetEntryAssembly().GetType(typeName, true);
              Types.Add(typeName, type);
            }
          }
        }

        return (TValue)Activator.CreateInstance(type, new object[] { this }, new object[] { });
      }
    }

    public List<TValue> ReadObjectList<TValue>()
      where TValue : Serializable
    {
      int count = ReadInt32();

      if (count < 0)
      {
        return null;
      }
      else
      {
        List<TValue> list = new List<TValue>();

        for (int index = 0; index < count; index++)
        {
          list.Add(ReadObject<TValue>());
        }

        return list;
      }
    }

    public Dictionary<int, TValue> ReadObjectDictionary<TValue>()
      where TValue : Serializable
    {
      int count = ReadInt32();

      if (count < 0)
      {
        return null;
      }
      else
      {
        Dictionary<int, TValue> list = new Dictionary<int, TValue>();

        for (int index = 0; index < count; index++)
        {
          list.Add(ReadInt32(), ReadObject<TValue>());
        }

        return list;
      }
    }

    public List<byte[]> ReadBytesList()
    {
      int count = ReadInt32();

      if (count < 0)
      {
        return null;
      }
      else
      {
        List<byte[]> list = new List<byte[]>();

        for (int index = 0; index < count; index++)
        {
          list.Add(ReadBytes());
        }

        return list;
      }
    }

    public List<Guid> ReadGuidList()
    {
      int count = ReadInt32();

      if (count < 0)
      {
        return null;
      }
      else
      {
        List<Guid> list = new List<Guid>();

        for (int index = 0; index < count; index++)
        {
          list.Add(ReadGuid());
        }

        return list;
      }
    }

    public List<BigInt> ReadBigIntList()
    {
      int count = ReadInt32();

      if (count < 0)
      {
        return null;
      }
      else
      {
        List<BigInt> list = new List<BigInt>();

        for (int index = 0; index < count; index++)
        {
          list.Add(ReadBigInt());
        }

        return list;
      }
    }

    public void Close()
    {
      this.reader.Close();
    }
  }
}
