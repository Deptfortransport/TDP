// *********************************************** 
// NAME                 : PrintableFindCarParkMap.aspx.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/08/2006
// DESCRIPTION			: Printable page for FindCarParkMap page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableFindCarParkMap.aspx.cs-arc  $ 
//
//   Rev 1.5   Nov 20 2009 09:28:18   apatel
//updated for map enhancement when javascript is disabled
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.4   Nov 12 2009 13:41:56   mmodi
//Updated to use printableMapControl
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Jan 15 2009 09:59:24   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:12   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Mar 25 2008 16:29:00 apatel
//  removed map image height and width set by code
//
//   Rev 1.0   Nov 08 2007 13:30:34   mturner
//Initial revision.
//
//   Rev 1.2   Nov 22 2006 13:17:04   PScott
//SCR 4271
//Ensure printable version of page entry event is called
//
//   Rev 1.1   Aug 16 2006 15:53:40   mmodi
//Added code to set label text
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.0   Aug 16 2006 15:20:00   mmodi
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
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Printable page for FindCarParkMap page
	/// </summary>
	public partial class PrintableFindCarParkMap : TDPrintablePage, INewWindowPage
	{
		#region Controls declaration

		protected FindCarParkResultsLocationControl carParkResultsLocationControl;
		protected FindCarParksResultsTableControl carParkResultsTableControl;

		#endregion

		#region Constructor and Page_Load

		/// <summary>
		/// Default Constructor
		/// </summary>
		public PrintableFindCarParkMap()
		{
			pageId = PageId.PrintableFindCarParkMap;
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

            // Populate the map control
            mapOutward.Populate(true, true, false);
                        
            // Populate footer labels
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


        /// <summary>
        /// Page Pre Render
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            mapOutward.Visible = this.IsJavascriptEnabled;
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
