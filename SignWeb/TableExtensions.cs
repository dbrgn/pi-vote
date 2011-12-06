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

namespace System.Web.UI.WebControls
{
  public static class TableExtensions
  {
    public static void AddHeaderRow(this Table table, int cellCount, string header)
    {
      TableRow row = new TableRow();

      TableHeaderCell cell = new TableHeaderCell();
      cell.ColumnSpan = cellCount;
      cell.HorizontalAlign = HorizontalAlign.Left;
      cell.Text = header;
      row.Cells.Add(cell);

      table.Rows.Add(row);
    }

    public static void AddSpaceRow(this Table table, int cellCount, int height)
    {
      TableRow row = new TableRow();
      row.Height = new Unit(height, UnitType.Pixel);

      for (int cellIndex = 0; cellIndex < cellCount; cellIndex++)
      {
        TableCell cell = new TableCell();
        cell.Text = string.Empty;
        row.Cells.Add(cell);
      }

      table.Rows.Add(row);
    }

    public static void AddRow(this Table table, params string[] cellTexts)
    {
      TableRow row = new TableRow();

      foreach (string cellText in cellTexts)
      {
        TableCell cell = new TableCell();
        cell.Text = cellText;
        row.Cells.Add(cell);
      }

      table.Rows.Add(row);
    }

    public static void AddRow(this Table table, params Control[] cellControls)
    {
      TableRow row = new TableRow();

      foreach (Control cellControl in cellControls)
      {
        TableCell cell = new TableCell();

        if (cellControl != null)
        {
          cell.Controls.Add(cellControl);
        }

        row.Cells.Add(cell);
      }

      table.Rows.Add(row);
    }
  }
}