
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
  /// Proof of a bad sharing.
  /// </summary>
  [SerializeObject("Proof of a bad sharing.")]
  public class BadShareProof : Serializable
  {
    /// <summary>
    /// Index of the complaining authority.
    /// </summary>
    [SerializeField(0, "Index of the complaining authority.")]
    public int ComplainingAuthorityIndex { get; private set; }

    /// <summary>
    /// Involved authorities.
    /// </summary>
    [SerializeField(5, "Involved authorities.")]
    public Dictionary<int, Certificate> Authorities { get; private set; }

    /// <summary>
    /// Certificate storage.
    /// </summary>
    [SerializeField(1, "Certificate storage.")]
    public CertificateStorage CertificateStorage { get; private set; }

    /// <summary>
    /// Signed voting parameters.
    /// </summary>
    [SerializeField(2, "Signed voting parameters.")]
    public Signed<VotingParameters> SignedParameters { get; private set; }

    /// <summary>
    /// Voting parameters.
    /// </summary>
    public VotingParameters Parameters { get { return SignedParameters.Value; } }

    /// <summary>
    /// All share parts.
    /// </summary>
    [SerializeField(3, "All share parts.")]
    public AllShareParts AllShareParts { get; private set; }

    /// <summary>
    /// Trap doors.
    /// </summary>
    [SerializeField(4, "Trap doors.")]
    public Dictionary<int, TrapDoor> TrapDoors { get; private set; }

    public string FileName(Guid authorityId)
    {
      return string.Format("{0}-{1}.pi-shareproof", Parameters.VotingId.ToString(), authorityId.ToString());
    }

    public BadShareProof(int complainingAuthorityIndex, CertificateStorage certificateStorage, Signed<VotingParameters> signedParameters, AllShareParts allShareParts, IDictionary<int, TrapDoor> trapDoors, IDictionary<int, Certificate> authorities)
    {
      ComplainingAuthorityIndex = complainingAuthorityIndex;
      CertificateStorage = certificateStorage;
      SignedParameters = signedParameters;
      AllShareParts = allShareParts;
      TrapDoors = new Dictionary<int, TrapDoor>(trapDoors);
      Authorities = new Dictionary<int, Certificate>(authorities);
    }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(ComplainingAuthorityIndex);
      context.Write(CertificateStorage);
      context.Write(SignedParameters);
      context.Write(AllShareParts);
      context.WriteDictionary(TrapDoors);
      context.WriteDictionary(Authorities);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      ComplainingAuthorityIndex = context.ReadInt32();
      CertificateStorage = context.ReadObject<CertificateStorage>();
      SignedParameters = context.ReadObject<Signed<VotingParameters>>();
      AllShareParts = context.ReadObject<AllShareParts>();
      TrapDoors = context.ReadObjectDictionary<TrapDoor>();
      Authorities = context.ReadObjectDictionary<Certificate>();
    }

    public BadShareProof(DeserializeContext context)
      : base(context)
    { }
  }
}
