using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using VirtualClient7.Bluetooth.Channels;
using Windows.Networking.Proximity;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;
using zVirtualClient.Helpers.Serialization;

namespace VirtualClient7.Bluetooth
{

    public delegate void DataRead(object Data, DateTime Timestamp);

    public delegate void Connected(Connection connection, DateTime Timestamp);

    public class Connection
    {
        private StreamSocket _socket = null;
        private DataWriter _dataWriter = null;
        private DataReader _dataReader = null;
        private IChannel _channel = null;
        private string _deviceName = "";
        public bool Connected { get; set; }

        public Connection(IChannel channel)
        {
            _channel = channel;
            Connected = false;

        }

        public async void ConnectoToDevice(string deviceName)
        {
            _deviceName = deviceName;
            if (await SetupDeviceConn())
            {
                Connected = true;
                if (OnConnected != null) OnConnected(this, DateTime.Now);
            }
        }

        public async void SendData(object payload)
        {
            if (Connected)
            {
                _channel.Write(_dataWriter, payload);
                await _dataWriter.StoreAsync();
            }

        }

        private async Task<bool> SetupDeviceConn()
        {
            //Connect to your paired host PC using BT + StreamSocket (over RFCOMM)
            PeerFinder.AlternateIdentities["Bluetooth:PAIRED"] = "";


            var devices = await PeerFinder.FindAllPeersAsync();

            if (devices.Count == 0)
            {
                MessageBox.Show("No paired device");
                await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-bluetooth:"));
                return false;
            }

            var peerInfo = devices.FirstOrDefault(c => c.DisplayName.ToLower().Contains(_deviceName.ToLower()));
            if (peerInfo == null)
            {
                MessageBox.Show("No paired device");
                await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings-bluetooth:"));
                return false;
            }

            _socket = new StreamSocket();

            //"{00001101-0000-1000-8000-00805f9b34fb}" - is the GUID for the serial port service.
            await _socket.ConnectAsync(peerInfo.HostName, "{00001101-0000-1000-8000-00805f9b34fb}");

            _dataWriter = new DataWriter(_socket.OutputStream);
            _dataReader = new DataReader(_socket.InputStream);
            _dataReader.InputStreamOptions = InputStreamOptions.Partial;

            ReadData();
            return true;
        }

        public event DataRead OnDataRead;
        public event Connected OnConnected;
        private void ReadThread(object state)
        {
            while (true)
            {
                Debug.WriteLine("read loop");
                object result = _channel.Read(_dataReader);
                if (result != null && OnDataRead != null) OnDataRead(result, DateTime.Now);
                Thread.Sleep(100);
            }
            
        }

        private void ReadData()
        {
            System.Threading.ThreadPool.QueueUserWorkItem(new WaitCallback(ReadThread), null);
        }

    }
}