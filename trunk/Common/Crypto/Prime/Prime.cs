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
using System.Security.Cryptography;
using Emil.GMP;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Finds prime numbers.
  /// </summary>
  public static class Prime
  {
    /// <summary>
    /// Low number of rabin-miller tests to perform.
    /// </summary>
    /// <remarks>
    /// Using low number of rabin-miller tests to find safe prime
    /// and high number of tests to be quite certain of primality.
    /// </remarks>
    private const int LowRabinMillerCount = 2;

    /// <summary>
    /// High number of rabin-miller test to perform.
    /// </summary>
    private const int HighRabinMillerCount = 128;

    /// <summary>
    /// Create only one random number generator.
    /// </summary>
    private static readonly RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();

    /// <summary>
    /// Create a random number.
    /// </summary>
    /// <param name="bitLength">Length in bits. Is rounded up to next byte length.</param>
    /// <returns>New random number.</returns>
    public static BigInt RandomNumber(int bitLength)
    {
      if (bitLength < 1)
        throw new ArgumentException("Bit length must be at least 1.");

      int byteLength = (bitLength - 1) / 8 + 1;
      byte[] data = new byte[byteLength];
      randomNumberGenerator.GetBytes(data);
      return new BigInt(data);
    }

    /// <summary>
    /// Is the number a prime?
    /// </summary>
    /// <param name="value">Number to test.</param>
    /// <returns>Is it a prime?</returns>
    public static bool IsPrime(BigInt number)
    {
      return number.IsProbablyPrimeRabinMiller(HighRabinMillerCount);
    }
  }
}
