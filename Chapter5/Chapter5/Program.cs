using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter5
{
    public delegate void MyDelegate(string str);
    public delegate int IntDelegate();
    class IntClass
    {
        public int Get15()
        {
            Console.Write("Get15(): ");
            return 15;
        }
        public int Get20()
        {
            Console.Write("Get20(): ");
            return 20;
        }
        public int Get30()
        {
            Console.Write("Get30(): ");
            return 30;
        }
    }
    class EmptyClass
    {
        public static void EmptyMethod()
        {
            Console.WriteLine("EmptyMethod();");
        }
        public static void VoidMethod()
        {
            Console.WriteLine("VoidMethod();");
        }
    }
    class MyClass
    {
        public void Dev(string name, string lang)
        {
            Console.WriteLine("Name : {0} Lang: {1}", name, lang);
        }
        public void DevSal(string name, int salary)
        {
            Console.WriteLine("Name: {0}, Salary: {1}", name, salary);
        }
        public int Add(int x, int y)
        {
            Console.Write("{0} + {1} = ", x, y);
            return x + y;
        }
        public int Subtract(int x, int y)
        {
            Console.Write("{0} - {1} = ", x, y);
            return x - y;
        }
        public string DevName(string name)
        {
            Console.Write("c# Developer name: ");
            return name;
        }
        public bool Even(int x)
        {
            return (x % 2 == 0);
        }


    }

    /* Covariance: 
     * A delegate whose signature has a return type of a base class can accept methods that return a sub class of the base class.
     */
    delegate Person PersonDelegate(string person);
    /*
     * Contravariance: 
     * Methods of a delegate can accept a parameter of a base type when the delegate signature requires a derived type of the base type.
     * This is true provided that the actual argument passed to an instance of the delegate is the actual derived class defined 
     * in the delegate's signature.
     */
    delegate void ChildDelegate(Student student);

    class Person
    {
        public virtual string Name { get; set; }
        
        public void PersonName(Person person)
        {
            Console.WriteLine("Person Name: {0}", person.Name);
        }
        public override string ToString()
        {
            return "Person Name: " + this.Name;
        }
    }
    class Student: Person
    {
        public override string Name { get; set; }
        public Student StudentName(string student)
        {
            return new Student { Name = student };
        }
        public override string ToString()
        {
            return this.Name;
        }

    }

    /*Problems with using delegate*/
    class Room
    {
        public Action<int> roomTemp;
        int temp;
        public int Temperature
        {
            get { return this.temp; }
            set
            {
                this.temp = value;
                if(temp > 60)
                {
                    if(roomTemp != null)
                    {
                        roomTemp(temp);
                    }
                }
            }
        }
        public void Alarm(int temp)
        {
            Console.WriteLine("Turn on the AC the room at {0}degs is too hot.", temp);
        }
        
        public void AcceptAction(Action act)
        {
            act();
        }
        public void AcceptFunc(Func<int, int> func, int x)
        {
            Console.WriteLine("Passing anonymous method of type Func<int int> with return value: int {0}", func(x));
        }
    }
    /*Event*/
    class Employee
    {
        public string Position { set; get; }
        public string Name { set; get; }
        public int Salary { set; get; }
    }
    class Company
    {
        public event Action<Employee> AddSenoir;
        public void AddWoker(Employee worker)
        {
            if((AddSenoir != null) && (worker.Position == "senior"))
            {
                AddSenoir(worker);
            }
        }
        public void OnSenior(Employee worker)
        {
            Console.WriteLine("Senoir Developer Added: {0}", worker.Name);
        }
        public void OnShowSalary(Employee worker)
        {
            Console.WriteLine("Senior Dev Salary {0}", worker.Salary);
        }
    }
    /*Built in delegate for Events: EventHandler*/
    class HotelRoom: EventArgs
    {
        public string Size { set; get; }
        public int Price { set; get; }
    }
    class Hotel
    {
        public event EventHandler BigRoom;
        public event EventHandler ExpensiveRoom;
        public string Name { get; }
        public Hotel(string name)
        {
            Name = name;
        }
        public void HireRoom(HotelRoom room)
        {
            if((BigRoom != null) && (room.Size == "big") && (room.Price > 700))
            {
                BigRoom(this, EventArgs.Empty);
                ExpensiveRoom(this, room);
            }
            else if((BigRoom != null) && (room.Size == "big"))
            {
                BigRoom(this, EventArgs.Empty);
            }else if ((ExpensiveRoom != null) && (room.Price > 700))
            {
                ExpensiveRoom(this, room);
            }

            
        }
        public void OnBigSize(object obj, EventArgs empty)
        {
            Hotel hotel = obj as Hotel;
            Console.WriteLine("Hotel Name: {0}.", hotel.Name);
        }
        public void OnExpensive(object obj, EventArgs args)
        {
            Hotel hotel = obj as Hotel;
            HotelRoom room = args as HotelRoom;
            Console.WriteLine("Room Size {0}, Room Price {1}, @ {2}", room.Size, room.Price, hotel.Name);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            
            MyDelegate del = new MyDelegate(display);
            del("Hello C#");

            MyDelegate del2 = display;
            del2("Chichi");
            del2.Invoke("Chima");

            /*Multicast Delegate*/
            MyDelegate dele = Display;
            dele += Show;
            dele += Screen;
            dele("C# Developer");

            dele -= Show;
            dele("dot NET");

            /*Using the GetInvocationList method to loop through the methods in a delegate*/
            IntClass intClass = new IntClass();
            IntDelegate intDel = intClass.Get15;
            intDel += intClass.Get20;
            intDel += intClass.Get30; 

            foreach(IntDelegate method in intDel.GetInvocationList())
            {
                Console.WriteLine(method());
            }
            /*Built in delegates: Action*/
            Action action = EmptyClass.EmptyMethod;
            action += EmptyClass.VoidMethod;
            action();

            /*Built in deledates: Action<>*/
            MyClass myclass = new MyClass();
            Action<string, string> devLang = myclass.Dev;
            devLang("Tochukwu ", "C#");

            Action<string, int> devSal = myclass.DevSal;
            devSal("Tochukwu ", 33000);

            /*Built in delegates: Func<> */
            Func<string, string> myname = myclass.DevName;
            Console.WriteLine(myname("Tochukwu Nwachukwu"));

            Func<int, int, int> arithmetic = myclass.Add;
            arithmetic += myclass.Subtract;

            foreach(Func<int, int, int> method in arithmetic.GetInvocationList())
            {
                Console.WriteLine(method(10, 5));
            }

            /*Built in delegates: Predicate<T>*/
            Predicate<int> isEven = myclass.Even;
            Console.WriteLine(isEven(7));

            /*Covariance*/
            PersonDelegate personDel = new Student().StudentName;
            Console.WriteLine(personDel("Jimi"));

            /*Contravarience */
            Student student = new Student { Name = "Gomo Briam" };
            ChildDelegate childDel = student.PersonName;
            childDel(student);

            /*Problems with using delegates*/
            Room room = new Room();
            room.roomTemp = room.Alarm;
            room.Temperature = 68;
            //Does not display as expected but contrary to the book's expectation. I don't see the probelm here.
            room.Temperature = 55;
            room.Temperature = 63;

            /*Anonymous methods*/
            Action act = delegate ()
            {
                Console.WriteLine("An anonymouse method of type Action");
            };
            Func<int, int> func = delegate (int num)
            {
                 Console.Write("An anonymouse method of type Func<int, int> returns: ");
                 return num;
            };

            act();
            Console.WriteLine(func(33000));

            /*Passing anonymous method as argument to a method.*/
            room.AcceptAction(delegate ()
            {
                Console.WriteLine("Passing anonymouse method of type Action");
            });
            room.AcceptFunc(delegate (int num)
            {
                return num;
            }, 28000);

            /*Anonymouse methods using Lambda Expression*/
            Action actLamb = () =>
            {
                Console.WriteLine("Anonymouse method of type Action using the Lambda expression");
            };
            Func<int, int> funcLamb = (int salary) =>
            {
                Console.Write("Anonymouse method of type Func<int int> using Lambda expresssion returns: int ");
                return salary;
            };

            actLamb();
            Console.WriteLine(funcLamb(28000)); ;

            /*Single line Lambda Expression*/
            Action singleAct = () => Console.WriteLine("Single line Lambda expresion with no return value");
            Func<int, int> singleFunc= (int x) => x + 1000;

            singleAct();
            Console.WriteLine(singleFunc(18000));

            /*Anonymous methods without specifying parameter type.*/
            Action<string> user = (name) => Console.WriteLine("User name is {0}", name);
            Action<string> username = name => Console.WriteLine("User is {0}", name); //You may omit the parenthesis for single parameter 
            Func<int, int> userID = (id) => id;

            user("truetochukz");
            username("tochukz");
            Console.WriteLine(userID(1054375));

            /*Pass Lambda expression as method argument*/
            room.AcceptAction(() => Console.WriteLine("Labda expression as Method argument"));
            room.AcceptFunc((salary) => salary, 21000);

            /*Events*/
            Employee tochukwu = new Employee { Name = "Tochukwu", Position = "senior", Salary = 32000 };
            Employee tochi = new Employee { Name = "Tochi", Position = "junior", Salary = 18900};
            Employee chuks = new Employee { Name = "Chuks", Position = "senior", Salary = 28000 };
            Company company = new Company();
            company.AddSenoir += company.OnSenior;
            company.AddSenoir += company.OnShowSalary;

            company.AddWoker(tochukwu);
            company.AddWoker(tochi);
            company.AddWoker(chuks);

            /*Using the built in EventHandler delegate*/
            HotelRoom bigRoom = new HotelRoom { Price = 450, Size = "big" };
            HotelRoom smallRoom = new HotelRoom { Price = 350, Size = "small" };
            HotelRoom pricyBigRoom = new HotelRoom { Price  = 950, Size = "big" };
            HotelRoom pricyRoom = new HotelRoom { Price = 850, Size = "medium" };

            List<HotelRoom> hotelRooms = new List<HotelRoom> { bigRoom, smallRoom, pricyBigRoom, pricyRoom };
            Hotel hotel = new Hotel("Honey Moon Hotel");
            hotel.BigRoom += hotel.OnBigSize;
            hotel.ExpensiveRoom += hotel.OnExpensive;

            foreach (HotelRoom hiredRoom in hotelRooms) {
                hotel.HireRoom(hiredRoom);
            }

            

            Console.ReadLine();
        }
        static void display(string str)
        {
            Console.WriteLine(str);
        }
        static void Display(string msg)
        {
            Console.WriteLine("Display: {0}", msg);
        }
        static void Show(string msg)
        {
            Console.WriteLine("Show: {0}", msg);
        }
        static void Screen(string msg)
        {
            Console.WriteLine("Screen: {0}", msg);
        }
    }
}
