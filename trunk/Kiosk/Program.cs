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
using System.Diagnostics;

namespace Pirate.PiVote.Kiosk
{
  public static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    public static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName + ".vshost").Count() > 0)
      {
        Application.Run(new KioskForm());
      }
      else
      {
        Application.Run(new ControlForm());
      }
    }
  }
}
