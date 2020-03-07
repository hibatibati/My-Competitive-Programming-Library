using System;
using System.Collections.Generic;
using System.Linq;

public static List<_RLEncoding<T>> RLEncoding<T>(IList<T> list, Comparison<T> cmp = null)
{
    if (list.Count == 0) throw new Exception("list is empty");
    cmp = cmp ?? Comparer<T>.Default.Compare;
    var rt = new List<_RLEncoding<T>> { new _RLEncoding<T>(list[0], 1) };
    for (var i = 1; i < list.Count; i++)
        if (cmp(rt.Last().dat, list[i]) == 0)
            rt[rt.Count - 1].cnt++;
        else rt.Add(new _RLEncoding<T>(list[i], 1));
    return rt;
}
public class _RLEncoding<T> { public T dat; public int cnt; public _RLEncoding(T d, int c) { dat = d; cnt = c; } }

