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
    public class SceneViewModel : INotifyPropertyChanged
    {
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