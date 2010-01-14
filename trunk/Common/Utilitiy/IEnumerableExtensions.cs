/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Thomas Bruderer <apophis@apophis.ch>
 *  File created by apophis at 12.09.2009 10:17
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using System;
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
    }
}
