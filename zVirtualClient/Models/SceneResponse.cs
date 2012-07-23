using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Models
{
    public class SceneResponse
    {
        public bool success { get; set; }
        public List<Scene> scenes { get; set; }
    }
}
