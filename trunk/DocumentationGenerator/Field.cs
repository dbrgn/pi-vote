using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentationGenerator
{
  public class Field
  {
    public FieldType Type { get; private set; }

    public string Name { get; private set; }

    public string Comment { get; private set; }

    public string Condition { get; private set; }

    public string FieldTypeName { get; private set; }

    public string ShortFieldTypeName
    {
      get
      {
        return FieldTypeName
          .Replace("Pirate.PiVote.", string.Empty)
          .Replace("System.", string.Empty);
      }
    }

    public Field(FieldType type, string fieldTypeName, string name, string comment, string condition)
    {
      Type = type;
      FieldTypeName = fieldTypeName;
      Name = name.Substring(0, 1).ToUpper() + name.Substring(1); 
      Comment = comment;
      Condition = condition;
    }
  }
}
