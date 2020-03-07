using System;
using System.Collections.Generic;
/// <summary>
/// 依存:deque
/// </summary>
/// <typeparam name="T"></typeparam>
/// <param name="ar"></param>
/// <param name="k"></param>
/// <returns></returns>
public static T[] SlideMin<T>(IList<T> A, int k)
        where T : IComparable<T>
{
    var deq = new Deque<int>();
    var rt = new T[A.Count - k + 1];
    for (var i = 0; i < A.Count; i++)
    {
        while (deq.Count != 0 && A[deq[deq.Count - 1]].CompareTo(A[i]) != -1)
            deq.PopTail();
        deq.PushHead(i);
        if (i - k + 1 >= 0)
        {
            rt[i - k + 1] = A[deq[0]];
            if (i - k + 1 == deq[0]) deq.PopHead();
        }
    }
    return rt;
}