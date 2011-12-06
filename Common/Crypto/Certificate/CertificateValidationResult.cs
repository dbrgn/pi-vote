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
    NotYetValid,
    Revoked,
    CrlMissing,
    SignerInvalid,
    UnknownSigner,
    SignatureDataInvalid
  }

  public static class CertificateValidationResultExtensions
  {
    public static string Text(this CertificateValidationResult result)
    {
      switch (result)
      {
        case  CertificateValidationResult.CrlMissing:
          return LibraryResources.CertificateStatusCrlMissing;
        case CertificateValidationResult.NoSignature:
          return LibraryResources.CertificateStatusNoSignature;
        case CertificateValidationResult.NotYetValid:
          return LibraryResources.CertificateStatusNotYetValid;
        case CertificateValidationResult.Outdated:
          return LibraryResources.CertificateStatusOutdated;
        case CertificateValidationResult.Revoked:
          return LibraryResources.CertificateStatusRevoked;
        case CertificateValidationResult.SelfsignatureInvalid:
          return LibraryResources.CertificateStatusSelfSignatureInvalid;
        case CertificateValidationResult.SignatureDataInvalid:
          return LibraryResources.CertificateStatusSignatureDataInvalid;
        case CertificateValidationResult.SignerInvalid:
          return LibraryResources.CertificateStatusSignerInvalid;
        case CertificateValidationResult.UnknownSigner:
          return LibraryResources.CertificateStatusUnknownSigner;
        case CertificateValidationResult.Valid:
          return LibraryResources.CertificateStatusValid;
        default:
          return LibraryResources.CertificateStatusUnknown;
      }
    }
  }
}
