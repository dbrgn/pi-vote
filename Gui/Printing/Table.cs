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
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Gui.Printing
{
  public class Table
  {
    public Font StandardFont { get; set; }

    public List<TableColumn> Columns { get; private set; }

    public List<TableRow> Rows { get; private set; }

    public Table(Font standardFont)
    {
      Columns = new List<TableColumn>();
      Rows = new List<TableRow>();
      StandardFont = standardFont;
    }

    public void AddColumn(float width)
    {
      Columns.Add(new TableColumn(width));
    }

    public void AddRow(params string[] texts)
    {
      AddRow(FontStyle.Regular, texts);
    }

    public void AddRow(string text, int columnSpan)
    {
      AddRow();
      AddCell(text, columnSpan);
    }

    public void AddRow(string text, int columnSpan, FontStyle style)
    {
      AddRow();
      AddCell(style, text, columnSpan);
    }

    public void AddRow(FontStyle style, params string[] texts)
    {
      TableRow row = new TableRow();

      foreach (string text in texts)
      {
        row.Cells.Add(new TableCell(text, new Font(StandardFont,style)));
      }

      Rows.Add(row);
    }

    public void AddCell(string text, int columnSpan)
    {
      AddCell(FontStyle.Regular, text, columnSpan);
    }

    public void AddCell(FontStyle style, string text, int columnSpan)
    {
      Rows[Rows.Count - 1].Cells.Add(new TableCell(text, new Font(StandardFont, style), columnSpan));
    }

    public void Draw(PointF position, Graphics graphics)
    {
      float y = position.Y;

      foreach (TableRow row in Rows)
      {
        float x = position.X;
        float rowHeight = 0;

        for (int columnIndex = 0; columnIndex < Columns.Count; columnIndex++)
        {
          TableCell cell = row.Cells[columnIndex];

          float width = 0f;
          for (int subColumnIndex = columnIndex; subColumnIndex < columnIndex + cell.ColumnSpan; subColumnIndex++)
          {
            width += Columns[subColumnIndex].Width;
          }

          float cellHeight = cell.Draw(graphics, new RectangleF(x, y, width, float.MaxValue));
          x += width;
          rowHeight = Math.Max(rowHeight, cellHeight);
          
          columnIndex += cell.ColumnSpan - 1;
        }

        y += rowHeight;
      }
    }
  }
}
