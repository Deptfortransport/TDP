// *********************************************** 
// NAME                 : CarParkInformation.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/08/2006 
// DESCRIPTION			: Displays available information
//						  on selected car park.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/CarParkInformation.aspx.cs-arc  $ 
//
//   Rev 1.7   Mar 29 2010 16:39:20   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.6   Dec 08 2009 11:26:40   mmodi
//Clear car park reference if returning to FindMapResult page
//
//   Rev 1.5   Dec 02 2009 15:57:52   mmodi
//Correctly return back to EBC and VisitPlanner pages
//
//   Rev 1.4   Nov 05 2009 14:56:26   apatel
//mapping enhancement code changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Jan 05 2009 09:30:10   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:24:10   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Feb 17 2008 18:00:00   mmodi
//Updated park and ride indicator check
//
//   Rev 1.0   Nov 08 2007 13:29:00   mturner
//Initial revision.
//
//   Rev 1.18   Oct 16 2006 17:46:48   mmodi
//Added transition event to return back to the Location Map page
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4227: Car Parking: Navigation issue from Find a Map - Car Park Information
//
//   Rev 1.17   Sep 30 2006 16:25:30   mmodi
//Added transition events to Refine pages
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4213: Car Parking: Extend journey navigation issue
//
//   Rev 1.16   Sep 29 2006 09:58:48   mmodi
//Added Transition Event to go to FindCarParkMap page
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.15   Sep 28 2006 15:29:24   esevern
//Added TransitionEvent.GoJourneyMap and GoJourneyDetails to the Back button click event so that we can return to the correct previous page from CarParkInformation
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4162: Car Parking: Navigation error when returning from information page
//
//   Rev 1.14   Sep 22 2006 14:00:52   esevern
//Amendments as result of code review
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.13   Sep 20 2006 15:10:44   esevern
//removed summaryPlannersSkipLink which was removed from the control html in version 1.12 - attempt to set the alt text on this control when clicking on back button resulted in null exception being thrown
//Resolution for 4185: Car Parking: Screen reader
//
//   Rev 1.12   Sep 19 2006 14:22:38   tmollart
//Modifications for Screen Reader skip links.
//Resolution for 4185: Car Parking: Screen reader
//
//   Rev 1.11   Sep 18 2006 17:24:04   tmollart
//Modified call to retrieve a car park from the catalogue.
//Resolution for 4190: Thread Safety Issue on Car Park Catalogue
//
//   Rev 1.10   Sep 18 2006 14:54:22   esevern
//Added transition event check for returning to JourneyDetails page in BackButtonClick event handler
//Resolution for 4163: Car Parking: Car park is not shown as a link in Details view
//
//   Rev 1.9   Sep 08 2006 14:49:28   esevern
//Amended call to CarParkCatalogue.LoadData - now only loads data on specific car park selected.
//
//Amended BackButtonClick event handler to check for calling page and return appropriately
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4162: Car Parking: Navigation error when returning from information page
//
//   Rev 1.8   Sep 05 2006 14:19:40   mmodi
//Updated check for Park and ride, for when no park and ride scheme is referenced
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.7   Sep 05 2006 11:02:30   esevern
//Added check to back button event handler to return user to correct page (location map page or find nearest results)
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.6   Sep 01 2006 13:29:08   mmodi
//Amended check for park and ride car park
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.5   Aug 16 2006 11:57:08   mmodi
//Updated to retrieve and populate Car Park object
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.4   Aug 03 2006 16:00:06   mmodi
//Updated check for Park and ride
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.3   Aug 03 2006 10:03:28   mmodi
//Added code to populate labels and controls
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.2   Aug 02 2006 12:51:22   esevern
//added pageid to constructor
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.1   Aug 02 2006 10:34:54   esevern
//added log header, corrected namespace and base class
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2

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
using TransportDirect.Common.Logging;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using ControlPopulator = TransportDirect.UserPortal.DataServices.DataServices;

using TransportDirect.UserPortal.Web.Controls;

using TransportDirect.UserPortal.LocationService;

