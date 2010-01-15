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

namespace System
{
  public static class Bytes
  {
    public static byte[] Concat(this byte[] part0, params byte[][] parts)
    {
      byte[] data = part0;

      foreach (byte[] part in parts)
      {
        data = data.Concat(part);
      }

      return data;
    }

    public static byte[] Concat(this byte[] data1, byte[] data2)
    {
      byte[] concat = new byte[data1.Length + data2.Length];
      System.Buffer.BlockCopy(data1, 0, concat, 0, data1.Length);
      System.Buffer.BlockCopy(data2, 0, concat, data1.Length, data2.Length);
      return concat;
    }

    public static void Clear(this byte[] data)
    {
      for (int index = 0; index < data.Length; index++)
      {
        data[index] = 0;
      }
    }

    public static byte[] Part(this byte[] data, int index, int length)
    {
      byte[] part = new byte[length];
      System.Buffer.BlockCopy(data, index, part, 0, length);
      return part;
    }

    public static byte[] Part(this byte[] data, int index)
    {
      return data.Part(index, data.Length - index);
    }

    public static bool Equal(this byte[] data1, byte[] data2)
    {
      if (data1.Length == data2.Length)
      {
        bool equal = true;

        for (int index = 0; index < data1.Length; index++)
        {
          equal &= data1[index] == data2[index];
        }

        return equal;
      }
      else
      {
        return false;
      }
    }

    public static bool StartWith(this byte[] data1, byte[] data2)
    {
      return Equal(Part(data1, 0, data2.Length), data2);
    }

    public static byte[] Xor(this byte[] data1, byte[] data2)
    {
      byte[] output = new byte[data1.Length];

      for (int i = 0; i < data1.Length; i++)
      {
        output[i] = (byte)((int)data1[i] ^ (int)data2[i]);
      }

      return output;
    }
  }
}
