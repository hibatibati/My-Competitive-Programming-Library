using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.CompilerServices;

class AggregationQueue<T>
{
    private Stack<Node> front, back;
    private Func<T, T, T> f;//半群
    public AggregationQueue(Func<T, T, T> f)
    {
        this.f = f;
        front = new Stack<Node>();
        back = new Stack<Node>();
    }
    public int Count => front.Count + back.Count;
    public bool Any() => front.Count > 0 || back.Count > 0;
    public T All_Fold()
    {
        if (!front.Any())
            return back.Peek().sum;
        if (!back.Any())
            return front.Peek().sum;
        return f(front.Peek().sum, back.Peek().sum);
    }
    public void Push(T v)
    {
        if (back.Any())
            back.Push(new Node(v, f(back.Peek().sum, v)));
        else back.Push(new Node(v, v));
    }
    public void Pop()
    {
        if (!front.Any())
        {
            front.Push(new Node(back.Peek().val, back.Pop().val));
            while (back.Any())
            {
                var s = f(back.Peek().val, front.Peek().sum);
                front.Push(new Node(back.Pop().val, s));
            }
        }
        front.Pop();
    }
    private struct Node
    {
        public T val, sum;
        public Node(T v, T s) { val = v; sum = s; }
    }
}