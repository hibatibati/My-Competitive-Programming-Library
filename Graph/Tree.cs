using System;
using System.Collections.Generic;
using System.Linq;

public class Tree
{
    /// <summary>
    /// 木の直径をO(E)で求める
    /// </summary>
    /// <param name="edge">辺の集合</param>
    /// <returns></returns>
    public static long Diameter(IList<IEnumerable<Pair<long, int>>> edge)
        => bfs(edge, bfs(edge, 0).v2).v1;
    //dfsにするとコードはかなり短くなる
    private static Pair<long, int> bfs(IList<IEnumerable<Pair<long, int>>> edge, int st)
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
    public static Pair<T1, T2> MakePair()
    {
        var r = ReadLine().Split(' ');
        return new Pair<T1, T2>(Input.getValue<T1>(r[0]), Input.getValue<T2>(r[1]));
    }
    public override string ToString()
        => $"{v1.ToString()} {v2.ToString()}";
}
