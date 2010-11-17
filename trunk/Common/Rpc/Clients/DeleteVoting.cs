
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
    /// Callback for the delete voting operation.
    /// </summary>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void DeleteVotingCallBack(Exception exception);

    /// <summary>
    /// Create voting operation.
    /// </summary>
    private class DeleteVotingOperation : Operation
    {
      /// <summary>
      /// Command to delete the voting.
      /// </summary>
      private Signed<DeleteVotingRequest.Command> command;

      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private DeleteVotingCallBack callBack;

      /// <summary>
      /// Create a new vote cast opeation.
      /// </summary>
      /// <param name="command">Command to delete the voting.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public DeleteVotingOperation(Signed<DeleteVotingRequest.Command> command, DeleteVotingCallBack callBack)
      {
        this.command = command;
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
          Text = LibraryResources.ClientDeleteVoting;
          Progress = 0d;
          SubText = string.Empty;
          SubProgress = 0d;

          client.proxy.DeleteVoting(this.command);

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
