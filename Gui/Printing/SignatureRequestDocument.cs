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
using PdfSharp;
using PdfSharp.Pdf;
using PdfSharp.Fonts;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;

namespace Pirate.PiVote.Gui.Printing
{
  public delegate string GetGroupNameHandler(int groupId);

  public class SignatureRequestDocument
  {
    private const string FontFace = "Dejavu Sans";
    private const double BaseFontSize = 10d;
    private const float FirstColumnWidth = 100f;

    private SignatureRequest signatureRequest;
    private Certificate certificate;
    private XGraphics graphics;
    private PdfPage page;
    private GetGroupNameHandler getGroupName;
    private PdfDocument document;

    public SignatureRequestDocument(SignatureRequest signatureRequest, Certificate certificate, GetGroupNameHandler getGroupName)
    {
      if (signatureRequest == null)
        throw new ArgumentNullException("signatureRequest");
      if (certificate == null)
        throw new ArgumentNullException("certificate");

      this.signatureRequest = signatureRequest;
      this.certificate = certificate;
      this.getGroupName = getGroupName;
    }

    public void Create(string fileName)
    {
      this.document = new PdfDocument();
      this.document.Info.Title = "Certificate Request";

      PrintPage();

      this.document.Save(fileName);
    }

    private void PrintPage()
    {
      this.page = this.document.AddPage();
      this.graphics = XGraphics.FromPdfPage(this.page);
      var marginBounds = new XRect(50d, 50d, (float)this.page.Width - 100d, (float)this.page.Height - 100d);

      this.graphics.Clear(Color.White);

      this.top = marginBounds.Top;

      PrintHeader(Snippet(marginBounds, 40d));
      PrintData(Snippet(marginBounds, 230d));

      if (this.signatureRequest is SignatureRequest2)
      {
        PrintParentData(Snippet(marginBounds, 140d));

        PrintDontSend(Snippet(marginBounds, 120d));
      }
      else
      {
        PrintRequest(Snippet(marginBounds, 100f));

        PrintInfo(Snippet(marginBounds, 160f));
      }

      PrintResponse(Snippet(marginBounds, 110f));
      PrintRevoke(Snippet(marginBounds, 110f));
      PrintFooter(Snippet(marginBounds, 10f));
    }

    private double top;

    private XRect Snippet(XRect pageBounds, double size)
    {
      var area = new XRect(pageBounds.Left, this.top, pageBounds.Width, this.top + size);
      this.top += size;
      return area;
    }

    private void PrintHeader(XRect bounds)
    {
      var headerFont = GetFont(BaseFontSize + 2, XFontStyle.Bold);

      var partySize = graphics.MeasureString(GuiResources.SigningRequestDocumentHeaderRight, headerFont);

      this.graphics.DrawLine(new Pen(Color.Black, 1.5f), bounds.Left, bounds.Top + partySize.Height, bounds.Right, bounds.Top + partySize.Height);
      this.graphics.DrawString(GuiResources.SigningRequestDocumentHeaderLeft, headerFont, Brushes.Black, bounds.Left, bounds.Top + partySize.Height - 5f);
      this.graphics.DrawString(GuiResources.SigningRequestDocumentHeaderRight, headerFont, Brushes.Black, bounds.Right - partySize.Width, bounds.Top + partySize.Height - 5f);
    }

