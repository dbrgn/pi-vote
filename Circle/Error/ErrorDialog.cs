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
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Gui;

namespace Pirate.PiVote.Circle.Error
{
  public partial class ErrorDialog : Form
  {
    public ErrorDialog()
    {
      InitializeComponent();
    }

    private void ErrorDialog_Load(object sender, EventArgs e)
    {
      CenterToScreen();

      Text = Resources.ErrorDialogTitle;
      this.iconBox.Image = System.Drawing.SystemIcons.Error.ToBitmap();
      this.infoLabel.Text = Resources.ErrorDialogInfo;
      this.closeButton.Text = Gui.GuiResources.ButtonClose;
    }

    public static void ShowError(Exception exception)
    {
      if (exception is SocketException || exception.InnerException is SocketException)
      {
        MessageForm.Show(Resources.ConnectionLostErrorMessage, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        System.Diagnostics.Process.GetCurrentProcess().Kill();
      }
      else
      {
        ErrorDialog dialog = new ErrorDialog();
        dialog.messageBox.Text = exception.Message;

        StringBuilder builder = new StringBuilder();
        AssemblyName programName = Assembly.GetExecutingAssembly().GetName();
        AssemblyName libraryName = typeof(Prime).Assembly.GetName();

        builder.AppendLine("Operating system:    {0}", Environment.OSVersion.VersionString);
        builder.AppendLine("Program version:     {0}", programName.Version.ToString());
        builder.AppendLine("Library version:     {0}", libraryName.Version.ToString());
        builder.AppendLine("Message:             {0}", exception.Message.ToString());

        if (exception is PiException &&
           !((PiException)exception).ServerMessage.IsNullOrEmpty())
        {
          builder.AppendLine();
          builder.AppendLine("Server Message:      {0}", ((PiException)exception).ServerMessage);
        }

        builder.AppendLine();
        builder.AppendLine(exception.ToString());

        dialog.detailBox.Text = builder.ToString();

        dialog.ShowDialog();
      }
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
      Close();
    }
  }
}
