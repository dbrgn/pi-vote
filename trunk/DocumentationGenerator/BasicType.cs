using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentationGenerator
{
  public class BasicType : FieldType
  {
    public string Comment { get; private set; }

    public BasicType(string name, string comment)
      : base(name)
    {
      Comment = comment;
    }
  }
}
