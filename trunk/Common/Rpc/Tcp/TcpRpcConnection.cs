
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
  public class TcpRpcConnection
  {
    private TcpClient client;
    private int packetLength;
    private VotingRpcServer rpcServer;
    private NetworkStream stream;
    private DateTime lastActivity;
    private byte[] buffer;
    private BinaryWriter writer;

    public TcpRpcConnection(TcpClient client, VotingRpcServer rpcServer)
    {
      this.lastActivity = DateTime.Now;
      this.client = client;
      this.stream = this.client.GetStream();
      this.writer = new BinaryWriter(this.stream);
      this.packetLength = 0;
      this.rpcServer = rpcServer;
      this.buffer = new byte[0];
    }

    public void Process()
    {
      int count = this.client.Available;

      if (count > 0)
      {
        byte[] data = new byte[count];
        this.stream.Read(data, 0, data.Length);
        this.buffer = buffer.Concat(data);

        if (this.packetLength == 0 && this.buffer.Length >= sizeof(int))
        {
          MemoryStream bufferStream = new MemoryStream(buffer);
          BinaryReader reader = new BinaryReader(bufferStream);
          this.packetLength = reader.ReadInt32();
          this.buffer = this.buffer.Part(sizeof(int));
        }

        if (this.packetLength > 0 && this.buffer.Length >= this.packetLength)
        {
          this.lastActivity = DateTime.Now;

          MemoryStream bufferStream = new MemoryStream(buffer);
          BinaryReader reader = new BinaryReader(bufferStream);
          byte[] requestPacket = reader.ReadBytes(this.packetLength);
          this.buffer = this.buffer.Part(this.packetLength);
          this.packetLength = 0;

          byte[] responsePacket = this.rpcServer.Execute(requestPacket);
          this.writer.Write(responsePacket.Length);
          writer.Write(responsePacket, 0, responsePacket.Length);
        }
      }
    }

    public bool Overdue
    {
      get { return DateTime.Now.Subtract(this.lastActivity).TotalMinutes > 5d; }
    }

    public void Close()
    {
      this.client.Close();
    }
  }
}
