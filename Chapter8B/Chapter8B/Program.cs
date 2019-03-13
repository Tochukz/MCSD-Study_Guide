using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Chapter8B
{
    class Program
    {
        static void Main(string[] args)
        {
            Task myTask = new Task(MyMethod);
            myTask.Start();
            myTask.Wait();
            Console.WriteLine("Bye from main method");

            /*Task.Factroty.StartNew() is more efficient than new Task() -> task.Start()*/
            Task task2 = Task.Factory.StartNew(new Action(MyMethod));
            task2.Wait();

            /*Task.Run() is more efficent than Task.Factory.StartNew()*/
            Task task3 = Task.Run(new Action(MyMethod));
            task3.Wait();

            /*Using lambda expression to shedule task*/
            Task task4 = Task.Run(() =>
            {
                Console.WriteLine("Welcome: Lambda Task");
                for(int x=0; x<10; x++)
                {
                    Console.Write($"{x}");
                }
                Console.WriteLine("\nBye: Lambda Task");
            });
            task4.Wait();

            /*Task that returns a value*/
            Task<int> task5 = new Task<int>(MyIntMethod);
            task5.Start();
            int salary = task5.Result;
            Console.WriteLine($"My salary is {salary:C2}");

            /*Using Task.Factory.StartNew() for task that returns a value*/
            Task<int> task6 = Task<int>.Factory.StartNew(MyIntMethod);
            int wage = task6.Result;
            Console.WriteLine($"My wage is {wage:C2}");

            /*Using Task.Run() for task that returns a value*/
            Task<int> task7 = Task.Run<int>(new Func<int>(MyIntMethod));
            int reward = task7.Result;
            Console.WriteLine($"My reward is {reward:C2}");

            /*Using Lambda expression for task that returns a value*/
            Task<int> task8 = Task.Run<int>(() => 33000);
            int pay = task8.Result;
            Console.WriteLine($"My pay is {pay:C2}");

            /*Chaining task with ContinueWith() method*/
            Task task9 = Task.Run(() =>
            {
                Console.WriteLine("Task 9 has completed");
            });
            Task task10 = task9.ContinueWith((tk1) =>{
                Console.WriteLine("Task 10 has completed");
            });
            task10.Wait();

            /*Return a value to next task in a chain*/
            Task<string> task11 = Task.Run(() =>
            {
                return "R33,000";
            });
            Task task12 = task11.ContinueWith((tk11) => {
                Console.WriteLine($"I earn more than {tk11.Result}  every month.");
            });
            task12.Wait();

            /*TaskContinuationOptions: OnlyOnFaulted*/
            Task<string> task13 = Task.Run(() =>
            {
                //throw new Exception();
                Console.WriteLine("Task 14 will not run if I, task 13 run to completion");
                return "Task 13";
            });
            Task task14 = task13.ContinueWith((t13) => 
            {
                Console.WriteLine("Task 14 ran after task 13 failed");
            }, TaskContinuationOptions.OnlyOnFaulted);
            //task14.Wait();

            /*Nested Task */
            Task outerTask = Task.Run(() =>
            {
                Console.WriteLine("Hi from outer task");

                Task innerTask = Task.Run(() =>
                {
                    Console.WriteLine("Hello from inner task");
                    Thread.Sleep(2000);
                    Console.WriteLine("Bye from inner task");
                });

                Thread.Sleep(500);
                Console.WriteLine("Bye from outer task ");
            });
            outerTask.Wait();

            /*Attach child to parent task*/
            Task parent = new Task(() => 
            {
                Console.WriteLine("Hi from parent task");
                Task child = new Task(() =>
                {
                    Console.WriteLine("Hello from child task");
                    Thread.Sleep(2000);
                    Console.WriteLine("Bye from child task");
                }, TaskCreationOptions.AttachedToParent);
                child.Start();

                Thread.Sleep(500);                
                Console.WriteLine("Bye from parent task");
            });
            parent.Start();
            parent.Wait();



            Console.ReadLine();
        }
        static int MyIntMethod()
        {
            return 33000;
        }
        static void MyMethod()
        {
            Console.WriteLine("Welcome from MyMethod");
            for(int i=0; i<10; i++)
            {
                Console.Write($"{i} ");

            }
            Console.WriteLine("\nBye from MyMethod");
        }
    }
}
