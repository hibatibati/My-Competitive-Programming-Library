using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

namespace Graph
{
    #region 強連結成分分解
    public class StronglyConnectedComponents
    {
        private Directed<Edge> g;
        private Stack<int> st;
        private List<List<int>> scc;
        private bool[] use;
        public StronglyConnectedComponents(int count)
        { g = new Directed<Edge>(count); }
        public void AddEdge(int from, int to)
        {
            var e = new Edge(from, to);
            g.Edges[from].Add(e);
            g.Edges[to].Add(e);
        }
        /// <summary>
        /// O(V+E)
        /// </summary>
        /// <returns>scc[i]:i番目の強連結成分の頂点</returns>
        public List<List<int>> Execute()
        {
            scc = new List<List<int>>();
            use = new bool[g.Count];
            st = new Stack<int>();
            for (var i = 0; i < g.Count; i++)
                if (!use[i])
                    dfs1(i);
            use = new bool[g.Count];
            while (st.Any())
            {
                scc.Add(new List<int>());
                dfs2(st.Pop());
                while (st.Any() && use[st.Peek()])
                    st.Pop();
            }
            return scc;
        }
        private void dfs1(int index)
        {
            use[index] = true;
            foreach (var e in g.Edges[index])
                if (index != e.To && !use[e.To])
                    dfs1(e.To);
            st.Push(index);
        }
        private void dfs2(int index)
        {
            use[index] = true;
            scc[scc.Count - 1].Add(index);
            foreach (var e in g.Edges[index])
                if (index != e.From && !use[e.From])
                    dfs2(e.From);
        }
    }
    #endregion
}
