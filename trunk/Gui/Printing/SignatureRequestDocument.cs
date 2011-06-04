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
using ThoughtWorks.QRCode.Codec;

namespace Pirate.PiVote.Gui.Printing
{
  public delegate string GetGroupNameHandler(int groupId);

  public class SignatureRequestDocument : PrintDocument
  {
    private const string FontFace = "DejaVu Sans";
    private const int BaseFontSize = 12;

    private SignatureRequest signatureRequest;
    private Certificate certificate;
    private Graphics graphics;
    private GetGroupNameHandler getGroupName;

    public SignatureRequestDocument(SignatureRequest signatureRequest, Certificate certificate, GetGroupNameHandler getGroupName)
    {
      if (signatureRequest == null)
        throw new ArgumentNullException("signatureRequest");
      if (certificate == null)
        throw new ArgumentNullException("certificate");

      this.signatureRequest = signatureRequest;
      this.certificate = certificate;
      this.getGroupName = getGroupName;

      DocumentName = "Certificate Request";
      OriginAtMargins = false;
    }

    protected override void OnPrintPage(PrintPageEventArgs e)
    {
      this.graphics = e.Graphics;

      this.graphics.Clear(Color.White);

      this.top = e.MarginBounds.Top;

      PrintHeader(Snippet(e.MarginBounds, 50f));
      PrintData(Snippet(e.MarginBounds, 360f));

      if (this.signatureRequest is SignatureRequest2)
      {
        PrintParentData(Snippet(e.MarginBounds, 200f));
      }
      else
      {
        PrintRequest(Snippet(e.MarginBounds, 200f));
      }

      PrintResponse(Snippet(e.MarginBounds, 200f));
      PrintRevoke(Snippet(e.MarginBounds, 200f));
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

      SizeF partySize = graphics.MeasureString(LibraryResources.SigningRequestDocumentHeaderRight, headerFont);

      this.graphics.DrawLine(new Pen(Color.Black, 2), bounds.Left, bounds.Top + partySize.Height + space, bounds.Right, bounds.Top + partySize.Height + space);
      this.graphics.DrawString(LibraryResources.SigningRequestDocumentHeaderLeft, headerFont, Brushes.Black, bounds.Left, bounds.Top);
      this.graphics.DrawString(LibraryResources.SigningRequestDocumentHeaderRight, headerFont, Brushes.Black, bounds.Right - partySize.Width, bounds.Top);
    }

    private void PrintData(RectangleF bounds)
    {
      QRCodeEncoder qrEncoder = new QRCodeEncoder();
      qrEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
      qrEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
      qrEncoder.QRCodeVersion = 12;
      qrEncoder.QRCodeScale = 3;
      string url = string.Format(
        "https://pivote.piratenpartei.ch/sign.aspx?id={0}&fp={1}",
        this.certificate.Id.ToString(),
        this.certificate.Fingerprint.Replace(" ", string.Empty));
      var image = qrEncoder.Encode(url);

      this.graphics.DrawImage(image, bounds.Right - image.Width, bounds.Top);

      Table table = new Table(new Font(FontFace, BaseFontSize));
      table.AddColumn(150f);
      table.AddColumn(bounds.Width - 150f);

      table.AddRow(LibraryResources.SigningRequestDocumentRequest, 2, FontStyle.Bold);

      table.AddRow(" ", 2);

      table.AddRow(LibraryResources.SigningRequestDocumentFamilyName, this.signatureRequest.FamilyName);
      table.AddRow(LibraryResources.SigningRequestDocumentFirstName, this.signatureRequest.FirstName);
      table.AddRow(LibraryResources.SigningRequestDocumentEmailAddress, this.signatureRequest.EmailAddress);
      if (this.certificate is VoterCertificate)
        table.AddRow(LibraryResources.SigningRequestDocumentGroup, this.getGroupName(((VoterCertificate)this.certificate).GroupId));
      table.AddRow(LibraryResources.SigningRequestDocumentCertificateType, this.certificate.TypeText);

      string certificateId = this.certificate.Id.ToString();
      int idLength = certificateId.Length / 2;
      table.AddRow(LibraryResources.SigningRequestDocumentCertificateId, certificateId.Substring(0, idLength));
      table.AddRow(string.Empty, certificateId.Substring(idLength));

      string fingerprint = this.certificate.Fingerprint.Replace(" ", string.Empty);
      int fingerprintLength = fingerprint.Length / 4;
      table.AddRow(LibraryResources.SigningRequestDocumentCertificateFingerprint, FourBlocks(fingerprint.Substring(0, fingerprintLength)));
      table.AddRow(string.Empty, FourBlocks(fingerprint.Substring(fingerprintLength, fingerprintLength)));
      table.AddRow(string.Empty, FourBlocks(fingerprint.Substring(fingerprintLength * 2, fingerprintLength)));
      table.AddRow(string.Empty, FourBlocks(fingerprint.Substring(fingerprintLength * 3, fingerprintLength)));

      table.Draw(new PointF(bounds.Left, bounds.Top), this.graphics);
    }