    private void PrintData(XRect bounds)
    {
      QRCodeEncoder qrEncoder = new QRCodeEncoder();
      qrEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
      qrEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
      qrEncoder.QRCodeVersion = 7;
      qrEncoder.QRCodeScale = 5;
      string url = string.Format(
        "https://pivote.piratenpartei.ch/sign.aspx?id={0}&k={1}",
        this.certificate.Id.ToString(),
        this.signatureRequest.Key.ToHexString());
      var image = qrEncoder.Encode(url);

      var stream = new System.IO.MemoryStream();
      image.Save(stream, ImageFormat.Png);
      var pngImage = Image.FromStream(stream);

      this.graphics.DrawImage(
        pngImage,
        bounds.Right - XUnit.FromInch(image.Width / graphics.Graphics.DpiX).Point,
        bounds.Top);

      var font = GetFont();
      Table table = new Table(font);
      table.AddColumn(FirstColumnWidth);
      table.AddColumn(bounds.Width - FirstColumnWidth);

      table.AddRow(GuiResources.SigningRequestDocumentRequest, 2, XFontStyle.Bold);

      table.AddRow(" ", 2);

      table.AddRow(GuiResources.SigningRequestDocumentFamilyName, this.signatureRequest.FamilyName);
      table.AddRow(GuiResources.SigningRequestDocumentFirstName, this.signatureRequest.FirstName);
      table.AddRow(GuiResources.SigningRequestDocumentEmailAddress, this.signatureRequest.EmailAddress);
      if (this.certificate is VoterCertificate)
        table.AddRow(GuiResources.SigningRequestDocumentGroup, this.getGroupName(((VoterCertificate)this.certificate).GroupId));
      table.AddRow(GuiResources.SigningRequestDocumentCertificateType, this.certificate.TypeText);

      string certificateId = this.certificate.Id.ToString();
      int idLength = certificateId.Length / 2;
      table.AddRow(GuiResources.SigningRequestDocumentCertificateId, certificateId.Substring(0, idLength));
      table.AddRow(string.Empty, certificateId.Substring(idLength));

      string requestKey = this.signatureRequest.Key.ToHexString();
      int requestKeyLength = requestKey.Length / 4;
      table.AddRow(GuiResources.SigningRequestDocumentRequestKey, FourBlocks(requestKey.Substring(0, requestKeyLength)));
      table.AddRow(string.Empty, FourBlocks(requestKey.Substring(requestKeyLength, requestKeyLength)));
      table.AddRow(string.Empty, FourBlocks(requestKey.Substring(requestKeyLength * 2, requestKeyLength)));
      table.AddRow(string.Empty, FourBlocks(requestKey.Substring(requestKeyLength * 3, requestKeyLength)));

      table.Draw(new XPoint(bounds.Left, bounds.Top), this.graphics);
    }

    private void PrintParentData(XRect bounds)
    {
      if (!(this.signatureRequest is SignatureRequest2))
        throw new InvalidOperationException("Must be a SignatureRequest2");

      SignatureRequest2 signatureRequest2 = (SignatureRequest2)this.signatureRequest;
      Certificate signingCertificate = signatureRequest2.SigningCertificate;

      var font = GetFont();
      Table table = new Table(font);
      table.AddColumn(FirstColumnWidth);
      table.AddColumn(bounds.Width - FirstColumnWidth);

      table.AddRow(GuiResources.SigningRequestDocumentParent, 2, XFontStyle.Bold);
      table.AddRow(" ", 2);

      string certificateId = signingCertificate.Id.ToString();
      int idLength = certificateId.Length / 2;
      table.AddRow(GuiResources.SigningRequestDocumentCertificateId, certificateId.Substring(0, idLength));
      table.AddRow(string.Empty, certificateId.Substring(idLength));

      string requestKey = this.signatureRequest.Key.ToHexString();
      int requestKeyLength = requestKey.Length / 4;
      table.AddRow(GuiResources.SigningRequestDocumentRequestKey, FourBlocks(requestKey.Substring(0, requestKeyLength)));
      table.AddRow(string.Empty, FourBlocks(requestKey.Substring(requestKeyLength, requestKeyLength)));
      table.AddRow(string.Empty, FourBlocks(requestKey.Substring(requestKeyLength * 2, requestKeyLength)));
      table.AddRow(string.Empty, FourBlocks(requestKey.Substring(requestKeyLength * 3, requestKeyLength)));

      table.Draw(new XPoint(bounds.Left, bounds.Top), this.graphics);
    }

