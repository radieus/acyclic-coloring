using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Graph
{
	private int V; // number of vertices
	private List<int> []adj; // Adjacency List Representation
    public List<int> colors;  // i-th elem is an i-th vertex

	public Graph(int v)
	{
		V = v;
		adj = new List<int>[v];
        colors = Enumerable.Repeat(0, v).ToList();
		for(int i = 0; i < v; ++i)
			adj[i] = new List<int>();
	}

    public void printGraph()
    {
        for (int i = 0; i < V; i++)
        {
            System.Console.Write(i + ": ");
            foreach(int v in adj[i])
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

    private Graph createGraphFromColors(List<int> desiredColors)
    {
        Graph g = new Graph(this.V);
        List<int> vertices = new List<int>();
        for(int i = 0; i < this.V; i++)
        {
            if (desiredColors.Contains(this.colors[i]))
            {
                vertices.Add(i);
            }
        }

        foreach(int v in vertices)
        {
            foreach(int addV in this.adj[v])
            {
                if (vertices.Contains(addV))
                    g.adj[v].Add(addV);
            }
        }
        return g;
    }

    public Boolean isProperCyclicColoring()
    {
        // check if 2-choosable subgraph is acyclic
        var distinctColors = colors.Distinct();
        int numberOfColors = distinctColors.Count();
        System.Console.WriteLine(numberOfColors);

        var combinations = distinctColors.SelectMany(x => distinctColors, (x, y) => Tuple.Create(x, y))
                       .Where(tuple => tuple.Item1 < tuple.Item2);

        foreach(var combination in combinations)
        {
            Graph g = createGraphFromColors(new List<int>{combination.Item1, combination.Item2});
            System.Console.WriteLine(combination);
            g.printGraph();
            if (g.isCyclic())
            {
                return false;
            }
        }

        // check if proper coloring
        for (int i = 0; i < V; i++)
        {
            foreach(int neighbour in adj[i])
            {
                if (colors[neighbour] == colors[i])
                    return false;
            }
        }
        return true;
    }

	// a recursive function that uses visited[]
	// and parent to detect cycle in subgraph
	// reachable from vertex v
	Boolean isCyclicUtil(int v, Boolean []visited,
						int parent)
	{
		// mark the current node as visited
		visited[v] = true;

		// recur for all the vertices adjacent to this vertex
		foreach(int i in adj[v])
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
		Boolean []visited = new Boolean[V];
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

        foreach (int i in indexes)
        {
            Console.WriteLine(i);
        }

        return indexes;
    }

    public int WelshPowellAlgorithm()
    {
        List<int> orderedVertices = this.GetDescendingIndexesOfDegrees();
        int color = 0;

        while (orderedVertices.Count != 0)
        {
            Thread.Sleep(2000);
            System.Console.Write("ordered vertices: ");
            System.Console.WriteLine("color: " + color);
            foreach(var item in orderedVertices)
            {
                Console.Write(item.ToString()+ " ");
            }

            System.Console.WriteLine();
            colors[orderedVertices[0]] = color;

            List<int> restrictedNeighbours = new List<int>();
            List<int> coloredVertices = new List<int>();
            coloredVertices.Add(orderedVertices[0]);
            restrictedNeighbours.AddRange(adj[orderedVertices[0]]);


            System.Console.WriteLine("restrictedNeighbours: ");
            foreach(var item in restrictedNeighbours)
            {
                Console.Write(item.ToString()+ " ");
            }
            System.Console.WriteLine();

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
            foreach(int v in coloredVertices)
            {
                orderedVertices.Remove(v);
            }

            color++;
        }

        return color - 1;
    }

    // Star Coloring 
    private List<int> vertices;
    private List<List<int>> forbiddenColors;
    private List<List<Edge>> trees;
    private Dictionary<Edge, Tuple<int,int>> firstVisitToTree;
    private Tuple<int,int>[] firstNeighbor;

    public void InitializeData()
    {
        Edge edge1 = new Edge(2,1);
        Edge edge2 = new Edge(1,2);
        if(edge1 == edge2)
            Console.WriteLine("GIT");
        else
            Console.WriteLine("CHUJ");   

        vertices = Enumerable.Repeat(0, V).ToList();

        for(var i=0; i<V; ++i)
            colors[i] = i+1;
        
        forbiddenColors = new List<List<int>>();
        for(var i=0; i<V; ++i)
            forbiddenColors.Add(new List<int>());
        
        trees = new List<List<Edge>>();
        // for(var i=0; i<V; ++i)
        //     for(var j=0; j<adj[i].Count; ++j)
        //         MakeSet(i,adj[i][j]);

        firstVisitToTree = new Dictionary<Edge, Tuple<int,int>>();
        for(var i=0; i>-V; --i)
            firstVisitToTree.Add(new Edge(i,i), new Tuple<int, int>(-1,-1));
    }

    public int NewAcyclicColoring()
    {
        InitializeData();
        foreach(var v in vertices)
        {
            foreach(var w in adj[v])
                forbiddenColors[colors[w]].Add(v);
            
            foreach(var w in adj[v])
                foreach(var x in adj[w])
                    if(!forbiddenColors[colors[x]].Contains(v))
                        PreventCycle(v,w,x);

            for(var i = 0; i < colors.Count; ++i)
                if(i != v)
                    colors[v] = i;

            foreach(var w in adj[v])
                GrowStar(v,w);

            foreach(var w in adj[v])
                foreach(var x in adj[w])
                {
                    if(x == v)
                        break;
                    
                    if(colors[x] == colors[v])
                        MergeTrees(v,w,x);
                }
        }
        return colors.Distinct().Count();
    }

    public void PreventCycle(int v, int w, int x)
    {
        var e = Find(w,x);
        if(!firstVisitToTree.ContainsKey(e))
            firstVisitToTree[e] = new Tuple<int, int>(v,w);
        else
        {
            Tuple<int,int> firstVisit = firstVisitToTree[e];
            if(firstVisit.Item1 != v)
                firstVisitToTree[e] = new Tuple<int, int>(v,w);
            else if(firstVisit.Item2 != w)
                forbiddenColors[colors[x]].Add(v);
                forbiddenColors[colors[x]] = v;
        }
    }

    public void GrowStar(int v, int w)
    {
        MakeSet(v,w);
        Tuple<int,int> pq = firstNeighbor[colors[w]];
        if(pq.Item1 != v)
            firstNeighbor[colors[w]] = new Tuple<int, int>(v,w);
        else
        {
            Edge e1 = Find(v,w);
            Edge e2 = Find(pq.Item1,pq.Item2);
            Tuple<int,int> e1 = Find(v,w);
            Tuple<int,int> e2 = Find(pq.Item1,pq.Item2);
            Union(e1,e2);
        }
    }

    public void MergeTrees(int v, int w, int x)
    {
        var e1 = Find(v,w);
        var e2 = Find(w,x);

        if(e1 != e2)
            Union(e1,e2);
    }

    public void MakeSet(int v, int w)
    {
        Edge t = new Edge(v,w);
        List<Edge> tree = new List<Edge>();
        tree.Add(t);
        trees.Add(tree);
    }

    public Edge Find(int w, int x)
    {
        Edge e = new Edge(w,x);
        List<Edge> treeToReturn = new List<Edge>();
        foreach(var tree in trees)
        {
            foreach(var edge in tree)
            {
                if(edge == e)
                    treeToReturn = tree;
            }
        }
        if(treeToReturn.Count != 0)
            return treeToReturn[0];
        else
        {
            MakeSet(w,x);
            return new Edge(w,x);
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
}

public class Edge
{
    public int a;
    public int b;

    public Edge(int a, int b)
    {
        this.a = a;
        this.b = b;
    }
    public static bool operator == (Edge e1, Edge e2)
    {
        if(e1.a == e2.a && e1.b == e2.b)
            return true;
        if(e1.a == e2.b && e1.b == e2.a)
            return true;    
        else
            return false;
    }

    public static bool operator != (Edge e1, Edge e2)
    {
        if(e1 == e2)
            return false;
        else
            return true;
    }

    public override bool Equals(object obj)
    {
        var e = obj as Edge;
        if(this == e)
            return true;
        else
            return false;
    }

    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            int hash = 17;
            // Suitable nullity checks etc, of course :)
            hash = hash * 23 + a.GetHashCode();
            hash = hash * 23 + b.GetHashCode();
            return hash;
        }
    }
}