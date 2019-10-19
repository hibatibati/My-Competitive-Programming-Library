using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

namespace Graph
{
    #region テンプレート
    public class Graph<E> where E : Edge
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

    public interface IWeight<T> where T : IComparable<T> { T Weight { get; set; } }
    public interface INNegWeight<T> : IWeight<T> where T : IComparable<T> { }
    public interface IVertex
    {
        int Id { get; }
        int ToId();
    }
    public class Edge
    {
        public int From { get; set; }
        public int To { get; set; }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Edge(int from, int to)
        { From = from; To = to; }
    }
    #endregion
}