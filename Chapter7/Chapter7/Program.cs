using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter7
{
    using System.IO;
    class MyClass: IDisposable
    {
        StreamReader reader;
        public void Dispose()
        {
            if(reader != null)
            {
                reader.Dispose();
            }
            GC.SuppressFinalize(this);
            Console.WriteLine("Disposed: o0o");
        }
    }
    /*Disposable Pattern*/
    class MyDispose: IDisposable
    {
        bool disposed = false;
        StreamReader reader;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
                if(reader != null)
                {
                    reader.Dispose();
                    
                }
                //Free other managed objects here
                Console.WriteLine("Disposed by Dispose()");
                disposed = true;         
            }
        }
        ~MyDispose()
        {
            Dispose(false);
            Console.WriteLine("Finalizer/Destructor");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            /*Calling Dispose method using try...finally*/
            MyClass myclass = null;
            try
            {
                myclass = new MyClass();
            }
            finally
            {
                myclass.Dispose();
            }

            /*Calling Dispose method using 'using' statement*/
            using(MyClass myclass1 = new MyClass() )
            {
                //TODO
                
            }
            //The Dispose method implemented in the MyClass object will be called automatocallu after the using block
            Console.WriteLine("End of using block");

            /*Disposable pattern*/
            using(MyDispose mydispose = new MyDispose())
            {
                //TODO
            }
            Console.WriteLine("End of MyDispose");


            Console.ReadLine();
        }
    }
}
