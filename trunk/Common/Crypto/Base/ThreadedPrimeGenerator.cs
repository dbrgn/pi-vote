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
    private const int LowRabinMillerCount = 10;

    /// <summary>
    /// High number of rabin-miller test to perform.
    /// </summary>
    private const int HighRabinMillerCount = 1000;

    private class Locker { }

    private int offset;
    private int step;
    private int bitLength;
    private BigInt base0;
    private BigInt base1;
    private BigInt prime;
    private BigInt safePrime;
    private Locker locker;
    private List<Thread> threads;

    public void FindPrimeAndSafePrime(int bitLength, out BigInt prime, out BigInt safePrime)
    {
      this.offset = 0;
      this.step = Environment.ProcessorCount;
      this.locker = new Locker();
      this.bitLength = bitLength;

      this.threads = new List<Thread>();
      for (int index = 0; index < Environment.ProcessorCount; index++)
      {
        Thread thread = new Thread(Find);
        thread.Start();
        this.threads.Add(thread);
      }

      foreach (Thread thread in this.threads)
      {
        thread.Join();
      }

      prime = this.prime;
      safePrime = this.safePrime;
    }

    private void Find()
    {
      int offset = 0;

      lock (locker)
      {
        offset = this.offset;
        this.offset++;
      }

      lock (locker)
      {
        if (this.base0 == null)
        {
          this.base0 = Prime.RandomNumber(this.bitLength);
          if (this.base0.IsEven)
            this.base0++;
        }
      }

      BigInt number = this.base0 + (2 * offset);

      while (!number.IsProbablyPrimeRabinMiller(HighRabinMillerCount) && this.prime == null)
      {
        number += (2 * this.step);
      }

      lock (this.locker)
      {
        if (number.IsProbablyPrimeRabinMiller(HighRabinMillerCount) && this.prime == null)
        {
          this.prime = number;
        }
      }

      lock (locker)
      {
        if (this.base1 == null)
        {
          this.base1 = number + Prime.RandomNumber(this.bitLength / 2);
          if (this.base1.IsEven)
            this.base1++;
        }
      }

      BigInt safeNumber = 2 * number + 1;

      while ((!number.IsProbablyPrimeRabinMiller(LowRabinMillerCount) ||
             !safeNumber.IsProbablyPrimeRabinMiller(HighRabinMillerCount) ||
             !number.IsProbablyPrimeRabinMiller(HighRabinMillerCount)) &&
             this.safePrime == null)
      {
        number += (2 * this.step);
        safeNumber = 2 * number + 1;
      }

      lock (this.locker)
      {
        if (number.IsProbablyPrimeRabinMiller(LowRabinMillerCount) &&
            safeNumber.IsProbablyPrimeRabinMiller(HighRabinMillerCount) &&
            number.IsProbablyPrimeRabinMiller(HighRabinMillerCount) &&
            this.safePrime == null)
        {
          this.safePrime = safeNumber;
        }
      }
    }
  }
}
