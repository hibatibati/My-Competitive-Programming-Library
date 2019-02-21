using System;
using System.Collections.Generic;

public class UnionFind
{
    private class Vertex
    {
        public T Key { get; set; }
        public Vertex Parent { get; set; }
        public int Rank { get; set; }

        public Vertex(T key) : this(key, null, 0)
        {

        }
        public Vertex(T key, Vertex Parent, int rank)
        {
            this.Key = key;
            this.Parent = Parent;
            this.Rank = rank;
        }
    }
    private readonly Dictionary<T, Vertex> dics;

    public int Count { get; private set; }

    public UnionFind()
    {
        dics = new Dictionary<T, Vertex>();
    }

    public T parent(T key)
        => FindSet(dics[key].Parent).Key;

    public bool IsSame(T key1, T key2)
    {
        if (!dics.ContainsKey(key1) || !dics.ContainsKey(key2))
            throw new ArgumentException("対応する頂点が存在しません");
        return ReferenceEquals(FindSet(dics[key1]), FindSet(dics[key2]));
    }

    public bool MakeSet(T key)
    {
        if (dics.ContainsKey(key))
            throw new ArgumentException($"{key}をキーとして持つ頂点が既に存在します");
        dics[key] = new Vertex(key, NIL, 0);
        dics[key].Parent = dics[key];
        Count++;
        return true;
    }

    public bool Union(T key1, T key2)
    {
        if (!dics.ContainsKey(key1) || !dics.ContainsKey(key2))
            throw new ArgumentException("対応する頂点が存在しません");
        if (IsSame(key1, key2)) return false;
        Link(FindSet(dics[key1]), FindSet(dics[key2]));
        Count--;
        return true;
    }

    private bool Link(Vertex vex1, Vertex vex2)
    {
        if (vex1.Rank > vex2.Rank)
            vex2.Parent = vex1;
        else
        {
            vex1.Parent = vex2;
            if (vex1.Rank == vex2.Rank)
                vex2.Rank++;
        }
        return true;
    }

    private Vertex FindSet(Vertex vex)
    {
        if (!ReferenceEquals(vex.Parent, vex))
            vex.Parent = FindSet(vex.Parent);
        return vex.Parent;
    }
}
