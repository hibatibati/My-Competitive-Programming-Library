using System;

/// <summary>
/// 依存:deque
/// 参考にしたサイト
/// ・https://ei1333.github.io/luzhiled/snippets/structure/convex-hull-trick-add-monotone.html
/// ・http://satanic0258.hatenablog.com/entry/2016/08/16/181331
/// </summary>
class ConvexHullTrick_Monotone
{
    bool isMin;
    Deque<Line> lines;
    public ConvexHullTrick_Monotone(bool isMin = true)
    {
        this.isMin = isMin;
        lines = new Deque<Line>();
    }
    bool CanErase(Line l1, Line l2, Line l3) => (l3.B - l2.B) * (l2.A - l1.A) >= (l2.B - l1.B) * (l3.A - l2.A);
    public void Add(long A, long B)
    {
        if (!isMin) { A *= -1; B *= -1; }
        var L = new Line(A, B);
        if (lines.Count == 0)
        {
            lines.PushHead(L); return;
        }
        if (lines[0].A <= A)
        {
            if (lines[0].A == A)
            {
                if (lines[0].B <= B) return;
                lines.PopHead();
            }
            while (lines.Count >= 2 && CanErase(L, lines[0], lines[1]))
                lines.PopHead();
            lines.PushHead(L);
        }
        else
        {
            if (lines[lines.Count - 1].A == A)
            {
                if (lines[lines.Count - 1].B <= B) return;
                lines.PopTail();
            }
            while (lines.Count >= 2 && CanErase(lines[lines.Count - 2], lines[lines.Count - 1], L))
                lines.PopTail();
            lines.PushTail(L);
        }
    }
    long Calc(int i, long x) => lines[i].A * x + lines[i].B;
    public long Query(long x)
    {
        int r = lines.Count - 1, l = -1;
        while (r - l > 1)
        {
            var m = (r + l) >> 1;
            if (Calc(m, x) <= Calc(m + 1, x)) r = m;
            else l = m;
        }
        return (isMin ? 1 : -1) * Calc(r, x);
    }
    public long Query_inc(long x)
    {
        while (lines.Count >= 2 && Calc(0, x) >= Calc(1, x)) lines.PopHead();
        return (isMin ? 1 : -1) * Calc(0, x);
    }
    public long Query_dec(long x)
    {
        while (lines.Count >= 2 && Calc(lines.Count - 1, x) >= Calc(lines.Count - 2, x)) lines.PopTail();
        return (isMin ? 1 : -1) * Calc(lines.Count - 1, x);
    }
    private struct Line
    {
        public long A, B;
        public Line(long A, long B) { this.A = A; this.B = B; }
        public int CompareTo(Line l2) => A == l2.A ? B.CompareTo(l2.B) : A.CompareTo(l2.A);
    }
}
