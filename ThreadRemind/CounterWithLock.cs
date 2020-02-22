using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadRemind
{
    public class CounterWithLock : CounterBase
    {
        private readonly object _syncRoot = new object();
        public int Count { get; private set; }

        public override void Decrement()
        {
            
          lock(_syncRoot)
            {
                Count--;
            }
        }

        public override void Increment()
        {
            lock (_syncRoot)
            {
                Count++;
            }
        }
    }
}
