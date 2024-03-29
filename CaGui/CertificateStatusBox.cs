﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pirate.PiVote;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.CaGui
{
  public class CertificateStatusBox : TypedBox<CertificateStatus>
  {
    public CertificateStatusBox()
    {
    }

    public override void SetValues()
    {
      AddItem(CertificateStatus.All);
      AddItem(CertificateStatus.New);
      AddItem(CertificateStatus.Valid);
      AddItem(CertificateStatus.NotYet);
      AddItem(CertificateStatus.Outdated);
      AddItem(CertificateStatus.Revoked);
      AddItem(CertificateStatus.Refused);

      SelectedIndex = 0;
    }

    private void AddItem(CertificateStatus status)
    {
      AddItem(status, status.Text());
    }
  }
}
