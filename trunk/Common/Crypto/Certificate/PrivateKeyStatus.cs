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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Status of a private key
  /// </summary>
  public enum PrivateKeyStatus
  {
    Unavailable = 0,
    Unencrypted = 1,
    Encrypted = 2,
    Decrypted = 3
  }
}
