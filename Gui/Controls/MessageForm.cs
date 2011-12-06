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

namespace Pirate.PiVote.Gui
{
  public partial class MessageForm : Form
  {
    public MessageForm()
    {
      InitializeComponent();
    }

    public static DialogResult Show(string message, string title)
    {
      return Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.None, DialogResult.OK);
    }

    public static DialogResult Show(string message, string title, MessageBoxIcon icon)
    {
      return Show(message, title, MessageBoxButtons.OK, icon, DialogResult.OK);
    }

    public static DialogResult Show(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
    {
      DialogResult defaultResult = DialogResult.None;

      switch (buttons)
      {
        case MessageBoxButtons.AbortRetryIgnore:
          defaultResult = DialogResult.Abort;
          break;
        case MessageBoxButtons.OK:
          defaultResult = DialogResult.OK;
          break;
        case MessageBoxButtons.OKCancel:
          defaultResult = DialogResult.OK;
          break;
        case MessageBoxButtons.RetryCancel:
          defaultResult = DialogResult.Cancel;
          break;
        case MessageBoxButtons.YesNo:
          defaultResult = DialogResult.No;
          break;
        case MessageBoxButtons.YesNoCancel:
          defaultResult = DialogResult.Cancel;
          break;
        default:
          defaultResult = DialogResult.None;
          break;
      }

      return Show(message, title, buttons, icon, defaultResult);
    }

    public static DialogResult Show(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon, DialogResult defaultResult)
    {
      MessageForm form = new MessageForm();

      form.Text = title;
      form.infoBox.Text = message;

      switch (icon)
      {
        case MessageBoxIcon.Error:
          form.iconBox.Image = System.Drawing.SystemIcons.Error.ToBitmap();
          break;
        case MessageBoxIcon.Exclamation:
          form.iconBox.Image = System.Drawing.SystemIcons.Exclamation.ToBitmap();
          break;
        case MessageBoxIcon.Information:
          form.iconBox.Image = System.Drawing.SystemIcons.Information.ToBitmap();
          break;
        case MessageBoxIcon.Question:
          form.iconBox.Image = System.Drawing.SystemIcons.Question.ToBitmap();
          break;
        default:
          form.iconBox.Image = null;
          break;
      }

      switch (buttons)
      {
        case MessageBoxButtons.AbortRetryIgnore:
          form.leftbutton.DialogResult = DialogResult.Abort;
          form.leftbutton.Text = GuiResources.ButtonAbort;
          form.middleButton.DialogResult = DialogResult.Retry;
          form.middleButton.Text = GuiResources.ButtonRetry;
          form.rightButton.DialogResult = DialogResult.Ignore;
          form.rightButton.Text = GuiResources.ButtonIgnore;

          switch (defaultResult)
          {
            case DialogResult.Abort:
              form.leftbutton.Select();
              break;
            case DialogResult.Retry:
              form.middleButton.Select();
              break;
            case DialogResult.Ignore:
              form.rightButton.Select();
              break;
          }

          break;

        case MessageBoxButtons.OK:
          form.leftbutton.Visible = false;
          form.middleButton.Visible = false;
          form.rightButton.DialogResult = DialogResult.OK;
          form.rightButton.Text = GuiResources.ButtonOk;
          form.rightButton.Select();
          break;

        case MessageBoxButtons.OKCancel:
          form.leftbutton.Visible = false;
          form.middleButton.DialogResult = DialogResult.OK;
          form.middleButton.Text = GuiResources.ButtonOk;
          form.rightButton.DialogResult = DialogResult.Cancel;
          form.rightButton.Text = GuiResources.ButtonCancel;

          switch (defaultResult)
          {
            case DialogResult.OK:
              form.middleButton.Select();
              break;
            case DialogResult.Cancel:
              form.rightButton.Select();
              break;
          }

          break;

        case MessageBoxButtons.RetryCancel:
          form.leftbutton.Visible = false;
          form.middleButton.DialogResult = DialogResult.Retry;
          form.middleButton.Text = GuiResources.ButtonRetry;
          form.rightButton.DialogResult = DialogResult.Cancel;
          form.rightButton.Text = GuiResources.ButtonCancel;

          switch (defaultResult)
          {
            case DialogResult.Retry:
              form.middleButton.Select();
              break;
            case DialogResult.Cancel:
              form.rightButton.Select();
              break;
          }

          break;

        case MessageBoxButtons.YesNo:
          form.leftbutton.Visible = false;
          form.middleButton.DialogResult = DialogResult.Yes;
          form.middleButton.Text = GuiResources.ButtonYes;
          form.rightButton.DialogResult = DialogResult.No;
          form.rightButton.Text = GuiResources.ButtonNo;

          switch (defaultResult)
          {
            case DialogResult.Yes:
              form.middleButton.Select();
              break;
            case DialogResult.No:
              form.rightButton.Select();
              break;
          }

          break;

        case MessageBoxButtons.YesNoCancel:
          form.leftbutton.DialogResult = DialogResult.Yes;
          form.leftbutton.Text = GuiResources.ButtonYes;
          form.middleButton.DialogResult = DialogResult.No;
          form.middleButton.Text = GuiResources.ButtonNo;
          form.rightButton.DialogResult = DialogResult.Cancel;
          form.rightButton.Text = GuiResources.ButtonCancel;

          switch (defaultResult)
          {
            case DialogResult.Yes:
              form.leftbutton.Select();
              break;
            case DialogResult.No:
              form.middleButton.Select();
              break;
            case DialogResult.Cancel:
              form.rightButton.Select();
              break;
          }

          break;

        default:
          form.leftbutton.Visible = false;
          form.middleButton.Visible = false;
          form.rightButton.DialogResult = DialogResult.OK;
          form.rightButton.Text = GuiResources.ButtonOk;
          form.rightButton.Select();
          break;
      }

      return form.ShowDialog();
    }

    private void rightButton_Click(object sender, EventArgs e)
    {
      DialogResult = rightButton.DialogResult;
      Close();
    }

    private void middleButton_Click(object sender, EventArgs e)
    {
      DialogResult = middleButton.DialogResult;
      Close();
    }

    private void leftbutton_Click(object sender, EventArgs e)
    {
      DialogResult = leftbutton.DialogResult;
      Close();
    }

    private void MessageForm_Load(object sender, EventArgs e)
    {
      CenterToScreen();
    }
  }
}
