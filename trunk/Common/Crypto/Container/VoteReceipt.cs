
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
  [SerializeObject("Receipt for a cast vote.")]
  public class VoteReceipt : Serializable
  {
    /// <summary>
    /// Id of the voting.
    /// </summary>
    [SerializeField(0, "Id of the voting.")]
    public Guid VotingId { get; private set; }

    /// <summary>
    /// Title of the voting.
    /// </summary>
    [SerializeField(3, "Title of the voting.")]
    public MultiLanguageString VotingTitle { get; private set; }

    /// <summary>
    /// Id of the voter.
    /// </summary>
    [SerializeField(1, "Id of the voter.")]
    public Guid VoterId { get; private set; }

    /// <summary>
    /// Hash of the signed envelope.
    /// </summary>
    [SerializeField(2, "Hash of the signed envelope.")]
    private byte[] SignedEnvelopeHash { get; set; }

    /// <summary>
    /// Creates a new vote receipt.
    /// </summary>
    /// <param name="parameters">Voting parameters.</param>
    /// <param name="signedEnvelope">Signed envelope.</param>
    public VoteReceipt(VotingParameters parameters, Signed<Envelope> signedEnvelope)
    {
      if (signedEnvelope.Value.VotingId != parameters.VotingId)
        throw new InvalidOperationException("Wrong parameters for envelope.");
      if (signedEnvelope.Value.VoterId != signedEnvelope.Certificate.Id)
        throw new InvalidOperationException("Inconsistent envelope.");

      VotingId = parameters.VotingId;
      VotingTitle = parameters.Title;
      VoterId = signedEnvelope.Certificate.Id;
      SHA256Managed sha256 = new SHA256Managed();
      byte[] signedEnvelopeData = signedEnvelope.ToBinary();
      SignedEnvelopeHash = sha256.ComputeHash(signedEnvelopeData);
    }

    /// <summary>
    /// Verifies that the envelope was not tampered with.
    /// </summary>
    /// <param name="signedEnvelope">Signed envelope.</param>
    /// <returns>Is it valid?</returns>
    public bool Verify(Signed<Envelope> signedEnvelope)
    {
      if (signedEnvelope.Value.VotingId != VotingId)
        return false;
      if (signedEnvelope.Value.VoterId != VoterId)
        return false;

      SHA256Managed sha256 = new SHA256Managed();
      byte[] signedEnvelopeData = signedEnvelope.ToBinary();
      byte[] signedEnvelopeHash = sha256.ComputeHash(signedEnvelopeData);
      return signedEnvelopeHash.Equal(SignedEnvelopeHash);
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
      context.Write(VotingId);
      context.Write(VoterId);
      context.Write(SignedEnvelopeHash);
      context.Write(VotingTitle);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingId = context.ReadGuid();
      VoterId = context.ReadGuid();
      SignedEnvelopeHash = context.ReadBytes();
      VotingTitle = context.ReadMultiLanguageString();
    }
  }
}
