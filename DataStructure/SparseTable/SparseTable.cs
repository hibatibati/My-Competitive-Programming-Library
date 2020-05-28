using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class SparseTable<T>
{
    private Func<T, T, T> f;
    private T[][] table;
    private int[] len;
    T id;
    public T this[int l, int r]
    {
        get { if (l >= r) return id; return f(table[len[r - l]][l], table[len[r - l]][r - (1 << len[r - l])]); }
    }
    public SparseTable(IList<T> A, Func<T, T, T> f, T id = default(T))
    {
        this.f = f;
        this.id = id;
        var mask = 0;
        while ((1 << mask) <= A.Count) mask++;
        table = Create(mask, () => new T[A.Count]);
        for (var i = 0; i < A.Count; i++)
            table[0][i] = A[i];
        for (var i = 1; i < table.Length; i++)
            for (var j = 0; j + (1 << i) <= table[i - 1].Length; j++)
                table[i][j] = f(table[i - 1][j], table[i - 1][j + (1 << (i - 1))]);
        len = new int[A.Count + 1];
        for (var i = 2; i < len.Length; i++)
            len[i] = len[i >> 1] + 1;
    }
}