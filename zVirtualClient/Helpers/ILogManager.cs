using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using zVirtualClient.Helpers;

namespace zVirtualClient.Helpers
{
    public interface ILogManager
    {
        void ConfigureLogging();
        ILog GetLogger<T>();
    }
}
