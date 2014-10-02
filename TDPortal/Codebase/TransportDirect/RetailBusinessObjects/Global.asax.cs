//******************************************************************************
//NAME			: Global.asax.cs
//AUTHOR		: Gary Eaton
//DATE CREATED	: 14/10/2003
//DESCRIPTION	: Contains code for responding to application-level events 
//******************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/Global.asax.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:06   mturner
//Initial revision.
//
//   Rev 1.4   May 13 2005 14:57:50   RPhilpott
//Add extra logging and defensive coding for Application initialisation problem.
//Resolution for 2511: PT costing - RBO initialisation failure
//
//   Rev 1.3   Oct 22 2003 15:36:30   CHosegood
//Uncommented TDServiceDiscovery
//
//   Rev 1.2   Oct 16 2003 20:55:22   CHosegood
//Performing remote configuration at applicationStart
//
//   Rev 1.1   Oct 15 2003 14:42:14   geaton
//Added initialisation.

using System;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Diagnostics;
using System.Collections;
using System.ComponentModel;
using System.Runtime.Remoting;

using TransportDirect.Common; 
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : System.Web.HttpApplication
	{
		public Global()
		{
			InitializeComponent();
		}	
		
		protected void Application_Start(Object sender, EventArgs e)
		{
			// TODO: test following initialisation. During development
			// initialisation was performed within NUnit tests.
			TDServiceDiscovery.Init( new RBOServiceInitialisation() );

            //configure the RetailBusinessObjectFacade to be a remote object
            string configPath = Path.Combine(Context.Server.MapPath( Context.Request.ApplicationPath ), "web.config");
			if  (File.Exists(configPath))
			{
				RemotingConfiguration.Configure(configPath);
			}

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
			// Server.GetLastError() normally returns an HttpUnhandledException 
			//  object that wraps the exception we actually want to log ...
				
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
		}
		#endregion
	}
}

