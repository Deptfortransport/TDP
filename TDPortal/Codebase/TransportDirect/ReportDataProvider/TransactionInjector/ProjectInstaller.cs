// *********************************************** 
// NAME			: ProjectInstaller.cs
// AUTHOR		: Jatinder S. Toor
// DATE CREATED	: 03/09/2003 
// DESCRIPTION	: Implementation of the ProjectInstaller class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/ProjectInstaller.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:40:00   mturner
//Initial revision.
//
//   Rev 1.7   Apr 08 2005 14:28:34   RPhilpott
//Add missing using

using System;
using System.Reflection;
using System.ServiceProcess;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;

using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Installer invoked class to perform install/uninstall for the project.
	/// </summary>
	[RunInstaller(true)]
	public class ProjectInstaller : System.Configuration.Install.Installer
	{
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		//private System.ComponentModel.Container components = null;

		public ProjectInstaller()
		{
			// get module name and split it in words
			string[] nameParts = System.Reflection.Assembly.GetExecutingAssembly().GetLoadedModules(false)[0].Name.Split('.');
			// servicename = last word - significant name before extension.
			string serviceName = nameParts[nameParts.Length-2];

			// This call is required by the Designer.
			InitializeComponent();


			System.Configuration.Install.Installer[] installers;
			// Get all services to create
			

			installers = new System.Configuration.Install.Installer[ 2 ];
			installers[0] = new System.ServiceProcess.ServiceProcessInstaller();
			((System.ServiceProcess.ServiceProcessInstaller)installers[0]).Account = ServiceAccount.LocalSystem;

			
			for(int i=1; i < 2; i += 1)
			{
							
				System.ServiceProcess.ServiceInstaller serviceInstaller = new System.ServiceProcess.ServiceInstaller();
				// string serviceName = string.Format("TransactionInjector{0}",i);

				serviceInstaller.ServiceName = serviceName;
				serviceInstaller.DisplayName = serviceName;
				installers[i] = serviceInstaller;

			}
			this.Installers.AddRange(installers);
		
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
