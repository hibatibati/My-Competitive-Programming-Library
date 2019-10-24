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
    private List<Pair<int, Number>>[] edges;
    public Dijkstra(int num)
    { this.num = num; edges = Create(num, () => new List<Pair<int, Number>>()); }
    public void AddEdge(int from, int to, Number weight)
        => edges[from].Add(new Pair<int, Number>(to, weight));
    public Number[] Execute(int st = 0)
    {
        var dist = Create(num, () => Number.MaxValue);
        var pq = new PriorityQueue<Pair<int, long>>((a, b) => a.v2.CompareTo(b.v2));
        pq.Enqueue(new Pair<int, Number>(st, 0));
        dist[st] = 0;
        while (pq.Any())
        {
            var p = pq.Dequeue();
            if (p.v2 < dist[p.v1]) continue;
            foreach (var e in edges[p.v1])
                if (chmin(ref dist[e.v1], e.v2 + p.v2))
                    pq.Enqueue(new Pair<int, Number>(e.v1, dist[e.v1]));
        }
        return dist;
    }
}