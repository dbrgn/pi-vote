
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

namespace Pirate.PiVote
{
  /// <summary>
  /// Codes for identifiing exceptions.
  /// </summary>
  public enum ExceptionCode
  {
    Unknown                       = 0,
    ArgumentNull                  = 1,
    ArgumentOutOfRange            = 2,
    BadSerializableFormat         = 3,
    InvalidCertificate            = 4,
    WrongStatusForOperation       = 5,
    RequestSignatureInvalid       = 6,
    NoAuthorizedAdmin             = 7,
    BadVotingMaterial             = 8,
    InvalidSignature              = 9,

    AuthorityCountOutOfRange      = 1000001,
    TheresholdOutOfRange          = 1000002,
    OptionCountOutOfRange         = 1000003,
    MaxVotaOutOfRange             = 1000004,
    OptionCountMismatch           = 1000005,
    PIsNoPrime                    = 1000006,
    PIsNoSafePrime                = 1000007,
    QIsNoPrime                    = 1000008,
    AuthorityCountMismatch        = 1000009,
    AuthorityInvalid              = 1000010,

    NoVotingWithId                = 2000001,

    NoAuthorityWithCertificate    = 3000001,

    AlreadyVoted                  = 4000001,
    VoteSignatureNotValid         = 4000002,
    NoVoterCertificate            = 4000003,

    SignatureRequestInvalid       = 5000001,
    SignatureRequestResponded     = 5000002,
    SignatureRequestNotFound      = 5000003
  }
}
