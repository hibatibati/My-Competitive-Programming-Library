using System;
using System.Collections.Generic;

public static Dictionary<T, int> Compression<T>(IList<T> A, int d = 0, Comparison<T> cmp = null)
{
    cmp = cmp ?? Comparer<T>.Default.Compare;
    var dic = new Dictionary<T, int>();
    var cp = A.ToArray();
    Array.Sort(cp, (a, b) => cmp(a, b));
    for (var i = 0; i < cp.Length; i++)
        if (!dic.ContainsKey(cp[i]))
            dic[cp[i]] = d++;
    return dic;
}