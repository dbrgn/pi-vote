/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
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
  /// Status of a private key.
  /// </summary>
  [SerializeEnum("Status of a private key.")]
  public enum PrivateKeyStatus
  {
    Unavailable = 0,
    Unencrypted = 1,
    Encrypted = 2,
    Decrypted = 3
  }
}
