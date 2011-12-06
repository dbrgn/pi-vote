/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emil.GMP;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Gui;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Circle.CreateVoting
{
  public class CreateDialogStatus
  {
    public CircleController Controller { get; private set; }

    public VotingData Data { get; set; }

    public DateTime FromDate { get; set; }

    public DateTime UntilDate { get; set; }

    public Group VotingGroup { get; set; }

    public BigInt SafePrime { get; set; }

    public BigInt Prime { get; set; }

    public IEnumerable<AuthorityCertificate> Authorites { get; set; }

    public CreateDialogStatus(CircleController controller)
    {
      Controller = controller;
    }
  }
}
