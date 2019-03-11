using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Chapter11
{
    [Serializable]
    [XmlRoot("user")]
    public class User
    {
        [XmlAttribute("id")]
        public int ID { set; get; }
        [XmlElement("username")]
        public string Username { set; get; }
        [XmlElement("password")]
        public string Password { set; get; }
        [XmlElement("first_name")]
        public string FirstName { set; get; }
        [XmlElement("last_name")]
        public string LastName { set; get; }
        [XmlElement("email")]
        public string Email { set; get; }
        [XmlElement("city")]
        public string City { set; get; }
        [XmlIgnore]
        public string DateOFBirth { set; get; }
        [XmlArray("education")]
        public string[] SchoolsAttended;
        [XmlElement]
        public UserOptions Options { set; get; }

        public string GetUserInfo()
        {
            return string.Format($"{FirstName} {LastName} studied at {SchoolsAttended[0]} lives in {City}. ");
        }
    }
}
