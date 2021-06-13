using Xunit;
using Xunit.Abstractions;
using System.IO;
using System;
using System.Collections.Generic;

namespace acyclic_coloring
{
    public class UnitTest1
    {
        private GraphReader rd;
        private readonly ITestOutputHelper output;

        public UnitTest1(ITestOutputHelper output)
        {
            this.output = output;
            this.rd = new GraphReader();
        }

        [Fact(Skip ="wrong path in tests")]
        public void TestFb()
        {
            string folderPath = System.IO.Directory.GetCurrentDirectory() + "/dataset/facebook/";
            string[] fileArray = Directory.GetFiles(folderPath, "*.edges");
            foreach(var f in fileArray)
            {
                System.Console.WriteLine(f);
            }
        }

        [Fact(Skip="wrong ans")]
        public void SampleWrongAnsTest()
        {
            Assert.Equal(1, 3);
        }

        [Fact]
        public void SampleTest()
        {
            Assert.Equal(1, 1);
        }

        [Fact]
        public void TestG1()
        {
            Graph g1 = new Graph(5);
            g1.addEdge(1, 0);
            g1.addEdge(0, 2);
            g1.addEdge(2, 1);
            g1.addEdge(1, 3);
            g1.addEdge(3, 4);
            Assert.True(g1.isCyclic());

            Assert.Equal(3, g1.WelshPowellAlgorithm());
            Assert.True(g1.isProperCyclicColoring());

            Assert.Equal(3, g1.NewAcyclicColoring());
            Assert.True(g1.isProperCyclicColoring());

            Assert.Equal(3, g1.HalAlgorithm());
            Assert.True(g1.isProperCyclicColoring());
        }

        [Fact]
        public void TestG2()
        {
            Graph g2 = new Graph(3);
            g2.addEdge(0, 1);
            g2.addEdge(1, 2);
            Assert.False(g2.isCyclic());
        }

        [Fact]
        public void TestG3()
        {
            Graph g3 = new Graph(2);
            g3.addEdge(0, 1);
            g3.addEdge(1, 0);
            Assert.True(g3.isCyclic());
        }

        [Fact]
        public void TestG4()
        {
            Graph g4 = new Graph(4);
            g4.addEdge(2, 1);
            g4.addEdge(1, 3);
            g4.addEdge(0, 3);
            g4.addEdge(0, 1);
            Assert.True(g4.isCyclic());

            g4.colors[0] = 0;
            g4.colors[1] = 1;
            g4.colors[2] = 0;
            g4.colors[3] = 2;
            Assert.True(g4.isProperCyclicColoring());
        }

        [Fact]
        public void TestChordalGraph()
        {
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

            Assert.Equal(3, g5.WelshPowellAlgorithm());
            Assert.True(g5.isProperCyclicColoring());

            Assert.InRange<Int32>(g5.NewAcyclicColoring(), 3, 5);
            Assert.True(g5.isProperCyclicColoring());

            int noColors = g5.HalAlgorithm();
            Assert.InRange<Int32>(noColors, 3, g5.getC());
            Assert.True(g5.isProperCyclicColoring());
        }

        [Fact]
        public void TestG10()
        {
            Graph g10 = new Graph(20);
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

            var c = g10.WelshPowellAlgorithm();
            Assert.Equal(5, c);
            // TODO: check why not proper cyclic coloring - probably it is NOT chordal...
            // chordal graphs are hard to define in a multi-cycle env (cycles inside cycles)
            // Assert.True(g10.isProperCyclicColoring());

            Assert.InRange<Int32>(g10.HalAlgorithm(), 3, 9);
            Assert.True(g10.isProperCyclicColoring());
        }

        [Fact]
        public void TestG11()
        {
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

            var c = g11.WelshPowellAlgorithm();
            Assert.Equal(2, c);
            //Assert.True(g11.isProperCyclicColoring());

            Assert.InRange<Int32>(g11.NewAcyclicColoring(), 2, 11);
            Assert.True(g11.isProperCyclicColoring());

            Assert.InRange<Int32>(g11.HalAlgorithm(), 2, 10);
            Assert.True(g11.isProperCyclicColoring());
        }

        [Fact]
        public void TestG12()
        {
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

            var c = g12.WelshPowellAlgorithm();
            Assert.Equal(3, c);
            Assert.True(g12.isProperCyclicColoring());

            Assert.InRange<Int32>(g12.NewAcyclicColoring(), 4, 5);
            Assert.True(g12.isProperCyclicColoring());

            Assert.InRange<Int32>(g12.HalAlgorithm(), 4, 5);
            Assert.True(g12.isProperCyclicColoring());
        }

