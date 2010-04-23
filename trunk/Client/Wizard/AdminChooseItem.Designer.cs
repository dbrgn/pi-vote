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
using System.IO;
using System.Windows.Forms;

namespace Pirate.PiVote.Client
{
  partial class AdminChooseItem
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.getSignatureRequestsRadio = new System.Windows.Forms.RadioButton();
      this.setSignatureResponsesRadio = new System.Windows.Forms.RadioButton();
      this.createVotingRadio = new System.Windows.Forms.RadioButton();
      this.SuspendLayout();
      // 
      // getSignatureRequestsRadio
      // 
      this.getSignatureRequestsRadio.AutoSize = true;
      this.getSignatureRequestsRadio.Location = new System.Drawing.Point(18, 18);
      this.getSignatureRequestsRadio.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.getSignatureRequestsRadio.Name = "getSignatureRequestsRadio";
      this.getSignatureRequestsRadio.Size = new System.Drawing.Size(228, 18);
      this.getSignatureRequestsRadio.TabIndex = 0;
      this.getSignatureRequestsRadio.TabStop = true;
      this.getSignatureRequestsRadio.Text = "Download signature requests from server";
      this.getSignatureRequestsRadio.UseVisualStyleBackColor = true;
      this.getSignatureRequestsRadio.CheckedChanged += new System.EventHandler(this.getSignatureRequestsRadio_CheckedChanged);
      // 
      // setSignatureResponsesRadio
      // 
      this.setSignatureResponsesRadio.AutoSize = true;
      this.setSignatureResponsesRadio.Location = new System.Drawing.Point(18, 40);
      this.setSignatureResponsesRadio.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.setSignatureResponsesRadio.Name = "setSignatureResponsesRadio";
      this.setSignatureResponsesRadio.Size = new System.Drawing.Size(204, 18);
      this.setSignatureResponsesRadio.TabIndex = 1;
      this.setSignatureResponsesRadio.TabStop = true;
      this.setSignatureResponsesRadio.Text = "Upload signature response to server.";
      this.setSignatureResponsesRadio.UseVisualStyleBackColor = true;
      this.setSignatureResponsesRadio.CheckedChanged += new System.EventHandler(this.setSignatureResponsesRadio_CheckedChanged);
      // 
      // createVotingRadio
      // 
      this.createVotingRadio.AutoSize = true;
      this.createVotingRadio.Location = new System.Drawing.Point(18, 62);
      this.createVotingRadio.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.createVotingRadio.Name = "createVotingRadio";
      this.createVotingRadio.Size = new System.Drawing.Size(151, 18);
      this.createVotingRadio.TabIndex = 2;
      this.createVotingRadio.TabStop = true;
      this.createVotingRadio.Text = "Create a voting procedure";
      this.createVotingRadio.UseVisualStyleBackColor = true;
      this.createVotingRadio.CheckedChanged += new System.EventHandler(this.createVotingRadio_CheckedChanged);
      // 
      // AdminChooseItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.createVotingRadio);
      this.Controls.Add(this.setSignatureResponsesRadio);
      this.Controls.Add(this.getSignatureRequestsRadio);
      this.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
      this.Name = "AdminChooseItem";
      this.Size = new System.Drawing.Size(467, 359);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private RadioButton getSignatureRequestsRadio;
    private RadioButton setSignatureResponsesRadio;
    private RadioButton createVotingRadio;



  }
}
