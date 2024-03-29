﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
  public static class Bytes
  {
    public static byte[] FromHexString(this string text)
    {
      if (text == null)
      {
        throw new ArgumentNullException("text");
      }
      else if (text.Length % 2 != 0)
      {
        throw new ArgumentException("Text length is uneven.");
      }

      var data = new byte[text.Length / 2];

      for (var index = 0; index < text.Length; index += 2)
      {
        var byteText = text.Substring(index, 2);
        var byteValue = byte.Parse(byteText, Globalization.NumberStyles.HexNumber);
        data[index / 2] = byteValue;
      }

      return data;
    }

    public static string ToHexString(this byte[] data)
    {
      return string.Join(string.Empty, data.Select(b => string.Format("{0:x2}", b)).ToArray());
    }

    public static byte[] Concat(this byte[] part0, params byte[][] parts)
    {
      if (part0 == null)
        throw new ArgumentNullException("part0");
      if (parts == null)
        throw new ArgumentNullException("parts");

      byte[] data = part0;

      foreach (byte[] part in parts)
      {
        data = data.Concat(part);
      }

      return data;
    }

    public static byte[] Concat(this byte[] data1, byte[] data2)
    {
      if (data1 == null)
        throw new ArgumentNullException("data1");
      if (data2 == null)
        throw new ArgumentNullException("data2");

      byte[] concat = new byte[data1.Length + data2.Length];
      System.Buffer.BlockCopy(data1, 0, concat, 0, data1.Length);
      System.Buffer.BlockCopy(data2, 0, concat, data1.Length, data2.Length);
      return concat;
    }

    public static void Clear(this byte[] data)
    {
      if (data == null)
        throw new ArgumentNullException("data");

      for (int index = 0; index < data.Length; index++)
      {
        data[index] = 0;
      }
    }

    public static byte[] Part(this byte[] data, int index, int length)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      if (index < 0 || index + length > data.Length)
        throw new ArgumentException("Index or length out of range.");

      byte[] part = new byte[length];
      System.Buffer.BlockCopy(data, index, part, 0, length);
      return part;
    }

    public static byte[] Part(this byte[] data, int index)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      if (index < 0 || index > data.Length)
        throw new ArgumentException("Index or length out of range.");

      return data.Part(index, data.Length - index);
    }

    public static bool Equal(this byte[] data1, byte[] data2)
    {
      if (data1 == null)
        throw new ArgumentNullException("data1");
      if (data2 == null)
        throw new ArgumentNullException("data2");

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
      if (data1 == null)
        throw new ArgumentNullException("data1");
      if (data2 == null)
        throw new ArgumentNullException("data2");

      return Equal(Part(data1, 0, data2.Length), data2);
    }

    public static byte[] Xor(this byte[] data1, byte[] data2)
    {
      if (data1 == null)
        throw new ArgumentNullException("data1");
      if (data2 == null)
        throw new ArgumentNullException("data2");

      byte[] output = new byte[data1.Length];

      for (int i = 0; i < data1.Length; i++)
      {
        output[i] = (byte)((int)data1[i] ^ (int)data2[i]);
      }

      return output;
    }

    public static byte[] Expand(this byte[] data, int length)
    {
      if (data == null)
        throw new ArgumentNullException("data");
      if (length < data.Length)
        throw new ArgumentException("Length must be greater or equal to the length of data.");

      byte[] newData = new byte[length];
      Buffer.BlockCopy(data, 0, newData, 0, data.Length);
      return newData;
    }

    public static void Display(this byte[] data, string name)
    {
      string firstBytes = string.Empty;

      for (int index = 0; index < Math.Min(data.Length, 8); index++)
      {
        firstBytes += string.Format("{0:x2} ", data[index]);
      }

      string lastBytes = string.Empty;

      for (int index = Math.Max(8, data.Length - 8); index < data.Length; index++)
      {
        lastBytes += string.Format("{0:x2} ", data[index]);
      }

      string text = string.Format("{0}: {1}... {2}length {3}", name, firstBytes, lastBytes, data.Length);

      Console.WriteLine(text);
    }
  }
}
