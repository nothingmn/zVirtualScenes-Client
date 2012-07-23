using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Helpers
{
    public class LogManager : ILogManager
    {
#if WINDOWS_PHONE
        public void ConfigureLogging()
        {
        }
        public ILog GetLogger<T>()
        {
            return new WP7Logger<T>();
        }

#else
        public void ConfigureLogging()
        {
            //nunit friendly way to get the loggin config file
            string dll = typeof(LogManager).Assembly.CodeBase;
            System.Uri u;
            System.Uri.TryCreate(dll, UriKind.RelativeOrAbsolute, out u);

            System.IO.FileInfo fi = new System.IO.FileInfo(u.LocalPath);
            string filename = System.IO.Path.Combine(fi.Directory.FullName, "log4net.config");
            if (System.IO.File.Exists(filename))
            {
                System.IO.FileInfo configFile = new System.IO.FileInfo(filename);
                log4net.Config.XmlConfigurator.ConfigureAndWatch(configFile);
            }
        }
        public ILog GetLogger<T>()
        {
            return new log4netLogger<T>();
        }
#endif


    }
}
