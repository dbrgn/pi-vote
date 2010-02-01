
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
  public enum ExceptionCode
  {
    Unknown                       = 0,
    ArgumentNull                  = 1,
    ArgumentOutOfRange            = 2,
    BadSerializableFormat         = 3,
    InvalidCertificate            = 4,

    AuthorityCountOutOfRange      = 1000001,
    TheresholdOutOfRange          = 1000002,
    OptionCountOutOfRange         = 1000003,
    MaxVotaOutOfRange             = 1000004,
    OptionCountMismatch           = 1000005,
    PIsNoPrime                    = 1000006,
    PIsNoSafePrime                = 1000007,
    QIsNoPrime                    = 1000008,
    AuthorityCountMismatch        = 1000009,

    NoVotingWithId                = 2000001,

    NoAuthorityWithCertificate    = 3000001
  }
}
