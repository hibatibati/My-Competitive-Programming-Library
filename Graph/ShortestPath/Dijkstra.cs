﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

/// <summary>
/// 依存:PriorityQueue
/// </summary>
class Dijkstra
{
    private int num;
    private List<Edge>[] edges;
    public Dijkstra(int num)
    { this.num = num; edges = Create(num, () => new List<Edge>()); }
    public void AddEdge(int from, int to, long weight)
        => edges[from].Add(new Edge(to, weight));
    public long[] Execute(int st = 0)
    {
        var dist = Create(num, () => long.MaxValue);
        var pq = new PriorityQueue<Edge>();
        pq.Push(new Edge(st, 0));
        dist[st] = 0;
        while (pq.Any())
        {
            var p = pq.Pop();
            if (p.cost > dist[p.to]) continue;
            foreach (var e in edges[p.to])
                if (chmin(ref dist[e.to], e.cost + p.cost))
                    pq.Push(new Edge(e.to, dist[e.to]));
        }
        return dist;
    }

    struct Edge : IComparable<Edge>
    {
        public int to;
        public long cost;
        public Edge(int to, long cost)
        { this.to = to; this.cost = cost; }
        public int CompareTo(Edge e)
            => cost.CompareTo(e.cost);
    }
}