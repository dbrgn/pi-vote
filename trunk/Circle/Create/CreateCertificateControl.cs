/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pirate.PiVote.Circle.Create
{
  public delegate void ShowNextControlHandler(CreateCertificateControl nextControl);

  public delegate void CloseCreateDialogHandler();

  public partial class CreateCertificateControl : UserControl
  {
    public event ShowNextControlHandler ShowNextControl;

    public event CloseCreateDialogHandler CloseCreateDialog;

    public CreateDialogStatus Status { get; set; }

    public CreateCertificateControl()
    {
      InitializeComponent();
    }

    protected void OnShowNextControl(CreateCertificateControl nextControl)
    {
      if (ShowNextControl != null)
      {
        ShowNextControl(nextControl);
      }
    }

    protected void OnCloseCreateDialog()
    {
      if (CloseCreateDialog != null)
      {
        CloseCreateDialog();
      }
    }
  }
}
