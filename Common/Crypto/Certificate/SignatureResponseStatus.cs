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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Status of the signature response.
  /// </summary>
  [SerializeEnum("Status of the signature response.")]
  public enum SignatureResponseStatus
  {
    Unknown = 0,
    Pending = 1,
    Accepted = 2,
    Declined = 3
  }
}
