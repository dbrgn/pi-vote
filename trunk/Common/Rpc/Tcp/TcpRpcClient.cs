
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
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// TCP based RPC server.
  /// </summary>
  public class TcpRpcClient : IBinaryRpcProxy
  {
    /// <summary>
    /// Server port.
    /// </summary>
    private const int Port = 4242;

    /// <summary>
    /// TCP client.
    /// </summary>
    private TcpClient client;

    /// <summary>
    /// TCP client stream.
    /// </summary>
    private NetworkStream stream;

    /// <summary>
    /// Creates a new TCP RPC client.
    /// </summary>
    public TcpRpcClient()
    {
      this.client = new TcpClient();
    }

    /// <summary>
    /// Connects the client to the server.
    /// </summary>
    /// <param name="address">Server IP address.</param>
    public void Connect(IPAddress address)
    {
      this.client.Connect(new IPEndPoint(address, Port));
      this.stream = this.client.GetStream();
    }

    /// <summary>
    /// Is the client connected to the server?
    /// </summary>
    public bool Connected
    {
      get
      {
        return
          this.client != null &&
          this.client.Connected;
      }
    }

    /// <summary>
    /// Disconnect the client from the server.
    /// </summary>
    public void Disconnect()
    {
      if (this.client != null)
      {
        this.client.Close();
        this.client = null;
        this.stream = null;
      }
    }

    /// <summary>
    /// Executes a request on the TCP RPC server.
    /// </summary>
    /// <param name="requestData">Binary data of the RPC request.</param>
    /// <returns>Binary data of the RPC response.</returns>
    public byte[] Execute(byte[] requestData)
    {
      BinaryWriter writer = new BinaryWriter(this.stream);
      writer.Write(requestData.Length);
      writer.Write(requestData, 0, requestData.Length);

      BinaryReader reader = new BinaryReader(this.stream);
      int packetLength = reader.ReadInt32();
      byte[] responseData = reader.ReadBytes(packetLength);

      return responseData;
    }
  }
}
