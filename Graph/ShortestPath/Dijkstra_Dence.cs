using System;

class Dijkstra_Dence
{
    private int num;
    private long[][] edge;
    public Dijkstra_Dence(int num)
    { this.num = num; edge = Create(num, () => Create(num, () => long.MaxValue)); }
    public void AddEdge(int v1, int v2, long weight)
        => edge[v1][v2] = weight;
    public long[] Execute(int st = 0)
    {
        var use = new bool[num];
        var dist = Create(num, () => long.MaxValue);
        dist[st] = 0;
        for (var p = 0; p < num; p++)
        {
            var min = long.MaxValue; var minj = -1;
            for (var i = 0; i < use.Length; i++)
                if (!use[i] && chmin(ref min, dist[i]))
                    minj = i;
            if (minj == -1) break;
            use[minj] = true;
            for (var i = 0; i < dist.Length; i++)
                if (edge[minj][i] != long.MaxValue)
                    chmin(ref dist[i], edge[minj][i] + min);
        }
        return dist;
    }
}