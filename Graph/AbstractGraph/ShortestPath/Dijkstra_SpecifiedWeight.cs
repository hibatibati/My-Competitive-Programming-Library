using System;

namespace Graph
{
    #region 特殊な重みのダイクストラ
    public abstract class DijEdge<W> : Edge, INNegWeight<W> where W : IComparable<W>
    {
        public static W Zero, Inf;
        public W Weight { get; set; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public abstract W Add(W w);
        public DijEdge(int from, int to, W weight) : base(from, to)
        { Weight = weight; }
    }

    public class Dijkstra_SpecifidWeight<W, DEdge> where W : IComparable<W> where DEdge : DijEdge<W>
    {
        private Graph<DEdge> g;
        public Dijkstra_SpecifidWeight(int c) { g = new Graph<DEdge>(c); }
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
