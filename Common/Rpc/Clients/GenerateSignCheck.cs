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
    /// Callback for generating a sign check.
    /// </summary>
    /// <param name="code">Code to be returned.</param>
    /// <param name="exception">Exception or null if sucessful.</param>
    public delegate void GenerateSignCheckCallBack(byte[] encryptedCode, Exception exception);

    /// <summary>
    /// Sign check generate operation.
    /// </summary>
    private class GenerateSignCheckOperation : Operation
    {
      /// <summary>
      /// Signed sign check cookie.
      /// </summary>
      private Signed<SignCheckCookie> signedCookie;

      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private GenerateSignCheckCallBack callBack;

      /// <summary>
      /// Create a new sign check generation operation.
      /// </summary>
      /// <param name="signedCookie">Signed sign check cookie.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public GenerateSignCheckOperation(Signed<SignCheckCookie> signedCookie, GenerateSignCheckCallBack callBack)
      {
        this.signedCookie = signedCookie;
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
          Text = LibraryResources.ClientGenerateSignCheck;
          Progress = 0d;
          SubText = string.Empty;
          SubProgress = 0d;

          byte[] code = client.proxy.PushSignCheckCookie(this.signedCookie);

          this.callBack(code, null);
        }
        catch (Exception exception)
        {
          this.callBack(null, exception);
        }
      }
    }
  }
}
