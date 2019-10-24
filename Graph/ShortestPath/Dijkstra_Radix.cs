using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;
/// <summary>
/// 依存:RadixHeap
/// </summary>
class Dijkstra_Radix
{
    private int num;
    private List<Pair<int, Number>>[] edges;
    public Dijkstra_Radix(int num)
    { this.num = num; edges = Create(num, () => new List<Pair<int, Number>>()); }
    public void AddEdge(int from, int to, Number weight)
        => edges[from].Add(new Pair<int, Number>(to, weight));
    public Number[] Execute(int st = 0)
    {
        var dist = Create(num, () => Number.MaxValue);
        var pq = new DataStructure.RadixHeap<int>();
        pq.Push(0, st);
        dist[st] = 0;
        while (pq.Any())
        {
            var p = pq.Pop();
            if (p.v1 > dist[p.v2]) continue;
            foreach (var e in edges[p.v2])
                if (chmin(ref dist[e.v1], e.v2 + p.v1))
                    pq.Push(dist[e.v1], e.v1);
        }
        return dist;
    }
}