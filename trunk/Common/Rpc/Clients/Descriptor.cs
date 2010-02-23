
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
    /// Voting descriptor.
    /// </summary>
    public class VotingDescriptor
    {
      private readonly Guid id;
      private readonly string name;
      private readonly VotingStatus status;
      private readonly List<OptionDescriptor> options;

      /// <summary>
      /// Id of the voting.
      /// </summary>
      public Guid Id { get { return this.id; } }

      /// <summary>
      /// Name of the voting.
      /// </summary>
      public string Name { get { return this.name; } }

      /// <summary>
      /// Status of the voting.
      /// </summary>
      public VotingStatus Status { get { return this.status; } }

      /// <summary>
      /// Options in the voting.
      /// </summary>
      public IEnumerable<OptionDescriptor> Options { get { return this.options; } }

      /// <summary>
      /// Create a new voting descriptor.
      /// </summary>
      /// <param name="material">Material of voting to describe.</param>
      /// <param name="status">Status of the voting.</param>
      public VotingDescriptor(VotingParameters parameters, VotingStatus status)
      {
        this.id = parameters.VotingId;
        this.name = parameters.VotingName;
        this.status = status;
        this.options = new List<OptionDescriptor>();

        parameters.Options.Foreach(option => this.options.Add(new OptionDescriptor(option)));
      }
    }

    /// <summary>
    /// Voting option descriptor.
    /// </summary>
    public class OptionDescriptor
    {
      private readonly string text;
      private readonly string description;

      /// <summary>
      /// Text of the option.
      /// </summary>
      public string Text { get { return this.text; } }

      /// <summary>
      /// Description of the option.
      /// </summary>
      public string Description { get { return this.description; } }

      /// <summary>
      /// Creates a new option decriptor.
      /// </summary>
      /// <param name="option">Option to decscribe.</param>
      public OptionDescriptor(Option option)
      {
        this.text = option.Text;
        this.description = option.Description;
      }
    }
  }
}
