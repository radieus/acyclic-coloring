using System;
using System.IO;
using System.Collections.Generic;

namespace acyclic_coloring
{
    // made with love 💔
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

            Graph g5 = new Graph(9);
            g5.addEdge(0, 1);
            g5.addEdge(0, 3);
            g5.addEdge(1, 2);
            g5.addEdge(1, 3);
            g5.addEdge(1, 4);
            g5.addEdge(1, 5);
            g5.addEdge(2, 5);
            g5.addEdge(3, 6);
            g5.addEdge(3, 4); // this edge makes it chordal - our favourite ❤️
            g5.addEdge(4, 5);
            g5.addEdge(4, 6);
            g5.addEdge(4, 7);
            g5.addEdge(4, 8);
            g5.addEdge(5, 8);
            g5.addEdge(7, 8);

            // int noColors = g5.HalAlgorithm();
            // System.Console.WriteLine("g5 - noColors" + noColors);
            // System.Console.WriteLine("g5.getC() : " + g5.getC());

            GraphReader rd = new GraphReader();

            // Graph graphFromFile = rd.createGraphFromDataset("facebook_combined.txt");
            // System.Console.WriteLine("graphFromFile getDelta: " + graphFromFile.getDelta());
            // System.Console.WriteLine("graphFromFile getC: " + graphFromFile.getC());
            // System.Console.WriteLine("graphFromFile getV: " + graphFromFile.getV());
            //int fbColors = graphFromFile.HalAlgorithm();
            //int fbColors = graphFromFile.NewAcyclicColoring(showProgress: true);
            //System.Console.WriteLine(fbColors);
            //System.Console.WriteLine(graphFromFile.isProperCyclicColoring());

            // string folderPath = System.IO.Directory.GetCurrentDirectory() + "/dataset/facebook/";
            // string[] fileArray = Directory.GetFiles(folderPath, "*.edges");
            // foreach(var f in fileArray)
            // {
            //     System.Console.WriteLine(f);
            //     Graph g = rd.createGraphFromPath(f);
            //     System.Console.WriteLine("f1 getV: " + g.getV());
            //     int noColors = g.HalAlgorithm(showProgress: false);
            //     System.Console.WriteLine("f1 fbColors: " + noColors);
            //     System.Console.WriteLine(g.isProperCyclicColoring());
            // }

            // Graph g14 = rd.createGraphFromDataset("ours/G14.txt");
            // System.Console.WriteLine("f1 getV: " + g14.getV());
            // int noColors = g14.HalAlgorithm(showProgress: false);
            // System.Console.WriteLine("f1 fbColors: " + noColors);
            // System.Console.WriteLine(g14.isProperCyclicColoring());

            // noColors = g14.NewAcyclicColoring();
            // System.Console.WriteLine(g14.isProperCyclicColoring());

            // noColors = g14.WelshPowellAlgorithm();
            // System.Console.WriteLine(g14.isProperCyclicColoring());
        }
    }
}
