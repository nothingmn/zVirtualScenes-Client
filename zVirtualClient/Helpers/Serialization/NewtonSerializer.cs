using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Helpers.Serialization
{
    public class NewtonSerializer<T> : ISerialize<T>
    {
        public string Serialize(T Data)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(Data);
        }

        public T Deserialize(string Data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Data);
        }

        public static string ToJSON<T>(T Data)
        {
            NewtonSerializer<T> js = new NewtonSerializer<T>();
            return js.Serialize(Data);
        }
        public static T FromJSON<T>(string Data)
        {
            NewtonSerializer<T> js = new NewtonSerializer<T>();
            return js.Deserialize(Data);
        }
    }
}
