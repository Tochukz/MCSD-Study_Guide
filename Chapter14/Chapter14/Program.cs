using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Chapter14
{
    class ClassA
    {
        public string Name { set; get; }
        public int Salary { set; get; }
    }
    class ClassB: ClassA
    {
        public int Sqrt(int x)
        {
            return x * x;
        }
        public string Username(int id)
        {
            return "User's name obtained using his ID";
        }
    }
    class Worker
    {        
        public string FirstName { set; get; }
        public int Salary { set; get; }

        private string Lang { set; get; } = "PHP, C#, JavaScript";
        string FrameWorks = "Laravel, BootStrap, ASP.NET MVC Core";
        private string Experince = "4 years";

        public string Details()
        {
            return String.Format($"{FirstName}, PHP, C#, TypeScript, R{Salary}.");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //Using a custom assembly.
            int fourSquare = MyCustomLibrary.Class1.square(4);
            Console.WriteLine($"Sqaure of 4 is {fourSquare}");

            //Using Reflection to get current assembly name
            Assembly assembly = Assembly.GetExecutingAssembly();
            string assemblyName = assembly.FullName;
            Console.WriteLine(assemblyName);

            //Using Reflection to get all the Types defined in the current assembly
            Type[] types = assembly.GetTypes();
            foreach(Type typ in types)
            {
                Console.WriteLine("Type Name: {0}, Base Type: {1}", typ.Name, typ.BaseType);
            }

            //Using Reflection to read the properties defined in a Type.
            Type type = assembly.GetType("Chapter14.ClassA");
            PropertyInfo[] propInfos = type.GetProperties();
            foreach(PropertyInfo propInfo in propInfos)
            {
                Console.WriteLine("Property Name: {0}, PropertyType: {1}", propInfo.Name, propInfo.PropertyType);
            }

            //Using Reflection to read the method metadata of a Type.
            Type type2 = assembly.GetType("Chapter14.ClassB");
            MethodInfo[] methodInfos = type2.GetMethods();
            foreach(MethodInfo mInfo in methodInfos)
            {
                Console.WriteLine("Method Name: {0}, Return Type: {01}", mInfo.Name, mInfo.ReturnType);
            }
            /*There are other methods in Type object which can be used to get information about events, interfaces, fields etc
             * e.g Type.GetEvents(), Type.GetFields(), Type.GetInterfaces() etc */

            //Using Reflection to read the property of an object.
            Worker me = new Worker { FirstName = "Tochukwu", Salary = 33000 };
            Type workerType = typeof(Worker); //In place of the typeof operator you can also use the GetType method e.g me.GetType();
            PropertyInfo workerNameProp = workerType.GetProperty("FirstName");
            var firstNameValue = workerNameProp.GetValue(me);
            Console.WriteLine("{0} - {1}", workerNameProp.Name, firstNameValue);

            //Using Reflection to set and read property of an object.
            PropertyInfo workerSalaryProp = workerType.GetProperty("Salary");
            workerSalaryProp.SetValue(me, 36000);
            var salaryValue = workerSalaryProp.GetValue(me);
            Console.WriteLine("{0} - {1}", workerSalaryProp.Name, salaryValue);

            //Using Reflection to invoke a method in an object
            Worker dev = new Worker { FirstName = "Tochukwu", Salary = 35000 };
            Type devWorker = dev.GetType();
            MethodInfo devInfo = devWorker.GetMethod("Details");
            var devDetails = devInfo.Invoke(dev, null);
            Console.WriteLine($"Developer details: {devDetails}");

            //Using Reflection to read private instance properties.
            Worker coder = new Worker();
            Type coderType = coder.GetType();
            PropertyInfo[] coderInfos = coderType.GetProperties(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach(PropertyInfo pInfo in coderInfos)
            {
                Console.WriteLine($"Property Name: {pInfo.Name}, Property Value:{pInfo.GetValue(coder)}");
            }

            //Using Reflection to read private instance fields.
            //To get public static field use the flags: BindingFlags.Public | BindingFlags.Static
            FieldInfo[] coderFields = coderType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            foreach(FieldInfo fInfo in coderFields)
            {
                Console.WriteLine($"Field name: {fInfo.Name}, Field Value: {fInfo.GetValue(coder)}");
            }

            //Getting Types marked with MyCustom Attribute
            IEnumerable<Type> typesWithAttr = TypesWithAttr();

            //Getting Properties marked with  MyCustom Attribute
            PropWithAttr(typesWithAttr);

            //Gettting Methogs marked with MyCustom Attribute
            MethodWithAttr(typesWithAttr);

            //Getting properties of MyCustom Attribute
            PropOfCustomAttr();

            //Attribute defined with a constructor
            AttrConstrutor();

            //Restricting Attribute Usage  using AttributeUsage and AttributeTargets enum.
            AttrUsage();

            Console.ReadLine();
        }
        static IEnumerable<Type> TypesWithAttr()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            var typesWithAttr = from typ in types where typ.GetCustomAttributes<MyCustomAttribute>().Count() > 0 select typ;
            foreach(Type type in typesWithAttr)
            {
                Console.WriteLine("Type With Attr Name: {0}", type.Name);
            }
            return typesWithAttr;
        }
        static void PropWithAttr(IEnumerable<Type> types)
        {            
            foreach(Type type in types)
            {
                var properties = from prop in type.GetProperties() where prop.GetCustomAttributes<MyCustomAttribute>().Count() > 0 select prop;
                foreach(var property in properties)
                {
                    Console.WriteLine($"Property Name:{property.Name}, PropertyType: {property.PropertyType}");
                }
            }
        }
        static void MethodWithAttr(IEnumerable<Type> types)
        {
            foreach(Type type in types)
            {
                var methods = from meth in type.GetMethods() where meth.GetCustomAttributes<MyCustomAttribute>().Count() > 0 select meth;
                foreach(var method in methods)
                {
                    Console.WriteLine($"Method Name: {method.Name}, Method Return Type: {method.ReturnType}");
                }

            }
        }
        static void PropOfCustomAttr()
        {
            Type coderType = typeof(Coder);
            Type mycustomType = typeof(MyCustomAttribute);
            MyCustomAttribute mycustom = (MyCustomAttribute)Attribute.GetCustomAttribute(coderType, mycustomType);
            Console.WriteLine($"MyCustom Attribute Name: {mycustom.Name}");
            Console.WriteLine($"MyCustom Attribute City: {mycustom.City}");
        }
        static void AttrConstrutor()
        {
            Type devOpsType = typeof(DevOps);
            Type anotherAttrType = typeof(AnotherAttribute);
            AnotherAttribute anotherAttr = (AnotherAttribute)Attribute.GetCustomAttribute(devOpsType, anotherAttrType);
            Console.WriteLine($"Another attribue Name: {anotherAttr.Name}");
            Console.WriteLine($"Another attribute City: {anotherAttr.City}");
        }
        static void AttrUsage()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            IEnumerable<Type> types = from type in assembly.GetTypes() where type.GetCustomAttributes<LimitAttribute>().Count() > 0 select type;
            foreach(Type typ in types)
            {
                Console.WriteLine($"Type Name: {typ.Name}");
                IEnumerable<PropertyInfo> propInfos = from prop in typ.GetProperties() where prop.GetCustomAttributes<LimitAttribute>().Count() > 0 select prop;
                foreach(PropertyInfo propInfo in propInfos){
                    Console.WriteLine($"Property Name: {propInfo.Name}, Property Type: {propInfo.PropertyType}");
                }

            }

        }
    }
}
