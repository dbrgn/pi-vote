
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
      /// Certificate storage to check against.
      /// </summary>
      private CertificateStorage certificateStorage;

      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private GetVotingListCallBack callBack;

      /// <summary>
      /// Path where program data is stored.
      /// </summary>
      private string dataPath;

      /// <summary>
      /// Create a new voting list get operation.
      /// </summary>
      /// <param name="certificateStorage">Certificate storage to check against.</param>
      /// <param name="dataPath">Path where program data is stored.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public GetVotingListOperation(CertificateStorage certificateStorage, string dataPath, GetVotingListCallBack callBack)
      {
        this.certificateStorage = certificateStorage;
        this.dataPath = dataPath;
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
          Text = LibraryResources.ClientGetVotingList;
          Progress = 0d;
          SubText = LibraryResources.ClientGetVotingListGetIds;
          SubProgress = 0d;

          List<VotingDescriptor> votingList = new List<VotingDescriptor>();
          IEnumerable<Guid> votingIds = client.proxy.FetchVotingIds();

          int counter = 0;
          Progress = 0.2d;
          SubText = string.Format(LibraryResources.ClientGetVotingListFetchVoting, counter, votingIds.Count());
          SubProgress = 0d;

          foreach (Guid votingId in votingIds)
          {
            List<Guid> authoritiesDone = null;
            VotingStatus status = client.proxy.FetchVotingStatus(votingId, out authoritiesDone);
            VotingMaterial material = client.proxy.FetchVotingMaterial(votingId);

            if (material.Valid(this.certificateStorage))
            {
              VotingParameters parameters = material.Parameters.Value;
              votingList.Add(new VotingDescriptor(parameters, status, authoritiesDone, material.CastEnvelopeCount));
            }

            counter++;
            SubText = string.Format(LibraryResources.ClientGetVotingListFetchVoting, counter, votingIds.Count());
            SubProgress = 1d / (double)votingIds.Count() * (double)counter;
          }

          Progress = 0.6d;
          SubProgress = 1d;

          DirectoryInfo appDataDirectory = new DirectoryInfo(this.dataPath);

          foreach (DirectoryInfo offlineDirectory in appDataDirectory.GetDirectories())
          {
            if (File.Exists(Path.Combine(offlineDirectory.FullName, Files.VotingMaterialFileName)))
            {
              votingList.Add(new VotingDescriptor(offlineDirectory.FullName));
            }
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
