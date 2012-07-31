﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Tests
{
    public class Mother
    {
        static CredentialStore store = new CredentialStore();
        public static Credential Credential
        {
            get
            {
                store.AddCredential(new Credential(){ Default=true, Host="home.chartier-family.com", Name="Home", Password="5757", Port=8030});
                store.SetDefault("Home");
                return store.DefaultCredential;
            }
        }
    }
}