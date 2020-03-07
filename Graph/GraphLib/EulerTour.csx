using System;
using System.Collections.Generic;
using System.Linq;
//部分木クエリのための非再帰オイラーツアー
int[] l, r;
public void EulerTour(List<int>[] edge, int root = 0)
{
    var st = new Stack<int>();
    st.Push(root);
    var idx = 0;
    l = Create(edge.Length, () => -1); r = new int[edge.Length];
    while (st.Any())
    {
        var i = st.Pop();
        if (l[i] != -1)
        {
            r[i] = idx++;
            continue;
        }
        st.Push(i);
        l[i] = idx++;
        foreach (var e in edge[i])
            if (l[e] == -1)
                st.Push(e);
    }
}
