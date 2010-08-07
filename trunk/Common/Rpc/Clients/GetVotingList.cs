
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
          SubText = string.Empty;
          SubProgress = 0d;

          List<VotingDescriptor> votingList = new List<VotingDescriptor>();
          
          var votingMaterials = client.proxy.FetchVotingMaterial(null);
          var votingDescriptors = votingMaterials
            .Where(votingMaterial => votingMaterial.First.Valid(this.certificateStorage))
            .Select(votingMaterial => new VotingDescriptor(votingMaterial.First.Parameters.Value, votingMaterial.Second, votingMaterial.Third, votingMaterial.First.CastEnvelopeCount));
          votingList.AddRange(votingDescriptors);

          DirectoryInfo appDataDirectory = new DirectoryInfo(this.dataPath);

          foreach (DirectoryInfo offlineDirectory in appDataDirectory.GetDirectories())
          {
            if (File.Exists(Path.Combine(offlineDirectory.FullName, Files.VotingMaterialFileName)))
            {
              votingList.Add(new VotingDescriptor(offlineDirectory.FullName));
            }
          }

          Progress = 1d;
          SubProgress = 0d;

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
