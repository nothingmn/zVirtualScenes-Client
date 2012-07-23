using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace zVirtualClient.Models
{
    [DataContract]
    public class LoginResponse
    {
        //{"success":true,"isLoggedIn":false}

        [DataMember]
        public bool success { get; set; }

        [DataMember]
        public bool isLoggedIn { get; set; }
    }
}
