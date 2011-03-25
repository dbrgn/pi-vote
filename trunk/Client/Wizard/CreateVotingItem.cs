/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Gui;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Client
{
  public partial class CreateVotingItem : WizardItem
  {
    public class VotingAuthoritiesFile : Serializable
    {
      public List<Guid> AuthorityIds { get; private set; }

      public VotingAuthoritiesFile(List<AuthorityCertificate> authorityCertificates)
      {
        AuthorityIds = new List<Guid>(authorityCertificates.Select(certificate => certificate.Id));
      }

      public VotingAuthoritiesFile(DeserializeContext context)
        : base(context)
      {
      }

      public override void Serialize(SerializeContext context)
      {
        base.Serialize(context);
        context.WriteList(AuthorityIds);
      }

      protected override void Deserialize(DeserializeContext context)
      {
        base.Deserialize(context);
        AuthorityIds = context.ReadGuidList();
      }
    }

    private const string SavedVotingParametersFileName = "current-voting.pi-parameters";
    private const string SavedVotingAuthoritiesFileName = "current-voting.pi-authorities";

    private bool done = false;
    private bool run = false;
    private Exception exception;
    private Thread generateThread;
    private List<AuthorityCertificate> authorityCertificates;
    private Dictionary<ComboBox, Dictionary<Guid, int>> authorityIndices;
    private VotingParameters votingParameters;
    private Group group;

    public CreateVotingItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      return new AdminChooseItem();
    }

    public override WizardItem Previous()
    {
      return new AdminChooseItem();
    }

    public override WizardItem Cancel()
    {
      return null;
    }

    public override bool CanCancel
    {
      get { return !this.run; }
    }

    public override bool CanPrevious
    {
      get { return !this.run; }
    }

    public override bool CanNext
    {
      get { return this.done; }
    }

    public override bool CancelIsDone
    {
      get { return this.done; }
    }

    public override void Begin()
    {
      this.run = true;
      OnUpdateWizard();
      SetEnable(false);

      Status.VotingClient.GetAuthorityCertificates(GetAuthorityCertificatesCompleted);

      while (this.run)
      {
        Status.UpdateProgress();
        Thread.Sleep(10);
      }

      Status.UpdateProgress();

      if (this.exception == null)
      {
        if (this.authorityCertificates != null &&
            this.authorityCertificates.Count > 0)
        {
          this.authorityIndices = new Dictionary<ComboBox,Dictionary<Guid,int>>();
          ComboBox[] authorityComboBoxes = new ComboBox[]{this.authority0List, this.authority1List, this.authority2List, this.authority3List, this.authority4List};

          foreach (var authorityComboBox in authorityComboBoxes)
          {
            this.authorityIndices.Add(authorityComboBox, new Dictionary<Guid,int>());
            this.authorityCertificates.Foreach(certificate => this.authorityIndices[authorityComboBox]
              .Add(certificate.Id, authorityComboBox.Items.Add(string.Format("{0}, {1}", certificate.Id.ToString(), certificate.FullName))));          
          }

          TryLoadVoting();

          Status.SetMessage("Authority certificates downloaded from server.", MessageType.Success);
        }
        else
        {
          Status.SetMessage("No authority certificates from server.", MessageType.Error);
        }
      }
      else
      {
        Status.SetMessage(this.exception.Message, MessageType.Error);
      }

      SetEnable(true);
      this.run = false;
      OnUpdateWizard();
    }

    private void TryLoadVoting()
    {
      string fileNameParameters = Path.Combine(Status.DataPath, SavedVotingParametersFileName);
      string fileNameAuthorities = Path.Combine(Status.DataPath, SavedVotingAuthoritiesFileName);

      if (File.Exists(fileNameParameters))
      {
        this.votingParameters = Serializable.Load<VotingParameters>(fileNameParameters);

        this.titleBox.Text = this.votingParameters.Title;
        this.descriptionBox.Text = this.votingParameters.Description;
        this.urlTextBox.Text = this.votingParameters.Url;
        this.votingFromPicker.Value = this.votingParameters.VotingBeginDate;
        this.votingUntilPicker.Value = this.votingParameters.VotingEndDate;
        this.groupComboBox.Value = Status.Groups.Where(group => group.Id == this.votingParameters.GroupId).FirstOrDefault();

        foreach (Question question in this.votingParameters.Questions)
        {
          Question newQuestion = new Question(question.Text, question.Description, question.Url, question.MaxVota);
          question.Options
            .Where(option => option.Text.Get(Language.English) != Resources.OptionAbstainEnglish &&
                             option.Text.Get(Language.English) != Resources.OptionAbstainSpecial)
            .Foreach(option => newQuestion.AddOption(option));

          ListViewItem item = new ListViewItem(question.Text.AllLanguages);
          item.SubItems.Add(question.Description.AllLanguages);
          item.Tag = newQuestion;
          this.questionListView.Items.Add(item);
        }
      }

      if (File.Exists(fileNameAuthorities))
      {
        VotingAuthoritiesFile authoritiesFile = Serializable.Load<VotingAuthoritiesFile>(fileNameAuthorities);
        this.authority0List.SelectedIndex = this.authorityIndices[this.authority0List][authoritiesFile.AuthorityIds.ElementAt(0)];
        this.authority1List.SelectedIndex = this.authorityIndices[this.authority1List][authoritiesFile.AuthorityIds.ElementAt(1)];
        this.authority2List.SelectedIndex = this.authorityIndices[this.authority2List][authoritiesFile.AuthorityIds.ElementAt(2)];
        this.authority3List.SelectedIndex = this.authorityIndices[this.authority3List][authoritiesFile.AuthorityIds.ElementAt(3)];
        this.authority4List.SelectedIndex = this.authorityIndices[this.authority4List][authoritiesFile.AuthorityIds.ElementAt(4)];
      }

      SetEnable(true);
    }

    private void GetAuthorityCertificatesCompleted(IEnumerable<AuthorityCertificate> authorityCertificates, Exception exception)
    {
      if (exception == null)
      {
        this.authorityCertificates = new List<AuthorityCertificate>(authorityCertificates.OrderBy(certificate => certificate.FullName));
      }

      this.exception = exception;
      this.run = false;
    }

    private void SetEnable(bool enable)
    {
      this.authority0List.Enabled = enable;
      this.authority1List.Enabled = enable;
      this.authority2List.Enabled = enable;
      this.authority3List.Enabled = enable;
      this.authority4List.Enabled = enable;
      this.titleBox.Enabled = enable;
      this.descriptionBox.Enabled = enable;
      this.urlTextBox.Enabled = enable;
      this.votingFromPicker.Enabled = enable;
      this.votingUntilPicker.Enabled = enable;
      this.groupComboBox.Enabled = enable;
      this.questionListView.Enabled = enable;
      this.clearButton.Enabled = enable;

      if (enable)
      {
        CheckEnable();
      }
      else
      {
        this.createButton.Enabled = false;
      }
    }

    private void createButton_Click(object sender, EventArgs e)
    {
      this.run = true;
      OnUpdateWizard();
      SetEnable(false);

      this.votingParameters =
        new VotingParameters(
          this.titleBox.Text,
          this.descriptionBox.Text,
          this.urlTextBox.Text,
          this.votingFromPicker.Value.Date,
          this.votingUntilPicker.Value.Date,
          this.group.Id);

      Status.SetProgress(Resources.CreateVotingCreating, 0d);
      Application.DoEvents();

      foreach (ListViewItem item in this.questionListView.Items)
      {
        Question question = (Question)item.Tag;

        if (question.MaxVota == 1)
        {
          MultiLanguageString abstainString = new MultiLanguageString();
          abstainString.Set(Language.English, Resources.OptionAbstainEnglish);
          abstainString.Set(Language.German, Resources.OptionAbstainGerman);
          abstainString.Set(Language.French, Resources.OptionAbstainFrench);

          MultiLanguageString descriptionString = new MultiLanguageString();
          descriptionString.Set(Language.English, string.Empty);
          descriptionString.Set(Language.German, string.Empty);
          descriptionString.Set(Language.French, string.Empty);

          MultiLanguageString urlString = new MultiLanguageString();
          urlString.Set(Language.English, string.Empty);
          urlString.Set(Language.German, string.Empty);
          urlString.Set(Language.French, string.Empty);

          Option abstain = new Option(abstainString, descriptionString, urlString);
          question.AddOption(abstain);
        }
        else
        {
          for (int index = 0; index < question.MaxVota; index++)
          {
            MultiLanguageString abstainString = new MultiLanguageString();
            abstainString.Set(Language.English, Resources.OptionAbstainSpecial);
            abstainString.Set(Language.German, Resources.OptionAbstainSpecial);
            abstainString.Set(Language.French, Resources.OptionAbstainSpecial);

            MultiLanguageString descriptionString = new MultiLanguageString();
            descriptionString.Set(Language.English, string.Empty);
            descriptionString.Set(Language.German, string.Empty);
            descriptionString.Set(Language.French, string.Empty);

            MultiLanguageString urlString = new MultiLanguageString();
            urlString.Set(Language.English, string.Empty);
            urlString.Set(Language.German, string.Empty);
            urlString.Set(Language.French, string.Empty);

            Option abstain = new Option(abstainString, descriptionString, urlString);
            question.AddOption(abstain);
          }
        }

        votingParameters.AddQuestion(question);
      }

      votingParameters.Save(Path.Combine(Status.DataPath, SavedVotingParametersFileName));

      List<AuthorityCertificate> authorities = new List<AuthorityCertificate>();
      authorities.Add(this.authorityCertificates[this.authority0List.SelectedIndex]);
      authorities.Add(this.authorityCertificates[this.authority1List.SelectedIndex]);
      authorities.Add(this.authorityCertificates[this.authority2List.SelectedIndex]);
      authorities.Add(this.authorityCertificates[this.authority3List.SelectedIndex]);
      authorities.Add(this.authorityCertificates[this.authority4List.SelectedIndex]);

      var invalidAuthorities = authorities
        .Where(authority => authority.Validate(Status.CertificateStorage, votingParameters.VotingBeginDate) != CertificateValidationResult.Valid);

      if (invalidAuthorities.Count() > 0)
      {
        StringBuilder message = new StringBuilder();
        message.AppendLine(Resources.CreateVotingInvalidAuthorities);
        invalidAuthorities.Foreach(authority => message.AppendLine(authority.Id.ToString() + " " + authority.FullName));
        MessageForm.Show(message.ToString(), GuiResources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
      }

      var authoritiesFile = new VotingAuthoritiesFile(authorities);
      authoritiesFile.Save(Path.Combine(Status.DataPath, SavedVotingAuthoritiesFileName));

      WaitForGeneration();

      if (this.votingParameters.Valid)
      {
        if (DecryptPrivateKeyDialog.TryDecryptIfNessecary(Status.Certificate, Resources.CreateVotingUnlockAction))
        {
          Signed<VotingParameters> signedVotingParameters = new Signed<VotingParameters>(votingParameters, Status.Certificate);

          Status.VotingClient.CreateVoting(signedVotingParameters, authorities, CreateVotingCompleted);

          while (this.run)
          {
            Application.DoEvents();
            Thread.Sleep(10);
          }

          if (this.exception == null)
          {
            Status.SetMessage(Resources.CreateVotingCreated, MessageType.Success);
          }
          else
          {
            Status.SetMessage(this.exception.Message, MessageType.Error);
          }

          Status.Certificate.Lock();
        }
        else
        {
          Status.SetMessage(Resources.CreateVotingCanceled, MessageType.Info);
        }
      }
      else
      {
        Status.SetMessage(Resources.CreateVotingCanceled, MessageType.Info);
      }

      Application.DoEvents();

      this.done = true;
      OnUpdateWizard();
    }

    private void WaitForGeneration()
    {
      this.run = true;
      this.generateThread = new Thread(Generate);
      this.generateThread.Start();

      DateTime lastUpdate = DateTime.Now;
      int progress = 0;
      Status.SetProgress(Resources.CreateVotingSearchPrime, (double)progress / 10d);

      while (this.run)
      {
        if (DateTime.Now.Subtract(lastUpdate).TotalSeconds > 0.25d)
        {
          progress = (progress + 1) % 11;
          Status.SetProgress(Resources.CreateVotingSearchPrime, (double)progress / 10d);
          lastUpdate = DateTime.Now;
        }

        Application.DoEvents();
        Thread.Sleep(10);
      }
    }

    private void CreateVotingCompleted(Exception exception)
    {
      this.exception = exception;
      this.run = false;
    }

    /// <summary>
    /// Generates the prime numbers.
    /// </summary>
    private void Generate()
    {
      this.votingParameters.GenerateNumbers(Status.DataPath);

      this.run = false;
    }

    private void titleBox_TextChanged(object sender, EventArgs e)
    {
      CheckEnable();
    }

    private void descriptionBox_TextChanged(object sender, EventArgs e)
    {
      CheckEnable();
    }

    private void questionBox_TextChanged(object sender, EventArgs e)
    {
      CheckEnable();
    }

    private void CheckEnable()
    {
      bool enable = true;

      enable &= !this.titleBox.IsEmpty;
      enable &= !this.descriptionBox.IsEmpty;
      enable &= this.votingFromPicker.Value.Date >= DateTime.Now.Date;
      enable &= this.votingUntilPicker.Value > this.votingFromPicker.Value;
      enable &= this.authority0List.SelectedIndex >= 0;
      enable &= this.authority1List.SelectedIndex >= 0;
      enable &= this.authority2List.SelectedIndex >= 0;
      enable &= this.authority3List.SelectedIndex >= 0;
      enable &= this.authority4List.SelectedIndex >= 0;

      List<int> indices = new List<int>();
      indices.Add(this.authority0List.SelectedIndex);
      indices.Add(this.authority1List.SelectedIndex);
      indices.Add(this.authority2List.SelectedIndex);
      indices.Add(this.authority3List.SelectedIndex);
      indices.Add(this.authority4List.SelectedIndex);
      enable &= !indices.Any(i => indices.Where(x => x == i).Count() > 1);

      enable &= this.questionListView.Items.Count >= 1;
      enable &= this.groupComboBox.SelectedIndex >= 0;

      this.createButton.Enabled = enable;
    }

    private void votingFromPicker_ValueChanged(object sender, EventArgs e)
    {
      CheckEnable();
    }

    private void votingUntilPicker_ValueChanged(object sender, EventArgs e)
    {
      CheckEnable();
    }

    private void CreateVotingItem_Load(object sender, EventArgs e)
    {

    }

    private void authority0List_SelectedIndexChanged(object sender, EventArgs e)
    {
      CheckEnable();
    }

    private void authority1List_SelectedIndexChanged(object sender, EventArgs e)
    {
      CheckEnable();
    }

    private void authority2List_SelectedIndexChanged(object sender, EventArgs e)
    {
      CheckEnable();
    }

    private void authority3List_SelectedIndexChanged(object sender, EventArgs e)
    {
      CheckEnable();
    }

    private void authority4List_SelectedIndexChanged(object sender, EventArgs e)
    {
      CheckEnable();
    }

    public override void UpdateLanguage()
    {
      base.UpdateLanguage();

      this.authoritiesLabel.Text = Resources.CreateVotingAuthorities;
      this.titleLabel.Text = Resources.CreateVotingTitle;
      this.descriptionLabel.Text = Resources.CreateVotingDescription;
      this.urlLabel.Text = Resources.CreateVotingUrl;
      this.votingFromLabel.Text = Resources.CreateVotingOpenFrom;
      this.votingUntilLabel.Text = Resources.CreateVotingOpenUntil;
      this.createButton.Text = Resources.CreateVotingButton;
      this.clearButton.Text = Resources.CreateVotingClearButton;
      this.questionLabel.Text = Resources.CreateVotingQuestions;
      this.textColumnHeader.Text = Resources.CreateVotingQuestionText;
      this.descriptionColumnHeader.Text = Resources.CreateVotingQuestionDescription;
      this.groupLabel.Text = Resources.CreateVotingGroup;

      this.groupComboBox.Clear();
      this.groupComboBox.Add(Status.Groups);
    }

    private void addToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Question question = AddQuestionDialog.ShowQuestion(null);

      if (question != null)
      {
        ListViewItem item = new ListViewItem(question.Text.AllLanguages);
        item.SubItems.Add(question.Description.AllLanguages);
        item.Tag = question;
        this.questionListView.Items.Add(item);

        CheckEnable();
      }
    }

    private void removeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.questionListView.SelectedItems.Count > 0)
      {
        this.questionListView.SelectedItems[0].Remove();

        CheckEnable();
      }
    }

    private void editToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.questionListView.SelectedItems.Count > 0)
      {
        ListViewItem item = this.questionListView.SelectedItems[0];
        Question question = (Question)item.Tag;
        question = AddQuestionDialog.ShowQuestion(question);

        if (question != null)
        {
          item.Text = question.Text.AllLanguages;
          item.SubItems[1].Text = question.Description.AllLanguages;
          item.Tag = question;
        }
      }
    }

    private void questionContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
      this.addToolStripMenuItem.Text = Resources.ContextMenuAdd;
      this.removeToolStripMenuItem.Text = Resources.ContextMenuRemove;
      this.editToolStripMenuItem.Text = Resources.ContextMenuEdit;

      this.removeToolStripMenuItem.Enabled = this.questionListView.SelectedItems.Count > 0;
      this.editToolStripMenuItem.Enabled = this.questionListView.SelectedItems.Count > 0;
    }

    private void cantonComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.group = this.groupComboBox.Value;
      CheckEnable();
    }

    private void clearButton_Click(object sender, EventArgs e)
    {
      this.authority0List.SelectedIndex = -1;
      this.authority1List.SelectedIndex = -1;
      this.authority2List.SelectedIndex = -1;
      this.authority3List.SelectedIndex = -1;
      this.authority4List.SelectedIndex = -1;
      this.titleBox.Clear();
      this.descriptionBox.Clear();
      this.urlTextBox.Clear();
      this.votingFromPicker.Value = DateTime.Now;
      this.votingUntilPicker.Value = DateTime.Now;
      this.questionListView.Items.Clear();
      this.groupComboBox.SelectedIndex = -1;
    }
  }
}
