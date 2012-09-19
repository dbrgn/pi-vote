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
using System.Xml.Linq;
using Pirate.PiVote;
using System.IO;

namespace Pirate.PiVote.ResourceManager
{
  public class Resources
  {
    public Dictionary<string, Resource> Content { get; private set; }
    public Dictionary<Language, XDocument> Documents { get; private set; }
    public Dictionary<Language, string> FileNames { get; private set; }

    public Resources()
    {
      Content = new Dictionary<string, Resource>();
      Documents = new Dictionary<Language, XDocument>();
      FileNames = new Dictionary<Language, string>();
    }

    public void Load(string fileName, Language language)
    {
      XDocument document = XDocument.Load(fileName);
      XElement rootElement = document.Element("root");

      foreach (XElement dataElement in rootElement.Elements("data"))
      {
        string name = dataElement.Attribute("name").Value;
        string text = dataElement.Value.Trim();

        if (Content.ContainsKey(name))
        {
          Content[name].Text.Set(language, text);
        }
        else
        {
          Content.Add(name, new Resource(name, language, text));
        }
      }

      Documents.Add(language, document);
      FileNames.Add(language, fileName);
    }

    public void Save()
    {
      foreach (KeyValuePair<Language, XDocument> document in Documents)
      {
        XElement rootElement = document.Value.Element("root");
        Dictionary<string, string> dataList = new Dictionary<string, string>();
        Content
          .Where(c => !c.Value.Text.GetOrEmpty(document.Key).IsNullOrEmpty())
          .Foreach(c => dataList.Add(c.Value.Name, c.Value.Text.GetOrEmpty(document.Key)));

        foreach (XElement dataElement in rootElement.Elements("data"))
        {
          if (dataElement.Elements("value").Count() > 0)
          {
            string name = dataElement.Attribute("name").Value;
            if (dataList.ContainsKey(name))
            {
              dataElement.Element("value").Value = dataList[name];
            }

            dataList.Remove(name);
          }
        }

        foreach (KeyValuePair<string, string> dataItem in dataList)
        {
          XElement dataElement = new XElement("data");
          dataElement.Add(new XAttribute("name", dataItem.Key));
          dataElement.Add(new XElement("value").Value = dataItem.Value);
          rootElement.Add(dataElement);
        }

        document.Value.Save(FileNames[document.Key]);
      }
    }

    public void ExportCsv(string fileName)
    {
      StringBuilder builder = new StringBuilder();

      foreach (Resource resource in Content.Values)
      {
        builder.Append("\"" + resource.Name + "\";");
        builder.Append("\"" + resource.Text.GetOrEmpty(Language.English) + "\";");
        builder.Append("\"" + resource.Text.GetOrEmpty(Language.German) + "\";");
        builder.Append("\"" + resource.Text.GetOrEmpty(Language.French) + "\"");
        builder.AppendLine();
      }

      File.WriteAllText(fileName, builder.ToString(), Encoding.UTF8);
    }

    public void ImportCsv(string fileName)
    {
      string text = File.ReadAllText(fileName, Encoding.UTF8);;
      string[] lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

      foreach (string line in lines)
      {
        IEnumerable<string> rows = ParseLine(line);

        if (rows.Count() >= 1)
        {
          string name = rows.ElementAt(0);
          string english = rows.Count() >= 2 ? rows.ElementAt(1) : string.Empty;
          string german = rows.Count() >= 3 ? rows.ElementAt(2) : string.Empty;
          string french = rows.Count() >= 4 ? rows.ElementAt(3) : string.Empty;

          if (!Content.ContainsKey(name))
            Content.Add(name, new Resource(name));

          if (!english.IsNullOrEmpty() && english != Content[name].Text.GetOrEmpty(Language.English))
            Content[name].Text.Set(Language.English, english);

          if (!german.IsNullOrEmpty() && german != Content[name].Text.GetOrEmpty(Language.German))
            Content[name].Text.Set(Language.German, german);

          if (!french.IsNullOrEmpty() && french != Content[name].Text.GetOrEmpty(Language.French))
            Content[name].Text.Set(Language.French, french);
        }
      }
    }

    private IEnumerable<string> ParseLine(string line)
    {
      List<string> parts = new List<string>();
      string remains = line.Trim();

      while (remains.Length > 0)
      {
        if (remains.StartsWith("\""))
        {
          int endIndex = remains.IndexOf("\"", 1);
          if (endIndex < 0)
            throw new Exception("Cannot parse CSV.");

          while (remains.Length > endIndex + 1 &&
                 remains.Substring(endIndex + 1, 1) == "\"")
          {
            endIndex = remains.IndexOf("\"", endIndex + 2);
            if (endIndex < 0)
              throw new Exception("Cannot parse CSV.");
          }

          string part = remains.Substring(1, endIndex - 1);
          part = part.Replace("\"\"", "\"");
          remains = remains.Substring(endIndex + 1);
          parts.Add(part);
        }
        else
        {
          int endIndex = remains.IndexOf(";");

          if (endIndex >= 0)
          {
            string part = remains.Substring(0, endIndex);
            remains = remains.Substring(endIndex);
            parts.Add(part);
          }
          else
          {
            string part = remains;
            remains = string.Empty;
            parts.Add(part);
          }
        }

        if (remains.StartsWith(";"))
          remains = remains.Substring(1);
      }

      return parts;
    }
  }
}
