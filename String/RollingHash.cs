using System;

public class RollingHash
{
    public string str { get; }
    private long[][] hashTable;
    private long[][] pow;
    public static int[] bases { get; }
    public static int BaseCount { get { return bases.Length; } }
    private const int MOD = (int)1e9 + 7;
    public RollingHash(string str)
    {
        this.str = str;
        hashTable = Enumerable.Repeat(0, BaseCount).Select(_ => new long[str.Length + 1]).ToArray();
        pow = Enumerable.Repeat(0, BaseCount).Select(_ => new long[str.Length + 1]).ToArray();
        for (var i = 0; i < BaseCount; i++)
            pow[i][0] = 1;
        for (var i = 0; i < BaseCount; i++)
            for (var j = 1; j <= str.Length; j++)
            {
                hashTable[i][j] = (hashTable[i][j - 1] + str[j - 1]) * bases[i] % MOD;
                pow[i][j] = pow[i][j - 1] * bases[i] % MOD;
            }
    }
    static RollingHash() { bases = new[] { 1009, 1007, 1229 }; }
    /// <summary>
    /// 与えられた文字列の[l,r)での部分文字列のハッシュ値を各baseごとに計算します
    /// 計算量:O(|s||Bases|)
    /// </summary>
    /// <param name="s"></param>
    /// <param name="l"></param>
    /// <param name="r"></param>
    /// <returns></returns>
    public static long[] GetHash(string s, int l = 0, int r = -1)
    {
        if (r < 0) r = s.Length;
        var h = new long[BaseCount];
        for (var i = 0; i < h.Length; i++)
            for (var j = l; j < r; j++)
                h[i] = (h[i] + s[j]) * bases[i] % MOD;
        return h;
    }
    /// <summary>
    /// [l,r)での部分文字列のハッシュ値を各baseごとに計算します
    /// 計算量:O(|Bases|)
    /// </summary>
    /// <param name="l"></param>
    /// <param name="r"></param>
    /// <returns></returns>
    public long[] GetHash(int l = 0, int r = -1)
    {
        if (r < 0) r = str.Length;
        var h = new long[BaseCount];
        for (var i = 0; i < BaseCount; i++)
            h[i] = ((hashTable[i][r] - hashTable[i][l] * pow[i][r - l]) % MOD + MOD) % MOD;
        return h;

    }

}   