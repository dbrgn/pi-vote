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
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Gui;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Circle.Create
{
  public class CreateDialogStatus
  {
    public CircleController Controller { get; private set; }

    public SignatureRequest SignatureRequest { get; set; }

    public SignatureRequestInfo SignatureRequestInfo { get; set; }

    public Certificate Certificate { get; set; }

    public string SignatureRequestFileName { get; set; }

    public string SignatureRequestInfoFileName { get; set; }

    public CreateDialogStatus(CircleController controller)
    {
      Controller = controller;
    }

    public bool TryLoadSignatureRequest()
    {
      SignatureRequestFileName = Path.Combine(Controller.Status.DataPath, Certificate.Id.ToString() + Files.SignatureRequestDataExtension);
      SignatureRequestInfoFileName = Path.Combine(Controller.Status.DataPath, Certificate.Id.ToString() + Files.SignatureRequestInfoExtension);

      if (File.Exists(SignatureRequestFileName) &&
          File.Exists(SignatureRequestInfoFileName))
      {
        SignatureRequest = Serializable.Load<SignatureRequest>(SignatureRequestFileName);
        SignatureRequestInfo = Serializable.Load<SignatureRequestInfo>(SignatureRequestInfoFileName);
        return true;
      }
      else
      {
        Controller.DeactiveCertificate(Certificate);
        MessageForm.Show(
          string.Format("Signature request data for your certificate {0} of type {1} count not be found. You must create a new certificate.", Certificate.Id.ToString(), Certificate.TypeText),
          Resources.MessageBoxTitle,
          MessageBoxButtons.OK,
          MessageBoxIcon.Information);
        return false;
      }
    }
  }
}
