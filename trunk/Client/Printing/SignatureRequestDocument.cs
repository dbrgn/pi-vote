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
    private const string FontFace = "Arial";

    private SignatureRequest signatureRequest;
    private Certificate certificate;
    private Graphics graphics;

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
      this.graphics = e.Graphics;

      this.graphics.Clear(Color.White);

      this.top = e.MarginBounds.Top;

      PrintHeader(Snippet(e.MarginBounds, 50f));
      PrintData(Snippet(e.MarginBounds, 200f));
      PrintRequest(Snippet(e.MarginBounds, 150f));
      PrintResponse(Snippet(e.MarginBounds, 150f));
      PrintRevoke(Snippet(e.MarginBounds, 150f));
      PrintFooter(Snippet(e.MarginBounds, 50f));

      e.HasMorePages = false;
    }

    private float top;

    private RectangleF Snippet(RectangleF pageBounds, float size)
    {
      RectangleF area = new RectangleF(pageBounds.Left, this.top, pageBounds.Width, this.top + size);
      this.top += size;
      return area;
    }

    private void PrintHeader(RectangleF bounds)
    {
      float space = 2f;
      Font headerFont = new Font(FontFace, 14, FontStyle.Bold);

      SizeF partySize = graphics.MeasureString(Resources.SigningRequestDocumentHeaderRight, headerFont);

      this.graphics.DrawLine(new Pen(Color.Black, 2), bounds.Left, bounds.Top + partySize.Height + space, bounds.Right, bounds.Top + partySize.Height + space);
      this.graphics.DrawString(Resources.SigningRequestDocumentHeaderLeft, headerFont, Brushes.Black, bounds.Left, bounds.Top);
      this.graphics.DrawString(Resources.SigningRequestDocumentHeaderRight, headerFont, Brushes.Black, bounds.Right - partySize.Width, bounds.Top);
    }

    private void PrintData(RectangleF bounds)
    {
      Table table = new Table(new Font(FontFace, 12));
      table.AddColumn(240f);
      table.AddColumn(bounds.Width - 240f);

      table.AddRow(Resources.SigningRequestDocumentRequest, 2, FontStyle.Bold);
      table.AddRow(" ", 2);
      table.AddRow(Resources.SigningRequestDocumentFamilyName, this.signatureRequest.FamilyName);
      table.AddRow(Resources.SigningRequestDocumentFirstName, this.signatureRequest.FirstName);
      table.AddRow(Resources.SigningRequestDocumentEmailAddress, this.signatureRequest.EmailAddress);
      if (this.certificate is VoterCertificate)
        table.AddRow(Resources.SigningRequestDocumentCanton, ((VoterCertificate)this.certificate).Canton.Text());
      table.AddRow(Resources.SigningRequestDocumentCertificateId, this.certificate.Id.ToString());
      table.AddRow(Resources.SigningRequestDocumentCertificateFingerprint, this.certificate.Fingerprint);

      table.Draw(new PointF(bounds.Left, bounds.Top), this.graphics);
    }

    private void PrintRequest(RectangleF bounds)
    {
      Font font = new Font(FontFace, 12);
      float eights = bounds.Width / 8f;

      SignObject requesterSign = new SignObject(this.graphics, Resources.SigningRequestDocumentSignRequester, Resources.SigningRequestDocumentSignSignature, Resources.SigningRequestDocumentSignDate, font);
      requesterSign.SetCenterTop(bounds.Left + eights, bounds.Top);
      requesterSign.Draw();

      SignObject firstAuthoritySign = new SignObject(this.graphics, Resources.SigningRequestDocumentSignFirstAuthority, Resources.SigningRequestDocumentSignSignature, Resources.SigningRequestDocumentSignDate, font);
      firstAuthoritySign.SetCenterTop(bounds.Left + 3 * eights, bounds.Top);
      firstAuthoritySign.Draw();

      SignObject secondAuthoritySign = new SignObject(this.graphics, Resources.SigningRequestDocumentSignSecondAuthority, Resources.SigningRequestDocumentSignSignature, Resources.SigningRequestDocumentSignDate, font);
      secondAuthoritySign.SetCenterTop(bounds.Left + 5 * eights, bounds.Top);
      secondAuthoritySign.Draw();

      SignObject thirdAuthoritySign = new SignObject(this.graphics, Resources.SigningRequestDocumentSignThirdAuthority, Resources.SigningRequestDocumentSignSignature, Resources.SigningRequestDocumentSignDate, font);
      thirdAuthoritySign.SetCenterTop(bounds.Left + 7 * eights, bounds.Top);
      thirdAuthoritySign.Draw();
    }

    private void PrintResponse(RectangleF bounds)
    {
      Font font = new Font(FontFace, 12);

      Table table = new Table(font);
      table.AddColumn(bounds.Width / 6f * 4f);
      table.AddRow(Resources.SigningRequestDocumentAccepted);
      table.AddRow(Resources.SigningRequestDocumentRefusedNoPirate);
      table.AddRow(Resources.SigningRequestDocumentRefusedHasCertificate);
      table.Draw(new PointF(bounds.Left, bounds.Top), this.graphics);

      SignObject caSign = new SignObject(this.graphics, Resources.SigningRequestDocumentSignCA, Resources.SigningRequestDocumentSignSignature, Resources.SigningRequestDocumentSignDate, font);
      caSign.SetCenterTop(bounds.Left + bounds.Width / 8f * 7f, bounds.Top);
      caSign.Draw();
    }

    private void PrintRevoke(RectangleF bounds)
    {
      Font font = new Font(FontFace, 12);

      Table table = new Table(font);
      table.AddColumn(bounds.Width / 6f * 4f);
      table.AddRow(Resources.SigningRequestDocumentRevokedLost);
      table.AddRow(Resources.SigningRequestDocumentRevokedStolen);
      table.AddRow(Resources.SigningRequestDocumentRevokedNoLonger);
      table.AddRow(Resources.SigningRequestDocumentRevokedMoved);
      table.Draw(new PointF(bounds.Left, bounds.Top), this.graphics);

      SignObject caSign = new SignObject(this.graphics, Resources.SigningRequestDocumentSignCA, Resources.SigningRequestDocumentSignSignature, Resources.SigningRequestDocumentSignDate, font);
      caSign.SetCenterTop(bounds.Left + bounds.Width / 8f * 7f, bounds.Top);
      caSign.Draw();
    }

    private void PrintFooter(RectangleF bounds)
    {
    }
  }
}
