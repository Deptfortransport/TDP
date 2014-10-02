//******************************************************************************
//NAME			: Global.asax.cs
//AUTHOR		: Gary Eaton
//DATE CREATED	: 04/10/2003
//DESCRIPTION	: Also known as the ASP.NET application file, this is a file that 
//contains code for responding to application-level events raised by ASP.NET.
//******************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/TransactionWebServices/Global.asax.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 13:55:24   mturner
//Initial revision.
//
//   Rev 1.3   Nov 05 2003 09:56:50   geaton
//Moved initialisation of services to initialisation class.

using System;

using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.ReportDataProvider.TransactionWebService
{
	public class Global : System.Web.HttpApplication
	{
		public Global()
		{
			InitializeComponent();
		}	
		
		/// <summary>
		/// Called once when the TD Transaction Web Service first starts up.
		/// </summary>
		protected void Application_Start(Object sender, EventArgs e)
		{
			// Initialise TD Services required by TD Transaction Web Service.
			TDServiceDiscovery.Init(new TransactionWebServiceInitialisation());
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
		}
		#endregion
	}
}

