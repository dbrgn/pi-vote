
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
    /// Callback for the check shares operation.
    /// </summary>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void CheckSharesCallBack(VotingDescriptor votingDescriptor, bool accept, Exception exception);

    /// <summary>
    /// Check shares parts.
    /// </summary>
    private class CheckSharesOperation : Operation
    {
      /// <summary>
      /// Id of the voting.
      /// </summary>
      private Guid votingId;

      /// <summary>
      /// Filename to save authority data.
      /// </summary>
      private string authorityFileName;

      /// <summary>
      /// Authority's certificate.
      /// </summary>
      private AuthorityCertificate authorityCertificate;

      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private CheckSharesCallBack callBack;

      /// <summary>
      /// Create a new vote cast opeation.
      /// </summary>
      /// <param name="votingId">Id of the voting.</param>
      /// <param name="authorityFileName">Filename to save authority data.</param>
      /// <param name="authorityCertificate">Authority's certificate.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public CheckSharesOperation(Guid votingId, AuthorityCertificate authorityCertificate, string authorityFileName, CheckSharesCallBack callBack)
      {
        this.votingId = votingId;
        this.authorityFileName = authorityFileName;
        this.authorityCertificate = authorityCertificate;
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
          Text = "Load authority data.";
          Progress = 0d;
          SubText = string.Empty;
          SubProgress = 0d;

          client.LoadAuthority(this.authorityFileName, this.authorityCertificate);

          Text = "Fetching all shares.";
          Progress = 0.2d;

          var allShareParts = client.proxy.FetchAllShares(this.votingId);

          Text = "Verify all shares.";
          Progress = 0.4d;

          var signedShareResponse = client.authorityEntity.VerifyShares(allShareParts);

          Text = "Save authority data.";
          Progress = 0.6d;

          client.SaveAuthority(this.authorityFileName);

          Text = "Push share response to server.";
          Progress = 0.6d;

          client.proxy.PushShareResponse(this.votingId, signedShareResponse);

          Text = "Get new voting status from server.";
          Progress = 0.8d;
          
          var parameters = client.proxy.FetchParameters(this.votingId, this.authorityCertificate); 
          List<Guid> authoritieDone;
          VotingStatus status = client.proxy.FetchVotingStatus(this.votingId, out authoritieDone);
          var votingDescriptor = new VotingDescriptor(parameters.Value, status, authoritieDone);

          Progress = 1d;

          this.callBack(votingDescriptor, signedShareResponse.Value.AcceptShares, null);
        }
        catch (Exception exception)
        {
          this.callBack(null, false, exception);
        }
      }
    }
  }
}
