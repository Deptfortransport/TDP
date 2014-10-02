// *********************************************** 
// NAME				 : ServiceDetails.aspx.cs
// AUTHOR			 : Richard Philpott
// DATE CREATED		 : 2005-07-18
// DESCRIPTION		 : Service Details page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/ServiceDetails.aspx.cs-arc  $
//
//   Rev 1.4   Mar 21 2013 10:13:42   mmodi
//Updates for journey display notes filtering
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.3   Jan 12 2009 11:13:32   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:46   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:22   mturner
//Initial revision.
//
//Rev DevFatory Feb 8th 16:28:00 dgath
//Line added to Page_Load event for White Label left menu configuration
//
//   Rev 1.12   Mar 31 2006 10:34:02   NMoorhouse
//Set of page title
//Resolution for 3755: Del 8.1 - Train Service Details page has title of "Transport Direct"
//
//   Rev 1.11   Feb 23 2006 19:05:50   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.10   Feb 10 2006 15:09:24   build
//Automatically merged from branch for stream3180
//
//   Rev 1.9.1.0   Dec 07 2005 12:16:08   AViitanen
//Changed to use the new HeaderControl and HeadElementControl. Changed the name of the Back button.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.9   Nov 08 2005 17:01:30   AViitanen
//HTML button changes
//
//   Rev 1.7   Nov 03 2005 16:00:44   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.6.1.0   Oct 12 2005 11:39:02   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.7   Oct 11 2005 17:45:00   RGriffith
//Replacing the image button with HTML button
//
//   Rev 1.6   Aug 23 2005 12:07:22   RPhilpott
//Always get PublicJourneyDetail, not just if not in PostBack, so that page can be switched to CMS edit mode. 
//Resolution for 2677: Del 7.1 - CMS issues with Service Details page.
//
//   Rev 1.5   Aug 16 2005 17:55:46   RPhilpott
//FxCop and code review fixes.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.4   Jul 28 2005 16:06:28   RPhilpott
//Unit test updates.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.3   Jul 25 2005 18:16:52   RPhilpott
//Updates during unit test.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.2   Jul 22 2005 20:01:58   RPhilpott
//Work in progress.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.1   Jul 19 2005 14:00:02   RPhilpott
//In progress
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 19 2005 10:10:38   RPhilpott
//Initial revision.
//

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;				
using TransportDirect.Common.ServiceDiscovery;

using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;

using TransportDirect.Web.Support;


namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// ServiceDetails page.
	/// </summary>
	public partial class ServiceDetails : TDPage
	{
	
		protected ServiceHeaderControl serviceHeaderControl;
		protected CallingPointsControl callingPointsControlBefore;
		protected CallingPointsControl callingPointsControlLeg;
		protected CallingPointsControl callingPointsControlAfter;
		protected ServiceNotesControl serviceNotesControl;
		protected ServiceOperationControl serviceOperationControl;

		/// <summary>
		/// Constructor - sets the page id
		/// </summary>
		public ServiceDetails() : base()
		{
			pageId = PageId.ServiceDetails;
		}

		/// <summary>
		/// Obtains the PublicJourneyDetail for the current leg of the journey 
		/// and uses it to initialise the constituent controls.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.PageTitle = GetResource("ServiceDetails.PageTitle");

			labelServiceDetailsTitle.Text = PageTitle;

			buttonBack.Text = GetResource("JourneyPlannerLocationMap.buttonBack.Text");

			ServiceDetailsJourneyDetailHelper helper = new ServiceDetailsJourneyDetailHelper();

            PublicJourney journey = helper.GetJourney();
			PublicJourneyDetail journeyDetail = helper.GetJourneyDetail();

			if	(journey != null && journeyDetail != null) 
			{
				InitialiseControls(journey, journeyDetail);
			}

            //Added for white labelling:
            ConfigureLeftMenu("FindCoachInput.clientLink.BookmarkTitle", "FindCoachInput.clientLink.LinkText", clientLink, expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextServiceDetails);
            expandableMenuControl.AddExpandedCategory("Related links");

		}


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
		/// Extra event subscription
		/// </summary>
		private void ExtraEventWireUp()
		{
			buttonBack.Click += new EventHandler(this.buttonBackClick);	
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.ID = "ServiceDetails";

		}
		#endregion

		private void InitialiseControls(PublicJourney journey, PublicJourneyDetail journeyDetail)
		{
			serviceHeaderControl.JourneyDetail = journeyDetail;
			serviceNotesControl.Features = journeyDetail.GetVehicleFeatures();
            serviceNotesControl.Journey = journey;
            serviceNotesControl.JourneyDetail = journeyDetail;
			serviceOperationControl.JourneyDetail = journeyDetail;	

			CallingPointsFormatter callingPoints = new CallingPointsFormatter(journeyDetail);	

			callingPointsControlBefore.Mode = CallingPointControlType.Before;
			callingPointsControlBefore.CallingPoints = callingPoints.GetCallingPointLines(CallingPointControlType.Before);

			callingPointsControlLeg.Mode = CallingPointControlType.Leg; 
			callingPointsControlLeg.CallingPoints = callingPoints.GetCallingPointLines(CallingPointControlType.Leg);

			callingPointsControlAfter.Mode = CallingPointControlType.After; 
			callingPointsControlAfter.CallingPoints = callingPoints.GetCallingPointLines(CallingPointControlType.After);
		}

		private void buttonBackClick(object sender, EventArgs e)
		{
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.ServiceDetailsBack;
		}
	}
}
