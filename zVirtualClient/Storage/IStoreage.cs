using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Storage
{
    interface IStoreage
    {        		
		void Save(string file, System.IO.MemoryStream contents);
		System.IO.MemoryStream Load(string file);
        bool FileExists(string file);
    }
}
