/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pirate.PiVote.Kiosk
{
  public enum MessageType : byte
  {
    Unknown                       = 0x00,
    Request                       = 0x01,
    Reply                         = 0x02
  }

  public enum MessageFunction : byte
  {
    Unknown                       = 0x00,
    FetchUserData                 = 0x01,
    PushSignatureRequest          = 0x02,
    FetchCertificateStorage       = 0x03,
    FetchServerCertificate        = 0x04
  }

  public enum MessageStatus : byte
  {
    Unknown                       = 0x00,
    Success                       = 0x01,
    FailBadRequest                = 0x02,
    FailServerError               = 0x03
  }
}
