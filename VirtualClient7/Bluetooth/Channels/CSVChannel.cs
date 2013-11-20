using System;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace VirtualClient7.Bluetooth.Channels
{
    public class CSVChannel : BaseChannel, IChannel
    {
        public CSVChannel()
        {
            Seperator = ',';
        }
        public char Seperator { get; set; }

        /// <summary>
        /// takes our serial port and converts grabs the data, converts it to a string and then returns a string[]
        /// </summary>
        /// <param name="port"></param>
        /// <returns>string[]</returns>
        public override object Read(DataReader reader)
        {
            string[] result = null;
            base.ReceiveMessages(reader).ContinueWith(t =>
                {
                    int stop = 0;
                    for (int x = 0; x <= t.Result.Length; x++)
                    {
                        if (t.Result[x] == byte.MinValue)
                        {
                            stop = x;
                            break;
                        }
                    }
                    string msg = System.Text.UTF8Encoding.UTF8.GetString(t.Result, 0, stop);
                    if (msg != null) result = msg.Split(Seperator);
                }).Wait();
            return result;

        }

        void CSVChannel_OnDataRead(byte[] bytes)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Takes a string[], and converts it to a UTF8 string and then to byte[] and writes it to the serial port
        /// </summary>
        /// <param name="port"></param>
        /// <param name="Data"></param>
        public override void Write(DataWriter writer, object data)
        {
            string[] payload = (data as string[]);
            var d = System.Text.Encoding.UTF8.GetBytes(string.Join(Seperator.ToString(), payload));
            var dNulled = new byte[d.Length+1];
            d.CopyTo(dNulled, 0);
            dNulled[d.Length] = byte.MinValue;
            writer.WriteBytes(dNulled);
        }
    }
}