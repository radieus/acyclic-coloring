# Acyclic coloring

## Description

This project implements 3 algorithms responsible for finding acyclic chromatic number of graphs. The acyclic chromatic number of a graph G is the smallest size of vertex partition V_1, V_2, ..., V_n such that each V_i is an independent set and for all i, j that graph G[V_i \ V_j] does not contain a cycle. In graph theory, an acyclic coloring is vertex coloring coloring in which every 2-chromatic subgraph is acyclic.


## Papers and sources
1. [New acyclic and star coloring algorithms with application to computing Hessians](https://cscapes.cs.purdue.edu/coloringpage/abstracts/acyclic-SISC.pdf)
2. [Acyclic Coloring of Graphs of Maximum Degree ∆](https://hal.inria.fr/hal-01184439/document)
3. [Welsh-Powell algorithm](https://iq.opengenus.org/welsh-powell-algorithm/)


## Usage
You can either open project in VS Code (config files attached) and proceed as usual or use 
```
dotnet run
```
in the root directory. You can also do 
```
dotnet test
```
to run tests.

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.
