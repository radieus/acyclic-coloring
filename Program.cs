using System;
using System.Collections.Generic;

namespace acyclic_coloring
{

    public class UserServiceDataEqualityComparer : IEqualityComparer<Edge>
    {
        public bool Equals(Edge x, Edge y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(Edge obj)
        {
            return obj.GetHashCode();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Graph g1 = new Graph(5);
            g1.addEdge(1, 0);
            g1.addEdge(0, 2);
            g1.addEdge(2, 1);
            g1.addEdge(1, 3);
            g1.addEdge(3, 4);

            int noColors = g1.HalAlgorithm();
            System.Console.WriteLine(noColors);
        }
    }
}
