using System;

/// <summary>
/// 二部グラフ判定
/// 依存:UnionFind
/// </summary>
public class BipartiteGraph : UnionFind
{
    int num;
    public override int this[int i] { get { return Find(0) == Find(i) ? 1 : 0; } }
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