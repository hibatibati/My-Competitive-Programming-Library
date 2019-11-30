using System;

public class RingBuffer<T>
{
    private T[] data;
    public int Count { get; }
    public RingBuffer(int size)
    {
        Count = size;
        data = new T[size];
    }
    public T this[int index]
    {
        get { index %= Count; if (index < 0) index += Count; return data[index]; }
        set { index %= Count; if (index < 0) index += Count; data[index] = value; }
    }
}