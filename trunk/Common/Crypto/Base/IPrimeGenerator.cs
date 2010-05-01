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
  public interface IPrimeGenerator
  {
    void FindPrimeAndSafePrime(int bitLength, out BigInt prime, out BigInt safePrime);
  }
}