    private void PrintRequest(XRect bounds)
    {
      var font = GetFont();
      double eights = bounds.Width / 8f;

      SignObject requesterSign = new SignObject(this.graphics, GuiResources.SigningRequestDocumentSignRequester, GuiResources.SigningRequestDocumentSignSignature, GuiResources.SigningRequestDocumentSignDate, font);
      requesterSign.SetCenterTop(bounds.Left + eights, bounds.Top);
      requesterSign.Draw();

      SignObject firstAuthoritySign = new SignObject(this.graphics, GuiResources.SigningRequestDocumentSignFirstAuthority, GuiResources.SigningRequestDocumentSignSignature, GuiResources.SigningRequestDocumentSignDate, font);
      firstAuthoritySign.SetCenterTop(bounds.Left + 3 * eights, bounds.Top);
      firstAuthoritySign.Draw();

      SignObject secondAuthoritySign = new SignObject(this.graphics, GuiResources.SigningRequestDocumentSignSecondAuthority, GuiResources.SigningRequestDocumentSignSignature, GuiResources.SigningRequestDocumentSignDate, font);
      secondAuthoritySign.SetCenterTop(bounds.Left + 5 * eights, bounds.Top);
      secondAuthoritySign.Draw();

      SignObject thirdAuthoritySign = new SignObject(this.graphics, GuiResources.SigningRequestDocumentSignThirdAuthority, GuiResources.SigningRequestDocumentSignSignature, GuiResources.SigningRequestDocumentSignDate, font);
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

    private void PrintInfo(XRect bounds)
    {
      var font = GetFont();

      var textFormatter0 = new XTextFormatter(this.graphics);
      textFormatter0.Alignment = XParagraphAlignment.Left;
      textFormatter0.DrawString(
        GuiResources.SigningRequestDocumentInfo, 
        font, 
        XBrushes.Black,
        new XRect(bounds.Left, bounds.Top, bounds.Width, 100d));

      Table table = new Table(font);
      table.AddColumn(FirstColumnWidth);
      table.AddColumn(bounds.Width - FirstColumnWidth);
      table.AddRow(GuiResources.SigningRequestDocumentSendTo, GuiResources.SigningRequestDocumentPpsAddress1);
      table.AddRow(string.Empty, GuiResources.SigningRequestDocumentPpsAddress2);
      table.Draw(new XPoint(bounds.Left, bounds.Top + 40f), this.graphics);

      var textFormatter1 = new XTextFormatter(this.graphics);
      textFormatter0.Alignment = XParagraphAlignment.Left;
      textFormatter1.DrawString(
        GuiResources.SigningRequestDocumentLeave, 
        font,
        XBrushes.Black,
        new XRect(bounds.Left, bounds.Top + 90d, bounds.Width, 100d));
    }

    private void PrintDontSend(XRect bounds)
    {
      var font = GetFont();

      var textFormatter = new XTextFormatter(this.graphics);
      textFormatter.Alignment = XParagraphAlignment.Left;
      textFormatter.DrawString(
        GuiResources.SigningRequestDocumentDontSend,
        font,
        XBrushes.Black,
        new XRect(bounds.Left, bounds.Top, bounds.Width, bounds.Height));
    }

    private void PrintResponse(XRect bounds)
    {
      var font = GetFont();

      Table table = new Table(font);
      table.AddColumn(bounds.Width);
      table.AddRow(GuiResources.SigningRequestDocumentAccepted);
      table.AddRow(GuiResources.SigningRequestDocumentRefusedFingerprintMismatch);
      table.AddRow(GuiResources.SigningRequestDocumentRefusedLost);
      table.AddRow(GuiResources.SigningRequestDocumentRefusedForgotten);
      table.AddRow(GuiResources.SigningRequestDocumentRefusedSignatureInvalid);

      if (this.certificate is VoterCertificate)
      {
        table.AddRow(GuiResources.SigningRequestDocumentRefusedNoMember);
      }
      else
      {
        table.AddRow(GuiResources.SigningRequestDocumentRefusedNotFx);
      }

      table.AddRow(GuiResources.SigningRequestDocumentRefusedHasCertificate);
      table.Draw(new XPoint(bounds.Left, bounds.Top), this.graphics);

      SignObject caSign = new SignObject(this.graphics, GuiResources.SigningRequestDocumentSignCA, GuiResources.SigningRequestDocumentSignSignature, GuiResources.SigningRequestDocumentSignDate, font);
      caSign.SetCenterTop(bounds.Left + bounds.Width / 8f * 7f, bounds.Top);
      caSign.Draw();
    }

    private static XFont GetFont()
    {
      return GetFont(BaseFontSize, XFontStyle.Regular);
    }

    private static XFont GetFont(double size, XFontStyle fontStyle)
    {
      var options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);
      var font = new XFont(FontFace, size, fontStyle, options);
      return font;
    }

    private void PrintRevoke(XRect bounds)
    {
      var font = GetFont();

      Table table = new Table(font);
      table.AddColumn(bounds.Width);
      table.AddRow(GuiResources.SigningRequestDocumentRevokedForgotten);
      table.AddRow(GuiResources.SigningRequestDocumentRevokedLost);
      table.AddRow(GuiResources.SigningRequestDocumentRevokedStolen);

      if (this.certificate is VoterCertificate)
      {
        table.AddRow(GuiResources.SigningRequestDocumentRevokedNoLonger);
      }
      else
      {
        table.AddRow(GuiResources.SigningRequestDocumentRevokedNoMoreFx);
      }

      table.AddRow(GuiResources.SigningRequestDocumentRevokedError);
      table.Draw(new XPoint(bounds.Left, bounds.Top), this.graphics);

      SignObject caSign = new SignObject(this.graphics, GuiResources.SigningRequestDocumentSignCA, GuiResources.SigningRequestDocumentSignSignature, GuiResources.SigningRequestDocumentSignDate, font);
      caSign.SetCenterTop(bounds.Left + bounds.Width / 8f * 7f, bounds.Top);
      caSign.Draw();
    }

    private void PrintFooter(XRect bounds)
    {
    }
  }
}
