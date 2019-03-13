using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4D
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    
    /*IComparable*/
    class Person : IComparable
    {
        public string Name { set; get; }
        public int Age { set; get; }
        public int CompareTo(object obj)
        {
            Person person = (Person)obj;
            return this.Age.CompareTo(person.Age);
        }
    }
    /*The implementation of IComparable<T> is the same as that of IComparable*/

    /*Implmentation of IComparer*/
    class Student
    {
        public string Name { set; get; }
        public int Score { set; get; }
    }
    class SortName: IComparer{
        public int Compare(object obj1, object obj2)
        {
            Student stu1 = (Student)obj1;
            Student stu2 = (Student)obj2;
            return stu1.Name.CompareTo(stu2.Name);
        }
    }
    class SortScore: IComparer
    {
        public int Compare(object obj1, object obj2)
        {
            Student stu1 = (Student)obj1;
            Student stu2 = (Student)obj2;
            //return stu1.Score.CompareTo(stu2.Score);
            return stu2.Score.CompareTo(stu1.Score);
        }
    }
    /*Implementation of IComparer<in T>*/
    class SortAgeG: IComparer<Student>
    {
        public int Compare(Student stu1, Student stu2)
        {
            return stu1.Score.CompareTo(stu2.Score);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            /*IComparable*/
             
            /*
            ArrayList arrayList = new ArrayList();
            arrayList.Add(new Person { Name = "Tailor", Age = 12 });
            arrayList.Add(new Person { Name = "John", Age = 17 });
            arrayList.Add(new Person { Name = "Maxwel ", Age = 23 });
            */
            
            ArrayList arrayList = new ArrayList
            {
                new Person { Name = "Tailor", Age = 12 },
                new Person { Name = "John", Age = 17 },
                new Person { Name = "Maxwel ", Age = 23 }
            };
            
            arrayList.Sort();

            foreach(Person person in arrayList)
            {
                Console.WriteLine("Name: {0}, Age {1}", person.Name, person.Age);
            }

            /*IComparer*/
            ArrayList students = new ArrayList
            {
                new Student { Name = "Jimi", Score = 67},
                new Student { Name = "Max", Score = 84},
                new Student { Name = "Jamie", Score = 69}
            };

           
            Console.WriteLine("Students sorted by name");
            students.Sort(new SortName());
            foreach(Student student in students)
            {
                Console.WriteLine("{0} {1}", student.Name, student.Score);
            }

            Console.WriteLine("Students sorted by score");
            students.Sort(new SortScore());
            foreach(Student student in students)
            {
                Console.WriteLine("{0} {1}", student.Name, student.Score);
            }
            Console.WriteLine(":::IComparer<in T>:::");
            /*IComparer<in T>*/
            List<Student> learners = new List<Student>
            {
                new Student { Name = "ken", Score = 65},
                new Student { Name = "Ekene", Score = 87},
                new Student { Name = "Henery", Score = 71}
            };
            learners.Sort(new SortAgeG());
            foreach(Student learner in learners)
            {
                Console.WriteLine("{0}, {1}", learner.Name, learner.Score);
            }


            Console.ReadLine();

        }
    }
}
