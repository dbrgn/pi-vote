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

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Result of a certificate validation.
  /// </summary>
  public enum CertificateValidationResult
  {
    Valid,
    SelfsignatureInvalid,
    NoSignature,
    Outdated,
    Revoked,
    SignerInvalid,
    UnknownSigner,
    SignatureDataInvalid
  }
}
