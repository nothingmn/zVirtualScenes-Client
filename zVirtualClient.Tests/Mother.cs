using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Tests
{
    public class Mother
    {
        public static Credentials Credentials
        {
            get
            {
                return new Credentials("http://styx", 8008, null, "password");
            }
        }
    }
}