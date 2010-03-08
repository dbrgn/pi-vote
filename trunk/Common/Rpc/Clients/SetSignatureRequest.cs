
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
    /// Callback for the set signature request operation.
    /// </summary>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void SetSignatureRequestCallBack(Exception exception);

    /// <summary>
    /// Set signature operation.
    /// </summary>
    private class SetSignatureRequestOperation : Operation
    {
      /// <summary>
      /// Signed signature request.
      /// </summary>
      private Signed<SignatureRequest> signatureRequest;

      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private SetSignatureRequestCallBack callBack;

      /// <summary>
      /// Create a new vote cast opeation.
      /// </summary>
      /// <param name="votingId">Id of the voting.</param>
      /// <param name="optionIndex">Index of chosen option.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public SetSignatureRequestOperation(Signed<SignatureRequest> signatureRequest, SetSignatureRequestCallBack callBack)
      {
        this.signatureRequest = signatureRequest;
        this.callBack = callBack;
      }

      /// <summary>
      /// Execute the operation.
      /// </summary>
      /// <param name="client">Voting client to execute against.</param>
      public override void Execute(VotingClient client)
      {
        try
        {
          Text = LibraryResources.ClientSetSignatureRequest;
          Progress = 0d;
          SubText = string.Empty;
          SubProgress = 0d;

          client.proxy.PushSignatureRequest(this.signatureRequest);

          Progress = 1d;

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
