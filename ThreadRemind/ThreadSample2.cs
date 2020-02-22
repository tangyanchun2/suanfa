using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadRemind
{
    public class ThreadSample2
    {
        private readonly int _iterations;

        public ThreadSample2(int paraIterations)
        {
            _iterations = paraIterations;
        }

        public void CountNumbers()
        {
            for(int i=0; i<_iterations; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine($"{Thread.CurrentThread.Name} prints {i}");
            }
        }
    }
}
