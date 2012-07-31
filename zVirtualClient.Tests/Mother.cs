﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace zVirtualClient.Tests
{
    public class Mother
    {
        static CredentialStore store = new CredentialStore(null);
        public static Credential Credential
        {
            get { return store.DefaultCredential; }
        }
    }
}