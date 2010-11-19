using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pirate.PiVote.CaGui
{
  public enum CertificateStatus
  {
    None,
    New,
    Valid,
    Revoked,
    Refused,
    All
  }

  public static class CertificateStatusExtensions
  {
    public static string Text(this CertificateStatus status)
    {
      switch (status)
      { 
        case CertificateStatus.All:
          return "All Status";
        case CertificateStatus.New:
          return "New";
        case CertificateStatus.Valid:
          return "Valid";
        case CertificateStatus.Revoked:
          return "Revoked";
        case CertificateStatus.Refused:
          return "Refused";
        default:
          return "Unknown";
      }
    }
  }
}
