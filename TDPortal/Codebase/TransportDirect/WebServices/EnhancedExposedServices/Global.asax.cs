// *********************************************** 
// NAME                 : Global.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 23/11/2005 
// DESCRIPTION  		: Gloabl class for EnhancedExposedServices.  
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/Global.asax.cs-arc  $ 
//
//   Rev 1.1   May 13 2009 09:47:10   build
//Changed for Tech Refresh to use  AppDomain.CurrentDomain.BaseDirectory for logging path instead of trying to get it from Context.Request, which is not available on app startup under iis7
//
//   Rev 1.0   Nov 08 2007 13:51:46   mturner
//Initial revision.
//
//   Rev 1.3   Feb 15 2006 10:00:08   RWilby
//Changed Global.asax.cs Application_Start method to read remoting configuration settings from the cjp.clent.config.
//Resolution for 3564: Mobile Exposed Services - Exception occurred while calling the CJP for bus arrivals
//
//   Rev 1.2   Jan 25 2006 14:32:48   mdambrine
//added initialisation of the journeyplanner component
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Dec 14 2005 15:53:12   schand
//Removed unwanted commented
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements


using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.IO;
using System.Configuration;
using System.Runtime.Remoting;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;   

namespace TransportDirect.EnhancedExposedServices 
{
	/// <summary>
	/// Gloabl class for EnhancedExposedServices.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public Global()
		{
			InitializeComponent();
		}	
		
		protected void Application_Start(Object sender, EventArgs e)
		{
			// Do remoting configuration before any initialisation is done
            string applicationPath = AppDomain.CurrentDomain.BaseDirectory;
			string configPath = Path.Combine(applicationPath, "cjp.client.config");			
			if(File.Exists(configPath))
				RemotingConfiguration.Configure(configPath, false);
			


			// Initialise Service Discovery
			TDServiceDiscovery.Init(new EnhancedExposedServicesInitialisation());
		}
 
		protected void Session_Start(Object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_Error(Object sender, EventArgs e)
		{

		}

		protected void Session_End(Object sender, EventArgs e)
		{

		}

		protected void Application_End(Object sender, EventArgs e)
		{

		}
			
		#region Web Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.components = new System.ComponentModel.Container();
		}
		#endregion
	}
}

