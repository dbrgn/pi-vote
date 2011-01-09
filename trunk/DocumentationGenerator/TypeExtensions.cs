using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Pirate.PiVote.Serialization;

namespace System
{
  public static class TypeExtensions
  {
    public static string GenericFullName(this Type type)
    {
      if (string.IsNullOrEmpty(type.FullName))
      {
        return type.Namespace + "." + type.Name.Split(new string[] { "`" }, StringSplitOptions.RemoveEmptyEntries)[0];
      }
      else
      {
        if (type.IsGenericType)
        {
          return type.OwnName() + "[" + GenericArguments(type, true) + "]";
        }
        else
        {
          return type.FullName;
        }
      }
    }

    public static string SpecificFullName(this Type type)
    {
      if (string.IsNullOrEmpty(type.FullName))
      {
        return type.Namespace + "." + type.Name.Split(new string[] { "`" }, StringSplitOptions.RemoveEmptyEntries)[0];
      }
      else
      {
        if (type.IsGenericType)
        {
          return type.OwnName() + "[" + GenericArguments(type, false) + "]";
        }
        else
        {
          return type.FullName;
        }
      }
    }
    
    public static string OwnName(this Type type)
    {
      if (string.IsNullOrEmpty(type.FullName))
      {
        return type.Namespace + "." + type.Name.Split(new string[] { "`" }, StringSplitOptions.RemoveEmptyEntries)[0];
      }
      else
      {
        return type.FullName.Split(new string[] { "`" }, StringSplitOptions.RemoveEmptyEntries)[0];
      }
    }

    private static string GenericArguments(Type type, bool generic)
    {
      if (generic)
      {
        Dictionary<int, GenericArgumentAttribute> genericArguments = new Dictionary<int, GenericArgumentAttribute>();

        foreach (GenericArgumentAttribute genericArgument in type.GetCustomAttributes(typeof(GenericArgumentAttribute), false))
        {
          genericArguments.Add(genericArgument.Index, genericArgument);
        }

        int index = 0;
        List<string> arguments = new List<string>();

        foreach (var argument in type.GetGenericArguments())
        {
          if (type.Namespace == "System.Collections.Generic" && type.Name.StartsWith("List") && index == 0)
          {
            arguments.Add("TValue");
          }
          else if (type.Namespace == "System.Collections.Generic" && type.Name.StartsWith("Dictionary") && index == 0)
          {
            arguments.Add("TKey");
          }
          else if (type.Namespace == "System.Collections.Generic" && type.Name.StartsWith("Dictionary") && index == 1)
          {
            arguments.Add("TValue");
          }
          else if (genericArguments.ContainsKey(index))
          {
            arguments.Add(genericArguments[index].Name);
          }
          else
          {
            arguments.Add(string.Format("T{0}", index));
          }

          index++;
        }

        return string.Join(", ", arguments.ToArray());
      }
      else
      {
        var arguments = type.GetGenericArguments().Select(t => t.SpecificFullName());

        return string.Join(", ", arguments.ToArray());
      }
    }
  }
}
