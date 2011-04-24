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
      this.propertyColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.valueColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
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
      this.resultList.Location = new System.Drawing.Point(1, 2);
      this.resultList.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
      this.resultList.MultiSelect = false;
      this.resultList.Name = "resultList";
      this.resultList.Size = new System.Drawing.Size(465, 356);
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
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.resultList);
      this.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
      this.Name = "TallyItem";
      this.Size = new System.Drawing.Size(467, 359);
      this.ResumeLayout(false);

    }

    #endregion

    private ListView resultList;
    private ColumnHeader propertyColumnHeader;
    private ColumnHeader valueColumnHeader;




  }
}
