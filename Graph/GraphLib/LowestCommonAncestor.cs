using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

class LowestCommonAncestor
{
    private List<int>[] edge;
    private int N, lb;
    private int[][] parent;
    private int[] depth;
    private bool finished;
    public LowestCommonAncestor(int N)
    { this.N = N; edge = Create(N, () => new List<int>()); }
    public void AddEdge(int u, int v)
    {
        edge[u].Add(v); edge[v].Add(u);
    }
    public void Build(int root = 0)
    {
        for (lb = 31; lb >= 0; lb--)
            if ((1 & N >> lb) == 1)
                break;
        parent = Create(lb + 1, () => Create(N, () => -1));
        depth = new int[N];
        var st = new Stack<int>();
        st.Push(-1);
        st.Push(root);
        while (st.Any())
        {
            int i = st.Pop(), p = st.Pop();
            parent[0][i] = p;
            foreach (var e in edge[i])
                if (e != p)
                {
                    depth[e] = depth[i] + 1;
                    st.Push(i); st.Push(e);
                }
        }
        for (var i = 1; i <= lb; i++)
            for (var j = 0; j < N; j++)
                if (parent[i - 1][j] != -1)
                {
                    parent[i][j] = parent[i - 1][parent[i - 1][j]];
                }
        finished = true;
    }
    public int LCA(int u, int v)
    {
        if (!finished) throw new Exception("You Need to Execute \"Build()\"");
        if (depth[u] > depth[v])
            swap(ref u, ref v);
        for (var i = lb; i >= 0; i--)
            if ((1 & (depth[v] - depth[u]) >> i) == 1)
                v = parent[i][v];
        if (u == v) return u;
        for (var i = lb; i >= 0; i--)
            if (parent[i][u] != parent[i][v])
            { u = parent[i][u]; v = parent[i][v]; }
        return parent[0][u];
    }
}