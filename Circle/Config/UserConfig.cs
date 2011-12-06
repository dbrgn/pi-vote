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
  public class 
    UserConfig : Config
  {
    public UserConfig(string fileName)
      : base(fileName)
    { }

    /// <summary>
    /// Initial number of proofs to check.
    /// </summary>
    public int InitialCheckProofCount
    {
      get { return ReadInt32("InitialCheckProofCount", Math.Min(8, Environment.ProcessorCount)); }
      set { Write("InitialCheckProofCount", value); }
    }

    protected override void Validate()
    {
      string dummy = null;

      dummy = InitialCheckProofCount.ToString();

      Save();
    }
  }
}
