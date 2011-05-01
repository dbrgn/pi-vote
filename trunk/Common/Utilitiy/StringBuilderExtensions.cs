using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
  public static class StringBuilderExtensions
  {
    public static void AppendLine(this StringBuilder builder, string text, params object[] arguments)
    {
      builder.AppendLine(string.Format(text, arguments));
    }
  }
}