using Logger=System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for CarParkInformation.
	/// </summary>
	public partial class CarParkInformation : TDPage
	{
		protected HeaderControl headerControl;

		
		protected CarParkInformationControl carParkInformationControl;

		private const string AT_CARPARK = "#CarPark";
		private const string AT_PARKANDRIDE = "#ParkAndRide";

		private ControlPopulator populator;

		#region Constructor, PageLoad

		/// <summary>
		/// CarParkInformation page contructor
		/// </summary>
		public CarParkInformation()
		{
			this.pageId = PageId.CarParkInformation;				
			populator = (ControlPopulator)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];		
		}

		/// <summary>
		/// Runs when the page loads
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">EventArgs</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            this.PageTitle = GetResource("CarParkInformation.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

			labelLocationInformationTitle.Visible = false;
			labelCarParkInformationTitle.Visible = true;
			labelErrorMessage.Visible = false;
			
			labelCarParkInformationTitle.Text = GetResource("CarParkInformation.labelCarParkInformationTitle");
			labelErrorMessage.Text = GetResource("CarParkInformation.labelErrorMessage");

			buttonBack.Text = GetResource("LocationInformation.buttonBack.Text");

            #region Determine context based on car parks mode
            TransportDirect.UserPortal.SuggestionLinkService.Context context;

            if (TDSessionManager.Current.FindCarParkPageState.CurrentFindMode == FindCarParkPageState.FindCarParkMode.Default)
                context = TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuFindAPlace;
            else
                context = TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney;
            #endregion

            expandableMenuControl.AddContext(context);

			// Skip link
			//imageMainContentSkipLink1.ImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
			//imageMainContentSkipLink1.AlternateText = GetResource("CarParkInformation.SkipLink_CarPark.AlternateText");
			
			// Determine the car park to display
			InputPageState inputPageState = TDSessionManager.Current.InputPageState;
			string carParkRef = inputPageState.CarParkReference;

            if (carParkRef.Length != 0)
            {
                try
                {
                    // Get the CarPark object
                    ICarParkCatalogue carParkCatalogue = (ICarParkCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.CarParkCatalogue];
                    CarPark carPark = carParkCatalogue.GetCarPark(carParkRef);

                    if (carPark != null)
                    {
                        carParkInformationControl.Data = carPark;

                        // Set the car park name label
                        labelCarParkName.Text = carParkInformationControl.Data.Name;

                        // Set the hyperlink text and urls
                        //carParkHyperlink.Text = GetResource("CarParkInformation.carParkHyperlink.Text");
                        //carParkHyperlink.NavigateUrl = AT_CARPARK;

                        // Only display the Park and Ride hyperlink if Car park is part of a P&R scheme
                        //if ((carParkInformationControl.Data.ParkAndRideIndicator.Trim().ToLower().Equals("true"))
                        //    && (carParkInformationControl.Data.ParkAndRideScheme != null))
                        //{
                        //    parkAndRideHyperlink.Text = GetResource("CarParkInformation.parkAndRideHyperlink.Text");
                        //    parkAndRideHyperlink.NavigateUrl = AT_PARKANDRIDE;
                        //}
                        //else
                        //{
                        //    parkAndRideHyperlink.Visible = false;
                        //}
                    }
                    else
                    {
                        // Failed to retrieve car park details
                        labelErrorMessage.Visible = true;
                        panelCarParkDetails.Visible = false;
                        //carParkHyperlink.Visible = false;
                        //parkAndRideHyperlink.Visible = false;
                    }
                }
                catch
                {
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "missing property in PropertyService : carpark");
                    Logger.Write(oe);
                    throw new TDException("No Car Park Information is available  : CarParkInformation", true, TDExceptionIdentifier.PSMissingProperty);
                }
            }
            else
            {
                // Because no Car Park Reference is in session, no car park details to display
                labelErrorMessage.Visible = true;
                panelCarParkDetails.Visible = false;
                //carParkHyperlink.Visible = false;
                //parkAndRideHyperlink.Visible = false;
            }

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextCarParkInformation);
            expandableMenuControl.AddExpandedCategory("Related links");
            }

		#endregion

		#region Private methods

		private void BackButtonClick(object sender, EventArgs e)
		{
			// Only need to induce a flag ensuring that this is on the way back from the info page.
			TDSessionManager.Current.SetOneUseKey( SessionKey.IndirectLocationPostBack, string.Empty );
			
			TransitionEvent te = TransitionEvent.CarParkInformationBack;
			PageId returnPage = (PageId)TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Pop();
			
			switch(returnPage) 
			{
				case PageId.FindCarParkResults:
					te = TransitionEvent.FindCarParkInputUnambiguous;
					break;
				case PageId.FindCarParkMap:
					te = TransitionEvent.FindCarParkResultsShowMap;
					break;
				case PageId.JourneyFares:
					te = TransitionEvent.GoJourneyFares;
					break;
				case PageId.JourneyDetails:
					te = TransitionEvent.GoJourneyDetails;
					break;
				case PageId.JourneyMap:
                case PageId.CycleJourneyMap:
					te = TransitionEvent.GoJourneyMap;
					break;
				case PageId.RefineDetails:
					te = TransitionEvent.RefineDetailsSchematic;
					break;
				case PageId.RefineTickets:
					te = TransitionEvent.RefineTicketsView;
					break;
				case PageId.RefineMap:
					te = TransitionEvent.RefineMapView;
					break;
				case PageId.JourneyPlannerLocationMap:
					te = TransitionEvent.LocationMapBack;
					break;
                case PageId.FindMapResult:

                    // If going back to the FindMapResult, then user has selected to "show car park information"
                    // link on the map. This has set the CarParkRef in the InputPageState, which must be cleared,
                    // otherwise the "plan a journey" function will incorrectly believe the location selected from 
                    // the map is a car park location.
                    TDSessionManager.Current.InputPageState.CarParkReference = string.Empty;

                    te = TransitionEvent.FindMapResult;
                    break;
                case PageId.EBCJourneyDetails:
                    te = TransitionEvent.EBCJourneyDetails;

                    // Indicate to the page to display the map
                    TDSessionManager.Current.SetOneUseKey(SessionKey.MapView, "true");
                    break;
                case PageId.EBCJourneyMap:
                    te = TransitionEvent.EBCJourneyMap;
                    break;
                case PageId.CycleJourneyDetails:
                    te = TransitionEvent.CycleJourneyDetails;
                    break;
                case PageId.VisitPlannerResults:
                    te = TransitionEvent.VisitPlannerResultsMapView;
                    
                    // Indicate to the page to display the map
                    TDSessionManager.Current.SetOneUseKey(SessionKey.MapView, "true");
                    break;
				default:
					te = TransitionEvent.CarParkInformationBack;
					break;
			}
			
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = te;
		}

		#endregion

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
			//Ensure eventhandlers are wired up to the HyperlinkPostBackControls on the page
			buttonBack.Click += new EventHandler(this.BackButtonClick);
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
