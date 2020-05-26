using System;
using System.Linq;

class LazyBinaryTrie
{
    const int MAX = 32;
    Node root;
    public LazyBinaryTrie() { }
    public int this[int i] => Get(root, i);
    public int Count => root == null ? 0 : root.cnt;
    public void Insert(int v) => root = Add(root, v);
    public void Erase(int v) => root = Sub(root, v);
    public void Xor_All(int v) { if (root != null) root.lazy ^= v; }
    public int Min_Element(int bias = 0) => GetMin(root, bias);//biasとxorしたときに最小になる値
    public int Max_Element(int bias = 0) => GetMin(root, ~bias);//biasとxorしたときに最大になる値
    public int LowerBound(int v) => CountLower(root, v);
    public int UpperBound(int v) => CountLower(root, v + 1);
    public int Number_of_Elements(int v)
    {
        if (root == null) return 0;
        var now = root;
        for (int i = MAX - 1; i >= 0; i--)
        {
            Eval(now, i);
            now = now.ch[(v >> i) & 1];
            if (now == null) return 0;
        }
        return now.cnt;
    }
    void Eval(Node nd, int b)
    {
        if ((1 & nd.lazy >> b) == 1) swap(ref nd.ch[0], ref nd.ch[1]);
        if (nd.ch[0] != null) nd.ch[0].lazy ^= nd.lazy;
        if (nd.ch[1] != null) nd.ch[1].lazy ^= nd.lazy;
        nd.lazy = 0;
    }
    Node Add(Node nd, int val, int b = MAX - 1)
    {
        nd = nd ?? new Node();
        nd.cnt++;
        if (b < 0) return nd;
        Eval(nd, b);
        var f = (val >> b) & 1;
        nd.ch[f] = Add(nd.ch[f], val, b - 1);
        return nd;
    }
    Node Sub(Node nd, int val, int b = MAX - 1)
    {
        nd.cnt--;
        if (nd.cnt == 0) return null;
        if (b < 0) return nd;
        Eval(nd, b);
        var f = (val >> b) & 1;
        nd.ch[f] = Sub(nd.ch[f], val, b - 1);
        return nd;
    }
    int GetMin(Node nd, int val, int b = MAX - 1)
    {
        if (b < 0) return 0;
        Eval(nd, b);
        var f = (val >> b) & 1; if (nd.ch[f] == null) f ^= 1;
        return GetMin(nd.ch[f], val, b - 1) | (f << b);
    }
    int Get(Node nd, int k, int b = MAX - 1)
    {
        if (b < 0) return 0;
        Eval(nd, b);
        int m = nd.ch[0] != null ? nd.ch[0].cnt : 0;
        return k < m ? Get(nd.ch[0], k, b - 1) : (Get(nd.ch[1], k - m, b - 1) | (1 << b));
    }
    int CountLower(Node nd, int val, int b = MAX - 1)
    {
        if (nd == null || b < 0) return 0;
        Eval(nd, b);
        var f = (val >> b) & 1;
        return ((f == 1 && nd.ch[0] != null) ? nd.ch[0].cnt : 0) + CountLower(nd.ch[f], val, b - 1);
    }
    class Node
    {
        public int cnt, lazy;
        public Node[] ch;
        public Node() { ch = new Node[2]; }
    }
}