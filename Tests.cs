using Xunit;
using System.IO;
using System;

namespace acyclic_coloring
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(1, 3);
        }

        private const string Expected = "Hello World!";
        [Fact]
        public void Test2()
        {
            using (var sw = new StringWriter())
            {
                System.Console.SetOut(sw);
                System.Console.WriteLine("Hello World!!@");

                var result = sw.ToString().Trim();
                Assert.Equal(Expected, result);
            }
        }

        [Fact]
        public void Test3()
        {
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
        }
    }
}