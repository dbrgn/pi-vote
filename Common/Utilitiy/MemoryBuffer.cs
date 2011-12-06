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

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// Buffers binary data in memory.
  /// </summary>
  public class MemoryBuffer
  {
    /// <summary>
    /// Buffer itself.
    /// </summary>
    private byte[] buffer;

    /// <summary>
    /// Start of the data in the buffer.
    /// </summary>
    private int start;

    /// <summary>
    /// Length of the data in the buffer.
    /// </summary>
    private int length;

    /// <summary>
    /// Creates a new memory buffer.
    /// </summary>
    /// <param name="defaultLength">Length of the buffer in the beginning.</param>
    public MemoryBuffer(int defaultLength)
    {
      this.buffer = new byte[defaultLength];
      this.start = 0;
      this.length = 0;
    }

    /// <summary>
    /// Length of data in the buffer.
    /// </summary>
    public int Length
    {
      get { return this.length; }
    }

    /// <summary>
    /// Writes a integer to the buffer.
    /// </summary>
    /// <param name="value">Integer value.</param>
    public void Write(int value)
    {
      MemoryStream stream = new MemoryStream();
      BinaryWriter writer = new BinaryWriter(stream);
      writer.Write(value);
      writer.Close();
      stream.Close();

      Write(stream.ToArray());
    }

    /// <summary>
    /// Write binary data to buffer.
    /// </summary>
    /// <param name="data">Data to write.</param>
    public void Write(byte[] data)
    {
      if (this.length + data.Length > this.buffer.Length)
      {
        this.buffer = this.buffer.Expand(this.length + data.Length);
      }

      if (this.start + this.length + data.Length > this.buffer.Length)
      {
        byte[] newBuffer = new byte[this.buffer.Length];
        Buffer.BlockCopy(this.buffer, this.start, newBuffer, 0, this.length);
        this.buffer = newBuffer;
        this.start = 0;
      }

      Buffer.BlockCopy(data, 0, this.buffer, this.start + this.length, data.Length);
      this.length += data.Length;
    }

    /// <summary>
    /// Read bytes from the buffer.
    /// </summary>
    /// <param name="count">Count of bytes to read.</param>
    /// <returns>Read bytes.</returns>
    public byte[] ReadBytes(int count)
    {
      byte[] data = this.buffer.Part(this.start, count);
      this.start += count;
      this.length -= count;
      return data;
    }

    /// <summary>
    /// Read an integer from the buffer.
    /// </summary>
    /// <returns>Read integer value.</returns>
    public int ReadInt32()
    {
      MemoryStream bufferStream = new MemoryStream(ReadBytes(sizeof(int)));
      BinaryReader reader = new BinaryReader(bufferStream);
      return reader.ReadInt32();
    }
  }
}
