// *********************************************** 
// NAME                 : ProjectInstaller.cs 
// AUTHOR               : Jatinder S. Toor
// DATE CREATED         : 18/08/2003 
// DESCRIPTION  :  Performs installtion tasks for service.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/EventReceiver/ProjectInstaller.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:36   mturner
//Initial revision.
//
//   Rev 1.3   Nov 13 2003 21:19:52   geaton
//Change of service name.
//
//   Rev 1.2   Nov 07 2003 08:57:34   geaton
//Added comment re: user/password.
//
//   Rev 1.1   Nov 06 2003 19:54:24   geaton
//Removed redundant key.
//
//   Rev 1.0   Aug 22 2003 11:49:40   jtoor
//Initial Revision

using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;

namespace EventReceiver
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
			this.serviceProcessInstaller	= new System.ServiceProcess.ServiceProcessInstaller();
			this.serviceInstaller			= new System.ServiceProcess.ServiceInstaller();

			// 
			// serviceProcessInstaller
			// 
	
			// Commenting out the following line forces user and password to be entered on installation.
			// If this line is uncommented then appropriate priveleges to resources (eg MSMQ) must be granted.
			//this.serviceProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;			
			//this.serviceProcessInstaller.Password = "";
			//this.serviceProcessInstaller.Username = "";			

			// 
			// serviceInstaller
			// 
			this.serviceInstaller.ServiceName	= "EventReceiver";
			this.serviceInstaller.DisplayName	= "EventReceiver";			

			this.serviceInstaller.StartType		= System.ServiceProcess.ServiceStartMode.Manual;

			// 
			// ProjectInstaller
			// 
			this.Installers.AddRange(new System.Configuration.Install.Installer[] {
																					  this.serviceProcessInstaller,
																					  this.serviceInstaller});

		}
		#endregion
	}
}
