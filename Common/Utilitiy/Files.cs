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
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote
{
  public static class Files
  {
    public const string TextFileFilter = "Text|*.txt";
    public const string RootCertificateFileName = "root.pi-cert";
    public const string CertificateStorageFileFilter = "Pi-Vote Certificate Storage|*.pi-cert-storage";
    public const string PdfFileFilter = "PDF|*.pdf";
    public const string CertificateFileFilter = "Pi-Vote Certificate|*.pi-cert";
    public const string BackupFileFilter = "Pi-Vote Backup|*.pi-backup";
    public const string CertificateExtension = ".pi-cert";
    public const string CertificatePattern = "*.pi-cert";
    public const string SignatureRequestExtension = ".pi-sig-req";
    public const string SignatureRequestPattern = "*.pi-sig-req";
    public const string SignatureRequestDataExtension = ".pi-sig-req-data";
    public const string SignatureRequestInfoExtension = ".pi-sig-req-info";
    public const string SignatureRequestFileFilter = "Pi-Vote Signature Request|*.pi-sig-req";
    public const string SignatureResponseExtension = ".pi-sig-resp";
    public const string SignatureResponsePattern = "*.pi-sig-resp";
    public const string SignatureResponseFileFilter = "Pi-Vote Signature Response|*.pi-sig-resp";
    public const string AuthorityDataFileFilter = "Pi-Vote Certificate|*.pi-auth";
    public const string BadShareProofFileFilter = "Pi-Vote Share Proof|*.pi-shareproof";
    public const string VotingMaterialFileName = "voting.pi-material";
    public const string EnvelopeFilePattern = "*.pi-envelope";
    public const string PartialDecipherFileString = "{0:00}.pi-partialdecipher";
    public const string EnvelopeFileString = "{0:00000000}.pi-envelope";
    public const string SafePrimePattern = "*.pi-safeprime";
    public const string SafePrimeFileString = "{0}.pi-safeprime";
    public const string DataFolder = "PiVote";
    public const string PortableDataFolder = "Data";
    public const string BakExtension = ".bak";
    public const string VoteReceiptExtension = ".pi-receipt";
    public const string VoteReceiptPattern = "*.pi-receipt";
    private const string VoteReceiptFileNameBuild = "{0}@{1}.pi-receipt";
    public const string CircleUserConfigFileName = "circle.user.cfg";

    public static string TestDataPath
    {
      get
      {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), DataFolder);
      }
    }

    public static string VoteReceiptFileName(Guid voterCertificateId, Guid votingId)
    {
      return string.Format(VoteReceiptFileNameBuild, voterCertificateId.ToString(), votingId.ToString());
    }
  }
}
