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
    private const string FontFace = "DejaVu Sans";
    private const int BaseFontSize = 12;

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
      PrintData(Snippet(e.MarginBounds, 300f));
      PrintRequest(Snippet(e.MarginBounds, 220f));
      PrintResponse(Snippet(e.MarginBounds, 220f));
      PrintRevoke(Snippet(e.MarginBounds, 220f));
      PrintFooter(Snippet(e.MarginBounds, 10f));

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
      Font headerFont = new Font(FontFace, BaseFontSize + 2, FontStyle.Bold);

      SizeF partySize = graphics.MeasureString(Resources.SigningRequestDocumentHeaderRight, headerFont);

      this.graphics.DrawLine(new Pen(Color.Black, 2), bounds.Left, bounds.Top + partySize.Height + space, bounds.Right, bounds.Top + partySize.Height + space);
      this.graphics.DrawString(Resources.SigningRequestDocumentHeaderLeft, headerFont, Brushes.Black, bounds.Left, bounds.Top);
      this.graphics.DrawString(Resources.SigningRequestDocumentHeaderRight, headerFont, Brushes.Black, bounds.Right - partySize.Width, bounds.Top);
    }

    private void PrintData(RectangleF bounds)
    {
      Table table = new Table(new Font(FontFace, BaseFontSize));
      table.AddColumn(200f);
      table.AddColumn(bounds.Width - 200f);

      table.AddRow(Resources.SigningRequestDocumentRequest, 2, FontStyle.Bold);
      table.AddRow(" ", 2);
      table.AddRow(Resources.SigningRequestDocumentFamilyName, this.signatureRequest.FamilyName);
      table.AddRow(Resources.SigningRequestDocumentFirstName, this.signatureRequest.FirstName);
      table.AddRow(Resources.SigningRequestDocumentEmailAddress, this.signatureRequest.EmailAddress);
      if (this.certificate is VoterCertificate)
        table.AddRow(Resources.SigningRequestDocumentCanton, ((VoterCertificate)this.certificate).Canton.Text());
      table.AddRow(Resources.SigningRequestDocumentCertificateType, this.certificate.TypeText);
      table.AddRow(Resources.SigningRequestDocumentCertificateId, this.certificate.Id.ToString());

      string fingerprint = this.certificate.Fingerprint;
      string fingerprintLine1 = ReworkFingerprintLine(fingerprint.Substring(0, fingerprint.Length / 2));
      string fingerprintLine2 = ReworkFingerprintLine(fingerprint.Substring(fingerprint.Length / 2 + 1));
      table.AddRow(Resources.SigningRequestDocumentCertificateFingerprint, fingerprintLine1);
      table.AddRow(string.Empty, fingerprintLine2);

      table.Draw(new PointF(bounds.Left, bounds.Top), this.graphics);
    }

    private void PrintRequest(RectangleF bounds)
    {
      Font font = new Font(FontFace, BaseFontSize);
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

    private string ReworkFingerprintLine(string line)
    {
      string[] parts = line.Split(new string[] { " " }, StringSplitOptions.None);
      string newLine = string.Empty;

      for (int index = 0; index < parts.Length; index++)
      {
        newLine += parts[index] + ((index % 2) == 1 ? " " : string.Empty);
      }

      return newLine;
    }

    private void PrintResponse(RectangleF bounds)
    {
      Font font = new Font(FontFace, BaseFontSize);

      Table table = new Table(font);
      table.AddColumn(bounds.Width);
      table.AddRow(Resources.SigningRequestDocumentAccepted);
      table.AddRow(Resources.SigningRequestDocumentRefusedFingerprintMismatch);
      if (this.certificate is VoterCertificate)
      {
        table.AddRow(Resources.SigningRequestDocumentRefusedNoPirate);
      }
      else
      {
        table.AddRow(Resources.SigningRequestDocumentRefusedNotFx);
      }
      table.AddRow(Resources.SigningRequestDocumentRefusedHasCertificate);
      table.Draw(new PointF(bounds.Left, bounds.Top), this.graphics);

      SignObject caSign = new SignObject(this.graphics, Resources.SigningRequestDocumentSignCA, Resources.SigningRequestDocumentSignSignature, Resources.SigningRequestDocumentSignDate, font);
      caSign.SetCenterTop(bounds.Left + bounds.Width / 8f * 7f, bounds.Top);
      caSign.Draw();
    }

    private void PrintRevoke(RectangleF bounds)
    {
      Font font = new Font(FontFace, BaseFontSize);

      Table table = new Table(font);
      table.AddColumn(bounds.Width);
      table.AddRow(Resources.SigningRequestDocumentRevokedForgotten);
      table.AddRow(Resources.SigningRequestDocumentRevokedLost);
      table.AddRow(Resources.SigningRequestDocumentRevokedStolen);
      if (this.certificate is VoterCertificate)
      {
        table.AddRow(Resources.SigningRequestDocumentRevokedNoLonger);
        table.AddRow(Resources.SigningRequestDocumentRevokedMoved);
      }
      else
      {
        table.AddRow(Resources.SigningRequestDocumentRevokedNoMoreFx);
      }
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
