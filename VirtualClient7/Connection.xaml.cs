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
using Microsoft.Phone.Controls;
using zVirtualClient;
using zVirtualClient.Configuration;

namespace VirtualClient7
{
    public partial class Connection : PhoneApplicationPage
    {
        public Connection()
        {
            InitializeComponent();
            var host = ConfigurationReader.ReadSetting<string>("Host");
            if (!string.IsNullOrEmpty(host)) this.HostInput.Text = host;
            var pass = ConfigurationReader.ReadSetting<string>("Port");
            if (!string.IsNullOrEmpty(pass)) this.PasswordInput.Password = pass;
            var port = ConfigurationReader.ReadSetting<int>("Password");
            if (!string.IsNullOrEmpty(host)) this.PortInput.Text = port.ToString();

        }

        IsolatedStorageConfigurationReader ConfigurationReader = new IsolatedStorageConfigurationReader();

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string host = this.HostInput.Text;
            string password = this.PasswordInput.Password;
            int port = Convert.ToInt32(this.PortInput.Text);

            ConfigurationReader.WriteSetting("Host", host);
            ConfigurationReader.WriteSetting("Port", port);
            ConfigurationReader.WriteSetting("Password", password);

            NavigationService.GoBack();


        }
    }
}