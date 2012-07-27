using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;


namespace VirtualClient7
{
    public class DeviceCommandsViewModel : INotifyPropertyChanged
    {
        public DeviceCommandsViewModel()
        {
            this.Items = new ObservableCollection<DeviceCommandViewModel>();
            
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<DeviceCommandViewModel> Items { get; private set; }

        public bool IsDataLoaded
        {
            get;
            set;
        }

        public int DeviceID { get; set; }
        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {

            // Sample data; replace with real data
            App.Client.DeviceCommands(DeviceID);
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