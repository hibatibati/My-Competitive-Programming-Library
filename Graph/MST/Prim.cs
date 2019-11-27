using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;
/// <summary>
/// 最小全域木を求める
/// 依存:PriorityQueue
/// </summary>
class Prim
{
    private int num;
    private List<Edge>[] edges;
    public Prim(int num)
    { this.num = num; edges = Create(num, () => new List<Edge>()); }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddEdge(int from, int to, Number weight)
        => edges[from].Add(new Edge(to, weight));
    public Number Execute(int st = 0)
    {
        Number res = 0;
        var pq = new PriorityQueue<Edge>();
        pq.Push(new Edge(st, 0));
        var use = new bool[num];
        while (pq.Any())
        {
            var p = pq.Pop();
            if (use[p.to]) continue;
            use[p.to] = true;
            res += p.cost;
            foreach (var e in edges[p.to])
                if (!use[e.to])
                    pq.Push(e);
        }
        return res;
    }
    private struct Edge : IComparable<Edge>
    {
        public int to;
        public Number cost;
        public Edge(int to, Number cost)
        { this.to = to; this.cost = cost; }
        public int CompareTo(Edge e)
            => cost.CompareTo(e.cost);
    }
}