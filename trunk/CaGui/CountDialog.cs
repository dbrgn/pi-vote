/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

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
  public partial class CountDialog : Form
  {
    public IEnumerable<ListEntry> Entries { get; set; }

    public CountDialog()
    {
      InitializeComponent();
    }

    private void CountDialog_Load(object sender, EventArgs e)
    {
      CenterToScreen();
      SetGroups();
      UpdateCounts();
    }

    private void SetGroups()
    {
      this.groupBox.Add(GroupList.Groups);

      if (this.groupBox.Items.Count > 0)
      {
        this.groupBox.SelectedIndex = 0;
      }
    }

    private void UpdateCounts()
    {
      int totalCount = 0;
      int pendingCount = 0;
      int refusedCount = 0;
      int revokedCount = 0;
      int validCount = 0;
      int outdatedCount = 0;
      int notYetCount = 0;

      if (Entries != null)
      {
        foreach (var entry in Entries)
        {
          if (entry.GroupId == this.groupBox.Value.Id)
          {
            totalCount++;

            switch (entry.StatusAtDate(this.datePicker.Value))
            {
              case CertificateStatus.New:
                pendingCount++;
                break;
              case CertificateStatus.Refused:
                refusedCount++;
                break;
              case CertificateStatus.Revoked:
                revokedCount++;
                break;
              case CertificateStatus.Valid:
                validCount++;
                break;
              case CertificateStatus.Outdated:
                outdatedCount++;
                break;
              case CertificateStatus.NotYet:
                notYetCount++;
                break;
            }
          }
        }
      }

      this.totalBox.Text = totalCount.ToString();
      this.pendingBox.Text = pendingCount.ToString();
      this.refusedBox.Text = refusedCount.ToString();
      this.revokedBox.Text = revokedCount.ToString();
      this.validBox.Text = validCount.ToString();
      this.outdatedBox.Text = outdatedCount.ToString();
      this.notYetBox.Text = notYetCount.ToString();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void datePicker_ValueChanged(object sender, EventArgs e)
    {
      UpdateCounts();
    }

    private void groupBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateCounts();
    }
  }
}
