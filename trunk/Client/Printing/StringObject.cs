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
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Client
{
  public class StringObject : DrawObject
  {
    private Graphics graphics;

    public float Width { get; set; }

    public string Text { get; set; }

    public Font Font { get; set; }

    public Brush Brush { get; set; }

    public PointF Position { get; set; }

    public SizeF Size { get { return this.graphics.MeasureString(Text, Font, new SizeF(Width, float.MaxValue)); } }

    public StringObject(Graphics graphics, string text, Font font)
    {
      this.graphics = graphics;
      Text = text;
      Font = font;
      Brush = Brushes.Black;
      Width = float.MaxValue;
    }

    public override void Draw()
    {
      if (Width == float.MaxValue)
      {
        this.graphics.DrawString(Text, Font, Brush, Position.X, Position.Y);
      }
      else
      {
        this.graphics.DrawString(Text, Font, Brush, new RectangleF(Position.X, Position.Y, Width, 10000f));
      }
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
