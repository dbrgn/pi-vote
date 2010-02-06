﻿
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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Assembly of all share parts from all authorities.
  /// </summary>
  /// <remarks>
  /// Used by authorities to verify share parts.
  /// </remarks>
  public class AllShareParts : Serializable
  {
    /// <summary>
    /// Id of the voting procedure.
    /// </summary>
    public int VotingId { get; private set; }

    /// <summary>
    /// Share parts from all authorities.
    /// </summary>
    public List<Signed<SharePart>> ShareParts { get; private set; }

    /// <summary>
    /// Create a new assembly of all share parts.
    /// </summary>
    /// <param name="votingId">Id of the voting procedure.</param>
    /// <param name="shareParts">Share parts from all authorities.</param>
    public AllShareParts(int votingId, IEnumerable<Signed<SharePart>> shareParts)
    {
      if (shareParts == null)
        throw new ArgumentNullException("shareParts");

      VotingId = votingId;
      ShareParts = new List<Signed<SharePart>>(shareParts);
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public AllShareParts(DeserializeContext context)
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
      context.WriteList(ShareParts);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingId = context.ReadInt32();
      ShareParts = context.ReadObjectList<Signed<SharePart>>();
    }
  }
}