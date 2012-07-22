using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Models
{
    public class DeviceCommand
    {
        public int id { get; set; }
        public string type { get; set; }
        public string friendlyname { get; set; }
        public string helptext { get; set; }
        public string name { get; set; }
    }
}
