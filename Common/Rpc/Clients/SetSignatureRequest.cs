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
      /// Signature Request signed and encrypted for the CA.
      /// </summary>
      private Secure<SignatureRequest> signatureRequest;

      /// <summary>
      /// Signature Request Info signed and encrypted for the server.
      /// </summary>
      private Secure<SignatureRequestInfo> signatureRequestInfo;

      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private SetSignatureRequestCallBack callBack;

      /// <summary>
      /// Create a new vote cast opeation.
      /// </summary>
      /// <param name="signatureRequest">Signature Request signed and encrypted for the CA.</param>
      /// <param name="signatureRequestInfo">Signature Request Info signed and encrypted for the server.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public SetSignatureRequestOperation(Secure<SignatureRequest> signatureRequest, Secure<SignatureRequestInfo> signatureRequestInfo, SetSignatureRequestCallBack callBack)
      {
        this.signatureRequest = signatureRequest;
        this.signatureRequestInfo = signatureRequestInfo;
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

          client.proxy.PushSignatureRequest(this.signatureRequest, this.signatureRequestInfo);

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
