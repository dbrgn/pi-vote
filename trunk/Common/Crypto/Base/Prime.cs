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
using System.IO;
using System.Security.Cryptography;
using Emil.GMP;
using Pirate.PiVote.Serialization;

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
    /// Finds a prime number.
    /// </summary>
    /// <param name="bitLength">Length in bits. Is rounded up to next byte length.</param>
    /// <returns>New prime number.</returns>
    public static BigInt Find(int bitLength)
    {
      if (bitLength < 1)
        throw new ArgumentException("Bit length must be at least 1.");

      BigInt number = RandomNumber(bitLength);

      while (!number.IsProbablyPrimeRabinMiller(HighRabinMillerCount))
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
      if (bitLength < 1)
        throw new ArgumentException("Bit length must be at least 1.");

      BigInt number = RandomNumber(bitLength);
      BigInt safeNumber = 2 * number + 1;

      //Using low number of rabin-miller tests to find safe prime
      //and high number of tests to be quite certain of primality.
      while (!number.IsProbablyPrimeRabinMiller(LowRabinMillerCount) ||
             !safeNumber.IsProbablyPrimeRabinMiller(LowRabinMillerCount) ||
             !number.IsProbablyPrimeRabinMiller(HighRabinMillerCount) ||
             !safeNumber.IsProbablyPrimeRabinMiller(HighRabinMillerCount))
      {
        number = RandomNumber(bitLength);
        safeNumber = 2 * number + 1;
      }

      return safeNumber;
    }

    /// <summary>
    /// Finds both as prime and a larger safe prime.
    /// </summary>
    /// <param name="bitLength">Length in bits of the prime.</param>
    /// <param name="prime">Random prime number.</param>
    /// <param name="safePrime">Random safe prime number.</param>
    public static void FindPrimeAndSafePrimeThreaded(int bitLength, out BigInt prime, out BigInt safePrime)
    {
      ThreadedPrimeGenerator generator = new ThreadedPrimeGenerator();
      generator.FindPrimeAndSafePrime(bitLength, out prime, out safePrime);
    }

    /// <summary>
    /// Finds both as prime and a larger safe prime.
    /// </summary>
    /// <param name="bitLength">Length in bits of the prime.</param>
    /// <param name="prime">Random prime number.</param>
    /// <param name="safePrime">Random safe prime number.</param>
    public static void FindPrimeAndSafePrime(int bitLength, out BigInt prime, out BigInt safePrime)
    {
      //Begin search as a random position.
      BigInt number = RandomNumber(bitLength);

      //Make it odd.
      if (number.IsEven)
        number++;

      //Seek from there.
      while (!number.IsProbablyPrimeRabinMiller(HighRabinMillerCount))
      {
        number += 2;
      }

      prime = number;

      //Add a small random offset.
      number += RandomNumber(bitLength / 2);

      //Make it odd.
      if (number.IsEven)
        number++;

      //Set safe number accordingly.
      BigInt safeNumber = 2 * number + 1;

      //Seek from there.
      while (!number.IsProbablyPrimeRabinMiller(LowRabinMillerCount) ||
             !safeNumber.IsProbablyPrimeRabinMiller(HighRabinMillerCount) ||
             !number.IsProbablyPrimeRabinMiller(HighRabinMillerCount))
      {
        number += 2;
        safeNumber = 2 * number + 1;
      }

      safePrime = safeNumber;
    }

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
      BigInt number = new BigInt(data);

      while (!HasSufficientLength(number, bitLength))
      {
        randomNumberGenerator.GetBytes(data);
        number = new BigInt(data);
      }

      return number;
    }

    /// <summary>
    /// Determines if a number has a sufficient bit length.
    /// </summary>
    /// <param name="number">Number to be tested.</param>
    /// <param name="bitLength">Optimal bit length.</param>
    /// <returns>Has number sufficient bit length.</returns>
    public static bool HasSufficientLength(BigInt number, int bitLength)
    {
      return number.BitLength >= bitLength - 4;
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

    /// <summary>
    /// Is the number a safe prime?
    /// </summary>
    /// <param name="value">Number to test.</param>
    /// <returns>Is it a safe prime?</returns>
    public static bool IsSafePrime(BigInt number)
    {
      return number.IsProbablyPrimeRabinMiller(HighRabinMillerCount) &&
             ((number - 1) / 2).IsProbablyPrimeRabinMiller(HighRabinMillerCount);
    }

    /// <summary>
    /// Tries to load a pregenerated safe prime.
    /// </summary>
    /// <param name="dataPath">Path where application data is stored.</param>
    /// <param name="bitLength">Length of the prime.</param>
    /// <param name="prime">Prime loaded.</param>
    /// <param name="safePrime">Safe prime loaded.</param>
    /// <returns>Was a prime and safe prime found?</returns>
    public static bool TryLoadPregeneratedSafePrime(string dataPath, int bitLength, out BigInt prime, out BigInt safePrime)
    {
      var dataDirectory = new DirectoryInfo(dataPath);
      var files = dataDirectory.GetFiles(Files.SafePrimePattern);

      foreach (var file in files)
      {
        try
        {
          FileStream fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
          DeserializeContext context = new DeserializeContext(fileStream);

          prime = context.ReadBigInt();
          safePrime = context.ReadBigInt();

          context.Close();
          fileStream.Close();

          file.Delete();

          if (Prime.HasSufficientLength(prime, bitLength) &&
              Prime.HasSufficientLength(safePrime, bitLength) &&
              Prime.IsPrime(prime) &&
              Prime.IsSafePrime(safePrime))
          {
            return true;
          }
        }
        catch
        {
          file.Delete();
        }
      }

      prime = null;
      safePrime = null;
      return false;
    }

    /// <summary>
    /// Generates and stores prime and safe prime.
    /// </summary>
    /// <param name="dataPath">Path where application data is stored.</param>
    public static void GenerateAndStoreSafePrime(string dataPath)
    {

      BigInt prime = null;
      BigInt safePrime = null;

      Prime.FindPrimeAndSafePrimeThreaded(4096, out prime, out safePrime);

      string fileName = Path.Combine(dataPath, string.Format(Files.SafePrimeFileString, DateTime.Now.Ticks));
      FileStream file = new FileStream(fileName, FileMode.Create, FileAccess.Write);
      SerializeContext context = new SerializeContext(file);

      context.Write(prime);
      context.Write(safePrime);

      context.Close();
      file.Close();
    }
  }
}
