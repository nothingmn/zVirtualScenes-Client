using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace VirtualClient7
{
    public class DeviceCommandViewModel : INotifyPropertyChanged
    {
        public zVirtualClient.Models.DeviceCommand DeviceCommand { get; set; }
        public int id
        {
            get
            {
                return DeviceCommand.id;
            }
            set
            {
                if (value != DeviceCommand.id)
                {
                    DeviceCommand.id = value;
                    NotifyPropertyChanged("id");
                }
            }
        }
        public string friendlyname
        {
            get { return DeviceCommand.friendlyname; }
            set
            {
                if (value != DeviceCommand.friendlyname)
                {
                    DeviceCommand.friendlyname = value;
                    NotifyPropertyChanged("friendlyname");
                }
            }
        }
        public string name
        {
            get { return DeviceCommand.name; }
            set
            {
                if (value != DeviceCommand.name)
                {
                    DeviceCommand.name = value;
                    NotifyPropertyChanged("name");
                }
            }
        }
        public string helptext
        {
            get
            {
                return DeviceCommand.helptext;
            }
            set
            {
                if (value != DeviceCommand.helptext)
                {
                    DeviceCommand.helptext = value;
                    NotifyPropertyChanged("helptext");
                }
            }
        }
        public string type
        {
            get
            {
                return DeviceCommand.type;
            }
            set
            {
                if (value != DeviceCommand.type)
                {
                    DeviceCommand.type = value;
                    NotifyPropertyChanged("type");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}