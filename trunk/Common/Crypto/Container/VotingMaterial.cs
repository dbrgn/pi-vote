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

    public VotingMaterial(VotingParameters parameters, IEnumerable<Signed<ShareResponse>> publicKeyParts)
    {
      if (parameters == null)
        throw new ArgumentNullException("parameters");
      if (publicKeyParts == null)
        throw new ArgumentNullException("publicKeyParts");

      Parameters = parameters;
      PublicKeyParts = new List<Signed<ShareResponse>>(publicKeyParts);
    }

    public VotingMaterial(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Parameters);
      context.WriteList(PublicKeyParts);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Parameters = context.ReadObject<VotingParameters>();
      PublicKeyParts = context.ReadObjectList<Signed<ShareResponse>>();
    }
  }
}
