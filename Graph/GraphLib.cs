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
    /// 依存:Pair
    /// </summary>
    public class LowLink
    {
        private static List<int> arti;
        private static int[] ord, low;
        private static List<Pair<int, int>> bridge;
        private static bool[] th;
        private static IList<IEnumerable<int>> adj;
        /// <summary>
        /// 関節点（削除するとグラフの連結成分数が増加する頂点）を求めます
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        public static List<int> Articulation(IList<IEnumerable<int>> edge)
        {
            adj = edge;
            arti = new List<int>();
            ord = new int[edge.Count];
            low = new int[edge.Count];
            bridge = new List<Pair<int, int>>();
            th = new bool[edge.Count];
            var ct = 0;
            for (var i = 0; i < edge.Count; i++)
                if (!th[i]) ct = dfs(i, ct, -1);
            return arti;
        }
        /// <summary>
        /// 橋（削除するとグラフの連結成分数が増加する辺）を求めます
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        public static List<Pair<int, int>> Bridge(IList<IEnumerable<int>> edge)
        {
            Articulation(edge);
            return bridge;
        }
        private static int dfs(int index, int ct, int par)
        {
            th[index] = true;
            ord[index] = ct;
            low[index] = ct++;
            var isArti = false;
            var c = 0;
            foreach (var ad in adj[index])
                if (!th[ad])
                {
                    c++;
                    ct = dfs(ad, ct, index);
                    low[index] = Min(low[index], low[ad]);
                    isArti |= par != -1 && low[ad] >= ord[index];
                    if (ord[index] < low[ad])
                        bridge.Add(new Pair<int, int>(Min(index, ad), Max(index, ad)));
                }
                else if (ad != par)
                    low[index] = Min(low[index], ord[ad]);
            isArti |= par == -1 && c > 1;
            if (isArti) arti.Add(index);
            return ct;
        }
    }
}


public class Pair<T1, T2> : IComparable<Pair<T1, T2>>
{
    public T1 v1 { get; set; }
    public T2 v2 { get; set; }
    public Pair() { v1 = Input.Next<T1>(); v2 = Input.Next<T2>(); }
    public Pair(T1 v1, T2 v2)
    { this.v1 = v1; this.v2 = v2; }

    public int CompareTo(Pair<T1, T2> p)
    {
        var c = Comparer<T1>.Default.Compare(v1, p.v1);
        if (c == 0)
            c = Comparer<T2>.Default.Compare(v2, p.v2);
        return c;
    }
    public override string ToString()
        => $"{v1.ToString()} {v2.ToString()}";
    public override bool Equals(object obj)
        => this == (Pair<T1, T2>)obj;
    public override int GetHashCode()
        => v1.GetHashCode() ^ v2.GetHashCode();
    public static bool operator ==(Pair<T1, T2> p1, Pair<T1, T2> p2)
        => p1.CompareTo(p2) == 0;
    public static bool operator !=(Pair<T1, T2> p1, Pair<T1, T2> p2)
        => p1.CompareTo(p2) != 0;
    public static bool operator >(Pair<T1, T2> p1, Pair<T1, T2> p2)
        => p1.CompareTo(p2) == 1;
    public static bool operator >=(Pair<T1, T2> p1, Pair<T1, T2> p2)
        => p1.CompareTo(p2) != -1;
    public static bool operator <(Pair<T1, T2> p1, Pair<T1, T2> p2)
        => p1.CompareTo(p2) == -1;
    public static bool operator <=(Pair<T1, T2> p1, Pair<T1, T2> p2)
        => p1.CompareTo(p2) != 1;
}