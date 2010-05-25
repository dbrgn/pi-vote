using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pirate.PiVote
{
  public partial class MultiLanguageTextBox : UserControl
  {
    private Dictionary<Language, TextBox> textBoxes;

    public MultiLanguageTextBox()
    {
      this.textBoxes = new Dictionary<Language, TextBox>();
      this.textBoxes.Add(Language.English, new TextBox());
      this.textBoxes.Add(Language.German, new TextBox());
      this.textBoxes.Add(Language.French, new TextBox());

      foreach (TextBox textBox in this.textBoxes.Values)
      {
        Controls.Add(textBox);
        textBox.TextChanged += new EventHandler(TextBox_TextChanged);
        textBox.Enter += new EventHandler(TextBox_Enter);
      }

      this.textBoxes[Language.English].Text = LibraryResources.LanguageEnglish;
      this.textBoxes[Language.German].Text = LibraryResources.LanguageGerman;
      this.textBoxes[Language.French].Text = LibraryResources.LanguageFrench;

      OnResize(new EventArgs());

      InitializeComponent();
    }

    private void TextBox_Enter(object sender, EventArgs e)
    {
      if (sender == this.textBoxes[Language.English] &&
          this.textBoxes[Language.English].Text == LibraryResources.LanguageEnglish)
      {
        this.textBoxes[Language.English].Text = string.Empty;
      }
      else if (sender == this.textBoxes[Language.German] &&
          this.textBoxes[Language.German].Text == LibraryResources.LanguageGerman)
      {
        this.textBoxes[Language.German].Text = string.Empty;
      }
      else if (sender == this.textBoxes[Language.French] &&
          this.textBoxes[Language.French].Text == LibraryResources.LanguageFrench)
      {
        this.textBoxes[Language.French].Text = string.Empty;
      }
    }

    private void TextBox_TextChanged(object sender, EventArgs e)
    {
      OnTextChanged(e);
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);

      int left = 0;
      foreach (TextBox textBox in this.textBoxes.Values)
      {
        textBox.Left = left;
        textBox.Top = 0;
        textBox.Width = Width / this.textBoxes.Count;
        textBox.Height = Height;

        left += textBox.Width;
      }
    }

    public bool IsEmpty
    {
      get
      {
        return this.textBoxes.Values.Any(textBox => textBox.Text.IsNullOrEmpty());
      }
    }

    public new MultiLanguageString Text
    {
      get
      {
        MultiLanguageString value = new MultiLanguageString();

        foreach (KeyValuePair<Language, TextBox> item in this.textBoxes)
        {
          value.Set(item.Key, item.Value.Text);
        }

        return value;
      }
      set
      {
        foreach (KeyValuePair<Language, TextBox> item in this.textBoxes)
        {
          item.Value.Text = value.Get(item.Key);
        }
      }
    }

    public bool Multiline
    {
      get { return this.textBoxes[Language.English].Multiline; }
      set { this.textBoxes.Values.Foreach(textBox => textBox.Multiline = value); }
    }

    public ScrollBars ScrollBars
    {
      get { return this.textBoxes[Language.English].ScrollBars; }
      set { this.textBoxes.Values.Foreach(textBox => textBox.ScrollBars = value); }
    }
  }
}
