using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Networking.Proximity;
using Windows.Storage.Streams;
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

        private ShellTileSchedule SampleTileSchedule = new ShellTileSchedule();
        private bool TileScheduleRunning = false;


        // Handle selection changed on ListBox
        private void MainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (devicesMainListBox.SelectedIndex == -1)
                return;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + devicesMainListBox.SelectedIndex,
                                               UriKind.Relative));

            // Reset selected index to -1 (no selection)
            devicesMainListBox.SelectedIndex = -1;
        }

        private ProgressIndicator prog;
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


            if (string.IsNullOrEmpty(creds.Host) || creds.Host == "localhost" || string.IsNullOrEmpty(creds.Password) ||
                creds.Port <= 0)
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
                App.Client.OnRequestCompleted +=
                    new zVirtualClient.Interfaces.RequestCompleted(Client_OnRequestCompleted);
                App.Client.Login();

                if (!App.DevicesViewModel.IsDataLoaded)
                {
                    //App.DevicesViewModel.LoadData();
                }
            }
        }

        private void Client_OnRequestCompleted(object Sender, string Type)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    SystemTray.SetIsVisible(this, false);
                    //prog.IsVisible = false;
                });
        }

        private void Client_OnRequest(object Sender, string Type)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    SystemTray.SetIsVisible(this, true);
                    //prog.IsVisible = true;
                });
        }

        private void Client_OnScenes(zVirtualClient.Models.SceneResponse SceneResponse)
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


        private void Client_OnDevices(zVirtualClient.Models.Devices DevicesResponse)
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

        private static void client_OnLogin(zVirtualClient.Models.LoginResponse LoginResponse)
        {
            App.Connected = LoginResponse.success;
            if (App.Connected)
            {
                App.Client.Devices();
            }
        }

        private void client_OnError(object Sender, string Message, Exception Exception)
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

        }
        private void ExecuteSelectedScene()
        {
            SceneViewModel svm = (scenesMainListBox.SelectedItem as SceneViewModel);
            if (svm != null)
            {
                  App.Client.StartScene(svm.id);
            }
        }

        private void Client_OnStartScene(zVirtualClient.Models.SceneNameChangeResponse SceneNameChangeResponse)
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

        private ProximityDevice _proximityDevice;
        private long subId = 0;
        private long pubId = 0;

        private void MenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            //SceneViewModel svm = (scenesMainListBox.SelectedItem as SceneViewModel);
            //if (svm != null)
            //{
            if (App.SupportsNFC)
            {
                if (_proximityDevice == null)
                {
                    _proximityDevice = ProximityDevice.GetDefault();
                    if (_proximityDevice != null)
                        subId = _proximityDevice.SubscribeForMessage("WriteableTag", OnWriteableTagArrived);

                    MessageBox.Show("Place the writeable tag near your device, then hit ok.");
                }

            }
            else
            {
                MessageBox.Show("I could not detect a NFC sensor on this device");
            }

            //SceneContextMenu
            //}

        }

        private void OnWriteableTagArrived(ProximityDevice sender, ProximityMessage message)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to write to this NFC Tag?", "Confirmation", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    var dataWriter = new DataWriter();
                    dataWriter.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf16LE;
                    string appLauncher = string.Format(@"vc7nfc:MainPage?source=scene");
                    dataWriter.WriteString(appLauncher);
                    pubId = sender.PublishBinaryMessage("WindowsUri:WriteTag", dataWriter.DetachBuffer());
                    MessageBox.Show("Completed writing to the tag.");
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Could not write to the tag.  Reason:" + e.Message);
                throw;
            }


        }

        private void MenuItem_RunScene_OnClick(object sender, RoutedEventArgs e)
        {
            ExecuteSelectedScene();
        }

        private void GestureListener_Tap(object sender, GestureEventArgs e)
        {
            TextBlock block = sender as TextBlock;
            ContextMenu contextMenu = ContextMenuService.GetContextMenu(block);
            if (contextMenu.Parent == null)
            {
                contextMenu.IsOpen = true;
            }
        }
    }
}