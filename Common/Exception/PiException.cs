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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote
{
  /// <summary>
  /// Exception thrown by the PiVote RPC.
  /// </summary>
  public class PiException : Exception
  {
    /// <summary>
    /// Identifiing code of exception.
    /// </summary>
    public ExceptionCode Code { get; private set; }

    /// <summary>
    /// Message from the server.
    /// </summary>
    public string ServerMessage { get; private set; }

    /// <summary>
    /// Creates an exception from a code and message.
    /// </summary>
    /// <param name="code">Identifiing code of exception.</param>
    /// <param name="message">English debugging message.</param>
    public PiException(ExceptionCode code, string message)
      : base(message)
    {
      Code = code;
    }

    /// <summary>
    /// Creates an exception containing a standard exception.
    /// </summary>
    /// <param name="exception">Exception to be incorporated.</param>
    public PiException(Exception exception)
      : base(exception.Message)
    {
      Code = ExceptionCode.Unknown;
    }

    /// <summary>
    /// Serializes the exception to binary.
    /// </summary>
    /// <returns>Binary data of exception.</returns>
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

    /// <summary>
    /// Deserializes an exception from binary data.
    /// </summary>
    /// <param name="data">Binary data of exception.</param>
    /// <returns>An exception.</returns>
    public static PiException FromBinary(byte[] data)
    {
      MemoryStream stream = new MemoryStream(data);
      DeserializeContext context = new DeserializeContext(stream);

      string typeName = context.ReadString();
      ExceptionCode code = (ExceptionCode)context.ReadInt32();
      string message = context.ReadString();

      Type type = Type.GetType(typeName);
      PiException exception = (PiException)Activator.CreateInstance(type, new object[] { code, code.Text() });
      exception.ServerMessage = message;

      context.Close();
      stream.Close();

      return exception;
    }

    public string Text
    {
      get { return Code.Text(); }
    }
  }
}
