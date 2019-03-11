using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter14
{
    [MyCustom]
    class Person
    {
        [MyCustom]
        public string Name { set; get; }

        [MyCustom]
        public string City { set; get; }
        public string School;

        [MyCustom]
        public string Details()
        {
            return string.Format($"{Name} {City} {School}");
        }

        public string Schools()
        {
            return "Uniben, Wits";
        }
    }

    [MyCustom(Name = "Tochukwu", City = "Pretoria")]
    class Coder
    {

    }

    [Another("Tochukwu", "Pretoria")]
    class DevOps
    {

    }

    [Limit]
    class User
    {
        [Limit]
        public string Name { set; get; }
        [Limit]
        public string City { set; get; }
        [Limit]
        public string Pass;

        //[Limit] 
        public void MyMethod()
        {
            //Limit attribute restricted from method usage
        }
    } 

    class MyCustomAttribute: Attribute
    {
        public string Name { set; get; }
        public string City { set; get; }
        
    }

    class AnotherAttribute: Attribute
    {
        public string Name { set; get; }
        public string City { set; get; }
        public AnotherAttribute(string name, string city = "Johannesburg")
        {
            Name = name;
            City = city;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field)]
    class LimitAttribute: Attribute
    {
        
    }
}
