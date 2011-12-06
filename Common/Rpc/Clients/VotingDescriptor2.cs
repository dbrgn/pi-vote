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
using System.Threading;
using System.Net;
using System.IO;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// Voting descriptor.
  /// </summary>
  public class VotingDescriptor2 : VotingDescriptor
  {
    private readonly List<Certificate> authorities;
    private readonly VotingMaterial material;

    /// <summary>
    /// List of authorities that participate in this voting.
    /// Null if not applicable.
    /// </summary>
    public IEnumerable<Certificate> Authorities { get { return this.authorities; } }

    /// <summary>
    /// Voting material including signed parameters.
    /// </summary>
    public VotingMaterial Material { get { return this.material; } }

    /// <summary>
    /// Creates a new voting descriptor from offline files.
    /// </summary>
    /// <param name="offlinePath">Path to the offline files.</param>
    public VotingDescriptor2(string offlinePath)
      : base(offlinePath)
    {
      string materialFileName = Path.Combine(offlinePath, Files.VotingMaterialFileName);

      if (!File.Exists(materialFileName))
        throw new ArgumentException("Offline voting material file not found.");

      this.material = Serializable.Load<VotingMaterial>(materialFileName);
      this.authorities = new List<Certificate>();

      for (int authorityIndex = 1; authorityIndex < this.material.Parameters.Value.AuthorityCount + 1; authorityIndex++)
      {
        string partialDecipherFileName = Path.Combine(offlinePath, string.Format(Files.PartialDecipherFileString, authorityIndex));

        if (File.Exists(partialDecipherFileName))
        {
          var partialDecipher = Serializable.Load<Signed<PartialDecipherList>>(partialDecipherFileName);

          this.authorities.Add(partialDecipher.Certificate);
        }
      }
    }

    /// <summary>
    /// Create a new voting descriptor.
    /// </summary>
    /// <param name="container">Container with all information about this voting.</param>
    public VotingDescriptor2(VotingContainer container)
      : base(container.Material.Parameters.Value, container.Status, container.AuthoritiesDone, container.EnvelopeCount)
    {
      this.material = container.Material;
      this.authorities = container.Authorities;
    }
  }
}
