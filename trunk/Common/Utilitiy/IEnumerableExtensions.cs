/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Linq;
using System.Collections.Generic;

namespace System.Collections.Generic
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<TResult> Parallel<TFirst, TSecond, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, TResult> selector)
        {
            if (first == null || second == null || selector == null)
                yield break;
            
            var enumerator1 = first.GetEnumerator();
            var enumerator2 = second.GetEnumerator();
            
            while ((enumerator1.MoveNext() && enumerator2.MoveNext()))
            {
                yield return selector(enumerator1.Current, enumerator2.Current);
            }
        }
        
        public static IEnumerable<TResult> Parallel<TFirst, TSecond, TThird, TResult>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second,IEnumerable<TThird> third, Func<TFirst, TSecond, TThird, TResult> selector)
        {
            if (first == null || second == null || third == null || selector == null)
                yield break;
            
            var enumerator1 = first.GetEnumerator();
            var enumerator2 = second.GetEnumerator();
            var enumerator3 = third.GetEnumerator();
            
            while ((enumerator1.MoveNext() && enumerator2.MoveNext() && enumerator3.MoveNext()))
            {
                yield return selector(enumerator1.Current, enumerator2.Current, enumerator3.Current);
            }
        }

        public static void Foreach<T>(this IEnumerable<T> list, Action<T> action)
        {
          foreach (T item in list)
          {
            action(item);
          }
        }

        public static T MaxOrDefault<T>(this IEnumerable<T> list, T defaultValue)
        {
          if (list.Count() > 0)
          {
            return list.Max();
          }
          else
          {
            return defaultValue;
          }
        }

        public static T MinOrDefault<T>(this IEnumerable<T> list, T defaultValue)
        {
          if (list.Count() > 0)
          {
            return list.Min();
          }
          else
          {
            return defaultValue;
          }
        }

        public static void Times(this int number, Action action)
        {
          for (int index = 0; index < number; index++)
          {
            action();
          }
        }
    }
}
