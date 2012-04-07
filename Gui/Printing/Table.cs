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
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Pdf;
using PdfSharp.Pdf;

namespace Pirate.PiVote.Gui.Printing
{
  public class Table
  {
    public XFont StandardFont { get; set; }

    public List<TableColumn> Columns { get; private set; }

    public List<TableRow> Rows { get; private set; }

    public Table(XFont standardFont)
    {
      Columns = new List<TableColumn>();
      Rows = new List<TableRow>();
      StandardFont = standardFont;
    }

    public void AddColumn(double width)
    {
      Columns.Add(new TableColumn(width));
    }

    public void AddRow(params string[] texts)
    {
      AddRow(XFontStyle.Regular, texts);
    }

    public void AddRow(string text, int columnSpan)
    {
      AddRow();
      AddCell(text, columnSpan);
    }

    public void AddRow(string text, int columnSpan, XFontStyle style)
    {
      AddRow();
      AddCell(style, text, columnSpan);
    }

    public void AddRow(XFontStyle style, params string[] texts)
    {
      TableRow row = new TableRow();
      var font = GetFont(style);

      foreach (string text in texts)
      {
        row.Cells.Add(new TableCell(text, font));
      }

      Rows.Add(row);
    }

    private XFont GetFont(XFontStyle style)
    {
      var options = new XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always);
      var font = new XFont(StandardFont.FontFamily.Name, StandardFont.Size, style, options);
      return font;
    }

    public void AddCell(string text, int columnSpan)
    {
      AddCell(XFontStyle.Regular, text, columnSpan);
    }

    public void AddCell(XFontStyle style, string text, int columnSpan)
    {
      var font = GetFont(style);
      Rows[Rows.Count - 1].Cells.Add(new TableCell(text, font, columnSpan));
    }

    public void Draw(XPoint position, XGraphics graphics)
    {
      double y = position.Y;

      foreach (TableRow row in Rows)
      {
        double x = position.X;
        double rowHeight = 0d;

        for (int columnIndex = 0; columnIndex < Columns.Count; columnIndex++)
        {
          TableCell cell = row.Cells[columnIndex];

          double width = 0d;
          for (int subColumnIndex = columnIndex; subColumnIndex < columnIndex + cell.ColumnSpan; subColumnIndex++)
          {
            width += Columns[subColumnIndex].Width;
          }

          double cellHeight = cell.Draw(graphics, new XRect(x, y, width, double.MaxValue));
          x += width;
          rowHeight = Math.Max(rowHeight, cellHeight);
          
          columnIndex += cell.ColumnSpan - 1;
        }

        y += rowHeight;
      }
    }
  }
}
