using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

/// <summary>
/// 依存:UnionFind
/// </summary>
class Kruskal
{
    private int num;
    private List<Pair<int, int, Number>> edges;
    public Kruskal(int num)
    { this.num = num; edges = new List<Pair<int, int, Number>>(); }
    public void AddEdge(int u, int v, Number weight)
        => edges.Add(new Pair<int, int, Number>(u, v, weight));
    public Number Execute()
    {
        edges.Sort((a, b) => a.v3.CompareTo(b.v3));
        Number res = 0;
        var uf = new UnionFind(num);
        foreach (var e in edges)
            if (uf.Union(e.v1, e.v2))
                res += e.v3;
        return res;
    }
}