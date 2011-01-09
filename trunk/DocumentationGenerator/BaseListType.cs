using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentationGenerator
{
  public class BaseListType : FieldType
  {
    public string Comment { get; private set; }

    public BaseListType(string name, string comment)
      : base(name)
    {
      Comment = comment;
    }
  }
}
