using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace VirtualClient7.Bluetooth.Channels
{
    public abstract class BaseChannel : IChannel
    {
        public abstract object Read(DataReader reader);
        public abstract void Write(DataWriter writer, object data);

        /// <summary>
        /// Get a string from the serial port.
        /// NOTE: This may not be the most efficient way of receiving strings from the serial port.
        /// </summary>
        /// <returns>The string that has been sent by the other application</returns>
        protected string GetString(DataReader reader)
        {
            byte[] data = GetBytes(reader);
            return new string(System.Text.Encoding.UTF8.GetChars(data));
        }

        /// <summary>
        /// Get a  byte[] from the serial port.
        /// </summary>
        /// <returns>The byte[] that has been sent by the other application</returns>
        protected byte[] GetBytes(DataReader dataReader)
        {

            //var d = dataReader.LoadAsync(1024);
            var buffer = new byte[1024];

            while (dataReader.UnconsumedBufferLength > 0)
            {
                dataReader.ReadBytes(buffer);
            }
            return buffer;

        }

        public delegate void DataRead(byte[] bytes);
        public event DataRead OnDataRead;

        /// <summary>
        /// Receive message through bluetooth.
        /// </summary>
        protected async Task<byte[]> ReceiveMessages(DataReader dataReader)
        {
            try
            {
                
                // Read the message. 
                List<Byte> all = new List<byte>();

                while (true)
                {
                    var bytesAvailable = await dataReader.LoadAsync(1000);
                    var byteArray = new byte[bytesAvailable];
                    dataReader.ReadBytes(byteArray);
                    if (byteArray.Length > 0 && byteArray[0] != byte.MinValue)
                    {
                        if (OnDataRead != null) OnDataRead(byteArray);
                        return byteArray;
                    }

                    Thread.Sleep(100);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return null;
        }
    }
}