/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
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
  public class ClientConfig : Config, IClientConfig
  {
    public ClientConfig(string fileName)
      : base(fileName)
    { }

    /// <summary>
    /// TCP port on with the server listens.
    /// </summary>
    public int ServerPort
    {
      get { return ReadInt32("ServerPort", 4242); }
    }

    /// <summary>
    /// DNS address of the Pi-Vote server.
    /// </summary>
    public string ServerAddress
    {
      get { return ReadString("ServerAddress", "pivote.server.org"); }
    }

    protected override void Validate()
    {
      string dummy = null;

      dummy = ServerPort.ToString();
      dummy = ServerAddress;
      dummy = SystemName.Text;
      dummy = WelcomeMessage.Text;
      dummy = ImageFile;

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
    public string ImageFile
    {
      get { return ReadString("ImageFile", null); }
    }
  }
}
