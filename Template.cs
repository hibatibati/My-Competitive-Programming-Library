using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

public class Template
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool chmin<T>(ref T num, T val) where T : IComparable<T>
    { if (num.CompareTo(val) == 1) { num = val; return true; } return false; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool chmax<T>(ref T num, T val) where T : IComparable<T>
    { if (num.CompareTo(val) == -1) { num = val; return true; } return false; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void swap<T>(ref T v1, ref T v2)
    { var t = v2; v2 = v1; v1 = t; }
    public static void Fail() => Fail("No");
    public static void Fail<T>(T s) { WriteLine(s); Console.Out.Close(); Environment.Exit(0); }
}