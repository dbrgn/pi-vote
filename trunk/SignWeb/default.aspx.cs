/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignWeb
{
  public partial class Default : System.Web.UI.Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void reportSignatureButton_Click(object sender, EventArgs e)
    {
      Response.Redirect(string.Format("./sign.aspx?id={0}&fp={1}", this.idTextBox.Text, this.fingerprintTextBox.Text));
    }
  }
}