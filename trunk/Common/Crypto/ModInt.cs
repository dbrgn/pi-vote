using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emil.GMP;

namespace Pirate.PiVote.Crypto
{
  public class ModInt
  {
    public BigInt Value { get; private set; }
    public BigInt Modulus { get; private set; }

    public ModInt(BigInt value, BigInt modulus)
    {
      Value = value;
      Modulus = modulus;
    }

    public static ModInt operator +(ModInt a, ModInt b)
    {
      if (a.Modulus != b.Modulus)
        throw new ArithmeticException("Moduli not matching.");

      return new ModInt((a.Value + b.Value).Mod(a.Modulus), a.Modulus);
    }

    public static ModInt operator -(ModInt a, ModInt b)
    {
      if (a.Modulus != b.Modulus)
        throw new ArithmeticException("Moduli not matching.");

      return new ModInt((a.Value - b.Value).Mod(a.Modulus), a.Modulus);
    }

    public static ModInt operator *(ModInt a, ModInt b)
    {
      if (a.Modulus != b.Modulus)
        throw new ArithmeticException("Moduli not matching.");

      return new ModInt((a.Value * b.Value).Mod(a.Modulus), a.Modulus);
    }

    public static ModInt operator /(ModInt a, ModInt b)
    {
      if (a.Modulus != b.Modulus)
        throw new ArithmeticException("Moduli not matching.");

      return new ModInt(a.Value.DivideMod(b.Value, a.Modulus), a.Modulus);
    }
  }
}
