﻿#pragma checksum "C:\Users\Rob\Desktop\zVirtualScenes-Client\zVirtualScenes-Client\VirtualClient7\Connection.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "88EF4B655256676A2716BF852451969C"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace VirtualClient7 {
    
    
    public partial class Connection : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock ApplicationTitle;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.Grid ContentPanel;
        
        internal System.Windows.Controls.TextBlock URLField;
        
        internal System.Windows.Controls.TextBlock HostTitle;
        
        internal System.Windows.Controls.TextBlock PortTitle;
        
        internal System.Windows.Controls.TextBlock PasswordTitle;
        
        internal System.Windows.Controls.TextBox HostInput;
        
        internal System.Windows.Controls.TextBox PortInput;
        
        internal System.Windows.Controls.PasswordBox PasswordInput;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/VirtualClient7;component/Connection.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ApplicationTitle = ((System.Windows.Controls.TextBlock)(this.FindName("ApplicationTitle")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.ContentPanel = ((System.Windows.Controls.Grid)(this.FindName("ContentPanel")));
            this.URLField = ((System.Windows.Controls.TextBlock)(this.FindName("URLField")));
            this.HostTitle = ((System.Windows.Controls.TextBlock)(this.FindName("HostTitle")));
            this.PortTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PortTitle")));
            this.PasswordTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PasswordTitle")));
            this.HostInput = ((System.Windows.Controls.TextBox)(this.FindName("HostInput")));
            this.PortInput = ((System.Windows.Controls.TextBox)(this.FindName("PortInput")));
            this.PasswordInput = ((System.Windows.Controls.PasswordBox)(this.FindName("PasswordInput")));
        }
    }
}
