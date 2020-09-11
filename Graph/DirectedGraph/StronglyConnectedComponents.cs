using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;



public class StronglyConnectedComponents
{
    int size;
    List<Edge> edges;
    private int[][] scc;
    public int Count { get; private set; }
    public int[] Group { get; private set; }
    public int[] GroupAt(int k) => scc[k];
    public StronglyConnectedComponents(int count)
    {
        size = count;
        edges = new List<Edge>();
    }
    public void AddEdge(int from, int to)
    {
        edges.Add(new Edge(from, to));
    }
    /// <summary>
    /// O(V+E)
    /// </summary>
    /// <returns>scc[i]:i番目の強連結成分の頂点</returns>
    public int[][] Execute()
    {
        //e.fromでe.toをソート
        var start = new int[size + 1];
        var toList = new int[edges.Count];
        foreach (var e in edges) start[e.from + 1]++;
        for (int i = 0; i < start.Length - 1; i++) start[i + 1] += start[i];
        var count = start.ToArray();
        foreach (var e in edges) toList[count[e.from]++] = e.to;
        //lowlink
        int nowOrd = 0;
        int[] low = new int[size], ord = new int[size];
        Group = new int[size];
        var stack = new Stack<int>(size);
        for (int i = 0; i < ord.Length; i++) ord[i] = -1;
        Action<int> dfs = null;
        dfs = v =>
        {
            low[v] = ord[v] = nowOrd++;
            stack.Push(v);
            for (int i = start[v]; i < start[v + 1]; i++)
            {
                var to = toList[i];
                if (ord[to] == -1)
                {
                    dfs(to);
                    low[v] = Min(low[v], low[to]);
                }
                else low[v] = Min(low[v], ord[to]);
            }
            if (low[v] == ord[v])
            {
                while (true)
                {
                    var u = stack.Pop();
                    ord[u] = size;
                    Group[u] = Count;
                    if (u == v) break;
                }
                Count++;
            }
        };
        for (int i = 0; i < ord.Length; i++)
        {
            if (ord[i] == -1) dfs(i);
        }
        for (int i = 0; i < Group.Length; i++)
        {
            Group[i] = Count - 1 - Group[i];
            count[i] = 0;
        }
        scc = new int[Count][];
        foreach (var g in Group) count[g]++;
        for (int i = 0; i < scc.Length; i++) scc[i] = new int[count[i]];
        for (int i = 0; i < Group.Length; i++) scc[Group[i]][--count[Group[i]]] = i;
        return scc;
    }
    struct Edge { public int from, to; public Edge(int f, int t) { from = f; to = t; } }
}