using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace Pirate.PiVote.Gui
{
  public static class UpdateChecker
  {
    public static void CheckUpdate(IRemoteConfig config, string updateDialogTitle, string updateDialogText)
    {
      var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
      int major = 0;
      int minor = 0;
      int build = 0;
      int revision = 0;

      string[] parts = config.UpdateVersion.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
      if (parts.Length == 4)
      {
        int.TryParse(parts[0], out major);
        int.TryParse(parts[1], out minor);
        int.TryParse(parts[2], out build);
        int.TryParse(parts[3], out revision);
      }

      if (IsUpdateVersionNewer(assemblyVersion, major, minor, build, revision))
      {
        string version = string.Format("{0}.{1}.{2}.{3}", major, minor, build, revision);
        string text = string.Format(updateDialogText, assemblyVersion.ToString(), version);

        if (MessageForm.Show(
          text,
          updateDialogTitle,
          MessageBoxButtons.YesNo,
          MessageBoxIcon.Information,
          DialogResult.Yes)
          == DialogResult.Yes)
        {
          System.Diagnostics.Process.Start(config.UpdateUrl);
          System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
      }
    }

    private static bool IsUpdateVersionNewer(Version assemblyVersion, int major, int minor, int build, int revision)
    {
      if (major > assemblyVersion.Major)
      {
        return true;
      }
      else if (major == assemblyVersion.Major)
      {
        if (minor > assemblyVersion.Minor)
        {
          return true;
        }
        else if (minor == assemblyVersion.Minor)
        {
          if (build > assemblyVersion.Build)
          {
            return true;
          }
          else if (build == assemblyVersion.Build)
          {
            if (revision > assemblyVersion.Revision)
            {
              return true;
            }
          }
        }
      }

      return false;
    }
  }
}
