using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

namespace Graph
{
    using Number = System.Int64;
    #region 特殊な頂点のダイクストラ

    public class Dijkstra_SpecifiedVertex<V> where V : IVertex
    {
        private Graph<V, DijEdge> g;
        public Dijkstra_SpecifiedVertex(int c) { g = new Graph<V, DijEdge>(c); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddEdge(V from, V to, Number w)
         => g.Edges[from.Id].Add(new DijEdge(from, to, w));
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
                {
                    var id = e.To.Id;
                    if (!use[id] && chmin(ref dist[id], e.Weight + p.v1))
                        pq.Enqueue(new Pair<Number, int>(dist[id], id));
                }
            }
            return dist;
        }
        public class DijEdge : Edge<V>
        {
            public Number Weight { get; set; }
            public DijEdge(V from, V to, Number weight) : base(from, to)
            { Weight = weight; }
        }
    }

    #endregion
}