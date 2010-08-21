
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
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Rpc
{
  public class EchoRequest : RpcRequest<EchoServer, EchoResponse>
  {
    private string message;

    public EchoRequest(
      Guid requestId, string message)
      : base(requestId)
    {
      this.message = message;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public EchoRequest(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(this.message);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      this.message = context.ReadString();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <param name="signer">Signer of the RPC request.</param>
    /// <returns>Response to the request.</returns>
    protected override EchoResponse Execute(EchoServer server)
    {
      return new EchoResponse(RequestId, server.Echo(this.message));
    }
  }
}
