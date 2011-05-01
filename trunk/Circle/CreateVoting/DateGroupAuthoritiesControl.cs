using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Gui;

namespace Pirate.PiVote.Circle.CreateVoting
{
  public partial class DateGroupAuthoritiesControl : CreateVotingControl
  {
    public DateGroupAuthoritiesControl()
    {
      InitializeComponent();
    }

    public override void Prepare()
    {
      this.authoritiesLabel.Text = Resources.CreateVotingAuthorities;
      this.votingFromLabel.Text = Resources.CreateVotingOpenFrom;
      this.votingUntilLabel.Text = Resources.CreateVotingOpenUntil;
      this.groupLabel.Text = Resources.CreateVotingGroup;
      this.nextButton.Text = GuiResources.ButtonNext;
      this.cancelButton.Text = GuiResources.ButtonCancel;

      try
      {
        Pirate.PiVote.Circle.Status.TextStatusDialog.ShowInfo(Status.Controller, FindForm());
        var authorities = Status.Controller.GetAuthorities();

        foreach (var certificate in authorities)
        {
          ListViewItem item = new ListViewItem(certificate.Id.ToString());
          item.SubItems.Add(certificate.FullName);
          item.Tag = certificate;
          this.authoritiesList.Items.Add(item);
        }

        this.groupComboBox.Add(Status.Controller.Status.Groups);

        Pirate.PiVote.Circle.Status.TextStatusDialog.HideInfo();
        CheckEnable();
      }
      catch (Exception exception)
      {
        Pirate.PiVote.Circle.Status.TextStatusDialog.HideInfo();
        Error.ErrorDialog.ShowError(exception);
        OnCloseCreateDialog();
      }
    }

    private void CheckEnable()
    {
      bool enable = true;

      enable &= this.votingFromPicker.Value.Date >= DateTime.Now.Date;
      enable &= this.votingUntilPicker.Value.Date >= this.votingFromPicker.Value.Date.AddDays(1);
      enable &= this.groupComboBox.Value != null;
      int authorityCount = 0;

      foreach (ListViewItem item in this.authoritiesList.Items)
      {
        if (item.Checked)
        {
          authorityCount++;
        }
      }

      enable &= authorityCount == 5;

      this.nextButton.Enabled = enable;
    }

    private void votingFromPicker_ValueChanged(object sender, EventArgs e)
    {
      CheckEnable();
    }

    private void votingUntilPicker_ValueChanged(object sender, EventArgs e)
    {
      CheckEnable();
    }

    private void groupComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      CheckEnable();
    }

    private void authoritiesList_ItemChecked(object sender, ItemCheckedEventArgs e)
    {
      CheckEnable();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      OnCloseCreateDialog();
    }

    private void nextButton_Click(object sender, EventArgs e)
    {
      List<AuthorityCertificate> authorities = new List<AuthorityCertificate>();

      foreach (ListViewItem item in this.authoritiesList.Items)
      {
        if (item.Checked)
        {
          authorities.Add((AuthorityCertificate)item.Tag);
        }
      }

      Status.Authorites = authorities;
      Status.FromDate = this.votingFromPicker.Value.Date;
      Status.UntilDate = this.votingUntilPicker.Value.Date;
      Status.VotingGroup = this.groupComboBox.Value;

      PrimeControl nextControl = new PrimeControl();
      nextControl.Status = Status;
      OnShowNextControl(nextControl);
    }
  }
}
