#region Amendment history
// *********************************************** 
// NAME			: $Workfile:   ProjectInstaller.cs  $
// AUTHOR		: Peter Norell
// DATE CREATED	: 01/11/2007
// REVISION		: $Revision:   1.1  $
// DESCRIPTION	: Project installer class for running as a service
// ************************************************ 
// $Log:   P:\archives\Codebase\WebTIS\CacheUpService\ProjectInstaller.cs-arc  $ 
//
//   Rev 1.1   Jun 03 2008 10:27:04   p.norell
//Added no dependency configuration when installing.
//
//   Rev 1.0   Nov 02 2007 15:13:08   p.norell
//Initial Revision
//
#endregion
#region Imports
using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
#endregion

namespace WT.CacheUpService
{
    /// <summary>
    /// Summary description for ProjectInstaller.
    /// </summary>
    [RunInstaller(true)]
    public class ProjectInstaller : System.Configuration.Install.Installer
    {
        #region Local declarations
        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller;
        private System.ServiceProcess.ServiceInstaller serviceInstaller;
        #endregion

        #region Constructors
        /// <summary>
        /// Required designer variable.
        /// </summary>
        public ProjectInstaller()
        {
            // This call is required by the Designer.
            InitializeComponent();
        }
        #endregion

        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.serviceProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller = new System.ServiceProcess.ServiceInstaller();
            // 
            // serviceProcessInstaller
            // 
            this.serviceProcessInstaller.Password = null;
            this.serviceProcessInstaller.Username = null;
            // 
            // serviceInstaller
            // 
            this.serviceInstaller.DisplayName = "CupService";
            this.serviceInstaller.ServiceName = "CupService";

            // this.serviceInstaller.BeforeInstall += new System.Configuration.Install.InstallEventHandler(this.OnBeforeInstall);
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller,
            this.serviceInstaller});

        }
        #endregion

        #region Before Install
        private string GetContextParameter(string key, string std)
        {
            string sValue = std;
            try
            {
                sValue = this.Context.Parameters[key];
            }
            catch
            {
                // Can be ignored safely
            }
            return sValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="savedState"></param>
        protected override void OnBeforeInstall(IDictionary savedState)
        {
            base.OnBeforeInstall(savedState);
            // Set the service name based on the command line
            string serviceName = GetContextParameter("NAME", "CupService");
            serviceInstaller.ServiceName = serviceName;
            serviceInstaller.DisplayName = serviceName;

            string strServiceAccount = GetContextParameter("ACCOUNT", "SERVICEACCOUNT");
            ServiceAccount serviceAccount = new ServiceAccount();
            if (strServiceAccount == null || strServiceAccount.Length == 0)
            {
                serviceAccount = ServiceAccount.LocalService;
            }
            else
            {
                serviceAccount = (ServiceAccount)Enum.Parse(typeof(ServiceAccount), strServiceAccount, true);
            }
            serviceProcessInstaller.Account = serviceAccount;

            if (serviceAccount == ServiceAccount.User)
            {
                string serviceUsername = GetContextParameter("USER", null);
                serviceProcessInstaller.Username = serviceUsername;
                string servicePassword = GetContextParameter("PASSWORD", null);
                serviceProcessInstaller.Password = servicePassword;
            }

            string nodep = GetContextParameter("NODEP", "");
            if (string.IsNullOrEmpty(nodep))
            {
                serviceInstaller.ServicesDependedOn = new string[] { "MSMQ" };
            }

            serviceInstaller.StartType = ServiceStartMode.Automatic;
        }
        #endregion

        #region Before un-install
        /// <summary>
        /// The OnBeforeUninstall overrid - used for un-installing the correct service.
        /// </summary>
        /// <param name="savedState">The saved state</param>
        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            base.OnBeforeUninstall(savedState);

            // Set the service name based on the command line
            string serviceName = GetContextParameter("NAME", "CupService");
            serviceInstaller.ServiceName = serviceName;
            serviceInstaller.DisplayName = serviceName;

        }
        #endregion
    }
}
