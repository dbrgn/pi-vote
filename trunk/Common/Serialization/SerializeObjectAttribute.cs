using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pirate.PiVote.Serialization
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
  public class SerializeObjectAttribute : Attribute
  {
    public string Comment { get; private set; }

    public SerializeObjectAttribute(string comment)
    {
      Comment = comment;
    }
  }
}
