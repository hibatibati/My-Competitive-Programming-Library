﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

#region Kruskal
/// <summary>
/// 依存:UnionFind
/// </summary>
class Kruskal
{
    private int num;
    private List<Edge> edges;
    public Kruskal(int num)
    { this.num = num; edges = new List<Edge>(); }
    public void AddEdge(int u, int v, long weight)
        => edges.Add(new Edge(u, v, weight));
    public long Execute()
    {
        edges.Sort();
        long res = 0;
        var uf = new UnionFind(num);
        foreach (var e in edges)
            if (uf.Union(e.from, e.to))
                res += e.cost;
        return res;
    }

    struct Edge : IComparable<Edge>
    {
        public int from;
        public int to;
        public long cost;
        public Edge(int from, int to, long cost)
        {
            this.from = from; this.to = to; this.cost = cost;
        }
        public int CompareTo(Edge e)
            => cost.CompareTo(e.cost);
    }
}
#endregion