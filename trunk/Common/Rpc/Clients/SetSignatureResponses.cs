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
    /// Callback for the signature responses set operation.
    /// </summary>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void SetSignatureResponsesCallBack(Exception exception);

    /// <summary>
    /// Cignature responses set operation.
    /// </summary>
    private class SetSignatureResponsesOperation : Operation
    {
      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private SetSignatureResponsesCallBack callBack;

      /// <summary>
      /// List of signature response files to set.
      /// </summary>
      private IEnumerable<string> fileNames;

      /// <summary>
      /// Create a new signature responses set operation.
      /// </summary>
      /// <param name="callBack">Callback upon completion.</param>
      public SetSignatureResponsesOperation(IEnumerable<string> fileNames, SetSignatureResponsesCallBack callBack)
      {
        this.fileNames = fileNames;
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
          int max = fileNames.Count();
          int done = 0;

          if (max > 0)
          {
            Text = string.Format(LibraryResources.ClientSetSignatureResponses, done, max);
            Progress = (double)done / (double)max;
            SubText = string.Empty;
            SubProgress = 0d;

            foreach (string fileName in this.fileNames)
            {
              var signatureResponse = Serializable.Load<Signed<SignatureResponse>>(fileName);
              client.proxy.PushSignatureResponse(signatureResponse);
              done++;
              Text = string.Format(LibraryResources.ClientSetSignatureResponses, done, max);
              Progress = (double)done / (double)max;
            }
          }
          else
          {
            Text = LibraryResources.ClientSetSignatureResponsesNoFiles;
            Progress = 1d;
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
