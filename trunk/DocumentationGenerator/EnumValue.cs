using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentationGenerator
{
  public class EnumValue
  {
    public int Value { get; private set; }

    public string Name { get; private set; }

    public EnumValue(int value, string name)
    {
      Value = value;
      Name = name;
    }
  }
}
