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
        public int[] Execute(int st = 0)
        {
            var dist = Create(g.Count, () => -1);
            var que = new Queue<int>();
            que.Enqueue(st);
            dist[st] = 0;
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
    #endregion
}