using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadRemind
{
    public class ThreadSampleA
    {
        private object locka = new object();

        public void lockPrint()
        {
            try
            {
                //Monitor.Enter(locka);
                Console.WriteLine("a");
                Thread.Sleep(2000);
                Console.WriteLine("b");
            }finally
            {
               // Monitor.Exit(locka);
            }
        }

    }
}
