using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

namespace Graph
{
    using Number = System.Int64;
    #region 密なグラフでのダイクストラ
    public abstract class DijEdge<W> : Edge, INNegWeight<W> where W : IComparable<W>
    {
        public static W Zero, Inf;
        public W Weight { get; set; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract W Add(W w);
        public DijEdge(int from, int to, W weight) : base(from, to)
        { Weight = weight; }
    }

    public class Dijkstra_Dence<W, DEdge> where W : IComparable<W> where DEdge : DijEdge<W>
    {
        private Graph<DEdge> g;
        public Dijkstra_Dence(int c) { g = new Graph<DEdge>(c); }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddEdge(DEdge e)
         => g.Edges[e.From].Add(e);
        public W[] Execute(int st = 0)
        {
            var dist = Create(g.Count, () => DijEdge<W>.Inf);
            dist[st] = DijEdge<W>.Zero;
            var use = new bool[g.Count];
            while (true)
            {
                var min = DijEdge<W>.Inf; int minj = -1;
                for (var i = 0; i < g.Count; i++)
                    if (!use[i] && chmin(ref min, dist[i]))
                        minj = i;
                if (minj == -1) break;
                use[minj] = true;
                foreach (var e in g.Edges[minj])
                    chmin(ref dist[e.To], e.Add(min));
            }
            return dist;
        }
    }
    public class Dijkstra_Dence
    {
        private Dijkstra_Dence<Number, DijEdge> dij;
        public Dijkstra_Dence(int count)
        { dij = new Dijkstra_Dence<Number, DijEdge>(count); }
        public void AddEdge(int from, int to, Number weight)
            => dij.AddEdge(new DijEdge(from, to, weight));
        public Number[] Execute(int st = 0)
            => dij.Execute(st);
        public class DijEdge : DijEdge<Number>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public override Number Add(Number w)
                => Weight + w;
            public DijEdge(int from, int to, Number weight) : base(from, to, weight)
            { Zero = 0; Inf = Number.MaxValue; }
        }
    }
    #endregion
}