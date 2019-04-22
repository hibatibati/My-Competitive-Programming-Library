using System;

public class Modulo
{
    static readonly int MOD = (int)1e9 + 7;
    private static int Multiple(int num1, int num2)
        => (int)(Math.BigMul(num1, num2) % MOD);

    public static int Pow(int m, int n)
    {
        if (n == 0) return 1;
        if (n % 2 == 0) return Math.Pow(Multiple(m, m), n / 2);
        else return Multiple(Pow(Multiple(m, m), n / 2), m);
    }

    public static int Div(int a, int b)
        => Multiple(a, Pow(b, MOD - 2));

    public class Combination
    {
        private int[] _fac;
        public Combination(int num)
        {
            _fac = new int[num + 1];
            _fac[0] = 1;
            for (var i = 1; i <= num; i++)
                _fac[i] = Multiple(_fac[i - 1], i);
        }
        public int Comb(int n, int r)
        {
            if (n < r) return 0;
            if (n == r) return 1;
            var calc = _fac[n];
            calc = Div(calc, _fac[r]);
            calc = Div(calc, _fac[n - r]);
            return calc;
        }
        public int fac(int num)
            => _fac[num];

    }
}
