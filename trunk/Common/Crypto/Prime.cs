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
  public static class Prime
  {
    /// <summary>
    /// Finds a prime number.
    /// </summary>
    /// <param name="bitLength">Length in bits. Is rounded up to next byte length.</param>
    /// <returns>New prime number.</returns>
    public static BigInt Find(int bitLength)
    {
      BigInt number = RandomNumber(bitLength);

      while (!number.IsProbablyPrimeRabinMiller(1000))
      {
        number = RandomNumber(bitLength);
      }

      return number;
    }

    /// <summary>
    /// Finds a safe prime number.
    /// </summary>
    /// <param name="bitLength">Length in bits. Is rounded up to next byte length.</param>
    /// <returns>New safe prime number.</returns>
    public static BigInt FindSafe(int bitLength)
    {
      BigInt number = RandomNumber(bitLength);

      while (!number.IsProbablyPrimeRabinMiller(1000) ||
             !(2 * number + 1).IsProbablyPrimeRabinMiller(1000))
      {
        number = RandomNumber(bitLength);
      }

      return number;
    }

    /// <summary>
    /// Create a random number.
    /// </summary>
    /// <param name="bitLength">Length in bits. Is rounded up to next byte length.</param>
    /// <returns>New random number.</returns>
    private static BigInt RandomNumber(int bitLength)
    {
      int byteLength = (bitLength - 1) / 8 + 1;
      byte[] data = new byte[byteLength];
      RandomNumberGenerator.Create().GetBytes(data);
      return new BigInt(data);
    }
  }
}
