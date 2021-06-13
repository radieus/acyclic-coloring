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
            string startupPath = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
            GraphReader rd = new GraphReader();

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

            ///////////////////////////
            //// generating random Graph
            // var gen = new RandomGraphGenerator();
            // Graph g = gen.generate(15, 20);
            // g.saveToFile("g.txt");
            // Graph g2 = rd.createGraphFromPath(startupPath + "g.txt");
            // g2.saveToFile("g.txt");
            ///////////////////////////

            // Graph graphFromFile = rd.createGraphFromDataset("facebook_combined.txt");
            // System.Console.WriteLine("graphFromFile getDelta: " + graphFromFile.getDelta());
            // System.Console.WriteLine("graphFromFile getC: " + graphFromFile.getC());
            // System.Console.WriteLine("graphFromFile getV: " + graphFromFile.getV());
            //int fbColors = graphFromFile.HalAlgorithm();
            //int fbColors = graphFromFile.NewAcyclicColoring(showProgress: true);
            //System.Console.WriteLine(fbColors);
            //System.Console.WriteLine(graphFromFile.isProperCyclicColoring());

            // // run for all Graphs from facebook dataset
            // string folderPath = System.IO.Directory.GetCurrentDirectory() + "/dataset/facebook/";
            // string[] fileArray = Directory.GetFiles(folderPath, "*.edges");
            // foreach(var f in fileArray)
            // {
            //     System.Console.WriteLine(f);
            //     if (f.Contains("1912.edges")) {
            //         System.Console.WriteLine("SKIP");
            //         continue;
            //     }
            //     Graph g = rd.createGraphFromPath(f);
            //     System.Console.WriteLine("f1 getV: " + g.getV());
            //     int noColors = g.HalAlgorithm(showProgress: false);
            //     System.Console.WriteLine("f1 fbColors: " + noColors);
            //     System.Console.WriteLine(g.isProperCyclicColoring());
            // }

            // DirectoryInfo dir = new DirectoryInfo(startupPath + "dataset/random/");

            // foreach(FileInfo fi in dir.GetFiles())
            // {
            //     System.Console.WriteLine(fi.FullName);  // which file
            //     Graph g = rd.createGraphFromPath(fi.FullName);
            //     //g.saveToFile("g2.txt");
            //     g.HalAlgorithm();
            //     g.printColoring();
            //     System.Console.WriteLine(g.isProperCyclicColoring(printDebug: true));
            // }
        }
    }
}
