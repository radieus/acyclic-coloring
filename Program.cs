using System;

namespace acyclic_coloring
{
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
            System.Console.WriteLine("delta:");
            System.Console.WriteLine(g5.getDelta());
            System.Console.WriteLine("========");
            System.Console.WriteLine(g5.WelshPowellAlgorithm());
            System.Console.WriteLine(g5.isProperCyclicColoring());

            Graph g6 = new Graph(10);
            g6.addEdge(0, 1);
            g6.addEdge(0, 2);
            g6.addEdge(0, 3);
            g6.addEdge(0, 4);
            g6.addEdge(5, 1);
            g6.addEdge(6, 1);
            g6.addEdge(6, 2);
            g6.addEdge(6, 3);
            g6.addEdge(7, 3);
            g6.addEdge(7, 4);
            g6.addEdge(8, 4);
            g6.addEdge(9, 2);
            g6.addEdge(9, 3);
            g6.addEdge(9, 4);

            foreach (var elem in g6.getS(0, 3)) 
            {
                System.Console.WriteLine(elem);
            }
        }
    }
}
