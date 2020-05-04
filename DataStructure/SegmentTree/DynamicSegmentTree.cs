using System;
using System.Collections.Generic;
using System.Linq;

class DynamicSegmentTree<T>
{
    int sz;
    Node root;
    T id;
    Func<T, T, T> merge;
    Func<T, T, T> update;
    public DynamicSegmentTree(int N, T id, Func<T, T, T> merge, Func<T, T, T> update = null)
    {
        sz = 1;
        while (sz < N) sz <<= 1;
        this.id = id;
        this.merge = merge;
        this.update = update ?? ((a, b) => b);
        root = new Node(id, null);
    }
    Node InsertNode(Node leaf, int l, int r, int idx, bool isLeft)
    {
        var rt = new Node(id, null, idx);
        while (true)
        {
            var v = new Node(id, leaf.par);
            if (isLeft)
                leaf.par.lft = v;
            else leaf.par.rgt = v;
            leaf.par = v;
            bool a = leaf.idx < ((l + r) >> 1), b = idx < ((l + r) >> 1);
            if (a)
            {
                isLeft = true;
                v.lft = leaf; r = (l + r) >> 1;
                if (!b) { v.rgt = rt; break; }
            }
            else
            {
                isLeft = false;
                v.rgt = leaf; l = (l + r) >> 1;
                if (b) { v.lft = rt; break; }
            }
        }
        rt.par = leaf.par;
        return rt;
    }
    public void Update(int i, T val)
    {
        var now = root;
        int l = 0, r = sz;
        while (r - l > 1)
        {
            if (now.idx != -1) break;
            if (i < ((l + r) >> 1))
            {
                if (now.lft == null)
                {
                    now.lft = new Node(id, now, i); now = now.lft; break;
                }
                if (now.lft.idx != -1 && now.lft.idx != i)
                { now = InsertNode(now.lft, l, (r + l) >> 1, i, true); break; }
                now = now.lft;
                r = (l + r) >> 1;
            }
            else
            {
                if (now.rgt == null)
                {
                    now.rgt = new Node(id, now, i); now = now.rgt; break;
                }
                if (now.rgt.idx != -1 && now.rgt.idx != i)
                { now = InsertNode(now.rgt, (l + r) >> 1, r, i, false); break; }
                now = now.rgt;
                l = (l + r) >> 1;
            }
        }
        now.val = update(now.val, val);
        while (now.par != null)
        {
            var p = now.par;
            var v = id;
            if (p.lft != null) v = merge(v, p.lft.val);
            if (p.rgt != null) v = merge(v, p.rgt.val);
            p.val = v;
            now = p;
        }
    }
    public T Query(int l, int r) => Query(l, r, root, 0, sz);
    private T Query(int l, int r, Node now, int a, int b)
    {
        if (l <= now.idx && now.idx < r) return now.val;
        if (b <= l || r <= a) return id;
        if (l <= a && b <= r) return now.val;
        var rt = id;
        if (now.lft != null)
            rt = merge(rt, Query(l, r, now.lft, a, (a + b) >> 1));
        if (now.rgt != null)
            rt = merge(rt, Query(l, r, now.rgt, (a + b) >> 1, b));
        return rt;
    }
    class Node
    {
        public T val;
        public Node par, lft, rgt;
        public int idx;
        public Node(T v, Node p, int i = -1) { val = v; par = p; idx = i; }
    }
}