using System;
using System.Collections.Generic;
using System.Linq;

public static List<_RLEncoding<T>> RLEncoding<T>(IList<T> list, Comparison<T> cmp = null)
{
    cmp = cmp ?? Comparer<T>.Default.Compare;
    var rt = new List<_RLEncoding<T>>();
    var idx = 0;
    for(int i = 0; i < list.Count; i = idx)
    {
        while (idx < list.Count && cmp(list[idx], list[i]) == 0) idx++;
        rt.Add(new _RLEncoding<T>(list[i], idx - i));
    }
    return rt;
}
public class _RLEncoding<T> { public T dat; public int cnt; public _RLEncoding(T d, int c) { dat = d; cnt = c; } }

