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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote
{
  /// <summary>
  /// Codes for identifiing exceptions.
  /// </summary>
  [SerializeEnum("Codes for identifiing exceptions.")]
  public enum ExceptionCode
  {
    Unknown                           = 0,
    ArgumentNull                      = 1,
    ArgumentOutOfRange                = 2,
    BadSerializableFormat             = 3,
    InvalidCertificate                = 4,
    WrongStatusForOperation           = 5,
    RequestSignatureInvalid           = 6,
    NoAuthorizedAdmin                 = 7,
    BadVotingMaterial                 = 8,
    InvalidSignature                  = 9,
    InvalidSignatureRequest           = 10,
    ServerCertificateInvalid          = 11,
    CanceledByUser                    = 12,

    AuthorityCountOutOfRange          = 1000001,
    TheresholdOutOfRange              = 1000002,
    OptionCountOutOfRange             = 1000003,
    MaxVotaOutOfRange                 = 1000004,
    OptionCountMismatch               = 1000005,
    PIsNoPrime                        = 1000006,
    PIsNoSafePrime                    = 1000007,
    QIsNoPrime                        = 1000008,
    AuthorityCountMismatch            = 1000009,
    AuthorityInvalid                  = 1000010,

    NoVotingWithId                    = 2000001,

    NoAuthorityWithCertificate        = 3000001,

    AlreadyVoted                      = 4000001,
    VoteSignatureNotValid             = 4000002,
    NoVoterCertificate                = 4000003,
    InvalidVoteReceipt                = 4000004,
    BadGroupIdInCertificate           = 4000005,
    InvalidEnvelope                   = 4000006,
    InvalidEnvelopeBadDateTime        = 4000007,
    InvalidEnvelopeBadVoterId         = 4000008,
    InvalidEnvelopeBadBallotCount     = 4000009,
    InvalidEnvelopeBadProofCount      = 4000010,
    InvalidEnvelopeBadVoteCount       = 4000011,

    SignatureRequestInvalid           = 5000001,
    SignatureRequestResponded         = 5000002,
    SignatureRequestNotFound          = 5000003,

    SignatureResponseNotFromCA        = 6000001,

    NoAuthorizedAuthority             = 7000001,
    AlreadyEnoughAuthorities          = 7000002,
    AuthorityAlreadyInVoting          = 7000003,
    AuthorityHasAlreadyDeposited      = 7000004,

    PartialDecipherBadSignature       = 8000001,
    PartialDecipherBadEnvelopeCount   = 8000002,
    PartialDecipherBadEnvelopeHash    = 8000003,

    ShareResponseBadSignature         = 9000001,
    ShareResponseWrongAuthority       = 9000002,
    ShareResponseNotAccepted          = 9000003,
    ShareResponseParametersDontMatch  = 9000004,

    CommandNotFromAdmin               = 19000001,
    CommandNotAllowedInStatus         = 19000002,
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
        case ExceptionCode.InvalidVoteReceipt:
          return LibraryResources.ExceptionInvalidVoteReceipt;
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
        case ExceptionCode.PartialDecipherBadSignature:
          return LibraryResources.ExceptionPartialDecipherBadSignature;
        case ExceptionCode.PartialDecipherBadEnvelopeCount:
          return LibraryResources.ExceptionPartialDecipherBadEnvelopeCount;
        case ExceptionCode.PartialDecipherBadEnvelopeHash:
          return LibraryResources.ExceptionPartialDecipherBadEnvelopeHash;
        case ExceptionCode.ShareResponseBadSignature:
          return LibraryResources.ExceptionShareResponseBadSignature;
        case ExceptionCode.ShareResponseNotAccepted:
          return LibraryResources.ExceptionShareResponseNotAccepted;
        case ExceptionCode.ShareResponseParametersDontMatch:
          return LibraryResources.ExceptionShareResponseParametersDontMatch;
        case ExceptionCode.ShareResponseWrongAuthority:
          return LibraryResources.ExceptionShareResponseWrongAuthority;
        case ExceptionCode.InvalidSignatureRequest:
          return LibraryResources.ExceptionInvalidSignatureRequest;
        case ExceptionCode.BadGroupIdInCertificate:
          return LibraryResources.ExceptionBadGroupIdInCertificate;
        case ExceptionCode.InvalidEnvelope:
          return LibraryResources.ExceptionInvalidEnvelope;
        case ExceptionCode.InvalidEnvelopeBadDateTime:
          return LibraryResources.ExceptionInvalidEnvelopeBadDateTime;
        case ExceptionCode.InvalidEnvelopeBadBallotCount:
          return LibraryResources.ExceptionInvalidEnvelopeBadBallotCount;
        case ExceptionCode.InvalidEnvelopeBadProofCount:
          return LibraryResources.ExceptionInvalidEnvelopeBadProofCount;
        case ExceptionCode.InvalidEnvelopeBadVoteCount:
          return LibraryResources.ExceptionInvalidEnvelopeBadVoteCount;
        case ExceptionCode.InvalidEnvelopeBadVoterId:
          return LibraryResources.ExceptionInvalidEnvelopeBadVoterId;
        case ExceptionCode.CommandNotFromAdmin:
          return LibraryResources.CommandNotFromAdmin;
        case ExceptionCode.CommandNotAllowedInStatus:
          return LibraryResources.CommandNotAllowedInStatus;
        case ExceptionCode.CanceledByUser:
          return LibraryResources.ExceptionCanceledByUser;
        default:
          return LibraryResources.ExceptionUnknown;
      }
    }
  }
}
