using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Graph
{
	private int V; // number of vertices
    private int Delta; // highest degree of a graph
	private List<int> []adj;  // Adjacency List Representation
    public List<int> colors;  // i-th elem is an i-th vertex
    public List<int> []S;  // list of S(v) for every v

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

    public int getDelta()
    {
        return this.adj.Select((x => x.Count)).Max();
    }
    public List<int> getCommonNeighbours(int u, int v)
    {
        return this.adj[u].Intersect(this.adj[v]).ToList();
    }

    public List<int> getS(int v, int upperBound = 2)
    {   
        int alpha = 1;
        // int upperBound = Convert.ToInt32(alpha * Math.Pow(this.getDelta(), 4/3));
        HashSet<int> neighbours2nd = new HashSet<int>();
        foreach (int n in adj[v])
        {
            foreach (int nn in adj[n])
            {
                neighbours2nd.Add(nn);
            }
        }

        List<int> NN = neighbours2nd.ToList();
        List<KeyValuePair<int, int>> NN_ordered = new List<KeyValuePair<int, int>>();

        // get count of common neighbours for each vertex in N^2(v)
        foreach(int u in NN)
        {
            NN_ordered.Add(new KeyValuePair<int, int>(u, getCommonNeighbours(u,v).Count));
        }

        // sort the list by the value
        NN_ordered.Sort((x, y) => (x.Value.CompareTo(y.Value)));

        // get every vertex up to bound and get keys only
        List<int> S = NN_ordered.Take(upperBound).Select(kvp => kvp.Key).ToList();

        return S;

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
    private List<int> forbiddenColors;
    private List<List<Tuple<int,int>>> trees;
    private Dictionary<Tuple<int,int>, Tuple<int,int>> firstVisitToTree;
    private Tuple<int,int>[] firstNeighbor;

    public void InitializeData()
    {
        vertices = Enumerable.Repeat(0, V).ToList();
        
        forbiddenColors = Enumerable.Repeat(-1, V).ToList();
        
        trees = new List<List<Tuple<int, int>>>();
        for(var i=0; i<V; ++i)
            for(var j=0; j<adj[i].Count; ++j)
                MakeSet(i,adj[i][j]);

        firstVisitToTree = new Dictionary<Tuple<int,int>, Tuple<int,int>>();
        for(var i=0; i>-V; --i)
            firstVisitToTree.Add(new Tuple<int, int>(i,i), new Tuple<int, int>(-1,-1));
        
        firstNeighbor = new Tuple<int, int>[colors.Count];
        for(var i=0; i<colors.Count; ++i)
            firstNeighbor[i] = new Tuple<int, int>(-1,-1);
    }

    public int NewAcyclicColoring()
    {
        InitializeData();
        foreach(var v in vertices)
        {
            foreach(var w in adj[v])
                forbiddenColors[colors[w]] = v;
            
            foreach(var w in adj[v])
                foreach(var x in adj[w])
                    if(forbiddenColors[colors[x]] != v)
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
        var eContr = new Tuple<int,int>(e.Item2,e.Item1);
        if(!firstVisitToTree.ContainsKey(e) && !firstVisitToTree.ContainsKey(eContr))
            firstVisitToTree[e] = new Tuple<int, int>(v,w);
        else
        {
            Tuple<int,int> firstVisit = firstVisitToTree[e];
            if(firstVisit.Item1 != v)
                firstVisitToTree[e] = new Tuple<int, int>(v,w);
            else if(firstVisit.Item2 != w)
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
        Tuple<int,int> t = new Tuple<int, int>(v,w);
        List<Tuple<int,int>> tree = new List<Tuple<int, int>>();
        tree.Add(t);
        trees.Add(tree);
    }

    public Tuple<int,int> Find(int w, int x)
    {
        Tuple<int,int> e = new Tuple<int, int>(w,x);
        List<Tuple<int,int>> treeToReturn = new List<Tuple<int, int>>();
        foreach(var tree in trees)
        {
            foreach(var edge in tree)
            {
                if(edge.Item1 == e.Item1 && edge.Item2 == e.Item2)
                    treeToReturn = tree;
            }
        }
        return treeToReturn[0];
    }

    public void Union(Tuple<int,int> edge1, Tuple<int,int> edge2)
    {
        List<Tuple<int,int>> set1 = trees.Find(t => t.Contains(edge1));
        List<Tuple<int,int>> set2 = trees.Find(t => t.Contains(edge2));
        List<Tuple<int,int>> mergedSet = set1.Concat(set2).ToList();

        trees.Remove(set1);
        trees.Remove(set2);
        trees.Add(mergedSet);
    }
}