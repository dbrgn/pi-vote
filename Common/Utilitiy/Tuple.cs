/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace System
{
  public class Tuple<T0, T1, T2, T3>
  {
    public T0 First { get; private set; }

    public T1 Second { get; private set; }

    public T2 Third { get; private set; }

    public T3 Fourth { get; private set; }

    public Tuple(T0 first, T1 second, T2 third, T3 fourth)
    {
      First = first;
      Second = second;
      Third = third;
      Fourth = fourth;
    }
  }

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
