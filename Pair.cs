﻿using System;

public class Pair<T1, T2> : IComparable<Pair<T1, T2>>
{
    public T1 v1 { get; set; }
    public T2 v2 { get; set; }
    public Pair() {
        //v1 = Input.Next<T1>(); v2 = Input.Next<T2>();
    }
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
        => base.GetHashCode();
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

public class Pair<T1, T2, T3> : Pair<T1, T2>, IComparable<Pair<T1, T2, T3>>
{
    public T3 v3 { get; set; }
    public Pair() : base() {
        //v3 = Input.Next<T3>(); 
    }
    public Pair(T1 v1, T2 v2, T3 v3) : base(v1, v2)
    { this.v3 = v3; }

    public int CompareTo(Pair<T1, T2, T3> p)
    {
        var c = base.CompareTo(p);
        if (c == 0)
            c = Comparer<T3>.Default.Compare(v3, p.v3);
        return c;
    }
    public override string ToString()
        => $"{base.ToString()} {v3.ToString()}";
    public override bool Equals(object obj)
       => this == (Pair<T1, T2, T3>)obj;
    public override int GetHashCode()
        => base.GetHashCode();
    public static bool operator ==(Pair<T1, T2, T3> p1, Pair<T1, T2, T3> p2)
        => p1.CompareTo(p2) == 0;
    public static bool operator !=(Pair<T1, T2, T3> p1, Pair<T1, T2, T3> p2)
        => p1.CompareTo(p2) != 0;
    public static bool operator >(Pair<T1, T2, T3> p1, Pair<T1, T2, T3> p2)
        => p1.CompareTo(p2) == 1;
    public static bool operator >=(Pair<T1, T2, T3> p1, Pair<T1, T2, T3> p2)
        => p1.CompareTo(p2) != -1;
    public static bool operator <(Pair<T1, T2, T3> p1, Pair<T1, T2, T3> p2)
        => p1.CompareTo(p2) == -1;
    public static bool operator <=(Pair<T1, T2, T3> p1, Pair<T1, T2, T3> p2)
        => p1.CompareTo(p2) != 1;
}