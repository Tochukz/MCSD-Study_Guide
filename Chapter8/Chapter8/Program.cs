using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter8
{
    using System.Threading;
    class Program
    {
        [ThreadStatic]
        static int count = 0;
        static void Main(string[] args)
        {
            //Thread myThread = new Thread(new ThreadStart(MyCustomThread));
            Thread myThread = new Thread(new ParameterizedThreadStart(MyCustomThread));
            // myThread.IsBackground = true;
            //myThread.Start();
            /*
            myThread.Start(5);
            myThread.Join();
            for(int i=0; i<5; i++)
            {
                Thread.Sleep(100);
                Console.Write("{0} ", i);
            }

            Console.WriteLine("\n" + "Hello from main Thread");
            */
            Thread threadOne = new Thread(new ThreadStart(PriorityCount));
            Thread threadTwo = new Thread(new ThreadStart(PriorityCount));
            Thread threadThree = new Thread(new ThreadStart(PriorityCount));

            threadOne.Priority = ThreadPriority.BelowNormal;
            threadTwo.Priority = ThreadPriority.Highest;
            threadThree.Priority = ThreadPriority.AboveNormal;
            /*
             //Runs to infinity.
            threadOne.Start();
            threadTwo.Start();
            threadThree.Start();
            */

            /*
            Thread threadA = new Thread(() =>
            {
                for(int x = 0; x<10; x++)
                {
                    Console.WriteLine($"Thread A {count++}");
                }
            });
            Thread threadB = new Thread(() =>
            {
                for(int y = 0; y<10; y++)
                {
                    Console.WriteLine($"Thread B {count++}");
                }
            });
            threadA.Start();
            threadB.Start();
            */

            ThreadPool.QueueUserWorkItem(new WaitCallback(Pool));
            Console.Write("The main method stays here...");
            Console.ReadLine();
            Console.WriteLine("Main method here again...");

            ThreadPool.QueueUserWorkItem((s) =>
            {
                Console.WriteLine("\nThread pool from Lambda Expression");
            });

            Console.ReadLine();
            
        }
        static void MyCustomThread()
        {
            Console.WriteLine("Hello from Custom thread.");
            for(int i=0; i<10; i++)
            {
                Console.Write("{0} ", i);
            }
            Console.WriteLine("\nBye from custom thread");
        }
        static void MyCustomThread(object obj)
        {
            Console.WriteLine("Hello from custom thread");
            int count = (int)obj;
            for(int x=0; x<count; x++)
            {
                Thread.Sleep(100);
                Console.Write("{0} ",x);
            }
            Console.WriteLine("\nBye from custom thread");
        }
        static void PriorityCount()
        {
            string threadName = Thread.CurrentThread.Name;
            string threadPriority = Thread.CurrentThread.Priority.ToString();
            int count = 0;
            bool stop = false;
            while(stop != true)
            {
                count++;
            }
            Console.WriteLine($"Thread: {threadName} with Priority{threadPriority} has CPU count of {count}");
        }

        static void Pool(object obj)
        {
            Console.WriteLine("\nThread pool from Pool method.");
        }
    }
}
