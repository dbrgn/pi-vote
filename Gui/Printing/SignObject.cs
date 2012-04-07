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
  public class SignObject : DrawObject
  {
    private const double LineHalf = 50d;
    private const double DateSpace = 15d;
    private const double SignSpace = 25d;

    private XGraphics graphics;

    public string SubjectText { get; set; }

    public string SignatureText { get; set; }

    public string DateText { get; set; }
    
    public XFont Font { get; set; }

    public Brush Brush { get; set; }

    public XPoint Position { get; set; }

    public SignObject(XGraphics graphics, string subjectText, string signatureText, string dateText, XFont font)
    {
      this.graphics = graphics;
      SubjectText = subjectText;
      DateText = dateText;
      SignatureText = signatureText;
      Font = font;
      Brush = Brushes.Black;
    }

    public void SetCenterTop(double center, double top)
    {
      Position = new XPoint(center, top);
    }

    public override void Draw()
    {
      var y = Position.Y;
      var center = Position.X;

      StringObject date = new StringObject(this.graphics, DateText, Font);
      date.SetCenterTop(center, y);
      date.Draw();
      y += date.Size.Height;

      y += DateSpace;
      this.graphics.DrawLine(new Pen(Color.Black, 1), center - LineHalf, y - date.Height, center + LineHalf, y - date.Height);

      StringObject signature = new StringObject(this.graphics, SignatureText, Font);
      signature.SetCenterTop(center, y);
      signature.Draw();
      y += signature.Size.Height;

      StringObject subject = new StringObject(this.graphics, SubjectText, Font);
      subject.SetCenterTop(center, y);
      subject.Draw();
      y += subject.Size.Height;

      y += SignSpace;
      this.graphics.DrawLine(new Pen(Color.Black, 1), center - LineHalf, y - subject.Height, center + LineHalf, y - subject.Height);
    }
  }
}
