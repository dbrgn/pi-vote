using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pirate.PiVote.CaGui
{
  public partial class WaitForm : Form
  {
    public WaitForm()
    {
      InitializeComponent();
    }

    private void WaitForm_Load(object sender, EventArgs e)
    {
      CenterToScreen();
    }

    public void Update(string info0, string info1, double percent)
    {
      this.info0Label.Text = info0;
      this.info1Label.Text = info1;
      this.progressBar.Value = Convert.ToInt32(percent);
    }
  }
}
