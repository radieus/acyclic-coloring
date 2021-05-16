using Xunit;
using System.IO;
using System;

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
        }

        [Fact]
        public void TestG2()
        {
            Graph g2 = new Graph(3);
            g2.addEdge(0, 1);
            g2.addEdge(1, 2);
            Assert.True(g2.isCyclic());
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
        }
    }
}