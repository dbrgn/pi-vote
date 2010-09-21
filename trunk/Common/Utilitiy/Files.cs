
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
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote
{
  public static class Files
  {
    public const string RootCertificateFileName = "root.pi-cert";
    public const string CertificateStorageFileFilter = "Pi-Vote Certificate Storage|*.pi-cert-storage";
    public const string CertificateFileFilter = "Pi-Vote Certificate|*.pi-cert";
    public const string FolderFileFilter = "Folder|*";
    public const string CertificateExtension = ".pi-cert";
    public const string SignatureRequestExtension = ".pi-sig-req";
    public const string SignatureRequestDataExtension = ".pi-sig-req-data";
    public const string SignatureRequestInfoExtension = ".pi-sig-req-info";
    public const string SignatureRequestFileFilter = "Pi-Vote Signature Request|*.pi-sig-req";
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
    public const string BackupExtension = ".bak";
  }
}
