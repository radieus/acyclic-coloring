using System;
using System.Collections.Generic;
using System.Linq;

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
}