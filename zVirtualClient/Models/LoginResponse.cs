using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace zVirtualClient.Models
{
    
    public class LoginResponse
    {
        //{"success":true,"isLoggedIn":false}

        
        public bool success { get; set; }

        
        public bool isLoggedIn { get; set; }
    }
}
