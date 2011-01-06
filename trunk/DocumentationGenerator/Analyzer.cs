using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Pirate.PiVote.Serialization;

namespace DocumentationGenerator
{
  public class Analyzer
  {
    public IEnumerable<FieldType> Types { get { return this.types.Values; } }

    private Dictionary<string, FieldType> types;
    private Type serializeObjectAttribute = typeof(SerializeObjectAttribute);
    private Type serializeAdditionalFieldAttribute = typeof(SerializeAdditionalFieldAttribute);
    private Type serializeFieldAttribute = typeof(SerializeFieldAttribute);
    private Type serializableType = typeof(Serializable);
    private Type serializeEnumAttribute = typeof(SerializeEnumAttribute);

    private void AddBasicType(string name, string comment)
    {
      this.types.Add(name, new BasicType(name, comment));
    }

    public void Analyze()
    {
      this.types = new Dictionary<string, FieldType>();
      AddBasicType("System.Int32", "4 byte signed integer written in little endian format");
      AddBasicType("System.UInt32", "4 byte unsigned integer written in little endian format");
      AddBasicType("System.Int64", "8 byte signed integer written in little endian format");
      AddBasicType("System.UInt64", "8 byte unsigned integer written in little endian format");
      AddBasicType("System.Single", "4 byte floating point written as per IEEE 754");
      AddBasicType("System.Double", "8 byte floating point written as per IEEE 754");
      AddBasicType("System.Boolean", "1 byte boolean written as 0 when false and 1 when true");
      AddBasicType("System.Guid", "16 byte Globally Unique Identifier written as Byte[]");
      AddBasicType("System.Byte", "Just one byte");
      AddBasicType("System.Byte[]", "UInt32 length followed by the individual bytes.");
      AddBasicType("System.DateTime", "Number of 100-nanosecond intervals that have elapsed since 12:00:00 midnight, January 1, 0001 written as Int64");
      AddBasicType("System.String", "UInt32 length followed by UTF8 encoded string data");
      AddBasicType("Emil.GMP.BigInt", "Arbitrary-sized integer as specified by GNU Multiprecision Library written as Byte[]");
      AddBasicType("Pirate.PiVote.PiException", "Exception code as Int32 followed by String message.");
      AddBasicType("Pirate.PiVote.MultiLanguageString", "Number of language entries as Int32 followed by each entry as Language as Int32 and String text");

      var assembly = this.serializableType.Assembly;

      foreach (Type type in assembly.GetTypes())
      {
        if (type.IsEnum)
        {
          if (type.GetCustomAttributes(this.serializeEnumAttribute, false).Length > 0)
          {
            AnalyzeType(type);
          }
        }
        if (type.IsSubclassOf(this.serializableType))
        {
          AnalyzeType(type);
        }
      }
    }

