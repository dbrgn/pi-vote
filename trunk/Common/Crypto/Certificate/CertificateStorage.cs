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
    /// Certificate revocation lists for certificate authorities.
    /// </summary>
    private Dictionary<Guid, RevocationList> revocationLists;

    /// <summary>
    /// Signed certificate revocation lists for certificate authorities.
    /// </summary>
    private Dictionary<Guid, Signed<RevocationList>> signedRevocationLists;

    /// <summary>
    /// Signed certificate revocation lists for certificate authorities.
    /// </summary>
    public IEnumerable<Signed<RevocationList>> SignedRevocationLists { get { return this.signedRevocationLists.Values; } }

    /// <summary>
    /// List of certificates.
    /// </summary>
    public IEnumerable<Certificate> Certificates { get { return this.certificates.Values.Where(certificate => !this.rootCertificateIds.Contains(certificate.Id)); } }

    /// <summary>
    /// Creates a new certificate storage.
    /// </summary>
    public CertificateStorage()
    {
      this.certificates = new Dictionary<Guid, Certificate>();
      this.revocationLists = new Dictionary<Guid, RevocationList>();
      this.rootCertificateIds = new List<Guid>();
      this.signedRevocationLists = new Dictionary<Guid, Signed<RevocationList>>();
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
      if (certificate == null)
        throw new ArgumentNullException("certificate");
      if (this.certificates.ContainsKey(certificate.Id))
        throw new ArgumentException("Certificate already added.");

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

    /// <summary>
    /// Set a certificate revocation list.
    /// </summary>
    /// <param name="signedRevocationList">Signed certificate revocation list.</param>
    public void SetRevocationList(Signed<RevocationList> signedRevocationList)
    {
      RevocationList revocationList = signedRevocationList.Value;
      if (revocationList.IssuerId != signedRevocationList.Certificate.Id)
        throw new ArgumentException("Wrong signer of revocation list.");
      if (!signedRevocationList.Verify(this))
        throw new ArgumentException("Bad signature on revocation list.");

      if (this.revocationLists.ContainsKey(revocationList.IssuerId))
      {
        this.revocationLists[revocationList.IssuerId] = revocationList;
      }
      else
      {
        this.revocationLists.Add(revocationList.IssuerId, revocationList);
      }

      if (this.signedRevocationLists.ContainsKey(revocationList.IssuerId))
      {
        this.signedRevocationLists[revocationList.IssuerId] = signedRevocationList;
      }
      else
      {
        this.signedRevocationLists.Add(revocationList.IssuerId, signedRevocationList);
      }
    }

    /// <summary>
    /// Is the certificate revoked.
    /// </summary>
    /// <param name="issuerId">Id of the issuer of a signature.</param>
    /// <param name="certificateId">Id of the certificate.</param>
    /// <returns>Is it revoked.</returns>
    public bool IsRevoked(Guid issuerId, Guid certificateId)
    {
      if (this.revocationLists.ContainsKey(issuerId))
      {
        RevocationList revocationList = this.revocationLists[issuerId];

        if (revocationList.ValidFrom < DateTime.Now &&
            revocationList.ValidUntil > DateTime.Now)
        {
          return revocationList.RevokedCertificates.Contains(certificateId);
        }
        else
        {
          return true;
        }
      }
      else
      {
        return true;
      }
    }
  }
}
