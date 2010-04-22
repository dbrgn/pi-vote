
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
    /// Callback for the create deciphers operation.
    /// </summary>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void CreateDeciphersCallBack(VotingDescriptor votingDescriptor, Exception exception);

    /// <summary>
    /// Create decipherss parts.
    /// </summary>
    private class CreateDeciphersOperation : Operation
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
      private CreateDeciphersCallBack callBack;

      /// <summary>
      /// Create a new vote cast opeation.
      /// </summary>
      /// <param name="votingId">Id of the voting.</param>
      /// <param name="authorityFileName">Filename to save authority data.</param>
      /// <param name="authorityCertificate">Authority's certificate.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public CreateDeciphersOperation(Guid votingId, AuthorityCertificate authorityCertificate, string authorityFileName, CreateDeciphersCallBack callBack)
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
          Text = LibraryResources.ClientCreateDeciphersLoadAuthority;
          Progress = 0d;
          SubText = string.Empty;
          SubProgress = 0d;

          client.LoadAuthority(this.authorityFileName, this.authorityCertificate);

          Text = LibraryResources.ClientCreateDeciphersFetchMaterial;
          Progress = 0.1d;

          var material = client.proxy.FetchVotingMaterial(this.votingId);
          client.authorityEntity.TallyBegin(material);

          Text = LibraryResources.ClientCreateDeciphersFetchEnvelopeCount;
          Progress = 0.2d;
          
          int envelopeCount = client.proxy.FetchEnvelopeCount(this.votingId);

          Text = string.Format(LibraryResources.ClientCreateDeciphersFetchEnvelope, 0, envelopeCount);
          Progress = 0.3d;
          
          for (int envelopeIndex = 0; envelopeIndex < envelopeCount; envelopeIndex++)
          {
            var signedEnvelope = client.proxy.FetchEnvelope(this.votingId, envelopeIndex);
            client.authorityEntity.TallyAdd(signedEnvelope);

            Text = string.Format(LibraryResources.ClientCreateDeciphersFetchEnvelope, 0, envelopeIndex + 1);
            Progress += 0.4d / (double)envelopeCount;
          }

          Text = LibraryResources.ClientCreateDeciphersCreatePartialDecipher;
          Progress = 0.7d;

          var decipherList = client.authorityEntity.PartiallyDecipher();

          Text = LibraryResources.ClientCreateDeciphersPushPartialDecipher;
          Progress = 0.8d;
          
          client.proxy.PushPartailDecipher(this.votingId, decipherList);

          Text = LibraryResources.ClientCreateDeciphersFetchVotingStatus;
          Progress = 0.9d;

          var parameters = client.proxy.FetchParameters(this.votingId, this.authorityCertificate); 
          List<Guid> authoritieDone;
          VotingStatus status = client.proxy.FetchVotingStatus(this.votingId, out authoritieDone);
          var votingDescriptor = new VotingDescriptor(parameters.Value.Value, status, authoritieDone);

          Progress = 1d;

          this.callBack(votingDescriptor, null);
        }
        catch (Exception exception)
        {
          this.callBack(null, exception);
        }
      }
    }
  }
}
