
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
    /// TCP client.
    /// </summary>
    private TcpClient client;

    /// <summary>
    /// TCP client stream.
    /// </summary>
    private NetworkStream stream;

    /// <summary>
    /// TCP stream writer.
    /// </summary>
    private BinaryWriter writer;

    /// <summary>
    /// TCP stream reader.
    /// </summary>
    private BinaryReader reader;

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
    /// <param name="serverEndPoint">Server IP address and port.</param>
    public void Connect(IPEndPoint serverEndPoint)
    {
      this.client.Connect(serverEndPoint);
      this.stream = this.client.GetStream();
      this.writer = new BinaryWriter(this.stream);
      this.reader = new BinaryReader(this.stream);
    }

    /// <summary>
    /// Connects the client to the server.
    /// </summary>
    /// <param name="serverEndPoint">Server IP address and port.</param>
    /// <param name="serverEndPoint">Proxy IP address and port.</param>
    public void Connect(IPEndPoint serverEndPoint, IPEndPoint proxyEndPoint)
    {
      this.client.Connect(proxyEndPoint);
      this.stream = this.client.GetStream();
      this.writer = new BinaryWriter(this.stream);
      this.reader = new BinaryReader(this.stream);

      this.writer.Write(string.Format("CONNECT {0}:{1} HTTP/1.0\r\n\r\n", serverEndPoint.Address.ToString(), serverEndPoint.Port.ToString()));
      this.writer.Flush();
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
        this.reader = null;
        this.writer = null;
      }
    }

    /// <summary>
    /// Executes a request on the TCP RPC server.
    /// </summary>
    /// <param name="requestData">Binary data of the RPC request.</param>
    /// <returns>Binary data of the RPC response.</returns>
    public byte[] Execute(byte[] requestData)
    {
      this.writer.Write(requestData.Length);
      this.writer.Write(requestData, 0, requestData.Length);
      this.writer.Flush();

      int packetLength = this.reader.ReadInt32();
      byte[] responseData = this.reader.ReadBytes(packetLength);

      return responseData;
    }
  }
}
