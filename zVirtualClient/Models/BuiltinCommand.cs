using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Models
{
    public class BuiltinCommand
    {
        public int id { get; set; }
        public string friendlyname { get; set; }
        public object helptext { get; set; }
        public string name { get; set; }
        public int arg { get; set; }
    }
}
