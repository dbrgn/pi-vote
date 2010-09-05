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
  /// Remote client config interface.
  /// </summary>
  public interface IRemoteConfig
  {
    /// <summary>
    /// Name of the voting system.
    /// </summary>
    MultiLanguageString SystemName { get; }

    /// <summary>
    /// Welcome message for users.
    /// </summary>
    MultiLanguageString WelcomeMessage { get; }

    /// <summary>
    /// Name of the image file.
    /// </summary>
    byte[] Image { get; }

    /// <summary>
    /// Url of the project.
    /// </summary>
    string Url { get; }

    /// <summary>
    /// The newest available version one could update to.
    /// </summary>
    string UpdateVersion { get; }

    /// <summary>
    /// Url were one can get the update.
    /// </summary>
    string UpdateUrl { get; }
  }
}
