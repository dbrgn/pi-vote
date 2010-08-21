
/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// TCP RPC connection on the server side.
  /// </summary>
  public class TcpRpcConnection
  {
    /// <summary>
    /// TCP client.
    /// </summary>
    private TcpClient client;

    /// <summary>
    /// TCP network stream.
    /// </summary>
    private NetworkStream stream;

    /// <summary>
    /// TCP network stream writer.
    /// </summary>
    private BinaryWriter writer;

    /// <summary>
    /// Length of the incoming request.
    /// </summary>
    private int messageLength;

    /// <summary>
    /// Voting RPC server.
    /// </summary>
    private RpcServer rpcServer;

    /// <summary>
    /// Time of last activity on this connection.
    /// </summary>
    private DateTime lastActivity;

    /// <summary>
    /// Time until idle client is disconnected in seconds.
    /// </summary>
    private double clientTimeOut;

    /// <summary>
    /// Buffer for incoming request data.
    /// </summary>
    private MemoryBuffer inBuffer;

    /// <summary>
    /// Buffer for outgoing response data.
    /// </summary>
    private MemoryBuffer outBuffer;

    /// <summary>
    /// Text identifiing the remote endpoint.
    /// </summary>
    public string RemoteEndPointText { get; private set; }

    /// <summary>
    /// Creates a new TCP RPC server connection.
    /// </summary>
    /// <param name="client">TCP client.</param>
    /// <param name="rpcServer">Voting RPC server.</param>
    /// <param name="clientTimeOut">Time until idle client is disconnected in seconds.</param>
    public TcpRpcConnection(TcpClient client, RpcServer rpcServer, double clientTimeOut)
    {
      this.clientTimeOut = clientTimeOut;
      this.lastActivity = DateTime.Now;
      this.client = client;
      this.stream = this.client.GetStream();
      this.writer = new BinaryWriter(this.stream);
      this.messageLength = 0;
      this.rpcServer = rpcServer;
      this.inBuffer = new MemoryBuffer(32768);
      this.outBuffer = new MemoryBuffer(32768);

      if (this.client.Client.RemoteEndPoint is IPEndPoint)
      {
        IPEndPoint ipEndPoint = (IPEndPoint)this.client.Client.RemoteEndPoint;
        RemoteEndPointText = ipEndPoint.Address.ToString() + ":" + ipEndPoint.Port.ToString();
      }
      else
      {
        RemoteEndPointText = "Unknown";
      }
    }

    /// <summary>
    /// Work the connection for incoming requests.
    /// </summary>
    /// <returns>
    /// Has some work actually be done?
    /// </returns>
    public bool Process()
    {
      bool doneSomeWork = false;

      //Read from network and put to inBuffer.
      if (this.client.Available > 0)
      {
        byte[] data = new byte[this.client.Available];
        this.stream.Read(data, 0, data.Length);
        this.inBuffer.Write(data);
        doneSomeWork = true;
      }

      //Read message length from inBuffer.
      if (this.messageLength == 0 && this.inBuffer.Length >= sizeof(int))
      {
        this.messageLength = this.inBuffer.ReadInt32();
        doneSomeWork = true;
      }

      //Read message from inBuffer, execute and write to outBuffer.
      if (this.messageLength > 0 && this.inBuffer.Length >= this.messageLength)
      {
        this.lastActivity = DateTime.Now;
        byte[] requestPacket = this.inBuffer.ReadBytes(this.messageLength);
        this.messageLength = 0;

        byte[] responseData = null;

        lock (this.rpcServer)
        {
          responseData = this.rpcServer.Execute(requestPacket);
        }

        this.outBuffer.Write(responseData.Length);
        this.outBuffer.Write(responseData);
        doneSomeWork = true;
      }

      //Write from outBuffer to network.
      if (this.outBuffer.Length > 0)
      {
        int sendLength = Math.Min(this.outBuffer.Length, this.client.SendBufferSize);
        byte[] data = this.outBuffer.ReadBytes(sendLength);
        this.writer.Write(data);
        doneSomeWork = true;
      }

      return doneSomeWork;
    }

    /// <summary>
    /// Has the connection been inactive too long?
    /// </summary>
    public bool Overdue
    {
      get { return DateTime.Now.Subtract(this.lastActivity).TotalMinutes > this.clientTimeOut; }
    }

    /// <summary>
    /// Close the connection.
    /// </summary>
    public void Close()
    {
      this.client.Close();
    }
  }
}
