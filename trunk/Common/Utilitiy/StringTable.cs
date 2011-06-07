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

namespace Pirate.PiVote
{
  public class StringTable
  {
    private List<int> columns;
    private List<List<string>> data;
    private bool hasHeader = true;

    public StringTable()
    {
      this.columns = new List<int>();
      this.data = new List<List<string>>();
      this.data.Add(new List<string>());
    }

    public void SetColumnCount(int count)
    {
      this.hasHeader = false;
      count.Times(() => this.columns.Add(1));
    }

    public void AddColumn(string header)
    {
      this.columns.Add(header.Length + 1);
      this.data[0].Add(header);
    }

    public void AddRow(params string[] text)
    {
      this.data.Add(new List<string>(text));

      for (int index = 0; index < text.Length; index++)
      {
        this.columns[index] = Math.Max(this.columns[index], text[index].Length + 1);
      }
    }

    public string Render()
    {
      StringBuilder builder = new StringBuilder();

      for (int rowIndex = 0; rowIndex < this.data.Count; rowIndex++)
      {
        var row = this.data[rowIndex];

        for (int columnIndex = 0; columnIndex < row.Count; columnIndex++)
        { 
          builder.Append(Fixed(row[columnIndex], this.columns[columnIndex]));
        }

        if (rowIndex == 0 && this.hasHeader)
        {
          builder.AppendLine();

          for (int columnIndex = 0; columnIndex < row.Count; columnIndex++)
          {
            builder.Append(Line(this.columns[columnIndex]));
          }
        }

        builder.AppendLine();
      }

      return builder.ToString();
    }

    private static string Line(int length)
    {
      string text = string.Empty;

      while (text.Length < length)
      {
        text += "-";
      }

      return text;
    }

    private static string Fixed(string input, int length)
    {
      string text = input.Length > length - 1 ? input.Substring(0, length - 1) : input;

      while (text.Length < length)
      {
        text += " ";
      }

      return text;
    }
  }
}
