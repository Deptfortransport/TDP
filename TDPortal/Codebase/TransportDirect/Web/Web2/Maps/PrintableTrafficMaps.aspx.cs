// *********************************************** 
// NAME                 : PrintableTrafficMap.aspx
// AUTHOR               : Andy Lole
// DATE CREATED         : 17/10/2003 
// DESCRIPTION			: Printable map for the TrafficMap page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Maps/PrintableTrafficMaps.aspx.cs-arc  $
//
//   Rev 1.3   Jan 05 2009 14:22:06   devfactory
//No change.
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:26:04   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:08   mturner
//Initial revision.
//
//   Rev 1.9   Jun 08 2006 10:05:12   mmodi
//IR4114: Wrap the location name to be displayed on printable page
//Resolution for 4114: Map Locations with long names are cut when Printer Friendly is printed
//
//   Rev 1.8   Feb 23 2006 18:26:10   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.7   Feb 10 2006 15:09:20   build
//Automatically merged from branch for stream3180
//
//   Rev 1.6.1.0   Dec 01 2005 12:03:08   RGriffith
//Changes applied for using new PrintableHeaderControl and HeadElementControl for Homepage Phase2
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.6   Nov 18 2005 16:47:44   halkatib
//Change as part of IR3051. Added inheritance reference to the INewWindowPage marker interface. 
//Resolution for 3051: DN077 - Error page behaves unexpectedly
//
//   Rev 1.5   Apr 28 2004 16:20:26   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.4   Mar 25 2004 14:40:02   CHosegood
//DEL 5.2 Map QA changes
//Resolution for 674: DEL 5.2 Map QA changes
//
//   Rev 1.3   Mar 01 2004 17:20:42   asinclair
//Added print instructions for Del 5.2
//
//   Rev 1.2   Nov 18 2003 16:15:16   alole
//Updated to fir the Printer Friendly view size
//
//   Rev 1.1   Oct 31 2003 09:31:46   passuied
//Get username from commerceserver for printable pages
//
//   Rev 1.0   Oct 21 2003 09:21:00   ALole
//Initial Revision


using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Printable map for the input side or when accessed through the Maps link
	/// </summary>
	
	public partial class PrintableTrafficMap : TDPrintablePage, INewWindowPage
	{

		protected PrintableSimpleMapControl PrintableSimpleMapControl;

        /// <summary>
        /// Constructor
        /// </summary>
		public PrintableTrafficMap()
		{
			pageId = PageId.PrintableTrafficMap;
		}


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			InputPageState inputPageState = TDSessionManager.Current.InputPageState;

			// Add a line break in location name to prevent it from being cut off 
			// on right hand side when printed
			string locationName = inputPageState.MapLocation.Description;
			locationName = this.AddSpacesToText(locationName);
			locationName = this.WrapText(locationName, 80);
			
			PrintableSimpleMapControl.Populate(true, true, locationName);
			labelDateTimeTitle.Text = Global.tdResourceManager.GetString( "PrintableJourneyMapInput.labelDateTimeTitle" );
			labelDateTime.Text = TDDateTime.Now.ToString("F");

			labelUsername.Visible = TDSessionManager.Current.Authenticated;
			labelUsernameTitle.Visible = TDSessionManager.Current.Authenticated;
			if( TDSessionManager.Current.Authenticated )
			{
				labelUsername.Text = TDSessionManager.Current.CurrentUser.Username;
			}

			labelPrinterFriendly.Text= Global.tdResourceManager.GetString(
				"StaticPrinterFriendly.labelPrinterFriendly", TDCultureInfo.CurrentUICulture);

			labelInstructions.Text = Global.tdResourceManager.GetString(
				"StaticPrinterFriendly.labelInstructions", TDCultureInfo.CurrentUICulture);

		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			TDSessionManager.Current.FormShift[ SessionKey.SkipScreenFlow ] = true;
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.ID = "PrintableTrafficMap";

		}
		#endregion
	}
}
