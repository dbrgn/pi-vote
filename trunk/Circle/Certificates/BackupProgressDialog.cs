using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pirate.PiVote.Circle.Certificates
{
  public partial class BackupProgressDialog : Form
  {
    public BackupProgressDialog()
    {
      InitializeComponent();
      Text = Resources.BackupProgressTitle;
    }

    private void BackupProgressDialog_Load(object sender, EventArgs e)
    {
      CenterToScreen();
    }

    public void SetProgress(string filename, double progress)
    {
      this.fileLabel.Text = filename;
      this.progressBar.Value = Convert.ToInt32(progress * 100d);
    }
  }
}
