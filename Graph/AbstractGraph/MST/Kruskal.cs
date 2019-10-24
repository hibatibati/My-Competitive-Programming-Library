using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;


namespace Graph
{
    using Number = System.Int64;
    #region クラスカル
    /// <summary>
    /// 依存:UnionFind
    /// </summary>
    public class Kruskal
    {
        public int Count { get; set; }
        private List<MSTEdge> edges;
        public Kruskal(int count)
        { Count = count; edges = new List<MSTEdge>(); }
        public void AddEdge(int from, int to, Number weight)
            => edges.Add(new MSTEdge(from, to, weight));
        /// <summary>
        /// O(ElogV)
        /// </summary>
        /// <returns></returns>
        public Number Calc()
        {
            var uf = new UnionFind(Count);
            Number res = 0;
            edges.Sort();
            foreach (var e in edges)
                if (uf.Union(e.From, e.To))
                    res += e.Weight;
            return res;
        }
        class MSTEdge : Edge, IWeight<Number>, IComparable<MSTEdge>
        {
            public Number Weight { get; set; }
            public MSTEdge(int from, int to, Number weight) : base(from, to)
            { Weight = weight; }
            public int CompareTo(MSTEdge e)
                => Weight.CompareTo(e.Weight);
        }
    }
    #endregion
}