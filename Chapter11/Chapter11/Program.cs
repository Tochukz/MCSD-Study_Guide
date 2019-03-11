using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Xml.Serialization;
namespace Chapter11
{
    [Serializable]
    public class Developer
    {
        public string Name { set; get; }
        public int Salary { set; get; }
        public string[] Langs { set; get; } = { "C#", "PHP", "JavaScript" };
        [NonSerialized]
        public string DateOfBirth;

        public override string ToString()
        {
            return string.Format($"His name is {Name} he codes in {Langs[0] + ", " + Langs[1] + " and " + Langs[2]}.");

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            /*Using BinaryFormatter for serilization*/
            BinSerial();
            BinDeserial();

            /*Using XmlSerilizer for serialization*/
            XmlSerial();
            XmlDeserial();

            /*Xml Serialization with Attribute Configuration*/
            XmlSerialWithAttr();
            XmlDeserialWithAttr();

            Console.ReadLine();
        }
        static void BinSerial()
        {
            Developer dev = new Developer { Name = "Tochukwu", Salary = 33000, DateOfBirth = "1984" };
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream("dev.txt", FileMode.OpenOrCreate, FileAccess.Write))
            {
                formatter.Serialize(stream, dev);
            }
            Console.WriteLine("Dev object has been binary seralized");
        }
        static void BinDeserial()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using(FileStream stream  = new FileStream("dev.txt", FileMode.Open, FileAccess.Read))
            {               
                Developer dev  = (Developer) formatter.Deserialize(stream);
                Console.WriteLine($"Dev Name: {dev.Name} - Dev Salary: {dev.Salary}");
                Console.WriteLine(dev);
            }
            Console.WriteLine("Dev object has been binary deserialized");
        }
        static void XmlSerial()
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Developer));
            Developer dev = new Developer { Name = "Chukz", Salary = 28000};
            dev.Langs[2] = "TypeScript";
            using (FileStream stream = new FileStream("dev.xml", FileMode.OpenOrCreate, FileAccess.Write))
            {
                xmlSerializer.Serialize(stream, dev);
            }
            Console.WriteLine("Dev has been xml serilized");
        }
        static void XmlDeserial()
        {
            using (FileStream stream = new FileStream("dev.xml", FileMode.Open, FileAccess.Read))
            {
                XmlSerializer serilizer = new XmlSerializer(typeof(Developer));
                Developer dev = (Developer) serilizer.Deserialize(stream);
                Console.WriteLine($"Name: {dev.Name} -  Salary {dev.Salary}");
                Console.WriteLine(dev.ToString());
            }
            Console.WriteLine("dev has been xml deserialized");
        }
        static void XmlSerialWithAttr()
        {
            XmlSerializer serilizer = new XmlSerializer(typeof(User));
            User user = new User
            {
                ID = 1054375,
                Username = "truetochukz",
                Email = "truetochukz@gmail.com",
                City = "Johannesburg",
                FirstName = "Tochukwu",
                LastName = "Nwachukwu",
                DateOFBirth = "84",
                SchoolsAttended = new string[]{ "Uniben", "Wits" },
                Options = new UserOptions
                {
                    HideDateOfBirth = true,
                    HideProfile = false
                }
            };
            using(FileStream stream = new FileStream("user.xml", FileMode.OpenOrCreate, FileAccess.Write))
            {
                serilizer.Serialize(stream, user);
            }
            Console.WriteLine("User object has been xml serilized with attributes confuguration ");
        }
        static void XmlDeserialWithAttr()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(User));
            using(FileStream stream  = new FileStream("user.xml", FileMode.Open, FileAccess.Read))
            {
                User user = (User)serializer.Deserialize(stream);
                Console.WriteLine($"Username: {user.Username} ID: {user.ID}");
                Console.WriteLine(user.GetUserInfo());
            }
            Console.WriteLine("User Object has been xml deserialized with attributes configuration");
        }
    }
}
