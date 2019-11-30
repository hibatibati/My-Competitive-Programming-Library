using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

/// <summary>
/// Push/Pop:O(logN)
/// Peek:O(1)
/// </summary>
/// <typeparam name="T"></typeparam>
public class PriorityQueue<T>
{
    private List<T> data = new List<T>();
    private Comparison<T> cmp;
    public int Count { get { return data.Count; } }
    public T Top { get { return data[0]; } }
    public PriorityQueue() { cmp = cmp ?? Comparer<T>.Default.Compare; }

    public PriorityQueue(Comparison<T> comparison) { cmp = comparison; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int Parent(int i)
        => (i - 1) >> 1;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int Left(int i)
        => (i << 1) + 1;
    public T Push(T val)
    {
        int i = data.Count;
        data.Add(val);
        while (i > 0)
        {
            int p = Parent(i);
            if (cmp(data[p], val) <= 0)
                break;
            data[i] = data[p];
            i = p;
        }
        data[i] = val;
        return val;
    }
    public T Pop()
    {
        var ret = data[0];
        var p = 0;
        var x = data[data.Count - 1];
        while (Left(p) < data.Count - 1)
        {
            var l = Left(p);
            if (l < data.Count - 2 && cmp(data[l + 1], data[l]) < 0) l++;
            if (cmp(data[l], x) >= 0)
                break;
            data[p] = data[l];
            p = l;
        }
        data[p] = x;
        data.RemoveAt(data.Count - 1);
        return ret;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Any() => data.Count > 0;
}