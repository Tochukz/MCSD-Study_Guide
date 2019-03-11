using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Chapter11
{
    [Serializable]
    class MySerializer: ISerializable
    {
        public int ID { set; get; }
        public string Username { set; get; }
        public MySerializer()
        {

        }
        protected MySerializer(SerializationInfo info, StreamingContext context)
        {
            this.ID = info.GetInt32("ID");
            this.Username = info.GetString("Username");
        }

        //[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ID", ID);
            info.AddValue("Username", Username);
        }
    }
}
