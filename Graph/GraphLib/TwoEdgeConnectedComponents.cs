using System;
using System.Collections.Generic;

class TwoEdgeConnectedComponents
{
    private int N, M;
    public int GroupCount { get; private set; }
    int _idx;
    List<E>[] g;
    E[] edge;
    int[] group, imos, dep;
    bool[] bridge, use, tree;
    List<List<int>> elements;
    public int this[int i] { get { return group[i]; } }
    public TwoEdgeConnectedComponents(int N, int M)
    {
        this.N = N; this.M = M;
        g = Create(N, () => new List<E>());
        edge = new E[M];
    }
    public void AddEdge(int a, int b)
    {
        g[a].Add(new E(-1, b, _idx));
        g[b].Add(new E(-1, a, _idx));
        edge[_idx] = new E(a, b, _idx);
        _idx++;
    }
    public List<List<int>> Execute()
    {
        group = new int[N];
        imos = new int[N];
        dep = new int[N];
        bridge = new bool[M];
        tree = new bool[M];
        use = new bool[N];
        elements = new List<List<int>>();
        for (int j = 0; j < N; j++)
        {
            if (use[j]) continue;
            MakeDfsTree(j);
        }
        for (var i = 0; i < M; i++)
            if (!tree[i])
            {
                if (dep[edge[i].fr] > dep[edge[i].to])
                    swap(ref edge[i].fr, ref edge[i].to);
                imos[edge[i].fr]--;
                imos[edge[i].to]++;
            }

        use = new bool[N];
        for (int j = 0; j < N; j++)
        {
            if (use[j]) continue;
            FindBridge(j, -1);
        }
        use = new bool[N];
        for (int i = 0; i < N; i++)
        {
            if (use[i]) continue;
            elements.Add(new List<int>());
            dfs(i);
            GroupCount++;
        }
        return elements;
    }
    void MakeDfsTree(int i)
    {
        use[i] = true;
        foreach (E e in g[i])
            if (!use[e.to])
            {
                tree[e.idx] = true; dep[e.to] = dep[i] + 1;
                MakeDfsTree(e.to);
            }
    }
    int FindBridge(int i, int p)
    {
        use[i] = true;
        int now = imos[i];
        foreach (E e in g[i]) if (tree[e.idx] && e.to != p)
            {
                int r = FindBridge(e.to, i);
                if (r == 0) bridge[e.idx] = true;
                now += r;
            }
        return now;
    }
    void dfs(int i)
    {
        group[i] = GroupCount;
        elements[GroupCount].Add(i);
        use[i] = true;
        foreach (E e in g[i]) if (!use[e.to] && !bridge[e.idx])
            {
                dfs(e.to);
            }
    }
    struct E { public int fr, to, idx; public E(int f, int t, int i) { fr = f; to = t; idx = i; } }
}