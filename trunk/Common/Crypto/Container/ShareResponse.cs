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
  /// Response of an authority to the sharings.
  /// </summary>
  /// <remarks>
  /// May either be negative (AcceptShares = false) or positive 
  /// (AcceptShares = true) in which case it also contains the
  /// public key part from that authority.
  /// </remarks>
  public class ShareResponse : Serializable
  {
    /// <summary>
    /// Id of the voting procedure.
    /// </summary>
    public Guid VotingId { get; private set; }

    /// <summary>
    /// Index of the issuing authority.
    /// </summary>
    public int AuthorityIndex { get; private set; }

    /// <summary>
    /// Does the authority accept all the shares?
    /// </summary>
    public bool AcceptShares { get; private set; }

    /// <summary>
    /// Public key part from that authority.
    /// </summary>
    /// <remarks>
    /// Must be 0 if shares are not accepted.
    /// </remarks>
    public BigInt PublicKeyPart { get; private set; }

    /// <summary>
    /// Create a new response to shares.
    /// </summary>
    /// <param name="votingId">Id of the voting procedure.</param>
    /// <param name="authorityIndex">Index of the issuing authority.</param>
    /// <param name="acceptShares">Does the authority accept all the shares?</param>
    /// <param name="publicKeyPart">Public key part from that authority.</param>
    public ShareResponse(Guid votingId, int authorityIndex, bool acceptShares, BigInt publicKeyPart)
    {
      if (publicKeyPart == null)
        throw new ArgumentNullException("publicKeyPart");

      VotingId = votingId;
      AuthorityIndex = authorityIndex;
      AcceptShares = acceptShares;
      PublicKeyPart = publicKeyPart;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public ShareResponse(DeserializeContext context)
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
      context.Write(AuthorityIndex);
      context.Write(AcceptShares);
      context.Write(PublicKeyPart);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingId = context.ReadGuid();
      AuthorityIndex = context.ReadInt32();
      AcceptShares = context.ReadBoolean();
      PublicKeyPart = context.ReadBigInt();
    }
  }
}
