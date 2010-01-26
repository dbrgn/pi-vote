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

    public void Write(BigInt value)
    {
      Write(value.ToByteArray());
    }

    public void Write<TValue>(TValue value)
      where TValue : Serializable
    {
      value.Serialize(this);
    }

    public void WriteList<TValue>(IEnumerable<TValue> list)
      where TValue : Serializable
    {
      Write(list.Count());

      foreach (TValue obj in list)
      {
        Write(obj);
      }
    }

    public void WriteList(IEnumerable<byte[]> list)
    {
      Write(list.Count());

      foreach (byte[] data in list)
      {
        Write(data);
      }
    }

    public void Close()
    {
      this.writer.Close();
    }
  }
}
