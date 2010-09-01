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
using System.Drawing;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Client
{
  public static class DocumentTest
  {
    public static void TestSignatureRequestDocument()
    {
      VoterCertificate voterCert = new VoterCertificate(Language.English, null, 0);
      voterCert.CreateSelfSignature();
      SignatureRequest request = new SignatureRequest("Hans", "Müller", "hans@mueller.ch");
      WizardStatus status = new WizardStatus(null, null);
      status.Groups = new Group[] { new Group(0, new MultiLanguageString("North")) };
      
      SignatureRequestDocument document = new SignatureRequestDocument(request, voterCert, status);

      PrintDialog dlg = new PrintDialog();
      dlg.Document = document;

      if (dlg.ShowDialog() == DialogResult.OK)
      {
        document.Print();
      }
    }
  }
}
