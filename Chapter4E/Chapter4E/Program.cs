using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4E
{
    using System;
    using System.Diagnostics;
    using System.Text;
    using System.IO;
    class Student
    {
        public string Name { get; set; }
        public int Age { set; get; }
        public override string ToString()
        {
            //return base.ToString();
            return "Student Name: " + Name + " Student Age: " + Age;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            /*Diagnostics of System.String and System.Text.StringBuilder*/
            Stopwatch watch = new Stopwatch();
            watch.Start();
            //string str = "Test";
            StringBuilder myStr = new StringBuilder("Test");
            for(int i=0; i<100000; i++)
            {
                //str += i;
                myStr.Append(i);
            }
            watch.Stop();
            float milliSec = watch.ElapsedMilliseconds / 1000;
            //Console.WriteLine("Elapsed time:{0} seconds", milliSec);
            Console.WriteLine("Elapsed time:{0} milliseconds", watch.ElapsedMilliseconds);

            /*Using System.IO.StringReader*/
            string me = @" Hi, my name is Chuks.
 I am a software developer.
 I work with JavaScript, PHP and C#.
 I love writing code and solving logic";
            StringReader reader = new StringReader(me);
            string line;
            int currentLine = 0;
            while((line = reader.ReadLine()) != null)
            {
                Console.WriteLine("{0}: {1}", ++currentLine, line);
            }

            /*Using System.IO.StringWriter and System.Text.StringBuilder*/
            StringBuilder builder = new StringBuilder();
            StringWriter writer = new StringWriter(builder);
            writer.WriteLine("Chuks the Programmer!");
            Console.WriteLine(builder.ToString());

            /*String methods*/
            string name = "Tochukwu";
            string tochi = name.Clone() as string;
            Console.WriteLine(tochi);

            string name1 = "Tochukwu";
            string name2 = "Tochi";
            if (name1.CompareTo(name2) == 0)
            {
                Console.WriteLine("Same name");
            }
            else
            {
                Console.WriteLine("Different names");
            }

            Console.WriteLine(name1.EndsWith("i"));

            string animal1 = "Lion";
            string animal2 = "Lion";
            Console.WriteLine(animal1.Equals(animal2));

            Console.WriteLine(animal1.IndexOf("o"));

            Console.WriteLine(animal1.ToUpper());
            Console.WriteLine(animal1.ToLower());
            Console.WriteLine(animal1.Insert(4, " King"));

            string hip = "Hippopotemouse";
            Console.WriteLine(hip.LastIndexOf("o"));
            Console.WriteLine(hip.Remove(3));
            Console.WriteLine(animal1.Replace("L", "Z"));

            string mynames = "Tochi Tochukwu Chuks Nwachukwu TK TC Tucks";
            string[] names = mynames.Split(' ');
            foreach(string n in names)
            {
                Console.WriteLine(n);
            }

            Console.WriteLine(animal1.StartsWith("L"));
            Console.WriteLine(mynames.Substring(6, 8));
            char[] nameChars = name2.ToCharArray();
            foreach(char nameChar in nameChars)
            {
                Console.WriteLine(nameChar);
            }

            string person = " Tochukwu ";
            Console.WriteLine(person);
            Console.WriteLine(person.Trim());

            Student student = new Student { Name = "Ali", Age = 19 };
            Console.WriteLine(student);
            Console.WriteLine(student.ToString());//Exactly the same as the above line


            string user = "Ali";
            int age = 13;
            string userInfo = string.Format("{0} {1}", user, age);
            Console.WriteLine(userInfo);

            double d = 34.8;
            object ob = d;
            int it = (int)(float)(double)ob;
            Console.WriteLine(it.GetType());
            Console.WriteLine(it);

            Console.ReadLine();
        }
    }
}
