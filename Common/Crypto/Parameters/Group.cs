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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// An group which may organize votings.
  /// </summary>
  /// <remarks>
  /// Example: National Party, Cantonal Party, etc.
  /// </remarks>
  [SerializeObject("An group which may organize votings.")]
  public class Group : Serializable
  {
    /// <summary>
    /// Id of the group.
    /// </summary>
    [SerializeField(0, "Id of the group.")]
    public int Id { get; private set; }

    /// <summary>
    /// Name of the group.
    /// </summary>
    [SerializeField(1, "Name of the group.")]
    public MultiLanguageString Name { get; private set; }

    /// <summary>
    /// Create a new option.
    /// </summary>
    /// <param name="id">Id of the group.</param>
    /// <param name="name">Name of the group.</param>
    public Group(int id, MultiLanguageString name)
    {
      if (name == null)
        throw new ArgumentNullException("text");

      Id = id;
      Name = name;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public Group(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Id);
      context.Write(Name);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      Id = context.ReadInt32();
      Name = context.ReadMultiLanguageString();
    }
  }
}
