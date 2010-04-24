﻿/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

namespace System
{
  public class Tuple<T0, T1, T2>
  {
    public T0 First { get; private set; }

    public T1 Second { get; private set; }

    public T2 Third { get; private set; }

    public Tuple(T0 first, T1 second, T2 third)
    {
      First = first;
      Second = second;
      Third = third;
    }
  }

  public class Tuple<T0, T1>
  {
    public T0 First { get; private set; }

    public T1 Second { get; private set; }

    public Tuple(T0 first, T1 second)
    {
      First = first;
      Second = second;
    }
  }
}