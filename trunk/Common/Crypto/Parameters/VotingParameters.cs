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
  [SerializeObject("Contains all parameters of a voting.")]
  public class VotingParameters : BaseParameters
  {
    /// <summary>
    /// Id of this voting.
    /// </summary>
    [SerializeField(0, "Id of this voting.")]
    public Guid VotingId { get; private set; }

    /// <summary>
    /// Title of this voting.
    /// </summary>
    [SerializeField(1, "Title of this voting.")]
    public MultiLanguageString Title { get; private set; }

    /// <summary>
    /// Description of this voting.
    /// </summary>
    [SerializeField(2, "Description of this voting.")]
    public MultiLanguageString Description { get; private set; }

    /// <summary>
    /// Url of the discussion of the voting.
    /// </summary>
    [SerializeField(3, "Url of the discussion of the voting.")]
    public MultiLanguageString Url { get; private set; }

    /// <summary>
    /// Date at which voting begins.
    /// </summary>
    [SerializeField(4, "Date at which voting begins.")]
    public DateTime VotingBeginDate { get; private set; }

    /// <summary>
    /// Date a which voting ends.
    /// </summary>
    [SerializeField(5, "Date a which voting ends.")]
    public DateTime VotingEndDate { get; private set; }

    /// <summary>
    /// Id of the group in which the voting takes place.
    /// </summary>
    [SerializeField(6, "Id of the group in which the voting takes place.")]
    public int GroupId { get; private set; }
    
    /// <summary>
    /// Create a new voting.
    /// </summary>
    /// <param name="dataPath">Path where application data is stored.</param>
    /// <param name="title">Title of this voting.</param>
    /// <param name="description">Description of this voting.</param>
    /// <param name="url">Url of the discussion of the voting.</param>
    /// <param name="votingBeginDate">Date at which voting begins.</param>
    /// <param name="votingEndDate">Date a which voting ends.</param>
    /// <param name="groupId">Id of the group in which the voting takes place.</param>
    public VotingParameters(
      MultiLanguageString title, 
      MultiLanguageString description, 
      MultiLanguageString url,
      DateTime votingBeginDate, 
      DateTime votingEndDate,
      int groupId)
    {
      if (title == null)
        throw new ArgumentNullException("title");
      if (description == null)
        throw new ArgumentNullException("description");

      VotingId = Guid.NewGuid();
      Title = title;
      Description = description;
      Url = url;
      VotingBeginDate = votingBeginDate;
      VotingEndDate = votingEndDate;
      GroupId = groupId;
    }

    /// <summary>
    /// Creates a set of test parameters.
    /// </summary>
    /// <param name="dataPath">Path where application data is stored.</param>
    /// <returns>Test voting parameters</returns>
    public static VotingParameters CreateTestParameters(string dataPath)
    {
      VotingParameters parameters = new VotingParameters();
      parameters.GenerateNumbers(dataPath, 512);
      Question question = new Question(new MultiLanguageString("?"), new MultiLanguageString(string.Empty), new MultiLanguageString(string.Empty), 2);
      question.AddOption(new Option(new MultiLanguageString("A"), new MultiLanguageString(string.Empty), new MultiLanguageString(string.Empty)));
      question.AddOption(new Option(new MultiLanguageString("B"), new MultiLanguageString(string.Empty), new MultiLanguageString(string.Empty)));
      question.AddOption(new Option(new MultiLanguageString("C"), new MultiLanguageString(string.Empty), new MultiLanguageString(string.Empty)));
      question.AddOption(new Option(new MultiLanguageString("D"), new MultiLanguageString(string.Empty), new MultiLanguageString(string.Empty)));
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
    /// <param name="dataPath">Path where application data is stored.</param>
    private VotingParameters()
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

    public override bool Valid
    {
      get
      {
        return base.Valid &&
          !Title.Text.IsNullOrEmpty() &&
          VotingEndDate >= VotingBeginDate;
      }
    }

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
      context.Write(Url);
      context.Write(VotingBeginDate);
      context.Write(VotingEndDate);
      context.Write(GroupId);
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
      Url = context.ReadMultiLanguageString();
      VotingBeginDate = context.ReadDateTime();
      VotingEndDate = context.ReadDateTime();
      GroupId = context.ReadInt32();
    }
  }
}
