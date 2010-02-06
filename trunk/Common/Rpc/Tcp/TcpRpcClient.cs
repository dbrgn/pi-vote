
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
  public class TcpRpcClient : IBinaryRpcProxy
  {
    private const int Port = 2323;
    private TcpClient client;
    private NetworkStream stream;

    public TcpRpcClient()
    {
      this.client = new TcpClient();
    }

    public void Connect(IPAddress address)
    {
      this.client.Connect(new IPEndPoint(address, Port));
      this.stream = this.client.GetStream();
    }

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
