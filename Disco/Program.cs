using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Disco
{
    class Program
    {
        static Semaphore GuestSem = new Semaphore(15, 15);
        static Mutex m = new Mutex();

        static void Main(string[] args)
        {
            DiscoOpen();
        }

        static void DiscoOpen()
        {
            for (int i = 0; i < 30; i++)
            {
                Thread guestThread = new Thread(GuestStuff);
                guestThread.Name = (i + 1).ToString();
                guestThread.Start();
            }
            Console.ReadKey();
        }

        static void GuestStuff()
        {
            Console.WriteLine("Guest {0} is waiting to enter the disco", Thread.CurrentThread.Name);
            GuestSem.WaitOne();

            m.WaitOne();
            Console.WriteLine("Guest {0} has entered the disco", Thread.CurrentThread.Name);
            Thread.Sleep(1000);
            m.ReleaseMutex();

            Console.WriteLine("Guest {0} is dancing and having fun...", Thread.CurrentThread.Name);
            Thread.Sleep(10000);

            Console.WriteLine("Guest {0} is leaving the disco", Thread.CurrentThread.Name);
            GuestSem.Release(1);
        }
    }
}
