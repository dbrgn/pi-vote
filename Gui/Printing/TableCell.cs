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
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using PdfSharp;
using PdfSharp.Drawing;

namespace Pirate.PiVote.Gui.Printing
{
  public class TableCell
  {
    public string Text { get; set; }

    public HorizontalAlignment Alignment { get; set; }

    public XFont Font { get; set; }

    public int ColumnSpan { get; set; }

    public TableCell(string text, XFont font)
      : this(text, font, 1)
    { }

    public TableCell(string text, XFont font, int columnSpan)
      : this(text, font, columnSpan, HorizontalAlignment.Left)
    { }

    public TableCell(string text, XFont font, int columnSpan, HorizontalAlignment alignment)
    {
      Text = text;
      Font = font;
      Alignment = alignment;
      ColumnSpan = columnSpan;
    }

    public double Draw(XGraphics graphics, XRect bounds)
    {
      StringObject stringObject = new StringObject(graphics, Text, Font);

      switch (Alignment)
      {
        case HorizontalAlignment.Left:
          stringObject.SetLeftTop(bounds.Left, bounds.Top);
          break;
        case HorizontalAlignment.Center:
          stringObject.SetCenterTop(bounds.Left + bounds.Width / 2f, bounds.Top);
          break;
        case HorizontalAlignment.Right:
          stringObject.SetRightTop(bounds.Right, bounds.Top);
          break;
      }

      stringObject.Draw();

      return stringObject.Size.Height;
    }
  }
}
