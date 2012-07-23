using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Models
{
    public class Group
    {
        public int id { get; set; }
        public string name { get; set; }
        public int count { get; set; }
        public List<Device> devices { get; set; }
    }
}
