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
using System.IO;

namespace Pirate.PiVote.DocumentationGenerator
{
  public class Generator
  {
    private const string TagSeparator = "$$";

    private const string ArgumentSeparator = ",";

    private const string TemplateFolder = "Templates";

    private StringBuilder document;

    public Generator()
    { 
    }

    private string BasePath
    {
      get
      {
        return new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
      }
    }

    private void AddTemplate(string templateFile)
    {
      string rawText = File.ReadAllText(Path.Combine(Path.Combine(Path.GetDirectoryName(BasePath), TemplateFolder), templateFile), Encoding.UTF8);

      string[] parts = rawText.Split(new string[] { TagSeparator }, StringSplitOptions.None);

      for (int index = 0; index < parts.Length; index++)
      {
        if (index % 2 == 0)
        {
          this.document.Append(parts[index]);
        }
        else if (parts[index] == string.Empty)
        {
          this.document.Append(TagSeparator);
        }
        else
        {
          this.document.Append(Function(parts[index]));
        }
      }
    }

    private string Function(string text)
    {
      string[] parts = text.Split(new string[] { ArgumentSeparator }, StringSplitOptions.None);

      switch (parts[0])
      {
        case "version":
          return typeof(Pirate.PiVote.Serialization.Serializable).Assembly.GetName().Version.ToString();
        default:
          throw new ArgumentException("Unknow function");
      }
    }

    public void Generate(IEnumerable<FieldType> types, IEnumerable<Request> requests)
    {
      this.document = new StringBuilder();

      AddTemplate("Meta.tex");
      AddTemplate("Header.tex");
      AddTemplate("Rpc.tex");
      AddTemplate("Sequence.tex");
      GenerateRequests(requests);
      GenerateTypes(types);
      AddTemplate("Footer.tex");
    }

    private void GenerateRequests(IEnumerable<Request> requests)
    {
      this.document.AppendLine(@"\section{Requests}");
      this.document.AppendLine();
      this.document.AppendLine("The following request are used to upload and download data from the Pi-Vote server. For detailed data types, consult the Types section.");
      this.document.AppendLine();

      foreach (Request request in requests.OrderBy(r => r.Name))
      {
        this.document.AppendLine(@"\begin{centering}");
        this.document.AppendLine(@"\begin{supertabular}{| p{2.2cm} | p{12.6cm} |}");

        this.document.AppendLine(@"\hline");
        this.document.AppendLine(@"\bf{Type} &");
        this.document.AppendLine(request.Name + @" \\");

        this.document.AppendLine(@"\bf{Text} &");
        this.document.AppendLine(request.Text + @" \\");

        if (!request.Input.IsNullOrEmpty())
        {
          this.document.AppendLine(@"\bf{Input} &");
          this.document.AppendLine(request.Input + @" \\");
        }

        if (!request.Output.IsNullOrEmpty())
        {
          this.document.AppendLine(@"\bf{Output} &");
          this.document.AppendLine(request.Output + @" \\");
        }
        
        this.document.AppendLine(@"\hline");

        this.document.AppendLine(@"\end{supertabular}");
        this.document.AppendLine(@"\end{centering}");
        this.document.AppendLine(@"\vspace{0.3cm}");
        this.document.AppendLine();
      } 
      
      this.document.AppendLine(@"\newpage");
    }

    private void GenerateTypes(IEnumerable<FieldType> types)
    {
      this.document.AppendLine(@"\section{Types}");
      this.document.AppendLine();
      this.document.AppendLine("The following type formats are used for messages and other containers contained in messages.");
      this.document.AppendLine();

      GenerateBasics(types);

      this.document.AppendLine(@"\newpage");

      GenerateLists(types);

      this.document.AppendLine(@"\newpage");

      GenerateEnums(types);

      this.document.AppendLine(@"\newpage");

      GenerateObjects(types);
    }

    private void GenerateEnums(IEnumerable<FieldType> types)
    {
      this.document.AppendLine(@"\subsection{Enumerations}");
      this.document.AppendLine();
      this.document.AppendLine(@"The following enumerations are used. The values and their names are specified below.");
      this.document.AppendLine();

      foreach (EnumType type in types.Where(t => t is EnumType).OrderBy(t => t.Name))
      {
        this.document.AppendLine(@"\begin{centering}");
        this.document.AppendLine(@"\begin{supertabular}{| p{2.2cm} | p{10.4cm} p{1.8cm} |}");

        this.document.AppendLine(@"\hline");
        this.document.AppendLine(@"\bf{Type} &");
        this.document.AppendLine(type.Name + " &");
        this.document.AppendLine(@" \\");

        if (type.ShortName != type.Name)
        {
          this.document.AppendLine(@"\bf{Short} &");
          this.document.AppendLine(type.ShortName + @" \\");
        }

        this.document.AppendLine(@"\bf{Comment} &");
        this.document.AppendLine(type.Comment + " &");
        this.document.AppendLine(@" \\");

        this.document.AppendLine(@"\hline");

        bool firstValue = true;

        foreach (var value in type.Values)
        {
          if (firstValue)
          {
            this.document.AppendLine(@"\bf{Values} &");
            firstValue = false;
          }
          else
          {
            this.document.AppendLine(" &");
          }

          this.document.AppendLine(value.Name + " &");
          this.document.AppendLine(value.Value.ToString() + @" \\");
        }
        
        this.document.AppendLine(@"\hline");

        this.document.AppendLine(@"\end{supertabular}");
        this.document.AppendLine(@"\end{centering}");
        this.document.AppendLine(@"\vspace{0.3cm}");
        this.document.AppendLine();
      }
    }

