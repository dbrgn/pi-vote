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
  public interface ICertificateStorage
  {
    /// <summary>
    /// Signed certificate revocation lists for certificate authorities.
    /// </summary>
    IEnumerable<Signed<RevocationList>> SignedRevocationLists { get; }

    /// <summary>
    /// List of certificates.
    /// </summary>
    IEnumerable<Certificate> Certificates { get; }

    /// <summary>
    /// Get a certificate by id.
    /// </summary>
    /// <param name="id">Certificate id.</param>
    /// <returns>Certificate for id.</returns>
    Certificate Get(Guid id);

    /// <summary>
    /// Is the certificate with this id in storage.
    /// </summary>
    /// <param name="id">Certificate id.</param>
    /// <returns>Is it there?</returns>
    bool Has(Guid id);

    /// <summary>
    /// Is this CRL in this storage.
    /// </summary>
    /// <param name="revocationList">Revocation list in question.</param>
    /// <returns>Was it already added?</returns>
    bool Has(RevocationList revocationList);

    /// <summary>
    /// Add a certificate to the storage.
    /// </summary>
    /// <param name="certificate">Certificate to add.</param>
    void Add(Certificate certificate);

    /// <summary>
    /// Add a root certificate to storage.
    /// </summary>
    /// <param name="certificate">Root certificate to add.</param>
    void AddRoot(Certificate certificate);

    /// <summary>
    /// Checks weather the certificate is a root certificate.
    /// </summary>
    /// <param name="certificate">Certificate to check.</param>
    /// <returns>Is it a root?</returns>
    bool IsRootCertificate(Certificate certificate);

    /// <summary>
    /// Set a certificate revocation list.
    /// </summary>
    /// <param name="signedRevocationList">Signed certificate revocation list.</param>
    void AddRevocationList(Signed<RevocationList> signedRevocationList);

    /// <summary>
    /// Is the certificate revoked.
    /// </summary>
    /// <param name="issuerId">Id of the issuer of a signature.</param>
    /// <param name="certificateId">Id of the certificate.</param>
    /// <returns>Is it revoked.</returns>
    bool IsRevoked(Guid issuerId, Guid certificateId, DateTime date);

    /// <summary>
    /// Adds all certificates and revocation lists from a storage.
    /// </summary>
    /// <remarks>
    /// Does NOT add root certificates.
    /// </remarks>
    /// <param name="certificateStorage">Certificate storage to add.</param>
    void Add(ICertificateStorage certificateStorage);
  }
}
