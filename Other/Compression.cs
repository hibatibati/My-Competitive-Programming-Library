using System;
using System.Collections.Generic;
using System.Linq;

public class Compression<T>
{
    private List<T> dat;
    public int Count => dat.Count;
    Dictionary<T, int> dic;
    public T this[int t] => dat[t];
    Comparison<T> cmp;
    public Compression(int f = 0, Comparison<T> cmp = null)
    {
        this.cmp = cmp ?? Comparer<T>.Default.Compare;
        dat = new List<T>();
        dic = new Dictionary<T, int>();
    }
    public void Add(T t) => dat.Add(t);
    public void Add(IList<T> d) => dat.AddRange(d);
    public void Build()
    {
        var rt = new List<T>();
        dat.Sort(cmp);
        for (int i = 0; i < dat.Count;)
        {
            int idx = i;
            while (idx < dat.Count && cmp(dat[idx], dat[i]) == 0) idx++;
            rt.Add(dat[i]);
            i = idx;
        }
        dat = rt;
        for (int i = 0; i < dat.Count; i++)
            dic[dat[i]] = i;
    }
    public int Get(T t) => dic[t];
}