using System;
using System.Collections.Generic;

public class DisjointSparseTable<T>
{
    Func<T, T, T> f;
    T[][] table;
    int[] len;
    T id;
    public T this[int l, int r]
    {
        get { if (r-- <= l) return id; if (l == r) return table[0][l]; return f(table[len[l ^ r]][l], table[len[l ^ r]][r]); }
    }
    public DisjointSparseTable(IList<T> A, Func<T, T, T> f, T id = default(T))
    {
        this.f = f;
        this.id = id;
        var mask = 0;
        while ((1 << mask) <= A.Count) mask++;
        table = Create(mask, () => new T[A.Count]);
        for (int i = 0; i < A.Count; i++)
            table[0][i] = A[i];
        len = new int[1 << mask];
        for (var i = 2; i < len.Length; i++)
            len[i] = len[i >> 1] + 1;
        for (int i = 1; i < mask; i++)
        {
            for (var j = 0; j < A.Count; j += 1 << (i + 1))
            {
                var t = Min(j + (1 << i), A.Count);
                table[i][t - 1] = A[t - 1];
                for (int k = t - 2; k >= j; k--)
                    table[i][k] = f(A[k], table[i][k + 1]);
                if (A.Count == t) break;
                table[i][t] = A[t];
                for (int k = t + 1; k < Min(t + (1 << i), A.Count); k++)
                    table[i][k] = f(table[i][k - 1], A[k]);
            }
        }
    }
}