using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter2
{
    enum Days: byte
    {
        Mon,
        Tue,
        Wed,
        Thu = 7,
        Fri,
        Sat,
        Sun
    }

    struct Coord
    {
        public int x;
        public int y;
    }

    struct Vector
    {
        public Vector(int a, int b)
        {
            x = a;
            y = b;
            /*You can also do*/
            //this.x = a;
            //this.y = b;
        }
        public int x;
        public int y;

    }

    class Person
    {
        public string name;
        public string job;

        public Person(string name, string job)
        {
            this.name = name;
            this.job = job;
        }
        public void displayPerson()
        {
            Console.WriteLine("Name: {0} | Job: {1}", name, job);
        }
    }

    class Employee : Person
    {
        public Employee(string name, string job): base(name, job)
        {

        }
    }

    static class Lang
    {
        public static string[] langs;
        static Lang()
        {
            Lang.langs = new string[] { "PHP", "JavaScript", "C#"};
        }
        public static string[] getLang()
        {
            return Lang.langs;
        }
        
        /*An Extension Method*/
        public static bool isGreater(this int origin, int compare)
        {
            if(origin > compare)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    class Byte
    {
        int bit = 8;
        /*User defined Implicit type conversion*/
        public static implicit operator int(Byte num)
        {
            return num.bit;
        }
    }

    /*Explicit user defined conversion*/
    class Worker
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public static explicit operator string(Worker worker)
        {
            return worker.Name;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            Days day = Days.Tue;
            string today;
            switch (day)
            {
                case Days.Mon:
                    today = "Monday";
                    break;
                case Days.Tue:
                    today = "Tuesday";
                    break;
                case Days.Wed:
                    today = "Wednesday";
                    break;
                default:
                    today = "We don't know";
                    break;

            }

            Console.WriteLine(today);
            Console.WriteLine(Days.Fri);

            int dayNo = (int)Days.Fri;
            Console.WriteLine(dayNo);

            Coord coord = new Coord();
            coord.x = 5;
            coord.y = 11;
            Console.WriteLine("x = {0}", coord.x);
            Console.WriteLine("y = {0}", coord.y);

            Vector vector = new Vector(7, 13);
            Console.WriteLine("x = {0}", vector.x);
            Console.WriteLine("y = {0}", vector.y);

            Person person = new Person(job: "c# Programmer", name: "Tochukwu");
            person.displayPerson();

            Employee employee = new Employee("Chuks", "Mobile App Dev");
            employee.displayPerson();

            /*ANONYMOUS TYPE looks much like Javascript Object except for the new key word and "=" operator */
            var person1 = new { name= "Chichi", city = "Benin"};
            Console.WriteLine("Name: {0}, City: {1}", person1.name, person1.city);

            /*Dynamic type */
            dynamic id = 245;
            dynamic user = "Tochukwu";
            Console.WriteLine(id.GetType());
            Console.WriteLine(user.GetType());

            id = "245";
            user = 897;
            Console.WriteLine(id.GetType());
            Console.WriteLine(user.GetType());

            /*Nullable type*/
            bool? ctrl = null;
            ctrl = true;
            Console.WriteLine(ctrl.GetType());

            /*Nullable type (Alternative syntax)*/
            Nullable<bool> isMarried = null;
            isMarried = true;
            Console.WriteLine(isMarried);

            /*Using static class */
            string[] langs = Lang.getLang();
            foreach(string lang in langs)
            {
                Console.Write(lang + " ");
            }
            Console.WriteLine();

            /*Using An Extension Method*/
            int age = 30;
            bool senior = age.isGreater(28);
            Console.WriteLine("Senior is:" + senior);

            /*Type Conversion */
            Employee emp = new Employee("Chichi", "Teacher");
            Console.WriteLine(emp.GetType());

            //Using the 'as' Keyword
            Person empPerson  = emp as Person;
            Console.WriteLine(empPerson.GetType());//Chapter2.Employee

            //Using the 'is' keyword and (type)
            if (emp is Person)
            {
                Person empPers = (Person)emp;
                Console.WriteLine(empPers.GetType()); //Chapter2.Emplyee
            }

            /*Implicit user defined type conversion*/
            Byte bi = new Byte();
            int bn = bi;
            Console.WriteLine(bn);

            /*Explicit user defined conversion*/
            Worker uche = new Worker { Name = "Uchenna", Age = 32 };
            string ucheBoy = (string)uche;
            Console.WriteLine(ucheBoy.GetType());
            Console.WriteLine(ucheBoy);


            Console.ReadLine();


        }
    }
}
