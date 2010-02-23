
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
    public delegate void GetSignatureResponseCallBack(SignatureResponseStatus status, Signed<SignatureResponse> response, Exception exception);

    /// <summary>
    /// Set signature operation.
    /// </summary>
    private class GetSignatureResponseOperation : Operation
    {
      /// <summary>
      /// Id of the certificate.
      /// </summary>
      private Guid certificateId;

      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private GetSignatureResponseCallBack callBack;

      /// <summary>
      /// Create a new vote cast opeation.
      /// </summary>
      /// <param name="votingId">Id of the voting.</param>
      /// <param name="optionIndex">Index of chosen option.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public GetSignatureResponseOperation(Guid certificateId, GetSignatureResponseCallBack callBack)
      {
        this.certificateId = certificateId;
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
          Text = "Set signature request.";
          Progress = 0d;
          SubText = string.Empty;
          SubProgress = 0d;

          Signed<SignatureResponse> signatureResponse;
          SignatureResponseStatus status = client.proxy.FetchSignatureResponse(this.certificateId, out signatureResponse);

          Progress = 1d;

          this.callBack(status, signatureResponse, null);
        }
        catch (Exception exception)
        {
          this.callBack(SignatureResponseStatus.Unknown, null, exception);
        }
      }
    }
  }
}
