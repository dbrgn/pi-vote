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
