using System;
/// <summary>
/// 計算量:Enqueue/Dequeue/ランダムアクセス:O(1)
/// </summary>
/// <typeparam name="T"></typeparam>
public class Deque<T>
{
    private T[] data;
    int offset, sz;
    public int Count { get; private set; }
    public Deque() { data = new T[sz = 16]; }
    public T this[int i]
    {
        get { return data[(i + offset) & (sz - 1)]; }
        set { data[(i + offset) & (sz - 1)] = value; }
    }
    private void Extend()
    {
        var t = new T[sz << 1];
        for (var i = 0; i < sz; i++)
            t[i] = data[(offset + i) & (sz - 1)];
        offset = 0;
        data = t;
        sz <<= 1;
    }
    public void PushHead(T item)
    {
        if (Count == sz) Extend();
        data[--offset & (sz - 1)] = item;
        Count++;
    }
    public T PopHead()
    {
        if (Count == 0) return default(T);
        Count--;
        return data[offset++ & (sz - 1)];
    }
    public void PushTail(T item)
    {
        if (Count == sz) Extend();
        data[(Count++ + offset) & (sz - 1)] = item;
    }
    public T PopTail()
    {
        if (Count == 0) return default(T);
        return data[(--Count + offset) & (sz - 1)];
    }
    public bool Any() => Count > 0;
}