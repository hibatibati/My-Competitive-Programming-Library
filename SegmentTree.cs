using System;
using System.Collections.Generic;
using System.Linq;

public class SegmentTree<T>
{
    private readonly List<T> item;
    private readonly int _num;
    private readonly Func<T, T, T> _func;
    private readonly Func<T, T, T> updateFunc;
    private readonly T _init;

    private int Parent(int index)
        => (index - 1) / 2;
    private int Left(int index)
        => 2 * index + 1;
    private int Right(int index)
        => 2 * (index + 1);

    public SegmentTree(int num, T init, Func<T, T, T> func, Func<T, T, T> updateFunc = null)
    {
        _func = func;
        _num = 1;
        _init = init;
        this.updateFunc = updateFunc ?? ((T val1, T val2) => val2);
        while (_num <= num)
            _num *= 2;
        item = new List<T>(2 * _num - 1);
        for (var i = 0; i < 2 * _num - 1; i++)
            item.Add(init);
    }
    public void Update(int index, T value)
    {
        index += _num - 1;
        item[index] = updateFunc(item[index], value);
        while (index > 0)
        {
            index = Parent(index);
            item[index] = _func(item[Left(index)], item[Right(index)]);
        }
    }
    public void Update(int left, int right, T value)
        => Update(left, right, 0, 0, _num, value);
    private void Update(int left, int right, int k, int l, int r, T value)
    {
        if (r <= left || right <= l) return;
        if (left <= l && r <= right) item[k] = updateFunc(item[k], value);
        else
        {
            Update(left, right, Left(k), l, (l + r) / 2, value);
            Update(left, right, Right(k), (l + r) / 2, r, value);
        }
    }
    public T Query(int index)
    {
        index += _num - 1;
        var value = _func(_init, item[index]);
        while (index > 0)
        {
            index = Parent(index);
            value = _func(value, item[index]);
        }
        return value;
    }
    public T Query(int left, int right)
        => Query(left, right, 0, 0, _num);
    private T Query(int left, int right, int k, int l, int r)
    {
        if (r <= left || right <= l) return _init;
        if (left <= l && r <= right) return item[k];
        else
            return _func(Query(left, right, Left(k), l, (l + r) / 2), Query(left, right, Right(k), (l + r) / 2, r));
    }
}