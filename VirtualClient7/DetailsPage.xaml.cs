using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Navigation;
using Coding4Fun.Phone.Controls;
using Microsoft.Phone.Controls;

namespace VirtualClient7
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        // Constructor
        public DetailsPage()
        {
            InitializeComponent();
            ContentPanel.DataContext = App.DeviceCommandsViewModel;
        }
        private DeviceViewModel model;

        void Client_OnDeviceCommands(zVirtualClient.Models.DeviceCommands DeviceCommandsResponse)
        {
            if (DeviceCommandsResponse.success)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        App.DeviceCommandsViewModel.Items.Clear();
                        foreach (
                            var d in DeviceCommandsResponse.device_commands)
                        {
                            App.DeviceCommandsViewModel.Items.Add(
                                new DeviceCommandViewModel()
                                    {DeviceCommand = d});
                        }
                        App.DeviceCommandsViewModel.IsDataLoaded = true;
                    });
            }
        }

        void Client_OnDeviceCommand(zVirtualClient.Models.DeviceCommandResponse DeviceCommandResponse)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                if (DeviceCommandResponse.success)
                {
                    MessageBox.Show("Success!");
                }
                else
                {
                    MessageBox.Show("Fail! Reason:" + DeviceCommandResponse.reason);

                }
            });

        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string selectedIndex = "";
            if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
            {
                int index = int.Parse(selectedIndex);
                DataContext = App.DevicesViewModel.Items[index];
            }
        }


        private void BackApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            App.Client.OnDeviceCommands -= new zVirtualClient.Interfaces.DeviceCommandsResponse(Client_OnDeviceCommands);
            App.Client.OnDeviceCommand -= new zVirtualClient.Interfaces.DeviceCommandResponse(Client_OnDeviceCommand);
            NavigationService.GoBack();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {

            model = (DeviceViewModel)this.DataContext;
            App.Client.OnDeviceCommand += new zVirtualClient.Interfaces.DeviceCommandResponse(Client_OnDeviceCommand);
            App.Client.OnDeviceCommands += new zVirtualClient.Interfaces.DeviceCommandsResponse(Client_OnDeviceCommands);
            
            App.Client.DeviceCommands(model.Device.id);
        }


        private void commandsMainListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeviceCommandViewModel vm = commandsMainListBox.SelectedItem as DeviceCommandViewModel;
            if (vm != null)
            {

                if (vm.name.StartsWith("TURN"))
                {
                    App.Client.DeviceCommand(this.model.id, vm.name, 0, vm.type);
                }
                else
                {
                    Coding4Fun.Phone.Controls.InputPrompt prompt = new InputPrompt();
                    prompt.Title = "What value would you like to set?";
                    prompt.Message = "New setting:";
                    prompt.InputScope = new InputScope { Names = { new InputScopeName() { NameValue = InputScopeNameValue.Number } } };
                    prompt.Completed += new EventHandler<PopUpEventArgs<string, PopUpResult>>(prompt_Completed);
                    prompt.Show();
                }
            }
        }

        void prompt_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            if (e.PopUpResult == PopUpResult.Ok)
            {
                DeviceCommandViewModel vm = commandsMainListBox.SelectedItem as DeviceCommandViewModel;
                if (vm != null)
                {
                    string cmd = vm.name;
                    string result = e.Result;
                    int arg = 0;
                    if (int.TryParse(result,out arg))
                    {
                        App.Client.DeviceCommand(this.model.id, vm.name, arg, vm.type);
                    }
                }
            }
        }
    }
}