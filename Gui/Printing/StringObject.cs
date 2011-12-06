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

namespace Pirate.PiVote.Gui.Printing
{
  public class StringObject : DrawObject
  {
    private Graphics graphics;

    public string Text { get; set; }

    public Font Font { get; set; }

    public Brush Brush { get; set; }

    public PointF Position { get; set; }

    public SizeF Size
    {
      get
      {
        return new SizeF(this.graphics.MeasureString(Text, Font).Width, Font.Height);
      }
    }

    public StringObject(Graphics graphics, string text, Font font)
    {
      this.graphics = graphics;
      Text = text;
      Font = font;
      Brush = Brushes.Black;
    }

    public override void Draw()
    {
      this.graphics.DrawString(Text, Font, Brush, Position.X, Position.Y);
    }

    public void SetLeftTop(float left, float top)
    {
      Position = new PointF(left, top);
    }

    public void SetRightTop(float right, float top)
    {
      Position = new PointF(right - Size.Width, top);
    }

    public void SetLeftBottom(float left, float bottom)
    {
      Position = new PointF(left, bottom - Size.Height);
    }

    public void SetRightBottom(float right, float bottom)
    {
      Position = new PointF(right - Size.Width, bottom - Size.Height);
    }

    public void SetCenterTop(float centerX, float top)
    {
      Position = new PointF(centerX - Size.Width / 2f, top);
    }

    public void SetCenterCenter(float centerX, float centerY)
    {
      Position = new PointF(centerX - Size.Width / 2f, centerY - Size.Height / 2f);
    }
  }
}
