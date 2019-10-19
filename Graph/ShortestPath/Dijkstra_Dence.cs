using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

namespace Graph
{
    using Number = System.Int64;
    #region 密なグラフでのダイクストラ
    public class Dijkstra_Dence
    {
        private Graph<DijEdge> g;
        public Dijkstra_Dence(int c) { g = new Graph<DijEdge>(c); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddEdge(int from, int to, Number weight)
         => g.Edges[from].Add(new DijEdge(from, to, weight));
        public Number[] Calc(int st = 0)
        {
            var dist = Create(g.Count, () => Number.MaxValue);
            dist[st] = 0;
            var use = new bool[g.Count];
            while (true)
            {
                Number min = Number.MaxValue; int minj = -1;
                for (var i = 0; i < g.Count; i++)
                    if (!use[i] && chmin(ref min, dist[i]))
                        minj = i;
                if (minj == -1) break;
                use[minj] = true;
                foreach (var e in g.Edges[minj])
                    chmin(ref dist[e.To], e.Weight + min);
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