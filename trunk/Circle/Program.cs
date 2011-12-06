/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Pirate.PiVote.Circle
{
  public static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    public static void Main()
    {
      var req = Pirate.PiVote.Serialization.Serializable.Load<Pirate.PiVote.Crypto.Secure<Pirate.PiVote.Crypto.SignatureRequest>>(@"d:\downloads\apo-req.bin");
      req.Certificate.Save(@"d:\downloads\apo-cert.bin");
      return;

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new Master());
    }
  }
}
