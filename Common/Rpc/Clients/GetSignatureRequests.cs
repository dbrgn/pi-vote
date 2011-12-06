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
using System.Threading;
using System.Net;
using System.IO;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// Asynchronous client.
  /// </summary>
  public partial class VotingClient
  {
    /// <summary>
    /// Callback for the signature requests get operation.
    /// </summary>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void GetSignatureRequestsCallBack(Exception exception);

    /// <summary>
    /// Cignature requests get operation.
    /// </summary>
    private class GetSignatureRequestsOperation : Operation
    {
      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private GetSignatureRequestsCallBack callBack;

      /// <summary>
      /// Path to save signature request files.
      /// </summary>
      private string savePath;

      /// <summary>
      /// Create a new voting list get operation.
      /// </summary>
      /// <param name="callBack">Callback upon completion.</param>
      public GetSignatureRequestsOperation(string savePath, GetSignatureRequestsCallBack callBack)
      {
        this.savePath = savePath;
        this.callBack = callBack;
      }

      /// <summary>
      /// Execute the operation.
      /// </summary>
      /// <param name="client">Voter client to execute against.</param>
      public override void Execute(VotingClient client)
      {
        try
        {
          Text = LibraryResources.ClientGetSignatureRequests;
          Progress = 0d;
          SubText = LibraryResources.ClientGetSignatureRequestsGetList;
          SubProgress = 0d;

          var signatureRequestList = client.proxy.FetchSignatureRequestList();

          int max = signatureRequestList.Count();
          int done = 0;

          if (max > 0)
          {
            Progress = 0.5d;
            SubText = string.Format(LibraryResources.ClientGetSignatureRequestsGetRequest, done, max);
            SubProgress = (double)done / (double)max;
            HasSingleProgress = true;
            SingleProgress = SubProgress;

            foreach (Guid id in signatureRequestList)
            {
              var signatureRequest = client.proxy.FetchSignatureRequest(id);
              string fileName = Path.Combine(savePath, id.ToString() + ".pi-sig-req");
              signatureRequest.Save(fileName);

              done++;
              SubText = string.Format(LibraryResources.ClientGetSignatureRequestsGetRequest, done, max);
              SubProgress = (double)done / (double)max;
              SingleProgress = SubProgress;
            }

            Progress = 1d;
            SubProgress = 1d;
            HasSingleProgress = false;
          }
          else
          {
            Progress = 1d;
            SubText = LibraryResources.ClientGetSignatureRequestsNoRequest;
            SubProgress = 1d;
          }

          this.callBack(null);
        }
        catch (Exception exception)
        {
          this.callBack(exception);
        }
      }
    }
  }
}
