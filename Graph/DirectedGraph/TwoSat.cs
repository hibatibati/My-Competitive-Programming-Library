using System;
//require "StronglyConnectedComponents"
class TwoSat
{
    StronglyConnectedComponents scc;
    public bool[] Answer { get; private set; }
    public TwoSat(int N)
    {
        scc = new StronglyConnectedComponents(N << 1);
        Answer = new bool[N];
    }
    //(x_i=f)V(x_j=g) <-> !(x_i=f)->(x_j=g) <-> !(x_j=g)->(x_i=f)の追加
    public void AddClosure(int i, bool f, int j, bool g)
    {
        scc.AddEdge((i << 1) | (f ? 0 : 1), (j << 1) | (g ? 1 : 0));
        scc.AddEdge((j << 1) | (g ? 0 : 1), (i << 1) | (f ? 1 : 0));
    }
    public bool Execute()
    {
        scc.Execute();
        var gp = scc.Group;
        var len = gp.Length >> 1;
        for (int i = 0; i < len; i++)
        {
            if (gp[i << 1] == gp[(i << 1) | 1]) return false;
            Answer[i] = gp[i<<1] < gp[(i<<1)|1];//DAG
        }
        return true;
    }
}
