
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
    public const string CertificateFileFilter = "Pi-Vote Certificate|*.pi-cert";
    public const string SignatureRequestFileFilter = "Pi-Vote Signature Request|*.pi-sig-resp";
    public const string SignatureResponseFileFilter = "Pi-Vote Signature Request|*.pi-sig-req";
    public const string AuthorityDataFileFilter = "Pi-Vote Certificate|*.pi-auth";
  }
}
