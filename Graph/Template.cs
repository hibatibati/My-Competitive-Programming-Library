using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

namespace Graph
{
    #region テンプレート
    public class UnWeightedGraph
    {
        public List<int>[] edges { get; set; }
        public int Count { get; set; }
        public bool IsDirected { get; set; }
        public List<int> this[int index]
        { get { return edges[index]; } }
        public UnWeightedGraph(int length, bool isDirected = false)
        {
            edges = Enumerable.Repeat(0, length).Select(_ => new List<int>()).ToArray();
            Count = length;
            IsDirected = isDirected;
        }
        private UnWeightedGraph(List<int>[] edges)
        { this.edges = edges; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddEdge(int from, int to)
        {
            edges[from].Add(to);
            if (!IsDirected)
                edges[to].Add(from);
        }
        public static implicit operator UnWeightedGraph(List<int>[] edges)
            => new UnWeightedGraph(edges);
    }

    public class WeightedGraph<T> where T : IComparable<T>
    {
        public List<Edge<T>>[] edges { get; set; }
        public int Count { get; }
        public bool IsDirected { get; set; }
        public List<Edge<T>> this[int index]
        { get { return edges[index]; } }
        public WeightedGraph(int length, bool isDirected = false)
        {
            edges = Enumerable.Repeat(0, length).Select(_ => new List<Edge<T>>()).ToArray();
            Count = length;
            IsDirected = isDirected;
        }
        private WeightedGraph(List<Edge<T>>[] edges)
        { this.edges = edges; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AddEdge(int from, int to, T cost)
        {
            edges[from].Add(new Edge<T>(to, cost));
            if (!IsDirected)
                edges[to].Add(new Edge<T>(from, cost));
        }
        public static implicit operator WeightedGraph<T>(List<Edge<T>>[] edges)
            => new WeightedGraph<T>(edges);
    }
    //隣接行列で持つ
    public class DenceGraph<T> where T : IComparable<T>
    {
        public T[][] edges { get; set; }
        public int Count { get; }
        public T[] this[int index]
        { get { return edges[index]; } }
        public DenceGraph(int length)
        {
            edges = Enumerable.Repeat(0, length).Select(_ => new T[length]).ToArray();
            Count = length;
        }
    }

    public struct Edge<T> : IComparable<Edge<T>> where T : IComparable<T>
    {
        public int From { get; set; }
        public int To { get; set; }
        public T Cost { get; set; }
        public Edge(int to, T cost) : this(-1, to, cost) { }
        public Edge(int from, int to, T cost)
        { From = from; To = to; Cost = cost; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator int(Edge<T> e)
            => e.To;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int CompareTo(Edge<T> e)
            => Cost.CompareTo(e.Cost);
    }
    #endregion
}