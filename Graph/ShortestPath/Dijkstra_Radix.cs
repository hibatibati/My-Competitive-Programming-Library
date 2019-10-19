using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

namespace Graph
{
    using Number = System.Int64;
    #region RadixHeapでのダイクストラ
    /// <summary>
    /// 依存:RadixHeap
    /// </summary>
    public class Dijkstra_Radix
    {
        private Graph<DijEdge> g;
        public Dijkstra_Radix(int c) { g = new Graph<DijEdge>(c); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddEdge(int from, int to, Number weight)
         => g.Edges[from].Add(new DijEdge(from, to, weight));
        public Number[] Calc(int st = 0)
        {
            var pq = new RadixHeap<int>();
            pq.Push(0, st);
            var dist = Create(g.Count, () => Number.MaxValue);
            dist[st] = 0;
            var use = new bool[g.Count];
            while (pq.Count != 0)
            {
                var p = pq.Pop();
                if (p.v1 > dist[p.v2] || use[p.v2]) continue;
                use[p.v2] = true;
                foreach (var e in g.Edges[p.v2])
                    if (!use[e.To] && chmin(ref dist[e.To], p.v1 + e.Weight))
                        pq.Push(dist[e.To], e.To);
            }
            return dist;
        }

        public class DijEdge : Edge, INNegWeight<Number>
        {
            public Number Weight { get; set; }
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public DijEdge(int from, int to, Number weight) : base(from, to)
            { Weight = weight; }
        }
    }
    #endregion
}