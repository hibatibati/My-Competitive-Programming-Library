using System;
using System.Collections.Generic;
using System.Linq;

public class MST
{
    /// <summary>
    /// 最小全域木の重みを求める
    /// </summary>
    /// <param name="num">頂点数</param>
    /// <param name="edges">辺の集合</param>
    /// <returns></returns>
    public static long Kruskal(int num, IEnumerable<Pair<long, int, int>> edges)
    {
        var es = edges.OrderBy(v => v);
        var uf = new UnionFind(num);
        var weight = 0L;
        foreach (var e in es)
            if (!uf.IsSame(e.v2, e.v3))
            {
                weight += e.v1;
                uf.Union(e.v2, e.v3);
            }
        return weight;
    }
}


public class UnionFind
{
    private int _num;
    private int[] _parent, _size, _rank;
    public UnionFind(int num)
    {
        _num = num;
        _parent = new int[num]; _size = new int[num]; _rank = new int[num];
        for (var i = 0; i < num; i++) { _parent[i] = i; _size[i] = 1; }
    }
    private int Find(int i)
        => i == _parent[i] ? i : (_parent[i] = Find(_parent[i]));
    public int Parent(int i)
        => Find(i);
    public int Size(int i)
        => _size[Find(i)];
    public bool Union(int v1, int v2)
    {
        v1 = Find(v1); v2 = Find(v2);
        if (v1 == v2) return false;
        _num--;
        if (_rank[v1] > _rank[v2])
        {
            _parent[v2] = v1;
            _size[v1] += _size[v2];
        }
        else
        {
            _parent[v1] = v2;
            _size[v2] += _size[v1];
            if (_rank[v1] == _rank[v2])
                _rank[v2]++;
        }
        return true;
    }
    public bool IsSame(int v1, int v2)
        => Find(v1) == Find(v2);
}

public class Pair<T1, T2> : IComparable<Pair<T1, T2>>
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

public class Pair<T1, T2, T3> : Pair<T1, T2>, IComparable<Pair<T1, T2, T3>>
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
    public new static Pair<T1, T2, T3> MakePair()
    {
        var r = ReadLine().Split(' ');
        return new Pair<T1, T2, T3>(Input.getValue<T1>(r[0]), Input.getValue<T2>(r[1]), Input.getValue<T3>(r[2]));
    }
    public override string ToString()
        => $"{base.ToString()} {v3.ToString()}";
}

