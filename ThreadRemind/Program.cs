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
        static AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        static string dataFromServer = "";

        private static AutoResetEvent _wokerEvent = new AutoResetEvent(false);
        private static AutoResetEvent _mainEvent = new AutoResetEvent(false);

        static void Main(string[] args)
        {
            //RunThreads();
            //testabort2();
            //RunThreadsBack();
            //RunThreadPara();
            //testaaa();
            //testLockTooMuch();
            //testMonitorA();
            //testFault();
            //TestCounterProgram();

            //TestMutex();
            //testDB();
            //testAutoReset();
            //testRes();
            //testAuto();
            testAuto2();
            Console.ReadKey();
     
        }

        static void testAuto2()
        {
            Task.Run(() => { process2();});
            Console.WriteLine("main wait");
            _wokerEvent.WaitOne();
            Console.WriteLine("main get res");
            _wokerEvent.WaitOne();
            Console.WriteLine("main get res 2");
        }

        static void process2()
        {
            Console.WriteLine("work start ...");
            Thread.Sleep(5000);
            Console.WriteLine("work end ...");
            _wokerEvent.Set();
            Console.WriteLine("work second start ....");
            Thread.Sleep(5000);
            Console.WriteLine("work second end ...");
            _wokerEvent.Set();
        }

        static void testAuto()
        {
            var t = new Thread(() => Process(10));
            t.Start();

            Console.WriteLine("main wait for work");
            _wokerEvent.WaitOne();
            Console.WriteLine("main wait  finished");
            Console.WriteLine("main perform a op");
            Thread.Sleep(TimeSpan.FromSeconds(5));
            _mainEvent.Set();
            Console.WriteLine("main second");
            _wokerEvent.WaitOne();
            Console.WriteLine("main seond finish");

            
        }

        static void testRes()
        {
            Task task = Task.Factory.StartNew(
                () =>
                {
                    GetDataFromServer();
                }
                );
            autoResetEvent.WaitOne();
            Console.WriteLine(dataFromServer);
        }

        static void GetDataFromServer()
        {
            Thread.Sleep(TimeSpan.FromSeconds(4));
            dataFromServer = "Webservice data";
           

        }


        static void Process(int seconds)
        {
            Console.WriteLine("woker Starting a long running work ....");
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.WriteLine("worker Work is done!");
            _wokerEvent.Set();
            Console.WriteLine("worker wait for main thread");
            _mainEvent.WaitOne();
            Console.WriteLine("worker start second operation");
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            Console.Write("worker work is done!");
            _wokerEvent.Set();


        }

        static void testDB()
        {
            for(int i=1; i<=6; i++)
            {
                string threadName = "Thread " + i;
                int secondsToWait = 2 + 2 * i;
                var t = new Thread(()=>AccessDatabase(threadName, secondsToWait));
                t.Start();
            }
        }

        static SemaphoreSlim _sema = new SemaphoreSlim(2,2);

        static void AccessDatabase(string name, int seconds)
        {
            Console.WriteLine($"{name} waits to access a database");
            _sema.Wait();
            try
            {
                Console.WriteLine($"{name} was granted an access to a database");
                Thread.Sleep(TimeSpan.FromSeconds(seconds));
                Console.WriteLine($"{name} is completed");
            }
            finally
            {
                _sema.Release();
            }
        }

        static void TestMutex()
        {
            const string MutextName = "abc123";
            using (var m = new Mutex(false, MutextName))
            {
                
                if(!m.WaitOne(TimeSpan.FromSeconds(10),false))
                {
                    Console.WriteLine("wait failed");
                }else
                {
                    Console.WriteLine("success running");
                    Console.ReadLine();
                    m.ReleaseMutex();
                }
            }
        }

        static void TestCounterProgram()
        {
            var c = new Counter();
            var t1 = new Thread(() => TestCounter(c));
            var t2 = new Thread(() => TestCounter(c));
            var t3 = new Thread(() => TestCounter(c));
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();

            Console.WriteLine("Total count: {0}", c.Count);
            Console.WriteLine("--------------------------");

            Console.WriteLine("Correct counter");

            var c1 = new CounterNoLock();

            t1 = new Thread(() => TestCounter(c1));
            t2 = new Thread(() => TestCounter(c1));
            t3 = new Thread(() => TestCounter(c1));
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();

            Console.WriteLine("Total count: {0}", c1.Count);

        }

        static void TestCounter(CounterBase c)
        {
            for(int i=0; i<100000; i++)
            {
                c.Increment();
                c.Decrement();
            }
        }

        static void testFault()
        {
            var t = new Thread(FaultyTread);
            t.Start();
            t.Join();

            Console.WriteLine("finished ...");
            try
            {
                t = new Thread(BadFaultyThread);
                t.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("We won't get here!");
            }
        }

        static void BadFaultyThread()
        {
            Console.WriteLine("Starting a bad....");
            Thread.Sleep(TimeSpan.FromSeconds(1));
            throw new Exception("bad boom");
        }

        static void FaultyTread()
        {
            try
            {
                Console.WriteLine("start faulty ... ");
                Thread.Sleep(TimeSpan.FromSeconds(1));
                throw new Exception("fault boom");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Faulty handled:{ex.Message}");
            }
        }

        static void testMonitorA()
        {
            ThreadSampleA sampa = new ThreadSampleA();
            Task.Run(() => { sampa.lockPrint(); });
            Task.Run(() => { sampa.lockPrint(); });

        }

        static void testLockTooMuch()
        {
            object lock1 = new object();
            object lock2 = new object();

            new Thread(() => LockTooMuch(lock1, lock2)).Start();
            
            lock(lock2)
            {
                Thread.Sleep(3000);
                Console.WriteLine("Monitor.TryEnter allows not to get stuck, returning false after a specified timeout is elapsed");

                bool a=false;
                Monitor.Enter(lock1,ref a);
                /*if (Monitor.TryEnter(lock1, TimeSpan.FromSeconds(5)))
                {
                    Console.WriteLine("Acquired a protected resource succesfully");
                }
                else
                {
                    Console.WriteLine("Timeout acquiring a resource!");
                }*/
            }
        }

        static void LockTooMuch(object lock1, object lock2)
        {
            lock(lock1)
            {
                Thread.Sleep(1000);
                lock (lock2) ;
                Console.WriteLine("get all");
            }
        }

        static void testaaa()
        {
            Console.WriteLine("Incorrect counter");

            var c = new Counter();

            var t1 = new Thread(() => TestCounter(c));
            var t2 = new Thread(() => TestCounter(c));
            var t3 = new Thread(() => TestCounter(c));
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();

            Console.WriteLine("Total count: {0}", c.Count);
            Console.WriteLine("--------------------------");

            var c1 = new CounterWithLock();

            t1 = new Thread(() => TestCounter(c1));
            t2 = new Thread(() => TestCounter(c1));
            t3 = new Thread(() => TestCounter(c1));
            t1.Start();
            t2.Start();
            t3.Start();
            t1.Join();
            t2.Join();
            t3.Join();
            Console.WriteLine("Total count: {0}", c1.Count);

            Console.ReadKey();
        }
        /*
        static void TestCounter(CounterBase c)
        {
            for (int i = 0; i < 100000; i++)
            {
                c.Increment();
                c.Decrement();
            }
        }*/

        static void RunThreadPara()
        {
            /*
            var sample3 = new ThreadSample3(10);
            var threadOne = new Thread(sample3.CountNumbers);
            threadOne.Name = "ThreadOne";
            threadOne.Start();
            threadOne.Join();
            Console.WriteLine("--------------------------");

            var threadTwo = new Thread(Count);
            threadTwo.Name = "ThreadTwo";
            threadTwo.Start(8);
            threadTwo.Join();
            Console.WriteLine("-------------------------");

            var threadThree = new Thread(() => CountNumbers(12));
            threadThree.Name = "ThreadThree";
            threadThree.Start();
            threadThree.Join();
            Console.WriteLine("------------------------");*/

            int i = 10;
            var threadFour = new Thread(() => PrintNumber(i));
            i = 20;
            var threadFive = new Thread(() => PrintNumber(i));
            threadFour.Start();
            threadFive.Start();

        }

        static void PrintNumber(int number)
        {
            Console.WriteLine(number);
        }

        static void Count(object iterations)
        {
            CountNumbers((int)iterations);
        }

        static void CountNumbers(int iterations)
        {
            for (int i = 1; i <= iterations; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine("{0} prints {1}", Thread.CurrentThread.Name, i);
            }
        }


        static void RunThreadsBack()
        {
            var sampleForeground = new ThreadSample2(10);
            var sampleBackgroud = new ThreadSample2(20);

            var threadOne = new Thread(sampleForeground.CountNumbers);
            threadOne.Name = "ForegroundThread";
            var threadTwo = new Thread(sampleBackgroud.CountNumbers);
            threadTwo.Name = "BackgroundThread";
            threadTwo.IsBackground = true;

            threadOne.Start();
            threadTwo.Start();

        }

        static void RunThreads()
        {
            var sample = new ThreadSample();
            var threadOne = new Thread(sample.CountNumbers);
            threadOne.Name = "ThreadOne";
            var threadTwo = new Thread(sample.CountNumbers);
            threadTwo.Name = "ThreadTwo";

            threadOne.Priority = ThreadPriority.Highest;
            threadTwo.Priority = ThreadPriority.Lowest;
            threadOne.Start();
            threadTwo.Start();

            Thread.Sleep(TimeSpan.FromSeconds(2));
            sample.Stop();
        }

        static void testabort2()
        {
            Console.WriteLine("Starting program...");
            Thread t = new Thread(PrintNumbersWithStatus);
            Thread t2 = new Thread(DoNothing);
            Console.WriteLine("out::"+t.ThreadState.ToString());
          //  t.IsBackground = true;
            t2.Start();
            t.Start();
           

            for (int i = 1; i < 30; i++)
            {
              //  Console.WriteLine(t.ThreadState.ToString());
            }

            Thread.Sleep(TimeSpan.FromSeconds(6));
            t.Abort();
            Console.WriteLine("A thread has been aborted");
            Console.WriteLine(t.ThreadState.ToString());
            Console.WriteLine(t2.ThreadState.ToString());
            Thread.Sleep(5000);
            Console.WriteLine(t.ThreadState.ToString());
            Console.WriteLine(t2.ThreadState.ToString());
        }

        static void DoNothing()
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
        }

        static void PrintNumbersWithStatus()
        {
            Console.WriteLine("start");
            Console.WriteLine("in ==="+Thread.CurrentThread.ThreadState.ToString());
            for (int i = 1; i < 10; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(2));
                Console.WriteLine(i);
            }
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
