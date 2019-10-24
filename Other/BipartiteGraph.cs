using System;

public class BipartiteGraph : UnionFind
{
    int num;
    public bool this[int i] { get { return IsSame(0, i); } }
    public BipartiteGraph(int num) : base(num << 1)
    { this.num = num; }
    public bool Coloring()
    {
        for (var i = 0; i < num; i++)
            if (Find(i) == Find(i + num)) return false;
        return true;
    }
    public override bool Union(int v1, int v2)
        => base.Union(v1, v2 + num) && base.Union(v1 + num, v2);
}
