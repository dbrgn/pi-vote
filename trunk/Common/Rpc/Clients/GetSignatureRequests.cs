
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
          Text = "Getting signature requests";
          Progress = 0d;
          SubText = "Getting list of signature requests";
          SubProgress = 0d;

          var signatureRequestList = client.proxy.FetchSignatureRequestList();

          int max = signatureRequestList.Count();
          int done = 0;

          if (max > 0)
          {
            Progress = 0.5d;
            SubText = string.Format("Getting signature requests {0} / {1}", done, max);
            SubProgress = (double)done / (double)max;

            foreach (Guid id in signatureRequestList)
            {
              var signatureRequest = client.proxy.FetchSignatureRequest(id);
              string fileName = Path.Combine(savePath, id.ToString() + ".pi-sig-req");
              signatureRequest.Save(fileName);

              done++;
              SubText = string.Format("Getting signature requests {0} / {1}", done, max);
              SubProgress = (double)done / (double)max;
            }
          }

          Progress = 1d;
          SubText = "No signature request on server.";
          SubProgress = 1d;

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
