using System;

/// <summary>
/// 依存:BIT,Compression(座圧)
/// </summary>
/// <param name="A"></param>
/// <returns></returns>
public static long InversionNumber(int[] A)
{
    var dic = Compression(A, 1);
    var bit = new BIT(A.Length + 1);
    var res = 0L;
    for (var i = 0; i < A.Length; i++)
    {
        var t = bit[dic[A[i]]];
        t = i - t;
        bit.add(dic[A[i]], 1);
        res += t;
    }
    return res;
}