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
  partial class TallyItem
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
      this.resultList = new System.Windows.Forms.ListView();
      this.propertyColumnHeader = new System.Windows.Forms.ColumnHeader();
      this.valueColumnHeader = new System.Windows.Forms.ColumnHeader();
      this.SuspendLayout();
      // 
      // resultList
      // 
      this.resultList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.resultList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.propertyColumnHeader,
            this.valueColumnHeader});
      this.resultList.FullRowSelect = true;
      this.resultList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
      this.resultList.Location = new System.Drawing.Point(2, 3);
      this.resultList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.resultList.MultiSelect = false;
      this.resultList.Name = "resultList";
      this.resultList.Size = new System.Drawing.Size(696, 532);
      this.resultList.TabIndex = 6;
      this.resultList.UseCompatibleStateImageBehavior = false;
      this.resultList.View = System.Windows.Forms.View.Details;
      // 
      // propertyColumnHeader
      // 
      this.propertyColumnHeader.Text = "Property";
      this.propertyColumnHeader.Width = 530;
      // 
      // valueColumnHeader
      // 
      this.valueColumnHeader.Text = "Value";
      this.valueColumnHeader.Width = 100;
      // 
      // TallyItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.resultList);
      this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.Name = "TallyItem";
      this.Size = new System.Drawing.Size(700, 538);
      this.ResumeLayout(false);

    }

    #endregion

    private ListView resultList;
    private ColumnHeader propertyColumnHeader;
    private ColumnHeader valueColumnHeader;




  }
}
