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
  public class ServerConfig : Config
  {
    public ServerConfig(string fileName)
      : base(fileName)
    { }

    /// <summary>
    /// Connection string for the MySQL database.
    /// </summary>
    public string MySqlConnectionString
    {
      get { return ReadString("MySqlConnectionString", null); }
    }

    protected override void Validate()
    {
      if (MySqlConnectionString.IsNullOrEmpty())
      {
        Save();
        throw new InvalidDataException("The MySQL connection string in the config file is not valid");
      }
    }
  }
}
