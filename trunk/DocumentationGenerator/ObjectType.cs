using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentationGenerator
{
  public class ObjectType : FieldType
  {
    private SortedList<int, Field> fields;

    public string Comment { get; private set; }

    public IEnumerable<Field> Fields { get { return this.fields.Values; } }

    public ObjectType Inherits { get; private set; }

    public ObjectType(string name, string comment, ObjectType inherits)
      : base(name)
    {
      Comment = comment;
      Inherits = inherits;
      this.fields = new SortedList<int, Field>();
    }

    public void AddField(int index, Field field)
    {
      this.fields.Add(index, field);
    }

    public void Validate()
    {
    }
  }
}
