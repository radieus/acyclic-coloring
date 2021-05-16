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
            g1.PrintIfCyclic();
            System.Console.WriteLine(g1.WelshPowellAlgorithm());
            System.Console.WriteLine("g1 New acyclic coloring: " + g1.NewAcyclicColoring());
            System.Console.WriteLine(g1.isProperCyclicColoring());

            Graph g2 = new Graph(3);
            g2.addEdge(0, 1);
            g2.addEdge(1, 2);
            g2.PrintIfCyclic();

            Graph g3 = new Graph(2);
            g3.addEdge(0, 1);
            g3.addEdge(1, 0);
            g3.PrintIfCyclic();

            Graph g4 = new Graph(4);
            g4.addEdge(2, 1);
            g4.addEdge(1, 3);
            g4.addEdge(0, 3);
            g4.addEdge(0, 1);
            g4.printGraph();
            g4.PrintIfCyclic();

            g4.colors[0] = 0;
            g4.colors[1] = 1;
            g4.colors[2] = 0;
            g4.colors[3] = 2;
            System.Console.WriteLine(g4.isProperCyclicColoring());
          
            System.Console.WriteLine("g1 Welsh coloring: " + g1.WelshPowellAlgorithm());
            System.Console.WriteLine("g1 New acyclic coloring: " + g1.NewAcyclicColoring());

            // https://iq.opengenus.org/welsh-powell-algorithm/
            Graph g5 = new Graph(9);
            g5.addEdge(0, 1);
            g5.addEdge(0, 3);
            g5.addEdge(1, 2);
            g5.addEdge(1, 3);
            g5.addEdge(1, 4);
            g5.addEdge(1, 5);
            g5.addEdge(2, 5);
            g5.addEdge(3, 6);
            g5.addEdge(3, 4); // this edge makes it chordal
            g5.addEdge(4, 5);
            g5.addEdge(4, 6);
            g5.addEdge(4, 7);
            g5.addEdge(4, 8);
            g5.addEdge(5, 8);
            g5.addEdge(7, 8);
          
            System.Console.WriteLine("g5 Welsh coloring: " + g5.WelshPowellAlgorithm());
            System.Console.WriteLine(g5.isProperCyclicColoring());
            System.Console.WriteLine("g5 New acyclic coloring: " + g5.NewAcyclicColoring());
            System.Console.WriteLine(g5.isProperCyclicColoring());

            System.Console.WriteLine("edge testing...");

            var e1 = new Edge(1,2);
            var e2 = new Edge(2,1);
            var e3 = new Edge(0,3);
            System.Console.WriteLine(e1.CompareTo(e2));
            System.Console.WriteLine(e1 == e2);

            var l = new List<Edge>();
            l.Add(e1);
            System.Console.WriteLine(l.Contains(e2));

            var dict = new Dictionary<Edge, int>(new UserServiceDataEqualityComparer());
            dict.Add(e1, 10);
            System.Console.WriteLine(dict.ContainsKey(e2));
            System.Console.WriteLine(dict.ContainsKey(e3));
        }
    }
}
