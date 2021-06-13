using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;

namespace acyclic_coloring
{

    public class RandomGraphGenerator
    {
        public Graph generate(int v, int e) {
            Graph g = new Graph(v);
            Random r = new Random();

            for (int iter = 0; iter < e; iter++) {
                int a = r.Next(v);
                int b = r.Next(v);
                while (a == b) {
                    a = r.Next(v);
                    b = r.Next(v);
                }
                g.addEdge(a, b);
            }

            return g;
        }
    }
    public class GraphReader
    {
        public Graph createGraphFromPath(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Cannot find the file.");
            }
            string line = "";
            SortedSet<int> vertices = new SortedSet<int>();
            List<Tuple<int, int>> edges = new List<Tuple<int, int>>();
            using (StreamReader sr = new StreamReader(path))
            {
                while ((line = sr.ReadLine()) != null)
                {
                    string[] numbers = line.Split(' ');
                    int a = int.Parse(numbers[0]);
                    int b = int.Parse(numbers[1]);
                    edges.Add(new Tuple<int, int>(a, b));
                    vertices.Add(a);
                    vertices.Add(b);
                }
            }

            Dictionary<int, int> mapFromZero = new Dictionary<int, int>();
            int i = 0;
            foreach(var v in vertices)
            {
                mapFromZero.Add(v, i);
                i++;
                //System.Console.WriteLine(i + " " + v);
            }         

            Graph g = new Graph(vertices.Count);
            foreach(Tuple<int, int> edge in edges)
            {
                g.addEdge(mapFromZero[edge.Item1], mapFromZero[edge.Item2]);
                //g.addEdge(edge.Item1, edge.Item2);
            }

