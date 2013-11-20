//using System.Threading.Tasks;
//using Windows.Networking.Sockets;
//using Windows.Storage.Streams;

//namespace VirtualClient7.Bluetooth.Channels
//{
//    public class ByteChannel : BaseChannel
//    {

//        public async Task<string> ReadAsync(DataReader reader)
//        {
//            return base.GetBytes(reader);
//        }


//        public override void Write(DataWriter writer, object data)
//        {
//            byte[] d = (data as byte[]);
//            writer.WriteBytes(d);
//        }
//    }
//}