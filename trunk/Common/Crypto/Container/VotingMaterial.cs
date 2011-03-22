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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// All things a voter needs to cast his vote.
  /// </summary>
  [SerializeObject("All things a voter needs to cast his vote.")]
  public class VotingMaterial : Serializable
  {
    /// <summary>
    /// Defines voting procedure.
    /// </summary>
    [SerializeField(0, "Defines voting procedure.")]
    public Signed<VotingParameters> Parameters { get; private set; }

    /// <summary>
    /// Responses that can be combined to a public key.
    /// </summary>
    [SerializeField(1, "Responses that can be combined to a public key.")]
    public List<Signed<ShareResponse>> PublicKeyParts { get; private set; }

    /// <summary>
    /// Number of cast envelopes.
    /// </summary>
    [SerializeField(2, "Number of cast envelopes.")]
    public int CastEnvelopeCount { get; private set; }

    /// <summary>
    /// Createa new voting material.
    /// </summary>
    /// <param name="parameters">Voting parameters.</param>
    /// <param name="publicKeyParts">Parts of the public key form authorities.</param>
    /// <param name="revocationLists">Certification revocation lists for CAs.</param>
    /// <param name="certificates">List of intermediate certificates.</param>
    public VotingMaterial(
      Signed<VotingParameters> parameters, 
      IEnumerable<Signed<ShareResponse>> publicKeyParts,
      int castEnvelopeCount)
    {
      if (parameters == null)
        throw new ArgumentNullException("parameters");
      if (publicKeyParts == null)
        throw new ArgumentNullException("publicKeyParts");

      Parameters = parameters;
      PublicKeyParts = new List<Signed<ShareResponse>>(publicKeyParts);
      CastEnvelopeCount = castEnvelopeCount;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public VotingMaterial(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Parameters);
      context.WriteList(PublicKeyParts);
      context.Write(CastEnvelopeCount);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Parameters = context.ReadObject<Signed<VotingParameters>>();
      PublicKeyParts = context.ReadObjectList<Signed<ShareResponse>>();
      CastEnvelopeCount = context.ReadInt32();
    }

    /// <summary>
    /// Is this material valid?
    /// </summary>
    /// <remarks>
    /// Might add some certificates to the storage.
    /// </remarks>
    /// <param name="certificateStorage">Certificate storage to check against.</param>
    /// <returns>Validity of the material.</returns>
    public bool Valid(CertificateStorage certificateStorage)
    {
      bool valid = true;
      VotingParameters parameters = Parameters.Value;

      valid &= Parameters.Verify(certificateStorage, parameters.VotingBeginDate);

      foreach (Signed<ShareResponse> signedShareResponse in PublicKeyParts)
      {
        valid &= signedShareResponse.Verify(certificateStorage, parameters.VotingBeginDate);

        ShareResponse shareResponse = signedShareResponse.Value;
        valid &= shareResponse.AcceptShares;
      }

      return valid;
    }
  }
}
