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
using MySql.Data.MySqlClient;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Status of the voting procedure.
  /// </summary>
  public enum VotingStatus
  {
    New,
    Sharing,
    Voting,
    Aborted,
    Ready,
    Deciphering,
    Finished,
  }

  public static class VotingStatusExtension
  {
    public static string Text(this VotingStatus status)
    {
      switch (status)
      {
        case VotingStatus.Aborted:
          return LibraryResources.VotingStatusAborted;
        case VotingStatus.Deciphering:
          return LibraryResources.VotingStatusDeciphering;
        case VotingStatus.Finished:
          return LibraryResources.VotingStatusFinished;
        case VotingStatus.New:
          return LibraryResources.VotingStatusNew;
        case VotingStatus.Ready:
          return LibraryResources.VotingStatusReady;
        case VotingStatus.Sharing:
          return LibraryResources.VotingStatusSharing;
        case VotingStatus.Voting:
          return LibraryResources.VotingStatusVoting;
        default:
          throw new ArgumentException("Unkown voting status.");
      }
    }
  }
}
