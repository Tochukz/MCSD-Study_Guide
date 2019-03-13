using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4
{
    class GenericClass<T> 
    {
        public T genericField;
        public T GenericMethod(T genericParam)
        {
            this.genericField = genericParam;
            return this.genericField;
        }
        public T GenericProperty { set; get; }
       
    }
    class GenericRef<T> where T:class
    { 
        /*  Other possible restrictions include: 
         *  where T:class  //For reference types only.
         *  where T:struct //For value types only.
         *  where T:IPerson //IPerson is an interface or a Type that implements IPerson
         *  where T:BaseClass //BaseClass is a class or a sub class that inheits from BaseClass
         *  where T:U // T and U are of the same Type e.g T is the child class of U
         *  where T:new() //new() is a Type that has a defined public default constructor.
         * 
         */

    }
    class GenericVal<T> where T:struct
    {
        //Code here
    }
    class GenericPerson<T> where T: Person
    {
        //Code here
    }
    class Person
    {

    }
    class Student: Person
    {

    }

    class Example
    {
        public T NoRestrict<T>(T param)
        {
            return param;
        }
        public T Restrict<T>(T param) where T:class{
            return param;
        }

        public void Multiple<T, U>(T name, U wage) where U : struct where T : class
        {
            Console.WriteLine("Name : {0}, Wage: {1}", name, wage);

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Generic type is a string in this case
            GenericClass<string> genStr = new GenericClass<string>();
            string name = genStr.GenericMethod("Tochukwu");
            genStr.GenericProperty = "Programmer";
            Console.WriteLine("{0}, {1}", name, genStr.GenericProperty);

            //Generic type is now an int in this case
            GenericClass<int> genInt = new GenericClass<int>();
            int salary = genInt.GenericMethod(33000);
            genInt.GenericProperty = 4;
            Console.WriteLine("R{0} - {1}yrs", salary, genInt.GenericProperty);

            /*Generic class with restrictions*/
            GenericRef<Person> person = new GenericRef<Person>();
            GenericVal<int> number = new GenericVal<int>();
            GenericPerson<Person> madu = new GenericPerson<Person>();
            GenericPerson<Student> learner = new GenericPerson<Student>();

            /*Generic method*/
            Example example = new Example();
            int wages = example.NoRestrict(33000);
            GenericRef<Person> someone = example.Restrict(person);
            Console.WriteLine(wages);
            Console.WriteLine(someone);

            example.Multiple<string, int>("Tochi", 33000);
            example.Multiple("Tochi", 33000);




            Console.ReadLine();
        }
    }
}
