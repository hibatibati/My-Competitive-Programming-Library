using System;

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
    public void add(int index, int value)
    {
        for (var i = index; i <= _item.Length; i += i & -i) _item[i] += value;
    }
}
