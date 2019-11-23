using System;
using System.Collections.Generic;
using System.Linq;
//色々
public class Other
{
    /// <summary>
    /// 座標圧縮をします
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ar"></param>
    /// <param name="d">初期値</param>
    /// <returns></returns>
    public static Dictionary<T, int> Compression<T>(T[] ar, int d = 0, Comparison<T> cmp = null)
    {
        cmp = cmp ?? Comparer<T>.Default.Compare;
        var dic = new Dictionary<T, int>();
        var cp = new T[ar.Length];
        Array.Copy(ar, cp, ar.Length);
        Array.Sort(cp, (a, b) => cmp(a, b));
        for (var i = 0; i < cp.Length; i++)
            if (!dic.ContainsKey(cp[i]))
                dic[cp[i]] = d++;
        return dic;
    }
    /// <summary>
    /// 転倒数を求めます.
    /// 計算量:O(NlogN)
    /// 依存:Binary-Indexed-Tree
    /// </summary>
    /// <param name="ar"></param>
    /// <returns></returns>
    public static long Inversion(int[] ar)
    {
        //座圧
        var dic = Compression(ar, 1);
        var bit = new BIT(ar.Length + 1);
        var res = 0L;
        for (var i = 0; i < ar.Length; i++)
        {
            var t = bit[dic[ar[i]]];
            t = i - t;
            bit.add(dic[ar[i]], 1);
            res += t;
        }
        return res;
    }
    
    /// <summary>
    /// 長さkである区間の各々の最小値を求める。
    /// 計算量:O(N)
    /// 依存:Deque
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="ar"></param>
    /// <param name="k">区間の長さ</param>
    /// <returns>ret[i]:[i,i+k)の最小値</returns>
    public static T[] SlideMin<T>(IList<T> ar,int k)
        where T : IComparable<T>
    {
        var deq = new Deque<int>();
        var ret = new T[ar.Count - k + 1];
        for(var i=0;i<ar.Count;i++)
        {
            while (deq.Count != 0 && ar[deq.PeekTail].CompareTo(ar[i]) != -1)
                deq.DequeueTail();
            deq.EnqueueTail(i);
            if(i-k+1>=0)
            {
                ret[i - k + 1] = ar[deq.PeekHead];
                if (i - k + 1 == deq.PeekHead) deq.DequeueHead();
            }
        }
        return ret;
    }

    /// <summary>
    /// 連長圧縮をします
    /// 依存:Pair
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <returns></returns>
    public static List<Pair<T, int>> RLEncoding<T>(IList<T> list)
        where T : IEquatable<T>
    {
        var ret = new List<Pair<T, int>> { new Pair<T, int>(list[0], 1) };
        for (var i = 1; i < list.Count; i++)
            if (ret.Last().v1.Equals(list[i]))
                ret[ret.Count - 1].v2++;
            else ret.Add(new Pair<T, int>(list[i], 1));
        return ret;
    }
}