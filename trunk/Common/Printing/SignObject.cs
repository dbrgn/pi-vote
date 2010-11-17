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

namespace Pirate.PiVote.Printing
{
  public class SignObject : DrawObject
  {
    private const float LineHalf = 70;
    private const float DateSpace = 20;
    private const float SignSpace = 30;

    private Graphics graphics;

    public string SubjectText { get; set; }

    public string SignatureText { get; set; }

    public string DateText { get; set; }
    
    public Font Font { get; set; }

    public Brush Brush { get; set; }

    public PointF Position { get; set; }

    public SignObject(Graphics graphics, string subjectText, string signatureText, string dateText, Font font)
    {
      this.graphics = graphics;
      SubjectText = subjectText;
      DateText = dateText;
      SignatureText = signatureText;
      Font = font;
      Brush = Brushes.Black;
    }

    public void SetCenterTop(float center, float top)
    {
      Position = new PointF(center, top);
    }

    public override void Draw()
    {
      float y = Position.Y;
      float center = Position.X;

      StringObject date = new StringObject(this.graphics, DateText, Font);
      date.SetCenterTop(center, y);
      date.Draw();
      y += date.Size.Height;

      y += DateSpace;
      this.graphics.DrawLine(new Pen(Color.Black, 1), center - LineHalf, y, center + LineHalf, y);

      StringObject signature = new StringObject(this.graphics, SignatureText, Font);
      signature.SetCenterTop(center, y);
      signature.Draw();
      y += signature.Size.Height;

      StringObject subject = new StringObject(this.graphics, SubjectText, Font);
      subject.SetCenterTop(center, y);
      subject.Draw();
      y += subject.Size.Height;

      y += SignSpace;
      this.graphics.DrawLine(new Pen(Color.Black, 1), center - LineHalf, y, center + LineHalf, y);
    }
  }
}
