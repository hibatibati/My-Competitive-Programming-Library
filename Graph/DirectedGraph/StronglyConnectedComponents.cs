using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

public class StronglyConnectedComponents
{
    private List<int>[] g, rev;
    private Stack<int> st;
    private List<List<int>> scc;
    private int[] group;
    private bool[] use;
    public int Count { get; private set; }
    public int this[int i] { get { return group[i]; } }
    public List<int> KthGroup(int k) => scc[k];
    public StronglyConnectedComponents(int count)
    {
        g = Create(count, () => new List<int>());
        rev = Create(count, () => new List<int>());
        group = new int[count];
    }
    public void AddEdge(int from, int to)
    {
        g[from].Add(to);
        rev[to].Add(from);
    }
    /// <summary>
    /// O(V+E)
    /// </summary>
    /// <returns>scc[i]:i番目の強連結成分の頂点</returns>
    public List<List<int>> Execute()
    {
        scc = new List<List<int>>();
        use = new bool[g.Length];
        st = new Stack<int>();
        for (var i = 0; i < g.Length; i++)
            if (!use[i])
                dfs1(i);
        use = new bool[g.Length];
        while (st.Any())
        {
            scc.Add(new List<int>());
            Count++;
            dfs2(st.Pop());
            while (st.Any() && use[st.Peek()])
                st.Pop();
        }
        return scc;
    }
    private void dfs1(int index)
    {
        use[index] = true;
        foreach (var e in g[index])
            if (!use[e])
                dfs1(e);
        st.Push(index);
    }
    private void dfs2(int index)
    {
        group[index] = Count;
        use[index] = true;
        scc[scc.Count - 1].Add(index);
        foreach (var e in rev[index])
            if (!use[e])
                dfs2(e);
    }
}