    private void GenerateBasics(IEnumerable<FieldType> types)
    {
      this.document.AppendLine(@"\subsection{Basic Types}");
      this.document.AppendLine();
      this.document.AppendLine(@"The following basic types are used throughout Pi-Vote.");
      this.document.AppendLine();

      foreach (BasicType type in types.Where(t => t is BasicType).OrderBy(t => t.Name))
      {
        this.document.AppendLine(@"\begin{centering}");
        this.document.AppendLine(@"\begin{supertabular}{| p{2.2cm} | p{12.6cm} |}");

        this.document.AppendLine(@"\hline");
        this.document.AppendLine(@"\bf{Type} &");
        this.document.AppendLine(type.Name + @" \\");

        if (type.ShortName != type.Name)
        {
          this.document.AppendLine(@"\bf{Short} &");
          this.document.AppendLine(type.ShortName + @" \\");
        }

        this.document.AppendLine(@"\bf{Serialize} &");
        this.document.AppendLine(type.Comment + @" \\");

        this.document.AppendLine(@"\hline");

        this.document.AppendLine(@"\end{supertabular}");
        this.document.AppendLine(@"\end{centering}");
        this.document.AppendLine(@"\vspace{0.3cm}");
        this.document.AppendLine();
      }
    }

    private void GenerateLists(IEnumerable<FieldType> types)
    {
      this.document.AppendLine(@"\subsection{List Types}");
      this.document.AppendLine();
      this.document.AppendLine(@"The following list types are used. They are all generic an can contains any number of items.");
      this.document.AppendLine();

      foreach (BaseListType type in types.Where(t => t is BaseListType).OrderBy(t => t.Name))
      {
        this.document.AppendLine(@"\begin{centering}");
        this.document.AppendLine(@"\begin{supertabular}{| p{2.2cm} | p{12.6cm} |}");

        this.document.AppendLine(@"\hline");
        this.document.AppendLine(@"\bf{Type} &");
        this.document.AppendLine(type.Name + @" \\");

        if (type.ShortName != type.Name)
        {
          this.document.AppendLine(@"\bf{Short} &");
          this.document.AppendLine(type.ShortName + @" \\");
        }

        this.document.AppendLine(@"\bf{Serialize} &");
        this.document.AppendLine(type.Comment + @" \\");

        this.document.AppendLine(@"\hline");

        this.document.AppendLine(@"\end{supertabular}");
        this.document.AppendLine(@"\end{centering}");
        this.document.AppendLine(@"\vspace{0.3cm}");
        this.document.AppendLine();
      }
    }
    
    private void GenerateObjects(IEnumerable<FieldType> types)
    {
      this.document.AppendLine(@"\subsection{Objects}");
      this.document.AppendLine();
      this.document.AppendLine(@"The following composite types are used. They inherit their fields from one another.");
      this.document.AppendLine();

      foreach (ObjectType type in types.Where(t => t is ObjectType).OrderBy(t => t.Name))
      {
        this.document.AppendLine(@"\begin{centering}");
        this.document.AppendLine(@"\begin{supertabular}{| p{2.2cm} | p{12.6cm} |}");

        this.document.AppendLine(@"\hline");

        this.document.AppendLine(@"\bf{Type Name} &");
        this.document.AppendLine(type.Name + @" \\");

        if (type.ShortName != type.Name)
        {
          this.document.AppendLine(@"\bf{Short Name} &");
          this.document.AppendLine(type.ShortName + @" \\");
        }

        if (type.Inherits != null)
        {
          this.document.AppendLine(@"\bf{Inherits} &");
          this.document.AppendLine(type.Inherits.ShortName + @" \\");
        }

        this.document.AppendLine(@"\bf{Comment} &");
        this.document.AppendLine(type.Comment + @" \\");

        foreach (var fields in type.Fields)
        {
          this.document.AppendLine(@"\hline");

          this.document.AppendLine(@"\bf{Field Type} &");
          this.document.AppendLine(fields.ShortFieldTypeName + @" \\");

          this.document.AppendLine(@"\bf{Field Name }&");
          this.document.AppendLine(fields.Name + @" \\");

          this.document.AppendLine(@"\bf{Comment} &");
          this.document.AppendLine(fields.Comment + @" \\");

          if (!string.IsNullOrEmpty(fields.Condition))
          {
            this.document.AppendLine(@"\bf{Condition} &");
            this.document.AppendLine(fields.Condition + @" \\");
          }

          if (fields.MinVersion > 0)
          {
            this.document.AppendLine(@"\bf{Min Version} &");
            this.document.AppendLine(fields.MinVersion.ToString() + @" \\");
          }
        }

        this.document.AppendLine(@"\hline");

        this.document.AppendLine(@"\end{supertabular}");
        this.document.AppendLine(@"\end{centering}");
        this.document.AppendLine(@"\vspace{0.3cm}");
        this.document.AppendLine();
      }
    }

    public string Text { get { return this.document.ToString(); } }
  }
}
