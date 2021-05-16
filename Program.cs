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

            var dict = new Dictionary<Edge, int>();
            dict.Add(e1, 10);
            System.Console.WriteLine(dict.ContainsKey(e2));
            System.Console.WriteLine(dict.ContainsKey(e3));

            Graph g10 = new Graph(20);
            g10.addEdge(0, 1);
            g10.addEdge(0, 1);
            g10.addEdge(3, 2);
            g10.addEdge(1, 2);
            g10.addEdge(4, 3);
            g10.addEdge(0, 4);
            g10.addEdge(2, 4);
            g10.addEdge(1, 4);
            g10.addEdge(3, 1);
            g10.addEdge(0, 2);
            g10.addEdge(0, 7);
            g10.addEdge(2, 7);
            g10.addEdge(5, 2);
            g10.addEdge(5, 4);
            g10.addEdge(6, 4);
            g10.addEdge(6, 1);
            g10.addEdge(9, 1);
            g10.addEdge(9, 3);
            g10.addEdge(3, 8);
            g10.addEdge(8, 0);
            g10.addEdge(7, 8);
            g10.addEdge(9, 8);
            g10.addEdge(6, 9);
            g10.addEdge(5, 7);
            g10.addEdge(5, 6);
            g10.addEdge(10, 6);
            g10.addEdge(10, 5);
            g10.addEdge(5, 14);
            g10.addEdge(14, 7);
            g10.addEdge(7, 13);
            g10.addEdge(13, 8);
            g10.addEdge(8, 12);
            g10.addEdge(12, 9);
            g10.addEdge(9, 11);
            g10.addEdge(11, 6);
            g10.addEdge(14, 15);
            g10.addEdge(10, 15);
            g10.addEdge(16, 11);
            g10.addEdge(10, 16);
            g10.addEdge(17, 11);
            g10.addEdge(12, 17);
            g10.addEdge(18, 12);
            g10.addEdge(18, 13);
            g10.addEdge(13, 19);
            g10.addEdge(19, 14);
            g10.addEdge(18, 8);
            g10.addEdge(19, 7);
            g10.addEdge(15, 5);
            g10.addEdge(6, 16);
            g10.addEdge(9, 17);

            Graph g11 = new Graph(20);
            g11.addEdge(0, 11);
            g11.addEdge(0, 12);
            g11.addEdge(0, 13);
            g11.addEdge(0, 14);
            g11.addEdge(0, 15);
            g11.addEdge(0, 16);
            g11.addEdge(0, 17);
            g11.addEdge(0, 18);
            g11.addEdge(0, 19);
            g11.addEdge(1, 10);
            g11.addEdge(1, 12);
            g11.addEdge(1, 13);
            g11.addEdge(1, 14);
            g11.addEdge(1, 15);
            g11.addEdge(1, 16);
            g11.addEdge(1, 17);
            g11.addEdge(1, 18);
            g11.addEdge(1, 19);
            g11.addEdge(2, 10);
            g11.addEdge(2, 11);
            g11.addEdge(2, 13);
            g11.addEdge(2, 14);
            g11.addEdge(2, 15);
            g11.addEdge(2, 16);
            g11.addEdge(2, 17);
            g11.addEdge(2, 18);
            g11.addEdge(2, 19);
            g11.addEdge(3, 10);
            g11.addEdge(3, 11);
            g11.addEdge(3, 12);
            g11.addEdge(3, 14);
            g11.addEdge(3, 15);
            g11.addEdge(3, 16);
            g11.addEdge(3, 17);
            g11.addEdge(3, 18);
            g11.addEdge(3, 19);
            g11.addEdge(4, 10);
            g11.addEdge(4, 11);
            g11.addEdge(4, 12);
            g11.addEdge(4, 13);
            g11.addEdge(4, 15);
            g11.addEdge(4, 16);
            g11.addEdge(4, 17);
            g11.addEdge(4, 18);
            g11.addEdge(4, 19);
            g11.addEdge(5, 10);
            g11.addEdge(5, 11);
            g11.addEdge(5, 12);
            g11.addEdge(5, 13);
            g11.addEdge(5, 14);
            g11.addEdge(5, 16);
            g11.addEdge(5, 17);
            g11.addEdge(5, 18);
            g11.addEdge(5, 19);
            g11.addEdge(6, 10);
            g11.addEdge(6, 11);
            g11.addEdge(6, 12);
            g11.addEdge(6, 13);
            g11.addEdge(6, 14);
            g11.addEdge(6, 15);
            g11.addEdge(6, 17);
            g11.addEdge(6, 18);
            g11.addEdge(6, 19);
            g11.addEdge(7, 10);
            g11.addEdge(7, 11);
            g11.addEdge(7, 12);
            g11.addEdge(7, 13);
            g11.addEdge(7, 14);
            g11.addEdge(7, 15);
            g11.addEdge(7, 16);
            g11.addEdge(7, 18);
            g11.addEdge(7, 19);
            g11.addEdge(8, 10);
            g11.addEdge(8, 11);
            g11.addEdge(8, 12);
            g11.addEdge(8, 13);
            g11.addEdge(8, 14);
            g11.addEdge(8, 15);
            g11.addEdge(8, 16);
            g11.addEdge(8, 17);
            g11.addEdge(8, 19);
            g11.addEdge(9, 10);
            g11.addEdge(9, 11);
            g11.addEdge(9, 12);
            g11.addEdge(9, 13);
            g11.addEdge(9, 14);
            g11.addEdge(9, 15);
            g11.addEdge(9, 16);
            g11.addEdge(9, 17);
            g11.addEdge(9, 18);

            Graph g12 = new Graph(13);
            g12.addEdge(0, 1);
            g12.addEdge(2, 1);
            g12.addEdge(0, 2);
            g12.addEdge(1, 3);
            g12.addEdge(3, 4);
            g12.addEdge(4, 1);
            g12.addEdge(5, 2);
            g12.addEdge(2, 6);
            g12.addEdge(6, 5);
            g12.addEdge(8, 0);
            g12.addEdge(0, 7);
            g12.addEdge(7, 8);
            g12.addEdge(9, 3);
            g12.addEdge(9, 10);
            g12.addEdge(11, 8);
            g12.addEdge(12, 5);
            g12.addEdge(11, 6);
            g12.addEdge(2, 8);
            g12.addEdge(8, 6);
        }
    }
}
