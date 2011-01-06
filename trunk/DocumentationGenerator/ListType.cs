using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentationGenerator
{
  public class ListType : FieldType
  {
    public FieldType ListOf { get; private set; }

    public ListType(string name, FieldType listOf)
      : base(name)
    {
      ListOf = listOf;
    }
  }
}
