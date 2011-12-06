/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
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
  [SerializeEnum("Status of the voting procedure.")]
  public enum VotingStatus
  {
    New = 0,
    Sharing = 1,
    Voting = 2,
    Aborted = 3,
    Ready = 4,
    Deciphering = 5,
    Finished = 6,
    Offline = 7
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
        case VotingStatus.Offline:
          return LibraryResources.VotingStatusOffline;
        default:
          throw new ArgumentException("Unkown voting status.");
      }
    }
  }
}
