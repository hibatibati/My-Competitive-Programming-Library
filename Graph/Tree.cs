using System;
using System.Collections.Generic;
using System.Linq;

public class Tree
{
    public List<Pair<long, int>>[] edge;
    public int Count { get; }
    public Tree(int num)
    {
        Count = num;
        edge = Enumerable.Repeat(0, num).Select(_ => new List<Pair<long, int>>()).ToArray();
    }
    private static void swap<T>(ref T v1, ref T v2)
    { var t = v2; v2 = v1; v1 = t; }
    /// <summary>
    /// 辺を追加します
    /// </summary>
    /// <param name="u"></param>
    /// <param name="v"></param>
    /// <param name="weight"></param>
    public void AddEdge(int u, int v, long weight = 1)
    {
        edge[u].Add(new Pair<long, int>(weight, v));
        edge[v].Add(new Pair<long, int>(weight, u));
    }
    /// <summary>
    /// 木の直径をO(E)で求める
    /// 依存:Pair
    /// </summary>
    /// <param name="edge">辺の集合</param>
    /// <returns></returns>
    public long Diameter()
        => Diameter(Diameter(0).v2).v1;

    private Pair<long, int> Diameter(int st)
    {
        var dist = Enumerable.Repeat(-1L, Count).ToArray();
        dist[st] = 0;
        var que = new Queue<int>();
        que.Enqueue(st);
        var maxj = st;
        while (que.Any())
        {
            var p = que.Dequeue();
            foreach (var e in edge[p])
                if (dist[e.v2] == -1)
                {
                    dist[e.v2] = dist[p] + e.v1;
                    if (dist[maxj] < dist[e.v2]) maxj = e.v2;
                    que.Enqueue(e.v2);
                }
        }
        return new Pair<long, int>(dist[maxj], maxj);
    }

    private List<int> etList;
    /// <summary>
    /// オイラーツアー
    /// </summary>
    /// <param name="tree"></param>
    /// <param name="root"></param>
    /// <returns></returns>
    public List<int> EularTour(int root = 0)
    {
        etList = new List<int>(Count * 2);
        EularTour(root, -1);
        return etList;
    }
    private void EularTour(int index, int pa)
    {
        etList.Add(index);
        foreach (var c in edge[index])
            if (c.v2 != pa)
                EularTour(c.v2, index);
        etList.Add(index);
    }

    private int[][] parent;
    private int[] depth;
    /// <summary>
    /// ダブリングで祖先テーブルを構築します
    /// 計算量:O(VlogV)
    /// </summary>
    /// <param name="root">根</param>
    public void LCABuild(int root = 0)
    {
        int ct = 0;
        for (var i = 31; i >= 0; i--)
            if ((1 & Count >> i) == 1)
            { ct = i; break; }
        parent = Enumerable.Repeat(0, ct + 1).Select(_ => Enumerable.Repeat(-1, Count).ToArray()).ToArray();
        depth = new int[Count];
        LCAdfs(root, -1);
        for (var i = 1; i <= ct; i++)
            for (var j = 0; j < Count; j++)
                if (parent[i - 1][j] != -1)
                    parent[i][j] = parent[i - 1][parent[i - 1][j]];
    }
    private void LCAdfs(int index, int pa)
    {
        parent[0][index] = pa;
        foreach (var e in edge[index])
            if (e.v2 != pa)
            {
                depth[e.v2] = depth[index] + 1;
                LCAdfs(e.v2, index);
            }
    }
    /// <summary>
    /// 二頂点の最小共通祖先を求めます
    /// 計算量:O(logV)
    /// </summary>
    /// <param name="u"></param>
    /// <param name="v"></param>
    /// <returns></returns>
    public int LCA(int u, int v)
    {
        if (depth[u] > depth[v])
            swap(ref u, ref v);
        for (var i = parent.Length - 1; i >= 0; i--)
            if ((1 & (depth[v] - depth[u]) >> i) == 1)
                v = parent[i][v];
        if (u == v) return u;
        for (var i = parent.Length - 1; i >= 0; i--)
            if (parent[i][u] != parent[i][v])
            { u = parent[i][u]; v = parent[i][v]; }
        return parent[0][u];
    }

}

public class Pair<T1, T2> : IComparable<Pair<T1, T2>>
{
    public T1 v1 { get; set; }
    public T2 v2 { get; set; }
    public Pair() { v1 = Input.Next<T1>(); v2 = Input.Next<T2>(); }
    public Pair(T1 v1, T2 v2)
    { this.v1 = v1; this.v2 = v2; }

    public int CompareTo(Pair<T1, T2> p)
    {
        var c = Comparer<T1>.Default.Compare(v1, p.v1);
        if (c == 0)
            c = Comparer<T2>.Default.Compare(v2, p.v2);
        return c;
    }
    public override string ToString()
        => $"{v1.ToString()} {v2.ToString()}";
    public override bool Equals(object obj)
        => this == (Pair<T1, T2>)obj;
    public override int GetHashCode()
        => v1.GetHashCode() ^ v2.GetHashCode();
    public static bool operator ==(Pair<T1, T2> p1, Pair<T1, T2> p2)
        => p1.CompareTo(p2) == 0;
    public static bool operator !=(Pair<T1, T2> p1, Pair<T1, T2> p2)
        => p1.CompareTo(p2) != 0;
    public static bool operator >(Pair<T1, T2> p1, Pair<T1, T2> p2)
        => p1.CompareTo(p2) == 1;
    public static bool operator >=(Pair<T1, T2> p1, Pair<T1, T2> p2)
        => p1.CompareTo(p2) != -1;
    public static bool operator <(Pair<T1, T2> p1, Pair<T1, T2> p2)
        => p1.CompareTo(p2) == -1;
    public static bool operator <=(Pair<T1, T2> p1, Pair<T1, T2> p2)
        => p1.CompareTo(p2) != 1;
}