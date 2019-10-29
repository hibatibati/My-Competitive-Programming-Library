using System;
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
    private List<Pair<Number, int>>[] edges;
    public Dijkstra(int num)
    { this.num = num; edges = Create(num, () => new List<Pair<Number, int>>()); }
    public void AddEdge(int from, int to, Number weight)
        => edges[from].Add(new Pair<Number, int>(weight, to));
    public Number[] Execute(int st = 0)
    {
        var dist = Create(num, () => Number.MaxValue);
        var pq = new PriorityQueue<Pair<Number, int>>((a, b) => a.v1.CompareTo(b.v1));
        pq.Enqueue(new Pair<Number, int>(0, st));
        dist[st] = 0;
        while (pq.Any())
        {
            var p = pq.Dequeue();
            if (p.v1 > dist[p.v2]) continue;
            foreach (var e in edges[p.v2])
                if (chmin(ref dist[e.v2], e.v1 + p.v1))
                    pq.Enqueue(new Pair<Number, int>(dist[e.v2], e.v2));
        }
        return dist;
    }
}