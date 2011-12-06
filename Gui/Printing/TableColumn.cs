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
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Gui.Printing
{
  public class TableColumn
  {
    public float Width { get; set; }

    public TableColumn(float width)
    {
      Width = width;
    }
  }
}
