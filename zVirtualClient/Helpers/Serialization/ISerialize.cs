﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Helpers.Serialization
{
    public interface ISerialize<T>
    {
        string Serialize(T Data);
        T Deserialize(string Data);
    }
}