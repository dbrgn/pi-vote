using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pirate.PiVote.Kiosk
{
  public enum Message
  {
    Invalid                     = 0,
    Reply                       = 1,
    Error                       = 2,
    FetchUserData               = 100000,
    PushSignatureRequest        = 100001,
    FetchCertificateStorage     = 100002,
    FetchServerCertificate      = 100003
  }
}
