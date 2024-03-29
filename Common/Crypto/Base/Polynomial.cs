﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emil.GMP;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Integer field polynomial.
  /// </summary>
  [SerializeObject("Integer field polynomial.")]
  public class Polynomial : Serializable
  {
    /// <summary>
    /// Coefficients of the polynom.
    /// </summary>
    [SerializeField(0, "Coefficients of the polynom.")]
    private List<BigInt> coefficients;

    /// <summary>
    /// Creates a new polynomial.
    /// </summary>
    public Polynomial()
    {
      this.coefficients = new List<BigInt>();
    }

    public Polynomial(DeserializeContext context, byte version)
      : base(context, version)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.WriteList(this.coefficients);
    }

    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      this.coefficients = context.ReadBigIntList();
    }

    /// <summary>
    /// Degree of the polynom.
    /// </summary>
    /// <remarks>
    /// Equivalent to security thereshold for voting.
    /// </remarks>
    public int Degree
    {
      get { return this.coefficients.Count - 1; }
    }

    /// <summary>
    /// Returns a coefficient by index.
    /// </summary>
    /// <param name="coefficientIndex">Index of coefficient.</param>
    /// <returns>A coefficient of the polynom.</returns>
    public BigInt GetCoefficient(int coefficientIndex)
    {
      if (coefficientIndex < 0 || coefficientIndex >= this.coefficients.Count)
        throw new ArgumentException("Coeffiction index out of range.");

      return this.coefficients[coefficientIndex];
    }

    /// <summary>
    /// Adds another coefficient thereby increasing the degree by one.
    /// </summary>
    /// <param name="coefficient">Coefficient to add.</param>
    public void AddCoefficient(BigInt coefficient)
    {
      if (coefficient == null)
        throw new ArgumentNullException("coefficient");

      this.coefficients.Add(coefficient);
    }

    /// <summary>
    /// Evaluate the polynom at x.
    /// </summary>
    /// <param name="x">Evaluate the polynom at x.</param>
    /// <returns>Evaluated value of the polynom.</returns>
    public BigInt Evaluate(BigInt x)
    {
      if (x == null)
        throw new ArgumentNullException("x");

      BigInt value = new BigInt(0);

      for (int coefficientIndex = 0; coefficientIndex < this.coefficients.Count; coefficientIndex++)
      {
        value += coefficients[coefficientIndex] * x.Power(coefficientIndex);
      }

      return value;
    }
  }
}
