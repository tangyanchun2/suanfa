using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SuanFa1
{
    public class Payment
    {
        private readonly string LockString;
        public readonly int ThreadNo;
        private readonly Object lockObj = new object();
        private static readonly Object StaticLockObj = new object();

        public Payment(string orderID, int threadNo)
        {
            LockString = orderID;
            ThreadNo = threadNo;
        }

        public void Pay(LockType lockType)
        {
            ShowMessage("等待锁资源");
            switch(lockType)
            {
                case LockType.LockThis:
                    lock(this){
                        showAction();
                    }
                    break;
                case LockType.LockString:
                    lock(LockString)
                    {
                        showAction();
                    }
                    break;
                case LockType.LockObject:
                    lock(lockObj)
                    {
                        showAction();
                    }
                    break;
                case LockType.LockStaticObject:
                    lock(StaticLockObj)
                    {
                        showAction();
                    }
                    break;
            }
           // ShowMessage("释放后，case 结束");
        }

        private void showAction()
        {
            ShowMessage("进入锁");
            Thread.Sleep(3000);
            ShowMessage("释放锁");
        }


        public void showLock()
        {
            lock(this)
            {
                ShowMessage("进入锁");
                Thread.Sleep(8000);
                ShowMessage("释放锁");
            }
             
        }

        public void show2Lock()
        {
            lock (this)
            {
                ShowMessage("2进入锁");
                Thread.Sleep(8000);
                ShowMessage("2释放锁");
            }

        }






        private void ShowMessage(string message)
        {
            Console.WriteLine(String.Format("订单{0}， 线程{1}， 信息{2}", LockString, ThreadNo, message));
        }

    }
}
