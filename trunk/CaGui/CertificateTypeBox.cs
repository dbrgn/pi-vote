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

      SelectedIndex = 0;
    }

    private void AddItem(CertificateType type)
    {
      AddItem(type, type.Text());
    }
  }
}
