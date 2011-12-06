/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.CaGui
{
  /// <summary>
  /// Group id -> group name assigments
  /// </summary>
  public static class GroupList
  {
    public const string FileName = "groups.txt";

    public static IEnumerable<Group> Groups
    {
      get
      {
        if (File.Exists(FileName))
        {
          string[] lines = File.ReadAllLines(FileName);
          for (int id = 0; id < lines.Length; id++)
          {
            yield return new Group(id, new MultiLanguageString(lines[id]));
          }
        }
      }
    }

    public static string GetGroupName(int groupId)
    {
      var groups = Groups.Where(group => group.Id == groupId);

      if (groups.Count() > 0)
      {
        return groups.First().Name.Text;
      }
      else
      {
        return string.Format("Group {0}", groupId);
      }
    }
  }
}
