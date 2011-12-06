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
  /// List of all authorities in the voting procedure.
  /// </summary>
  [SerializeObject("List of all authorities in the voting procedure.")]
  public class AuthorityList : Serializable
  {
    /// <summary>
    /// Id of the voting procedure.
    /// </summary>
    [SerializeField(0, "Id of the voting procedure.")]
    public int VotingId { get; private set; }

    /// <summary>
    /// List of all authorities in the voting procedure.
    /// </summary>
    [SerializeField(1, "List of all authorities in the voting procedure.")]
    public List<Certificate> Authorities { get; private set; }

    /// <summary>
    /// Intermediate certificates.
    /// </summary>
    [SerializeField(2, "Intermediate certificates.")]
    public List<Certificate> Certificates { get; private set; }

    /// <summary>
    /// Certificate revocation list for CAs.
    /// </summary>
    [SerializeField(3, "Certificate revocation list for CAs.")]
    public List<Signed<RevocationList>> RevocationLists { get; private set; }

    /// <summary>
    /// Creates a new list of authorities for a voting procedure.
    /// </summary>
    /// <param name="votingId">Id of the voting procedure.</param>
    /// <param name="authorities">List of authorities.</param>
    public AuthorityList(Guid votingId, IEnumerable<Certificate> authorities, IEnumerable<Certificate> certificates, IEnumerable<Signed<RevocationList>> revocationLists)
    {
      if (authorities == null)
        throw new ArgumentNullException("authorities");

      Authorities = new List<Certificate>(authorities);
      Certificates = new List<Certificate>(certificates);
      RevocationLists = new List<Signed<RevocationList>>(revocationLists);
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public AuthorityList(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(VotingId);
      context.WriteList(Authorities);
      context.WriteList(Certificates);
      context.WriteList(RevocationLists);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      VotingId = context.ReadInt32();
      Authorities = context.ReadObjectList<Certificate>();
      Certificates = context.ReadObjectList<Certificate>();
      RevocationLists = context.ReadObjectList<Signed<RevocationList>>();
    }
  }
}
