// *********************************************** 
// NAME                 : PrintableRefineMap.ascx.cs 
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 02/01/2006
// DESCRIPTION			: A new page as part of the new Extension, Replan and Adjust pages.  
//						Printable version displaying maps of the planned journey.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableRefineMap.aspx.cs-arc  $
//
//   Rev 1.3   Jan 09 2009 13:36:28   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:30   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:30:58   mturner
//Initial revision.
//
//   Rev 1.5   Mar 17 2006 15:32:38   RGriffith
//Extend, adjust, and replan code review suggestions
//
//   Rev 1.4   Mar 14 2006 13:18:42   pcross
//Added header, headelementcontrol, resource manager reference
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Mar 10 2006 09:51:26   NMoorhouse
//Fix a problem display maps of adjusted journeys
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Mar 06 2006 18:17:34   NMoorhouse
//Updated following FxCop
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Feb 09 2006 19:00:38   NMoorhouse
//Updates to make RefineMap screen consistent
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Feb 09 2006 16:21:54   NMoorhouse
//Initial revision.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Feb 07 2006 18:47:24   NMoorhouse
//Initial revision.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
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
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for PrintableRefineMap.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class PrintableRefineMap : TDPrintablePage, INewWindowPage
	{
		// HTML Controls
	
		// TD Web Controls
		protected TransportDirect.UserPortal.Web.Controls.PrintableMapControl journeyMapControlOutward;
		protected TransportDirect.UserPortal.Web.Controls.PrintableMapControl journeyMapControlReturn;

		private ITDSessionManager tdSessionManager;
		private TDItineraryManager itineraryManager;

		/// <summary>
		/// True if the Itinerary exists, containing the Initial journey and zero or more extensions
		/// </summary>
		private bool itineraryExists;

		/// <summary>
		/// True if an extension to an Itinerary is in the process of being planned and has not yet been added to the Itinerary
		/// </summary>
		private bool extendInProgress;

		// State of results
		/// <summary>
		///  True if there is an outward trip for the current selection (Journey, Itinerary or Extension)
		/// </summary>
		private bool outwardExists;

		/// <summary>
		///  True if there is a return trip for the current selection (Journey, Itinerary or Extension)
		/// </summary>
		private bool returnExists;

		/// <summary>
		/// Constructor for the Page
		/// </summary>
		public PrintableRefineMap() : base()
		{
			// Set page Id
			pageId = PageId.PrintableRefineMap;

			// Initialise Resource Manager
			this.LocalResourceManager = TDResourceManager.REFINE_JOURNEY_RM;
		}

		#region Event Handlers
		/// <summary>
		/// Handles page load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Initialise text and button properties
			PopulateControls();
		}

		/// <summary>
		/// Handles the page prerender event. This performs any last-minute updates to the controls
		/// that are displayed to the user
		/// </summary>
		protected override void OnPreRender(EventArgs e)
		{
			SetControlVisibility();

			base.OnPreRender(e);
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Method to initialise text and button properties
		/// </summary>
		private void PopulateControls()
		{
			// Get correct resource strings for labels
			labelTitle.Text = GetResource("RefineMap.labelTitle.Text");
			labelIntroductoryText.Text = GetResource("RefineMap.labelIntroductoryText.Text");
			labelPrinterFriendly.Text = GetResource("StaticPrinterFriendly.labelPrinterFriendly");
			labelInstructions.Text = GetResource("StaticPrinterFriendly.labelInstructions");
			labelDateTitle.Text = GetResource("StaticPrinterFriendly.labelDateTitle");
			labelDate.Text = TDDateTime.Now.ToString("G");
			labelUsername.Visible = TDSessionManager.Current.Authenticated;
			labelUsernameTitle.Visible = TDSessionManager.Current.Authenticated;
			if( TDSessionManager.Current.Authenticated )
			{
				labelUsernameTitle.Text = GetResource("StaticPrinterFriendly.labelUsernameTitle");
				labelUsername.Text = TDSessionManager.Current.CurrentUser.Username;
			}
		}

		/// <summary>
		/// Establish what mode the Itinerary Manager is in and whether we have any Return results
		/// </summary>
		private void DetermineStateOfResults()
		{
			tdSessionManager = TDSessionManager.Current;
			itineraryManager = TDItineraryManager.Current;

			itineraryExists = (itineraryManager.Length > 0);
			extendInProgress = itineraryManager.ExtendInProgress;
			bool showItinerary = (itineraryExists && !extendInProgress);

			if ( showItinerary )
			{
				outwardExists = (itineraryManager.OutwardLength > 0);
				returnExists = (itineraryManager.ReturnLength > 0);
			}
			else
			{
				//check for normal result
				ITDJourneyResult result = tdSessionManager.JourneyResult;
				if(result != null) 
				{
					outwardExists = ((result.OutwardPublicJourneyCount + result.OutwardRoadJourneyCount) > 0) && result.IsValid;
					returnExists = ((result.ReturnPublicJourneyCount + result.ReturnRoadJourneyCount) > 0) && result.IsValid;
				}
			}
		}

		/// <summary>
		/// Determines which controls should be visible
		/// </summary>
		private void SetControlVisibility()
		{
			DetermineStateOfResults();

			if (outwardExists)
			{
				outwardPanel.Visible = true;
				ShowMapControl(journeyMapControlOutward, true);
			}
			else
			{
				outwardPanel.Visible = false;
			}

			if (returnExists)
			{
				returnPanel.Visible = true;
				ShowMapControl(journeyMapControlReturn, false);
			}
			else
			{
				returnPanel.Visible = false;
			}
		}

		/// <summary>
		/// Handles the visual bits associated with showing the map control - ie makes it visible and updates the buttons
		/// </summary>
		/// <param name="?"></param>
		private void ShowMapControl(PrintableMapControl journeyMapControl, bool outward)
		{
			journeyMapControl.Visible = true;
			journeyMapControl.Populate(outward, false, TDSessionManager.Current.IsFindAMode);

			TDJourneyViewState viewState = itineraryManager.JourneyViewState;

			if(outward)
			{
				viewState.OutwardShowMap = true;
			}
			else
			{
				viewState.ReturnShowMap = true;
			}
				
		}
		#endregion

		#region Web Form Designer generated code
		/// <summary>
		/// Web Form Designer generated code
		/// </summary>
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