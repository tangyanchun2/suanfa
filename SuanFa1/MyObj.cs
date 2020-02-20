using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SuanFa1
{
    //测试Lock this, 一个方法锁定，另一个访问能否访问方法
    //lock 对象。看对象其它方法能否访问
    //方法如何同步
    //多线程复习
    //算法加强
    public class MyObj
    {
        public string s1 = "123";

        public object obj = new object();
        public void Print1()
        {
            lock(this)
            {
                for(int i=0; i<10; i++)
                {
                    Console.WriteLine("Print1 " + i);
                    Thread.Sleep(1000);
                }
            }

        }

    }
}
