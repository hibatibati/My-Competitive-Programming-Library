using System;

public struct ModInt
{
    public const long MOD = (int)1e9 + 7;
    //public const long MOD = 998244353;
    public long value { get; set; }
    public ModInt(long n = 0) { value = n; }
    private static ModInt[] fac;//階乗
    private static ModInt[] inv;//逆数
    private static ModInt[] facinv;//1/(i!)
    public override string ToString()
        => value.ToString();
    public static ModInt operator +(ModInt l, ModInt r)
    {
        l.value += r.value;
        if (l.value >= MOD) l.value -= MOD;
        return l;
    }
    public static ModInt operator -(ModInt l, ModInt r)
    {
        l.value -= r.value;
        if (l.value < 0) l.value += MOD;
        return l;
    }
    public static ModInt operator *(ModInt l, ModInt r)
        => new ModInt(l.value * r.value % MOD);
    public static ModInt operator /(ModInt l, ModInt r)
        => l * Pow(r, MOD - 2);
    public static implicit operator long(ModInt l)
        => l.value;
    public static implicit operator ModInt(long n)
    {
        n %= MOD; if (n < 0) n += MOD;
        return new ModInt(n);
    }

    public static ModInt Pow(ModInt m, long n)
    {
        if (n == 0) return 1;
        if (n % 2 == 0) return Pow(m * m, n >> 1);
        else return Pow(m * m, n >> 1) * m;
    }

    public static void Build(int n)
    {
        fac = new ModInt[n + 1];
        facinv = new ModInt[n + 1];
        inv = new ModInt[n + 1];
        inv[1] = 1;
        fac[0] = fac[1] = 1;
        facinv[0] = facinv[1] = 1;
        for (var i = 2; i <= n; i++)
        {
            fac[i] = fac[i - 1] * i;
            inv[i] = MOD - inv[MOD % i] * (MOD / i);
            facinv[i] = facinv[i - 1] * inv[i];
        }
    }

    public static ModInt Fac(ModInt n)
        => fac[n];
    public static ModInt Inv(ModInt n)
        => inv[n];
    public static ModInt FacInv(ModInt n)
        => facinv[n];
    public static ModInt Comb(ModInt n, ModInt r)
    {
        if (n < r) return 0;
        if (n == r) return 1;
        var calc = fac[n];
        calc = calc * facinv[r];
        calc = calc * facinv[n - r];
        return calc;
    }
}