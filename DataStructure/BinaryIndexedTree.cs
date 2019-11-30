using System;

public class BIT
{
    //1-indexed
    long[] data;
    public BIT(int num)
    {
        data = new long[num + 1];
    }
    public long this[int index]
    {
        get { var s = 0L; for (var i = index; i > 0; i -= i & -i) s += data[i]; return s; }
    }
    public void add(int index, long value)
    {
        for (var i = index; i < data.Length; i += i & -i) data[i] += value;
    }
}