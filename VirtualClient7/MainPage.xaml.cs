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
using Microsoft.Phone.Shell;
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

            SetupTile();

        }


        private void SetupTile()
        {
            var tileCredential = new CredentialStore();
            if (tileCredential.DefaultCredential.Host != "localhost")
            {
                // Updates will happen on a fixed interval. 
                SampleTileSchedule.Recurrence = UpdateRecurrence.Interval;

                // Updates will happen every hour.  Because MaxUpdateCount is not set, the schedule will run indefinitely.
                SampleTileSchedule.Interval = UpdateInterval.EveryHour;

                string color = "Black";
                if (App.Theme == "dark")
                {
                    color = "White";
                }
                var url = string.Format("{0}/tile.png?Color={1}", tileCredential.DefaultCredential.Uri.ToString(), color);
                SampleTileSchedule.RemoteImageUri = new Uri(url);
                SampleTileSchedule.Start();
                TileScheduleRunning = true;
            }
        }
        ShellTileSchedule SampleTileSchedule = new ShellTileSchedule();
        bool TileScheduleRunning = false;


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

        ProgressIndicator prog;
        // Load data for the DevicesViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {

            SystemTray.SetIsVisible(this, true);
            SystemTray.SetOpacity(this, 0.5);
            SystemTray.SetBackgroundColor(this, SystemColors.DesktopColor);
            SystemTray.SetForegroundColor(this, SystemColors.MenuColor);

            prog = new ProgressIndicator();            
            prog.IsVisible = true;
            prog.IsIndeterminate = true;
            prog.Text = "Connecting, please wait...";
            SystemTray.SetProgressIndicator(this, prog);

            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MainPivot.Items.Clear();
                MainPivot.Items.Add(ConnectionPivotItem);
            });


            AttemptConnection();
        }

        private void TweakUIFromConfig(bool isConfigured)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    MainPivot.Items.Clear();
                    SystemTray.SetIsVisible(this, false);
                    if (isConfigured)
                    {
                        MainPivot.Items.Add(DevicesPivotItem);
                        MainPivot.Items.Add(ScenesPivotItem);


                    }
                    else
                    {
                        MainPivot.Items.Add(SetupPivotItem);

                    }

                });
        }

        private void AttemptConnection()
        {

            ConfigurationReader = new zVirtualClient.Configuration.IsolatedStorageConfigurationReader();
            zVirtualClient.HTTP.HttpClient.Timeout = int.MaxValue;
            App.Connected = false;

            creds = App.CredentialStore.DefaultCredential;


            if (string.IsNullOrEmpty(creds.Host) || creds.Host == "localhost"  || string.IsNullOrEmpty(creds.Password) || creds.Port <= 0)
            {
                TweakUIFromConfig(false);
            }
            else
            {
                App.Client = new Client(creds);
                App.Client.OnError += new zVirtualClient.Interfaces.Error(client_OnError);
                App.Client.OnLogin += new zVirtualClient.Interfaces.LoginResponse(client_OnLogin);
                App.Client.OnDevices += new zVirtualClient.Interfaces.DevicesResponse(Client_OnDevices);
                App.Client.OnScenes += new zVirtualClient.Interfaces.SceneResponse(Client_OnScenes);
                App.Client.OnStartScene += new zVirtualClient.Interfaces.SceneNameChangeResponse(Client_OnStartScene);
                App.Client.OnRequest += new zVirtualClient.Interfaces.Request(Client_OnRequest);
                App.Client.OnRequestCompleted += new zVirtualClient.Interfaces.RequestCompleted(Client_OnRequestCompleted);
                App.Client.Login();

                if (!App.DevicesViewModel.IsDataLoaded)
                {
                    //App.DevicesViewModel.LoadData();
                }
            }
        }

        void Client_OnRequestCompleted(object Sender, string Type)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                SystemTray.SetIsVisible(this, false);
                //prog.IsVisible = false;
            });
        }

        void Client_OnRequest(object Sender, string Type)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    SystemTray.SetIsVisible(this, true);
                    //prog.IsVisible = true;
                });
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

                    TweakUIFromConfig(true);

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

        void client_OnError(object Sender, string Message, Exception Exception)
        {           
            App.Connected = false;

            TweakUIFromConfig(false);

        }

        public static zVirtualClient.Configuration.IConfigurationReader ConfigurationReader;
        private static System.Threading.AutoResetEvent evt;

        private Credential creds;

        private void ForgetCredentialsApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Connection.xaml", UriKind.RelativeOrAbsolute));

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

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            App.Client.Devices();
        }

        private void TextBlock_Tap_1(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Connection.xaml", UriKind.RelativeOrAbsolute));
        }


    }
}