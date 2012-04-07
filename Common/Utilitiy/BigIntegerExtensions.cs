using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Math;

namespace Pirate.PiVote.Utilitiy
{
  public static class BigIntegerExtensions
  {
    public static BigInteger Mod(this BigInteger a, BigInteger b)
    {
      return a.ModPow(1, b);
    }

    public static BigInteger PowerMod(this BigInteger a, BigInteger b, BigInteger c)
    {
      return a.ModPow(b, c);
    }

    public static BigInteger Power(this BigInteger a, BigInteger b)
    {
      BigInteger value = 1;

      for (BigInteger count = 0; count < b; count += 1)
      {
        value *= a;
      }

      return value;
    }
  }
}
