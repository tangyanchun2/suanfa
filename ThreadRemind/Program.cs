using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadRemind
{
    class Program
    {
        static void Main(string[] args)
        {

            testabort();
            Console.ReadKey();
     
        }

        static void testabort()
        {
            Thread t = new Thread(PrintNumbersWithDelay);
            t.Start();
            Thread.Sleep(TimeSpan.FromSeconds(6));
            t.Abort();
            Console.WriteLine("A thread has been aborted");
        }

        static void PrintNumbersWithDelay()
        {
            try
            {
                Console.WriteLine("Starting...");
                for (int i = 1; i < 10; i++)
                {
                    Thread.Sleep(TimeSpan.FromSeconds(2));
                    Console.WriteLine(i);
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                int s = 1;
                Thread.ResetAbort();
            }
            Console.WriteLine("a");
            //Thread.Sleep(TimeSpan.FromSeconds(2000));
           
            Console.WriteLine("b");
        }

        static void PrintNumbers()
        {
            Console.WriteLine("start ...");
            for(int i=1; i <10; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(1000);
            }
        }

        static void testCreate()
        {
            Thread t = new Thread(PrintNumbers);
            t.Start();
            PrintNumbers();
            Console.WriteLine("abc");
            Thread.Sleep(2000);
            Console.WriteLine("cba");

        }
    }
}
