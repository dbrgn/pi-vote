﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
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
    /// Abstract opertion of the voting client.
    /// </summary>
    public abstract class Operation
    {
      /// <summary>
      /// Execute the operation.
      /// </summary>
      /// <remarks>
      /// Don't forget to callback when implementing.
      /// </remarks>
      /// <param name="client">Voter client to execute against.</param>
      public abstract void Execute(VotingClient client);

      /// <summary>
      /// Progress of the operation.
      /// </summary>
      public double Progress { get; protected set; }

      /// <summary>
      /// Progress of a long running suboperation. May reset to zero multiple times during an operation.
      /// </summary>
      public double SingleProgress { get; protected set; }

      /// <summary>
      /// Has the current operation currently a meaningful single progress value?
      /// </summary>
      public bool HasSingleProgress { get; protected set; }

      /// <summary>
      /// Text of the operation.
      /// </summary>
      public string Text { get; protected set; }

      /// <summary>
      /// Progress of the suboperation.
      /// </summary>
      public double SubProgress { get; protected set; }

      /// <summary>
      /// Text of the suboperation.
      /// </summary>
      public string SubText { get; protected set; }
    }
  }
}
