using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Coding4Fun.Phone.Controls;
using Microsoft.Phone.Controls;
using zVirtualClient;

namespace VirtualClient7
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            // Set the data context of the listbox control to the sample data
            this.devicesContentPanel.DataContext = App.DevicesViewModel;
            this.scenesContentPanel.DataContext = App.ScenesViewModel;

            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }


        // Handle selection changed on ListBox
        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (devicesMainListBox.SelectedIndex == -1)
                return;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + devicesMainListBox.SelectedIndex, UriKind.Relative));

            // Reset selected index to -1 (no selection)
            devicesMainListBox.SelectedIndex = -1;
        }


        // Load data for the DevicesViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            AttemptConnection();
        }

        private void AttemptConnection()
        {

            ConfigurationReader = new zVirtualClient.Configuration.IsolatedStorageConfigurationReader();
            zVirtualClient.HTTP.HttpClient.Timeout = int.MaxValue;
            App.Connected = false;

            creds = new Credentials(ConfigurationReader.ReadSetting<string>("Host"),
                                    ConfigurationReader.ReadSetting<int>("Port"), null,
                                    ConfigurationReader.ReadSetting<string>("Password"));


            if (string.IsNullOrEmpty(creds.Host) || string.IsNullOrEmpty(creds.Password) || creds.Port <= 0)
            {
                NavigationService.Navigate(new Uri("/Connection.xaml", UriKind.RelativeOrAbsolute));
            }
            else
            {
                App.Client = new Client(creds);
                App.Client.OnError += new zVirtualClient.Interfaces.Error(client_OnError);
                App.Client.OnLogin += new zVirtualClient.Interfaces.LoginResponse(client_OnLogin);
                App.Client.OnDevices += new zVirtualClient.Interfaces.DevicesResponse(Client_OnDevices);
                App.Client.OnScenes += new zVirtualClient.Interfaces.SceneResponse(Client_OnScenes);
                App.Client.OnStartScene += new zVirtualClient.Interfaces.SceneNameChangeResponse(Client_OnStartScene);

                App.Client.Login();

                if (!App.DevicesViewModel.IsDataLoaded)
                {
                    //App.DevicesViewModel.LoadData();
                }
            }
        }

        void Client_OnScenes(zVirtualClient.Models.SceneResponse SceneResponse)
        {
            if (SceneResponse.success)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    App.ScenesViewModel.Items.Clear();
                    foreach (var d in SceneResponse.scenes)
                    {
                        App.ScenesViewModel.Items.Add(
                            new SceneViewModel() { Scene = d });
                    }
                    App.ScenesViewModel.IsDataLoaded = true;
                });
            }
        }


        void Client_OnDevices(zVirtualClient.Models.Devices DevicesResponse)
        {
            if (DevicesResponse.success)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    App.DevicesViewModel.Items.Clear();
                    foreach (var d in DevicesResponse.devices)
                    {
                        App.DevicesViewModel.Items.Add(new DeviceViewModel() { Device = d });
                    }
                    App.DevicesViewModel.IsDataLoaded = true;
                    App.Client.Scenes();
                });
            }
        }

        static void client_OnLogin(zVirtualClient.Models.LoginResponse LoginResponse)
        {
            App.Connected = LoginResponse.success;
            if (App.Connected)
            {
                App.Client.Devices();
            }
        }

        static void client_OnError(object Sender, string Message, Exception Exception)
        {
            App.Connected = false;
            if (Exception != null) throw Exception;
        }

        public static zVirtualClient.Configuration.IConfigurationReader ConfigurationReader;
        private static System.Threading.AutoResetEvent evt;

        private Credentials creds;

        private void ForgetCredentialsApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            ConfigurationReader.WriteSetting("Host", "");
            ConfigurationReader.WriteSetting("Port", "0");
            ConfigurationReader.WriteSetting("Password", "");

        }

        private void scenesMainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SceneViewModel svm = (scenesMainListBox.SelectedItem as SceneViewModel);
            if (svm != null)
            {
                App.Client.StartScene(svm.id);
            }
        }
        void Client_OnStartScene(zVirtualClient.Models.SceneNameChangeResponse SceneNameChangeResponse)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                if (SceneNameChangeResponse.success)
                {
                    MessageBox.Show("Success!");
                }
                else
                {
                    MessageBox.Show("Fail! Reason:" + SceneNameChangeResponse.desc);

                }
            });

        }


    }
}