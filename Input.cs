using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

public class Scanner
{
    public string Str => ReadLine().Trim();
    public int Int => int.Parse(Str);
    public long Long => long.Parse(Str);
    public double Double => double.Parse(Str);
    public int[] ArrInt => Str.Split(' ').Select(int.Parse).ToArray();
    public long[] ArrLong => Str.Split(' ').Select(long.Parse).ToArray();
    public char[][] Grid(int n) => Create(n, () => Str.ToCharArray());
    public int[] ArrInt1D(int n) => Create(n, () => Int);
    public long[] ArrLong1D(int n) => Create(n, () => Long);
    public int[][] ArrInt2D(int n) => Create(n, () => ArrInt);
    public long[][] ArrLong2D(int n) => Create(n, () => ArrLong);
    public Pair<T1, T2> PairMake<T1, T2>() => new Pair<T1, T2>(Next<T1>(), Next<T2>());
    public Pair<T1, T2, T3> PairMake<T1, T2, T3>() => new Pair<T1, T2, T3>(Next<T1>(), Next<T2>(), Next<T3>());
    private Queue<string> q = new Queue<string>();
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T Next<T>() { if (q.Count == 0) foreach (var item in Str.Split(' ')) q.Enqueue(item); return (T)Convert.ChangeType(q.Dequeue(), typeof(T)); }
    public void Make<T1>(out T1 v1) => v1 = Next<T1>();
    public void Make<T1, T2>(out T1 v1, out T2 v2) { v1 = Next<T1>(); v2 = Next<T2>(); }
    public void Make<T1, T2, T3>(out T1 v1, out T2 v2, out T3 v3) { Make(out v1, out v2); v3 = Next<T3>(); }
    public void Make<T1, T2, T3, T4>(out T1 v1, out T2 v2, out T3 v3, out T4 v4) { Make(out v1, out v2, out v3); v4 = Next<T4>(); }
    public void Make<T1, T2, T3, T4, T5>(out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5) { Make(out v1, out v2, out v3, out v4); v5 = Next<T5>(); }
    public void Make<T1, T2, T3, T4, T5, T6>(out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5, out T6 v6) { Make(out v1, out v2, out v3, out v4, out v5); v6 = Next<T6>(); }
}