using Xunit;
using System.IO;
using System;
using System.Collections.Generic;

namespace acyclic_coloring
{
    public class UnitTest1
    {
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