    private void PrintParentData(RectangleF bounds)
    {
      if (!(this.signatureRequest is SignatureRequest2))
        throw new InvalidOperationException("Must be a SignatureRequest2");

      SignatureRequest2 signatureRequest2 = (SignatureRequest2)this.signatureRequest;
      Certificate signingCertificate = signatureRequest2.SigningCertificate;

      Table table = new Table(new Font(FontFace, BaseFontSize));
      table.AddColumn(200f);
      table.AddColumn(bounds.Width - 200f);

      table.AddRow(LibraryResources.SigningRequestDocumentParent, 2, FontStyle.Bold);
      table.AddRow(" ", 2);

      string certificateId = signingCertificate.Id.ToString();
      int idLength = certificateId.Length / 2;
      table.AddRow(LibraryResources.SigningRequestDocumentCertificateId, certificateId.Substring(0, idLength));
      table.AddRow(string.Empty, certificateId.Substring(idLength));

      string fingerprint = signingCertificate.Fingerprint.Replace(" ", string.Empty);
      int fingerprintLength = fingerprint.Length / 4;
      table.AddRow(LibraryResources.SigningRequestDocumentCertificateFingerprint, FourBlocks(fingerprint.Substring(0, fingerprintLength)));
      table.AddRow(string.Empty, FourBlocks(fingerprint.Substring(fingerprintLength, fingerprintLength)));
      table.AddRow(string.Empty, FourBlocks(fingerprint.Substring(fingerprintLength * 2, fingerprintLength)));
      table.AddRow(string.Empty, FourBlocks(fingerprint.Substring(fingerprintLength * 3, fingerprintLength)));

      table.Draw(new PointF(bounds.Left, bounds.Top), this.graphics);
    }

    private void PrintRequest(RectangleF bounds)
    {
      Font font = new Font(FontFace, BaseFontSize);
      float eights = bounds.Width / 8f;

      SignObject requesterSign = new SignObject(this.graphics, LibraryResources.SigningRequestDocumentSignRequester, LibraryResources.SigningRequestDocumentSignSignature, LibraryResources.SigningRequestDocumentSignDate, font);
      requesterSign.SetCenterTop(bounds.Left + eights, bounds.Top);
      requesterSign.Draw();

      SignObject firstAuthoritySign = new SignObject(this.graphics, LibraryResources.SigningRequestDocumentSignFirstAuthority, LibraryResources.SigningRequestDocumentSignSignature, LibraryResources.SigningRequestDocumentSignDate, font);
      firstAuthoritySign.SetCenterTop(bounds.Left + 3 * eights, bounds.Top);
      firstAuthoritySign.Draw();

      SignObject secondAuthoritySign = new SignObject(this.graphics, LibraryResources.SigningRequestDocumentSignSecondAuthority, LibraryResources.SigningRequestDocumentSignSignature, LibraryResources.SigningRequestDocumentSignDate, font);
      secondAuthoritySign.SetCenterTop(bounds.Left + 5 * eights, bounds.Top);
      secondAuthoritySign.Draw();

      SignObject thirdAuthoritySign = new SignObject(this.graphics, LibraryResources.SigningRequestDocumentSignThirdAuthority, LibraryResources.SigningRequestDocumentSignSignature, LibraryResources.SigningRequestDocumentSignDate, font);
      thirdAuthoritySign.SetCenterTop(bounds.Left + 7 * eights, bounds.Top);
      thirdAuthoritySign.Draw();
    }

    private string FourBlocks(string line)
    {
      string newLine = string.Empty;

      for (int index = 0; index < line.Length; index++)
      {
        newLine += line.Substring(index, 1);

        if (index % 4 == 3)
        {
          newLine += " ";
        }
      }

      return newLine;
    }

    private void PrintResponse(RectangleF bounds)
    {
      Font font = new Font(FontFace, BaseFontSize);

      Table table = new Table(font);
      table.AddColumn(bounds.Width);
      table.AddRow(LibraryResources.SigningRequestDocumentAccepted);
      table.AddRow(LibraryResources.SigningRequestDocumentRefusedFingerprintMismatch);
      if (this.certificate is VoterCertificate)
      {
        table.AddRow(LibraryResources.SigningRequestDocumentRefusedNoMember);
      }
      else
      {
        table.AddRow(LibraryResources.SigningRequestDocumentRefusedNotFx);
      }
      table.AddRow(LibraryResources.SigningRequestDocumentRefusedHasCertificate);
      table.Draw(new PointF(bounds.Left, bounds.Top), this.graphics);

      SignObject caSign = new SignObject(this.graphics, LibraryResources.SigningRequestDocumentSignCA, LibraryResources.SigningRequestDocumentSignSignature, LibraryResources.SigningRequestDocumentSignDate, font);
      caSign.SetCenterTop(bounds.Left + bounds.Width / 8f * 7f, bounds.Top);
      caSign.Draw();
    }

    private void PrintRevoke(RectangleF bounds)
    {
      Font font = new Font(FontFace, BaseFontSize);

      Table table = new Table(font);
      table.AddColumn(bounds.Width);
      table.AddRow(LibraryResources.SigningRequestDocumentRevokedForgotten);
      table.AddRow(LibraryResources.SigningRequestDocumentRevokedLost);
      table.AddRow(LibraryResources.SigningRequestDocumentRevokedStolen);
      if (this.certificate is VoterCertificate)
      {
        table.AddRow(LibraryResources.SigningRequestDocumentRevokedNoLonger);
      }
      else
      {
        table.AddRow(LibraryResources.SigningRequestDocumentRevokedNoMoreFx);
      }
      table.Draw(new PointF(bounds.Left, bounds.Top), this.graphics);

      SignObject caSign = new SignObject(this.graphics, LibraryResources.SigningRequestDocumentSignCA, LibraryResources.SigningRequestDocumentSignSignature, LibraryResources.SigningRequestDocumentSignDate, font);
      caSign.SetCenterTop(bounds.Left + bounds.Width / 8f * 7f, bounds.Top);
      caSign.Draw();
    }

    private void PrintFooter(RectangleF bounds)
    {
    }
  }
}
