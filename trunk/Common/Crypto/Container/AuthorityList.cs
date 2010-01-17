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
  /// List of all authorities in the voting procedure.
  /// </summary>
  public class AuthorityList : Serializable
  {
    /// <summary>
    /// Id of the voting procedure.
    /// </summary>
    public int VotingId { get; private set; }

    /// <summary>
    /// List of all authorities in the voting procedure.
    /// </summary>
    public List<Certificate> Authorities { get; private set; }

    /// <summary>
    /// Creates a new list of authorities for a voting procedure.
    /// </summary>
    /// <param name="votingId">Id of the voting procedure.</param>
    /// <param name="authorities">List of authorities.</param>
    public AuthorityList(int votingId, IEnumerable<Certificate> authorities)
    {
      if (authorities == null)
        throw new ArgumentNullException("authorities");

      Authorities = new List<Certificate>(authorities);
    }

    public AuthorityList(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(VotingId);
      context.WriteList(Authorities);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingId = context.ReadInt32();
      Authorities = context.ReadObjectList<Certificate>();
    }
  }
}
