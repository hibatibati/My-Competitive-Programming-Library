using System;

public class BIT
{
    //1-indexed
    int bit;
    long[] dat;
    public BIT(int N)
    {
        dat = new long[N + 1];
        bit = 1;
        while ((bit << 1) <= N) bit <<= 1;
    }
    public long this[int idx]
    {
        get { long s = 0; for (var i = idx; i > 0; i -= i & -i) s += dat[i]; return s; }
    }
    public void add(int idx, long v)
    {
        for (var i = idx; i < dat.Length; i += i & -i) dat[i] += v;
    }
    public int LowerBound(long v)
    {
        int now = 0;
        for(int k = bit; k > 0; k >>= 1)
        {
            if (now + k < dat.Length && dat[now + k] < v)
            {
                v -= dat[now + k];
                now += k;
            }
        }
        return now+1;
    }
    public int UpperBound(long v)
    {
        int now = 0;
        for (int k = bit; k > 0; k >>= 1)
        {
            if (now + k < dat.Length && dat[now + k] <= v)
            {
                v -= dat[now + k];
                now += k;
            }
        }
        return now+1;
    }
}