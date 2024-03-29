﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
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
  /// Share from one authority given to another.
  /// </summary>
  [SerializeObject("Share from one authority given to another.")]
  public class Share : Serializable
  {
    /// <summary>
    /// Value of share.
    /// </summary>
    [SerializeField(2, "Value of share.")]
    public BigInt Value { get; private set; }

    /// <summary>
    /// Index of issuing authority.
    /// </summary>
    [SerializeField(0, "Index of issuing authority.")]
    public int SourceAuthorityIndex { get; private set; }

    /// <summary>
    /// Index of receiving authority.
    /// </summary>
    [SerializeField(1, "Index of receiving authority.")]
    public int DestinationAuthorityIndex { get; private set; }

    public Share(BigInt value, int sourceAuthorityIndex, int destinationAuthorityIndex)
    {
      if (value == null)
        throw new ArgumentNullException("value");

      Value = value;
      SourceAuthorityIndex = sourceAuthorityIndex;
      DestinationAuthorityIndex = destinationAuthorityIndex;
    }

    public Share(DeserializeContext context, byte version)
      : base(context, version)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(SourceAuthorityIndex);
      context.Write(DestinationAuthorityIndex);
      context.Write(Value);
    }

    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      SourceAuthorityIndex = context.ReadInt32();
      DestinationAuthorityIndex = context.ReadInt32();
      Value = context.ReadBigInt();
    }

    /// <summary>
    /// Verify this share of the secret.
    /// </summary>
    /// <param name="authorityIndex">Index of the verifying authority.</param>
    /// <param name="parameters">Cryptographic parameters.</param>
    /// <param name="polynomial">Polynomial to verify against.</param>
    /// <param name="verificationValues">Verification values. Also known as A(i)(j).</param>
    /// <returns>Was share accepted?</returns>
    public bool Verify(int authorityIndex, BaseParameters parameters, List<VerificationValue> verificationValues)
    {
      if (verificationValues == null)
        throw new ArgumentNullException("verificationValues");
      if (verificationValues
        .Any(verificationValueList => verificationValueList == null))
        throw new ArgumentException("No verification value can be null.");
      if (verificationValues.Count != parameters.Thereshold + 1)
        throw new ArgumentException("Bad verificaton value count.");

      if (DestinationAuthorityIndex != authorityIndex)
        return false;

      BigInt GtoS = parameters.G.PowerMod(Value, parameters.P);
      BigInt aProduct = new BigInt(1);
      for (int k = 0; k <= parameters.Thereshold; k++)
      {
        VerificationValue verificationValue = verificationValues[k];
        if (verificationValue.SourceAuthorityIndex != SourceAuthorityIndex)
          return false;

        aProduct *= verificationValue.Value.PowerMod(new BigInt(authorityIndex).PowerMod(new BigInt(k), parameters.P), parameters.P);
        aProduct = aProduct.Mod(parameters.P);
      }

      return GtoS == aProduct;
    }
  }
}
