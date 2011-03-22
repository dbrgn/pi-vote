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
using System.Security.Cryptography;
using Emil.GMP;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// A question in a voting.
  /// </summary>
  [SerializeObject("A question in a voting.")]
  public class Question : Serializable
  {
    /// <summary>
    /// Number of vota each voter may cast.
    /// </summary>
    [SerializeField(3, "Number of vota each voter may cast.")]
    public int MaxVota { get; private set; }

    /// <summary>
    /// List of possible options for the voters.
    /// </summary>
    [SerializeField(4, "List of possible options for the voters.")]
    private List<Option> options;

    /// <summary>
    /// Text of the question.
    /// </summary>
    /// <example>
    /// Do you wish to abolish the army?
    /// </example>
    [SerializeField(0, "Text of the question.")]
    public MultiLanguageString Text { get; private set; }

    /// <summary>
    /// Description or explaination of the question.
    /// </summary>
    /// <example>
    /// This would mean we could save 100 million $ each year.
    /// </example>
    [SerializeField(1, "Description or explaination of the question.")]
    public MultiLanguageString Description { get; private set; }

    /// <summary>
    /// Url of the discussion of the option.
    /// </summary>
    [SerializeField(2, "Url of the discussion of the option.")]
    public MultiLanguageString Url { get; private set; }

    /// <summary>
    /// List of possible options for the voters.
    /// </summary>
    public IEnumerable<Option> Options
    {
      get { return this.options; }
    }

    /// <summary>
    /// Creates a new question.
    /// </summary>
    /// <param name="question">Text of the question.</param>
    /// <param name="description">Description of the question.</param>
    /// <param name="maxVota">Maximum number of options a voter can select.</param>
    /// <param name="url">Url of the discussion of the option.</param>
    public Question(MultiLanguageString question, MultiLanguageString description, MultiLanguageString url, int maxVota)
    {
      if (question == null)
        throw new ArgumentNullException("question");
      if (description == null)
        throw new ArgumentNullException("description");
      if (!maxVota.InRange(1, 100))
        throw new ArgumentException("maxVota out of range.");

      Description = description;
      Text = question;
      Url = url;
      MaxVota = maxVota;
      this.options = new List<Option>();
    }

    /// <summary>
    /// Add an option to this question.
    /// </summary>
    /// <param name="option">An selectable option.</param>
    public void AddOption(Option option)
    {
      if (option == null)
        throw new ArgumentNullException("option");

      this.options.Add(option);
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public Question(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Text);
      context.Write(Description);
      context.Write(Url);
      context.Write(MaxVota);
      context.WriteList(this.options);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Text = context.ReadMultiLanguageString();
      Description = context.ReadMultiLanguageString();
      Url = context.ReadMultiLanguageString();
      MaxVota = context.ReadInt32();
      this.options = context.ReadObjectList<Option>();
    }
  }
}

