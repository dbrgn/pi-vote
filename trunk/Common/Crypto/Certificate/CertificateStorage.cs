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
  /// Stores certificates for validation.
  /// </summary>
  public class CertificateStorage
  {
    /// <summary>
    /// Ids of root certificates.
    /// </summary>
    private List<Guid> rootCertificateIds;

    /// <summary>
    /// List of certificates.
    /// </summary>
    private Dictionary<Guid, Certificate> certificates;

    /// <summary>
    /// Creates a new certificate storage.
    /// </summary>
    public CertificateStorage()
    {
      this.certificates = new Dictionary<Guid, Certificate>();
      this.rootCertificateIds = new List<Guid>();
    }

    /// <summary>
    /// Get a certificate by id.
    /// </summary>
    /// <param name="id">Certificate id.</param>
    /// <returns>Certificate for id.</returns>
    public Certificate Get(Guid id)
    {
      if (!Has(id))
        throw new ArgumentException("Certificate not in storage.");

      return this.certificates[id];
    }

    /// <summary>
    /// Is the certificate with this id in storage.
    /// </summary>
    /// <param name="id">Certificate id.</param>
    /// <returns>Is it there?</returns>
    public bool Has(Guid id)
    {
      return this.certificates.ContainsKey(id);
    }

    /// <summary>
    /// Add a certificate to the storage.
    /// </summary>
    /// <param name="certificate">Certificate to add.</param>
    public void Add(Certificate certificate)
    {
      this.certificates.Add(certificate.Id, certificate);
    }

    /// <summary>
    /// Add a root certificate to storage.
    /// </summary>
    /// <param name="certificate">Root certificate to add.</param>
    public void AddRoot(Certificate certificate)
    {
      this.certificates.Add(certificate.Id, certificate);
      this.rootCertificateIds.Add(certificate.Id);
    }

    /// <summary>
    /// Checks weather the certificate is a root certificate.
    /// </summary>
    /// <param name="certificate">Certificate to check.</param>
    /// <returns>Is it a root?</returns>
    public bool IsRootCertificate(Certificate certificate)
    {
      return this.rootCertificateIds.Contains(certificate.Id);
    }
  }
}
