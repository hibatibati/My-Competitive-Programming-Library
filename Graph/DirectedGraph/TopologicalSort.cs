using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

namespace Graph
{
    #region トポロジカルソート
    public class TopologicalSort
    {
        private Directed<Edge> g;
        private int[] indeg;
        private List<int> list;
        public TopologicalSort(int count)
        { g = new Directed<Edge>(count); indeg = new int[count]; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddEdge(int from, int to)
        {
            g.Edges[from].Add(new Edge(from, to));
            indeg[to]++;
        }
        /// <summary>
        /// O(V+E)
        /// </summary>
        /// <returns></returns>
        public List<int> Execute()
        {
            list = new List<int>(g.Count);
            var q = new Queue<int>();
            for (var i = 0; i < g.Count; i++)
                if (indeg[i] == 0)
                    q.Enqueue(i);
            while (q.Any())
            {
                var p = q.Dequeue();
                list.Add(p);
                foreach (var e in g.Edges[p])
                    if (--indeg[e.To] == 0)
                        q.Enqueue(e.To);
            }
            return list;
        }
    }
    #endregion
}