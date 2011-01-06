using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pirate.PiVote.Serialization
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
  public class SerializeAdditionalFieldAttribute : Attribute
  {
    public Type Type { get; private set; }

    public string Name { get; private set; }

    public int Index { get; private set; }

    public string Comment { get; private set; }

    public string Condition { get; private set; }

    public SerializeAdditionalFieldAttribute(Type type, string name, int index, string comment)
      : this(type, name, index, comment, string.Empty)
    {
    }

    public SerializeAdditionalFieldAttribute(Type type, string name, int index, string comment, string condition)
    {
      Index = index;
      Comment = comment;
      Condition = condition;
    }
  }
}
