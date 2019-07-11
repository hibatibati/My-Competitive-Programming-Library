using System;
/// <summary>
/// 計算量:Enqueue/Dequeue/ランダムアクセス:O(1)
/// 依存:RingBuffer
/// </summary>
/// <typeparam name="T"></typeparam>
public class Deque<T>
{
    private RingBuffer<T> _buf;
    int _offset = 0, _size;
    public int Count { get; private set; }
    public Deque() { _buf = new RingBuffer<T>(_size = 16); }
    public Deque(int size) { _buf = new RingBuffer<T>(_size = size); }
    public T this[int index]
    {
        get { return _buf[index + _offset]; }
        set { _buf[index + _offset] = value; }
    }
    private void Extend()
    {
        var t = new RingBuffer<T>(_size << 1);
        for (var i = 0; i < _size; i++)
            t[i] = _buf[_offset + i];
        _offset = 0;
        _buf = t;
        _size <<= 1;
    }
    public void EnqueueHead(T item)
    {
        if (Count == _size) Extend();
        _buf[--_offset] = item;
        Count++;
    }
    public T DequeueHead()
    {
        if (Count == 0) return default(T);
        Count--;
        return _buf[_offset++];
    }
    public void EnqueueTail(T item)
    {
        if (Count == _size) Extend();
        _buf[Count++ + _offset] = item;
    }
    public T DequeueTail()
    {
        if (Count == 0) return default(T);
        return _buf[--Count + _offset];
    }
}

class RingBuffer<T>
{
    private T[] _item;
    private int _size;
    public RingBuffer(int size)
    {
        _size = size;
        _item = new T[size];
    }
    public T this[int index]
    {
        get { index %= _size; if (index < 0) index += _size; return _item[index]; }
        set { index %= _size; if (index < 0) index += _size; _item[index] = value; }
    }
}