using System;
using System.Collections.Generic;
using System.Linq;

namespace DataStructure
{
    using C = System.Int32;
    public class RadixHeap<T>
    {
        public int Count { get; set; }
        private C last;
        private List<Pair<C, T>>[] v;
        private int len;
        public RadixHeap()
        {
            len=Convert.ToString(C.MaxValue, 2).Length;
            v = Enumerable.Repeat(0, len + 2).Select(_ => new List<Pair<C, T>>()).ToArray();
        }
        private int GetBit(C a)
        {
            for (var i = len; i >= 0; i--)
                if ((1 & a >> i) == 1)
                    return i+1;
            return 0;
        }
        public void Push(Pair<C, T> p)
            => Push(p.v1, p.v2);
        /// <summary>
        /// last(=最後にpopしたもののkey)以上のkeyについて(key,value)をpushします
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Push(C key, T value)
        {
            Count++;
            v[GetBit(key ^ last)].Add(new Pair<C, T>(key, value));
        }
        public Pair<C, T> Pop()
        {
            if (v[0].Count == 0)
            {
                var ind = 1;
                while (v[ind].Count == 0) ind++;
                last = v[ind].Min().v1;
                foreach (var p in v[ind])
                    v[GetBit(p.v1 ^ last)].Add(p);
                v[ind].Clear();
            }
            Count--;
            var ret = v[0].Last();
            v[0].RemoveAt(v[0].Count - 1);
            return ret;
        }
        public bool Any() => Count > 0;
    }
}
