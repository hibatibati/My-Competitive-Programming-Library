using System;
using System.Collections.Generic;
using System.Linq;

class SegmentTreeFractionalCascading
{
    Tuple<int, long>[][] seg;
    int[][] L, R;
    long[][] sum;
    int sz;
    public SegmentTreeFractionalCascading(List<Tuple<int, long>>[] A)
    {
        sz = 1;
        while (sz < A.Length) sz <<= 1;
        seg = new Tuple<int, long>[2 * sz - 1][];
        L = new int[2 * sz - 1][];
        R = new int[2 * sz - 1][];
        sum = new long[2 * sz - 1][];
        for (int i = 0; i < sz; i++)
        {
            if (i < A.Length)
            {
                seg[i + sz - 1] = A[i].ToArray();
                sum[i + sz - 1] = new long[seg[i + sz - 1].Length + 1];
                Array.Sort(seg[i + sz - 1], (a, b) => a.Item1 - b.Item1);
                for (int j = 0; j < seg[i + sz - 1].Length; j++)
                    sum[i + sz - 1][j + 1] = sum[i + sz - 1][j] + seg[i + sz - 1][j].Item2;
            }
            else
            {
                seg[i + sz - 1] = new Tuple<int, long>[0];
                sum[i + sz - 1] = new long[1];
            }
        }
        for (int k = sz - 2; k >= 0; k--)
        {
            seg[k] = new Tuple<int, long>[seg[2 * k + 1].Length + seg[2 * k + 2].Length];
            L[k] = new int[seg[k].Length + 1];
            R[k] = new int[seg[k].Length + 1];
            sum[k] = new long[seg[k].Length + 1];
            int t1 = 0, t2 = 0;
            for (int i = 0; i < seg[k].Length; i++)
            {
                L[k][i] = t1;
                R[k][i] = t2;
                if (t1 == seg[2 * k + 1].Length)
                {
                    seg[k][i] = seg[2 * k + 2][t2++];
                }
                else if (t2 == seg[2 * k + 2].Length)
                {
                    seg[k][i] = seg[2 * k + 1][t1++];
                }
                else
                {
                    if (seg[2 * k + 1][t1].Item1 < seg[2 * k + 2][t2].Item1)
                        seg[k][i] = seg[2 * k + 1][t1++];
                    else
                        seg[k][i] = seg[2 * k + 2][t2++];
                }
                sum[k][i + 1] = sum[k][i] + seg[k][i].Item2;
            }
            L[k][seg[k].Length] = seg[2 * k + 1].Length;
            R[k][seg[k].Length] = seg[2 * k + 2].Length;
        }
    }
    public long Query(int l, int r, int d, int u)
    {
        d = LowerBound(seg[0], new Tuple<int, long>(d, -1), (a, b) => a.Item1.CompareTo(b.Item1));
        u = LowerBound(seg[0], new Tuple<int, long>(u, -1), (a, b) => a.Item1.CompareTo(b.Item1));
        return Query(l, r, d, u, 0, 0, sz);
    }
    long Query(int l, int r, int lw, int up, int k, int lft, int rgt)
    {
        if (rgt <= l || r <= lft) return 0;
        if (l <= lft && rgt <= r) return sum[k][up] - sum[k][lw];
        return Query(l, r, L[k][lw], L[k][up], 2 * k + 1, lft, (lft + rgt) >> 1) + Query(l, r, R[k][lw], R[k][up], 2 * k + 2, (lft + rgt) >> 1, rgt);
    }
    #region UpperBound/LowerBound
    public static int UpperBound<T>(IList<T> array, T value, Comparison<T> cmp = null)
    {
        cmp = cmp ?? Comparer<T>.Default.Compare;
        var low = -1;
        var high = array.Count;
        while (high - low > 1)
        {
            var mid = (high + low) / 2;
            if (cmp(array[mid], value) == 1) high = mid;
            else low = mid;
        }
        return high;
    }

    public static int LowerBound<T>(IList<T> array, T value, Comparison<T> cmp = null)
    {
        cmp = cmp ?? Comparer<T>.Default.Compare;
        var low = -1;
        var high = array.Count;
        while (high - low > 1)
        {
            var mid = (high + low) / 2;
            if (cmp(array[mid], value) != -1) high = mid;
            else low = mid;
        }
        return high;
    }
    #endregion
}
