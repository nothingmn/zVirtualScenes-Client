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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace VirtualClient7
{
    public class SceneViewModel : INotifyPropertyChanged
    {
        private static Dictionary<string, Uri> _Images;
        public static Dictionary<string, Uri> Images
        {
            get
            {
                if (_Images == null)
                {
                    _Images = new Dictionary<string, Uri>();
                    _Images.Add("exit,leave,off", new System.Uri(App.FixThemeImage("/Images/dark/Exit.png"), UriKind.RelativeOrAbsolute));
                    _Images.Add("play,on,enter", new System.Uri(App.FixThemeImage("/Images/dark/play.png"), UriKind.RelativeOrAbsolute));
                    _Images.Add("movie", new System.Uri(App.FixThemeImage("/Images/dark/movie.png"), UriKind.RelativeOrAbsolute));
                    _Images.Add("work", new System.Uri(App.FixThemeImage("/Images/dark/work.png"), UriKind.RelativeOrAbsolute));
                    _Images.Add("stop", new System.Uri(App.FixThemeImage("/Images/dark/stop.png"), UriKind.RelativeOrAbsolute));
                    _Images.Add("switch", new System.Uri(App.FixThemeImage("/Images/dark/switch.png"), UriKind.RelativeOrAbsolute));
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
        
        public zVirtualClient.Models.Scene Scene { get; set; }
        public int id
        {
            get
            {
                return Scene.id;
            }
            set
            {
                if (value != Scene.id)
                {
                    Scene.id = value;
                    NotifyPropertyChanged("id");
                }
            }
        }
        public int cmd_count
        {
            get { return Scene.cmd_count; }
            set
            {
                if (value != Scene.cmd_count)
                {
                    Scene.cmd_count = value;
                    NotifyPropertyChanged("cmd_count");
                }
            }
        }
        public string name
        {
            get
            {
                return Scene.name;
            }
            set
            {
                if (value != Scene.name)
                {
                    Scene.name = value;
                    NotifyPropertyChanged("name");
                }
            }
        }
        public bool is_running
        {
            get
            {
                return Scene.is_running;
            }
            set
            {
                if (value != Scene.is_running)
                {
                    Scene.is_running = value;
                    NotifyPropertyChanged("is_running");
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