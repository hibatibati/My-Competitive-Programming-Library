using System;
using System.Collections.Generic;
using System.Linq;
//色々
public class Other
{
    /// <summary>
    /// 転倒数を求めます.
    /// 計算量:O(NlogN)
    /// 依存:Binary-Indexed-Tree
    /// </summary>
    /// <param name="ar"></param>
    /// <returns></returns>
    public static long Inversion(int[] ar)
    {
        var dic = new Dictionary<int, int>();
        var d = 0;
        //座圧
        foreach (var v in ar.OrderBy(v => v))
            if (!dic.ContainsKey(v))
                dic[v] = ++d;
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
        var deq = new Deque<T>();
        var ret = new T[ar.Count - k];
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
