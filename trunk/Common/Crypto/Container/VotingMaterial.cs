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
  /// All things a voter needs to cast his vote.
  /// </summary>
  public class VotingMaterial : Serializable
  {
    /// <summary>
    /// Id of the voting procedure.
    /// </summary>
    public int VotingId { get { return Parameters.VotingId; } }

    /// <summary>
    /// Defines voting procedure.
    /// </summary>
    public VotingParameters Parameters { get; private set; }

    /// <summary>
    /// Responses that can be combined to a public key.
    /// </summary>
    public List<Signed<ShareResponse>> PublicKeyParts { get; private set; }

    /// <summary>
    /// Revocation lists of the CA certificates.
    /// </summary>
    public List<Signed<RevocationList>> RevocationLists { get; private set; }

    /// <summary>
    /// Intermediate certificates.
    /// </summary>
    public List<Certificate> Certificates { get; private set; }

    /// <summary>
    /// Createa new voting material.
    /// </summary>
    /// <param name="parameters">Voting parameters.</param>
    /// <param name="publicKeyParts">Parts of the public key form authorities.</param>
    /// <param name="revocationLists">Certification revocation lists for CAs.</param>
    /// <param name="certificates">List of intermediate certificates.</param>
    public VotingMaterial(
      VotingParameters parameters, 
      IEnumerable<Signed<ShareResponse>> publicKeyParts,
      IEnumerable<Signed<RevocationList>> revocationLists,
      IEnumerable<Certificate> certificates)
    {
      if (parameters == null)
        throw new ArgumentNullException("parameters");
      if (publicKeyParts == null)
        throw new ArgumentNullException("publicKeyParts");

      Parameters = parameters;
      PublicKeyParts = new List<Signed<ShareResponse>>(publicKeyParts);
      Certificates = new List<Certificate>(certificates);
      RevocationLists = new List<Signed<RevocationList>>(revocationLists);
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
      context.WriteList(Certificates);
      context.WriteList(RevocationLists);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Parameters = context.ReadObject<VotingParameters>();
      PublicKeyParts = context.ReadObjectList<Signed<ShareResponse>>();
      Certificates = context.ReadObjectList<Certificate>();
      RevocationLists = context.ReadObjectList<Signed<RevocationList>>();
    }
  }
}
