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
using System.Drawing;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Gui.Printing
{
  public static class DocumentTest
  {
    public static void TestSignatureRequestDocument()
    {
      VoterCertificate voterCert = new VoterCertificate(Language.English, null, 0);
      voterCert.CreateSelfSignature();
      SignatureRequest request = new SignatureRequest("Hans", "Müller", "hans@mueller.ch");
      SignatureRequestInfo requestInfo = new SignatureRequestInfo("hans@mueller.ch", request.Encrypt());

      SignatureRequestDocument document = new SignatureRequestDocument(request, voterCert, GetGroupName);

      PrintDialog dialog = new PrintDialog();
      dialog.Document = document;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        document.Print();
      }
    }

    private static string GetGroupName(int groupId)
    {
      return "TestGroup";
    }
  }
}
