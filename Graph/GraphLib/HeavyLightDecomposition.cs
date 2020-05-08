using System;
using System.Collections.Generic;
using System.Linq;
class HLDecomposition
{
    List<int>[] G;
    public int this[int i] => vertex[i];
    private int[] vertex, head, subtreeSize, par, dep, rev, right;
    public HLDecomposition(int sz)
    {
        G = Create(sz, () => new List<int>());
        vertex = Create(sz, () => -1);
        head = new int[sz];
        subtreeSize = new int[sz];
        par = Create(sz, () => -1);
        dep = new int[sz];
        rev = new int[sz];
        right = new int[sz];
    }
    public void AddEdge(int u, int v)
    {
        G[u].Add(v);
        G[v].Add(u);
    }
    public void Build(int root = 0)
    {
        int c = 0, pos = 0;
        head[root] = root;
        CalcSubtreeSize(root);
        Decomposition(root, ref pos);
    }

    void CalcSubtreeSize(int idx)
    {
        for (int i = 0; i < G[idx].Count; i++)
            if (G[idx][i] == par[idx]) { G[idx].swap(i, G[idx].Count - 1); break; }
        if (par[idx] != -1) G[idx].PopBack();
        for (int i = 0; i < G[idx].Count; i++)
        {
            int to = G[idx][i];
            par[to] = idx;
            dep[to] = dep[idx] + 1;
            CalcSubtreeSize(to);
            subtreeSize[idx] += subtreeSize[to];
            if (subtreeSize[to] > subtreeSize[G[idx][0]]) G[idx].swap(i, 0);
        }
        subtreeSize[idx]++;
    }
    void Decomposition(int idx, ref int pos)
    {
        vertex[idx] = pos++;
        rev[vertex[idx]] = idx;
        for (int i = 0; i < G[idx].Count; i++)
        {
            int to = G[idx][i];
            head[to] = (i == 0 ? head[idx] : to);
            Decomposition(to, ref pos);
        }
        right[idx] = pos;
    }

    public int LCA(int u, int v)
    {
        while (true)
        {
            if (vertex[u] > vertex[v]) swap(ref u, ref v);
            if (head[u] == head[v]) return u;
            v = par[head[v]];
        }
    }
    public int Distance(int u, int v) => dep[u] + dep[v] - 2 * dep[LCA(u, v)];

    public void For_each(int u, int v, Action<int, int> f)
    {
        while (true)
        {
            if (vertex[u] > vertex[v]) swap(ref u, ref v);
            f(Max(vertex[head[v]], vertex[u]), vertex[v] + 1);
            if (head[u] != head[v]) v = par[head[v]];
            else break;
        }
    }

    public void For_each_Edge(int u, int v, Action<int, int> f)
    {
        while (true)
        {
            if (vertex[u] > vertex[v]) swap(ref u, ref v);
            if (head[u] != head[v])
            {
                f(vertex[head[v]], vertex[v] + 1);
                v = par[head[v]];
            }
            else
            {
                if (u != v) f(vertex[u] + 1, vertex[v] + 1);
                break;
            }
        }
    }

    public void SubtreeQuery(int v, Action<int, int> f) => f(vertex[v], right[v]);
}