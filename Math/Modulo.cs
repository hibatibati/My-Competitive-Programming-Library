using System;

public class Modulo
{
    private static readonly int MOD = (int)1e9 + 7;
    //private static readonly int MOD =998244353;
    private static int Multiple(int num1, int num2)
        => (int)(BigMul(num1, num2) % MOD);

    public static int Pow(int m, int n)
    {
        if (n == 0) return 1;
        if (n % 2 == 0) return Pow(Multiple(m, m), n >> 1);
        else return Multiple(Pow(Multiple(m, m), n >> 1), m);
    }

    public static int Div(int a, int b)
        => Multiple(a, Pow(b, MOD - 2));

    public class Combination
    {
        private int[] _fac;//階乗
        private int[] _inv;//逆数
        private int[] _facrev;//1/(i!)
        public Combination(int num)
        {
            _fac = new int[num + 1];
            _facrev = new int[num + 1];
            _inv = new int[num + 1];
            _inv[1] = 1;
            _fac[0] = _fac[1] = 1;
            _facrev[0] = _facrev[1] = 1;
            for (var i = 2; i <= num; i++)
            {
                _fac[i] = Multiple(_fac[i - 1], i);
                _inv[i] = MOD - Multiple(_inv[MOD % i], MOD / i);
                _facrev[i] = Multiple(_facrev[i - 1], _inv[i]);
            }
        }
        public int Comb(int n, int r)
        {
            if (n < r) return 0;
            if (n == r) return 1;
            var calc = _fac[n];
            calc = Multiple(calc, _facrev[r]);
            calc = Multiple(calc, _facrev[n - r]);
            return calc;
        }
        public int fac(int num)
            => _fac[num];
        public int facrev(int num)
            => _facrev[num];
        public int inv(int num)
            => _inv[num];
    }
}
