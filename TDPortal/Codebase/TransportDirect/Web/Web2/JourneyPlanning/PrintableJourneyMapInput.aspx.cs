// *********************************************** 
// NAME                 : PrintableJourneyMapInput.aspx
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 02/09/2003 
// DESCRIPTION  : Printable map for the input side or when accessed through the Maps link
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableJourneyMapInput.aspx.cs-arc  $ 
//
//   Rev 1.3   Jan 14 2009 11:52:38   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:26   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:30:54   mturner
//Initial revision.
//
//   Rev 1.15   Jun 08 2006 09:50:42   mmodi
//IR4114: Wrap the location name to be displayed on printable page
//Resolution for 4114: Map Locations with long names are cut when Printer Friendly is printed
//
//   Rev 1.14   Feb 23 2006 18:20:10   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.13   Feb 10 2006 15:08:34   build
//Automatically merged from branch for stream3180
//
//   Rev 1.12.1.0   Dec 01 2005 11:48:30   RGriffith
//Changes applied for using new PrintableHeaderControl and HeadElementControl for Homepage Phase2
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.12   Nov 18 2005 16:45:44   halkatib
//Change as part of IR3051. Added inheritance reference to the INewWindowPage marker interface. 
//Resolution for 3051: DN077 - Error page behaves unexpectedly
//
//   Rev 1.11   Jun 09 2004 15:41:36   RPhilpott
//Add explicit support for FindA options
//
//   Rev 1.10   Apr 28 2004 16:20:22   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.9   Mar 01 2004 12:11:12   asinclair
//Added print instructions for Del 5.2
//
//   Rev 1.8   Nov 17 2003 17:10:14   hahad
//changed the title Tag so it it reads 'Transport Direct' from Langstrings
//
//   Rev 1.7   Oct 31 2003 09:31:42   passuied
//Get username from commerceserver for printable pages
//
//   Rev 1.6   Oct 22 2003 12:55:00   passuied
//hidden username if not authenticated
//
//   Rev 1.5   Oct 20 2003 10:53:04   passuied
//Changes after fxcop
//
//   Rev 1.4   Oct 13 2003 13:55:36   passuied
//hidden map title if description not filled yet
//
//   Rev 1.3   Oct 06 2003 13:12:00   PNorell
//Fixed reference bug I introduced in previous checkin.
//
//   Rev 1.2   Oct 06 2003 11:44:28   PNorell
//Updated to conform to new wireframes.
//Updated to exclude them from the ScreenFlow.
//
//   Rev 1.1   Oct 02 2003 18:11:24   passuied
//added strings for printablemapinput
//
//   Rev 1.0   Oct 02 2003 16:55:06   passuied
//Initial revision.


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
using TransportDirect.UserPortal.Web;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Printable map for the input side or when accessed through the Maps link
	/// </summary>
	
	public partial class PrintableJourneyMapInput : TDPrintablePage, INewWindowPage
	{

		

		protected PrintableMapControl map;
	
		public PrintableJourneyMapInput()
		{
			pageId = PageId.PrintableJourneyMapInput;
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{
			InputPageState inputPageState = TDSessionManager.Current.InputPageState;
			
			map.Populate(true, true, false);
			labelDateTime.Text = TDDateTime.Now.ToString("G");
			
			if (inputPageState.MapLocation.Description.Length != 0 && inputPageState.MapLocation.Description != null)
			{
				// Add a line break in location name to prevent it from being cut off 
				// on right hand side when printed
				string locationName = inputPageState.MapLocation.Description;
				locationName = this.AddSpacesToText(locationName);
				locationName = this.WrapText(locationName, 60);

                labelLocationDescription.Text = locationName;
			}
			else
			{
				labelLocationDescription.Visible = false;
				labelMapTitle.Visible = false;
			}
			
			labelUsernameTitle.Visible = TDSessionManager.Current.Authenticated;
			labelUsername.Visible = TDSessionManager.Current.Authenticated;
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

		}
		#endregion
	}
}
