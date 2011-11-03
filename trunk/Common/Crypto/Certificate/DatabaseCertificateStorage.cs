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
using System.IO;
using System.Security.Cryptography;
using Pirate.PiVote.Serialization;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Stores certificates for validation.
  /// </summary>
  public class DatabaseCertificateStorage : ICertificateStorage
  {
    /// <summary>
    /// Load certificate storage preload file name.
    /// </summary>
    private const string LoadStorageFileName = "storage.pi-cert-storage";

    /// <summary>
    /// Load server certificate file name.
    /// </summary>
    private const string LoadServerCertFileName = "server.pi-cert";

    /// <summary>
    /// MySQL database connection.
    /// </summary>
    private MySqlConnection dbConnection;

    /// <summary>
    /// The root certificate.
    /// </summary>
    private Certificate rootCertificate;

    /// <summary>
    /// Checked database connections.
    /// </summary>
    private MySqlConnection DbConnection
    {
      get { return this.dbConnection; }
    }

    /// <summary>
    /// Signed certificate revocation lists for certificate authorities.
    /// </summary>
    public IEnumerable<Signed<RevocationList>> SignedRevocationLists
    {
      get
      {
        List<Signed<RevocationList>> signedRevocationLists = new List<Signed<RevocationList>>();
        MySqlCommand command = new MySqlCommand("SELECT Value FROM revocationlist", DbConnection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
          byte[] revocationListData = reader.GetBlob(0);
          signedRevocationLists.Add(Serializable.FromBinary<Signed<RevocationList>>(revocationListData));
        }

        reader.Close();

        return signedRevocationLists;
      }
    }

    /// <summary>
    /// List of certificates.
    /// </summary>
    public IEnumerable<Certificate> Certificates
    {
      get
      {
        List<Certificate> certificates = new List<Certificate>();
        MySqlCommand command = new MySqlCommand("SELECT Value FROM certificate", DbConnection);
        MySqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
          byte[] certificateData = reader.GetBlob(0);
          certificates.Add(Serializable.FromBinary<Certificate>(certificateData));
        }

        reader.Close();

        return certificates;
      }
    }

    /// <summary>
    /// Creates a new certificate storage.
    /// </summary>
    public DatabaseCertificateStorage(MySqlConnection dbConnection)
    {
      this.dbConnection = dbConnection;
    }

    /// <summary>
    /// Get a certificate by id.
    /// </summary>
    /// <param name="id">Certificate id.</param>
    /// <returns>Certificate for id.</returns>
    public Certificate Get(Guid id)
    {
      MySqlCommand command = new MySqlCommand("SELECT Value FROM certificate WHERE Id = @Id", DbConnection);
      command.Parameters.AddWithValue("@Id", id.ToByteArray());
      MySqlDataReader reader = command.ExecuteReader();

      if (reader.Read())
      {
        byte[] certificateData = reader.GetBlob(0);
        reader.Close();
        return Serializable.FromBinary<Certificate>(certificateData);
      }
      else
      {
        reader.Close();
        throw new ArgumentException("Certificate not in storage.");
      }
    }

    /// <summary>
    /// Is the certificate with this id in storage.
    /// </summary>
    /// <param name="id">Certificate id.</param>
    /// <returns>Is it there@</returns>
    public bool Has(Guid id)
    {
      MySqlCommand command = new MySqlCommand("SELECT Id FROM certificate WHERE Id = @Id", DbConnection);
      command.Parameters.AddWithValue("@Id", id.ToByteArray());

      MySqlDataReader reader = command.ExecuteReader();
      bool has = reader.Read();
      reader.Close();

      return has;
    }

    /// <summary>
    /// Is this CRL in this storage.
    /// </summary>
    /// <param name="revocationList">Revocation list in question.</param>
    /// <returns>Was it already added@</returns>
    public bool Has(RevocationList revocationList)
    {
      MySqlCommand command = new MySqlCommand("SELECT count(*) FROM revocationlist WHERE IssuerId = @IssuerId AND ValidFrom = @ValidFrom AND ValidUntil = @ValidUntil", DbConnection);
      command.Parameters.AddWithValue("@IssuerId", revocationList.IssuerId.ToByteArray());
      command.Parameters.AddWithValue("@ValidFrom", revocationList.ValidFrom.Date.ToString("yyyy-MM-dd"));
      command.Parameters.AddWithValue("@ValidUntil", revocationList.ValidUntil.Date.ToString("yyyy-MM-dd"));
      return (long)command.ExecuteScalar() > 0;
    }

    /// <summary>
    /// Add a certificate to the storage.
    /// </summary>
    /// <param name="certificate">Certificate to add.</param>
    public void Add(Certificate certificate)
    {
      if (certificate == null)
        throw new ArgumentNullException("certificate");

      if (Has(certificate.Id))
      {
        Certificate storedCertificate = Get(certificate.Id);
        storedCertificate.Merge(certificate);

        MySqlCommand command = new MySqlCommand("UPDATE certificate SET Value = @Value WHERE Id = @Id", DbConnection);
        command.Parameters.AddWithValue("@Id", certificate.Id.ToByteArray());
        command.Parameters.AddWithValue("@Value", certificate.ToBinary());
        command.ExecuteNonQuery();
      }
      else
      {
        MySqlCommand command = new MySqlCommand("INSERT INTO certificate (Id, Value, Root) VALUES (@Id, @Value, @Root)", DbConnection);
        command.Parameters.AddWithValue("@Id", certificate.Id.ToByteArray());
        command.Parameters.AddWithValue("@Value", certificate.ToBinary());
        command.Parameters.AddWithValue("@Root", 0);
        command.ExecuteNonQuery();
      }
    }

    /// <summary>
    /// Add a root certificate to storage.
    /// </summary>
    /// <param name="certificate">Root certificate to add.</param>
    public void AddRoot(Certificate certificate)
    {
      if (!Has(certificate.Id))
      {
        MySqlCommand command = new MySqlCommand("INSERT INTO certificate (Id, Value, Root) VALUES (@Id, @Value, @Root)", DbConnection);
        command.Parameters.AddWithValue("@Id", certificate.Id.ToByteArray());
        command.Parameters.AddWithValue("@Value", certificate.ToBinary());
        command.Parameters.AddWithValue("@Root", 1);
        command.ExecuteNonQuery();
      }
      else
      {
        Certificate storedCertificate = Get(certificate.Id);
        storedCertificate.Merge(certificate);

        MySqlCommand command = new MySqlCommand("UPDATE certificate SET Value = @Value, Root = @Root WHERE Id = @Id", DbConnection);
        command.Parameters.AddWithValue("@Id", certificate.Id.ToByteArray());
        command.Parameters.AddWithValue("@Value", certificate.ToBinary());
        command.Parameters.AddWithValue("@Root", 1);
        command.ExecuteNonQuery();
      }
    }

    /// <summary>
    /// Checks weather the certificate is a root certificate.
    /// </summary>
    /// <param name="certificate">Certificate to check.</param>
    /// <returns>Is it a root@</returns>
    public bool IsRootCertificate(Certificate certificate)
    {
      return certificate.IsIdentic(this.rootCertificate);
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
      if (!(signedRevocationList.Certificate is CACertificate))
        throw new ArgumentException("Certificate is not from proper CA.");
      if (!signedRevocationList.Verify(this))
        throw new ArgumentException("Bad signature on revocation list.");

      MySqlCommand command = new MySqlCommand("SELECT Value FROM revocationlist WHERE IssuerId = @IssuerId AND ValidFrom = @ValidFrom AND ValidUntil = @ValidUntil", DbConnection);
      command.Parameters.AddWithValue("@IssuerId", revocationList.IssuerId.ToByteArray());
      command.Parameters.AddWithValue("@ValidFrom", revocationList.ValidFrom.ToString("yyyy-MM-dd"));
      command.Parameters.AddWithValue("@ValidUntil", revocationList.ValidUntil.ToString("yyyy-MM-dd"));
      MySqlDataReader reader = command.ExecuteReader();

      if (!reader.Read())
      {
        reader.Close();

        MySqlCommand insertCommand = new MySqlCommand("INSERT INTO revocationlist (IssuerId, ValidFrom, ValidUntil, Value) VALUES (@IssuerId, @ValidFrom, @ValidUntil, @Value)", DbConnection);
        insertCommand.Parameters.AddWithValue("@IssuerId", revocationList.IssuerId.ToByteArray());
        insertCommand.Parameters.AddWithValue("@ValidFrom", revocationList.ValidFrom.ToString("yyyy-MM-dd"));
        insertCommand.Parameters.AddWithValue("@ValidUntil", revocationList.ValidUntil.ToString("yyyy-MM-dd"));
        insertCommand.Parameters.AddWithValue("@Value", signedRevocationList.ToBinary());
        insertCommand.ExecuteNonQuery();
      }
      else
      {
        reader.Close();
      }
    }

    /// <summary>
    /// Is the certificate revoked.
    /// </summary>
    /// <param name="issuerId">Id of the issuer of a signature.</param>
    /// <param name="certificateId">Id of the certificate.</param>
    /// <returns>Is it revoked.</returns>
    public CertificateValidationResult IsRevoked(Guid issuerId, Guid certificateId, DateTime date)
    {
      MySqlCommand command = new MySqlCommand("SELECT Value FROM revocationlist WHERE IssuerId = @IssuerId AND @Date >= ValidFrom AND @Date <= ValidUntil", DbConnection);
      command.Parameters.AddWithValue("@IssuerId", issuerId.ToByteArray());
      command.Parameters.AddWithValue("@Date", date.Date.ToString("yyyy-MM-dd"));

      MySqlDataReader reader = command.ExecuteReader();
      CertificateValidationResult result;

      if (reader.Read())
      {
        byte[] revocationListData = reader.GetBlob(0);
        Signed<RevocationList> signedRevocationList = Serializable.FromBinary<Signed<RevocationList>>(revocationListData);
        RevocationList revocationList = signedRevocationList.Value;

        if (revocationList.RevokedCertificates.Contains(certificateId))
        {
          result = CertificateValidationResult.Revoked;
        }
        else
        {
          result = CertificateValidationResult.Valid;
        }
      }
      else
      {
        result = CertificateValidationResult.CrlMissing;
      }

      reader.Close();

      return result;
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
      certificateStorage.SignedRevocationLists
        .Foreach(reveocationList => AddRevocationList(reveocationList));
      certificateStorage.Certificates
        .Foreach(certificate => Add(certificate));
    }

    /// <summary>
    /// Adds certificates.
    /// </summary>
    /// <param name="certificates">Add these.</param>
    public void Add(IEnumerable<Certificate> certificates)
    {
      certificates
        .Foreach(certificate => Add(certificate));
    }

    /// <summary>
    /// Adds certificate revocation lists..
    /// </summary>
    /// <param name="revocationLists">Add these.</param>
    public void Add(IEnumerable<Signed<RevocationList>> revocationLists)
    {
      revocationLists
        .Foreach(reveocationList => AddRevocationList(reveocationList));
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
        .Foreach(reveocationList => AddRevocationList(reveocationList));
    }

    /// <summary>
    /// Clones only CA certificates and CRLs.
    /// </summary>
    /// <remarks>
    /// Does NOT copy root property of certificates.
    /// </remarks>
    /// <returns>Memory copy of certificate storage.</returns>
    public CertificateStorage CloneOnlyCA()
    {
      CertificateStorage certificateStorage = new CertificateStorage();
      certificateStorage.AddOnlyCA(this);
      return certificateStorage;
    }

    public ServerCertificate LoadServerCertificate()
    {
      if (File.Exists(LoadServerCertFileName))
      {
        ServerCertificate serverCertificate =
          Serializable.Load<ServerCertificate>(LoadServerCertFileName);
        Add(serverCertificate.OnlyPublicPart);

        return serverCertificate;
      }
      else
      {
        throw new InvalidOperationException("Server certificate file not found.");
      }
    }

    /// <summary>
    /// Loads the root certificate from resources.
    /// </summary>
    public bool TryLoadRoot()
    {
      if (File.Exists(Files.RootCertificateFileName))
      {
        this.rootCertificate = Serializable.Load<Certificate>(Files.RootCertificateFileName);
        AddRoot(this.rootCertificate);
        return true;
      }
      else
      {
        return false;
      }
    }

    /// <summary>
    /// Imports certificate storage if needed.
    /// </summary>
    public void ImportStorageIfNeed()
    {
      long validCertificateCount = Certificates
        .Where(certificate => certificate.Validate(this) == CertificateValidationResult.Valid)
        .Count();

      if (validCertificateCount < 2)
      {
        if (!File.Exists(LoadStorageFileName))
          throw new InvalidOperationException("Certificate storage preload file not found.");

        CertificateStorage certificateStorage = Serializable.Load<CertificateStorage>(LoadStorageFileName);
        Add(certificateStorage);
      }
    }
  }
}
