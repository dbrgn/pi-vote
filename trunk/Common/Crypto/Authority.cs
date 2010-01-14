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

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Cryptographic voting authority.
  /// </summary>
  public class Authority
  {
    /// <summary>
    /// Polynomial from the authority.
    /// To be dealt out to the others.
    /// </summary>
    private Polynomial f;

    /// <summary>
    /// Cryptographic parameters.
    /// </summary>
    private Parameters parameters;

    /// <summary>
    /// Share of total secret.
    /// </summary>
    /// <remarks>
    /// What the hell shall we do with this???
    /// </remarks>
    public BigInt x;

    /// <summary>
    /// Index of this authority.
    /// </summary>
    public int Index { get; private set; }

    /// <summary>
    /// Create a new authority.
    /// </summary>
    /// <param name="index">Index of new authority. Must be at least one.</param>
    /// <param name="parameters">Cryptographic parameters.</param>
    public Authority(int index, Parameters parameters)
    {
      this.Index = index;
      this.parameters = parameters;
    }

    /// <summary>
    /// Create one's own polynomial.
    /// </summary>
    /// <param name="degree">Degress of the polynomial. Equal to the cryptographic thereshold.</param>
    public void CreatePolynomial(int degree)
    {
      this.f = new Polynomial();

      while (this.f.Degree < degree)
      {
        this.f.AddCoefficient(this.parameters.Random());
      }
    }

    /// <summary>
    /// Verification value to be published.
    /// </summary>
    /// <param name="index">Index of verification value.</param>
    /// <returns>Verification value.</returns>
    public BigInt A(int index)
    {
      return this.parameters.G.PowerMod(this.f.GetCoefficient(index), this.parameters.P);
    }

    /// <summary>
    /// Private key part.
    /// </summary>
    /// <remarks>
    /// Remove this before release!
    /// </remarks>
    public BigInt Z
    {
      get
      {
        return this.f.GetCoefficient(0);
      }
    }

    /// <summary>
    /// Public key part.
    /// </summary>
    public BigInt Y
    {
      get
      {
        return A(0);
      }
    }

    /// <summary>
    /// Share to hand out to authority by authorithyIndex.
    /// </summary>
    /// <param name="authorithyIndex">Index of the authority for which this share is intended.</param>
    /// <returns>A share of the secret.</returns>
    public BigInt S(int authorithyIndex)
    {
      return this.f.Evaluate(new BigInt(authorithyIndex), this.parameters.P);
    }

    public BigInt PartialDecipher(Vote vote, int multiply, int divide)
    {
      return vote.HalfKey.PowerMod(this.x * 3 * 4 * multiply / divide, this.parameters.P);
    }

    /// <summary>
    /// Verify the sharing of the secret.
    /// </summary>
    /// <param name="shares">Shares from all authorities.</param>
    /// <param name="As">Verification values.</param>
    public void VerifySharing(List<BigInt> shares, List<List<BigInt>> As)
    {
      for (int index = 0; index < shares.Count; index++)
      {
        BigInt GtoS = this.parameters.G.PowerMod(shares[index], this.parameters.P);
        BigInt aProduct = new BigInt(1);
        for (int k = 0; k <= this.f.Degree; k++)
        {
          BigInt A = As[index][k];
          aProduct *= A.PowerMod(new BigInt(Index).PowerMod(new BigInt(k), this.parameters.P), this.parameters.P);
          aProduct = aProduct.Mod(this.parameters.P);
        }

        if (GtoS != aProduct)
        {
          throw new Exception("FAIL");
        }
      }

      this.x = new BigInt(0);
      foreach (BigInt share in shares)
      {
        this.x += share;
      }
    }
  }
}
