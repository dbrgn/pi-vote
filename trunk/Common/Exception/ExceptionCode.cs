
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
    SignatureRequestNotFound      = 5000003,

    SignatureResponseNotFromCA    = 6000001,

    NoAuthorizedAuthority         = 7000001,
    AlreadyEnoughAuthorities      = 7000002,
    AuthorityAlreadyInVoting      = 7000003,
    AuthorityHasAlreadyDeposited  = 7000004
  }

  public static class ExceptionCodeExtension
  {
    public static string Text(this ExceptionCode code)
    {
      switch (code)
      {
        case ExceptionCode.WrongStatusForOperation:
          return LibraryResources.ExceptionWrongStatusForOperation;
        case ExceptionCode.VoteSignatureNotValid:
          return LibraryResources.ExceptionVoteSignatureNotValid;
        case ExceptionCode.Unknown:
          return LibraryResources.ExceptionUnknown;
        case ExceptionCode.TheresholdOutOfRange:
          return LibraryResources.ExceptionTheresholdOutOfRange;
        case ExceptionCode.SignatureResponseNotFromCA:
          return LibraryResources.ExceptionSignatureResponseNotFromCA;
        case ExceptionCode.SignatureRequestResponded:
          return LibraryResources.ExceptionSignatureRequestResponded;
        case ExceptionCode.SignatureRequestNotFound:
          return LibraryResources.ExceptionSignatureRequestNotFound;
        case ExceptionCode.SignatureRequestInvalid:
          return LibraryResources.ExceptionSignatureRequestInvalid;
        case ExceptionCode.RequestSignatureInvalid:
          return LibraryResources.ExceptionRequestSignatureInvalid;
        case ExceptionCode.QIsNoPrime:
          return LibraryResources.ExceptionQIsNoPrime;
        case ExceptionCode.PIsNoSafePrime:
          return LibraryResources.ExceptionPIsNoSafePrime;
        case ExceptionCode.PIsNoPrime:
          return LibraryResources.ExceptionPIsNoPrime;
        case ExceptionCode.OptionCountOutOfRange:
          return LibraryResources.ExceptionOptionCountOutOfRange;
        case ExceptionCode.OptionCountMismatch:
          return LibraryResources.ExceptionOptionCountMismatch;
        case ExceptionCode.NoVotingWithId:
          return LibraryResources.ExceptionNoVotingWithId;
        case ExceptionCode.NoVoterCertificate:
          return LibraryResources.ExceptionNoVoterCertificate;
        case ExceptionCode.NoAuthorizedAdmin:
          return LibraryResources.ExceptionNoAuthorizedAdmin;
        case ExceptionCode.NoAuthorityWithCertificate:
          return LibraryResources.ExceptionNoAuthorityWithCertificate;
        case ExceptionCode.MaxVotaOutOfRange:
          return LibraryResources.ExceptionMaxVotaOutOfRange;
        case ExceptionCode.InvalidSignature:
          return LibraryResources.ExceptionInvalidSignature;
        case ExceptionCode.InvalidCertificate:
          return LibraryResources.ExceptionInvalidCertificate;
        case ExceptionCode.BadVotingMaterial:
          return LibraryResources.ExceptionBadVotingMaterial;
        case ExceptionCode.BadSerializableFormat:
          return LibraryResources.ExceptionBadSerializableFormat;
        case ExceptionCode.AuthorityInvalid:
          return LibraryResources.ExceptionAuthorityInvalid;
        case ExceptionCode.AuthorityCountOutOfRange:
          return LibraryResources.ExceptionAuthorityCountOutOfRange;
        case ExceptionCode.AuthorityCountMismatch:
          return LibraryResources.ExceptionAuthorityCountMismatch;
        case ExceptionCode.ArgumentOutOfRange:
          return LibraryResources.ExceptionArgumentOutOfRange;
        case ExceptionCode.ArgumentNull:
          return LibraryResources.ExceptionArgumentNull;
        case ExceptionCode.AlreadyVoted:
          return LibraryResources.ExceptionAlreadyVoted;
        case ExceptionCode.NoAuthorizedAuthority:
          return LibraryResources.ExceptionNoAuthorizedAuthority;
        case ExceptionCode.AlreadyEnoughAuthorities:
          return LibraryResources.ExceptionAlreadyEnoughAuthorities;
        case ExceptionCode.AuthorityAlreadyInVoting:
          return LibraryResources.ExceptionAuthorityAlreadyInVoting;
        case ExceptionCode.AuthorityHasAlreadyDeposited:
          return LibraryResources.ExceptionAuthorityHasAlreadyDeposited;
        default:
          return LibraryResources.ExceptionUnknown;
      }
    }
  }
}
