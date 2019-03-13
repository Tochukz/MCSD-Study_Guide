using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter9
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                
                Divide(5, 4, null);
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Invalid Argument: {ex.Message}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Invalid input: {ex.Message}");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"General exception: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Program End");
            }

        }
        static void Divide(int x, int y, string s)
        {
            if(s == null)
            {
                throw new ArgumentNullException();
            }
            Console.WriteLine($"{s} {x / y}");
        }

    }
}
