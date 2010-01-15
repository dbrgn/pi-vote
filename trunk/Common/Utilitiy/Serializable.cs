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
      MemoryStream memoryStream = new MemoryStream(data);
      DeserializeContext context = new DeserializeContext(memoryStream);

      TValue obj = context.ReadObject<TValue>();

      context.Close();
      memoryStream.Close();

      return obj;
    }
  }
}
