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
using System.Windows.Forms;
using Pirate.PiVote;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.CaGui
{
  public enum CertificateType
  {
    None,
    CA,
    Admin,
    Authority,
    Voter,
    Server,
    Notary,
    All
  }

  public static class CertificateTypeExtension
  {
    public static string GetGroupName(this Certificate certificate)
    {
      if (certificate is VoterCertificate)
      {
        return GroupList.GetGroupName(((VoterCertificate)certificate).GroupId);
      }
      else
      {
        return string.Empty;
      }
    }

    public static CertificateType ToType(this Certificate certificate)
    {
      if (certificate is CACertificate)
      {
        return CertificateType.CA;
      }
      else if (certificate is AuthorityCertificate)
      {
        return CertificateType.Authority;
      }
      else if (certificate is AdminCertificate)
      {
        return CertificateType.Admin;
      }
      else if (certificate is VoterCertificate)
      {
        return CertificateType.Voter;
      }
      else if (certificate is ServerCertificate)
      {
        return CertificateType.Server;
      }
      else if (certificate is NotaryCertificate)
      {
        return CertificateType.Notary;
      }
      else
      {
        return CertificateType.None;
      }
    }

    public static string Text(this CertificateType type)
    {
      switch (type)
      {
        case CertificateType.None:
          return "No Types";
        case CertificateType.All:
          return "All Types";
        case CertificateType.CA:
          return "CA";
        case CertificateType.Admin:
          return "Admin";
        case CertificateType.Authority:
          return "Authority";
        case CertificateType.Voter:
          return "Voter";
        case CertificateType.Server:
          return "Server";
        case CertificateType.Notary:
          return "Notary";
        default:
          return "Unknown";
      }
    }

    public static bool IsOfType(this CertificateType type, Certificate certificate)
    {
      switch (type)
      {
        case CertificateType.None:
          return false;
        case CertificateType.All:
          return true;
        case CertificateType.CA:
          return certificate is CACertificate;
        case CertificateType.Admin:
          return certificate is AdminCertificate;
        case  CertificateType.Authority:
          return certificate is AuthorityCertificate;
        case CertificateType.Voter:
          return certificate is VoterCertificate;
        case CertificateType.Server:
          return certificate is ServerCertificate;
        default:
          return false;
      }
    }
  }
}
