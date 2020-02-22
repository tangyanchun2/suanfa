using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadRemind
{
    public class ThreadSample
    {
        private bool _isStopped = false;

        public void Stop()
        {
            _isStopped = true;
        }

        public void CountNumbers()
        {
            long counter = 0;
            while(!_isStopped)
            {
                counter++;
            }

            Console.WriteLine($"{Thread.CurrentThread.Name} with {Thread.CurrentThread.Priority} " +
                $"Priority has a count = {counter.ToString("N0")}");
        }
    }
}