    private void AnalyzeType(Type type)
    {
      if (!this.types.ContainsKey(type.ProperFullName()))
      {
        if (type.IsEnum)
        {
          AnalyzeEnumType(type);
        }
        if (type.IsSubclassOf(this.serializableType))
        {
          var soa = (SerializeObjectAttribute)type.GetCustomAttributes(this.serializeObjectAttribute, false).SingleOrDefault();

          ObjectType objectType = new ObjectType(type.ProperFullName(), soa.Comment);

          foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
          {
            if (field.DeclaringType == type)
            {
              AnalyzeType(objectType, field);
            }
          }

          foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance))
          {
            if (property.DeclaringType == type)
            {
              AnalyzeType(objectType, property);
            }
          }

          if (soa == null)
          {
            Console.WriteLine("  Serialize Object Attribute missing");
          }

          objectType.Validate();

          this.types.Add(objectType.Name, objectType);
          
          if (soa == null)
          {
            Console.ReadLine();
          }
        }
        else if (type.OwnName() == "System.Tuple")
        {
          ObjectType objectType = new ObjectType(type.ProperFullName(), "Tuple of values.");

          foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
          {
            AnalyzeType(objectType, property);
          }

          objectType.Validate();

          this.types.Add(objectType.Name, objectType);
        }
        else
        {
          Console.WriteLine("**  Type {0} cannot be analyzed.", type.ProperFullName());
        }
      }
    }

    private void AnalyzeEnumType(Type type)
    {
      var enumAttribute = type.GetCustomAttributes(this.serializeEnumAttribute, false).SingleOrDefault() as SerializeEnumAttribute;
      var enumType = new EnumType(type.FullName, enumAttribute == null ? string.Empty : enumAttribute.Comment);

      List<int> values = new List<int>();
      foreach (object v in Enum.GetValues(type)) { values.Add((int)v); }
      var names = Enum.GetNames(type);

      for (int index = 0; index < names.Length; index++)
      {
        enumType.Values.Add(new EnumValue(values[index], names[index]));
      }

      this.types.Add(enumType.Name, enumType);

      if (enumAttribute == null)
      {
        Console.WriteLine("**  Serialize Enum Attribute on " + type.FullName + " missing");
      }
    }

    private void AnalyzeType(ObjectType objectType, FieldInfo field)
    {
      var attribute = (SerializeFieldAttribute)field.GetCustomAttributes(this.serializeFieldAttribute, false).SingleOrDefault();

      if (attribute != null)
      {
        AnalyzeType(
          objectType,
          attribute.AlternateType == null ? field.FieldType : attribute.AlternateType,
          attribute,
          field.Name);
      }
    }

    private void AnalyzeType(ObjectType objectType, PropertyInfo property)
    {
      var attribute = (SerializeFieldAttribute)property.GetCustomAttributes(this.serializeFieldAttribute, false).SingleOrDefault();

      if (attribute != null)
      {
        AnalyzeType(
          objectType, 
          attribute.AlternateType == null ? property.PropertyType : attribute.AlternateType, 
          attribute, 
          property.Name);
      }
    }
    
    private void AnalyzeType(ObjectType objectType, Type type, SerializeFieldAttribute attribute, string fieldName)
    {
      if (this.types.ContainsKey(type.ProperFullName()))
      {
        objectType.AddField(attribute.Index, new Field(this.types[type.ProperFullName()], fieldName, attribute.Comment));
      }
      else if (type.IsSubclassOf(this.serializableType))
      {
        AnalyzeType(type);
        objectType.AddField(attribute.Index, new Field(this.types[type.ProperFullName()], fieldName, attribute.Comment));
      }
      else if (IsListOf(type))
      {
        AnalyzeListType(type);
        objectType.AddField(attribute.Index, new Field(this.types[type.ProperFullName()], fieldName, attribute.Comment));
      }
      else if (type.IsEnum)
      {
        AnalyzeEnumType(type);
        objectType.AddField(attribute.Index, new Field(this.types[type.ProperFullName()], fieldName, attribute.Comment));
      }
      else
      {
        Console.WriteLine("**  Type {0} not recognized", type.ProperFullName());
      }
    }

    private void AnalyzeListType(Type type)
    {
      switch (type.OwnName())
      {
        case "System.Collections.Generic.List":
          if (!this.types.ContainsKey(type.GetGenericArguments()[0].ProperFullName()))
          {
            AnalyzeType(type.GetGenericArguments()[0]);
          }

          var listType = new ListType(type.ProperFullName(), this.types[type.GetGenericArguments()[0].ProperFullName()]);
          this.types.Add(listType.Name, listType);

          break;
        case "System.Collections.Generic.Dictionary":
          if (!this.types.ContainsKey(type.GetGenericArguments()[1].ProperFullName()))
          {
            AnalyzeType(type.GetGenericArguments()[1]);
          }

          var listType2 = new ListType(type.ProperFullName(), this.types[type.GetGenericArguments()[1].ProperFullName()]);
          this.types.Add(listType2.Name, listType2);

          break;
        default:
          throw new Exception("Unknown list type.");
      }
    }

    private bool IsListOf(Type type)
    {
      if (type.IsGenericType)
      {
        switch (type.OwnName())
        {
          case "System.Collections.Generic.List":
            return 
              this.types.ContainsKey(type.GetGenericArguments()[0].ProperFullName()) ||
              type.GetGenericArguments()[0].IsSubclassOf(typeof(Serializable)) ||
              type.GetGenericArguments()[0].OwnName() == "System.Tuple";
          case "System.Collections.Generic.Dictionary":
            return
              type.GetGenericArguments()[0] == typeof(int) &&
              (this.types.ContainsKey(type.GetGenericArguments()[1].ProperFullName()) ||
              type.GetGenericArguments()[1].IsSubclassOf(typeof(Serializable)) ||
              type.GetGenericArguments()[1].OwnName() == "System.Tuple");
          default:
            return false;
        }
      }
      else
      {
        return false;
      }
    }
  }
}

