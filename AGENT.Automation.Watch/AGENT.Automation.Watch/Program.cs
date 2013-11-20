using System;
using Agent.Contrib.Communication.Channels;
using Agent.Contrib.Hardware.Bluetooth;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Microsoft.SPOT.Presentation;
using Microsoft.SPOT.Presentation.Media;
using System.Threading;

namespace AGENT.Automation.Watch
{
    public class Program
    {
        static Bitmap _display;

        public static void Main()
        {
            // initialize display buffer
            _display = new Bitmap(Bitmap.MaxWidth, Bitmap.MaxHeight);

            // sample "hello world" code
            _display.Clear();
            Font fontNinaB = Resources.GetFont(Resources.FontResources.NinaB);
            _display.DrawText("Waiting.", fontNinaB, Color.White, 10, 64);
            _display.Flush();

            var connection = new Connection("COM3", new CSVChannel());
            connection.Open();
            connection.OnReceived+=connection_OnReceived;
            // go to sleep; all further code should be timer-driven or event-driven
            Thread.Sleep(Timeout.Infinite);
        }

        static void connection_OnReceived(object Data, System.IO.Ports.SerialPort port, IChannel channel, DateTime Timestamp)
        {
        }

    }
}
