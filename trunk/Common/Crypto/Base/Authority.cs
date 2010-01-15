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
    /// <remarks>
    /// Known as f_i for authority i.
    /// </remarks>
    private Polynomial polynomial;

    /// <summary>
    /// Cryptographic parameters.
    /// </summary>
    private Parameters parameters;

    /// <summary>
    /// Part of total secret.
    /// </summary>
    /// <remarks>
    /// To be linarly combined with the other shares.
    /// Known as x(i) for authority i.
    /// </remarks>
    private BigInt secretKeyPart;

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
    public void CreatePolynomial()
    {
      if (this.polynomial != null)
        throw new InvalidOperationException("Polynom already created.");

      this.polynomial = new Polynomial();

      while (this.polynomial.Degree < this.parameters.Thereshold)
      {
        this.polynomial.AddCoefficient(this.parameters.Random());
      }
    }

    /// <summary>
    /// Verification value to be published.
    /// </summary>
    /// <remarks>
    /// Known as A(i,j) for authority i and value j.
    /// </remarks>
    /// <param name="index">Index of verification value.</param>
    /// <returns>Verification value.</returns>
    public VerificationValue VerificationValue(int index)
    {
      if (this.polynomial == null)
        throw new InvalidOperationException("Polynom not yet created.");
      if (index < 0 || index > this.polynomial.Degree)
        throw new ArgumentException("Coefficient index out of range.");

      BigInt value = this.parameters.G.PowerMod(this.polynomial.GetCoefficient(index), this.parameters.P);

      return new VerificationValue(value, Index);
    }

    /// <summary>
    /// Public key part.
    /// </summary>
    /// <remarks>
    /// Known as Y(i) for authority i.
    /// </remarks>
    public BigInt PublicKeyPart
    {
      get
      {
        return VerificationValue(0).Value;
      }
    }

    /// <summary>
    /// Share to hand out to authority by authorithyIndex.
    /// </summary>
    /// <remarks>
    /// Known as S(i,j) for authorities i and j.
    /// </remarks>
    /// <param name="authorithyIndex">Index of the authority for which this share is intended.</param>
    /// <returns>A share of the secret.</returns>
    public Share Share(int authorithyIndex)
    {
      BigInt value = this.polynomial.Evaluate(new BigInt(authorithyIndex), this.parameters.P);
      return new Share(value, Index, authorithyIndex);
    }

    /// <summary>
    /// Creates all partial deciphers for a vote.
    /// </summary>
    /// <remarks>
    /// Partial deciphers from t+1 authority with the same index are
    /// to be combined to fully decipher the vote.
    /// </remarks>
    /// <param name="vote">Vote to partially decipher.</param>
    /// <returns>List of partial deciphers.</returns>
    public IEnumerable<PartialDecipher> PartialDeciphers(Vote vote, int optionIndex)
    {
      List<PartialDecipher> partialDeciphers = new List<PartialDecipher>();

      //The magic numbers for multiply and divide listed here for
      //each authority index and partial decipher group index
      //are computed to provide linear combinations of the partial
      //decipher that yield a full decipher.
      //
      //Example for 5 authorities and threshold 3.
      //f1-f5   polynoms of authorities 1-5
      //
      //each autority j has shares f1(j) to f5(j) which expand to
      //a polynomial of degree 3, namely f1(j) = a0+a1*j+a2*j^2+a3*j^3
      //these shares are summed as x(j) = f1(j)+f2(j)+f3(j)+f4(j)+f5(j)
      //
      //these are then linarly combined to the secret key:
      //x(1)*4 + x(2)*-6 + x(3)*4 + x(4)*-1 = ... = a0(1)+a0(2)+a0(3)+a(4)+a(5)

      switch (Index)
      { 
        case 1:
          partialDeciphers.Add(new PartialDecipher(2, optionIndex, PartialDecipher(vote, 5, 2)));
          partialDeciphers.Add(new PartialDecipher(3, optionIndex, PartialDecipher(vote, 10, 3)));
          partialDeciphers.Add(new PartialDecipher(4, optionIndex, PartialDecipher(vote, 15, 4)));
          partialDeciphers.Add(new PartialDecipher(5, optionIndex, PartialDecipher(vote, 4, 1)));
          break;
        case 2:
          partialDeciphers.Add(new PartialDecipher(1, optionIndex, PartialDecipher(vote, 10, 1)));
          partialDeciphers.Add(new PartialDecipher(3, optionIndex, PartialDecipher(vote, -10, 3)));
          partialDeciphers.Add(new PartialDecipher(4, optionIndex, PartialDecipher(vote, -5, 1)));
          partialDeciphers.Add(new PartialDecipher(5, optionIndex, PartialDecipher(vote, -6, 1)));
          break;
        case 3:
          partialDeciphers.Add(new PartialDecipher(1, optionIndex, PartialDecipher(vote, -20, 1)));
          partialDeciphers.Add(new PartialDecipher(2, optionIndex, PartialDecipher(vote, -5, 1)));
          partialDeciphers.Add(new PartialDecipher(4, optionIndex, PartialDecipher(vote, 5, 2)));
          partialDeciphers.Add(new PartialDecipher(5, optionIndex, PartialDecipher(vote, 4, 1)));
          break;
        case 4:
          partialDeciphers.Add(new PartialDecipher(1, optionIndex, PartialDecipher(vote, 15, 1)));
          partialDeciphers.Add(new PartialDecipher(2, optionIndex, PartialDecipher(vote, 5, 1)));
          partialDeciphers.Add(new PartialDecipher(3, optionIndex, PartialDecipher(vote, 5, 3)));
          partialDeciphers.Add(new PartialDecipher(5, optionIndex, PartialDecipher(vote, -1, 1)));
          break;
        case 5:
          partialDeciphers.Add(new PartialDecipher(1, optionIndex, PartialDecipher(vote, -4, 1)));
          partialDeciphers.Add(new PartialDecipher(2, optionIndex, PartialDecipher(vote, -3, 2)));
          partialDeciphers.Add(new PartialDecipher(3, optionIndex, PartialDecipher(vote, -2, 3)));
          partialDeciphers.Add(new PartialDecipher(4, optionIndex, PartialDecipher(vote, -1, 4)));
          break;
        default:
          throw new InvalidOperationException("Bad authority index.");
      }

      return partialDeciphers;
    }

    /// <summary>
    /// Partially deciphers a vote.
    /// </summary>
    /// <remarks>
    /// Observes multiply/divide for linear combination with other
    /// partial deciphers.
    /// </remarks>
    /// <param name="vote">Vote to partially decipher.</param>
    /// <param name="multiply">Linear combination multiplicator.</param>
    /// <param name="divide">Linear combination dividend.</param>
    /// <returns>Partial decipher value.</returns>
    private BigInt PartialDecipher(Vote vote, int multiply, int divide)
    {
      //The 12 magic number is inserted to avoid division remainders when
      //dividing partial deciphers for linear combinations by 2, 3 and 4.
      return vote.HalfKey.PowerMod(this.secretKeyPart * 12 * multiply / divide, this.parameters.P);
    }

    /// <summary>
    /// Verify the sharing of the secret.
    /// </summary>
    /// <param name="shares">Shares from all authorities.</param>
    /// <param name="As">Verification values.</param>
    /// <returns>Was sharing accepted?</returns>
    public bool VerifySharing(List<Share> shares, List<List<VerificationValue>> As)
    {
      for (int index = 0; index < shares.Count; index++)
      {
        //Check each part of the sharing.
        Share share = shares[index];
        if (share.DestinationAuthorityIndex != Index)
          return false;

        BigInt GtoS = this.parameters.G.PowerMod(shares[index].Value, this.parameters.P);
        BigInt aProduct = new BigInt(1);
        for (int k = 0; k <= this.polynomial.Degree; k++)
        {
          VerificationValue verificationValue = As[index][k];
          if (verificationValue.SourceAuthorityIndex != share.SourceAuthorityIndex)
            return false;

          aProduct *= verificationValue.Value.PowerMod(new BigInt(Index).PowerMod(new BigInt(k), this.parameters.P), this.parameters.P);
          aProduct = aProduct.Mod(this.parameters.P);
        }

        if (GtoS != aProduct)
          return false;
      }

      //Compute this authority's secret.
      this.secretKeyPart = new BigInt(0);
      foreach (Share share in shares)
      {
        this.secretKeyPart += share.Value;
      }

      return true;
    }
  }
}
