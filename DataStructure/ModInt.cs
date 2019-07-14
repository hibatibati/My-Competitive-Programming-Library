using System;

public struct ModInt
{
    public const long MOD = (int)1e9 + 7;
    //public const long MOD = 998244353;
    public long num { get; set; }
    public ModInt(long n = 0) { num = n; }
    private static ModInt[] _fac;//階乗
    private static ModInt[] _inv;//逆数
    private static ModInt[] _facrev;//1/(i!)
    public override string ToString()
        => num.ToString();
    public static ModInt operator +(ModInt l, ModInt r)
    {
        l.num += r.num;
        if (l.num >= MOD) l.num -= MOD;
        return l;
    }
    public static ModInt operator -(ModInt l, ModInt r)
    {
        l.num -= r.num;
        if (l.num < 0) l.num += MOD;
        return l;
    }
    public static ModInt operator *(ModInt l, ModInt r)
        => new ModInt(l.num * r.num % MOD);
    public static ModInt operator /(ModInt l, ModInt r)
        => l * Pow(r, MOD - 2);
    public static implicit operator long(ModInt l)
        => l.num;
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

    public static void CombBuild(int n)
    {
        _fac = new ModInt[n + 1];
        _facrev = new ModInt[n + 1];
        _inv = new ModInt[n + 1];
        _inv[1] = 1;
        _fac[0] = _fac[1] = 1;
        _facrev[0] = _facrev[1] = 1;
        for (var i = 2; i <= n; i++)
        {
            _fac[i] = _fac[i - 1] * i;
            _inv[i] = MOD - _inv[MOD % i] * (MOD / i);
            _facrev[i] = _facrev[i - 1] * _inv[i];
        }
    }

    public static ModInt Fac(ModInt n)
        => _fac[n];
    public static ModInt Div(ModInt n)
        => _inv[n];
    public static ModInt FacRev(ModInt n)
        => _facrev[n];
    public static ModInt Comb(ModInt n, ModInt r)
    {
        if (n < r) return 0;
        if (n == r) return 1;
        var calc = _fac[n];
        calc = calc * _facrev[r];
        calc = calc * _facrev[n - r];
        return calc;
    }
}