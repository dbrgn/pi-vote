
/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// Binary RPC proxy.
  /// </summary>
  /// <remarks>
  /// Implemented by web service client.
  /// </remarks>
  public interface IBinaryRpcProxy
  {
    byte[] Execute(byte[] request);
  }
}
