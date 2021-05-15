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

    public void MyAlgo()
    {
        for (int i = 0; i < colors.Count; i++)
            colors[i] = 0;
        
        SortedList<int, int> degreeIndex = new SortedList<int, int>();
        for (int i = 0; i < V; i++)
            degreeIndex.Add(adj[i].Count, i);

        int t = 10000;
        for (int i = 1; i < t; i++)
        {
            var uncoloredVertices = new List<int>();
            for(int index = 0; index < V; index++)
                if (colors[index] == 0)
                    uncoloredVertices.Add(index);

            int v = -1;
            foreach(var ver in degreeIndex)
            {
                if (uncoloredVertices.Contains(ver.Value))
                {
                    v = ver.Value;
                    break;
                }
            }

            if (v == -1)
                break;

            colors[v] = i;

            Boolean someCaseMet = false;
            
            foreach(int u in adj[v])
            {
                if (colors[u] == colors[v] && colors[u] != 0 && colors[v] != 0)
                {
                    colors[v] = 0;
                    someCaseMet = true;
                    break;
                }
            }
            // if (!someCaseMet)
            // {
            //     foreach(int u in getS(v))
            //     {
            //         if (colors[u] == colors[v] && colors[u] != 0 && colors[v] != 0)
            //         {
            //             colors[v] = 0;
            //             someCaseMet = true;
            //         }
            //     }
            // }
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
        int color = 1;

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
}