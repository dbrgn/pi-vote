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

    /// <summary>
    /// TCP port on which the proxy listens.
    /// </summary>
    int ProxyPort { get; }

    /// <summary>
    /// DNS or IP address of the proxy.
    /// </summary>
    string ProxyAddress { get; }
  }
}
