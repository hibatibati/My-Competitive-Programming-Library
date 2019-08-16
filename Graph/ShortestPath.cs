using System;
using System.Collections.Generic;
using System.Linq;

public class ShortestPath
{
    /// <summary>
    /// 単一始点最短距離をO((E+V)logV)で求める
    /// 依存:PriorityQueue,Pair
    /// </summary>
    /// <param name="edges">負辺を含まない辺の集合</param>
    /// <param name="st">始点</param>
    /// <returns></returns>
    public static long[] Dijkstra(IList<IEnumerable<Pair<long, int>>> edges, int st)
    {
        var dist = Enumerable.Repeat(long.MaxValue, edges.Count).ToArray();
        var pq = new PriorityQueue<Pair<long, int>>(false);
        pq.Enqueue(new Pair<long, int>(0, st));
        dist[st] = 0;
        while (pq.Count != 0)
        {
            var p = pq.Dequeue();
            if (dist[p.v2] < p.v1) continue;
            foreach (var e in edges[p.v2])
                if (dist[e.v2] > e.v1 + dist[p.v2])
                {
                    dist[e.v2] = e.v1 + dist[p.v2];
                    pq.Enqueue(new Pair<long, int>(dist[e.v2], e.v2));
                }
        }
        return dist;
    }
    /// <summary>
    /// 全点対最短距離をO(V^3)で求める
    /// </summary>
    /// <param name="num">頂点数</param>
    /// <param name="edges">辺の集合</param>
    /// <param name="directed">有向グラフか</param>
    /// <returns></returns>
    public static long[][] WarshallFloyd(int num, IEnumerable<Pair<long, int, int>> edges, bool directed = false)
    {
        var dist = Enumerable.Repeat(0, num).Select(_ => Enumerable.Repeat(long.MaxValue / 2, num).ToArray()).ToArray();
        foreach (var e in edges)
        {
            dist[e.v2][e.v3] = e.v1;
            if (!directed)
                dist[e.v3][e.v2] = e.v1;
        }
        for (var k = 0; k < num; k++)
            for (var i = 0; i < num; i++)
                for (var j = 0; j < num; j++)
                    dist[i][j] = Min(dist[i][j], dist[i][k] + dist[k][j]);
        return dist;
    }
    /// <summary>
    /// 有向グラフ上の単一始点最短距離をO(VE)で求める
    /// </summary>
    /// <param name="num">頂点数</param>
    /// <param name="edges">辺の集合</param>
    /// <param name="st">始点</param>
    /// <param name="dist">始点からの最短距離</param>
    /// <returns>始点から辿り着ける負閉路が存在した場合、false</returns>
    public static bool BellmanFord(int num, IEnumerable<Pair<long, int, int>> edges, int st, out long[] dist)
    {
        dist = Enumerable.Repeat(long.MaxValue, num).ToArray();
        dist[st] = 0;
        for (var i = 0; i < num - 1; i++)
            foreach (var e in edges)
                if (dist[e.v2] != long.MaxValue)
                    dist[e.v3] = Min(dist[e.v3], dist[e.v2] + e.v1);
        for(var i=0;i<num;i++)
            foreach (var e in edges)
            {
                if (dist[e.v2] == long.MaxValue) continue;
                if (dist[e.v3] > dist[e.v2] + e.v1)
                    return false;
            }
        return true;
    }
}

class PriorityQueue<T> where T : IComparable<T>
{
    public List<T> _item;
    public int Count { get { return _item.Count; } }
    public bool IsMaxHeap { get; set; }
    public T Peek { get { return _item[0]; } }
    public PriorityQueue(bool IsMaxHeap = true) { _item = new List<T>(); this.IsMaxHeap = IsMaxHeap; }
    private int Compare(int i, int j) => (IsMaxHeap ? 1 : -1) * _item[i].CompareTo(_item[j]);
    private int Parent(int i)
        => (i - 1) >> 1;
    private int Left(int i)
        => (i << 1) + 1;
    public T Enqueue(T val)
    {
        int i = _item.Count;
        _item.Add(val);
        while (i > 0)
        {
            int p = Parent(i);
            if (Compare(i, p) > 0)
            {
                var tmp = _item[i];
                _item[i] = _item[p];
                _item[p] = tmp;
            }
            i = p;
        }
        return val;
    }
    public T Dequeue()
    {
        T val = _item[0];
        _item[0] = _item[_item.Count - 1];
        _item.RemoveAt(_item.Count - 1);
        for (int i = 0, j; (j = Left(i)) < _item.Count; i = j)
        {
            if (j != _item.Count - 1 && Compare(j, j + 1) < 0) j++;
            if (Compare(i, j) < 0)
            {
                var tmp = _item[i];
                _item[i] = _item[j];
                _item[j] = tmp;
            }
        }
        return val;
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

class Pair<T1, T2, T3> : Pair<T1, T2>, IComparable<Pair<T1, T2, T3>>
{
    public T3 v3 { get; set; }
    public Pair() : base() { v3 = default(T3); }
    public Pair(T1 v1, T2 v2, T3 v3) : base(v1, v2)
    { this.v3 = v3; }

    public int CompareTo(Pair<T1, T2, T3> p)
    {
        var c = base.CompareTo(p);
        if (c == 0)
            c = Comparer<T3>.Default.Compare(v3, p.v3);
        return c;
    }
    public override string ToString()
        => $"{base.ToString()} {v3.ToString()}";
}

