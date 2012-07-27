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

            var port = ConfigurationReader.ReadSetting<string>("Port");
            if (!string.IsNullOrEmpty(port)) this.PortInput.Text = port;

            var pass = ConfigurationReader.ReadSetting<string>("Password");
            if (!string.IsNullOrEmpty(pass)) this.PasswordInput.Password = pass;

            this.HostInput.KeyUp += new KeyEventHandler(HostInput_KeyUp);
            this.PortInput.KeyUp += new KeyEventHandler(HostInput_KeyUp);
        }

 

        void HostInput_KeyUp(object sender, KeyEventArgs e)
        {
            this.URLField.Text = "";
            System.Uri uri;
            string h = this.HostInput.Text;
            string p = this.PortInput.Text;

            if (System.Uri.TryCreate(string.Format("{0}:{1}/API/", h, p), UriKind.Absolute, out uri))
            {
                this.URLField.Text = uri.ToString();
            }
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