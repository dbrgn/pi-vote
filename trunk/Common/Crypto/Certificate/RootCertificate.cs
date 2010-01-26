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
using System.Security.Cryptography;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Handles the root certificate.
  /// </summary>
  public static class RootCertificate
  {
    /// <summary>
    /// File name for storing the root certificate.
    /// </summary>
    private const string RootCertificateFileName = "root.cert";

    /// <summary>
    /// The root certificate itself.
    /// </summary>
    public static CACertificate Value { get; private set; }

    /// <summary>
    /// Creates a new root certificate.
    /// </summary>
    /// <param name="fullName">Full name of the root certificate authority.</param>
    public static void Create(string fullName)
    {
      if (fullName == null)
        throw new ArgumentException("fullName");

      Value = new CACertificate(fullName);
      Value.CreateSelfSignature();
    }

    /// <summary>
    /// Load the root certificate.
    /// </summary>
    public static void Load()
    {
      Value = (CACertificate)Certificate.Load(RootCertificateFileName);
    }

    /// <summary>
    /// Save the root certificate.
    /// </summary>
    public static void Save()
    {
      Value.Save(RootCertificateFileName);
    }
  }
}
