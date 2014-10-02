// *********************************************** 
// NAME                 : PrintableFindCarParkResults.aspx.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/08/2006 
// DESCRIPTION			: Printable page for FindCarParkResults page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableFindCarParkResults.aspx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:25:14   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:30:34   mturner
//Initial revision.
//
//   Rev 1.2   Nov 22 2006 13:17:04   PScott
//SCR 4271
//Ensure printable version of page entry event is called
//
//   Rev 1.1   Aug 16 2006 15:55:32   mmodi
//Added code to set label text
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.0   Aug 16 2006 14:25:26   mmodi
//Initial revision.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//

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
	/// Printable page for FindCarParkResults 
	/// </summary>
	public partial class PrintableFindCarParkResults : TDPrintablePage, INewWindowPage
	{
		#region Controls declaration

	
		protected FindCarParkResultsLocationControl carParkResultsLocationControl;
		protected FindCarParksResultsTableControl carParkResultsTableControl;

		#endregion

		#region Constructor and Page_Load

		/// <summary>
		/// Default constructor
		/// </summary>
		public PrintableFindCarParkResults()
		{
			pageId = PageId.PrintableFindCarParkResults;
		}

		/// <summary>
		/// Page Load
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			carParkResultsLocationControl.Printable = true;
			carParkResultsTableControl.Printable = true;

			labelDateTime.Text = TDDateTime.Now.ToString("G");
			labelDateTimeTitle.Text = GetResource("PrintableJourneyMap.labelDateTimeTitle");
			labelUsernameTitle.Text = GetResource("PrintableJourneyMap.labelUsernameTitle");

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