            return g;
        }
        public Graph createGraphFromDataset(string fileFromDataset)
        {
            // find path to project
            string startupPath = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
            string path = startupPath + "/dataset/" + fileFromDataset;
            return createGraphFromPath(path);
        }
    }
    public class Graph
    {
        private int V; // number of vertices

        public int getV()
        {
            return this.V;
        }
        private List<int>[] adj; // Adjacency List Representation

        public List<int>[] getAdj() {
            return adj;
        }
        public List<int> colors;  // i-th elem is an i-th vertex

        public Graph(int v)
        {
            V = v;
            adj = new List<int>[v];
            colors = Enumerable.Repeat(0, v).ToList();
            for (int i = 0; i < v; ++i)
                adj[i] = new List<int>();
        }

        public void printColoring() {
            for (int i = 0; i < V; i++) {
                System.Console.WriteLine(i + " : " + colors[i]);
            }
        }

        public void printGraph()
        {
            for (int i = 0; i < V; i++)
            {
                System.Console.Write(i + ": ");
                foreach (int v in adj[i])
                {
                    System.Console.Write(v + ", ");
                }
                System.Console.WriteLine();
            }
        }

        public void addEdge(int v, int w)
        {
            adj[v].Add(w);
            adj[w].Add(v);
        }

        public void saveToFile(string fileName) {
            // Write file using StreamWriter  
            string startupPath = AppContext.BaseDirectory.Substring(0, AppContext.BaseDirectory.IndexOf("bin"));
            System.Console.WriteLine(startupPath + fileName);
            using (StreamWriter writer = new StreamWriter(startupPath + fileName))  
            {  
                for (int i = 0; i < adj.Count(); i++) {
                    foreach(var n in adj[i]) {
                        if (n >= i)
                            writer.WriteLine(i + " " + n);
                    }
                }
            }  
        }

        private Graph createGraphFromColors(List<int> desiredColors)
        {
            // Graph g = new Graph(this.V);
            // List<int> vertices = new List<int>();
            // for (int i = 0; i < this.V; i++)
            // {
            //     if (desiredColors.Contains(this.colors[i]))
            //     {
            //         vertices.Add(i);
            //         g.colors[i] = colors[i];
            //     }
            // }

            // foreach (int v in vertices)
            // {
            //     foreach (int addV in this.adj[v])
            //     {
            //         if (vertices.Contains(addV))
            //             g.adj[v].Add(addV);
            //     }
            // }
            SortedSet<int> vertices = new SortedSet<int>();
            HashSet<Edge> edges = new HashSet<Edge>();

            foreach(var vv in desiredColors) {
                vertices.Add(vv);
            }
            for (int v = 0; v < this.adj.Count(); v++)
            {
                foreach(var n in adj[v]) {
                    if (desiredColors.Contains(v) && desiredColors.Contains(n)) {
                        edges.Add(new Edge(v, n));
                    }
                }
            }

            Dictionary<int, int> mapFromZero = new Dictionary<int, int>();
            int i = 0;
            foreach(var v in vertices)
            {
                mapFromZero.Add(v, i);
                i++;
            }         

            Graph g = new Graph(vertices.Count);
            foreach(Edge edge in edges)
            {
                g.addEdge(mapFromZero[edge.a], mapFromZero[edge.b]);
            }
            return g;
        }

        public Boolean isProperCyclicColoring(Boolean printDebug=false)
        {
            // check if proper coloring
            for (int i = 0; i < V; i++)
            {
                foreach (int neighbour in adj[i])
                {
                    if (colors[neighbour] == colors[i])
                        return false;
                }
            }

            if (printDebug) {
                System.Console.WriteLine("Proper coloring");
            }

            // check if 2-choosable subgraph is acyclic
            var distinctColors = colors.Distinct();
            int numberOfColors = distinctColors.Count();
            //System.Console.WriteLine("numberOfColors: " + numberOfColors);

            var combinations = distinctColors.SelectMany(x => distinctColors, (x, y) => Tuple.Create(x, y))
                        .Where(tuple => tuple.Item1 < tuple.Item2);

            foreach (Tuple<int, int> combination in combinations)
            {
                Graph g = createGraphFromColors(new List<int> { combination.Item1, combination.Item2 });
                //System.Console.WriteLine(combination);
                //g.printGraph();
                if (g.isCyclic())
                {
                    if (printDebug) {
                        System.Console.WriteLine("Not acyclic here - Graph induced by 2 below colors:");
                        System.Console.WriteLine(combination.Item1 + " " + combination.Item2);
                    }
                    return false;
                }
            }

            return true;
        }

        // a recursive function that uses visited[]
        // and parent to detect cycle in subgraph
        // reachable from vertex v
        Boolean isCyclicUtil(int v, Boolean[] visited,
                            int parent)
        {
            // mark the current node as visited
            visited[v] = true;

            // recur for all the vertices adjacent to this vertex
            foreach (int i in adj[v])
            {
                // if an adjacent is not visited, then recur for that adjacent
                if (!visited[i])
                {
                    if (isCyclicUtil(i, visited, v))
                        return true;
                }

                // if an adjacent is visited and not parent of current vertex, then there is a cycle
                else if (i != parent)
                    return true;
            }
            return false;
        }

        public Boolean isCyclic()
        {
            // mark all the vertices as not visited and not part of recursion stack
            Boolean[] visited = new Boolean[V];
            for (int i = 0; i < V; i++)
                visited[i] = false;

            // call the recursive helper function to detect cycle in different DFS trees
            for (int u = 0; u < V; u++)

                // don't recur for u if already visited
                if (!visited[u])
                    if (isCyclicUtil(u, visited, -1))
                        return true;

            return false;
        }

        public void PrintIfCyclic()
        {
            if (this.isCyclic())
                Console.WriteLine("Graph contains cycle");
            else
                Console.WriteLine("Graph doesn't contains cycle");
        }

        // returns list of indexes sorted by degrees of V in descending order
        public List<int> GetDescendingIndexesOfDegrees()
        {
            var degreeToIndex = this.adj.Select((x, i) => new KeyValuePair<int, int>(x.Count, i))
                .OrderByDescending(x => x.Key)
                .ToList();

            // degree value corresponding to the index in original adjacency list
            List<int> degrees = degreeToIndex.Select(x => x.Key).ToList();
            List<int> indexes = degreeToIndex.Select(x => x.Value).ToList();

            // foreach (int i in indexes)
            // {
            //     Console.WriteLine(i);
            // }

            return indexes;
        }

        public int WelshPowellAlgorithm()
        {
            List<int> orderedVertices = this.GetDescendingIndexesOfDegrees();
            int color = 0;

            while (orderedVertices.Count != 0)
            {
                // Thread.Sleep(2000);
                // System.Console.Write("ordered vertices: ");
                // System.Console.WriteLine("color: " + color);
                // foreach(var item in orderedVertices)
                // {
                //     Console.Write(item.ToString()+ " ");
                // }

                // System.Console.WriteLine();
                colors[orderedVertices[0]] = color;

                List<int> restrictedNeighbours = new List<int>();
                List<int> coloredVertices = new List<int>();
                coloredVertices.Add(orderedVertices[0]);
                restrictedNeighbours.AddRange(adj[orderedVertices[0]]);

                // System.Console.WriteLine("restrictedNeighbours: ");
                // foreach(var item in restrictedNeighbours)
                // {
                //     Console.Write(item.ToString()+ " ");
                // }
                // System.Console.WriteLine();

                for (int i = 1; i < orderedVertices.Count; i++)
                {
                    int currentVertex = orderedVertices[i];
                    if (!restrictedNeighbours.Contains(currentVertex))
                    {
                        colors[currentVertex] = color;
                        restrictedNeighbours.AddRange(adj[currentVertex]);
                        coloredVertices.Add(currentVertex);
                    }
                }
                foreach (int v in coloredVertices)
                {
                    orderedVertices.Remove(v);
                }

                color++;
            }

            return colors.Distinct().Count();
        }

        // Star Coloring 
        private List<int> vertices;
        private List<List<int>> forbiddenColors;
        private List<List<Edge>> trees;
        private Dictionary<Edge, Tuple<int, int>> firstVisitToTree;
        private Edge[] firstNeighbor;

        public void InitializeData()
        {
            vertices = Enumerable.Repeat(0, V).ToList();

            for (var i = 0; i < V; i++)
                colors[i] = i;
            //colors[i] = 0;

            forbiddenColors = new List<List<int>>();
            for (var i = 0; i < V; ++i)
                forbiddenColors.Add(new List<int>());

            trees = new List<List<Edge>>();
            // for(var i=0; i<V; ++i)
            //     for(var j=0; j<adj[i].Count; ++j)
            //         MakeSet(i,adj[i][j]);

            firstVisitToTree = new Dictionary<Edge, Tuple<int, int>>();

            firstNeighbor = new Edge[colors.Count];
            for (var i = 0; i < colors.Count; i++)
                firstNeighbor[i] = new Edge(-1, -1);
        }

        public int NewAcyclicColoring(Boolean showProgress=false)
        {
            InitializeData();
            for (int v = 0; v < this.V; v++)
            {
                foreach (var w in adj[v])
                    forbiddenColors[colors[w]].Add(v);

                foreach (var w in adj[v])
                    foreach (var x in adj[w])
                        if (!forbiddenColors[colors[x]].Contains(v))
                            PreventCycle(v, w, x);

                for (var i = 0; i < colors.Count; ++i)
                    if (!forbiddenColors[i].Contains(v))
                    {
                        colors[v] = i;
                        break;
                    }

                foreach (var w in adj[v])
                    GrowStar(v, w);

                foreach (var w in adj[v])
                    foreach (var x in adj[w])
                    {
                        if (x == v)
                            break;

                        if (colors[x] == colors[v])
                            MergeTrees(v, w, x);
                    }

                if (showProgress)
                {
                    System.Console.WriteLine("i: " + v);
                    // foreach (var c in colors)
                    //     System.Console.Write(c + " ");
                    // System.Console.WriteLine();
                }
            }
            // foreach(var c in colors)
            //     System.Console.Write(c + " ");
            // System.Console.WriteLine();

            return colors.Distinct().Count();
        }

        public void PreventCycle(int v, int w, int x)
        {
            var e = Find(w, x);
            if (!firstVisitToTree.ContainsKey(e))
                firstVisitToTree[e] = new Tuple<int, int>(v, w);
            else
            {
                Tuple<int, int> firstVisit = firstVisitToTree[e];
                if (firstVisit.Item1 != v)
                    firstVisitToTree[e] = new Tuple<int, int>(v, w);
                else if (firstVisit.Item2 != w)
                    forbiddenColors[colors[x]].Add(v);
            }
        }

        public void GrowStar(int v, int w)
        {
            MakeSet(v, w);
            Edge pq = firstNeighbor[colors[w]];
            if (pq.a != v)
                firstNeighbor[colors[w]] = new Edge(v, w);
            else
            {
                Edge e1 = Find(v, w);
                Edge e2 = Find(pq.a, pq.b);
                Union(e1, e2);
            }
        }

        public void MergeTrees(int v, int w, int x)
        {
            var e1 = Find(v, w);
            var e2 = Find(w, x);

            if (e1 != e2)
                Union(e1, e2);
        }

        public void MakeSet(int v, int w)
        {
            Edge t = new Edge(v, w);
            List<Edge> tree = new List<Edge>();
            tree.Add(t);
            trees.Add(tree);
        }

        public Edge Find(int w, int x)
        {
            Edge e = new Edge(w, x);
            List<Edge> treeToReturn = new List<Edge>();
            foreach (var tree in trees)
            {
                foreach (var edge in tree)
                {
                    if (edge == e)
                        treeToReturn = tree;
                }
            }
            if (treeToReturn.Count != 0)
                return treeToReturn[0];
            else
            {
                MakeSet(w, x);
                return new Edge(w, x);
            }
        }

        public void Union(Edge edge1, Edge edge2)
        {
            List<Edge> set1 = trees.Find(t => t.Contains(edge1));
            List<Edge> set2 = trees.Find(t => t.Contains(edge2));
            List<Edge> mergedSet = set1.Concat(set2).ToList();

            trees.Remove(set1);
            trees.Remove(set2);
            trees.Add(mergedSet);

        }

        // public bool isVertexNS(int u, int v)
        // {
        //     if (adj[u])
        // }

        public Dictionary<int, HashSet<int>> getColorToEdges(int u)
        {
            var colorToEdges = new Dictionary<int, HashSet<int>>();  // key-color, value-edges

            foreach(int n in adj[u])
            {
                int ColorOfNeighbor = colors[n];
                if (ColorOfNeighbor == 0)  // uncolored vertex yet
                {
                    continue;
                }

                if (!colorToEdges.ContainsKey(ColorOfNeighbor))
                {
                    colorToEdges[ColorOfNeighbor] = new HashSet<int>();
                }
                colorToEdges[ColorOfNeighbor].Add(n);
            }

            return colorToEdges;
        }

        public int getDelta()
        {
            return this.adj.Select((x => x.Count)).Max();
        }

        public int getC() 
        {
            // C is the upper bound for the acyclic chromatic number of the "3rd algo" (change name)
            int delta = getDelta();
            if (delta % 2 == 0)
            {
                return ((delta * (delta-1)) / 2) + 1;
            } else {
                return (delta * (delta-1)) / 2;
            }
        }
        public List<int> isPropertyPiSatisfied(int u)
        {
            // returns empty list if P_i(u) is satisfied
            // if not it returns list of neighbours of u to recolor
            var l = new List<int>();
            Dictionary<int, HashSet<int>> colorToEdges = getColorToEdges(u);

            //System.Console.WriteLine("colorToEdges.Count" + colorToEdges.Count);

            foreach(KeyValuePair<int, HashSet<int>> colorList in colorToEdges)
            {
                int color = colorList.Key;
                if (colorToEdges[color].Count <= 1)  // color appears only once
                {
                    continue;
                }
                foreach(int n in colorToEdges[color]) // n is colored Neighbor (i colors of neighbors)
                {
                    if (adj[n].Count == 1)
                    {
                        continue;
                    }
                    Dictionary<int, HashSet<int>> colorToEdgesOfN = getColorToEdges(n);
                    Boolean neighborToAdd = true;
                    foreach(KeyValuePair<int, HashSet<int>> entry in colorToEdgesOfN)  // iterate through NN's
                    {
                        if (entry.Value.Count > 1)  // our statement is not satisfied
                        {
                            neighborToAdd = false;
                            break;
                        }
                    }

                    if (neighborToAdd)
                    {
                        l.Add(n);
                    }
                }
            }

            return l;
        }

        public int HalAlgorithm(Boolean showProgress=false)
        {
            for(int i = 0; i < colors.Count; i++)
            {
                colors[i] = 0;
            }

            // algo loop
            for(int i = 0; i < this.V; i++)  // i - vertex
            {
                if (showProgress)
                {
                    System.Console.WriteLine("i: " + i);
                }
                if (colors[i] != 0) // already colored, wtf
                {
                    continue;
                }
                List<int> verticesToRecolor = isPropertyPiSatisfied(i);
                Dictionary<int, HashSet<int>> colorToEdges = getColorToEdges(i);
                if (verticesToRecolor.Count > 0)
                {
                    // TODO: check if proper logic behind it?
                    // restricting to recolor n so that not to create a new pair of NS
                    HashSet<int> restrictedColorsFromNeighbors = new HashSet<int>();
                    foreach(int n in adj[i])
                    {
                        if (colors[n] != 0)
                            restrictedColorsFromNeighbors.Add(colors[n]);
                    }
                    
                    // recolor each vertex s.t. our condition stil fails
                    foreach(int n in verticesToRecolor)
                    {
                        if (colorToEdges[colors[n]].Count == 1)  // do not recolor NS as it has been changed to singular neighbor
                        {
                            continue;
                        }
                        // search for restricted colors
                        HashSet<int> restricedColors = new HashSet<int>();
                        restricedColors.UnionWith(restrictedColorsFromNeighbors);
                        
                        foreach(int nn in adj[n])
                        {
                            if (colors[nn] != 0)
                            {
                                restricedColors.Add(colors[nn]);
                            }
                        }
                        // recolor
                        int oldColor = 0;
                        for (int cc = 1; cc < int.MaxValue; cc++)
                        {
                            if (!restricedColors.Contains(cc))
                            {
                                oldColor = colors[n];
                                colors[n] = cc;
                                restricedColors.Add(cc); // correct?
                                // restrictedColorsFromNeighbors.Add(cc);  // ???
                                break;
                            }
                        }

                        // remove newly recolored vertex
                        if (colorToEdges[oldColor].Contains(n)) 
                        {
                            colorToEdges[oldColor].Remove(n);
                        } else {
                            throw new Exception("Removal exception");
                        }
                    }
                    //verticesToRecolor = isPropertyPiSatisfied(i);
                }

                var existingColors = new HashSet<int>();
                foreach(int col in colors)
                {
                    if (col != 0)
                    {
                        existingColors.Add(col);
                    }
                }
                Boolean isColored = false;
                int c = 0;
                // color i-th vertex... (u)
                while(!isColored)
                {   
                    Boolean neighbourHasSameColor = false;
                    c++;  // start with color 1
                    foreach(int n in adj[i]) // check if proper coloring
                    {
                        if (colors[n] == c) 
                        {
                            neighbourHasSameColor = true;
                            break;
                        }
                    }
                    if (neighbourHasSameColor)
                        continue;

                    // check if proper acyclic coloring
                    // Foreach subgraph H induced by color c and every other color in G
                    // H does not contain any cycle
                    Boolean areAllSubgraphsAcyclic = true;
                    foreach(int existingColor in existingColors)
                    {
                        if (existingColor == c) // optimalisation - no need to induce graph from one color
                            continue;

                        Graph g = createGraphFromColors(new List<int> { c, existingColor });
                        if (!g.isProperCyclicColoring())
                        {
                            areAllSubgraphsAcyclic = false;
                            break;
                        }
                    }
                    if (areAllSubgraphsAcyclic)
                    {
                        colors[i] = c;
                        isColored = true;
                    }
                }
            }
            return colors.Distinct().Count();;
        }
    }
}
