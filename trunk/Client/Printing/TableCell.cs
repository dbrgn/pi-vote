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
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Client
{
  public class TableCell
  {
    public string Text { get; set; }

    public HorizontalAlignment Alignment { get; set; }

    public Font Font { get; set; }

    public int ColumnSpan { get; set; }

    public TableCell(string text, Font font)
      : this(text, font, 1)
    { }

    public TableCell(string text, Font font, int columnSpan)
      : this(text, font, columnSpan, HorizontalAlignment.Left)
    { }

    public TableCell(string text, Font font, int columnSpan, HorizontalAlignment alignment)
    {
      Text = text;
      Font = font;
      Alignment = alignment;
      ColumnSpan = columnSpan;
    }

    public float Draw(Graphics graphics, RectangleF bounds)
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

      stringObject.Width = bounds.Width;

      stringObject.Draw();

      return stringObject.Size.Height;
    }
  }
}
