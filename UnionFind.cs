using System;

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
