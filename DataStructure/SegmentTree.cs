using System;
using System.Collections.Generic;
using System.Linq;

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
    /// <summary>
    /// 一点更新
    /// 計算量:O(logN)
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value"></param>
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
    /// <summary>
    /// [left,right)間をvalueで更新
    /// 計算量:O(logN)
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <param name="value"></param>
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
    /// <summary>
    ///　O(N)で構築
    /// </summary>
    public void All_Update()
    {
        for (int i = num - 2; i >= 0; i--)
            item[i] = func(item[Left(i)], item[Right(i)]);
    }
    /// <summary>
    /// 一点取得
    /// 計算量:O(logN)
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
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
    /// <summary>
    /// 区間取得
    /// 計算量:O(logN)
    /// </summary>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
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