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
using Emil.GMP;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Share from one authority given to another.
  /// </summary>
  public class Share : Serializable
  {
    /// <summary>
    /// Value of share.
    /// </summary>
    public BigInt Value { get; private set; }

    /// <summary>
    /// Index of issuing authority.
    /// </summary>
    public int SourceAuthorityIndex { get; private set; }

    /// <summary>
    /// Index of receiving authority.
    /// </summary>
    public int DestinationAuthorityIndex { get; private set; }

    public Share(BigInt value, int sourceAuthorityIndex, int destinationAuthorityIndex)
    {
      if (value == null)
        throw new ArgumentNullException("value");

      Value = value;
      SourceAuthorityIndex = sourceAuthorityIndex;
      DestinationAuthorityIndex = destinationAuthorityIndex;
    }

    public Share(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(SourceAuthorityIndex);
      context.Write(DestinationAuthorityIndex);
      context.Write(Value);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
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
    public bool Verify(int authorityIndex, Parameters parameters, List<VerificationValue> verificationValues)
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
