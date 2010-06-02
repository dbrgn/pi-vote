using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Pirate.PiVote;

namespace ResourceManager
{
  public partial class Master : Form
  {
    private Dictionary<string, Resources> resourcesList;

    public Master()
    {
      InitializeComponent();

      CenterToScreen();

      this.pathTextBox.Text = Path.Combine("..", Path.Combine("..", ".."));
    }

    private void pathTextBox_TextChanged(object sender, EventArgs e)
    {
      this.loadButton.Enabled = Directory.Exists(this.pathTextBox.Text);
    }

    private void loadButton_Click(object sender, EventArgs e)
    {
      LoadResources();
      DisplayResources();
    }

    private void DisplayResources()
    {
      this.loadList.Items.Clear();

      foreach (KeyValuePair<string, Resources> resources in this.resourcesList)
      {
        int englishCount = resources.Value.Content.Count(c => c.Value.Text.Has(Language.English));
        int germanCount = resources.Value.Content.Count(c => c.Value.Text.Has(Language.German));
        int frenchCount = resources.Value.Content.Count(c => c.Value.Text.Has(Language.French));

        ListViewItem item = new ListViewItem(resources.Key);
        item.SubItems.Add(englishCount.ToString());
        item.SubItems.Add(germanCount.ToString());
        item.SubItems.Add(frenchCount.ToString());
        item.Tag = resources.Value;
        this.loadList.Items.Add(item);
      }
    }

    private void LoadResources()
    {
      Dictionary<string, Language> patterns = new Dictionary<string, Language>();
      patterns.Add(".de-DE.resx", Language.German);
      patterns.Add(".fr-FR.resx", Language.French);
      patterns.Add(".resx", Language.English);

      DirectoryInfo rootDirectory = new DirectoryInfo(this.pathTextBox.Text);
      this.resourcesList = new Dictionary<string, Resources>();

      foreach (FileInfo resourceFile in rootDirectory.GetFiles("*.resx", SearchOption.AllDirectories))
      {
        var matches = patterns.Where(p => resourceFile.Name.Contains(p.Key));
        var pattern = matches.Where(p => p.Key.Length == matches.Max(x => x.Key.Length)).FirstOrDefault();

        if (pattern.Key != null)
        {
          string name = resourceFile.FullName.Replace(pattern.Key, string.Empty);

          if (!resourcesList.ContainsKey(name))
          {
            resourcesList.Add(name, new Resources());
          }

          resourcesList[name].Load(resourceFile.FullName, pattern.Value);
        }

        var emptyResources = new List<string>(resourcesList.Where(x => x.Value.Content.Count < 10).Select(x => x.Key));
        emptyResources.ForEach(e => this.resourcesList.Remove(e));
      }
    }

    private void loadContextMenu_Opening(object sender, CancelEventArgs e)
    {
      this.exportToolStripMenuItem.Enabled = this.loadList.SelectedItems.Count > 0;
      this.importToolStripMenuItem.Enabled = this.loadList.SelectedItems.Count > 0;
      this.saveToolStripMenuItem.Enabled = this.loadList.SelectedItems.Count > 0;
    }

    private void exportToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.loadList.SelectedItems.Count > 0)
      {
        SaveFileDialog dialog = new SaveFileDialog();
        dialog.Filter = "CSV file|*.csv";
        dialog.Title = "Export CSV";
        dialog.ValidateNames = true;
        dialog.OverwritePrompt = true;
        dialog.CheckPathExists = true;

        if (dialog.ShowDialog() == DialogResult.OK)
        {
          Resources resources = (Resources)this.loadList.SelectedItems[0].Tag;
          resources.ExportCsv(dialog.FileName);
        }
      }
    }

    private void importToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.loadList.SelectedItems.Count > 0)
      {
        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Filter = "CSV file|*.csv";
        dialog.Title = "Import CSV";
        dialog.ValidateNames = true;
        dialog.CheckPathExists = true;
        dialog.CheckFileExists = true;

        if (dialog.ShowDialog() == DialogResult.OK)
        {
          ListViewItem item = this.loadList.SelectedItems[0];
          Resources resources = (Resources)item.Tag;
          resources.ImportCsv(dialog.FileName);

          int englishCount = resources.Content.Count(c => c.Value.Text.Has(Language.English));
          int germanCount = resources.Content.Count(c => c.Value.Text.Has(Language.German));
          int frenchCount = resources.Content.Count(c => c.Value.Text.Has(Language.French));

          item.SubItems[1].Text = englishCount.ToString();
          item.SubItems[2].Text = germanCount.ToString();
          item.SubItems[3].Text = frenchCount.ToString();
        }
      }
    }

    private void saveToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.loadList.SelectedItems.Count > 0)
      {
        ListViewItem item = this.loadList.SelectedItems[0];
        Resources resources = (Resources)item.Tag;
        resources.Save();
      }
    }
  }
}
