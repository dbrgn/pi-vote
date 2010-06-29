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
  /// <summary>
  /// Contains all parameters of a voting.
  /// </summary>
  public class VotingParameters : BaseParameters
  {
    /// <summary>
    /// Id of this voting.
    /// </summary>
    public Guid VotingId { get; private set; }

    /// <summary>
    /// Title of this voting.
    /// </summary>
    public MultiLanguageString Title { get; private set; }

    /// <summary>
    /// Description of this voting.
    /// </summary>
    public MultiLanguageString Description { get; private set; }

    /// <summary>
    /// Date at which voting begins.
    /// </summary>
    public DateTime VotingBeginDate { get; private set; }

    /// <summary>
    /// Date a which voting ends.
    /// </summary>
    public DateTime VotingEndDate { get; private set; }

    /// <summary>
    /// Canton in which the voting takes place.
    /// </summary>
    public Canton Canton { get; private set; }
    
    /// <summary>
    /// Create a new voting.
    /// </summary>
    public VotingParameters(
      MultiLanguageString title, 
      MultiLanguageString description, 
      DateTime votingBeginDate, 
      DateTime votingEndDate,
      Canton canton)
    {
      if (title == null)
        throw new ArgumentNullException("title");
      if (description == null)
        throw new ArgumentNullException("description");

      VotingId = Guid.NewGuid();
      Title = title;
      Description = description;
      VotingBeginDate = votingBeginDate;
      VotingEndDate = votingEndDate;
      Canton = canton;
    }

    /// <summary>
    /// Creates a set of test parameters.
    /// </summary>
    /// <returns>Test voting parameters</returns>
    public static VotingParameters CreateTestParameters()
    {
      VotingParameters parameters = new VotingParameters(512);
      Question question = new Question(new MultiLanguageString("?"), new MultiLanguageString(string.Empty), 2);
      question.AddOption(new Option(new MultiLanguageString("A"), new MultiLanguageString(string.Empty)));
      question.AddOption(new Option(new MultiLanguageString("B"), new MultiLanguageString(string.Empty)));
      question.AddOption(new Option(new MultiLanguageString("C"), new MultiLanguageString(string.Empty)));
      question.AddOption(new Option(new MultiLanguageString("D"), new MultiLanguageString(string.Empty)));
      parameters.AddQuestion(question);

      return parameters;
    }

    /// <summary>
    /// Create a new voting with test parameters.
    /// </summary>
    /// <remarks>
    /// Must only be used for testing.
    /// </remarks>
    /// <param name="primeBits">Number of bits of the safe prime.</param>
    private VotingParameters(int primeBits)
      : base(primeBits)
    {
      VotingId = Guid.NewGuid();
      Title = new MultiLanguageString("Test");
      Description = new MultiLanguageString(string.Empty);
      VotingBeginDate = DateTime.Now;
      VotingEndDate = DateTime.Now.AddDays(1);
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public VotingParameters(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(VotingId);
      context.Write(Title);
      context.Write(Description);
      context.Write(VotingBeginDate);
      context.Write(VotingEndDate);
      context.Write((int)Canton);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingId = context.ReadGuid();
      Title = context.ReadMultiLanguageString();
      Description = context.ReadMultiLanguageString();
      VotingBeginDate = context.ReadDateTime();
      VotingEndDate = context.ReadDateTime();
      Canton = (Canton)context.ReadInt32();
    }
  }
}
