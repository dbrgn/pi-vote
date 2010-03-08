
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
    /// Callback for the authority certificates get operation.
    /// </summary>
    /// <param name="authorityCertificates">List of authority certificates or null in case of failure.</param>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void GetAuthorityCertificatesCallBack(IEnumerable<AuthorityCertificate> authorityCertificates, Exception exception);

    /// <summary>
    /// Authority certificates get operation.
    /// </summary>
    private class GetAuthorityCertificatesOperation : Operation
    {
      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private GetAuthorityCertificatesCallBack callBack;

      /// <summary>
      /// Create a new voting list get operation.
      /// </summary>
      /// <param name="callBack">Callback upon completion.</param>
      public GetAuthorityCertificatesOperation(GetAuthorityCertificatesCallBack callBack)
      {
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
          Text = LibraryResources.ClientGetAuthorityCertificates;
          Progress = 0d;
          SubText = string.Empty;
          SubProgress = 0d;

          var authorityCertificates = client.proxy.FetchAuthorityCertificates();

          this.callBack(authorityCertificates, null);
        }
        catch (Exception exception)
        {
          this.callBack(null, exception);
        }
      }
    }
  }
}
