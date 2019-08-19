using System;
using System.Collections.Generic;
using System.Linq;

public class Tree
{
    private IList<IEnumerable<Pair<long, int>>> edge;
    private List<int> etList;
    public int Count { get { return edge.Count(); } }
    public Tree(IList<IEnumerable<Pair<long, int>>> edge)
    {
        this.edge = edge;
    }
    public Tree(IList<IEnumerable<int>> edge)
    {
        this.edge = edge.Select(adj => adj.Select(v => new Pair<long, int>(1, v))).ToArray();
    }
    /// <summary>
    /// 木の直径をO(E)で求める
    /// 依存:Pair
    /// </summary>
    /// <param name="edge">辺の集合</param>
    /// <returns></returns>
    public long Diameter()
        => Diameter(Diameter(0).v2).v1;
    //bfsなのはStackOverFlow対策(ACならdfsでもよい)
    private Pair<long, int> Diameter(int st)
    {
        var dist = Enumerable.Repeat(-1L, edge.Count).ToArray();
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

    /// <summary>
    /// オイラーツアー
    /// </summary>
    /// <param name="tree"></param>
    /// <param name="root"></param>
    /// <returns></returns>
    public List<int> EularTour(int root)
    {
        etList = new List<int>(edge.Count * 2);
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