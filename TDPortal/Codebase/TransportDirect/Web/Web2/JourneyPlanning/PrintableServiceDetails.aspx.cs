// *********************************************** 
// NAME				 : PrintableServiceDetails.aspx.cs
// AUTHOR			 : Richard Philpott
// DATE CREATED		 : 2005-07-18
// DESCRIPTION		 : Prinatble Service Details page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableServiceDetails.aspx.cs-arc  $
//
//   Rev 1.4   Mar 21 2013 10:13:36   mmodi
//Updates for journey display notes filtering
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.3   Jan 12 2009 11:13:32   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:34   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:04   mturner
//Initial revision.
//
//   Rev 1.7   Feb 23 2006 18:23:18   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.6   Feb 10 2006 15:08:36   build
//Automatically merged from branch for stream3180
//
//   Rev 1.5.1.0   Dec 02 2005 11:28:50   RGriffith
//Changes applied for using new PrintableHeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.5   Nov 18 2005 16:46:44   halkatib
//Change as part of IR3051. Added inheritance reference to the INewWindowPage marker interface. 
//Resolution for 3051: DN077 - Error page behaves unexpectedly
//
//   Rev 1.4   Aug 23 2005 12:07:22   RPhilpott
//Always get PublicJourneyDetail, not just if not in PostBack, so that page can be switched to CMS edit mode. 
//Resolution for 2677: Del 7.1 - CMS issues with Service Details page.
//
//   Rev 1.3   Aug 16 2005 17:55:44   RPhilpott
//FxCop and code review fixes.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.2   Jul 29 2005 17:16:36   RPhilpott
//Add styles.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.1   Jul 25 2005 18:16:20   RPhilpott
//Updates during unit test.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 22 2005 20:04:32   RPhilpott
//Initial revision.
//
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
	public partial class PrintableServiceDetails : TDPrintablePage, INewWindowPage
	{
		protected ServiceHeaderControl serviceHeaderControl;
		protected CallingPointsControl callingPointsControlBefore;
		protected CallingPointsControl callingPointsControlLeg;
		protected CallingPointsControl callingPointsControlAfter;
		protected ServiceNotesControl  serviceNotesControl;
		protected ServiceOperationControl serviceOperationControl;

		protected Label labelFootnote;


		/// <summary>
		/// Constructor - sets the page id
		/// </summary>
		public PrintableServiceDetails() : base()
		{
			pageId = PageId.PrintableServiceDetails;
		}

		
		protected void Page_Load(object sender, System.EventArgs e)
		{

			PageTitle = GetResource("ServiceDetails.PageTitle");
			labelServiceDetailsTitle.Text = PageTitle;

			ServiceDetailsJourneyDetailHelper helper = new ServiceDetailsJourneyDetailHelper();

            PublicJourney journey = helper.GetJourney();
            PublicJourneyDetail journeyDetail = helper.GetJourneyDetail();

			if	(journey != null && journeyDetail != null) 
			{
				InitialiseControls(journey, journeyDetail);
			}
	
		}

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
			this.ID = "PrintableServiceDetails";

		}
		#endregion


		private void InitialiseControls(PublicJourney journey, PublicJourneyDetail journeyDetail)
		{

			labelPrinterFriendly.Text= Global.tdResourceManager.GetString(
				"StaticPrinterFriendly.labelPrinterFriendly", TDCultureInfo.CurrentUICulture);

			labelInstructions.Text = Global.tdResourceManager.GetString(
				"StaticPrinterFriendly.labelInstructions", TDCultureInfo.CurrentUICulture);

			labelDate.Text = TDDateTime.Now.ToString("G");

			labelUsername.Visible = TDSessionManager.Current.Authenticated;
			labelUsernameTitle.Visible = TDSessionManager.Current.Authenticated;
			
			if  (TDSessionManager.Current.Authenticated)
			{
				labelUsername.Text = TDSessionManager.Current.CurrentUser.Username;
			}

			TDItineraryManager itineraryManager = TDItineraryManager.Current;
			bool itineraryExists = (itineraryManager.Length > 0);
			bool extendInProgress = itineraryManager.ExtendInProgress;
			bool showItinerary = (itineraryExists && !extendInProgress);

			labelReferenceNumberTitle.Visible = false;

			if (!showItinerary)
			{
				int refNo = TDSessionManager.Current.JourneyResult.JourneyReferenceNumber;

				if	(refNo > 0)
				{
					labelReferenceNumberTitle.Visible = true;
					labelJourneyReferenceNumber.Text = refNo.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
				}
			}
			
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

	}
}
