using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentationGenerator
{
  public class FieldType
  {
    public string Name { get; private set; }

    public string ShortName
    {
      get
      {
        return Name
          .Replace("Pirate.PiVote.", string.Empty)
          .Replace("System.", string.Empty);
      }
    }

    public FieldType(string name)
    {
      Name = name.Substring(0, 1).ToUpper() + name.Substring(1);
    }
  }
}
