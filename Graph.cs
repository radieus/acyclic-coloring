using System;
using System.Collections.Generic;

class Graph
{
	private int V; // number of vertices
	private List<int> []adj; // Adjacency List Representation
    private List<int> colors;  // i-th elem is an i-th vertex

	public Graph(int v)
	{
		V = v;
		adj = new List<int>[v];
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
}