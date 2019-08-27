using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Monoid:funcに対して区間更新/区間取得がO(log(要素数))でできるデータ構造
/// </summary>
/// <typeparam name="T"></typeparam>
public class LazySegmentTree<T> : SegmentTree<T>
    where T : IComparable<T>
{
    protected T e, min;
    protected T[] lazy;
    protected Func<T, T, T> lazyFunc;
    protected Func<T, int, T> lazyUpdateFunc;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="num">要素数</param>
    /// <param name="init">値配列の初期値</param>
    /// <param name="e">遅延評価配列の初期値</param>
    /// <param name="func">値のマージをする関数</param>
    /// <param name="updateFunc">値の更新をする関数</param>
    /// <param name="lazyFunc">遅延値の更新をする関数</param>
    /// <param name="lazyUpdateFunc">遅延値決定をするために区間の長さとの関数が必要な場合、設定する</param>
    /// <param name="min">最小元(基本はinit)</param>
    public LazySegmentTree(int num, T init, T e, Func<T, T, T> func, Func<T, T, T> updateFunc = null, Func<T, T, T> lazyFunc = null, Func<T, int, T> lazyUpdateFunc = null, T min = default(T))
        : base(num, init, func, updateFunc)
    {
        this.e = e;
        this.min = min;
        if (min.CompareTo(default(T)) == 0)
            this.min = init;
        this.lazyFunc = lazyFunc ?? ((a, b) => b);
        this.lazyUpdateFunc = lazyUpdateFunc ?? ((a, b) => a);
        lazy = Enumerable.Repeat(0, 2 * this.num + 1).Select(_ => e).ToArray();
    }
    protected void eval(int len, int k)
    {
        if (lazy[k].CompareTo(e) == 0) return;
        if (Right(k) < num * 2)
        {
            lazy[Left(k)] = lazyFunc(lazy[Left(k)], lazy[k]);
            lazy[Right(k)] = lazyFunc(lazy[Right(k)], lazy[k]);
        }
        item[k] = updateFunc(item[k], lazyUpdateFunc(lazy[k], len));
        lazy[k] = e;
    }
    /// <summary>
    /// [left,right)間を更新
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="value"></param>
    public override void Update(int left, int right, T value)
        => Update(left, right, 0, 0, num, value);
    protected override void Update(int left, int right, int k, int l, int r, T value)
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
    /// <summary>
    /// 区間取得
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public override T Query(int left, int right)
        => Query(left, right, 0, 0, num);
    protected override T Query(int left, int right, int k, int l, int r)
    {
        if (r <= left || right <= l) return min;
        eval(r - l, k);
        if (left <= l && r <= right) return item[k];
        else
            return func(Query(left, right, Left(k), l, (l + r) >> 1), Query(left, right, Right(k), (l + r) >> 1, r));
    }
}

public class SegmentTree<T>
{
    protected readonly T[] item;
    protected readonly int num;
    protected readonly Func<T, T, T> func;
    protected readonly Func<T, T, T> updateFunc;
    protected readonly T init;

    protected int Parent(int index)
        => (index - 1) >> 1;
    protected int Left(int index)
        => (index << 1) + 1;
    protected int Right(int index)
        => (index + 1) << 1;
    public T this[int i]
    {
        get { return item[i + num - 1]; }
        set { item[i + num - 1] = value; }
    }

    public SegmentTree(int num, T init, Func<T, T, T> func, Func<T, T, T> updateFunc = null)
    {
        this.func = func;
        this.num = 1;
        this.init = init;
        this.updateFunc = updateFunc ?? ((T val1, T val2) => val2);
        while (this.num <= num)
            this.num *= 2;
        item = new T[2 * this.num - 1];
        for (var i = 0; i < 2 * this.num - 1; i++)
            item[i] = init;
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
        var value = func(init, item[index]);
        while (index > 0)
        {
            index = Parent(index);
            value = func(value, item[index]);
        }
        return value;
    }
    //[left,right)
    public virtual T Query(int left, int right)
        => Query(left, right, 0, 0, num);
    protected virtual T Query(int left, int right, int k, int l, int r)
    {
        if (r <= left || right <= l) return init;
        if (left <= l && r <= right) return item[k];
        else
            return func(Query(left, right, Left(k), l, (l + r) >> 1), Query(left, right, Right(k), (l + r) >> 1, r));
    }
}
