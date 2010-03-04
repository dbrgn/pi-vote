
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
    /// Callback for the create share parts operation.
    /// </summary>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void CreateSharePartCallBack(Exception exception);

    /// <summary>
    /// Create share parts.
    /// </summary>
    private class CreateSharePartOperation : Operation
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
      private CreateSharePartCallBack callBack;

      /// <summary>
      /// Create a new vote cast opeation.
      /// </summary>
      /// <param name="votingId">Id of the voting.</param>
      /// <param name="authorityFileName">Filename to save authority data.</param>
      /// <param name="authorityCertificate">Authority's certificate.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public CreateSharePartOperation(Guid votingId, AuthorityCertificate authorityCertificate, string authorityFileName, CreateSharePartCallBack callBack)
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
          Text = "Preparing authority.";
          Progress = 0d;
          SubText = string.Empty;
          SubProgress = 0d;

          var parameters = client.proxy.FetchParameters(this.votingId, this.authorityCertificate);

          client.authorityEntity.Prepare(parameters.Key, parameters.Value);

          Text = "Fetching authority list.";
          Progress = 0.25d;

          var authorityList = client.proxy.FetchAuthorityList(this.votingId);

          client.authorityEntity.SetAuthorities(authorityList);

          Text = "Saving authority.";
          Progress = 0.5d;

          client.SaveAuthority(this.authorityFileName);

          Text = "Creating and pushing share parts.";
          Progress = 0.75d;

          var sharePart = client.authorityEntity.GetShares();

          client.proxy.PushShares(this.votingId, sharePart);

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
