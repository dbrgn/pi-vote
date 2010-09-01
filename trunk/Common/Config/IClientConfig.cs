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
  /// Client config interface.
  /// </summary>
  public interface IClientConfig
  {
    /// <summary>
    /// DNS address of the Pi-Vote server.
    /// </summary>
    string ServerAddress { get; }

    /// <summary>
    /// TCP port on with the server listens.
    /// </summary>
    int ServerPort { get; }
  }
}
