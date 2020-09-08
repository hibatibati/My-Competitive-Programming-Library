using System;
using System.Collections.Generic;


class Dinic
{
    public List<Edge> edges;//(index&1)==0:edge, otherwise:reverse edge
    List<int>[] G;
    int[] mincost, iter;
    public Dinic(int V)
    {
        iter = new int[V];
        mincost = new int[V];
        edges = new List<Edge>();
        G = Create(V, () => new List<int>());
    }
    public void AddEdge(int from, int to, int cap)
    {
        int edgeIndex = edges.Count;
        G[from].Add(edgeIndex);
        edges.Add(new Edge(from, to, cap, edgeIndex));
        G[to].Add(edgeIndex + 1);
        edges.Add(new Edge(to, from, 0, edgeIndex + 1));
    }
    bool ExistAugmentingPath(int s, int t)
    {
        for (int i = 0; i < mincost.Length; i++) mincost[i] = -1;
        var Q = new Queue<int>();
        mincost[s] = 0;
        Q.Enqueue(s);
        while (Q.Any() && mincost[t] == -1)
        {
            var p = Q.Dequeue();
            foreach (var eidx in G[p])
            {
                var e = edges[eidx];
                if (e.cap > 0 && mincost[e.to] == -1)
                {
                    mincost[e.to] = mincost[p] + 1;
                    Q.Enqueue(e.to);
                }
            }
        }
        return mincost[t] != -1;
    }
    int FindBottleneck(int idx, int t, int flow)
    {
        if (idx == t) return flow;
        for (; iter[idx] < G[idx].Count; iter[idx]++)
        {
            var eg = edges[G[idx][iter[idx]]];
            if (eg.cap > 0 && mincost[idx] < mincost[eg.to])
            {
                var d = FindBottleneck(eg.to, t, Min(flow, eg.cap));
                if (d > 0)
                {
                    eg.cap -= d;
                    edges[eg.idx ^ 1].cap += d;
                    return d;
                }
            }
        }
        return 0;
    }
    public Edge GetEdge(int i) => new Edge { fr = edges[i].fr, to = edges[i].to, cap = edges[i].cap + edges[i ^ 1].cap, flow = edges[i ^ 1].cap };
    public void ChangeEdge(int i, int newCap, int newFlow)
    {
        edges[i].cap = newCap - newFlow;
        edges[i ^ 1].cap = newFlow;
    }
    public bool[] MinCut(int s)
    {
        var res = new bool[G.Length];
        var Q = new Queue<int>();
        Q.Enqueue(s);
        res[s] = true;
        while (Q.Any())
        {
            var p = Q.Dequeue();
            foreach (var eidx in G[p])
            {
                var edge = edges[eidx];
                if (!res[edge.to] && edge.cap != 0)
                {
                    res[edge.to] = true;
                    Q.Enqueue(edge.to);
                }
            }
        }
        return res;
    }
    public int Execute(int s, int t, int f = int.MaxValue)
    {
        int flow = 0;
        while (f > 0 && ExistAugmentingPath(s, t))
        {
            Array.Clear(iter, 0, iter.Length);
            int df = 0;
            while ((df = FindBottleneck(s, t, f)) > 0) { flow += df; f -= df; }
        }
        return flow;
    }
    public class Edge
    {
        public int fr, to, idx; public int cap, flow;
        public Edge() { }
        public Edge(int f, int t, int c, int i)
        {
            fr = f; to = t; cap = c; idx = i;
        }
    }
}