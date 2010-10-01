
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

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// RPC server.
  /// </summary>
  public class EchoServer : RpcServer
  {
    private ILogger logger;

    private IServerConfig serverConfig;

    public EchoServer()
    {
      this.logger = new DummyLogger();
      this.serverConfig = new DummyConfig();
    }

    public override ILogger Logger
    {
      get { return this.logger; }
    }

    public override IServerConfig ServerConfig
    {
      get { return this.serverConfig; }
    }

    public string Echo(string message)
    {
      return message;
    }

    public override void Idle()
    {
    }

    public override void Process()
    {
    }

    /// <summary>
    /// Excecutes a RPC request.
    /// </summary>
    /// <param name="requestData">Serialized request data.</param>
    /// <returns>Serialized response data</returns>
    public override byte[] Execute(byte[] requestData)
    {
      var request = Serializable.FromBinary<RpcRequest<EchoServer>>(requestData);
      var response = request.TryExecute(this);

      byte[] responseData = response.ToBinary();

      return responseData;
    }
  }
}
