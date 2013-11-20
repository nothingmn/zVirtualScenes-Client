//using System.Threading.Tasks;
//using Windows.Networking.Sockets;
//using Windows.Storage.Streams;

//namespace VirtualClient7.Bluetooth.Channels
//{
//    public class StringChannel : BaseChannel
//    {

//        public override object Read(DataReader reader)
//        {
//            string result = (base.GetString(reader) as string);
//            return result;

//        }


//        public override void Write(DataWriter writer, object data)
//        {
//            string d = (data as string);
//            byte[] payload = System.Text.Encoding.UTF8.GetBytes(d);
//            writer.WriteBytes(payload);
//        }
//    }
//}
