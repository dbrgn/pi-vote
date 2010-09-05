/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace Pirate.PiVote.Client
{
  [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
  public partial class ScrollableContainerControl : UserControl
  {
    private int oldValue;

    public ScrollableContainerControl()
    {
      InitializeComponent();

      this.ControlAdded += new ControlEventHandler(Panel_ControlAdded);
      this.ControlRemoved += new ControlEventHandler(Panel_ControlRemoved);
      this.vScrollBar.Scroll += new ScrollEventHandler(vScrollBar_Scroll);
    }

    private void vScrollBar_Scroll(object sender, ScrollEventArgs e)
    {
      foreach (Control control in Controls)
      {
        if (control != this.vScrollBar)
        {
          control.Top += this.oldValue - e.NewValue;
        }
      }

      this.oldValue = e.NewValue;
    }

    private void UpdateScrollBar()
    {
      int bottom = 0;

      foreach (Control control in Controls)
      {
        if (control != this.vScrollBar)
        {
          bottom = Math.Max(bottom, control.Top + control.Height);
        }
      }

      this.vScrollBar.Minimum = 0;
      this.vScrollBar.Maximum = bottom - Height;

      if (this.vScrollBar.Maximum > 0)
      {
        this.vScrollBar.Value = 0;
        this.oldValue = 0;
      }
    }

    private void Panel_ControlRemoved(object sender, ControlEventArgs e)
    {
      UpdateScrollBar();
    }

    private void Panel_ControlAdded(object sender, ControlEventArgs e)
    {
      UpdateScrollBar();
    }

    public int ContentWidth
    {
      get { return Width - this.vScrollBar.Width; }
    }
  }
}
