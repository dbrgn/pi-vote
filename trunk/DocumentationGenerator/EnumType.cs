using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentationGenerator
{
  public class EnumType : FieldType
  {
    public List<EnumValue> Values { get; private set; }

    public string Comment { get; private set; }

    public EnumType(string name, string comment)
      : base(name)
    {
      Comment = comment;
      Values = new List<EnumValue>();
    }
  }
}
