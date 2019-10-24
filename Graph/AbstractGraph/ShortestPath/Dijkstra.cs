using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

namespace Graph
{
    using Number = System.Int64;
    #region ダイクストラ
    public class Dijkstra
    {
        private Graph<DijEdge> g;
        public Dijkstra(int c) { g = new Graph<DijEdge>(c); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddEdge(int from, int to, Number weight)
         => g.Edges[from].Add(new DijEdge(from, to, weight));
        public Number[] Execute(int st = 0)
        {
            var pq = new PriorityQueue<Pair<Number, int>>(false);
            pq.Enqueue(new Pair<Number, int>(0, st));
            var dist = Create(g.Count, () => Number.MaxValue);
            dist[st] = 0;
            var use = new bool[g.Count];
            while (pq.Count != 0)
            {
                var p = pq.Dequeue();
                if (p.v1.CompareTo(dist[p.v2]) == 1 || use[p.v2]) continue;
                use[p.v2] = true;
                foreach (var e in g.Edges[p.v2])
                    if (!use[e.To] && chmin(ref dist[e.To], e.Weight + p.v1))
                        pq.Enqueue(new Pair<Number, int>(dist[e.To], e.To));
            }
            return dist;
        }
        public class DijEdge : Edge
        {
            public Number Weight { get; set; }
            public DijEdge(int from, int to, Number weight) : base(from, to)
            { Weight = weight; }
        }
    }

    #endregion
}