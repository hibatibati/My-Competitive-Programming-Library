using System;
/// <summary>
/// Union:O(α(N))
/// </summary>
public class UnionFind
{
    protected int[] parent, size;
    public virtual int this[int i] { get { return Find(i); } }
    public UnionFind(int num)
    {
        parent = new int[num]; size = new int[num];
        for (var i = 0; i < num; i++) { parent[i] = i; size[i] = 1; }
    }
    protected int Find(int i)
        => i == parent[i] ? i : (parent[i] = Find(parent[i]));
    public int Size(int i)
        => size[Find(i)];
    public virtual bool Union(int v1, int v2)
    {
        v1 = Find(v1); v2 = Find(v2);
        if (v1 == v2) return false;
        if (size[v1] < size[v2])
            swap(ref v1, ref v2);
        parent[v2] = v1;
        size[v1] += size[v2];
        return true;
    }
}