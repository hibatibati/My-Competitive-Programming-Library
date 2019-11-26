using System;
/// <summary>
/// Union:O(α(N))
/// </summary>
public class UnionFind
{
    public int GroupCount { get; private set; }
    protected int[] parent;
    public virtual int this[int i] { get { return Find(i); } }
    public UnionFind(int num)
    {
        parent = new int[num];
        GroupCount = num;
        for (var i = 0; i < num; i++)
            parent[i] = -1;
    }
    protected int Find(int i)
        => parent[i] < 0 ? i : (parent[i] = Find(parent[i]));
    public int Size(int i)
        => -parent[Find(i)];
    public virtual bool Union(int v1, int v2)
    {
        v1 = Find(v1); v2 = Find(v2);
        if (v1 == v2) return false;
        if (parent[v1] > parent[v2])
            swap(ref v1, ref v2);
        GroupCount--;
        parent[v1] += parent[v2];
        parent[v2] = v1;
        return true;
    }
}