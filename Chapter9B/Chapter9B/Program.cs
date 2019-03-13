using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Chapter9B
{
    class Program
    {
        static void Main(string[] args)
        {
            startValidation:
            Console.Write("Enter a valid Email Address: ");
            string email = Console.ReadLine();
            string pattern = @"\w+@\w+.\w{2,4}";
            bool matchEmail = Regex.IsMatch(email, pattern);
            if (matchEmail)
            {
                Console.WriteLine($"{email} is a valid email address");
            }
            else
            {
                Console.WriteLine($"{email} is invalid email address");
            }
            goto startValidation;

        }
    }
}
