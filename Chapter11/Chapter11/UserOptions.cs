using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
namespace Chapter11
{
    [Serializable]
    public class UserOptions
    {
        [XmlElement("hide_email")]
        public bool HideEmail { set; get; }
        [XmlElement("hide_profile")]
        public bool HideProfile { set; get; }
        [XmlElement("hide_date_of_birth")]
        public bool HideDateOfBirth { set; get; }
    }
}
