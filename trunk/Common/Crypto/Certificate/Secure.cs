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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Authenticated and encrypted serializable object.
  /// </summary>
  [SerializeObject("Authenticated and encrypted serializable object.")]
  [GenericArgument(0, "TValue", "Type of object to be encrypted and signed.")]
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
