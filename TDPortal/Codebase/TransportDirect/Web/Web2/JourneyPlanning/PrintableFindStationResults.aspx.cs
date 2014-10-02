// *********************************************** 
// NAME                 : PrintableFindStationResults.aspx.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 01/06/2004 
// DESCRIPTION  : Printable page for FindStationResults page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableFindStationResults.aspx.cs-arc  $ 
//
//   Rev 1.3   Dec 17 2008 15:52:06   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:20   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:30:44   mturner
//Initial revision.
//
//   Rev 1.5   Feb 23 2006 18:15:14   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.4   Feb 10 2006 15:04:50   build
//Automatically merged from branch for stream3180
//
//   Rev 1.3.1.0   Dec 02 2005 11:28:50   RGriffith
//Changes applied for using new PrintableHeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.3   Nov 18 2005 16:44:22   halkatib
//Change as part of IR3051. Added inheritance reference to the INewWindowPage marker interface. 
//Resolution for 3051: DN077 - Error page behaves unexpectedly
//
//   Rev 1.2   Jun 30 2004 15:43:06   passuied
//Cleaning up
//
//   Rev 1.1   Jun 02 2004 16:40:28   passuied
//working version
//
//   Rev 1.0   Jun 01 2004 13:09:18   passuied
//Initial Revision


using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.SessionManager;

namespace TransportDirect.UserPortal.Web.Templates
{

	/// <summary>
	/// Printable page for FindStationResults 
	/// </summary>
	public partial class PrintableFindStationResults : TDPrintablePage, INewWindowPage
	{
	
		#region Controls declaration
		protected FindStationResultsLocationControl locationControl;
		protected FindStationResultsTable stationResultsTable;
		#endregion

		#region Constructor and Page_Load

		/// <summary>
		/// Default constructor
		/// </summary>
		public PrintableFindStationResults()
		{
			pageId = PageId.FindStationResults;
		}

		/// <summary>
		/// Page Load
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			locationControl.Printable = true;
			stationResultsTable.Printable = true;

			labelDateTime.Text = TDDateTime.Now.ToString("G");

			labelUsernameTitle.Visible = TDSessionManager.Current.Authenticated;
			labelUsername.Visible = TDSessionManager.Current.Authenticated;
			if( TDSessionManager.Current.Authenticated )
			{
				labelUsername.Text = TDSessionManager.Current.CurrentUser.Username;
			}
		}
		#endregion


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
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
