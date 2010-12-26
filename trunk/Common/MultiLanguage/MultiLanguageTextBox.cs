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
    private Dictionary<TextBox, bool> langaugeDisplay;

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

      langaugeDisplay = new Dictionary<TextBox, bool>();
      this.langaugeDisplay.Add(this.textBoxes[Language.English], true);
      this.langaugeDisplay.Add(this.textBoxes[Language.German], true);
      this.langaugeDisplay.Add(this.textBoxes[Language.French], true);
      this.textBoxes[Language.English].Text = LibraryResources.LanguageEnglish;
      this.textBoxes[Language.German].Text = LibraryResources.LanguageGerman;
      this.textBoxes[Language.French].Text = LibraryResources.LanguageFrench;

      this.textBoxes.Values.Foreach(textBox => textBox.ForeColor = Color.Gray);

      OnResize(new EventArgs());

      InitializeComponent();
    }

    private void TextBox_Enter(object sender, EventArgs e)
    {
      if (sender is TextBox)
      {
        TextBox textBox = (TextBox)sender;
        if (this.langaugeDisplay.ContainsKey(textBox) &&
            this.langaugeDisplay[textBox])
        {
          textBox.Text = string.Empty;
          textBox.ForeColor = Color.Black;
          this.langaugeDisplay[textBox] = false;
        }
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
        return this.langaugeDisplay.Values.Any(value => value) ||
          this.textBoxes.Values.Any(textBox => textBox.Text.IsNullOrEmpty());
      }
    }

    public void Clear()
    {
      foreach (TextBox textBox in this.textBoxes.Values)
      {
        textBox.Text = string.Empty;
        this.langaugeDisplay[textBox] = true;
      }
    }

    public new MultiLanguageString Text
    {
      get
      {
        MultiLanguageString value = new MultiLanguageString();

        foreach (KeyValuePair<Language, TextBox> item in this.textBoxes)
        {
          if (this.langaugeDisplay[item.Value])
          {
            value.Set(item.Key, string.Empty);
          }
          else
          {
            value.Set(item.Key, item.Value.Text);
          }
        }

        return value;
      }
      set
      {
        foreach (KeyValuePair<Language, TextBox> item in this.textBoxes)
        {
          string stringValue = value.Get(item.Key);

          if (!stringValue.IsNullOrEmpty())
          {
            item.Value.Text = stringValue;
            item.Value.ForeColor = Color.Black;
            this.langaugeDisplay[item.Value] = false;
          }
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
