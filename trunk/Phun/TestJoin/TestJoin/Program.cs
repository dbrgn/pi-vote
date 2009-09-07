using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestJoin
{
  class Program
  {
    public class A
    {
      public int Id { get; set; }
      public string Name { get; set; }

      public A(int id, string name)
      {
        Id = id;
        Name = name;
      }
    }

    public class B
    {
      public int Id { get; set; }
      public string Name { get; set; }

      public B(int id, string name)
      {
        Id = id;
        Name = name;
      }
    }

    static void Main(string[] args)
    {
      List<A> a = new List<A>();
      a.Add(new A(0, "Alpha"));
      a.Add(new A(1, "Bravo"));
      a.Add(new A(2, "Charlie"));
      a.Add(new A(3, "Delta"));

      List<B> b = new List<B>();
      b.Add(new B(2, "Red"));
      b.Add(new B(3, "Blue"));
      b.Add(new B(4, "Green"));
      b.Add(new B(5, "Black"));

      foreach (string s in a.Join(b, x => x.Id, y => y.Id, (x, y) => string.Format("{0} {1}", x.Name, y.Name)))
      {
        Console.WriteLine(s);
      }

      Console.ReadLine();
    }
  }
}
