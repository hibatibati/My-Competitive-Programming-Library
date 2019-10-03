using System;
using System.Collections.Generic;
using System.Linq;

public class SkewHeap<T> where T : IComparable<T>
{
    private Node root;
    public bool IsMax { get; }
    public int Count { get; private set; }
    public T Top { get { return root.Key; } }
    public SkewHeap(bool isMax = false)
    { IsMax = isMax; }
    private SkewHeap(Node r,bool isMax)
    { root = r;IsMax = isMax; }
    private int Compare(T v1, T v2) => (IsMax ? 1 : -1) * v1.CompareTo(v2);
    private void Swap<U>(ref U v1, ref U v2) { var t = v1; v1 = v2; v2 = t; }
    /// <summary>
    /// heap同士のマージを行います
    /// 計算量:O(logN)
    /// </summary>
    /// <param name="sh"></param>
    public void Merge(SkewHeap<T> sh)
    { root = Merge(root, sh.root); Count += sh.Count; }
    private Node Merge(Node x, Node y)
    {
        if (x == null || y == null)
            return x ?? y;
        if (Compare(x.Key, y.Key) == -1)
            Swap(ref x, ref y);
        x.right = Merge(y, x.right);
        Swap(ref x.left, ref x.right);
        return x;
    }
    /// <summary>
    /// 要素を追加します
    /// 計算量:O(logN)
    /// </summary>
    /// <param name="key"></param>
    public void Push(T key)
    { root = Merge(root, new Node(key)); Count++; }
    /// <summary>
    /// 一番大きい要素を取り出します
    /// </summary>
    /// <returns></returns>
    public T Pop()
    {
        var ret = root.Key;
        root = Merge(root.left, root.right);
        Count--;
        return ret;
    }
    public void Clear()
    {   root = null; Count = 0; }
    class Node
    {
        public T Key { get; set; }
        public Node left, right;
        public Node(T key, Node left = null, Node right = null)
        {
            Key = key; this.left = left; this.right = right;
        }
    }
}
