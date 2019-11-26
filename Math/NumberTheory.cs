using System;
using System.Collections.Generic;

public class NumberTheory
{
    public static int LCM(int num1, int num2)
        => num1 / GCD(num1, num2) * num2;

    public static long LCM(long num1, long num2)
        => num1 / GCD(num1, num2) * num2;

    public static int GCD(int num1, int num2)
        => num1 < num2 ? GCD(num2, num1) :
           num2 > 0 ? GCD(num2, num1 % num2) : num1;

    public static long GCD(long num1, long num2)
        => num1 < num2 ? GCD(num2, num1) :
           num2 > 0 ? GCD(num2, num1 % num2) : num1;
    /// <summary>
    /// ax+by=gcd(a,b)の整数解を求めます.
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns>gcd(a,b)</returns>
    public static long ExtGCD(long a, long b, out long x, out long y)
    {
        if (b == 0) { x = 1; y = 0; return a; }
        var d = ExtGCD(b, a % b, out y, out x);
        y -= a / b * x;
        return d;
    }

    public static Dictionary<long, int> Factorize(long num)
    {
        var dic = new Dictionary<long, int>();
        for (var i = 2L; i * i <= num; i++)
        {
            var ct = 0;
            while (num % i == 0)
            {
                ct++;
                num /= i;
            }
            if (ct != 0) dic[i] = ct;
        }
        if (num != 1) dic[num] = 1;
        return dic;
    }

    public static bool IsPrime(long num)
    {
        if (num % 2 == 0 || num == 1) return num == 2;
        for (var i = 3; i * i <= num; i += 2)
            if (num % i == 0) return false;
        return true;
    }
}
