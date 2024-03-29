﻿/*
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
using System.Drawing;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Rpc;

namespace Pirate.PiVote.Client
{
  public partial class Message : UserControl
  {
    public Message()
    {
      InitializeComponent();
    }

    public void Set(string message, MessageType type)
    {
      this.messageLabel.Text = message;

      switch (type)
      {
        case MessageType.Success:
          this.iconBox.BackgroundImage = Resources.Success;
          break;
        case MessageType.Error:
          this.iconBox.BackgroundImage = Resources.Error;
          break;
        default:
          this.iconBox.BackgroundImage = Resources.Info;
          break;
      }
    }
  }
}
