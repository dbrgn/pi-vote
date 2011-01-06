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
    public static string ProperFullName(this Type type)
    {
      if (type.IsGenericType)
      {
        string argumentNames = string.Join(", ", type.GetGenericArguments().Select(argument => argument.ProperFullName()).ToArray());

        return type.OwnName() + "[" + argumentNames + "]";
      }
      else
      {
        return type.FullName;
      }
    }

    public static string OwnName(this Type type)
    {
      return type.FullName.Split(new string[] { "`" }, StringSplitOptions.RemoveEmptyEntries)[0];
    }
  }
}
