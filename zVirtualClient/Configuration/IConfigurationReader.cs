using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Configuration
{
    public interface IConfigurationReader
    {
        T ReadSetting<T>(string Key);
        void WriteSetting<T>(string Key, T Value);
    }
}
