using System;
using System.Collections.Generic;
//O(N)構築,O(1)クエリ(ただし、msbの計算量を無視している)
class LinearRMQ
{
    const int SIZE = 16;
    int[] A, small;
    private int[][] table;
    //[l,r]
    public int this[int l, int r]
    {
        get
        {
            var res = int.MaxValue;
            var lft = (l >> 4) + 1;
            var rgt = r >> 4;
            if (lft <= rgt)
            {
                if (lft < rgt)
                {
                    var p = MSB(rgt - lft);
                    res = Min(table[p][lft], table[p][rgt - (1 << p)]);
                }
                var m = small[(lft << 4) - 1] & ((~0) << (l & (SIZE - 1)));
                res = Min(res, A[CTZ(m) + ((lft - 1) << 4)]);
                res = Min(res, A[CTZ(small[r]) + (rgt << 4)]);
            }
            else
            {
                var m = small[r] & ((~0) << (l & (SIZE - 1)));
                res = A[(rgt << 4) + CTZ(m)];
            }
            return res;
        }
    }
    public LinearRMQ(int[] A)
    {
        this.A = A;
        var large = new int[A.Length >> 4];
        small = new int[A.Length];
        var st = new Stack<int>();
        var min = int.MaxValue;
        for (int i = 0; i < A.Length; i++)
        {
            min = Min(min, A[i]);
            while (st.Any() && A[st.Peek()] > A[i]) st.Pop();
            if (st.Any()) small[i] = small[st.Peek()];
            small[i] |= 1 << (i & (SIZE - 1));
            st.Push(i);
            if (((i + 1) & (SIZE - 1)) == 0)
            {
                large[i >> 4] = min;
                min = int.MaxValue;
                st.Clear();
            }
        }
        var mask = 0;
        while ((1 << mask) <= large.Length) mask++;
        table = Enumerable.Repeat(0, mask).Select(_ => new int[large.Length]).ToArray();
        for (var i = 0; i < large.Length; i++)
            table[0][i] = large[i];
        for (var i = 1; i < table.Length; i++)
            for (var j = 0; j + (1 << i) <= table[i - 1].Length; j++)
                table[i][j] = Min(table[i - 1][j], table[i - 1][j + (1 << (i - 1))]);
    }
    int CTZ(int v) => MSB(v & -v);
    int MSB(int v)
    {
        var rt = 0;
        if ((v >> 8) > 0) { rt |= 8; v >>= 8; }
        if ((v >> 4) > 0) { rt |= 4; v >>= 4; }
        if ((v >> 2) > 0) { rt |= 2; v >>= 2; }
        return rt | (v >> 1);
    }
}