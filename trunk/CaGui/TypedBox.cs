/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pirate.PiVote;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.CaGui
{
  public class TypedBox<T> : ComboBox
  {
    private Dictionary<int, T> indexValue;
    private Dictionary<T, int> valueIndex;

    public TypedBox()
    {
      this.indexValue = new Dictionary<int, T>();
      this.valueIndex = new Dictionary<T, int>();
    }

    public virtual void SetValues()
    { 
    }

    protected virtual void AddItem(T value, string text)
    {
      int index = Items.Add(text);
      this.indexValue.Add(index, value);
      this.valueIndex.Add(value, index);
    }

    public T Value
    {
      get
      {
        if (SelectedIndex >= 0)
        {
          return this.indexValue[SelectedIndex];
        }
        else
        {
          return default(T);
        }
      }
      set
      {
        if (value != null &&
          this.valueIndex.Count > 0)
        {
          SelectedIndex = this.valueIndex[value];
        }
        else
        {
          SelectedIndex = -1;
        }
      }
    }
  }
}
