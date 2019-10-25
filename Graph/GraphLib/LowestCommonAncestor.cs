using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

class LowestCommonAncestor
{
    private List<int>[] edge;
    private int num, lb;
    private int[][] parent;
    private int[] depth;
    public LowestCommonAncestor(int num)
    { this.num = num; edge = Create(num, () => new List<int>()); }
    public void AddEdge(int u, int v)
    {
        edge[u].Add(v); edge[v].Add(u);
    }
    public void Build(int root = 0)
    {
        for (lb = 31; lb >= 0; lb--)
            if ((1 & num >> lb) == 1)
                break;
        parent = Create(lb + 1, () => Create(num, () => -1));
        depth = new int[num];
        PreDFS(root, -1);
        for (var i = 1; i <= lb; i++)
            for (var j = 0; j < num; j++)
                if (parent[i - 1][j] != -1)
                {
                    parent[i][j] = parent[i - 1][parent[i - 1][j]];
                }
    }
    private void PreDFS(int index, int p)
    {
        parent[0][index] = p;
        foreach (var e in edge[index])
            if (e != p)
            {
                depth[e] = depth[index] + 1;
                PreDFS(e, index);
            }
    }
    public int LCA(int u, int v)
    {
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