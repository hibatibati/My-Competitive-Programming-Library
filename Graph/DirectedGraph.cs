using System;
using System.Collections.Generic;
using System.Linq;

public class DirectedGraph
{
    private List<int>[] adj, rev;
    private List<int> topoList;
    private Stack<int> st;
    private int[] scc;
    private bool[] th;
    public int Count { get; }
    public DirectedGraph(int num)
    {
        this.Count = num;
        adj = Enumerable.Repeat(0, Count).Select(_ => new List<int>()).ToArray();
        rev = Enumerable.Repeat(0, Count).Select(_ => new List<int>()).ToArray();
    }
    /// <summary>
    ///有向辺(u,v)を追加します
    /// </summary>
    /// <param name="u"></param>
    /// <param name="v"></param>
    /// <param name="logRev">逆辺を記録するか</param>
    public void AddEdge(int u, int v, bool logRev = true)
    {
        adj[u].Add(v);
        if (logRev)
            rev[v].Add(u);
    }
    /// <summary>
    /// DAGであるか（閉路を持たないか)を判定します
    /// </summary>
    /// <returns></returns>
    public bool IsDAG()
    {
        topoList = topoList ?? TopologicalSort();
        return topoList.Count == Count;
    }
    /// <summary>
    /// トポロジカルソートをします
    /// 計算量:O(V+E)
    /// </summary>
    /// <returns></returns>
    public List<int> TopologicalSort()
    {
        topoList = new List<int>(Count);
        var ct = new int[Count];
        st = new Stack<int>();
        for (var i = 0; i < Count; i++)
            if (rev[i].Count == 0)
                st.Push(i);
        while (st.Any())
        {
            var index = st.Pop();
            topoList.Add(index);
            foreach (var ad in adj[index])
            {
                ct[ad]++;
                if (ct[ad] == rev[ad].Count) st.Push(ad);
            }
        }
        return topoList;
    }
    /// <summary>
    /// 強連結成分分解をします
    /// 計算量:O(V+E)
    /// </summary>
    /// <param name="ct">連結成分の総数</param>
    /// <returns></returns>
    public int[] StronglyConnectedComponents(out int ct)
    {
        th = new bool[Count];
        st = new Stack<int>();
        for (var i = 0; i < Count; i++)
            if (!th[i])
                SCCdfs(i);
        scc = Enumerable.Repeat(-1, Count).ToArray();
        ct = 0;
        while (st.Any())
        {
            SCCrdfs(st.Pop(), ct++);
            while (st.Any() && scc[st.Peek()] != -1)
                st.Pop();
        }
        return scc;
    }
    private void SCCdfs(int index)
    {
        th[index] = true;
        foreach (var ad in adj[index])
            if (!th[ad])
                SCCdfs(ad);
        st.Push(index);
    }
    private void SCCrdfs(int index, int ct)
    {
        scc[index] = ct;
        foreach (var ad in rev[index])
            if (scc[ad] == -1)
                SCCrdfs(ad, ct);
    }
}
