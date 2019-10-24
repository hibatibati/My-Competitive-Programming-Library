using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

public class Input
{
    public static string read => Console.ReadLine().Trim();
    public static int[] ar => read.Split(' ').Select(int.Parse).ToArray();
    public static int num => Convert.ToInt32(read);
    public static long[] arL => read.Split(' ').Select(long.Parse).ToArray();
    public static long numL => Convert.ToInt64(read);
    public static char[][] grid(int h)
        => Create(h, () => read.ToCharArray());
    public static int[] ar1D(int n)
        => Create(n, () => num);
    public static long[] arL1D(int n)
        => Create(n, () => numL);
    public static string[] strs(int n)
        => Create(n, () => read);
    public static int[][] ar2D(int n)
        => Create(n, () => ar);
    public static long[][] arL2D(int n)
        => Create(n, () => arL);
    public static List<T>[] edge<T>(int n)
        => Create(n, () => new List<T>());
    public static void Make<T1, T2>(out T1 v1, out T2 v2)
    {
        v1 = Next<T1>();
        v2 = Next<T2>();
    }
    public static void Make<T1, T2, T3>(out T1 v1, out T2 v2, out T3 v3)
    {
        Make(out v1, out v2);
        v3 = Next<T3>();
    }
    public static void Make<T1, T2, T3, T4>(out T1 v1, out T2 v2, out T3 v3, out T4 v4)
    {
        Make(out v1, out v2, out v3);
        v4 = Next<T4>();
    }
    public static void Make<T1, T2, T3, T4, T5>(out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5)
    {
        Make(out v1, out v2, out v3, out v4);
        v5 = Next<T5>();
    }
    public static void Make<T1, T2, T3, T4, T5, T6>(out T1 v1, out T2 v2, out T3 v3, out T4 v4, out T5 v5, out T6 v6)
    {
        Make(out v1, out v2, out v3, out v4, out v5);
        v6 = Next<T6>();
    }
    static Input()
    {
        sc = new Queue<string>();
        dic = new Dictionary<Type, Func<string, object>>();
        dic[typeof(int)] = s => int.Parse(s);
        dic[typeof(long)] = s => long.Parse(s);
        dic[typeof(char)] = s => char.Parse(s);
        dic[typeof(double)] = s => double.Parse(s);
        dic[typeof(uint)] = s => uint.Parse(s);
        dic[typeof(ulong)] = s => ulong.Parse(s);
        dic[typeof(string)] = s => s;
    }
    private static Dictionary<Type, Func<string, object>> dic;
    private static Queue<string> sc;
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static T Next<T>() { if (sc.Count == 0) foreach (var item in read.Split(' ')) sc.Enqueue(item); return (T)dic[typeof(T)](sc.Dequeue()); }
    public const int MOD = 1000000007;
}
