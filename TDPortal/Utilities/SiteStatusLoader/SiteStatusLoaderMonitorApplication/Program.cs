// *********************************************** 
// NAME                 : Program.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Entry point to application
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/SiteStatusLoaderMonitorApplication/Program.cs-arc  $
//
//   Rev 1.1   Apr 06 2009 16:19:48   mmodi
//Added Code to ensure single instance only can be run for a session
//Resolution for 5273: Site Status Loader
//
//   Rev 1.0   Apr 01 2009 13:34:38   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;

namespace AO.SiteStatusLoaderMonitorApplication
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                // For FindWindow to work, applicationName must be the same as the title (the Text property) of ServiceMonitor form.
                string applicationName = "Site Status Loader Monitor";

                // Code to ensure only one instance of the application is running for a user
                bool onlyInstance = false;
                string mutexName = AssemblyGuid;

                Mutex mutex = new Mutex(true, mutexName, out onlyInstance);


                if (!onlyInstance)
                {
                    IntPtr hwndFirstInstance = WinApi.FindWindow(null, applicationName);
                    WinApi.PostMessage(
                        hwndFirstInstance,
                        WinApi.WM_SHOWFIRSTINSTANCE,
                        IntPtr.Zero,
                        IntPtr.Zero);
                    return;
                }

                // Start the application
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new ServiceMonitor());

                // Needed to prevent the Mutex from expiring and thus allowing multiple instances of this application
                GC.KeepAlive(mutex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    string.Format("Exception thrown by application, press OK to exit application. \n\r{0}", ex.Message),
                    "Site Status Loader Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Returns a unique id
        /// </summary>
        static public string AssemblyGuid
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(System.Runtime.InteropServices.GuidAttribute), false);
                if (attributes.Length == 0)
                {
                    return String.Empty;
                }
                return ((System.Runtime.InteropServices.GuidAttribute)attributes[0]).Value;
            }
        }
    }
}