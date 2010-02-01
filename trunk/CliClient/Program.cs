/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;

namespace Pirate.PiVote.CliClient
{
  class Program
  {
    static void Main(string[] args)
    {
      Client client = new Client();
      client.Start();
    }
  }
}
