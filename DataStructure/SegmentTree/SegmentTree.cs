using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

/// <summary>
/// 一点更新/区間取得、または区間更新/一点取得がO(logN)でできるデータ構造
/// </summary>
/// <typeparam name="T"></typeparam>
public class SegmentTree<T>
{
    protected readonly T[] item;
    protected readonly int num;
    protected readonly Func<T, T, T> func;
    protected readonly Func<T, T, T> updateFunc;
    protected readonly T minT;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected int Parent(int index)
        => (index - 1) >> 1;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected int Left(int index)
        => (index << 1) + 1;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected int Right(int index)
        => (index + 1) << 1;
    public T this[int i]
    {
        get { return item[i + num - 1]; }
        set { item[i + num - 1] = value; }
    }

    public SegmentTree(int num, T minT, Func<T, T, T> func, Func<T, T, T> updateFunc = null)
    {
        this.func = func;
        this.num = 1;
        this.minT = minT;
        this.updateFunc = updateFunc ?? ((T val1, T val2) => val2);
        while (this.num <= num)
            this.num <<= 1;
        item = new T[2 * this.num - 1];
        for (var i = 0; i < 2 * this.num - 1; i++)
            item[i] = minT;
    }

    public void Update(int index, T value)
    {
        index += num - 1;
        item[index] = updateFunc(item[index], value);
        while (index > 0)
        {
            index = Parent(index);
            item[index] = func(item[Left(index)], item[Right(index)]);
        }
    }

    public virtual void Update(int left, int right, T value)
        => Update(left, right, 0, 0, num, value);
    protected virtual void Update(int left, int right, int k, int l, int r, T value)
    {
        if (r <= left || right <= l) return;
        if (left <= l && r <= right) item[k] = updateFunc(item[k], value);
        else
        {
            Update(left, right, Left(k), l, (l + r) >> 1, value);
            Update(left, right, Right(k), (l + r) >> 1, r, value);
        }
    }

    public void All_Update()
    {
        for (int i = num - 2; i >= 0; i--)
            item[i] = func(item[Left(i)], item[Right(i)]);
    }

    public T Query(int index)
    {
        index += num - 1;
        var value = func(minT, item[index]);
        while (index > 0)
        {
            index = Parent(index);
            value = func(value, item[index]);
        }
        return value;
    }

    public virtual T Query(int left, int right)
        => Query(left, right, 0, 0, num);
    protected virtual T Query(int left, int right, int k, int l, int r)
    {
        if (r <= left || right <= l) return minT;
        if (left <= l && r <= right) return item[k];
        else
            return func(Query(left, right, Left(k), l, (l + r) >> 1), Query(left, right, Right(k), (l + r) >> 1, r));
    }

    /// <summary>
    /// check(func(item[st]...item[i]))がtrueとなる最小のi
    /// </summary>
    public int Find(int st, Func<T, bool> check)
    {
        var x = minT;
        return Find(st, check, ref x, 0, 0, num);
    }
    private int Find(int st, Func<T, bool> check, ref T x, int k, int l, int r)
    {
        if (l + 1 == r)
        {
            x = func(x, item[k]);
            return check(x) ? k - num + 1 : -1;
        }
        var m = (l + r) >> 1;
        if (m <= st) return Find(st, check, ref x, Right(k), m, r);
        if (st <= l && !check(func(x, item[k])))
        { x = func(x, item[k]); return -1; }
        var xl = Find(st, check, ref x, Left(k), l, m);
        if (xl >= 0) return xl;
        return Find(st, check, ref x, Right(k), m, r);
    }
}