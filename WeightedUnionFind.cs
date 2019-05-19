using System;

public class WeightedUnionFind
{
    private int _num;
    private int[] _parent, _size, _rank;
    private long[] _dif;
    public WeightedUnionFind(int num)
    {
        _num = num;
        _parent = new int[num]; _size = new int[num]; _rank = new int[num]; _dif = new long[num];
        for (var i = 0; i < num; i++) { _parent[i] = i; _size[i] = 1; }
    }
    private int Find(int i)
    {
        if (_parent[i] == i) return i;
        var r = Find(_parent[i]);
        _dif[i] += _dif[_parent[i]];
        return _parent[i] = r;
    }
    private long Weight(int i) { Find(i); return _dif[i]; }
    public long Dif(int v1, int v2)
        => Weight(v2) - Weight(v1);
    public int Parent(int i)
        => Find(i);
    public int Size(int i)
        => _size[Find(i)];
    public bool Union(int v1, int v2, long weight)
    {
        weight += Weight(v1); weight -= Weight(v2);
        v1 = Find(v1); v2 = Find(v2);
        if (v1 == v2) return false;
        _num--;
        if (_rank[v1] > _rank[v2])
        {
            _parent[v2] = v1;
            _size[v1] += _size[v2];
            _dif[v2] = weight;
        }
        else
        {
            _parent[v1] = v2;
            _size[v2] += _size[v1];
            _dif[v1] = -weight;
            if (_rank[v1] == _rank[v2])
                _rank[v2]++;
        }
        return true;
    }
    public bool IsSame(int v1, int v2)
        => Find(v1) == Find(v2);
}
