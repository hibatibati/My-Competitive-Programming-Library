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
    public static Dictionary<T, int> CoordinateComp<T>(IList<T> ar,int d=0)
        where T:IComparable<T>
    {
        var dic = new Dictionary<T, int>();
        foreach (var v in ar.OrderBy(v => v))
            if (!dic.ContainsKey(v))
                dic[v] = d++;
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
        var dic = CoordinateComp(ar, 1);
        var bit = new BIT(ar.Length + 1);
        var res = 0;
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

class BIT
{
    //1-indexed
    int[] _item;
    public BIT(int num)
    {
        _item = new int[num + 1];
    }
    public int this[int index]
    {
        get { var s = 0; for (var i = index; i > 0; i -= i & -i) s += _item[i]; return s; }
    }
    public void add(int index, int value)
    {
        for (var i = index; i < _item.Length; i += i & -i) _item[i] += value;
    }
}

public class Deque<T>
{
    private RingBuffer<T> _buf;
    int _offset = 0, _size;
    public int Count { get; private set; }
    public T PeekHead { get { return _buf[_offset]; } }
    public T PeekTail { get { return _buf[Count + _offset - 1]; } }
    public Deque() { _buf = new RingBuffer<T>(_size = 16); }
    public Deque(int size) { _buf = new RingBuffer<T>(_size = size); }
    public T this[int index]
    {
        get { return _buf[index + _offset]; }
        set { _buf[index + _offset] = value; }
    }
    private void Extend()
    {
        var t = new RingBuffer<T>(_size << 1);
        for (var i = 0; i < _size; i++)
            t[i] = _buf[_offset + i];
        _offset = 0;
        _buf = t;
        _size <<= 1;
    }
    public void EnqueueHead(T item)
    {
        if (Count == _size) Extend();
        _buf[--_offset] = item;
        Count++;
    }
    public T DequeueHead()
    {
        if (Count == 0) return default(T);
        Count--;
        return _buf[_offset++];
    }
    public void EnqueueTail(T item)
    {
        if (Count == _size) Extend();
        _buf[Count++ + _offset] = item;
    }
    public T DequeueTail()
    {
        if (Count == 0) return default(T);
        return _buf[--Count + _offset];
    }
}

class RingBuffer<T>
{
    private T[] _item;
    public int Count { get; private set; }
    public RingBuffer(int size)
    {
        _item = new T[Count = size];
    }
    public T this[int index]
    {
        get { index %= Count; if (index < 0) index += Count; return _item[index]; }
        set { index %= Count; if (index < 0) index += Count; _item[index] = value; }
    }
}

public class Pair<T1, T2> : IComparable<Pair<T1, T2>>
{
    public T1 v1 { get; set; }
    public T2 v2 { get; set; }
    public Pair() { v1 = Input.Next<T1>(); v2 = Input.Next<T2>(); }
    public Pair(T1 v1, T2 v2)
    { this.v1 = v1; this.v2 = v2; }

    public int CompareTo(Pair<T1, T2> p)
    {
        var c = Comparer<T1>.Default.Compare(v1, p.v1);
        if (c == 0)
            c = Comparer<T2>.Default.Compare(v2, p.v2);
        return c;
    }
    public override string ToString()
        => $"{v1.ToString()} {v2.ToString()}";
    public override bool Equals(object obj)
        => this == (Pair<T1, T2>)obj;
    public override int GetHashCode()
        => v1.GetHashCode() ^ v2.GetHashCode();
    public static bool operator ==(Pair<T1, T2> p1, Pair<T1, T2> p2)
        => p1.CompareTo(p2) == 0;
    public static bool operator !=(Pair<T1, T2> p1, Pair<T1, T2> p2)
        => p1.CompareTo(p2) != 0;
    public static bool operator >(Pair<T1, T2> p1, Pair<T1, T2> p2)
        => p1.CompareTo(p2) == 1;
    public static bool operator >=(Pair<T1, T2> p1, Pair<T1, T2> p2)
        => p1.CompareTo(p2) != -1;
    public static bool operator <(Pair<T1, T2> p1, Pair<T1, T2> p2)
        => p1.CompareTo(p2) == -1;
    public static bool operator <=(Pair<T1, T2> p1, Pair<T1, T2> p2)
        => p1.CompareTo(p2) != 1;
}
