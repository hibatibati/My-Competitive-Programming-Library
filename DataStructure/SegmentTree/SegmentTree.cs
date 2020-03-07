using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public class SegmentTree<T>
{
    protected readonly T[] dat;
    protected readonly int sz;
    protected readonly Func<T, T, T> merge;
    protected readonly Func<T, T, T> update;
    protected readonly T id;
    private bool finished = true;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected int Left(int i)
        => i << 1;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected int Right(int i)
        => (i << 1) | 1;
    public T this[int i]
    {
        get { return dat[i + sz]; }
        set { finished = false; dat[i + sz] = value; }
    }

    public SegmentTree(int N, T id, Func<T, T, T> merge, Func<T, T, T> update = null)
    {
        this.sz = 1;
        while (sz < N) sz <<= 1;
        this.id = id;
        this.merge = merge;
        this.update = update ?? ((T val1, T val2) => val2);
        dat = Create(sz << 1, () => id);
    }

    public void Update(int i, T value)
    {
        i += sz;
        dat[i] = update(dat[i], value);
        while (i > 1)
        {
            i >>= 1;
            dat[i] = merge(dat[Left(i)], dat[Right(i)]);
        }
    }

    public void Build()
    {
        for (int i = sz - 1; i > 0; i--)
            dat[i] = merge(dat[Left(i)], dat[Right(i)]);
        finished = true;
    }

    public virtual T Query(int left, int right)
    {
        if (!finished) throw new Exception("You need to Execute \"Build()\"");
        T l = id, r = id;
        for (left += sz, right += sz; left < right; left >>= 1, right >>= 1)
        {
            if ((left & 1) == 1) l = merge(l, dat[left++]);
            if ((right & 1) == 1) r = merge(dat[--right], r);
        }
        return merge(l, r);
    }

    public int Find(int st, Func<T, bool> check)
    {
        var x = id;
        return Find(st, check, ref x, 1, 0, sz);
    }
    private int Find(int st, Func<T, bool> check, ref T x, int k, int l, int r)
    {
        if (l + 1 == r)
        { x = merge(x, dat[k]); return check(x) ? k - sz : -1; }
        var m = (l + r) >> 1;
        if (m <= st) return Find(st, check, ref x, Right(k), m, r);
        if (st <= l && !check(merge(x, dat[k])))
        { x = merge(x, dat[k]); return -1; }
        var xl = Find(st, check, ref x, Left(k), l, m);
        if (xl >= 0) return xl;
        return Find(st, check, ref x, Right(k), m, r);
    }
}