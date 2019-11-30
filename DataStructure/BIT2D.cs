using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

class BIT2D
{
    int H, W;
    long[] item;
    public long this[int i, int j]
    {
        get { return RangeSum(i, j, i, j); }
        set { Add(i, j, value - this[i, j]); }
    }
    public BIT2D(int h, int w)
    { H = h; W = w; item = new long[h * w]; }
    public void Add(int h, int w, long val)
    { for (++h; h <= H; h += h & -h) AddX(h, w, val); }
    public void AddRange(int h1, int w1, int h2, int w2, long val)
    {
        Add(h1, w1, val);
        if (h2 + 1 < H) Add(h2 + 1, w1, -val);
        if (w2 + 1 < W) Add(h1, w2 + 1, -val);
        if (h2 + 1 < H && w2 + 1 < W) Add(h2 + 1, w2 + 1, val);
    }
    public long Sum(int h, int w)
    {
        long r = 0;
        for (++h; h > 0; h -= h & -h) r += SumX(h, w);
        return r;
    }
    public long RangeSum(int h1, int w1, int h2, int w2)
        => Sum(h2, w2) - Sum(h1 - 1, w2) - Sum(h2, w1 - 1) + Sum(h1 - 1, w1 - 1);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int id(int h, int w) => (h - 1) * W + w - 1;
    private void AddX(int h, int w, long val)
    { for (++w; w <= W; w += w & -w) item[id(h, w)] += val; }
    private long SumX(int h, int w)
    {
        long r = 0;
        for (++w; w > 0; w -= w & -w) r += item[id(h, w)];
        return r;
    }
}