using System;
using System.Collections.Generic;
using System.Linq;
//グラフその他
public class GraphLib
{
    //二部グラフ判定
    public static bool IsBipartite(IList<IEnumerable<int>> edge, int index, int[] color, int c)
    {
        if (color[index] != 0) return color[index] == c;
        color[index] = c;
        var t = true;
        foreach (var ad in edge[index])
            t &= IsBipartite(edge, ad, color, 3 - c);
        return t;
    }
}
