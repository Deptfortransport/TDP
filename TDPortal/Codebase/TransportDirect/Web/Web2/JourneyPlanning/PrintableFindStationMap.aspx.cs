// *********************************************** 
// NAME                 : PrintableFindStationMap.aspx.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 01/06/2004 
// DESCRIPTION  : Printable page for FindStationMap page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableFindStationMap.aspx.cs-arc  $ 
//
//   Rev 1.4   Nov 20 2009 09:28:22   apatel
//updated for map enhancement when javascript is disabled
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Dec 17 2008 15:52:04   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:18   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:30:42   mturner
//Initial revision.
//
//   Rev 1.6   Feb 23 2006 18:57:44   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.5   Feb 21 2006 11:41:46   aviitanen
//Merge from stream0009 
//
//   Rev 1.4   Feb 10 2006 15:04:50   build
//Automatically merged from branch for stream3180
//
//   Rev 1.3.1.0   Dec 02 2005 11:28:50   RGriffith
//Changes applied for using new PrintableHeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.3   Nov 18 2005 16:44:02   halkatib
//Change as part of IR3051. Added inheritance reference to the INewWindowPage marker interface. 
//Resolution for 3051: DN077 - Error page behaves unexpectedly
//
//   Rev 1.2   Jun 30 2004 15:43:06   passuied
//Cleaning up
//
//   Rev 1.1   Jun 02 2004 16:40:26   passuied
//working version
//
//   Rev 1.0   Jun 01 2004 13:09:08   passuied
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
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Printable page for FindStationMap page
	/// </summary>
	public partial class PrintableFindStationMap : TDPrintablePage, INewWindowPage
	{
		#region Controls declaration
		protected FindStationResultsLocationControl locationControl;
		protected MapLocationIconsDisplayControl iconsDisplay;
		protected FindStationResultsTable stationResultsTable;
		#endregion

		#region Constants declaration
		private const string RES_SCALE = "FindStation.scale";
		#endregion

		#region Constructor and Page_Load

		/// <summary>
		/// Default Constructor
		/// </summary>
		public PrintableFindStationMap()
		{
			pageId = PageId.PrintableFindStationMap;
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

			InputPageState inputPageState = TDSessionManager.Current.InputPageState;
			imageOverview.ImageUrl = inputPageState.OverviewMapUrlOutward;

            imageOverview.AlternateText = Global.tdResourceManager.GetString(
                       "JourneyMapControl.imageSummaryMap.AlternateText", TDCultureInfo.CurrentUICulture);

            imageMap.AlternateText = Global.tdResourceManager.GetString
                    ("langStrings", "JourneyMapControl.imageMap.AlternateText");

			// Call GetHighResolutionMapImageUrl to set map image url, height and width.
			MapHelper helper = new MapHelper();
			MapImageProperties props = helper.GetHighResolutionMapImageUrl(true);

			//if high-resolution map has failed to generate, standard resolution map is displayed
			if (props.ImageUrl == String.Empty)
			{
				imageMap.ImageUrl = inputPageState.MapUrlOutward;
			}
			else 
			{
				imageMap.ImageUrl = props.ImageUrl;
			}

			labelMapScale.Text = "1:" +inputPageState.MapScaleOutward.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
			labelMapScaleTitle.Text = 
				Global.tdResourceManager.GetString(
					RES_SCALE,
					TDCultureInfo.CurrentUICulture);
			labelMapScaleTitle.Text += ": ";

			labelOverview.Text = 
				Global.tdResourceManager.GetString(
				"PrintableMapControl.labelOverview",
				TDCultureInfo.CurrentUICulture);

			iconsDisplay.Populate(TDSessionManager.Current.InputPageState.IconSelectionOutward);
			panelIcons.Visible = !iconsDisplay.IsEmpty;

			labelDateTime.Text = TDDateTime.Now.ToString("G");

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
            mapTable.Visible = this.IsJavascriptEnabled;
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
