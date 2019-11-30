using System;
using System.Collections.Generic;
using System.Linq;

public class UndoableUnionFind
{
    public int GroupCount { get; private set; }
    protected int[] data;
    private Stack<Tuple<int, int>> history;
    public virtual int this[int i] { get { return Find(i); } }
    public UndoableUnionFind(int size)
    {
        data = Create(size, () => -1);
        history = new Stack<Tuple<int, int>>();
        GroupCount = size;
    }
    protected int Find(int i)
        => data[i] < 0 ? i : Find(data[i]);
    public int Size(int i)
        => -data[Find(i)];
    public virtual bool Union(int u, int v)
    {
        u = Find(u); v = Find(v);
        history.Push(new Tuple<int, int>(u, data[u]));
        history.Push(new Tuple<int, int>(v, data[v]));
        if (u == v) return false;
        if (data[u] > data[v])
            swap(ref u, ref v);
        GroupCount--;
        data[u] += data[v];
        data[v] = u;
        return true;
    }
    public void Undo()
    {
        if (!history.Any()) return;
        data[history.Peek().Item1] = history.Pop().Item2;
        data[history.Peek().Item1] = history.Pop().Item2;
    }
    public void SnapShot() => history.Clear();
    public void RollBack()
    {
        while (history.Any()) Undo();
    }
}