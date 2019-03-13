using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter8D
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcurrentDictionary<int, int> nums = new ConcurrentDictionary<int, int>();
            Task task1 = Task.Run(() =>
            {
                for(int i=0; i<10; i++)
                {
                    nums.TryAdd(i, i + 1);
                }
            });
            Task task2 = Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    nums.TryAdd(i + 1, i);
                }
            });

            Task[] allTasks = { task1, task2 };
            Task.WaitAll(allTasks);

            foreach(var item in nums)
            {
                Console.Write($"{item.Key} => {item.Value} \n");
            }

            Console.WriteLine("::Paralell.For::");

            Parallel.For(1, 10, (i) =>
            {
                Console.WriteLine(i);
            });

            Console.WriteLine("::Paralell.Foreach::");

            int[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            Parallel.ForEach<int>(data, (d) =>
            {
                Console.WriteLine(d);
            });

            Console.ReadLine();
        }
    }
}
