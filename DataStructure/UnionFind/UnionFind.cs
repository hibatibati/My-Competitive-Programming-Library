using System;
/// <summary>
/// Union:O(α(N))
/// </summary>
public class UnionFind
{
    public int GroupCount { get; private set; }
    protected int[] data;
    public virtual int this[int i] { get { return Find(i); } }
    public UnionFind(int size)
    {
        data = new int[size];
        GroupCount = size;
        for (var i = 0; i < size; i++)
            data[i] = -1;
    }
    protected int Find(int i)
        => data[i] < 0 ? i : (data[i] = Find(data[i]));
    public int Size(int i)
        => -data[Find(i)];
    public virtual bool Union(int u, int v)
    {
        u = Find(u); v = Find(v);
        if (u == v) return false;
        if (data[u] > data[v])
            swap(ref u, ref v);
        GroupCount--;
        data[u] += data[v];
        data[v] = u;
        return true;
    }
}