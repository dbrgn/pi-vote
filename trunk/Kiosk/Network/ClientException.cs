using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pirate.PiVote.Kiosk
{
  public class ClientException :Exception
  {
    public ClientException(string message)
      : base(message)
    { }
  }
}
