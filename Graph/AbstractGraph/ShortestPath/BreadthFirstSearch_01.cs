using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

namespace Graph
{
    #region 01BFS
    /// <summary>
    /// 依存:Deque
    /// </summary>
    public class BreadthFirstSearch_01
    {
        private Graph<BfsEdge> g;
        public BreadthFirstSearch_01(int count)
        { g = new Graph<BfsEdge>(count); }
        public void AddEdge(int from, int to, int weight)
            => g.Edges[from].Add(new BfsEdge(from, to, weight));
        /// <summary>
        /// O(V+E)
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        public int[] Execute(int st = 0)
        {
            var deq = new Deque<int>();
            var dist = Create(g.Count, () => int.MaxValue);
            dist[st] = 0;
            deq.EnqueueHead(st);
            while (deq.Count != 0)
            {
                var p = deq.DequeueHead();
                foreach (var e in g.Edges[p])
                    if (chmin(ref dist[e.To], dist[p] + ToInt32(e.Weight)))
                    {
                        if (e.Weight)
                            deq.EnqueueTail(e.To);
                        else deq.EnqueueHead(e.To);
                    }
            }
            return dist;
        }

        public class BfsEdge : Edge, IWeight<bool>
        {
            public bool Weight { get; set; }
            public BfsEdge(int from, int to, int weight) : base(from, to)
            { Weight = weight == 1; }
        }
    }

    public class BreadthFirstSearch_01<V> where V : IVertex
    {
        private BreadthFirstSearch_01 g;
        public BreadthFirstSearch_01(int count)
        { g = new BreadthFirstSearch_01(count); }
        public void AddEdge(V from, V to, int weight)
            => g.AddEdge(from.Id, to.Id, weight);
        /// <summary>
        /// O(V+E)
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        public int[] Execute(V st)
            => g.Execute(st.Id);
    }
    #endregion
}