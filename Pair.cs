using System;

public class Pair<T1, T2> : IComparable<Pair<T1, T2>>
{
    public T1 v1;
    public T2 v2;
    public Pair() { }
    public Pair(T1 v1, T2 v2)
    { this.v1 = v1; this.v2 = v2; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(Pair<T1, T2> p)
    {
        var c = Comparer<T1>.Default.Compare(v1, p.v1);
        if (c == 0)
            c = Comparer<T2>.Default.Compare(v2, p.v2);
        return c;
    }
    public override string ToString()
        => $"{v1.ToString()} {v2.ToString()}";
    public void Tie(out T1 a, out T2 b) { a = v1; b = v2; }
}

public class Pair<T1, T2, T3> : Pair<T1, T2>, IComparable<Pair<T1, T2, T3>>
{
    public T3 v3;
    public Pair() : base() { }
    public Pair(T1 v1, T2 v2, T3 v3) : base(v1, v2)
    { this.v3 = v3; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int CompareTo(Pair<T1, T2, T3> p)
    {
        var c = base.CompareTo(p);
        if (c == 0)
            c = Comparer<T3>.Default.Compare(v3, p.v3);
        return c;
    }
    public override string ToString()
        => $"{base.ToString()} {v3.ToString()}";
    public void Tie(out T1 a, out T2 b, out T3 c) { Tie(out a, out b); c = v3; }
}