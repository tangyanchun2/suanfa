using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SuanFa1
{
    public class Program
    {
        const string firstOrderId = "001";
        const string secondOrderId = "002";
        const string thirdOrderId = "003";
        static void Main(string[] args)
        {

            //GetRepeatedCharacter();
            //testCharArray();
            //Console.WriteLine("hi122");
            //testRemoveDup();
            //testlock1();

            //test(LockType.LockThis);
            //test(LockType.LockString);
            testa();
            Console.ReadKey();
        }

        static void testa()
        {
            Payment pay = new Payment("s1", 3);
            Payment pay2 = new Payment("s2", 3);
            new Thread(()=> { pay.showLock(); }).Start();
            new Thread(() => { pay.show2Lock(); }).Start();
            for(int i=0; i<=4; i++)
            {
                Thread.Sleep(1200);
                Console.WriteLine(pay.ThreadNo);
                Console.WriteLine(pay2.ThreadNo);
            }

        }

        static void test(LockType lockType)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------测试相同订单------------");
            Console.ForegroundColor = ConsoleColor.White;
            OrderPay(firstOrderId, 1, lockType);
            OrderPay(firstOrderId, 2, lockType);
            OrderPay(firstOrderId, 3, lockType);
            Thread.Sleep(10000);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("------------测试不同订单------------");
            Console.ForegroundColor = ConsoleColor.White;
            OrderPay(firstOrderId, 1, lockType);
            OrderPay(secondOrderId, 2, lockType);
            OrderPay(thirdOrderId, 3, lockType);
        }

        static void OrderPay(string orderId,int threadNo, LockType lockType)
        {
            new Thread(() => new Payment(orderId, threadNo).Pay(lockType)).Start();
            Thread.Sleep(100);
        }

        public static void testlock1()
        {
            MyObj myobj = new MyObj();
            Task.Run(() => { myobj.Print1(); });
            Thread.Sleep(3000);
            Console.WriteLine("in out " + myobj.s1);
        }

        public static void testRemoveDup()
        {
            string s1 = "kkkdjaadklsk";
            var array = s1?.ToCharArray() ?? new char[0];
            var rmdStr = removeDup(array);
            int s = 1;
            //kdjadklsk
        }

        public static string removeDup(Char[] charArray)
        {
            HashSet<Char> set = new HashSet<char>();
            StringBuilder sb = new StringBuilder();
            for(int i=0; i<charArray.Length; i++)
            {
                if(!set.Contains(charArray[i]))
                {
                    sb.Append(charArray[i]);
                    set.Add(charArray[i]);
                }
            }
            return sb.ToString();
        }


        public static void testCharArray()
        {
            string s1 = "abbcde";
           
            string s2 = "";
            string s3 = null;
            var ss1 = s1.ToCharArray();
            var ss2 = s2.ToCharArray();
            var ss3 = s3?.ToCharArray()??new char[0];

            var ssa = GetRepeatedCharacter(ss1);
            var ssb = GetRepeatedCharacter(ss2);
            var ssc = GetRepeatedCharacter(ss3);
            int e = 1;
        }

        public static bool GetRepeatedCharacter(Char[] charArray)
        {
            HashSet<Char> myset = new HashSet<char>(20);
            if(charArray.Length == 0)
            {
                return false;
            }

            for(int i=0; i<charArray.Length; i++)
            {
                myset.Add(charArray[i]);
            }

            if(myset.Count == charArray.Length)
            {
                return false;
            }else
            {
                return true;
            }

        }



        public static void printContinueNumber()
        {
            int sum = 15;
            print(sum);

          
        }

        public static void print(int n)
        {
            for(int i=1; i<=n/2; i++)
            {
                for(int j=i+1; j<n; j++)
                {
                    if(sumcon(i,j) == n)
                    {
                        Console.WriteLine(i+ "-" + j);
                    }
                }
            }
        }

        public static int sumcon(int begin, int end)
        {
            var s = (begin + end) * (end - begin + 1) / 2;
            return s;
        }
    }
}
