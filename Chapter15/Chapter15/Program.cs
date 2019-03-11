using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter15
{
    class Program
    {
        static void Main(string[] args)
        {
            int age = 10;
            if(age == 15)
            {
                Console.WriteLine("Control should be here");
            }
            else
            {
                Console.WriteLine("Control should NOT be here");
            }

            Console.ReadLine();
        }
    }
}
