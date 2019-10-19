using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

namespace Graph
{
    #region 幅優先探索
    public class BreadthFirstSearch
    {
        private Graph<BfsEdge> g;
        public BreadthFirstSearch(int count)
        { g = new Graph<BfsEdge>(count); }
        public void AddEdge(int from, int to)
            => g.Edges[from].Add(new BfsEdge(from, to));
        /// <summary>
        /// O(V+E)
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        public int[] Execute(int[] st)
        {
            var dist = Create(g.Count, () => -1);
            var que = new Queue<int>();
            foreach (var f in st)
            {
                que.Enqueue(f);
                dist[f] = 0;
            }
            while (que.Any())
            {
                var p = que.Dequeue();
                foreach (var e in g.Edges[p])
                    if (dist[e.To] == -1)
                    {
                        dist[e.To] = dist[p] + 1;
                        que.Enqueue(e.To);
                    }
            }
            return dist;
        }

        public class BfsEdge : Edge
        {
            public BfsEdge(int from, int to) : base(from, to)
            { }
        }
    }
    public class BreadthFirstSearch<V> where V : IVertex
    {
        private BreadthFirstSearch g;
        public BreadthFirstSearch(int count)
        { g = new BreadthFirstSearch(count); }
        public void AddEdge(V from, V to)
            => g.AddEdge(from.Id, to.Id);
        public int[] Execute(V[] st)
            => g.Execute(st.Select(a => a.Id).ToArray());
    }
    #endregion
}