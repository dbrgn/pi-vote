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
using Pirate.PiVote.Crypto;
using PdfSharp;
using PdfSharp.Drawing;

namespace Pirate.PiVote.Gui.Printing
{
  public class StringObject : DrawObject
  {
    private XGraphics graphics;

    public string Text { get; set; }

    public XFont Font { get; set; }

    public Brush Brush { get; set; }

    public XPoint Position { get; set; }

    public XSize Size
    {
      get
      {
        if (Text.Trim().Length > 0)
        {
          return new XSize(this.graphics.MeasureString(Text, Font).Width, Font.Height);
        }
        else
        {
          return new XSize(this.graphics.MeasureString("X", Font).Width, Font.Height);
        }
      }
    }

    public StringObject(XGraphics graphics, string text, XFont font)
    {
      this.graphics = graphics;
      Text = text;
      Font = font;
      Brush = Brushes.Black;
    }

    public double Height
    {
      get
      {
        return this.graphics.MeasureString(Text, Font).Height;
      }
    }

    public override void Draw()
    {
      if (Text.Trim().Length > 0)
      {
        this.graphics.DrawString(Text, Font, Brush, Position.X, Position.Y);
      }
    }

    public void SetLeftTop(double left, double top)
    {
      Position = new XPoint(left, top);
    }

    public void SetRightTop(double right, double top)
    {
      Position = new XPoint(right - Size.Width, top);
    }

    public void SetLeftBottom(float left, float bottom)
    {
      Position = new XPoint(left, bottom - Size.Height);
    }

    public void SetRightBottom(double right, double bottom)
    {
      Position = new XPoint(right - Size.Width, bottom - Size.Height);
    }

    public void SetCenterTop(double centerX, double top)
    {
      Position = new XPoint(centerX - Size.Width / 2f, top);
    }

    public void SetCenterCenter(double centerX, double centerY)
    {
      Position = new XPoint(centerX - Size.Width / 2f, centerY - Size.Height / 2f);
    }
  }
}
