using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter6
{
    using System.Xml.Linq;
    using System.IO;
    class Person
    {
        public string Name { set; get; }
        public string Lang { set; get; }
    }
    class Animal
    {
        public string Name { set; get; }
        public string Habitat { set; get; }
    }
    class User
    {
        public int ID { set; get; }
        public string Name { set; get; }
        public string Address { set; get; }
        public decimal Salary { set; get; }
    }
    class Owner
    {
        public int ID { set; get; }
        public string Name { set; get; }
    }
    class Company
    {
        public int ID { set; get; }
        public string Name { set; get; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            string[] fruits = new string[] { "Apple", "Mango", "Orange", "Pawpaw", "Pineapple", "Pear", "Avocado" };
            /*LINQ: Method Extension Syntax also called Lambda Syntax Query*/
            IEnumerable<string> pFruits = fruits.Where(p => p.StartsWith("P"));
            foreach(string fruit in pFruits)
            {
                Console.Write(fruit+ " ");
            }
            Console.WriteLine();

            int pFruitCount = fruits.Where(p => p.StartsWith("P")).Count();
            Console.WriteLine("Number of 'p' fruits: {0}", pFruitCount);

            /*LINQ: Query Expression Syntax, also called Query Comprehension*/
            IEnumerable<string> aFruits = from p in fruits where p.StartsWith("A") select p;
            foreach(string fruit in aFruits)
            {
                Console.Write(fruit + " ");
            }
            Console.WriteLine();

            int aFruitCount = (from p in fruits where p.StartsWith("A") select p).Count();
            Console.WriteLine("Number of 'a' fruits: {0}", aFruitCount);

            /*Differed Execution of Query*/
            List<Person> people = new List<Person>
            {
                new Person { Name = "Tochukwu", Lang = "C#"},
                new Person { Name = "Chuks", Lang = "PHP"}
            };

            IEnumerable<Person> peps = from p in people select p;
            people.Add(new Person { Name = "Tochukz", Lang = "dot NET" }); //This will be put in query
            foreach(Person person in peps)
            {
                Console.WriteLine("Name: {0}, Lang: {1}", person.Name, person.Lang);
            }
            /*Immediate Execution of Query*/
            List<Animal> animals = new List<Animal>()
            {
                new Animal{ Name = "Lion", Habitat = "Land"},
                new Animal{ Name = "Shark", Habitat = "Sea"}
            };
            IEnumerable<Animal> selectedAnimals = (from p in animals select p).ToList();
            animals.Add(new Animal { Name = "Sea Lion", Habitat = "Sea" }); //This will NOT be put in query

            foreach(Animal animal in selectedAnimals)
            {
                Console.WriteLine("Animal: {0}, Habitat: {1}", animal.Name, animal.Habitat);
            }

            /*Object Initialization*/
            List<User> users = new List<User>
            {
                new User{ ID = 1, Name = "Chuks", Address = "JHB", Salary = 28910},
                new User{ ID = 2, Name = "Tochukwu", Address = "Durban", Salary = 27000},
                new User{ ID = 3, Name = "ToChuks", Address = "Cape Town", Salary = 26010},
                new User{ ID = 4, Name = "Tochi", Address = "Cape Town", Salary = 24000 },
                new User{ ID = 5, Name = "Tucks", Address = "JHB", Salary = 21300},
                new User{ ID = 6, Name = "TK", Address = "Cape Town", Salary = 23300},
                new User{ ID = 7, Name = "TC", Address = "JHB", Salary = 24300},

            };
            /*Filter operator*/
            IEnumerable<User> users1 = from p in users
                                       where p.Name.Length < 6
                                       select p;
            foreach(User user in users1)
            {
                Console.WriteLine("Name: {0}, Salary: {1}", user.Name, user.Salary);
            }
            /*Projection operators*/
            /*1: Select*/
            IEnumerable<decimal> salaries = from p in users
                                            where p.Salary > 24000
                                            select p.Salary;
            foreach(decimal salary in salaries)
            {
                Console.WriteLine("Salaray {0}", salary);
            }

            /*2: Select Many or Anonymouse Type Query*/
            var users2 = from p in users
                         where p.Salary <= 24000
                         select new
                         {
                             DevName = p.Name,
                             DevSalary = p.Salary
                         };
            foreach(var user in users2)
            {
                Console.WriteLine("DevName: {0}, DevSalary: {1}", user.DevName, user.DevSalary);
            }

            /*Join operator*/
            List<Owner> owners = new List<Owner>
            {
                new Owner{ID = 1, Name = "Bill Gate"},
                new Owner{ID = 2, Name = "Steve Job"},
                new Owner{ID = 3, Name = "Mark Zuck" }
            };
            List<Company> companies = new List<Company>
            {
                new Company{ID = 1, Name = "Microsoft"},
                new Company{ID = 2, Name = "Apple" },
                new Company{ID = 3, Name = "Facebook"}
            };
            var firms = from own in owners
                        join com in companies
                        on own.ID equals com.ID
                        select new
                        {
                            CompID = own.ID,
                            CompName = com.Name,
                            CompOwener = own.Name
                        };
            foreach(var firm in firms)
            {
                Console.WriteLine("{0}) Company Name: {1}, Owner: {2}", firm.CompID, firm.CompName, firm.CompOwener);
            }

            /*Grouping Operator*/
            var groups = from p in users group p by p.Address;
            foreach(var group in groups)
            {
                Console.WriteLine("Address: {0}", group.Key);
                foreach (var user in group)
                {
                    Console.WriteLine("Name: {0}, Address: {1}", user.Name, user.Address);
                }
            }

            /*Partition Operator*/
            var capes = (from p in users where p.Address.StartsWith("C") select p).Take(2);
            Console.WriteLine("::Captonians::");
            foreach (var cape in capes)
            {
                Console.WriteLine("Name: {0}, City: {1}", cape.Name, cape.Address);
            }

            var JHBs = (from p in users where p.Address.StartsWith("J") select p).Skip(2);
            Console.WriteLine("::JHBians::");
            foreach(var JHB in JHBs)
            {
                Console.WriteLine("Name: {0}, City {1}", JHB.Name, JHB.Address);
            }
        

            /*Aggregation Operators*/
            decimal avgJHBSalary = (from p in users where p.Address.Contains("HB") select p.Salary).Average();
            int noOfCape = (from p in users where p.Address.Contains("Cape") select p).Count();
            decimal maxSalary = (from p in users select p.Salary).Max();
            decimal minSalary = (from p in users select p.Salary).Min();

            Console.WriteLine("::Aggregation::");
            Console.WriteLine("Avg JHB Salary = {0}", avgJHBSalary);
            Console.WriteLine("Number of Captonian = {0}", noOfCape);
            Console.WriteLine("Max Salary = {0}", maxSalary);
            Console.WriteLine("Min Salary = {0}", minSalary);

            /*LINQ to XML*/
            /*Create XML document*/
            XElement xmlRoot = new XElement("Developer");
            xmlRoot.Add(new XElement("Name", "Tochukwu"));
            xmlRoot.Add(new XElement("Position", "Senior Dev"));
            xmlRoot.Add(new XElement("Langs", "PHP C#, JavaScript"));
            xmlRoot.Add(new XElement("Salary", 37000));
            xmlRoot.Save("dev.xml");

            /*Update XML document*/
            Console.WriteLine("::Updating XML Document::");
            string xmlString = @"<Developer>
                                    <Name>Tochukwu</Name>
                                    <Position>Senior Dev</Position>
                                    <Langs>PHP C#, JavaScript</Langs>
                                    <Salary>37000</Salary>
                                </Developer>
                                    ";

            XDocument document = XDocument.Parse(xmlString);
            var nameNode = (from p in document.Descendants()
                       where p.Element("Position").Value == "Senior Dev"
                       select p.Element("Langs")).FirstOrDefault();
            Console.WriteLine("Senior Dev Lang: {0}", nameNode.Value);
            //Update
            nameNode.ReplaceWith(new XElement("Skills", "PHP, Xamarin, C#"));
            document.Save("dev2.xml");
            //Remove
            document.Descendants().Where(s => s.Value == "37000").Remove();
            document.Save("dev3.xml");

            /*Reading XML from file*/
            Stream stream = File.Open("dev.xml", FileMode.Open);
            StreamReader reader = new StreamReader(stream);
            string xmlStr = reader.ReadToEnd();
            Console.WriteLine(xmlStr);

            XDocument XDoc = XDocument.Parse(xmlStr);
            var elements = (from p in XDoc.Elements() select p).ToList();
            foreach(var element in elements)
            {
                Console.WriteLine("{0} ", element.Value);
            }
            //Read node name
            var nameOfNode = (from p in XDoc.Elements() select p.Element("Name")).FirstOrDefault();
            Console.WriteLine("Name Node: {0}", nameOfNode);
            //Read node value
            var valueOfNode = (from p in XDoc.Elements() select p.Element("Name").Value).FirstOrDefault();
            Console.WriteLine("Node Value: {0}", valueOfNode);
            //Read based on criteria
            var criteria = (
                            from p in XDoc.Elements()
                            where p.Element("Salary").Value == "37000"
                            select p.Element("Salary").Value
                            ).FirstOrDefault();
            Console.Write("Salary Criteria: {0}", criteria);

            Console.ReadLine();
        }
    }
}
