﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Coding4Fun.Phone.Controls;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using zVirtualClient;

namespace VirtualClient7
{

    public partial class App : Application
    {
        private static DevicesViewModel devicesViewModel = null;
        private static ScenesViewModel scenesViewModel = null;
        private static DeviceCommandsViewModel deviceCommandsViewModel = null;

        public static string Theme = "dark";

        public static System.Uri FixThemeImage(System.Uri Uri)
        {
            return new System.Uri(FixThemeImage(Uri.ToString()), UriKind.RelativeOrAbsolute);
        }
        public static string FixThemeImage(string Uri)
        {
            if (Theme != "dark")
            {
                Uri = Uri.Replace("/dark/", string.Format("/{0}/", Theme));
            }
            return Uri;
        }

        public static bool Connected { get; set; }
        static Client client;
        public static Client Client
        {
            get { return client; }
            set { client = value; }
        }

        private static CredentialStore credentialStore;
        public static CredentialStore CredentialStore
        {
            get
            {
                if (credentialStore == null)
                {
                    credentialStore = new CredentialStore();
                }
                return credentialStore;
            }
        }
        /// <summary>
        /// A static DevicesViewModel used by the views to bind against.
        /// </summary>
        /// <returns>The DevicesViewModel object.</returns>
        public static DeviceCommandsViewModel DeviceCommandsViewModel
        {
            get
            {
                // Delay creation of the view model until necessary
                if (deviceCommandsViewModel == null)
                    deviceCommandsViewModel = new DeviceCommandsViewModel();

                return deviceCommandsViewModel;
            }
        }
        /// <summary>
        /// A static DevicesViewModel used by the views to bind against.
        /// </summary>
        /// <returns>The DevicesViewModel object.</returns>
        public static DevicesViewModel DevicesViewModel
        {
            get
            {
                // Delay creation of the view model until necessary
                if (devicesViewModel == null)
                    devicesViewModel = new DevicesViewModel();

                return devicesViewModel;
            }
        }

        /// <summary>
        /// A static DevicesViewModel used by the views to bind against.
        /// </summary>
        /// <returns>The DevicesViewModel object.</returns>
        public static ScenesViewModel ScenesViewModel
        {
            get
            {
                // Delay creation of the view model until necessary
                if (scenesViewModel == null)
                    scenesViewModel = new ScenesViewModel();

                return scenesViewModel;
            }
        }
        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            if (((Color)Application.Current.Resources["PhoneBackgroundColor"] != Colors.Black)) Theme = "light";

            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are handed off GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            // Ensure that application state is restored appropriately
            if (!App.DevicesViewModel.IsDataLoaded)
            {
                App.Client.Login();
            }
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            // Ensure that required application state is persisted here.
            if (App.Client != null) App.Client.PersistCookie();
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            App.Client.PersistCookie();
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }

        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
#if DEBUG
            MessageBox.Show(e.ExceptionObject.ToString());
#else
            MessageBox.Show("There was an error, the app will now exit.");
#endif
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}