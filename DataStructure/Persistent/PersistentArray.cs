using System;
//verifyしていません
public class PersistentArray<T>
{
    public int Length { get; private set; }
    Node root;
    public PersistentArray(int sz)
    {
        Length = sz;
        root = new Node { idx = 1 };
        var Q = new Queue<Node>();
        if (2 <= sz)
            Q.Enqueue(root);
        while (Q.Any())
        {
            var p = Q.Dequeue();
            p.lft = new Node() { idx = 1 << p.idx };
            if ((p.lft.idx << 1) <= sz) Q.Enqueue(p.lft);
            if (((p.idx << 1) | 1) <= sz)
            {
                p.rgt = new Node() { idx = (p.idx << 1) | 1 };
                if ((p.rgt.idx << 1) <= sz) Q.Enqueue(p.rgt);
            }
        }
    }
    PersistentArray(Node r, int sz) { root = r; Length = sz; }
    public T this[int i]
    {
        get
        {
            var mask = MSB(++i) - 1;
            var nd = root;
            while (mask >= 0) { if ((1 & i >> mask) == 0) nd = nd.lft; else nd = nd.rgt; mask--; }
            return nd.dat;
        }
    }
    public PersistentArray<T> Set(int i, T v)
    {
        var nd = root.DeepCopy();
        var mask = MSB(++i) - 1;
        var r = new PersistentArray<T>(nd, Length);
        while (mask >= 0)
        {
            if ((1 & i >> mask) == 0) nd = nd.lft = nd.lft.DeepCopy();
            else nd = nd.rgt = nd.rgt.DeepCopy();
            mask--;
        }
        nd.dat = v;
        return r;
    }
    class Node
    {
        public int idx;
        public T dat;
        public Node lft, rgt;
        public Node DeepCopy() => new Node() { idx = idx, dat = dat, lft = lft, rgt = rgt };
    }

    int MSB(int v)
    {
        var rt = 0;
        if ((v >> 16) > 0) { rt |= 16; v >>= 16; }
        if ((v >> 8) > 0) { rt |= 8; v >>= 8; }
        if ((v >> 4) > 0) { rt |= 4; v >>= 4; }
        if ((v >> 2) > 0) { rt |= 2; v >>= 2; }
        return rt | (v >> 1);
    }
}