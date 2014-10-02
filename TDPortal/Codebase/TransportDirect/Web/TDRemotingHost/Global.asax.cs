using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Runtime.Remoting;
using System.IO;
using System.Diagnostics;

using TransportDirect.Common; 
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.TDRemotingHost 
{
	/// <summary>
	/// Summary description for Global.
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
			TDServiceDiscovery.Init( new TDRemotingHostInitialisation() );

			//Configure the hosted remoting objects to be a remote object
            string configPath = AppDomain.CurrentDomain.BaseDirectory + "remoting.config";
			if  (File.Exists(configPath))
			{
				RemotingConfiguration.Configure(configPath, false);
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Loaded remoting configuration from " + configPath));				
			}
			else
                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Could not find remoting configuration file: " + configPath));				


			Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Application Start Complete"));				

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
			Exception ex = Server.GetLastError().InnerException;
			
			if  (ex == null)
			{
				ex = Server.GetLastError();
			}

			string message = "Unhandled Exception: " 
				+ Environment.NewLine + Environment.NewLine 
				+ "Message: " + Environment.NewLine + ex.Message
				+ Environment.NewLine + Environment.NewLine 
				+ "Stack trace:" + Environment.NewLine + ex.StackTrace;

			Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, message));
		}

		protected void Session_End(Object sender, EventArgs e)
		{

		}

		protected void Application_End(Object sender, EventArgs e)
		{
			Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Application End"));				
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

