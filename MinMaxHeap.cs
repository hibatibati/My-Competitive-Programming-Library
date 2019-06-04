using System;
using System.Collections.Generic;

/// <summary>
/// Enqueue/DequeueMin/DequeueMax:ならしO(logN)
/// PeekMin/PeekMax:ならしO(1)
/// </summary>
/// <typeparam name="T">Comparableである型</typeparam>
public class MinMaxHeap<T> where T : IComparable<T>
{
    PriorityQueue<T> minh, minp, maxh, maxp;
    public MinMaxHeap()
    {
        minh = new PriorityQueue<T>(false);
        minp = new PriorityQueue<T>(false);
        maxh = new PriorityQueue<T>();
        maxp = new PriorityQueue<T>();
    }
    void normalize()
    {
        while (minp.Count != 0 && minp.Peek.CompareTo(minh.Peek) == 0)
        {
            minp.Dequeue(); minh.Dequeue();
        }
        while (maxp.Count != 0 && maxp.Peek.CompareTo(maxh.Peek) == 0)
        {
            maxp.Dequeue(); maxh.Dequeue();
        }
    }
    public int Count { get { return minh.Count - minp.Count; } }
    public void Enqueue(T value) { minh.Enqueue(value); maxh.Enqueue(value); }
    public T PeekMin() { normalize(); return minh.Peek; }
    public T PeekMax() { normalize(); return maxh.Peek; }
    public T DequeueMin() { normalize(); var t = minh.Dequeue(); maxp.Enqueue(t); return t; }
    public T DequeueMax() { normalize(); var t = maxh.Dequeue(); minp.Enqueue(t); return t; }
}

class PriorityQueue<T> where T : IComparable<T>
{
    public List<T> _item;
    public int Count { get { return _item.Count; } }
    public bool IsMaxHeap { get; set; }
    public T Peek { get { return _item[0]; } }
    public PriorityQueue(bool IsMaxHeap = true) { _item = new List<T>(); this.IsMaxHeap = IsMaxHeap; }
    private int Compare(int i, int j) => (IsMaxHeap ? 1 : -1) * _item[i].CompareTo(_item[j]);
    private int Parent(int i)
        => (i - 1) >> 1;
    private int Left(int i)
        => (i << 1) + 1;
    public T Enqueue(T val)
    {
        int i = _item.Count;
        _item.Add(val);
        while (i > 0)
        {
            int p = Parent(i);
            if (Compare(i, p) > 0)
            {
                var tmp = _item[i];
                _item[i] = _item[p];
                _item[p] = tmp;
            }
            i = p;
        }
        return val;
    }
    public T Dequeue()
    {
        T val = _item[0];
        _item[0] = _item[_item.Count - 1];
        _item.RemoveAt(_item.Count - 1);
        for (int i = 0, j; (j = Left(i)) < _item.Count; i = j)
        {
            if (j != _item.Count - 1 && Compare(j, j + 1) < 0) j++;
            if (Compare(i, j) < 0)
            {
                var tmp = _item[i];
                _item[i] = _item[j];
                _item[j] = tmp;
            }
        }
        return val;
    }
}
