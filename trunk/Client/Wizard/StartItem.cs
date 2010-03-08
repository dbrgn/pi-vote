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
using System.Globalization;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Rpc;

namespace Pirate.PiVote.Client
{
  public partial class StartItem : WizardItem
  {
    public StartItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      if (this.haveCertificateRadio.Checked)
      {
        return new ChooseCertificateItem();
      }
      else
      {
        return new CreateCertificateItem();
      }
    }

    public override WizardItem Previous()
    {
      return null;
    }

    public override WizardItem Cancel()
    {
      return null;
    }

    public override bool CanCancel
    {
      get { return true; }
    }

    public override bool CanNext
    {
      get { return this.haveCertificateRadio.Checked || this.needCertificateRadio.Checked; }
    }

    public override bool CanPrevious
    {
      get { return false; }
    }

    private void StartItem_Load(object sender, EventArgs e)
    {
    }

    private void haveCertificateRadio_CheckedChanged(object sender, EventArgs e)
    {
      OnUpdateWizard();
    }

    private void needCertificateRadio_CheckedChanged(object sender, EventArgs e)
    {
      OnUpdateWizard();
    }

    public override void Begin()
    {
    }

    public override void UpdateLanguage()
    {
      base.UpdateLanguage();

      this.haveCertificateRadio.Text = Resources.HaveCertificate;
      this.needCertificateRadio.Text = Resources.NeedCertificate;
      this.certificateGroup.Text = Resources.StartCertificate;
    }

    private void englishRadio_CheckedChanged(object sender, EventArgs e)
    {
      Resources.Culture = CultureInfo.CreateSpecificCulture("en-US");
      LibraryResources.Culture = CultureInfo.CreateSpecificCulture("en-US");
      OnChangeLanguage();
    }

    private void germanRadio_CheckedChanged(object sender, EventArgs e)
    {
      Resources.Culture = CultureInfo.CreateSpecificCulture("de-DE");
      LibraryResources.Culture = CultureInfo.CreateSpecificCulture("de-DE");
      OnChangeLanguage();
    }

    private void frenchRadio_CheckedChanged(object sender, EventArgs e)
    {
      Resources.Culture = CultureInfo.CreateSpecificCulture("fr-FR");
      LibraryResources.Culture = CultureInfo.CreateSpecificCulture("fr-FR");
      OnChangeLanguage();
    }
  }
}
