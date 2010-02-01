
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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote
{
  public class PiException : Exception
  {
    public ExceptionCode Code { get; private set; }

    public PiException(ExceptionCode code, string message)
      : base(message)
    {
      Code = code;
    }

    public PiException(Exception exception)
      : base(exception.Message)
    {
      Code = ExceptionCode.Unknown;
    }

    public byte[] ToBinary()
    {
      MemoryStream stream = new MemoryStream();
      SerializeContext context = new SerializeContext(stream);

      context.Write(GetType().FullName);
      context.Write((int)Code);
      context.Write(Message);

      context.Close();
      stream.Close();

      return stream.ToArray();
    }

    public static PiException FromBinary(byte[] data)
    {
      MemoryStream stream = new MemoryStream(data);
      DeserializeContext context = new DeserializeContext(stream);

      string typeName = context.ReadString();
      ExceptionCode code = (ExceptionCode)context.ReadInt32();
      string message = context.ReadString();

      Type type = Type.GetType(typeName);
      PiException exception = (PiException)Activator.CreateInstance(type, new object[] { code, message });

      context.Close();
      stream.Close();

      return exception;
    }
  }
}
