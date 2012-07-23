using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Models
{
    public class Scene
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool is_running { get; set; }
        public int cmd_count { get; set; }
    }
}
