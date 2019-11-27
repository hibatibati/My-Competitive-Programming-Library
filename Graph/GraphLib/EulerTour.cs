using System;
using System.Collections.Generic;

//部分木クエリのためのオイラーツアー
int[] l, r;
public void EulerTour(List<int>[] edge, int root = 0)
{
    var st = new Stack<int>();
    st.Push(root);
    var idx = 0;
    l = new int[edge.Length]; r = new int[edge.Length];
    var use = new bool[edge.Length];
    while (st.Any())
    {
        var i = st.Pop();
        if (use[i])
        {
            r[i] = idx++;
            continue;
        }
        st.Push(i);
        use[i] = true;
        l[i] = idx++;
        foreach (var e in edge[i])
            if (!use[e])
            {
                st.Push(e);
            }
    }
}
