using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;

namespace DailyReportFramework
{
	/// <summary>
	/// Summary description for ProjectInstaller.
	/// </summary>
	[RunInstaller(true)]
	public class ProjectInstaller : System.Configuration.Install.Installer
	{
		private System.ServiceProcess.ServiceProcessInstaller	serviceProcessInstaller;
		private System.ServiceProcess.ServiceInstaller			serviceInstaller;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		//private System.ComponentModel.Container components = null;

		public ProjectInstaller()
		{
			// This call is required by the Designer.
			InitializeComponent();			
		}

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
			// Commenting out the following line forces user and password to be entered on installation.
			// If this line is uncommented then appropriate priveleges to resources (eg MSMQ) must be granted.
			//this.serviceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;			

			this.serviceProcessInstaller.Password = "";
			this.serviceProcessInstaller.Username = "";		
			// 
			// serviceInstaller
			// 
			this.serviceInstaller.DisplayName = "TransportDirect.Reporting";
			this.serviceInstaller.ServiceName = "TransportDirect.Reporting";
			this.serviceInstaller.StartType		= System.ServiceProcess.ServiceStartMode.Automatic;
			this.serviceInstaller.AfterInstall += new System.Configuration.Install.InstallEventHandler(this.serviceInstaller_AfterInstall);


			// 
			// ProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
																					  this.serviceProcessInstaller,
																					  this.serviceInstaller});

		}
		#endregion

		private void serviceInstaller_AfterInstall(object sender, System.Configuration.Install.InstallEventArgs e)
		{
		
		}
	}
}
