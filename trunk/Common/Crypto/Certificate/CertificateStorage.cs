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
  public class CertificateStorage : Serializable, ICertificateStorage
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
    private List<RevocationList> revocationLists;

    /// <summary>
    /// Signed certificate revocation lists for certificate authorities.
    /// </summary>
    private List<Signed<RevocationList>> signedRevocationLists;

    /// <summary>
    /// Signed certificate revocation lists for certificate authorities.
    /// </summary>
    public IEnumerable<Signed<RevocationList>> SignedRevocationLists { get { return this.signedRevocationLists; } }

    /// <summary>
    /// List of certificates.
    /// </summary>
    public IEnumerable<Certificate> Certificates { get { return this.certificates.Values; } }

    /// <summary>
    /// Creates a new certificate storage.
    /// </summary>
    public CertificateStorage()
    {
      this.certificates = new Dictionary<Guid, Certificate>();
      this.revocationLists = new List<RevocationList>();
      this.rootCertificateIds = new List<Guid>();
      this.signedRevocationLists = new List<Signed<RevocationList>>();
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public CertificateStorage(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(this.rootCertificateIds.Count);
      this.rootCertificateIds.ForEach(rootCertificateId => context.Write(rootCertificateId));
      context.WriteList(this.certificates.Values);
      context.WriteList(this.revocationLists);
      context.WriteList(this.signedRevocationLists);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);

      int count = context.ReadInt32();
      this.rootCertificateIds = new List<Guid>();
      count.Times(() => this.rootCertificateIds.Add(context.ReadGuid()));

      this.certificates = new Dictionary<Guid,Certificate>();
      List<Certificate> certificates = context.ReadObjectList<Certificate>();
      certificates.ForEach(certificate => this.certificates.Add(certificate.Id, certificate));

      this.revocationLists = context.ReadObjectList<RevocationList>();
      this.signedRevocationLists = context.ReadObjectList<Signed<RevocationList>>();
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
    /// Is this CRL in this storage.
    /// </summary>
    /// <param name="revocationList">Revocation list in question.</param>
    /// <returns>Was it already added?</returns>
    public bool Has(RevocationList revocationList)
    { 
      return SignedRevocationLists
        .Any(other => other.Value.IssuerId == revocationList.IssuerId &&
                      other.Value.ValidFrom == revocationList.ValidFrom &&
                      other.Value.ValidUntil == revocationList.ValidUntil);
    }

    /// <summary>
    /// Add a certificate to the storage.
    /// </summary>
    /// <param name="certificate">Certificate to add.</param>
    public void Add(Certificate certificate)
    {
      if (certificate == null)
        throw new ArgumentNullException("certificate");
      if (certificate.HasPrivateKey)
        throw new ArgumentException("You must not add private key to certificate storages.");

      if (this.certificates.ContainsKey(certificate.Id))
      {
        this.certificates[certificate.Id].Merge(certificate);
      }
      else
      {
        this.certificates.Add(certificate.Id, certificate);
      }
    }

    /// <summary>
    /// Add a root certificate to storage.
    /// </summary>
    /// <param name="certificate">Root certificate to add.</param>
    public void AddRoot(Certificate certificate)
    {
      if (certificate == null)
        throw new ArgumentNullException("certificate");
      if (certificate.HasPrivateKey)
        throw new ArgumentException("You must not add private key to certificate storages.");

      Add(certificate);

      if (!this.rootCertificateIds.Contains(certificate.Id))
      {
        this.rootCertificateIds.Add(certificate.Id);
      }
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
    public void AddRevocationList(Signed<RevocationList> signedRevocationList)
    {
      RevocationList revocationList = signedRevocationList.Value;
      if (revocationList.IssuerId != signedRevocationList.Certificate.Id)
        throw new ArgumentException("Wrong signer of revocation list.");
      if (!signedRevocationList.Verify(this))
        throw new ArgumentException("Bad signature on revocation list.");

      if (this.revocationLists
        .Where(list => list.IssuerId == revocationList.IssuerId &&
               list.ValidFrom == revocationList.ValidFrom &&
               list.ValidUntil == revocationList.ValidUntil).Count() < 1)
      {
        this.revocationLists.Add(revocationList);
        this.signedRevocationLists.Add(signedRevocationList);
      }
    }

    /// <summary>
    /// Set a certificate revocation list without checking validity.
    /// </summary>
    /// <param name="signedRevocationList">Signed certificate revocation list.</param>
    private void  ForceAddRevocationList(Signed<RevocationList> signedRevocationList)
    {
      RevocationList revocationList = signedRevocationList.Value;

      if (this.revocationLists
        .Where(list => list.IssuerId == revocationList.IssuerId &&
               list.ValidFrom == revocationList.ValidFrom &&
               list.ValidUntil == revocationList.ValidUntil).Count() < 1)
      {
        this.revocationLists.Add(revocationList);
        this.signedRevocationLists.Add(signedRevocationList);
      }
    }

    /// <summary>
    /// Is the certificate revoked.
    /// </summary>
    /// <param name="issuerId">Id of the issuer of a signature.</param>
    /// <param name="certificateId">Id of the certificate.</param>
    /// <returns>Is it revoked.</returns>
    public bool IsRevoked(Guid issuerId, Guid certificateId, DateTime date)
    {
      RevocationList revocationList = this.revocationLists
        .Where(list => list.IssuerId == issuerId &&
               date.Date >= list.ValidFrom.Date &&
               date.Date <= list.ValidUntil.Date)
        .FirstOrDefault();

      if (revocationList == null)
      {
        return true;
      }
      else
      {
        return revocationList.RevokedCertificates.Contains(certificateId);
      }
    }

    /// <summary>
    /// Adds all certificates and revocation lists from a storage.
    /// </summary>
    /// <remarks>
    /// Does NOT add root certificates.
    /// </remarks>
    /// <param name="certificateStorage">Certificate storage to add.</param>
    public void Add(ICertificateStorage certificateStorage)
    {
      certificateStorage.Certificates
        .Foreach(certificate => Add(certificate));
      certificateStorage.SignedRevocationLists
        .Foreach(reveocationList => ForceAddRevocationList(reveocationList));
    }

    /// <summary>
    /// Adds only CA certificates and revocation lists from a storage.
    /// </summary>
    /// <remarks>
    /// Does NOT add root certificates.
    /// </remarks>
    /// <param name="certificateStorage">Certificate storage to add.</param>
    public void AddOnlyCA(ICertificateStorage certificateStorage)
    {
      certificateStorage.Certificates
        .Where(certificate => certificate is CACertificate)
        .Foreach(certificate => Add(certificate));
      certificateStorage.SignedRevocationLists
        .Foreach(reveocationList => ForceAddRevocationList(reveocationList));
    }

    /// <summary>
    /// Loads the root certificate from resources.
    /// </summary>
    public bool TryLoadRoot()
    {
      string fileName = Path.Combine(System.Windows.Forms.Application.StartupPath, Files.RootCertificateFileName);

      if (File.Exists(fileName))
      {
        AddRoot(Serializable.Load<Certificate>(fileName));
        return true;
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Determines if there is a valid revocation list for a given CA.
    /// </summary>
    /// <param name="issuerId">Id of the issuing CA.</param>
    /// <param name="date">Date at which the revocation list shall be valid.</param>
    /// <returns>Is there a vaild CRL?</returns>
    public bool HasValidRevocationList(Guid issuerId, DateTime date)
    {
      return SignedRevocationLists
        .Where(signedRevocationList => signedRevocationList.Verify(this))
        .Select(signedRevocationList => signedRevocationList.Value)
        .Where(revocationList => revocationList.IssuerId == issuerId &&
                                 revocationList.ValidFrom.Date <= date.Date &&
                                 revocationList.ValidUntil.Date >= date.Date)
        .Count() > 0;
    }
  }
}
