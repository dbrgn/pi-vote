
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
using System.Security.Cryptography;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Receipt for a cast vote.
  /// </summary>
  /// <remarks>
  /// To be signed by the server.
  /// </remarks>
  public class VoteReceipt : Serializable
  {
    public Guid VotingId { get; private set; }

    public string VotingTitle { get; private set; }

    public Guid VoterId { get; private set; }

    private byte[] SignedEnvelopeHash { get; set; }

    /// <summary>
    /// Creates a new vote receipt.
    /// </summary>
    /// <param name="parameters">Voting parameters.</param>
    /// <param name="signedEnvelope">Signed envelope.</param>
    public VoteReceipt(VotingParameters parameters, Signed<Envelope> signedEnvelope)
    {
      VotingId = parameters.VotingId;
      VotingTitle = parameters.Title;
      VoterId = signedEnvelope.Certificate.Id;
      SHA256Managed sha256 = new SHA256Managed();
      SignedEnvelopeHash = sha256.ComputeHash(signedEnvelope.ToBinary());
    }

    /// <summary>
    /// Verifies that the envelope was not tampered with.
    /// </summary>
    /// <param name="signedEnvelope">Signed envelope.</param>
    /// <returns>Is it valid?</returns>
    public bool Verify(Signed<Envelope> signedEnvelope)
    {
      SHA256Managed sha256 = new SHA256Managed();
      return sha256.ComputeHash(signedEnvelope.ToBinary())
        .Equal(SignedEnvelopeHash);
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public VoteReceipt(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
    }
  }
}
