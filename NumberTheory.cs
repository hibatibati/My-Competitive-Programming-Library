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

    public static List<int> PrimeList(long num)
    {
        if (num < 2) return new List<int>();
        var prime = new List<int> { 2 };
        var bo = new bool[num + 1];
        for (var i = 3; i <= num; i += 2)
            if (!bo[i])
            {
                prime.Add(i);
                for (var j = 3 * i; j <= num; j += 2 * i)
                    bo[j] = true;
            }
        return prime;
    }

    public static Dictionary<long, int> Factorize(long num)
    {
        var dic = new Dictionary<long, int>();
        for (var i = 2; i * i <= num; i++)
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
