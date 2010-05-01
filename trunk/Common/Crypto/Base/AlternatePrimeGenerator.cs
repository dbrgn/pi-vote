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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  public class PrimeFile
  {
    private List<uint> primes;
    private string fileName;

    public PrimeFile(string fileName)
    {
      this.fileName = fileName;
    }

    public void Load()
    {
      FileStream file = new FileStream(this.fileName, FileMode.Open, FileAccess.Read);
      DeserializeContext context = new DeserializeContext(file);
      int count = (int)file.Length / sizeof(uint);
      this.primes = new List<uint>();

      for (int index = 0; index <= count; index++)
      {
        this.primes.Add(context.ReadUInt32());
      }

      context.Close();
      file.Close();
    }

    public void Save()
    {
      FileStream file = new FileStream(this.fileName, FileMode.Create, FileAccess.Write);
      SerializeContext context = new SerializeContext(file);

      foreach (uint prime in this.primes)
      {
        context.Write(prime);
      }

      context.Close();
      file.Close();
    }

    public void Generate()
    {
      this.primes = new List<uint>();
      BigInt prime = 2;
      DateTime startTime = DateTime.Now;
      DateTime lastTime = DateTime.Now;

      while (prime.BitLength <= 32)
      {
        this.primes.Add((uint)prime);
        prime = prime.NextPrimeGMP();

        if (DateTime.Now.Subtract(lastTime).TotalSeconds > 1d)
        {
          lastTime = DateTime.Now;
          double percent = 100d / (double)uint.MaxValue * (double)(uint)prime;
          double perSecond = (double)(uint)prime / DateTime.Now.Subtract(startTime).TotalSeconds;
          double todo = (double)(uint.MaxValue - (uint)prime);
          double seconds = todo / perSecond;
          TimeSpan estimated = startTime.AddSeconds(seconds).Subtract(startTime);

          Console.WriteLine("{0:0.00000}% {1:0.00}p/s {2}", percent, perSecond, estimated);
        }
      }
    }

    public int Count
    {
      get { return this.primes.Count; }
    }
  }

  public class AlternatePrimeGenerator : IPrimeGenerator
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
      //if (File.Exists("prime")) this.prime = new BigInt(File.ReadAllBytes("prime"));
      //if (File.Exists("safeprime")) this.safePrime = new BigInt(File.ReadAllBytes("safeprime"));

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
