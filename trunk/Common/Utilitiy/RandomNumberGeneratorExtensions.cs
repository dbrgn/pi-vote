using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Security.Cryptography
{
  public static class RandomNumberGeneratorExtensions
  {
    public static int GetInteger(this RandomNumberGenerator rng)
    {
      int number = 0;
      byte[] data = new byte[4];
      rng.GetBytes(data);

      for (int index = 0; index < 4; index += 8)
      {
        number |= (data[index] << (index * 8));
      }

      return number;
    }
  }
}
