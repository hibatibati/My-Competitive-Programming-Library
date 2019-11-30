using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public class LazySegmentTree<T, E>
where E : IComparable<E>
{
    protected readonly int size;
    protected readonly T idT;
    protected readonly T[] data;
    protected readonly E idE;
    protected readonly E[] lazy;
    protected readonly Func<T, T, T> mergeT;
    protected readonly Func<T, E, T> update;
    protected readonly Func<E, E, E> mergeE;
    protected readonly Func<E, int, E> sec;

    public T this[int i]
    {
        get { return data[i + size - 1]; }
        set { data[i + size - 1] = value; }
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected int Parent(int index)
        => (index - 1) >> 1;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected int Left(int index)
        => (index << 1) + 1;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected int Right(int index)
        => (index + 1) << 1;
    public LazySegmentTree(int N, T idT, E idE, Func<T, T, T> mergeT, Func<T, E, T> update = null, Func<E, E, E> mergeE = null, Func<E, int, E> sec = null)
    {
        this.mergeT = mergeT;
        this.size = 1;
        this.idT = idT;
        this.idE = idE;
        this.update = update ?? ((a, b) => a);
        this.mergeE = mergeE ?? ((a, b) => b);
        this.sec = sec ?? ((a, b) => a);

        while (this.size < N)
            this.size <<= 1;
        data = new T[2 * this.size - 1];
        for (var i = 0; i < 2 * this.size - 1; i++)
            data[i] = idT;
        lazy = Enumerable.Repeat(0, 2 * this.size + 1).Select(_ => idE).ToArray();
    }
    protected void eval(int len, int k)
    {
        if (lazy[k].CompareTo(idE) == 0) return;
        if (Right(k) < size * 2)
        {
            lazy[Left(k)] = mergeE(lazy[Left(k)], lazy[k]);
            lazy[Right(k)] = mergeE(lazy[Right(k)], lazy[k]);
        }
        data[k] = update(data[k], sec(lazy[k], len));
        lazy[k] = idE;
    }
    public void Build()
    {
        for (int i = size - 2; i >= 0; i--)
            data[i] = mergeT(data[Left(i)], data[Right(i)]);
    }
    public void Update(int left, int right, E value, int k = 0, int l = 0, int r = -1)
    {
        if (r == -1) r = size;
        eval(r - l, k);
        if (r <= left || right <= l) return;
        if (left <= l && r <= right)
        {
            lazy[k] = mergeE(lazy[k], value);
            eval(r - l, k);
        }
        else
        {
            Update(left, right, value, Left(k), l, (l + r) >> 1);
            Update(left, right, value, Right(k), (l + r) >> 1, r);
            data[k] = mergeT(data[Left(k)], data[Right(k)]);
        }
    }
    public T Query(int left, int right, int k = 0, int l = 0, int r = -1)
    {
        if (r == -1) r = size;
        if (r <= left || right <= l) return idT;
        eval(r - l, k);
        if (left <= l && r <= right) return data[k];
        else
            return mergeT(Query(left, right, Left(k), l, (l + r) >> 1), Query(left, right, Right(k), (l + r) >> 1, r));
    }

    public int Find(int st, Func<T, bool> check)
    {
        var x = idT;
        return Find(st, check, ref x, 0, 0, size);
    }
    private int Find(int st, Func<T, bool> check, ref T x, int k, int l, int r)
    {
        if (l + 1 == r)
        { x = mergeT(x, data[k]); return check(x) ? k - size + 1 : -1; }
        eval(r - l, k);
        var m = (l + r) >> 1;
        if (m <= st) return Find(st, check, ref x, Right(k), m, r);
        if (st <= l && !check(mergeT(x, data[k])))
        { x = mergeT(x, data[k]); return -1; }
        var xl = Find(st, check, ref x, Left(k), l, m);
        if (xl >= 0) return xl;
        return Find(st, check, ref x, Right(k), m, r);
    }
}
