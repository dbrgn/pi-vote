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
using System.Threading;
using Emil.GMP;

namespace Pirate.PiVote.Crypto
{
  public class ThreadedPrimeGenerator
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
    /// Lengths in bits of the numbers.
    /// </summary>
    private int bitLength;

    /// <summary>
    /// The found prime.
    /// </summary>
    private BigInt prime;
    
    /// <summary>
    /// The found safe prime.
    /// </summary>
    private BigInt safePrime;

    /// <summary>
    /// Finds both a prime and a larger safe prime.
    /// </summary>
    /// <param name="bitLength">Length in bits for the numbers.</param>
    /// <param name="prime">Returns the prime.</param>
    /// <param name="safePrime">Returns the safe prime.</param>
    public void FindPrimeAndSafePrime(int bitLength, out BigInt prime, out BigInt safePrime)
    {
      this.bitLength = bitLength;

      List<Thread> threads = new List<Thread>();
      Environment.ProcessorCount.Times(() => threads.Add(new Thread(FindPrime)));
      threads.ForEach(thread => thread.Start());
      threads.ForEach(thread => thread.Join());

      threads = new List<Thread>();
      Environment.ProcessorCount.Times(() => threads.Add(new Thread(FindSafePrime)));
      threads.ForEach(thread => thread.Start());
      threads.ForEach(thread => thread.Join());

      prime = this.prime;
      safePrime = this.safePrime;
    }

    /// <summary>
    /// Thread work to find a prime.
    /// </summary>
    private void FindPrime()
    {
      BigInt number = Prime.RandomNumber(bitLength);

      while (this.prime == null)
      {
        number = number.NextPrimeGMP();

        if (this.prime == null && number.IsProbablyPrimeRabinMiller(HighRabinMillerCount))
        {
          this.prime = number;
        }
      }
    }

    /// <summary>
    /// Thread work to find a safe prime.
    /// </summary>
    private void FindSafePrime()
    {
      BigInt number = this.prime + Prime.RandomNumber(bitLength);
      BigInt safeNumber;

      while (this.safePrime == null)
      {
        number = number.NextPrimeGMP();
        safeNumber = number * 2 + 1;

        if (this.safePrime == null && safeNumber.IsProbablyPrimeRabinMiller(LowRabinMillerCount))
        {
          if (this.safePrime == null && number.IsProbablyPrimeRabinMiller(HighRabinMillerCount))
          {
            if (this.safePrime == null && safeNumber.IsProbablyPrimeRabinMiller(HighRabinMillerCount))
            {
              this.safePrime = safeNumber;
            }
          }
        }
      }
    }
  }
}
