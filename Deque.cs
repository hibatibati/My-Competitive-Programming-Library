using System;

public class Deque<T>
{
    private RingBuffer<T> _buf;
    int _offset = 0, _size;
    public int Count { get; private set; }
    public Deque(int size) { _buf = new RingBuffer<T>(_size = size); }
    public T this[int index]
    {
        get { return _buf[index + _offset]; }
        set { _buf[index + _offset] = value; }
    }
    public void EnqueueHead(T item)
    {
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
        _buf[Count++ + _offset] = item;
    }
    public T DequeueTail()
    {
        if (Count == 0) return default(T);
        return _buf[--Count + _offset];
    }
}

public class RingBuffer<T>
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