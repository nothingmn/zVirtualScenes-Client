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
    public class DeviceViewModel : INotifyPropertyChanged
    {
        public zVirtualClient.Models.Device Device { get; set; }
        public int id
        {
            get
            {
                return Device.id;
            }
            set
            {
                if (value != Device.id)
                {
                    Device.id = value;
                    NotifyPropertyChanged("id");
                }
            }
        }
        public int level
        {
            get
            {
                return Device.level;
            }
            set
            {
                if (value != Device.level)
                {
                    Device.level = value;
                    NotifyPropertyChanged("level");
                }
            }
        }
        public string level_txt
        {
            get
            {
                return Device.level_txt;
            }
            set
            {
                if (value != Device.level_txt)
                {
                    Device.level_txt = value;
                    NotifyPropertyChanged("level_txt");
                }
            }
        }
        public string name
        {
            get
            {
                return Device.name;
            }
            set
            {
                if (value != Device.name)
                {
                    Device.name = value;
                    NotifyPropertyChanged("name");
                }
            }
        }

        public string on_off
        {
            get
            {
                return Device.on_off;
            }
            set
            {
                if (value != Device.on_off)
                {
                    Device.on_off = value;
                    NotifyPropertyChanged("on_off");
                }
            }
        }

        public string plugin_name
        {
            get
            {
                return Device.plugin_name;
            }
            set
            {
                if (value != Device.plugin_name)
                {
                    Device.plugin_name = value;
                    NotifyPropertyChanged("plugin_name");
                }
            }
        }

        public string type
        {
            get
            {
                return Device.type;
            }
            set
            {
                if (value != Device.type)
                {
                    Device.type = value;
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