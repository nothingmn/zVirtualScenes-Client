using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;

namespace zVirtualClient.Helpers.Serialization
{
    public class JSONSerializer<T> : ISerialize<T>
    {
        public static string ToJSON<T>(T Data)
        {
            JSONSerializer<T> js = new JSONSerializer<T>();
            return js.Serialize(Data);
        }
        public static T FromJSON<T>(string Data)
        {
            JSONSerializer<T> js = new JSONSerializer<T>();
            return js.Deserialize(Data);
        }

        public string Serialize(T Data)
        {
            string result = "";
            DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(T));
            using (System.IO.MemoryStream stm = new System.IO.MemoryStream())
            {
                s.WriteObject(stm, Data);
                if (stm.Position > 0) stm.Seek(0, System.IO.SeekOrigin.Begin);
                result = System.Text.Encoding.UTF8.GetString(stm.ToArray());
            }
            return result;
        }

        public T Deserialize(string Data)
        {
            DataContractJsonSerializer s = new DataContractJsonSerializer(typeof(T));
            using (System.IO.MemoryStream stm = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(Data)))
            {
                if (stm.Position > 0) stm.Seek(0, System.IO.SeekOrigin.Begin);
                return (T)s.ReadObject(stm);

            }
        }
    }
}