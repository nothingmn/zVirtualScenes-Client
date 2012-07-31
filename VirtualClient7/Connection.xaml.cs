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
using Coding4Fun.Phone.Controls;
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


            if (!string.IsNullOrEmpty(App.CredentialStore.DefaultCredential.Host))
                this.HostInput.Text = App.CredentialStore.DefaultCredential.Host;

            if (App.CredentialStore.DefaultCredential.Port > 0) this.PortInput.Text = App.CredentialStore.DefaultCredential.Port.ToString();
            if (!string.IsNullOrEmpty(App.CredentialStore.DefaultCredential.Password)) this.PasswordInput.Password = App.CredentialStore.DefaultCredential.Password;

            this.HostInput.KeyUp += new KeyEventHandler(HostInput_KeyUp);
            this.PortInput.KeyUp += new KeyEventHandler(HostInput_KeyUp);
            this.ProfileList.SelectionChanged += new SelectionChangedEventHandler(ProfileList_SelectionChanged);
            foreach (var c in App.CredentialStore.Credentials)
            {
                this.ProfileList.Items.Add(c);
            }
            this.ProfileList.Items.Add("Add New...");
            this.ProfileList.SelectedItem = App.CredentialStore.DefaultCredential;
        }

        void ProfileList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ProfileList.SelectedItem != null)
            {
                string name = this.ProfileList.SelectedItem.ToString();
                bool AddNew = (name == "Add New...");
                if (AddNew)
                {
                    this.HostInput.Text = "";
                    this.PasswordInput.Password = "";
                    this.PortInput.Text = "";
                }
                else
                {
                    var c = App.CredentialStore.Find(name);
                    if (c != null)
                    {
                        this.HostInput.Text = (string.IsNullOrEmpty(c.Host) ? "" : c.Host);
                        this.PasswordInput.Password = (string.IsNullOrEmpty(c.Password) ? "" : c.Password);
                        this.PortInput.Text = c.Port.ToString();
                    }
                }
            }
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
            if (this.ProfileList.SelectedItem != null)
            {
                string host = this.HostInput.Text;
                string password = this.PasswordInput.Password;
                int port = Convert.ToInt32(this.PortInput.Text);

                string name = this.ProfileList.SelectedItem.ToString();
                bool AddNew = (name == "Add New...");
                var c = new Credential() {Default = true, Host = host, Name = name, Password = password, Port = port};
                if (AddNew)
                {
                    c.Name = c.Host;
                    App.CredentialStore.AddCredential(c, true);
                    MessageBox.Show("Added your new credential, and set it as the default.");
                }
                else
                {
                    App.CredentialStore.UpdateCredential(name, c);
                    MessageBox.Show("Updated your existing credential.");
                }
            }
        }

        private void RenameButton_Click(object sender, EventArgs e)
        {
            Coding4Fun.Phone.Controls.InputPrompt p = new InputPrompt();
            p.Title = "New Name...";
            p.Completed += new EventHandler<PopUpEventArgs<string, PopUpResult>>(p_Completed);
            p.Show();
        }

        void p_Completed(object sender, PopUpEventArgs<string, PopUpResult> e)
        {
            if (e.PopUpResult == PopUpResult.Ok)
            {
                string oldName = this.ProfileList.SelectedItem.ToString();
                string newName = e.Result;
                var cred = App.CredentialStore.Find(oldName);
                if (cred != null)
                {
                    cred.Name = newName;
                    App.CredentialStore.UpdateCredential(oldName, cred);

                    this.ProfileList.Items.Clear();
                    foreach (var c in App.CredentialStore.Credentials)
                    {
                        this.ProfileList.Items.Add(c);
                    }
                    this.ProfileList.Items.Add("Add New...");
                    this.ProfileList.SelectedItem = cred;
                }
            }
        }
    }
}