using System;
using System.Collections.Generic;
using System.Linq;
//グラフその他
public class GraphLib
{
    /// <summary>
    /// 二部グラフ判定
    /// </summary>
    /// <param name="edge"></param>
    /// <param name="index"></param>
    /// <param name="color"></param>
    /// <param name="c"></param>
    /// <returns></returns>
    public static bool IsBipartite(IList<IEnumerable<int>> edge, int index, int[] color, int c=1)
    {
        if (color[index] != 0) return color[index] == c;
        color[index] = c;
        var t = true;
        foreach (var ad in edge[index])
            t &= IsBipartite(edge, ad, color, 3 - c);
        return t;
    }
    /// <summary>
    /// 有向グラフに対して、その転置グラフを求めます
    /// </summary>
    /// <param name="adj"></param>
    /// <returns></returns>
    public static List<int>[] Reverse(IList<IEnumerable<int>> adj)
    {
        var list = Enumerable.Repeat(0, adj.Count).Select(_ => new List<int>()).ToArray();
        for (var i = 0; i < adj.Count; i++)
            foreach (var ad in adj[i])
                list[ad].Add(i);
        return list;
    }
    /// <summary>
    /// DAGを順番を守ったままリストに変換します
    /// </summary>
    /// <param name="adj"></param>
    /// <returns></returns>
    public static List<int> TopologicalSort(IList<IEnumerable<int>> adj)
    {
        var res = new List<int>(adj.Count);
        var th = new bool[adj.Count];
        for (var i = 0; i < adj.Count; i++)
            if (!th[i])
                Topodfs(adj, i, th, res);
        res.Reverse();
        return res;
    }
    private static void Topodfs(IList<IEnumerable<int>> adj, int index, bool[] th, List<int> res)
    {
        th[index] = true;
        foreach (var ad in adj[index])
            if (!th[ad])
                Topodfs(adj, ad, th, res);
        res.Add(index);
    }
    /// <summary>
    /// 各頂点がどの強連結成分に属しているかを求めます
    /// </summary>
    /// <param name="adj"></param>
    /// <param name="ct">成分の総数</param>
    /// <returns></returns>
    public static int[] SCC(IList<IEnumerable<int>> adj, out int ct)
    {
        var rev = Reverse(adj);
        var th = new bool[adj.Count];
        var st = new Stack<int>();
        for (var i = 0; i < adj.Count; i++)
            if (!th[i])
                SCCdfs(adj, i, st, th);
        var res = Enumerable.Repeat(-1, adj.Count).ToArray();
        ct = 0;
        while (st.Any())
        {
            SCCrdfs(rev, st.Pop(), res, ct++);
            while (st.Any() && res[st.Peek()] != -1)
                st.Pop();
        }
        return res;
    }
    private static void SCCdfs(IList<IEnumerable<int>> adj, int index, Stack<int> st, bool[] th)
    {
        th[index] = true;
        foreach (var ad in adj[index])
            if (!th[ad])
                SCCdfs(adj, ad, st, th);
        st.Push(index);
    }
    private static void SCCrdfs(IList<IEnumerable<int>> rev, int index, int[] res, int ct)
    {
        res[index] = ct;
        foreach (var ad in rev[index])
            if (res[ad] == -1)
                SCCrdfs(rev, ad, res, ct);
    }
}
