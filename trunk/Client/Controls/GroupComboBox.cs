using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Rpc;

namespace Pirate.PiVote.Client
{
  public class GroupComboBox : ComboBox
  {
    private Dictionary<int, Group> indexToGroup;
    private Dictionary<Group, int> groupToIndex;

    public GroupComboBox()
    {
      this.indexToGroup = new Dictionary<int, Group>();
      this.groupToIndex = new Dictionary<Group, int>();
    }

    public void Clear()
    {
      Items.Clear();
      this.indexToGroup.Clear();
      this.groupToIndex.Clear();
    }

    public void Add(Group group)
    {
      int index = Items.Add(group.Name.Text);
      this.indexToGroup.Add(index, group);
      this.groupToIndex.Add(group, index);
    }

    public void Add(IEnumerable<Group> groups)
    {
      groups.Foreach(group => Add(group));
    }

    public Group Value
    {
      get
      {
        if (this.indexToGroup.ContainsKey(SelectedIndex))
        {
          return this.indexToGroup[SelectedIndex];
        }
        else
        {
          return null;
        }
      }
      set
      {
        if (value == null)
        {
          SelectedIndex = -1;
        }
        else
        {
          SelectedIndex = this.groupToIndex[value];
        }
      }
    }
  }
}
