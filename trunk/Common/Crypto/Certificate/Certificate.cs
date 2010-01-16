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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  public class Certificate : Serializable
  {
    public Certificate()
    { }

    public Certificate(DeserializeContext context)
      : base(context)
    { }

    public byte[] Sign(byte[] data)
    { 
      return new byte[]{};
    }

    public bool Verify(byte[] data, byte[] signature)
    {
      return true;
    }

    public bool IsIdentic(Certificate other)
    {
      return true;
    }

    public byte[] Encrypt(byte[] data)
    { 
      return data;
    }

    public byte[] Decrypt(byte[] data)
    {
      return data;
    }

    public bool HasPrivateKey
    {
      get { return true; }
    }

    public Certificate OnlyPublicPart
    {
      get { return this; }
    }
  }
}
