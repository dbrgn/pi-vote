
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
  /// Voting descriptor.
  /// </summary>
  public class VotingDescriptor
  {
    private readonly Guid id;
    private readonly MultiLanguageString title;
    private readonly MultiLanguageString descripton;
    private readonly VotingStatus status;
    private readonly List<Guid> authoritiesDone;
    private readonly DateTime voteFrom;
    private readonly DateTime voteUntil;
    private readonly int authorityCount;
    private readonly int envelopeCount;
    private readonly List<QuestionDescriptor> questions;
    private readonly string offlinePath;
    private readonly int groupId;

    /// <summary>
    /// Id of the voting.
    /// </summary>
    public Guid Id { get { return this.id; } }

    /// <summary>
    /// Title of the voting.
    /// </summary>
    public MultiLanguageString Title { get { return this.title; } }

    /// <summary>
    /// Description of the voting.
    /// </summary>
    public MultiLanguageString Description { get { return this.descripton; } }

    /// <summary>
    /// Status of the voting.
    /// </summary>
    public VotingStatus Status { get { return this.status; } }

    /// <summary>
    /// Id of group in which the voting takes place.
    /// </summary>
    public int GroupId { get { return this.groupId; } }

    /// <summary>
    /// Date when voting begins.
    /// </summary>
    public DateTime VoteFrom { get { return this.voteFrom; } }

    /// <summary>
    /// Date when voting ends.
    /// </summary>
    public DateTime VoteUntil { get { return this.voteUntil; } }

    /// <summary>
    /// Number of authorities needed to complete phase.
    /// </summary>
    public int AuthorityCount { get { return this.authorityCount; } }

    /// <summary>
    /// Number of envelopes cast.
    /// </summary>
    public int EnvelopeCount { get { return this.envelopeCount; } }

    /// <summary>
    /// List of authorities that have done the current step.
    /// Null if not applicable.
    /// </summary>
    public IEnumerable<Guid> AuthoritiesDone { get { return this.authoritiesDone; } }

    /// <summary>
    /// Questions posed in that voting.
    /// </summary>
    public IEnumerable<QuestionDescriptor> Questions { get { return this.questions; } }

    /// <summary>
    /// Offline file path where the voting files are storaged.
    /// </summary>
    public string OfflinePath { get { return this.offlinePath; } }

    /// <summary>
    /// Creates a new voting descriptor from offline files.
    /// </summary>
    /// <param name="offlinePath">Path to the offline files.</param>
    public VotingDescriptor(string offlinePath)
    {
      string materialFileName = Path.Combine(offlinePath, Files.VotingMaterialFileName);

      if (!File.Exists(materialFileName))
        throw new ArgumentException("Offline voting material file not found.");

      DirectoryInfo offlineDirectory = new DirectoryInfo(offlinePath);
      VotingMaterial material = Serializable.Load<VotingMaterial>(materialFileName);
      VotingParameters parameters = material.Parameters.Value;

      this.offlinePath = offlinePath;
      this.id = parameters.VotingId;
      this.title = parameters.Title;
      this.descripton = parameters.Description;
      this.status = VotingStatus.Offline;
      this.authoritiesDone = null;
      this.voteFrom = parameters.VotingBeginDate;
      this.voteUntil = parameters.VotingEndDate;
      this.authorityCount = parameters.AuthorityCount;
      this.envelopeCount = offlineDirectory.GetFiles(Files.EnvelopeFilePattern).Count();
      this.questions = new List<QuestionDescriptor>();
      this.questions.AddRange(parameters.Questions.Select(question => new QuestionDescriptor(question)));
      this.groupId = parameters.GroupId;
    }

    /// <summary>
    /// Create a new voting descriptor.
    /// </summary>
    /// <param name="parameters">Parameters of voting to describe.</param>
    /// <param name="status">Status of the voting.</param>
    /// <param name="authoritiesDone">List of authorities that have completed the current step if applicable.</param>
    public VotingDescriptor(VotingParameters parameters, VotingStatus status, List<Guid> authoritiesDone, int envelopeCount)
    {
      this.id = parameters.VotingId;
      this.title = parameters.Title;
      this.descripton = parameters.Description;
      this.status = status;
      this.authoritiesDone = authoritiesDone;
      this.voteFrom = parameters.VotingBeginDate;
      this.voteUntil = parameters.VotingEndDate;
      this.authorityCount = status == VotingStatus.Deciphering ? parameters.Thereshold + 1 : parameters.AuthorityCount;
      this.envelopeCount = envelopeCount;
      this.questions = new List<QuestionDescriptor>();
      this.questions.AddRange(parameters.Questions.Select(question => new QuestionDescriptor(question)));
      this.groupId = parameters.GroupId;
    }
  }
}
