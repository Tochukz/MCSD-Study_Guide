using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter3
{
    class Student
    {   
        //Indexer
        private float[] heights = { 6.1F, 5.9F, 5.2F, 5.4F };
        public float this[int index]
        {
            set { heights[index] = value; }
            get { return heights[index];  }
        }


        //Auto property defination
        public int age { set; get; }

        //Full proprty defination
        private string name;
        public string Name
        {
            set { this.name = value; }
            get { return this.name;  }
        }
    }

    interface IVehicle
    {
        int Wheels { get; }
        int User();
    }
    /* Implicit interface implemenation */
    class Bike : IVehicle
    {
        
        public int wheels;
        public int Wheels
        {
            get { return wheels; }
        }
        public int User()
        {
            return 10;
        }
    }

    interface IMath
    {
        int Score();
    }
    interface IEnglish
    {
        int Score();
    }
    /* Explicit interface implementation */
    class Learner: IMath, IEnglish
    {
        private int maths = 73;
        private int english = 66;

        int IMath.Score()
        {
            return maths;
        }
        int IEnglish.Score()
        {
            return english;
        }
    }

    /* Operator overloading */
    class Distance
    {
        public int meter { get; set; }
        //Uniary operator overloading
        public static Distance operator ++(Distance distance)
        {
            distance.meter = ++distance.meter;
            return distance;
        }

        //Binary operator overloading
        public static Distance operator +(Distance dist1, Distance dist2)
        {
            int mts = dist1.meter + dist2.meter;
            Distance distance = new Distance { meter = mts };
            return distance;
        }
        public static bool operator >(Distance dist1, Distance dist2)
        {
            return (dist1.meter > dist2.meter);
        }
        public static bool operator <(Distance dist1, Distance dist2)
        {
            return (dist1.meter < dist2.meter);
        }
    }
    class Vehicle
    {
        public virtual void Run()
        {
            Console.WriteLine("Run Vehicle");
        }
    }
    class Car: Vehicle
    {
        public override void Run()
        {
            //base.Run();
            Console.WriteLine("Running Car.");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            Student student = new Student();
            
            //Using the indexer
            Console.WriteLine(student[2]);
            student[2] = 5.7F;
            Console.WriteLine(student[2]);
            
            //Using the auto property
            student.age = 19;
            Console.WriteLine(student.age);
            
            //Using the full property
            student.Name = "Busi";
            Console.WriteLine(student.Name);

            //Implicit interface implementation
            IVehicle vehicle = new Bike();
            Console.WriteLine(vehicle.Wheels);

            //Explicit interface implementation
            Learner learner = new Learner();
            int mathScore = ((IMath)learner).Score();
            int engScore = ((IEnglish)learner).Score();
            Console.WriteLine("Score for math {0}", mathScore);
            Console.WriteLine("Score for english {0}", engScore);

            /* Operator overloading */
            //Uninary operator overloading
            Distance distance = new Distance { meter=20 };
            distance++;
            Console.WriteLine(distance.meter);

            //Binary operator overloading
            Distance dist1 = new Distance { meter = 27 };
            Distance dist2 = new Distance { meter = 13 };
            Distance dist3 = dist1 + dist2;
            Console.WriteLine(dist3.meter);

            Distance dist4 = new Distance { meter = 23 };
            Distance dist5 = new Distance { meter = 46 };
            bool dist = dist5 > dist4;
            Console.WriteLine(dist);

            /*Dynamic polymophism */
            //Virtual method override
            Vehicle car = new Car();
            car.Run();






            Console.ReadLine();
        }
    }
}
