using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

public class TopologicalSort
{
    private List<int>[] g;
    private int[] indeg;
    private List<int> list;
    public TopologicalSort(int count)
    { g = Create(count, () => new List<int>()); indeg = new int[count]; }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddEdge(int from, int to)
    {
        g[from].Add(to);
        indeg[to]++;
    }
    public List<int> Execute()
    {
        list = new List<int>(g.Length);
        var q = new Queue<int>();
        for (var i = 0; i < g.Length; i++)
            if (indeg[i] == 0)
                q.Enqueue(i);
        while (q.Any())
        {
            var p = q.Dequeue();
            list.Add(p);
            foreach (var e in g[p])
                if (--indeg[e] == 0)
                    q.Enqueue(e);
        }
        return list;
    }
}