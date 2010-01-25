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
using System.IO;
using System.Security.Cryptography;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  public class AuthorityCertificate : Certificate
  {
    public string FullName { get; private set; }

    public AuthorityCertificate(string fullName)
      : base()
    {
      FullName = fullName;
    }

    public AuthorityCertificate(DeserializeContext context)
      : base(context)
    { }

    public AuthorityCertificate(AuthorityCertificate original, bool onlyPublicPart)
      : base(original, onlyPublicPart)
    {
      FullName = original.FullName;
    }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(FullName);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      FullName = context.ReadString();
    }
  }
}
