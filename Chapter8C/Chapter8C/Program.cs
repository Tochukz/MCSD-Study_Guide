using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Chapter8C
{
    class Numbers
    {
        string[] numbers = {"One", "Two", "Three", "Four", "Five",
        "Six", "Seven", "Eight", "Nine", "Ten"};

        string[] days = { "Sun", "Mon", "Tues", "Wed", "Thu", "Fri", "Sat" };

        int current = 0;

        private object lockKey = new object();

        private object lock2 = new object();

        public string GetNumber(string name, char bar)
        {
            /*Block synchronized using lock keyword. It may bow be said to be "thread safe" or "Atomic"*/
            lock (lockKey)
            {
                StringBuilder str = new StringBuilder(name + "\n");
                for (int x = 0; x < 10; x++)
                {
                    if (current >= numbers.Length)
                    {
                        current = 0;
                    }
                    Random random = new Random();
                    Thread.Sleep(random.Next(100));
                    str.AppendFormat(string.Format($"{x + 1}{bar} {numbers[current++]}\n"));
                }
                return str.ToString();
            }
            
        }
        public string GetDays(string name, char bar)
        {
            Monitor.Enter(lock2);
            try
            {
                StringBuilder str = new StringBuilder(name + "\n");
                for (int i = 0; i < days.Length; i++)
                {
                    if (current >= days.Length)
                    {
                        current = 0;
                    }
                    str.AppendFormat($"{i + 1}{bar} {days[current++]}\n");
                    Random rand = new Random();
                    Thread.Sleep(rand.Next(100));
                }
                return str.ToString();
            }
            finally
            {
                Monitor.Exit(lock2);
            }
            
        }
        public int Add(ref int num)
        {            
           
            Random rand = new Random();
            Thread.Sleep(rand.Next(100));
            return ++num;
            //return Interlocked.Increment(ref num);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            /*Synchronized block using lock keyword*/
            Numbers numbers = new Numbers();
            Task task1 = Task.Run(() =>
            {
                //Calling the sychronized method in the object numbers 
                Console.WriteLine(numbers.GetNumber("Inner Task1", ':'));
            });


            //Calling the same synchronized method in the object numbers 
            Console.WriteLine(numbers.GetNumber("Main Thread", ']'));

            task1.Wait();

            Numbers days = new Numbers();
            Task task2 = Task.Run(() =>
            {
                //Calling the synchronized method in the object days
                Console.WriteLine(days.GetDays("Task 2", '|'));
            });

            //Calling the same synchronized method in the object days
            Console.WriteLine(days.GetDays("Main Thread", ';'));

            /*Using Interlock to synchronize single data points or perate on single point of data atomically*/
            Numbers addition = new Numbers();
            Task task3 = Task.Run(() =>
            {
                //Random random = new Random();
                int num = 30; // random.Next(50);
                Console.WriteLine($"Task3 > {num} + {1} =  {addition.Add(ref num)}");
            });
            //Random rand = new Random();
            int num2 = 25; // rand.Next(50);
            Console.WriteLine($"Main Thread > {num2} + {1} =  {addition.Add(ref num2)}");

            /*CancelationToken*/
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            Task task4 = Task.Run(() => {
                Console.WriteLine("Hello from task4\n");
                while (true)
                {
                    Thread.Sleep(500);
                    Console.Write("*");

                    if(token.IsCancellationRequested == true)
                    {
                        Console.WriteLine("\nBye from task4");
                        return;
                    }
                }
            }, token);

            Console.WriteLine("Main thread after task4");
            Thread.Sleep(2000);
            source.Cancel();
            Console.WriteLine("Bye from main thread after task4");
            Thread.Sleep(500);


            Console.ReadLine();
        }
    }
}
