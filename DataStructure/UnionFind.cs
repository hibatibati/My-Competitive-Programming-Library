using System;
/// <summary>
/// Union/IsSame:O(α(N))
/// </summary>
public class UnionFind
{
    protected int[] parent, size, rank;
    public UnionFind(int num)
    {
        parent = new int[num]; size = new int[num]; rank = new int[num];
        for (var i = 0; i < num; i++) { parent[i] = i; size[i] = 1; }
    }
    protected int Find(int i)
        => i == parent[i] ? i : (parent[i] = Find(parent[i]));
    public int Parent(int i)
        => Find(i);
    public int Size(int i)
        => size[Find(i)];
    public bool Union(int v1, int v2)
    {
        v1 = Find(v1); v2 = Find(v2);
        if (v1 == v2) return false;
        if (rank[v1] < rank[v2])
            swap(ref v1, ref v2);
        parent[v2] = v1;
        size[v1] += size[v2];
        if (rank[v1] == rank[v2])
            rank[v1]++;
        return true;
    }
    public bool IsSame(int v1, int v2)
        => Find(v1) == Find(v2);
}
