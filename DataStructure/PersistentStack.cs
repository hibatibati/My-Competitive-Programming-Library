using System;

public class PersistentStack<T>
{
    public class Node
    {
        public T Key { get; set; }
        public Node next { get; set; }
        public Node(T key, Node next) { this.Key = key; this.next = next; }
    }
    public int Count { get; private set; }
    public PersistentStack() { }
    private PersistentStack(Node head, int Count = 0) { this.head = head; this.Count = Count; }
    public Node head { get; private set; }
    public T Peek { get { return head.Key; } }
    public PersistentStack<T> Push(T val)
        => new PersistentStack<T>(new Node(val, head), Count + 1);
    public PersistentStack<T> Pop()
        => new PersistentStack<T>(head.next, Count - 1);

}