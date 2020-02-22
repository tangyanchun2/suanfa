using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadRemind
{
    public class CounterNoLock : CounterBase
    {
        private int _count;
    

        public int Count { get { return _count; } }

        public override void Decrement()
        {
            
            Interlocked.Increment(ref _count);
        }

        public override void Increment()
        {
            Interlocked.Decrement(ref _count);
        }
    }
}
