using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Models
{
    public class CommandsResponse
    {
        public bool success { get; set; }
        public List<BuiltinCommand> builtin_commands { get; set; }
    }
}
