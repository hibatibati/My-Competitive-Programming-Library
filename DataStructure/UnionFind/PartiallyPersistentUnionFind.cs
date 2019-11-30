using System;
using System.Collections.Generic;
using System.Linq;

public class PartiallyPersistentUnionFind
{
    private int now = -1;
    protected int[] data, time;
    List<Tuple<int, int>>[] size;
    public virtual int this[int t, int i] { get { return Find(t, i); } }
    public PartiallyPersistentUnionFind(int size)
    {
        data = Create(size, () => -1);
        this.size = Create(size, () => new List<Tuple<int, int>> { new Tuple<int, int>(-1, 1) });
        time = Create(size, () => int.MaxValue);
    }
    protected int Find(int t, int i)
    {
        if (time[i] > t) return i;
        return Find(t, data[i]);
    }
    public int Size(int t, int x)
    {
        x = Find(t, x);
        int r = size[x].Count, l = -1;
        while (r - l > 1)
        {
            var m = (r + l) / 2;
            if (size[x][m].Item1 > t) r = m;
            else l = m;
        }
        return size[x][l].Item2;
    }
    public virtual bool Union(int u, int v)
    {
        now++;
        u = Find(now, u); v = Find(now, v);
        if (u == v) return false;
        if (data[u] > data[v])
            swap(ref u, ref v);
        data[u] += data[v];
        size[u].Add(new Tuple<int, int>(now, -data[u]));
        data[v] = u;
        time[v] = now;
        return true;
    }
}