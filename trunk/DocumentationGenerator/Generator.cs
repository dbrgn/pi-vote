using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentationGenerator
{
  public class Generator
  {
    private StringBuilder document;

    public Generator()
    { 
    }

    public void Generate(IEnumerable<FieldType> types)
    {
      string version = typeof(Pirate.PiVote.Serialization.Serializable).Assembly.GetName().Version.ToString();

      this.document = new StringBuilder();

      this.document.AppendLine(@"\documentclass[a4paper]{article}");
      this.document.AppendLine(@"\usepackage[UTF8]{inputenc}");
      this.document.AppendLine(@"\usepackage{supertabular}");
      this.document.AppendLine(@"\usepackage{fullpage}");

      this.document.AppendLine();
      this.document.AppendLine(@"\title{Pi-Vote Protocol Documentation}");
      this.document.AppendLine(@"\author{");
      this.document.AppendLine(@"\and");
      this.document.AppendLine(@"Pi-Vote Doc Generator, Pirate Party Switzerland");
      this.document.AppendLine(@"\and");
      this.document.AppendLine(@"Stefan Thöni, Pirate Party Switzerland");
      this.document.AppendLine(@"}");
      this.document.AppendLine(@"\date{Steinhausen, \today, Version " + version + "}");

      this.document.AppendLine();
      this.document.AppendLine(@"\begin{document}");
      this.document.AppendLine();
      this.document.AppendLine(@"\maketitle");
      this.document.AppendLine();
      this.document.AppendLine(@"\tableofcontents");
      this.document.AppendLine();
      this.document.AppendLine(@"\newpage");

      GenerateRpc();

      GenerateTypes(types);

      this.document.AppendLine(@"\end{document}");
    }

    private void GenerateRpc()
    {
      this.document.AppendLine(@"\section{RPC Protocol}");
      this.document.AppendLine();
      this.document.AppendLine(@"Pi-Vote uses an Remote Procedure Call protocol over TCP. To establish");
      this.document.AppendLine(@"communication the client opens a TCP connection to the server. All");
      this.document.AppendLine(@"action is initiated by the client sending a request. The server");
      this.document.AppendLine(@"processes these request and answers each one with a response.");

      this.document.AppendLine(@"\subsection{Messages}");
      this.document.AppendLine();
      this.document.AppendLine(@"Both request and response are messages which use a common transmission");
      this.document.AppendLine(@"format.");

      this.document.AppendLine(@"\begin{center}");
      this.document.AppendLine(@"\begin{supertabular}{| p{2cm} | p{2cm} | p{8cm}|}");

      this.document.AppendLine(@"\hline");
      this.document.AppendLine(@"\bf{Part} &");
      this.document.AppendLine(@"\bf{Type} &");
      this.document.AppendLine(@"\bf{Usage} \\");

      this.document.AppendLine(@"\hline");
      this.document.AppendLine(@"Length &");
      this.document.AppendLine(@"Int32 &");
      this.document.AppendLine(@"Contains the length of the following data.\\");

      this.document.AppendLine(@"\hline");
      this.document.AppendLine(@"Data &");
      this.document.AppendLine(@"Byte[] &");
      this.document.AppendLine(@"Contains the serialized message data.\\");

      this.document.AppendLine(@"\hline");
      this.document.AppendLine(@"\end{supertabular}");
      this.document.AppendLine(@"\end{center}");

      this.document.AppendLine(@"\subsection{Example}");
      this.document.AppendLine();
      this.document.AppendLine(@"Exampele of a keep alive request.");

      this.document.AppendLine(@"\begin{center}");
      this.document.AppendLine(@"\begin{supertabular}{| p{7cm} | p{8cm} |}");
      this.document.AppendLine(@"\hline");
      this.document.AppendLine(@"Hex bytes &");
      this.document.AppendLine(@"Comment \\");
      this.document.AppendLine(@"\hline");
      this.document.AppendLine(@"37 00 00 00 &");
      this.document.AppendLine(@"Length of the following data. \\");
      this.document.AppendLine(@"22 50 69 72 61 74 65 2e  50 69 56 6f 74 65 2e 52 70 63 2e 4b 65 65 70 41 6c 69 76 65 52 65 71 75 65 73 74 &");
      this.document.AppendLine(@"String 'Pirate.PiVote.Rpc.KeepAliveRequest' in UTF8 with prefixed length. \\");
      this.document.AppendLine(@"10 00 00 00 09 35 5d d3  9a b9 8a 41 96 e0 41 18 82 a5 85 90 &");
      this.document.AppendLine(@"Request Guid as 16 bytes with prefixed length. \\");
      this.document.AppendLine(@"\hline");
      this.document.AppendLine(@"\end{supertabular}");
      this.document.AppendLine(@"\end{center}");

      this.document.AppendLine(@"And the corresponding keep alive response:");

      this.document.AppendLine(@"\begin{center}");
      this.document.AppendLine(@"\begin{supertabular}{| p{7cm} | p{8cm} |}");
      this.document.AppendLine(@"\hline");
      this.document.AppendLine(@"Hex bytes &");
      this.document.AppendLine(@"Comment \\");
      this.document.AppendLine(@"\hline");
      this.document.AppendLine(@"39 00 00 00 &");
      this.document.AppendLine(@"Length of the following data. \\");
      this.document.AppendLine(@"23 50 69 72 61 74 65 2e  50 69 56 6f 74 65 2e 52 70 63 2e 4b 65 65 70 41  6c 69 76 65 52 65 73 70 6f 6e 73 65 &");
      this.document.AppendLine(@"String 'Pirate.PiVote.Rpc.KeepAliveResponse' in UTF8 with prefixed length. \\");
      this.document.AppendLine(@"10 00 00 00 09 35 5d d3  9a b9 8a 41 96 e0 41 18 82 a5 85 90 &");
      this.document.AppendLine(@"Request Guid as 16 bytes with prefixed length. \\");
      this.document.AppendLine(@"01 &");
      this.document.AppendLine(@"Boolean specifying that no exception occurred in execution. \\");
      this.document.AppendLine(@"\hline");
      this.document.AppendLine(@"\end{supertabular}");
      this.document.AppendLine(@"\end{center}"); 
      
      this.document.AppendLine();
      this.document.AppendLine(@"\newpage");
      this.document.AppendLine();
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
