// *********************************************** 
// NAME                 : PrintableCarDetails.ascx.cs 
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 23/02/2006
// DESCRIPTION			: A new page as part of the new Extension, Replan and Adjust pages. 
//                      Printable version displaying the details of selected car journey.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableCarDetails.aspx.cs-arc  $
//
//   Rev 1.4   Sep 01 2011 10:44:50   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.3   Jan 02 2009 15:21:56   apatel
//XHTML Compliance changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:08   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:30:28   mturner
//Initial revision.
//
//   Rev 1.6   Jun 28 2007 16:27:58   mmodi
//Added code to populate the Car details control with journey parameters
//Resolution for 4458: DEL 9.7 - Car journey details
//
//   Rev 1.5   Mar 22 2006 20:27:36   rhopkins
//Minor FxCop fixes.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Mar 17 2006 15:32:36   RGriffith
//Extend, adjust, and replan code review suggestions
//
//   Rev 1.3   Mar 14 2006 13:19:06   pcross
//Added header, headelementcontrol, resource manager reference
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Mar 10 2006 11:47:20   NMoorhouse
//Ensure correct journey is selected from front end selected segment
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Mar 06 2006 18:17:32   NMoorhouse
//Updated following FxCop
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Feb 24 2006 14:39:30   NMoorhouse
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
	/// Summary description for PrintableCarDetails.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class PrintableCarDetails : TDPrintablePage, INewWindowPage
	{
		protected TransportDirect.UserPortal.Web.Controls.TDButton backButton;
		protected TransportDirect.UserPortal.Web.Controls.CarAllDetailsControl carDetailsControl;

		private bool outward = true;

		/// <summary>
		/// Constructor for the Page
		/// </summary>
		public PrintableCarDetails() : base()
		{
			// Set page Id
			pageId = PageId.PrintableCarDetails;

			// Initialise Resource Manager
			this.LocalResourceManager = TDResourceManager.REFINE_JOURNEY_RM;
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
			TDItineraryManager itineraryManager = TDItineraryManager.Current;
			TDJourneyParametersMulti journeyParams = TDItineraryManager.Current.JourneyParameters as TDJourneyParametersMulti;

			RoadJourney roadJourney = GetJourneyDetail();

			// Because there is only the ability to store one set of JourneyParameters in the session, 
			// we cannot allow the carAllDetailsControl to display the CarJourneyType/Options controls 
			// because the user may have two car segments with different parameters.
			if((itineraryManager.ItineraryMode == ItineraryManagerMode.ExtendJourney) 
				&&
				(!itineraryManager.ExtendInProgress) )
			{
				carDetailsControl.Initialise(roadJourney, viewState, outward,!IsPostBack);
			}
			else
			{
				carDetailsControl.Initialise(roadJourney, viewState, outward, journeyParams,!IsPostBack);
			}
			
			labelTitle.Text = GetResource("CarDetails.labelTitle.Text");
			labelIntroductoryText.Text = GetResource("CarDetails.labelIntroductoryText.Text");
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
		/// Gets the RoadJourney that was selected from the session data.  
		/// </summary>
		/// <returns>RoadJourney</returns>
		private RoadJourney GetJourneyDetail()
		{
			Journey journey;

			TDItineraryManager itineraryManager = TDItineraryManager.Current;
			TDJourneyViewState viewState = itineraryManager.JourneyViewState;

			bool outward = TDSessionManager.Current.Session[SessionKey.JourneyDetailsOutward];  

			if (outward)
			{
				journey = TDItineraryManager.Current.SelectedOutwardJourney;
			}
			else
			{
				journey = TDItineraryManager.Current.SelectedReturnJourney;
			}

			if (viewState == null)
			{
				return null;
			}
			else if (viewState.SelectedIntermediateItinerarySegment >= 0)
			{
				if (outward)
				{
					labelDirection.Text = GetResource("CarDetails.labelOutwardDirection.Text");
					journey = itineraryManager.GetOutwardJourney(viewState.SelectedIntermediateItinerarySegment);
				}
				else
				{
					labelDirection.Text = GetResource("CarDetails.labelReturnDirection.Text");
					journey = itineraryManager.GetReturnJourney(viewState.SelectedIntermediateItinerarySegment);
				}
			}

			if	(journey != null) 
			{
				RoadJourney roadJourney = journey as RoadJourney;
				
				if (roadJourney != null)
					labelSummary.Text = String.Concat(GetResource("CarDetails.JourneySummary.TextElement1"), roadJourney.OriginLocation.Description, GetResource("CarDetails.JourneySummary.TextElement2"), roadJourney.DepartDateTime.ToString("HH:mm"));
				
				return roadJourney;
			}
			else
			{
				return null;
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
		}
		#endregion
	}
}
