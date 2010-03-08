
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
    /// Callback for the create voting operation.
    /// </summary>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void CreateVotingCallBack(Exception exception);

    /// <summary>
    /// Create voting operation.
    /// </summary>
    private class CreateVotingOperation : Operation
    {
      /// <summary>
      /// Parameters of voting procedure.
      /// </summary>
      private Signed<VotingParameters> votingParameters;

      /// <summary>
      /// Authorities oversseeing this voting.
      /// </summary>
      private IEnumerable<AuthorityCertificate> authorities;

      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private CreateVotingCallBack callBack;

      /// <summary>
      /// Create a new vote cast opeation.
      /// </summary>
      /// <param name="votingParameters">Parameters of voting procedure.</param>
      /// <param name="authorities">Authorities oversseeing this voting.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public CreateVotingOperation(Signed<VotingParameters> votingParameters, IEnumerable<AuthorityCertificate> authorities, CreateVotingCallBack callBack)
      {
        this.votingParameters = votingParameters;
        this.authorities = authorities;
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
          Text = LibraryResources.ClientCreateVoting;
          Progress = 0d;
          SubText = string.Empty;
          SubProgress = 0d;

          client.proxy.CreateVoting(this.votingParameters, this.authorities);

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
