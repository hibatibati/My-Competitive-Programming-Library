using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

namespace Graph
{
    using Number = System.Int64;
    #region 処理中に辺を生成するダイクストラ
    public class DijEdge<U> : Edge<U> where U : IVertex
    {
        public Number Weight { get; set; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public DijEdge(U from, U to, Number weight) : base(from, to)
        { Weight = weight; }
    }

    public class Dijkstra_Dynamic<V> where V : IVertex
    {
        private Dynamic<V, DijEdge<V>> g;
        private Func<int, List<DijEdge<V>>[], List<Pair<int, Number>>> GenerateEdge;
        public Dijkstra_Dynamic(int c, int pre, Func<int, List<DijEdge<V>>[], List<Pair<int, Number>>> genEdge) { g = new Dynamic<V, DijEdge<V>>(c, pre); GenerateEdge = genEdge; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddEdge(V from, V to, Number weight)
         => g.AddEdge(new DijEdge<V>(from, to, weight));
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
                foreach (var e in GenerateEdge(p.v2, g.Edges))
                    if (!use[e.v1] && chmin(ref dist[e.v1], p.v1 + e.v2))
                        pq.Enqueue(new Pair<Number, int>(dist[e.v1], e.v1));
            }
            return dist;
        }
    }

    #endregion
}