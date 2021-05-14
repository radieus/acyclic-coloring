using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Graph
{
	private int V; // number of vertices
	private List<int> []adj; // Adjacency List Representation
    private List<int> colors;  // i-th elem is an i-th vertex

	public Graph(int v)
	{
		V = v;
		adj = new List<int>[v];
        colors = Enumerable.Repeat(0, v).ToList();
		for(int i = 0; i < v; ++i)
			adj[i] = new List<int>();
	}

	public void addEdge(int v, int w)
	{
		adj[v].Add(w);
		adj[w].Add(v);
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
}