// *********************************************** 
// NAME                 : TicketUpgrade.aspx.cs 
// AUTHOR               : Rachel Geraghty
// DATE CREATED         : 11/03/2005 
// DESCRIPTION			: Page displays ticket upgrade information 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/TicketUpgrade.aspx.cs-arc  $
//
//   Rev 1.4   Jan 13 2009 15:28:58   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Jan 13 2009 14:44:02   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:50   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:50   mturner
//Initial revision.
//
//   Rev 1.5   Mar 14 2006 10:28:12   RGriffith
//Manual merge for Stream3353
//
//   Rev 1.2.3.0   Mar 09 2006 11:52:34   RGriffith
//Changes to ensure back button can go back to the RefineTickets page
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Feb 24 2006 10:17:32   rgreenwood
//stream3129: Manual merge changes
//
//   Rev 1.3   Feb 10 2006 15:09:32   build
//Automatically merged from branch for stream3180
//
//   Rev 1.2.1.0   Dec 06 2005 09:46:50   AViitanen
//Changed to use headerControl and headElementControl as part of Homepage Phase 2
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.2   Nov 08 2005 19:02:08   kjosling
//Automatically merged for stream 2816
//
//   Rev 1.1.1.0   Oct 14 2005 14:54:04   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.1   Apr 05 2005 14:03:36   rgeraghty
//Changes made post code review
//Resolution for 1948: DEV CODE REVIEW: FR Retailer Information
//
//   Rev 1.0   Mar 14 2005 14:32:04   rgeraghty
//Initial revision.


using System;
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
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Displays Ticket Upgrade information.
	/// </summary>
	public partial class TicketUpgrade : TDPage
	{
		#region control declarations

		// names of the header controls matter since the name is used to automatically populate
		// resources in langstrings	
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;
	
		
		#endregion

		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public TicketUpgrade() : base()
		{
			//set the page id
			pageId = PageId.TicketUpgrade;

			//use the fares and tickets resource manager for this control
			this.LocalResourceManager = TDResourceManager.FARES_AND_TICKETS_RM;
		}

		#endregion Constructor


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			ExtraEventWireUp();
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

		/// <summary>
		/// Extra event subscription
		/// </summary>
		private void ExtraEventWireUp()
		{
			imageButtonBack.Click += new EventHandler(this.imageButtonBack_Click);
		}

		#endregion

		#region private methods

		/// <summary>
		/// Set the page's static controls
		/// </summary>
		private void InititialiseStaticControls()
		{
			// Get Langstrings resource file
			TDResourceManager langstrings = TDResourceManager.GetResourceManagerFromCache(TDResourceManager.LANG_STRINGS);
			imageButtonBack.Text = langstrings.GetString("TicketUpgrade.imageButtonBack.Text");

			headerLabel.Text = GetResource("TicketUpgrade.HeaderText");			
			descriptionLabel.Text = GetResource("TicketUpgrade.DescriptionText");			
		}


		#endregion

		#region event handlers

		/// <summary>
		/// Used to redirect the page to the Journey Fares page
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void imageButtonBack_Click(object sender, EventArgs e)
		{
			ItineraryManagerMode itineraryManagerMode = TDSessionManager.Current.ItineraryMode;

			// Detect Itinerary Manger mode to determine if coming from RefineTickets Page
			if ((itineraryManagerMode == ItineraryManagerMode.Replan)
				|| (itineraryManagerMode == ItineraryManagerMode.ExtendJourney)
				|| (TDSessionManager.Current.JourneyResult.AmendedOutwardPublicJourney != null)
				|| (TDSessionManager.Current.JourneyResult.AmendedReturnPublicJourney != null))
			{
				// redirect to RefineTickets page
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.RefineTicketsView;
			} 
			else
			{
				//redirect page to Journey Fares if not coming from RefineTicketsPage
				PricingRetailOptionsState options = TDItineraryManager.Current.PricingRetailOptions;
				options.LeaveTicketDisplay = true; //this flag is used by the JourneyFares page for re-displaying the tickets selected
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoJourneyFares;
			}
		}
			
		/// <summary>
		/// Page Load event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			InititialiseStaticControls();
            //Added for white labelling:
            ConfigureLeftMenu("TicketUpgrade.clientLink.BookmarkTitle", "TicketUpgrade.clientLink.LinkText", null, expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextTicketUpgrade);
            expandableMenuControl.AddExpandedCategory("Related links");
		}

		#endregion
	}
}
