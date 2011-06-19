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
using System.Windows.Forms;
using Pirate.PiVote;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.CaGui
{
  public class CertificateTypeBox : TypedBox<CertificateType>
  {
    public CertificateTypeBox()
    {
      AddItem(CertificateType.All);
      AddItem(CertificateType.CA);
      AddItem(CertificateType.Admin);
      AddItem(CertificateType.Authority);
      AddItem(CertificateType.Voter);
      AddItem(CertificateType.Server);
      AddItem(CertificateType.Notary);

      SelectedIndex = 0;
    }

    private void AddItem(CertificateType type)
    {
      AddItem(type, type.Text());
    }
  }
}
