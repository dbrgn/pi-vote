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
  public class SignatureRequestDocument : PrintDocument
  {
    private SignatureRequest signatureRequest;
    private Certificate certificate;

    public SignatureRequestDocument(SignatureRequest signatureRequest, Certificate certificate)
    {
      if (signatureRequest == null)
        throw new ArgumentNullException("signatureRequest");
      if (certificate == null)
        throw new ArgumentNullException("certificate");

      this.signatureRequest = signatureRequest;
      this.certificate = certificate;

      DocumentName = "Certificate Request";
      OriginAtMargins = false;
    }

    protected override void OnPrintPage(PrintPageEventArgs e)
    {
      e.Graphics.Clear(Color.White);

      this.top = e.MarginBounds.Top;

      PrintHeader(e.Graphics, Snippet(e.MarginBounds, 50f));
      PrintData(e.Graphics, Snippet(e.MarginBounds, 200f));
      PrintRequest(e.Graphics, Snippet(e.MarginBounds, 150f));
      PrintResponse(e.Graphics, Snippet(e.MarginBounds, 150f));
      PrintRevoke(e.Graphics, Snippet(e.MarginBounds, 150f));
      PrintFooter(e.Graphics, Snippet(e.MarginBounds, 50f));

      e.HasMorePages = false;
    }

    private float top;

    private RectangleF Snippet(RectangleF pageBounds, float size)
    {
      RectangleF area = new RectangleF(pageBounds.Left, this.top, pageBounds.Width, this.top + size);
      this.top += size;
      return area;
    }

    private void PrintHeader(Graphics graphics, RectangleF bounds)
    {
      string piVoteString = "π-Vote";
      string partyString = "Pirate Party Switzerland";
      float space = 2f;
      Font headerFont = new Font("Arial", 14, FontStyle.Bold);

      SizeF partySize = graphics.MeasureString(partyString, headerFont);

      graphics.DrawLine(new Pen(Color.Black, 2), bounds.Left, bounds.Top + partySize.Height + space, bounds.Right, bounds.Top + partySize.Height + space);
      graphics.DrawString(piVoteString, headerFont, Brushes.Black, bounds.Left, bounds.Top);
      graphics.DrawString(partyString, headerFont, Brushes.Black, bounds.Right - partySize.Width, bounds.Top);
    }

    private void PrintData(Graphics graphics, RectangleF bounds)
    {
      string familyNameString = "Family name:";
      string firstNameString = "First name:";
      string emailAddressString = "Email address:";
      string certificateIdString = "Certificate id:";
      string certificateFingerPrintString = "Certificate fingerprint:";

      this.lineSpace = 2f;
      this.lineTop = bounds.Top;
      this.lineTabs = new float[] { bounds.Left, bounds.Left + 200f, bounds.Right };
      this.lineFont = new Font("Arial", 12);
      this.lineGraphics = graphics;
      this.lineMaxHeight = 300f;

      PrintLine(familyNameString, this.signatureRequest.FamilyName);
      PrintLine(firstNameString, this.signatureRequest.FirstName);
      PrintLine(emailAddressString, this.signatureRequest.EmailAddress);
      PrintLine(certificateIdString, this.certificate.Id.ToString());
      PrintLine(certificateFingerPrintString, this.certificate.Fingerprint);
    }

    private float lineSpace;
    private float lineTop;
    private float[] lineTabs;
    private Font lineFont;
    private Graphics lineGraphics;
    private float lineMaxHeight;

    private void PrintLine(params string[] texts)
    { 
      for (int index = 0; index < texts.Length; index++)
      {
        string text = texts[index];
        float tab = this.lineTabs[index];
        this.lineGraphics.DrawString(text, this.lineFont, Brushes.Black, new RectangleF(tab, this.lineTop, this.lineTabs[index + 1] - this.lineTabs[index], this.lineMaxHeight));
      }

      this.lineTop += this.lineSpace + this.lineGraphics.MeasureString("X", this.lineFont).Height;
    }

    private void PrintRequest(Graphics graphics, RectangleF bounds)
    {
      string signString = "Signature";
      string requesterString = "Requester";
      string firstAuthorityString = "First Authority";
      string secondAuthorityString = "Second Authority";
      float sixth = bounds.Width / 6f;

      this.lineSpace = 2f;
      this.lineTop = bounds.Top;
      this.lineFont = new Font("Arial", 12);
      this.lineGraphics = graphics;
      this.lineMaxHeight = 300f;

      float signWidth = graphics.MeasureString(signString, this.lineFont).Width;
      this.lineTabs = new float[] { 
        bounds.Left + sixth - signWidth / 2f, 
        bounds.Left + 3 * sixth - signWidth / 2f, 
        bounds.Left + 5 * sixth - signWidth / 2f, bounds.Right };
      PrintLine(signString, signString, signString);

      float requesterWidth = graphics.MeasureString(requesterString, this.lineFont).Width;
      float firstAuthorityWidth = graphics.MeasureString(firstAuthorityString, this.lineFont).Width;
      float secondAuthorityWidth = graphics.MeasureString(secondAuthorityString, this.lineFont).Width;
      this.lineTabs = new float[] { 
        bounds.Left + sixth - requesterWidth / 2f, 
        bounds.Left + 3 * sixth - firstAuthorityWidth / 2f, 
        bounds.Left + 5 * sixth - secondAuthorityWidth / 2f, 
        bounds.Right };
      PrintLine(requesterString, firstAuthorityString, secondAuthorityString);

      Pen linePen = new Pen(Color.Black, 1);
      float lineHalf = 70f;
      float lineDown = 40f;
      graphics.DrawLine(linePen, bounds.Left + sixth - lineHalf, this.lineTop + lineDown, bounds.Left + sixth + lineHalf, this.lineTop + lineDown);
      graphics.DrawLine(linePen, bounds.Left + 3 * sixth - lineHalf, this.lineTop + lineDown, bounds.Left + 3 * sixth + lineHalf, this.lineTop + lineDown);
      graphics.DrawLine(linePen, bounds.Left + 5 * sixth - lineHalf, this.lineTop + lineDown, bounds.Left + 5 * sixth + lineHalf, this.lineTop + lineDown);
    }

    private void PrintResponse(Graphics graphics, RectangleF bounds)
    {
      string acceptedString = "□ Accepted";
      string refusedNoPirateString = "□ Refused, no pirate";
      string refusedHasCertificateString = "□ Refused, already has valid certificate";
      string signString = "Signature";
      string caString = "Certificate Authority";

      this.lineSpace = 2f;
      this.lineTop = bounds.Top;
      this.lineTabs = new float[] { bounds.Left, bounds.Left + 200f, bounds.Right };
      this.lineFont = new Font("Arial", 12);
      this.lineGraphics = graphics;
      this.lineMaxHeight = 300f;

      float signCenter = bounds.Left + bounds.Width / 6f * 5f;

      float signWidth = graphics.MeasureString(signString, this.lineFont).Width;
      this.lineTabs = new float[] { bounds.Left, signCenter - signWidth / 2f, bounds.Right };
      PrintLine(acceptedString, signString);

      float caWidth = graphics.MeasureString(caString, this.lineFont).Width;
      this.lineTabs = new float[] { bounds.Left, signCenter - caWidth / 2f, bounds.Right };
      PrintLine(refusedNoPirateString, caString);

      Pen linePen = new Pen(Color.Black, 1);
      float lineHalf = 70f;
      float lineDown = 40f;
      graphics.DrawLine(linePen, signCenter - lineHalf, this.lineTop + lineDown, signCenter + lineHalf, this.lineTop + lineDown);

      PrintLine(refusedHasCertificateString);
    }

    private void PrintRevoke(Graphics graphics, RectangleF bounds)
    {
      string lostString = "□ Revoked, presumed lost";
      string stolenString = "□ Revoked, presumed stolen";
      string noLongerString = "□ Revoked, no longer pirate";
      string signString = "Signature";
      string caString = "Certificate Authority";

      this.lineSpace = 2f;
      this.lineTop = bounds.Top;
      this.lineTabs = new float[] { bounds.Left, bounds.Left + 200f, bounds.Right };
      this.lineFont = new Font("Arial", 12);
      this.lineGraphics = graphics;
      this.lineMaxHeight = 300f;

      float signCenter = bounds.Left + bounds.Width / 6f * 5f;

      float lostWidth = graphics.MeasureString(signString, this.lineFont).Width;
      this.lineTabs = new float[] { bounds.Left, signCenter - lostWidth / 2f, bounds.Right };
      PrintLine(lostString, signString);

      float stolenWidth = graphics.MeasureString(caString, this.lineFont).Width;
      this.lineTabs = new float[] { bounds.Left, signCenter - stolenWidth / 2f, bounds.Right };
      PrintLine(stolenString, caString);

      Pen linePen = new Pen(Color.Black, 1);
      float lineHalf = 70f;
      float lineDown = 40f;
      graphics.DrawLine(linePen, signCenter - lineHalf, this.lineTop + lineDown, signCenter + lineHalf, this.lineTop + lineDown);

      PrintLine(noLongerString);
    }

    private void PrintFooter(Graphics graphics, RectangleF bounds)
    {
    }
  }
}
