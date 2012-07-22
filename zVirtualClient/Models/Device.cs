using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Models
{
    public class Device
    {
        public int id { get; set; }
        public string name { get; set; }
        public string on_off { get; set; }
        public int level { get; set; }
        public string level_txt { get; set; }
        public string type { get; set; }
        public string plugin_name { get; set; }
    }
}
