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
using Emil.GMP;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  public class Voting : Serializable
  {
    private List<Question> questions;

    public Guid Id { get; private set; }

    public MultiLanguageString Title { get; private set; }

    public MultiLanguageString Description { get; private set; }

    public IEnumerable<Question> Questions { get { return this.questions; } }

    /// <summary>
    /// Date at which voting begins.
    /// </summary>
    public DateTime VotingBeginDate { get; private set; }

    /// <summary>
    /// Date a which voting ends.
    /// </summary>
    public DateTime VotingEndDate { get; private set; }

    /// <summary>
    /// Number of authorities.
    /// </summary>
    public int AuthorityCount { get; private set; }

    public Voting()
    {
      this.questions = new List<Question>();
    }

    public Voting(DeserializeContext context)
      : base(context)
    { 
    }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Id);
      context.Write(Title);
      context.Write(Description);
      context.WriteList(this.questions);
      context.Write(VotingBeginDate);
      context.Write(VotingEndDate);
      context.Write(AuthorityCount);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Id = context.ReadGuid();
      Title = context.ReadMultiLanguageString();
      Description = context.ReadMultiLanguageString();
      this.questions = context.ReadObjectList<Question>();
      VotingBeginDate = context.ReadDateTime();
      VotingEndDate = context.ReadDateTime();
      AuthorityCount = context.ReadInt32();
    }
  }
}
