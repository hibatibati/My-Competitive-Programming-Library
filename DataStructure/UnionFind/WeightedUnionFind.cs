using System;

class WeightedUnionFind
{
    public int GroupCount { get; private set; }
    protected int[] data;
    private long[] dif;
    public virtual int this[int i] { get { return Find(i); } }
    public WeightedUnionFind(int size)
    {
        data = new int[size]; dif = new long[size];
        GroupCount = size;
        for (var i = 0; i < size; i++)
            data[i] = -1;
    }
    protected int Find(int i)
    {
        if (data[i] < 0) return i;
        var root = Find(data[i]);
        dif[i] += dif[data[i]];
        return data[i] = root;
    }
    private long Weight(int i) { Find(i); return dif[i]; }
    public long Dif(int u, int v)
        => Weight(v) - Weight(u);
    public int Size(int i)
        => -data[Find(i)];
    public virtual bool Union(int u, int v, long w)
    {
        w += Weight(u); w -= Weight(v);
        u = Find(u); v = Find(v);
        if (u == v) return false;
        if (data[u] > data[v])
        { swap(ref u, ref v); w = -w; }
        GroupCount--;
        data[u] += data[v];
        data[v] = u;
        dif[v] = w;
        return true;
    }
}
