using System;
using System.Collections.Generic;
using System.Linq;

public class MST
{
    /// <summary>
    /// 最小全域木の重みをO(ElogV)で求める
    /// 依存:UnionFind
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
    /// <summary>
    /// ある頂点を含む連結グラフの最小全域木の重みを求めます
    /// 依存:PriorityQueue,Pair
    /// </summary>
    /// <param name="edge">辺の集合</param>
    /// <param name="st">始点</param>
    /// <returns></returns>
    public static long Prim(IList<IEnumerable<Pair<long, int>>> edge, int st)
    {
        var res = 0L;
        var pq = new PriorityQueue<Pair<long, int>>(false);
        pq.Enqueue(new Pair<long, int>(0, st));
        var th = new bool[edge.Count];
        while (pq.Count != 0)
        {
            var p = pq.Dequeue();
            if (th[p.v2]) continue;
            th[p.v2] = true;
            res += p.v1;
            foreach (var e in edge[p.v2])
                if (!th[e.v2])
                    pq.Enqueue(e);
        }
        return res;
    }
    /// <summary>
    /// 最小全域木のコストを求めます
    /// 依存:UnionFind,Pair
    /// </summary>
    /// <param name="num">頂点数</param>
    /// <param name="func">(連結成分数,頂点がどの連結成分に属しているか)->(連結成分とそれ以外の連結成分を繋ぐ最小の辺）となる関数</param>
    /// <returns></returns>
    public static long Boruvka(int num, Func<int, int[], Pair<long, int>[]> func)
    {
        var uf = new UnionFind(num);
        var res = 0L;
        var rev = new int[num];
        var parent = new int[num];
        while (true)
        {
            var update = false;
            var c = 0;
            for (var i = 0; i < num; i++)
                if (i == uf.Parent(i))
                {
                    rev[c] = i;
                    parent[i] = c++;
                }
            for (var i = 0; i < num; i++)
                parent[i] = parent[uf.Parent(i)];
            var pair = func(c, parent);
            for (var i = 0; i < c; i++)
                if (pair[i].v2 != -1 && !uf.IsSame(rev[pair[i].v2], rev[i]))
                {
                    uf.Union(rev[pair[i].v2], rev[i]);
                    res += pair[i].v1;
                    update = true;
                }
            if (!update) break;
        }
        return res;
    }
}


class UnionFind
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

class PriorityQueue<T> where T : IComparable<T>
{
    public List<T> _item;
    public int Count { get { return _item.Count; } }
    public bool IsMaxHeap { get; set; }
    public T Peek { get { return _item[0]; } }
    public PriorityQueue(bool IsMaxHeap = true, IEnumerable<T> list = null)
    {
        _item = new List<T>();
        this.IsMaxHeap = IsMaxHeap;
        if (list != null)
        {
            _item.AddRange(list);
            Build();
        }
    }
    private int Compare(int i, int j) => (IsMaxHeap ? 1 : -1) * _item[i].CompareTo(_item[j]);
    private void Swap(int i, int j) { var t = _item[i]; _item[i] = _item[j]; _item[j] = t; }
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
                Swap(i, p);
            i = p;
        }
        return val;
    }
    private void Heapify(int index)
    {
        for (int i = index, j; (j = Left(i)) < _item.Count; i = j)
        {
            if (j != _item.Count - 1 && Compare(j, j + 1) < 0) j++;
            if (Compare(i, j) < 0)
                Swap(i, j);
        }
    }
    public T Dequeue()
    {
        T val = _item[0];
        _item[0] = _item[_item.Count - 1];
        _item.RemoveAt(_item.Count - 1);
        Heapify(0);
        return val;
    }
    private void Build()
    {
        for (var i = (_item.Count >> 1) - 1; i >= 0; i--)
            Heapify(i);
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

