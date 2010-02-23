/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Rpc;

namespace Pirate.PiVote.WebService
{
  /// <summary>
  /// Summary description for Service1
  /// </summary>
  [WebService(Namespace = "http://tempuri.org/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  [System.ComponentModel.ToolboxItem(false)]
  // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
  // [System.Web.Script.Services.ScriptService]
  public class VoteService : System.Web.Services.WebService
  {
    private static VotingRpcServer RpcServer;

    public VoteService()
    {
      if (RpcServer == null)
      {
        RpcServer = new VotingRpcServer();
      }
    }

    [WebMethod]
    public byte[] Execute(byte[] request)
    {
      lock (RpcServer)
      {
        return RpcServer.Execute(request);
      }
    }
  }
}
