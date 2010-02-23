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
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Rpc;

namespace Pirate.PiVote.Client
{
  public class WizardStatus
  {
    public CertificateStorage CertificateStorage { get; set; }

    public Certificate Certificate { get; set; }

    public string CertificateFileName { get; set; }

    public VotingClient VotingClient { get; set; }
  }
}
