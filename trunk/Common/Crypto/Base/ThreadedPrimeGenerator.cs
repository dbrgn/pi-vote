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
using System.IO;
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

    private int tested;
    private int safePrimes;

    private void PrimeTest()
    {
      Console.WriteLine();
      Console.WriteLine();

      this.tested = 0;
      this.safePrimes = 0;
      DateTime start = DateTime.Now;
      DateTime last = DateTime.Now;

      Environment.ProcessorCount.Times(() => new Thread(PrimeTestThread).Start());

      while (true)
      {
        if (DateTime.Now.Subtract(last).TotalSeconds > 5)
        {
          last = DateTime.Now;
          string since = DateTime.Now.Subtract(start).ToString();
          double safePrimePercent = 100d / (double)this.tested * (double)this.safePrimes;
          double safePrimePerSecond = (double)this.safePrimes / DateTime.Now.Subtract(start).TotalSeconds;
          Console.WriteLine("{0}: {1,10} / {2,10} = {3:0.00000}%, {4:0.00000}", since, this.safePrimes, this.tested, safePrimePercent, safePrimePerSecond);
        }
      }
    }

    private void PrimeTestThread()
    {
      BigInt number = Prime.RandomNumber(2048);
      BigInt safeNumber;

      while (true)
      {
        //number = Prime.RandomNumber(64).NextPrimeGMP();
        number = number.NextPrimeGMP();
        safeNumber = number * 2 + 1;
        Interlocked.Increment(ref this.tested);

        if (safeNumber.IsProbablyPrimeRabinMiller(1))
        {
          if (number.IsProbablyPrimeRabinMiller(64))
          {
            if (safeNumber.IsProbablyPrimeRabinMiller(64))
            {
              Interlocked.Increment(ref this.safePrimes);
            }
          }
        }
      }
    }

    /// <summary>
    /// Finds both a prime and a larger safe prime.
    /// </summary>
    /// <param name="bitLength">Length in bits for the numbers.</param>
    /// <param name="prime">Returns the prime.</param>
    /// <param name="safePrime">Returns the safe prime.</param>
    public void FindPrimeAndSafePrime(int bitLength, out BigInt prime, out BigInt safePrime)
    {
      if (File.Exists("prime")) this.prime = new BigInt(File.ReadAllBytes("prime"));
      if (File.Exists("safeprime")) this.safePrime = new BigInt(File.ReadAllBytes("safeprime"));

      DateTime start = DateTime.Now;

      this.bitLength = bitLength;

      List<Thread> threads = new List<Thread>();
      Environment.ProcessorCount.Times(() => threads.Add(new Thread(FindPrime)));
      threads.ForEach(thread => thread.Start());
      while (this.prime == null) { Thread.Sleep(100); }
      threads.ForEach(thread => thread.Join());

      threads = new List<Thread>();
      Environment.ProcessorCount.Times(() => threads.Add(new Thread(FindSafePrime)));
      threads.ForEach(thread => thread.Start());
      while (this.safePrime == null) { Thread.Sleep(100); }
      threads.ForEach(thread => thread.Join());

      prime = this.prime;
      safePrime = this.safePrime;

      Console.WriteLine(DateTime.Now.Subtract(start).ToString());

      File.WriteAllBytes("prime", this.prime.ToByteArray());
      File.WriteAllBytes("safeprime", this.safePrime.ToByteArray());
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
