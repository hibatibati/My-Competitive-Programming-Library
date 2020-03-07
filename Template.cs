using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

public class Template
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool chmin<T>(ref T a, T b) where T : IComparable<T> { if (a.CompareTo(b) == 1) { a = b; return true; } return false; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool chmax<T>(ref T a, T b) where T : IComparable<T> { if (a.CompareTo(b) == -1) { a = b; return true; } return false; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void swap<T>(ref T a, ref T b) { var t = b; b = a; a = t; }
    public static T[] Shuffle<T>(this IList<T> A) { T[] rt = A.ToArray(); Random rnd = new Random(); for (int i = rt.Length - 1; i >= 1; i--) swap(ref rt[i], ref rt[rnd.Next(i + 1)]); return rt; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] Create<T>(int n, Func<T> f) { var rt = new T[n]; for (var i = 0; i < rt.Length; ++i) rt[i] = f(); return rt; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T[] Create<T>(int n, Func<int, T> f) { var rt = new T[n]; for (var i = 0; i < rt.Length; ++i) rt[i] = f(i); return rt; }
    public static void Fail<T>(T s) { Console.WriteLine(s); Console.Out.Close(); Environment.Exit(0); }
}
