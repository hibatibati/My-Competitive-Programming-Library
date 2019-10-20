using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

namespace Graph
{
    #region テンプレート
    public class Graph<E> where E : IEdge
    {
        public List<E>[] Edges { get; set; }
        public int Count { get; }
        public Graph(int count)
        {
            Count = count;
            Edges = Create(count, () => new List<E>());
        }
    }
    public class Directed<E> : Graph<E> where E : Edge
    { public Directed(int count) : base(count) { } }
    public class Undirected<E> : Graph<E> where E : Edge
    { public Undirected(int count) : base(count) { } }
    public class Graph<V, E> where V : IVertex where E : IEdge<V>
    {
        public List<E>[] Edges { get; set; }
        public int Count { get; }
        public Graph(int count)
        {
            Count = count;
            Edges = Create(count, () => new List<E>());
        }
    }
    public class Dynamic<V, E> where V : IVertex where E : IEdge<V>
    {
        private Graph<V, E> g;
        public List<E>[] Edges { get { return g.Edges; } }
        public int Count { get; }
        public void AddEdge(E e)
            => g.Edges[e.From.Id].Add(e);
        public Dynamic(int count, int pre)
        {
            Count = count;
            g = new Graph<V, E>(pre);
        }
    }

    public interface IWeight<T> where T : IComparable<T> { T Weight { get; set; } T Add(T t); }
    public interface INNegWeight<T> : IWeight<T> where T : IComparable<T> { }
    public interface IVertex
    {
        int Id { get; }
    }
    public interface IEdge
    {
        int From { get; set; }
        int To { get; set; }
    }
    public interface IEdge<V> where V : IVertex
    {
        V From { get; set; }
        V To { get; set; }
    }
    public class Edge : IEdge
    {
        public int From { get; set; }
        public int To { get; set; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Edge(int from, int to)
        { From = from; To = to; }
    }
    public class Edge<V> : IEdge<V> where V : IVertex
    {
        public V From { get; set; }
        public V To { get; set; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Edge(V from, V to)
        { From = from; To = to; }
    }
    #endregion
}