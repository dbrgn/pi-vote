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
  [SerializeObject("Contains all the shares and verification values of one authority.")]
  public class SharePart : Serializable
  {
    /// <summary>
    /// Id of the voting procedure.
    /// </summary>
    public Guid VotingId { get; private set; }

    /// <summary>
    /// Index of the issuing authority.
    /// </summary>
    [SerializeField(0, "Index of the issuing authority.")]
    public int AuthorityIndex { get; private set; }

    /// <summary>
    /// Encrypted shares for the other authorities.
    /// </summary>
    [SerializeField(1, "Encrypted shares for the other authorities.")]
    public List<Encrypted<Share>> EncryptedShares { get; private set; }

    /// <summary>
    /// Verification values for the shares.
    /// </summary>
    [SerializeField(2, "Verification values for the shares.")]
    public List<VerificationValue> VerificationValues { get; private set; }

    /// <summary>
    /// Creates a new share part for an authority.
    /// </summary>
    /// <remarks>
    /// Don't forget to add encrypted shares and verifcation values.
    /// </remarks>
    /// <param name="votingId">Id of the voting procedure.</param>
    /// <param name="authorityIndex">Index of the issuing authority.</param>
    public SharePart(Guid votingId, int authorityIndex)
    {
      VotingId = votingId;
      AuthorityIndex = authorityIndex;
      EncryptedShares = new List<Encrypted<Share>>();
      VerificationValues = new List<VerificationValue>();
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public SharePart(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(AuthorityIndex);
      context.WriteList(EncryptedShares);
      context.WriteList(VerificationValues);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      AuthorityIndex = context.ReadInt32();
      EncryptedShares = context.ReadObjectList<Encrypted<Share>>();
      VerificationValues = context.ReadObjectList<VerificationValue>();
    }
  }
}
