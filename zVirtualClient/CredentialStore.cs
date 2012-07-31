
using System;

namespace zVirtualClient
{
    public class CredentialStore
    {
        private Configuration.IConfigurationReader ConfigurationReader;
        public CredentialStore(Configuration.IConfigurationReader configurationReader = null)
        {
            ConfigurationReader = configurationReader;
            if (ConfigurationReader == null)
            {
#if WINDOWS_PHONE
                ConfigurationReader = new Configuration.IsolatedStorageConfigurationReader();
#else
                ConfigurationReader = new Configuration.AppConfigConfigurationReader();
#endif
            }

            Credentials = new Credentials();
            var profilesIndex = ConfigurationReader.ReadSetting<string>("Profiles");
            if (!string.IsNullOrEmpty(profilesIndex))
            {
                Credentials = Newtonsoft.Json.JsonConvert.DeserializeObject<Credentials>(profilesIndex);
                foreach (var c in Credentials)
                {
                    if (c.Default)
                    {
                        DefaultCredential = c;
                        break;
                    }
                }
            }

            SetupDefault();
        }

        private void SetupDefault()
        {
            if (DefaultCredential == null)
            {
                DefaultCredential = new Credential();
                DefaultCredential.Default = true;
                DefaultCredential.Name = "Profile 1";
                DefaultCredential.Host = "localhost";
                DefaultCredential.Port = 6000;
                DefaultCredential.Password = "password";
                Credentials.Add(DefaultCredential);
                this.Save();
            }
        }

        public Credential DefaultCredential { get; set; }
        public Credentials Credentials { get; set; }

        public Credential Find(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                Credential found = null;
                foreach (var c in this.Credentials)
                {
                    if (c.Name == Name)
                    {
                        return c;
                        break;
                    }
                }
            }
            return null;
        }
        public void Save()
        {
            try
            {
                string payload = Newtonsoft.Json.JsonConvert.SerializeObject(Credentials);
                if (!string.IsNullOrEmpty(payload))
                {
                    ConfigurationReader.WriteSetting("Profiles", payload);
                }
            }
            catch (Exception e) 
            {
                    
                throw;
            }
        }
        public void UpdateCredential(string Name, Credential NewCredential)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                Credential updateMe = null;
                foreach (var c in this.Credentials)
                {
                    if (c.Name == Name)
                    {
                        updateMe = c;
                        
                    }

                }
                if(updateMe!=null)
                {
                    updateMe.Default = NewCredential.Default;
                    updateMe.Domain = NewCredential.Domain;
                    updateMe.Host = NewCredential.Host;
                    updateMe.Password = NewCredential.Password;
                    updateMe.Port = NewCredential.Port;
                    updateMe.Username = NewCredential.Username;
                    updateMe.Name = NewCredential.Name;
                    if (updateMe.Default)
                    {
                        this.DefaultCredential = updateMe;
                        foreach (var c in this.Credentials)
                        {
                            if (c.Name != Name) c.Default = false;

                        }
                    }

                    this.Save();
                }
            }
        }

        public void SetDefault(string Name)
        {
            foreach (var c in this.Credentials)
            {
                if(c.Name == Name)
                {
                    this.DefaultCredential = c;
                    this.DefaultCredential.Default = true;
                } else
                {
                    c.Default = false;
                }
            }
            Save();
        }
        public void AddCredential(Credential Credential, bool MakeDefault = true)
        {
            if (Credential != null)
            {
                Credential.Default = false;
                if (MakeDefault)
                {
                    foreach (var c in this.Credentials)
                    {
                        c.Default = false;
                    }
                    Credential.Default = true;
                }
                this.Credentials.Add(Credential);
                this.Save();
            }
        }
        public void RemoveCredential(string Name)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                Credential deleteMe = null;
                foreach (var c in this.Credentials)
                {
                    if (c.Name == Name)
                    {
                        deleteMe = c;
                        break;
                    }
                }
                if (deleteMe != null)
                {
                    this.Credentials.Remove(deleteMe);
                    if (deleteMe.Default)
                    {
                        DefaultCredential = null;
                        SetupDefault();
                    }
                    else
                    {
                        this.Save();
                    }
                }

            }
        }
    }
}