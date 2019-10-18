using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Graph
{
    using Number = System.Int64;
    public class ShortestPath
    {
        #region ダイクストラ
        /// <summary>
        /// 負の重みの辺を持たないグラフでの単一始点最短距離をO(ElogV)で求める
        /// 依存:PriorityQueue,Pair
        /// </summary>
        /// <param name="edges"></param>
        /// <param name="st">始点</param>
        /// <returns></returns>
        public static Number[] Dijkstra(WeightedGraph<Number> edges, int st = 0)
        {
            var dist = Enumerable.Repeat(Number.MaxValue, edges.Count).ToArray();
            var pq = new PriorityQueue<Pair<Number, int>>(false);
            pq.Enqueue(new Pair<Number, int>(0, st));
            dist[st] = 0;
            while (pq.Count != 0)
            {
                var p = pq.Dequeue();
                if (dist[p.v2] < p.v1) continue;
                foreach (var e in edges[p.v2])
                    if (chmin(ref dist[e.To], e.Cost + p.v1))
                        pq.Enqueue(new Pair<Number, int>(dist[e.To], e.To));
            }
            return dist;
        }
        #endregion
        #region RadixHeapでのダイクストラ
        /// <summary>
        /// RadixHeapを用いて単一始点最短経路を求める
        /// 依存:RadixHeap
        /// </summary>
        /// <param name="edges"></param>
        /// <param name="st"></param>
        /// <returns></returns>
        public static Number[] RadixDijkstra(WeightedGraph<Number> edges, int st)
        {
            var dist = Enumerable.Repeat(Number.MaxValue, edges.Count).ToArray();
            var pq = new RadixHeap<int>();
            pq.Push(0, st);
            dist[st] = 0;
            while (pq.Count != 0)
            {
                var p = pq.Pop();
                if (dist[p.v2] < p.v1) continue;
                foreach (var e in edges[p.v2])
                    if (chmin(ref dist[e.To], e.Cost + p.v1))
                        pq.Push(dist[e.To], e.To);
            }
            return dist;
        }
        #endregion
        #region 密なグラフでのダイクストラ
        /// <summary>
        /// 負の重みの辺を持たないグラフでの単一始点最短経路をO(V^2)で求める
        /// </summary>
        /// <param name="gr"></param>
        /// <param name="st"></param>
        /// <returns></returns>
        public static Number[] Dijkstra(DenceGraph<Number> gr, int st)
        {
            var dist = Enumerable.Repeat(Number.MaxValue, gr.Count).ToArray();
            dist[st] = 0;
            var use = new bool[gr.Count];
            while (true)
            {
                var min = Number.MaxValue;
                var minj = -1;
                for (var i = 0; i < gr.Count; i++)
                    if (!use[i] && chmin(ref min, dist[i]))
                        minj = i;
                if (minj == -1) break;
                use[minj] = true;
                for (var i = 0; i < gr.Count; i++)
                    chmin(ref dist[i], dist[minj] + gr[minj][i]);
            }
            return dist;
        }
        #endregion
        #region ワーシャルフロイド
        /// <summary>
        /// 全点対最短距離をO(V^3)で求める
        /// </summary>
        /// <param name="num">頂点数</param>
        /// <param name="edges">辺の集合</param>
        /// <param name="directed">有向グラフか</param>
        /// <returns></returns>
        public static Number[][] WarshallFloyd(int num, IEnumerable<Edge<Number>> edges, bool directed = false)
        {
            var dist = Enumerable.Repeat(0, num).Select(_ => Enumerable.Repeat(Number.MaxValue / 2, num).ToArray()).ToArray();
            foreach (var e in edges)
            {
                dist[e.From][e.To] = e.Cost;
                if (!directed)
                    dist[e.To][e.From] = e.Cost;
            }
            for (var k = 0; k < num; k++)
                for (var i = 0; i < num; i++)
                    for (var j = 0; j < num; j++)
                        chmin(ref dist[i][j], dist[i][k] + dist[k][j]);
            return dist;
        }
        #endregion
        #region ベルマンフォード
        /// <summary>
        /// 有向グラフ上の単一始点最短距離をO(VE)で求める
        /// </summary>
        /// <param name="num">頂点数</param>
        /// <param name="edges">辺の集合</param>
        /// <param name="st">始点</param>
        /// <param name="dist">始点からの最短距離</param>
        /// <returns>始点から辿り着ける負閉路が存在した場合、false</returns>
        static bool BellmanFord(int num, IEnumerable<Edge<Number>> edges, int st, out long[] dist)
        {
            dist = Enumerable.Repeat(long.MaxValue, num).ToArray();
            dist[st] = 0;
            for (var i = 0; i < num - 1; i++)
                foreach (var e in edges)
                    if (dist[e.From] != long.MaxValue)
                        chmin(ref dist[e.To], dist[e.From] + e.Cost);
            for (var i = 0; i < num; i++)
                foreach (var e in edges)
                {
                    if (dist[e.From] == long.MaxValue) continue;
                    if (dist[e.To] > dist[e.From] + e.Cost)
                        return false;
                }
            return true;
        }
        #endregion
    }
}

