using System;
using System.Collections.Generic;
using System.Linq;

public class Tree
{
    /// <summary>
    /// 木の直径をO(E)で求める
    /// 依存:Pair
    /// </summary>
    /// <param name="edge">辺の集合</param>
    /// <returns></returns>
    public static long Diameter(IList<IEnumerable<Pair<long, int>>> edge)
        => bfs(edge, Diabfs(edge, 0).v2).v1;
    //bfsなのはStackOverFlow対策(ACならdfsでもよい)
    private static Pair<long, int> Diabfs(IList<IEnumerable<Pair<long, int>>> edge, int st)
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
    public static List<int> EularTour(IList<IEnumerable<int>> tree, int root)
    {
        var list = new List<int>(tree.Count * 2);
        EularTour(tree, root, -1, list);
        return list;
    }
    private static void EularTour(IList<IEnumerable<int>> tree, int index, int pa, List<int> list)
    {
        list.Add(index);
        foreach (var c in tree[index])
            if (c != pa)
                EularTour(tree, c, index, list);
        list.Add(index);
    }
}

class Pair<T1, T2> : IComparable<Pair<T1, T2>>
{
    public T1 v1 { get; set; }
    public T2 v2 { get; set; }
    public Pair() : this(default(T1), default(T2)) { }
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
}
