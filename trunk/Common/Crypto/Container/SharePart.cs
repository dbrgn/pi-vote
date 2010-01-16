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
  /// Contains all the shares and verification values of one authority.
  /// </summary>
  public class SharePart : Serializable
  {
    /// <summary>
    /// Id of the voting procedure.
    /// </summary>
    public int VotingId { get; private set; }

    /// <summary>
    /// Index of the issuing authority.
    /// </summary>
    public int AuthorityIndex { get; private set; }

    /// <summary>
    /// Encrypted shares for the other authorities.
    /// </summary>
    public List<Encrypted<Share>> EncryptedShares { get; private set; }

    /// <summary>
    /// Verification values for the shares.
    /// </summary>
    public List<VerificationValue> VerificationValues { get; private set; }

    /// <summary>
    /// Creates a new share part for an authority.
    /// </summary>
    /// <remarks>
    /// Don't forget to add encrypted shares and verifcation values.
    /// </remarks>
    /// <param name="votingId">Id of the voting procedure.</param>
    /// <param name="authorityIndex">Index of the issuing authority.</param>
    public SharePart(int votingId, int authorityIndex)
    {
      VotingId = votingId;
      AuthorityIndex = authorityIndex;
      EncryptedShares = new List<Encrypted<Share>>();
      VerificationValues = new List<VerificationValue>();
    }

    public SharePart(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(AuthorityIndex);
      context.WriteList(EncryptedShares);
      context.WriteList(VerificationValues);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      AuthorityIndex = context.ReadInt32();
      EncryptedShares = context.ReadObjectList<Encrypted<Share>>();
      VerificationValues = context.ReadObjectList<VerificationValue>();
    }
  }
}
