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
using Emil.GMP;

namespace Pirate.PiVote.Serialization
{
  public class SerializeContext
  {
    private BinaryWriter writer;

    public SerializeContext(Stream stream)
    {
      this.writer = new BinaryWriter(stream);
    }

    public void Write(int value)
    {
      this.writer.Write(value);
    }

    public void Write(uint value)
    {
      this.writer.Write(value);
    }

    public void Write(long value)
    {
      this.writer.Write(value);
    }

    public void Write(ulong value)
    {
      this.writer.Write(value);
    }

    public void Write(float value)
    {
      this.writer.Write(value);
    }

    public void Write(double value)
    {
      this.writer.Write(value);
    }

    public void Write(bool value)
    {
      this.writer.Write(value);
    }

    public void Write(string value)
    {
      this.writer.Write(value);
    }

    public void Write(byte[] value)
    {
      Write(value.Length);
      this.writer.Write(value);
    }

    public void Write(DateTime value)
    {
      Write(value.Ticks);
    }

    public void Write(Guid value)
    {
      Write(value.ToByteArray());
    }

    public void Write(BigInt value)
    {
      Write(value.ToByteArray());
    }

    public void Write(MultiLanguageString value)
    {
      value.Serialize(this);
    }

    public void Write<TValue>(TValue value)
      where TValue : Serializable
    {
      if (value == null)
      {
        Write(string.Empty);
      }
      else
      {
        value.Serialize(this);
      }
    }

    public void WriteDictionary<TValue>(IDictionary<int, TValue> list)
      where TValue : Serializable
    {
      if (list == null)
      {
        Write(-1);
      }
      else
      {
        Write(list.Count());

        foreach (KeyValuePair<int, TValue> pair in list)
        {
          Write(pair.Key);
          Write(pair.Value);
        }
      }
    }

    public void WriteList<TValue>(IEnumerable<TValue> list)
      where TValue : Serializable
    {
      if (list == null)
      {
        Write(-1);
      }
      else
      {
        Write(list.Count());

        foreach (TValue obj in list)
        {
          Write(obj);
        }
      }
    }

    public void WriteList(IEnumerable<byte[]> list)
    {
      if (list == null)
      {
        Write(-1);
      }
      else
      {
        Write(list.Count());

        foreach (byte[] data in list)
        {
          Write(data);
        }
      }
    }

    public void WriteList(IEnumerable<Guid> list)
    {
      if (list == null)
      {
        Write(-1);
      }
      else
      {
        Write(list.Count());

        foreach (Guid data in list)
        {
          Write(data);
        }
      }
    }

    public void WriteList(IEnumerable<BigInt> list)
    {
      if (list == null)
      {
        Write(-1);
      }
      else
      {
        Write(list.Count());

        foreach (BigInt data in list)
        {
          Write(data);
        }
      }
    }
    
    public void Close()
    {
      this.writer.Close();
    }
  }
}
