using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pirate.PiVote.Serialization
{
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
  public class SerializeFieldAttribute : Attribute
  {
    public int Index { get; private set; }

    public string Comment { get; private set; }

    public string Condition { get; private set; }

    public Type AlternateType { get; private set; }

    public SerializeFieldAttribute(int index, string comment)
      : this(index, comment, string.Empty, null)
    { 
    }

    public SerializeFieldAttribute(int index, string comment, Type alternateType)
      : this(index, comment, string.Empty, alternateType)
    {
    }

    public SerializeFieldAttribute(int index, string comment, string condition, Type alternateType)
    {
      Index = index;
      Comment = comment;
      Condition = condition;
      AlternateType = alternateType;
    }
  }
}
