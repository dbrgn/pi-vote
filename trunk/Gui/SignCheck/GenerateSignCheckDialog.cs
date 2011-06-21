using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ThoughtWorks.QRCode.Codec;

namespace Pirate.PiVote.Gui
{
  public partial class GenerateSignCheckDialog : Form
  {
    public GenerateSignCheckDialog()
    {
      InitializeComponent();

      Text = GuiResources.GenerateSignCheckDialogTitle;
      this.infoLabel.Text = GuiResources.GenerateSignCheckDialogInfo;
      this.secretLabel.Text = GuiResources.GenerateSignCheckDialogSecret;
      this.closeButton.Text = GuiResources.ButtonClose;
    }

    private void GenerateSignCheckDialog_Load(object sender, EventArgs e)
    {
      CenterToScreen();
    }

    public static void ShowSignCheck(Guid notaryId, byte[] code)
    {
      string url = string.Format(
        "https://pivote.piratenpartei.ch/set.aspx?id={0}&c={1}",
        notaryId.ToString(),
        code.ToHexString());

      GenerateSignCheckDialog dialog = new GenerateSignCheckDialog();
      dialog.browseLink.Text = url;

      QRCodeEncoder qrEncoder = new QRCodeEncoder();
      qrEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
      qrEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
      qrEncoder.QRCodeVersion = 7;
      qrEncoder.QRCodeScale = 10;

      Image qrImage = qrEncoder.Encode(url);;
      var image = new Bitmap(qrImage.Width / 4 * 6, qrImage.Height / 4 * 6);
      var graphics = Graphics.FromImage(image);
      graphics.Clear(Color.White);
      graphics.DrawImage(qrImage, (float)qrImage.Width / 4f, (float)qrImage.Height / 4f);
      graphics.Flush();
      dialog.qrCodeImage.Image = image;

      dialog.ShowDialog();
    }

    private void browseLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start(this.browseLink.Text);
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
