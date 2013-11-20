using System.Threading.Tasks;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace VirtualClient7.Bluetooth.Channels
{
    public interface IChannel
    {
        object Read(DataReader reader);
        void Write(DataWriter writer, object data);
    }
}
