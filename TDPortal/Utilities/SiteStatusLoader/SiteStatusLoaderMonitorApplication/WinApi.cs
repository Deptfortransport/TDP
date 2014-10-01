// *********************************************** 
// NAME                 : WinApi.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Wrapper class to Windows User32.dll methods
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/SiteStatusLoaderMonitorApplication/WinApi.cs-arc  $
//
//   Rev 1.1   Apr 09 2009 10:46:52   mmodi
//Method to prevent user from dragging form
//Resolution for 5273: Site Status Loader
//
//   Rev 1.0   Apr 06 2009 16:20:22   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader
//

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AO.SiteStatusLoaderMonitorApplication
{
    public class WinApi
    {
        public static readonly int WM_SHOWFIRSTINSTANCE = RegisterWindowMessage("WM_SHOWFIRSTINSTANCE");
        public static readonly int WM_NCLBUTTONDOWN = 161;
        public static readonly int WM_SYSCOMMAND = 274;
        public static readonly int HTCAPTION = 2;
        public static readonly int SC_MOVE = 61456;

        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

        [DllImport("user32.dll")]
        public static extern int RegisterWindowMessage(string message);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// Brings the specified application window name in to focus
        /// </summary>
        /// <param name="windowName"></param>
        public static void ShowToFront(string windowName)
        {
            IntPtr window = FindWindow(null, windowName);
            ShowWindow(window, 1);
            SetForegroundWindow(window);
        }

        /// <summary>
        /// Returns true if the user action is moving the form
        /// </summary>
        /// <param name="message"></param>
        public static bool IsMovingForm(ref Message message)
        {
            if ((message.Msg == WinApi.WM_SYSCOMMAND) && (message.WParam.ToInt32() == WinApi.SC_MOVE))
            {
                return true;
            }

            if ((message.Msg == WinApi.WM_NCLBUTTONDOWN) && (message.WParam.ToInt32() == WinApi.HTCAPTION))
            {
                return true;
            }

            return false;
        }

    }
}
