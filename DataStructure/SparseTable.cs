using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class SparseTable<T>
{
    private Func<T, T, T> f;
    private T[][] st;
    private int[] len;
    //[l,r)
    public T this[int i, int j] => f(st[len[j - i]][i], st[len[j - i]][j - (1 << len[j - i])]);
    public SparseTable(IList<T> A, Func<T, T, T> f)
    {
        this.f = f;
        var mask = 0;
        while ((1 << mask) <= A.Count) mask++;
        st = Create(mask, () => new T[1 << mask]);
        for (var i = 0; i < A.Count; i++)
            st[0][i] = A[i];
        for (var i = 1; i < st.Length; i++)
            for (var j = 0; j + (1 << i) <= st[0].Length; j++)
                st[i][j] = f(st[i - 1][j], st[i - 1][j + (1 << (i - 1))]);
        len = new int[A.Count + 1];
        for (var i = 2; i < len.Length; i++)
            len[i] = len[i >> 1] + 1;
    }
}