using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

/// <summary>
/// Enqueue/Dequeue:O(logN)
/// Peek:O(1)
/// </summary>
/// <typeparam name="T"></typeparam>
public class PriorityQueue<T>
{
    private List<T> item = new List<T>();
    private Comparison<T> cmp;
    public int Count { get { return item.Count; } }
    public T Peek { get { return item[0]; } }
    public PriorityQueue() { cmp = Comparer<T>.Default.Compare; }

    public PriorityQueue(Comparison<T> comparison) { cmp = comparison; }

    public PriorityQueue(IComparer<T> comparer) { cmp = comparer.Compare; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int Parent(int i)
        => (i - 1) >> 1;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int Left(int i)
        => (i << 1) + 1;
    public T Enqueue(T val)
    {
        int i = item.Count;
        item.Add(val);
        while (i > 0)
        {
            int p = Parent(i);
            if (cmp(item[p], val) <= 0)
                break;
            item[i] = item[p];
            i = p;
        }
        item[i] = val;
        return val;
    }
    public T Dequeue()
    {
        var ret = item[0];
        var p = 0;
        var x = item[item.Count - 1];
        while (Left(p) < item.Count - 1)
        {
            var l = Left(p);
            if (l < item.Count - 2 && cmp(item[l + 1], item[l]) < 0) l++;
            if (cmp(item[l], x) >= 0)
                break;
            item[p] = item[l];
            p = l;
        }
        item[p] = x;
        item.RemoveAt(item.Count - 1);
        return ret;
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Any() => item.Count > 0;
}