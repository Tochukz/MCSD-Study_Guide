using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter9A
{
    class MyException: Exception
    {
        public MyException(string message) : base(message)
        {

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                //Divide(5, 4, null);
                Divide(5, 0, "Result = ");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"Invalid Argument: {ex.Message}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Invalid input: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General exception: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Program End");
            }


            try
            {
                Fullname(null, "Nwachukwu");
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine($"Second Exception {ex.StackTrace}");
            }

            Console.WriteLine(":::Inner Exception::");
            try
            {
                Login("tochi", null);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
                Console.WriteLine($"{ex.StackTrace}");
                Console.WriteLine($"{ex.InnerException.Message}");
            }

            Console.WriteLine("::Custom Exception::");

            try
            {
                Custom();
            }
            catch(MyException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            Console.ReadLine();


        }
        static void Divide(int x, int y, string s)
        {
            if (s == null)
            {
                throw new ArgumentNullException();
            }
            Console.WriteLine($"{s} {x / y}");
        }
        static void Fullname(string fname, string lname)
        {
            try
            {
                Console.WriteLine($"{fname.ToLower()} {lname.ToUpper()}");
            }
            catch(NullReferenceException)
            {
                throw;
            }
        }
        static void Login(string user, string pass)
        {
            if(pass == null){
                ArgumentException ex = new ArgumentException("pass is null", pass);
                throw new Exception("Login Exception", ex);
            }
            
        }
        static void Custom()
        {
            throw new MyException("Tochi custom exception");
        }
    }
}
