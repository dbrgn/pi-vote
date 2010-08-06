﻿/*
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

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Authenticated and encrypted serializable object.
  /// </summary>
  public class Secure<TValue> : Signed<Encrypted<TValue>>
    where TValue : Serializable
  {
    /// <summary>
    /// Encrypts an serializable object.
    /// </summary>
    /// <param name="value">Serializable object.</param>
    /// <param name="receiverCertificate">Certificate of the receiver.</param>
    public Secure(TValue value, Certificate receiverCertificate, Certificate senderCertificate)
      : base(new Encrypted<TValue>(value, receiverCertificate), senderCertificate)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (receiverCertificate == null)
        throw new ArgumentNullException("receiverCertificate");
    }

    public Secure(DeserializeContext context)
      : base(context)
    { }
  }
}