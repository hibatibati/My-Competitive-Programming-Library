using System;

public class RingBuffer<T>
{
    private T[] _item;
    public int Count { get; private set; }
    public RingBuffer(int size)
    {
        _item = new T[Count = size];
    }
    public T this[int index]
    {
        get { index %= Count; if (index < 0) index += Count; return _item[index]; }
        set { index %= Count; if (index < 0) index += Count; _item[index] = value; }
    }
}