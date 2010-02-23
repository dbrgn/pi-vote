
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
    /// Callback for the voting list get operation.
    /// </summary>
    /// <param name="votingList">Voting list or null in case of failure.</param>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void GetVotingListCallBack(IEnumerable<VotingDescriptor> votingList, Exception exception);

    /// <summary>
    /// Voting list get operation.
    /// </summary>
    private class GetVotingListOperation : Operation
    {
      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private GetVotingListCallBack callBack;

      /// <summary>
      /// Create a new voting list get operation.
      /// </summary>
      /// <param name="callBack">Callback upon completion.</param>
      public GetVotingListOperation(GetVotingListCallBack callBack)
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
          Text = "Getting voting list";
          Progress = 0d;
          SubText = "Fetching voting ids";
          SubProgress = 0d;

          List<VotingDescriptor> votingList = new List<VotingDescriptor>();
          IEnumerable<Guid> votingIds = client.proxy.FetchVotingIds();

          Progress = 0.5d;
          SubText = "Fetching voting material and status";
          SubProgress = 0d;

          foreach (Guid votingId in votingIds)
          {
            VotingStatus status = client.proxy.FetchVotingStatus(votingId);
            VotingMaterial material = client.proxy.FetchVotingMaterial(votingId);

            if (material.Valid(client.voterEntity.CertificateStorage))
            {
              VotingParameters parameters = material.Parameters.Value;
              votingList.Add(new VotingDescriptor(parameters, status));
            }

            SubProgress += 1d / (double)votingIds.Count();
          }

          Progress = 1d;
          SubProgress = 1d;

          this.callBack(votingList, null);
        }
        catch (Exception exception)
        {
          this.callBack(null, exception);
        }
      }
    }
  }
}
