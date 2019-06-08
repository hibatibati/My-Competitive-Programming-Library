using System;
using System.Collections.Generic;
using System.Linq;
//書き留めて置きたいが分別できないコード群
public class Other
{
    //転倒数
    public static long Inversion(int[] ar)
    {
        var dic = new Dictionary<int, int>();
        var d = 0;
        //座圧
        foreach (var v in ar.OrderBy(v => v))
            if (!dic.ContainsKey(v))
                dic[v] = ++d;
        var bit = new BIT(num + 1); var res = 0;
        for (var i = 0; i < num; i++)
        {
            var t = bit[dic[ar[i]]];
            t = i - t;
            bit.add(dic[ar[i]], 1);
            res += t;
        }
    }
}

public class BIT
{
    int[] _item;
    public BIT(int num)
    {
        _item = new int[num + 1];
    }
    public int this[int index]
    {
        get { var s = 0; for (var i = index; i > 0; i -= i & -i) s += _item[i]; return s; }
    }
    //0を与えると破滅します
    public void add(int index, int value)
    {
        for (var i = index; i < _item.Length; i += i & -i) _item[i] += value;
    }
}
