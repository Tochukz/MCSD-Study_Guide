using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4B
{
    
    using System.Collections;
    using System.Collections.Generic;
    class Person
    {
        public string Position { set; get; }
        public string Name { set; get; }
    }
    class Student
    {
        public string Name { set; get; }
        public int Age { set; get; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            /*ArrayList*/
            Console.WriteLine(":::::::::::ArrayList:::::::::::::::::::::");
            ArrayList arrayList = new ArrayList();
            arrayList.Add("Tochukwu");
            arrayList.Add(33000);
            arrayList.Add(true);
            arrayList.Add("Chima");
            for(int i=0; i<arrayList.Count; i++)
            {
                Console.WriteLine(arrayList[i]);               
            }
            Console.WriteLine();
            arrayList.Remove("Chima");
            foreach(var item in arrayList)
            {
                Console.WriteLine(item);
            }
            if (arrayList.Contains("Tochukwu"))
            {
                Console.WriteLine("Yes, Tohchukwu is in there.");
            }

            Console.WriteLine("::::::::::::::::Hashtable::::::::::::::");

            /*Hashtable*/
            Hashtable owners = new Hashtable();
            owners.Add("Mark", "Facebook");
            owners.Add("Bill", "Microsoft");
            owners.Add("Paul", "Microsoft");
            owners.Add("Steve", "Apple");

            Console.WriteLine("Steve is the owner of {0}", owners["Steve"]);
            Console.WriteLine();

            if (!owners.ContainsKey("Trump"))
            {
                owners.Add("Trump", "Trump towers");
            }
            foreach(DictionaryEntry item in owners)
            {
                Console.WriteLine("{0} is the owner of {1}", item.Key, item.Value);
            }

            Console.WriteLine();

            var companies =  owners.Values;
            foreach(var company in companies )
            {
                Console.WriteLine(company);
            }

            Console.WriteLine("::::::::::Queue::::::::::::::");
            /*Queue*/
            Queue days = new Queue();
            days.Enqueue("Mon");
            days.Enqueue("Tue");
            days.Enqueue("Wed");
            days.Enqueue("Thu");
            days.Enqueue("Fri");

            Console.WriteLine("{0} is first day of work", days.Dequeue());
            Console.WriteLine("{0} is the second day of work", days.Peek());
            foreach(var day in days)
            {
                Console.WriteLine(day);
            }

            Console.WriteLine("::::::::::::::::Stack:::::::::::::::::");
            Stack history = new Stack();
            history.Push("Google.com");
            history.Push("Twitter.com");
            history.Push("Facebook.com");
            history.Push("Instagram.com");
            history.Push("Snapchat.com");

            Console.WriteLine("Number of sites is {0}", history.Count);
            Console.WriteLine("Last site {0}", history.Pop());
            Console.WriteLine("Second to last {0}", history.Peek());

            foreach(var item in history)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("::::::::::::::::System.Collections.Generic:::::::::::::::");
            /*::::::System.Collections.Generic*/
            /*List<T>*/
            List<Person> people = new List<Person>();
            people.Add(new Person { Name = "Tochukwu", Position = "C# Developer" });
            people.Add(new Person { Name = "Chuks", Position = "dot Net programmer" });
            people.Add(new Person { Name = "Tochi", Position = "PHP developer " });

            foreach(var person in people)
            {
                Console.WriteLine("Name: {0}, Position: {1}", person.Name, person.Position);
            }
            Console.WriteLine();

            List<int> scores = new List<int> {87, 67, 95, 61 };
            scores.Remove(67);
            for(int i=0; i<scores.Count; i++)
            {
                Console.Write(scores[i]+ " ");
            }

            Console.WriteLine("\n:::::::::::Dictinary<TKey, TValue>:::::::::::::::");

            /*Dictionary<TKey, TValue>*/
            Dictionary<string, Student>  students = new Dictionary<string, Student>();
            students.Add("Ade", new Student { Name= "Adeleke Sufiano", Age = 31 });
            students.Add("Bryman", new Student { Name = "Sekelela Gomo", Age = 27 });
            students.Add("Lebo", new Student { Name = "Leboganag Schoole", Age = 25 });

            Console.WriteLine("Student: {0}, {1}", students["Lebo"].Name, students["Lebo"].Age);
            if (!students.ContainsKey("Tamara"))
            {
                students.Add("Tamara", new Student { Name = "Tamara Ponsho", Age = 26 });
            }
            foreach(KeyValuePair<string, Student> student in students)
            {
                Console.WriteLine( "{0}: {1} - {2}", student.Key, student.Value.Name, student.Value.Age);
            }
            Console.WriteLine();
            var studentValues = students.Values;
            foreach(var stu in studentValues)
            {
                Console.WriteLine(stu.Name);
            }

            /*Queue<T>*/
            Console.WriteLine("::Queue<T>::");

            Queue<string> wkDays = new Queue<string>();
            wkDays.Enqueue("Mon");
            wkDays.Enqueue("Tue");
            wkDays.Enqueue("Wed");
            wkDays.Enqueue("Thu");
            wkDays.Enqueue("Frin");

            Console.WriteLine("First Working Day:{0}", wkDays.Dequeue());
            Console.WriteLine("Second Working Day :{0}", wkDays.Peek());
            Console.WriteLine();
            foreach(var dy in wkDays)
            {
                Console.WriteLine(dy);
            }

            /*Stack<T>*/
            Stack<string> hist = new Stack<string>();
            hist.Push("Google.com");
            hist.Push("Yahoo.com");
            hist.Push("Facebook.com");
            hist.Push("Instagram.com");
            hist.Push("Wechat.com");

            Console.WriteLine("Last hist os {0}", hist.Pop());
            Console.WriteLine("Second to last hist is {0}", hist.Peek());
            foreach(var item in hist)
            {
                Console.WriteLine(item);
            }





            Console.ReadLine();


        }
    }
}