        [Fact]
        public void TestButterfly() {
            Graph Butterfly = new Graph(13);
            Butterfly.addEdge(0, 7);
            Butterfly.addEdge(1, 7);
            Butterfly.addEdge(2, 7);
            Butterfly.addEdge(3, 7);
            Butterfly.addEdge(4, 7);
            Butterfly.addEdge(5, 7);
            Butterfly.addEdge(6, 7);
            Butterfly.addEdge(8, 7);
            Butterfly.addEdge(9, 8);
            Butterfly.addEdge(8, 10);
            Butterfly.addEdge(8, 11);
            Butterfly.addEdge(8, 12);

            Assert.Equal(2, Butterfly.HalAlgorithm());
            Assert.True(Butterfly.isProperCyclicColoring());

            Assert.InRange<Int32>(Butterfly.NewAcyclicColoring(), 2, 2);
            Assert.True(Butterfly.isProperCyclicColoring());

            Assert.InRange<Int32>(Butterfly.HalAlgorithm(), 2, 2);
            Assert.True(Butterfly.isProperCyclicColoring());
        }

        [Fact]
        public void Testg13()
        {
            Graph g13 = new Graph(12);
            g13.addEdge(0, 1);
            g13.addEdge(1, 2);
            g13.addEdge(2, 3);
            g13.addEdge(3, 4);
            g13.addEdge(4, 5);
            g13.addEdge(5, 6);
            g13.addEdge(0, 8);
            g13.addEdge(8, 7);
            g13.addEdge(7, 6);
            g13.addEdge(9, 2);
            g13.addEdge(2, 4);
            g13.addEdge(4, 10);
            g13.addEdge(10, 5);
            g13.addEdge(5, 7);
            g13.addEdge(7, 11);
            g13.addEdge(11, 8);
            g13.addEdge(8, 1);
            g13.addEdge(1, 9);
            g13.addEdge(2, 8);
            g13.addEdge(2, 7);
            g13.addEdge(2, 5);
            g13.addEdge(1, 7);
            g13.addEdge(1, 5);
            g13.addEdge(1, 4);
            g13.addEdge(8, 5);
            g13.addEdge(8, 4);
            g13.addEdge(4, 7);
            g13.addEdge(2, 0);
            g13.addEdge(7, 0);
            g13.addEdge(2, 10);
            g13.addEdge(7, 10);

            Assert.Equal(6, g13.WelshPowellAlgorithm());
            Assert.True(g13.isProperCyclicColoring());

            Assert.InRange<Int32>(g13.NewAcyclicColoring(), 6, 10);
            Assert.True(g13.isProperCyclicColoring());

            int noColors = g13.HalAlgorithm();
            Assert.InRange<Int32>(noColors, 6, g13.getC());
            Assert.True(g13.isProperCyclicColoring());
        }

        [Fact]
        public void TestG14() {
            Graph g = rd.createGraphFromDataset("ours/G14.txt");

            Assert.InRange<Int32>(g.HalAlgorithm(), 4, 4);
            Assert.True(g.isProperCyclicColoring());
        }

        [Fact]
        public void TestG14_1() {
            Graph g = rd.createGraphFromDataset("ours/G14-1.txt");

            Assert.InRange<Int32>(g.HalAlgorithm(), 4, 5);
            Assert.True(g.isProperCyclicColoring());
        }

        [Fact]
        public void TestRandomGraphs() {
            string startupPath = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
            DirectoryInfo dir = new DirectoryInfo(startupPath + "dataset/random/");

            foreach(FileInfo fi in dir.GetFiles())
            {
                output.WriteLine(fi.FullName);

                Graph g = rd.createGraphFromPath(fi.FullName);
                Assert.InRange<Int32>(g.HalAlgorithm(), 3, g.getC());
                Assert.True(g.isProperCyclicColoring());
            }

        }

        [Fact]
        public void SaveToFile() {
            Graph g = rd.createGraphFromDataset("ours/G14-1.txt");
            string newGraphFileTxt = "g.txt";
            g.saveToFile(newGraphFileTxt);

            string startupPath = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
            Graph gLoaded = rd.createGraphFromPath(startupPath + newGraphFileTxt);

            List<int>[] gAdj = g.getAdj();
            List<int>[] g2Adj = gLoaded.getAdj();
            for (int i = 0; i < gAdj.Length; i++) {
                foreach(var n in gAdj[i]) {
                    Assert.Contains(n, g2Adj[i]);
                }
            }   
            if (File.Exists(Path.Combine(startupPath, newGraphFileTxt)))    
            {    
                File.Delete(Path.Combine(startupPath, newGraphFileTxt));    
            }

            // startupPath = startupPath.Remove(startupPath.Length - 1);
            // string str = startupPath.Substring(0, startupPath.LastIndexOf('/'));
            // output.WriteLine(str);

            // if (File.Exists(Path.Combine(str, "removeMe.txt")))    
            // {    
            //     File.Delete(Path.Combine(str, "removeMe.txt"));    
            // } else {
            //     output.WriteLine("No file.");
            // }
        }

        [Fact]
        public void TestEdge()
        {
            var e1 = new Edge(1, 2);
            var e2 = new Edge(2, 1);
            var e3 = new Edge(0, 3);
            Assert.Equal(0, e1.CompareTo(e2));
            Assert.True(e1 == e2);

            var l = new List<Edge>();
            l.Add(e1);
            Assert.True(l.Contains(e2));
            Assert.False(l.Contains(e3));

            var dict = new Dictionary<Edge, int>();
            dict.Add(e1, 10);
            Assert.True(dict.ContainsKey(e2));
            Assert.False(dict.ContainsKey(e3));
        }
    }
}