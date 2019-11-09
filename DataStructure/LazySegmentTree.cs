using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

public class LazySegmentTree<T, E>
where E : IComparable<E>
{
    protected readonly int num;
    protected readonly T minT;
    protected readonly T[] item;
    protected readonly E minE;
    protected readonly E[] lazy;
    protected readonly Func<T, T, T> func;
    protected readonly Func<T, E, T> updateFunc;
    protected readonly Func<E, E, E> lazyFunc;
    protected readonly Func<E, int, E> secf;

    public T this[int i]
    {
        get { return item[i + num - 1]; }
        set { item[i + num - 1] = value; }
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
    public LazySegmentTree(int num, T minT, E minE, Func<T, T, T> func, Func<T, E, T> updateFunc = null, Func<E, E, E> lazyFunc = null, Func<E, int, E> secf = null)
    {
        this.func = func;
        this.num = 1;
        this.minT = minT;
        this.minE = minE;
        this.updateFunc = updateFunc ?? ((a, b) => a);
        this.lazyFunc = lazyFunc ?? ((a, b) => b);
        this.secf = secf ?? ((a, b) => a);

        while (this.num <= num)
            this.num <<= 1;
        item = new T[2 * this.num - 1];
        for (var i = 0; i < 2 * this.num - 1; i++)
            item[i] = minT;
        lazy = Enumerable.Repeat(0, 2 * this.num + 1).Select(_ => minE).ToArray();
    }
    protected void eval(int len, int k)
    {
        if (lazy[k].CompareTo(minE) == 0) return;
        if (Right(k) < num * 2)
        {
            lazy[Left(k)] = lazyFunc(lazy[Left(k)], lazy[k]);
            lazy[Right(k)] = lazyFunc(lazy[Right(k)], lazy[k]);
        }
        item[k] = updateFunc(item[k], secf(lazy[k], len));
        lazy[k] = minE;
    }
    public void All_Update()
    {
        for (int i = num - 2; i >= 0; i--)
            item[i] = func(item[Left(i)], item[Right(i)]);
    }
    public void Update(int left, int right, E value)
        => Update(left, right, 0, 0, num, value);
    protected void Update(int left, int right, int k, int l, int r, E value)
    {
        eval(r - l, k);
        if (r <= left || right <= l) return;
        if (left <= l && r <= right)
        {
            lazy[k] = lazyFunc(lazy[k], value);
            eval(r - l, k);
        }
        else
        {
            Update(left, right, Left(k), l, (l + r) >> 1, value);
            Update(left, right, Right(k), (l + r) >> 1, r, value);
            item[k] = func(item[Left(k)], item[Right(k)]);
        }
    }
    public T Query(int left, int right)
        => Query(left, right, 0, 0, num);
    protected T Query(int left, int right, int k, int l, int r)
    {
        if (r <= left || right <= l) return minT;
        eval(r - l, k);
        if (left <= l && r <= right) return item[k];
        else
            return func(Query(left, right, Left(k), l, (l + r) >> 1), Query(left, right, Right(k), (l + r) >> 1, r));
    }
}