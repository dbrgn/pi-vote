/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace Pirate.PiVote
{
  /// <summary>
  /// Config file for the voting server.
  /// </summary>
  public class RemoteStoredConfig : Config, IRemoteConfig
  {
    public RemoteStoredConfig(string fileName)
      : base(fileName)
    { }

    protected override void Validate()
    {
      string dummy = null;

      dummy = SystemName.Text;
      dummy = WelcomeMessage.Text;
      dummy = Image.Length.ToString();

      Save();
    }

    /// <summary>
    /// Name of the eVoting system.
    /// </summary>
    public MultiLanguageString SystemName
    {
      get
      {
        MultiLanguageString systemName = new MultiLanguageString();
        systemName.Set(Language.English, ReadString("SystemName-English", "eVoting"));
        systemName.Set(Language.German, ReadString("SystemName-German", "eVoting"));
        systemName.Set(Language.French, ReadString("SystemName-French", "eVoting"));
        return systemName;
      }
    }

    /// <summary>
    /// Welcome message to users.
    /// </summary>
    public MultiLanguageString WelcomeMessage
    {
      get
      {
        MultiLanguageString welcomeMessage = new MultiLanguageString();
        welcomeMessage.Set(Language.English, ReadString("WelcomeMessage-English", "Welcome."));
        welcomeMessage.Set(Language.German, ReadString("WelcomeMessage-German", "Welcome."));
        welcomeMessage.Set(Language.French, ReadString("WelcomeMessage-French", "Welcome."));
        return welcomeMessage;
      }
    }

    /// <summary>
    /// Image file on the start wizard item.
    /// </summary>
    public byte[] Image
    {
      get
      {
        string fileName = ReadString("ImageFile", "_");

        if (File.Exists(fileName))
        {
          return File.ReadAllBytes(fileName);
        }
        else
        {
          return new byte[0];
        }
      }
    }

    /// <summary>
    /// Url of the project.
    /// </summary>
    public string Url
    {
      get { return ReadString("Url", string.Empty); }
    }

    /// <summary>
    /// The newest available version one could update to.
    /// </summary>
    public string UpdateVersion
    {
      get { return ReadString("UpdateVersion", string.Empty); }
    }

    /// <summary>
    /// Url were one can get the update.
    /// </summary>
    public string UpdateUrl
    {
      get { return ReadString("UpdateUrl", string.Empty); }
    }
  }
}
