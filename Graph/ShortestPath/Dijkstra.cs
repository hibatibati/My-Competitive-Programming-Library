using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

namespace Graph
{
    using Number = System.Int64;
    #region ダイクストラ
    public abstract class DijEdge<W> : Edge, INNegWeight<W> where W : IComparable<W>
    {
        public static W Zero, Inf;
        public W Weight { get; set; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract W Add(W w);
        public DijEdge(int from, int to, W weight) : base(from, to)
        { Weight = weight; }
    }

    public class Dijkstra
    {
        private Dijkstra<Number, DijEdge> dij;
        public Dijkstra(int count)
        { dij = new Dijkstra<Number, DijEdge>(count); }
        public void AddEdge(int from, int to, Number weight)
            => dij.AddEdge(new DijEdge(from, to, weight));
        public Number[] Execute(int st = 0)
            => dij.Execute(st);
        public class DijEdge : DijEdge<Number>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override long Add(Number w)
                => Weight + w;
            public DijEdge(int from, int to, Number weight) : base(from, to, weight)
            { Zero = 0; Inf = Number.MaxValue; }
        }
    }
    public class Dijkstra<W, DEdge> where W : IComparable<W> where DEdge : DijEdge<W>
    {
        private Graph<DEdge> g;
        public Dijkstra(int c) { g = new Graph<DEdge>(c); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddEdge(DEdge e)
         => g.Edges[e.From].Add(e);
        public W[] Execute(int st = 0)
        {
            var pq = new PriorityQueue<Pair<W, int>>(false);
            pq.Enqueue(new Pair<W, int>(DijEdge<W>.Zero, st));
            var dist = Create(g.Count, () => DijEdge<W>.Inf);
            dist[st] = DijEdge<W>.Zero;
            var use = new bool[g.Count];
            while (pq.Count != 0)
            {
                var p = pq.Dequeue();
                if (p.v1.CompareTo(dist[p.v2]) == 1 || use[p.v2]) continue;
                use[p.v2] = true;
                foreach (var e in g.Edges[p.v2])
                    if (!use[e.To] && chmin(ref dist[e.To], e.Add(p.v1)))
                        pq.Enqueue(new Pair<W, int>(dist[e.To], e.To));
            }
            return dist;
        }
    }

    #endregion
}