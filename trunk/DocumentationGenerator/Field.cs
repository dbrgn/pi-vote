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

    public Field(FieldType type, string name, string comment)
    {
      Type = type;
      Name = name;
      Comment = comment;
    }
  }
}
