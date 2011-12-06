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
using System.Threading;

namespace System
{
  /// <summary>
  /// Callback for report on the progress of some lengty work.
  /// </summary>
  /// <param name="value">Total amount of work done.</param>
  public delegate void ProgressHandler(double value);

  /// <summary>
  /// Reports on the progress of some lengthy work.
  /// </summary>
  public class Progress
  {
    /// <summary>
    /// Total amount of work done.
    /// </summary>
    private double value;

    /// <summary>
    /// Fraction of work diffrent steps represent by thread id.
    /// </summary>
    private Stack<double> fractions;

    /// <summary>
    /// Call this number to report.
    /// </summary>
    private ProgressHandler handler;

    /// <summary>
    /// Value of the last progress set to take
    /// into account when setting again.
    /// </summary>
    private double lastProgressSet;

    /// <summary>
    /// Creates a new progress reporter.
    /// </summary>
    /// <param name="handler">Report through this handler.</param>
    public Progress(ProgressHandler handler)
    {
      this.handler = handler;
      this.fractions = new Stack<double>();
      this.fractions.Push(1d);
      this.value = 0d;
      this.lastProgressSet = 0;
    }

    /// <summary>
    /// Down one step in the hierarchy.
    /// </summary>
    /// <param name="fraction">What fraction of work is done in that step.</param>
    public void Down(double fraction)
    {
      this.fractions.Push(this.fractions.Peek() * fraction);
      this.lastProgressSet = 0;
    }

    /// <summary>
    /// Add some progress.
    /// </summary>
    /// <param name="addValue">Amount of work done in that fraction.</param>
    public void Add(double addValue)
    {
      this.value += this.fractions.Peek() * addValue;

      if (handler != null) handler(this.value);
    }

    /// <summary>
    /// Set progress.
    /// </summary>
    /// <param name="addValue">Total amount of work done in that fraction.</param>
    public void Set(double setValue)
    {
      double newValue = this.fractions.Peek() * setValue;
      this.value += (newValue - this.lastProgressSet);
      this.lastProgressSet = newValue;

      if (handler != null) handler(this.value);
    }

    /// <summary>
    /// Up one step in the hierarchy.
    /// </summary>
    public void Up()
    {
      this.fractions.Pop();
      this.lastProgressSet = 0;
    }
  }
}
