using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media.Imaging;

namespace VirtualClient7
{
    public class DeviceViewModel : INotifyPropertyChanged
    {
        private static Dictionary<string, Uri> _Images;
        public static Dictionary<string, Uri> Images
        {
            get
            {
                if (_Images == null)
                {
                    _Images = new Dictionary<string, Uri>();
                    _Images.Add("christmas,xmas,tree,holiday", new System.Uri("/Images/christmastree.png", UriKind.RelativeOrAbsolute));
                    _Images.Add("living,study,couch,sofa", new System.Uri("/Images/sofa.png", UriKind.RelativeOrAbsolute));
                    _Images.Add("door,exit,entry", new System.Uri("/Images/door.png", UriKind.RelativeOrAbsolute));
                    _Images.Add("fan", new System.Uri("/Images/fan.png", UriKind.RelativeOrAbsolute));
                    _Images.Add("kitchen", new System.Uri("/Images/kitchen.png", UriKind.RelativeOrAbsolute));
                    _Images.Add("bed,bedroom,sleep", new System.Uri("/Images/bed.png", UriKind.RelativeOrAbsolute));

                    _Images.Add("play", new System.Uri("/Images/play.png", UriKind.RelativeOrAbsolute));
                    _Images.Add("stop", new System.Uri("/Images/stop.png", UriKind.RelativeOrAbsolute));

                    _Images.Add("light", new System.Uri("/Images/light.png", UriKind.RelativeOrAbsolute));
                    _Images.Add("switch", new System.Uri("/Images/switch.png", UriKind.RelativeOrAbsolute));
                }
                return _Images;
            }
        }

        public zVirtualClient.Models.Device Device { get; set; }
        public BitmapImage Image
        {
            get
            {

                foreach (string key in Images.Keys)
                {
                    string[] lst = key.Split(',');
                    foreach (var s in lst)
                    {
                        if (this.name.ToLower().Contains(s))
                        {
                            return new BitmapImage(Images[key]);
                        }
                    }
                }

                return new BitmapImage(Images["switch"]);
            }
        }
        

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