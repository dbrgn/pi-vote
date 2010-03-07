
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
      private readonly string title;
      private readonly string descripton;
      private readonly string question;
      private readonly VotingStatus status;
      private readonly List<Guid> authoritiesDone;
      private readonly List<OptionDescriptor> options;
      private readonly int maxOptions;

      /// <summary>
      /// Id of the voting.
      /// </summary>
      public Guid Id { get { return this.id; } }

      /// <summary>
      /// Title of the voting.
      /// </summary>
      public string Title { get { return this.title; } }

      /// <summary>
      /// Description of the voting.
      /// </summary>
      public string Description { get { return this.descripton; } }

      /// <summary>
      /// Question of the voting.
      /// </summary>
      public string Question { get { return this.question; } }
      
      /// <summary>
      /// Status of the voting.
      /// </summary>
      public VotingStatus Status { get { return this.status; } }

      /// <summary>
      /// List of authorities that have done the current step.
      /// Null if not applicable.
      /// </summary>
      public IEnumerable<Guid> AuthoritiesDone { get { return this.authoritiesDone; } }

      /// <summary>
      /// Options in the voting.
      /// </summary>
      public IEnumerable<OptionDescriptor> Options { get { return this.options; } }

      /// <summary>
      /// Maximum number of options one voter may vote for.
      /// </summary>
      public int MaxOptions { get { return this.maxOptions; } }

      /// <summary>
      /// Create a new voting descriptor.
      /// </summary>
      /// <param name="material">Material of voting to describe.</param>
      /// <param name="status">Status of the voting.</param>
      /// <param name="authoritiesDone">List of authorities that have completed the current step if applicable.</param>
      public VotingDescriptor(VotingParameters parameters, VotingStatus status, List<Guid> authoritiesDone)
      {
        this.id = parameters.VotingId;
        this.title = parameters.Title;
        this.descripton = parameters.Description;
        this.question = parameters.Question;
        this.status = status;
        this.authoritiesDone = authoritiesDone;
        this.options = new List<OptionDescriptor>();
        this.maxOptions = parameters.MaxVota;

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
