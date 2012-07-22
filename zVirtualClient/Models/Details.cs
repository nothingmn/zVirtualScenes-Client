using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Models
{
    public class Details
    {
        public int id { get; set; }
        public string name { get; set; }
        public string on_off { get; set; }
        public int level { get; set; }
        public string level_txt { get; set; }
        public string type { get; set; }
        public string type_txt { get; set; }
        public string last_heard_from { get; set; }
        public string groups { get; set; }
        public string mode { get; set; }
        public string fan_mode { get; set; }
        public string op_state { get; set; }
        public string fan_state { get; set; }
        public string heat_p { get; set; }
        public string cool_p { get; set; }
        public string esm { get; set; }
        public string plugin_name { get; set; }
    }
}
