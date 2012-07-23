using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Models
{
    public class GroupsResponse
    {
        public bool success { get; set; }
        public List<Group> groups { get; set; }
    }
}
