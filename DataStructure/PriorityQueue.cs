using System;
using System.Collections.Generic;

public class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> _item;
    public int Count { get { return _item.Count; } }
    public bool IsMaxHeap { get; set; }
    public T Peek { get { return _item[0]; } }
    public PriorityQueue(bool IsMaxHeap = true) { _item = new List<T>(); this.IsMaxHeap = IsMaxHeap; }
    private int Compare(int i, int j) => (IsMaxHeap ? 1 : -1) * _item[i].CompareTo(_item[j]);
    private int Parent(int i)
        => (i - 1) >> 1;
    private int Left(int i)
        => (i << 1) + 1;
    public void Enqueue(T val)
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

