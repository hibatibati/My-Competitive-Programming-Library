using System;
using System.Collections.Generic;
using System.Linq;

class FastFactorize
{
    //ミラーラビン素数判定法とロー法による素因数分解　モンテカルロ法
    //http://joisino.hatenablog.com/entry/2017/08/03/210000
    //http://miller-rabin.appspot.com/
    //https://mathtrain.jp/rhoalgorithm
    static readonly long[] small = new[] { 2L, 7, 61 };
    static readonly long[] big = new[] { 2L, 325, 9375, 28178, 450775, 9780504, 1795265022 };
    static readonly long[] prime = new[] { 2L, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37 };
    static Random rnd = new Random();
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static long F(long v, long m) => (long)(((BigInteger)v * v + 1) % m);
    private static long GCD(long a, long b) => a < b ? GCD(b, a) : b > 0 ? GCD(b, a % b) : a;
    public static Dictionary<long, int> Factorize(long x)
    {
        var res = new Dictionary<long, int>();
        var st = new List<long> { x };
        while (st.Count > 0)
        {
            var now = st.PopBack();
            if (now == 1) continue;
            if (IsPrime(now))
            {
                if (!res.ContainsKey(now)) res[now] = 0;
                res[now]++;
                continue;
            }
            long f = Find(now);
            st.Add(f);
            st.Add(now / f);
        }
        return res;
    }
    public static bool IsPrime(long x)
    {
        if (x < 2) return false;
        if (prime.Contains(x)) return true;
        var d = x - 1;
        var count2 = 0;
        while ((d & 1) == 0) { d >>= 1; count2++; }
        foreach (var v in (x >> 33) == 0 ? small : big)
        {
            if (v == x) return true;
            var calc = ModPow(v, d, x);
            if (calc == 1) continue;
            var ok = true;
            for (int r = 0; r < count2; r++)
            {
                ok &= calc != x - 1;
                if (!ok) break;
                calc = ModPow(calc, 2, x);
            }
            if (ok) return false;
        }
        return true;
    }
    private static long Find(long x)
    {
        foreach (var p in prime) if (x % p == 0) return p;
        var r = rnd.Next(1, 1001001);
        while (true)
        {
            ++r;
            long a = r % x;
            long b = F(r, x);
            long d = 1;
            while (true)
            {
                d = GCD(Abs(a - b), x);
                if (d >= x) break;
                if (d > 1) return d;
                a = F(a, x);
                b = F(F(b, x), x);
            }
        }
    }
    private static long ModPow(long a, long b, long c)
    {
        BigInteger res = 1, now = a % c;
        while (b != 0)
        {
            if ((b & 1) == 1)
            {
                res = res * now % c;
            }
            now = now * now % c;
            b >>= 1;
        }
        return (long)res;
    }
}