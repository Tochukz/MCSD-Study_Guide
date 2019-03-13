using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4C
{
    using System.Collections;
    using System.Collections.Generic;

    /*Implementation for IEnumerable*/
    class MyArrayList: IEnumerable
    {
        object[] arrayList = new object[5];
        int index = -1;
        public void Add(object obj)
        {
          
            if(++index < arrayList.Length)
            {
                arrayList[index] = obj;
            }
            
        }
        public IEnumerator GetEnumerator()
        {
            for(int i=0; i<arrayList.Length; i++)
            {
                yield return arrayList[i];
            }
        }
    }

    /*Implemenatation for IEnumerable<T>*/
    class MyList<T> : IEnumerable<T>
    {
        List<T> list = new List<T>();

        public int Length
        {
            get { return list.Count; }
        }
        public void Add(T item)
        {
            list.Add(item);
        }
        public IEnumerator<T> GetEnumerator()
        {
            foreach(var item in list)
            {
                yield return item;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
    class Person
    {
        public string Job { set; get; }
        public int Salary { set; get; }
    }

    /*::::Implementations for IEnumerator::::*/
    class MyEnumerator: IEnumerator
    {
        Student[] students;
        int index = -1;
        public MyEnumerator(Student[] students)
        {
            this.students = students;
        }
        public object  Current {
            get { return students[index];  }
        }        
        public bool MoveNext()
        {
            return (++index < students.Length);
        }
        public void Reset()
        {
            index = -1;
        }
    }
    class Students: IEnumerable
    {
        Student[] students;
      
        protected int index = -1;

        public Students(int size)
        {
            students = new Student[size];            
        }
        public void Add(string name, string city)
        {
            if(++index < students.Length)
            {
                students[index] = new Student { Name = name, City = city};
            }
            
        }
        public IEnumerator GetEnumerator()
        {
            return new MyEnumerator(students);
        }
    }
    class Student
    {
        public string Name { set; get; }
        public string City { set; get; }
    }
    /*Implementation of IEnumerator<T>*/
    class GeneralList<T> : IEnumerable<T>
    {
        protected T[] list;
        protected int index = -1;
        public GeneralList(int size)
        {
            list = new T[size];
        }
        public void Add(T item)
        {
            if (++index < list.Length)
            {
                list[index] = item;
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            return new GeneralEnum<T>(list);
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        public int Length
        {
            get { return list.Length; }
        }
        
      

    }
    class GeneralEnum<T> : IEnumerator<T>
    {
        T[] list;
        int index = -1;
        public GeneralEnum(T[] list)
        {
            this.list = list;
        }
        public T Current
        {
            get { return list[index]; }
        }
        object IEnumerator.Current{
            get { return this.Current;  }
        }
        public bool MoveNext()
        {
            return (++index < this.list.Length);
        }
        public void Reset()
        {
            index = -1;
        }
        
        public void Dispose()
        {
            //Do anything you like.
        }
    }
    class Animal
    {
        public string Name { set; get; }
        public string Habitat { set; get; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            /*IEnumerable*/
            MyArrayList arrayList = new MyArrayList();
            arrayList.Add("Chchi");
            arrayList.Add(33000);
            arrayList.Add(true);
            arrayList.Add(new { Name = "Tochukwu", Job = "C# Developer" });

            foreach(var item in arrayList)
            {
                Console.WriteLine(item);
            }

            
            Console.WriteLine(":::Ienumerable<T>::::");
            /*IEnumerable<T>*/
            MyList<Person> people = new MyList<Person>();
            people.Add(new Person { Job = "PHP Developer", Salary = 28000 });
            people.Add(new Person { Job = "C# Developer", Salary = 31000 });
            people.Add(new Person { Job = "Full stack Developer", Salary = 33000 });

            Console.WriteLine("No of people: {0}", people.Length);
            foreach(Person person in people)
            {
                Console.WriteLine("TItle: {0}, Reward : {1}", person.Job, person.Salary);
            }

            Console.WriteLine(":::IEnumerator:::");
            /*IEnumerator*/
            Students students = new Students(3);
            students.Add("Chichi", "Lagos");
            students.Add("Meka", "Durban");
            students.Add("Joshua", "Benin");

            foreach(var obj in students)
            {
                //Perhaps MyEnumerator.Enumerator returns Object so we cast it to Student
                Student student = (Student)obj;
                Console.WriteLine("Student Name: {0} - City {1}", student.Name, student.City);
            }

            Console.WriteLine("::::IEnumerator<T>::::");
            /*IEnumerator<T>*/         
            GeneralList<Animal> animals = new GeneralList<Animal>(3);

            animals.Add(new Animal { Name = "Lion", Habitat = "Lithosphere" });
            animals.Add(new Animal { Name = "Sea Lion", Habitat = "Aquatic" });
            animals.Add(new Animal { Name = "Goat", Habitat = "Lithosphere" });

            Console.WriteLine("Number of animals = {0}", animals.Length);

            foreach(var animal in animals)
            {
                //Animal animal = (Animal)obj; //No need to cast
                Console.WriteLine("Animal: {0}, Habitat: {1}", animal.Name, animal.Habitat);
            }


            Console.ReadLine();
        }
    }
}
