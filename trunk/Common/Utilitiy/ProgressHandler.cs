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

namespace Pirate.PiVote
{
  public delegate void ProgressHandler(double value);

  public class Progress
  {
    private double value;
    private Stack<double> fractions;
    private ProgressHandler handler;

    public Progress(ProgressHandler handler)
    {
      this.handler = handler;
      this.fractions = new Stack<double>();
      this.fractions.Push(1d);
      this.value = 0d;
    }

    public void Down(double fraction)
    {
      this.fractions.Push(this.fractions.Peek() * fraction);
    }

    public void Add(double addValue)
    {
      this.value += this.fractions.Peek() * addValue;

      if (handler != null) handler(this.value);
    }

    public void Up()
    {
      this.fractions.Pop();
    }
  }
}
