// ************************************************************************* 
// NAME                 : VisitPlannerAdapter.cs 
// AUTHOR               : Tolu Olomolaiye
// DATE CREATED         : 06/10/2005
// DESCRIPTION			: Adapter class that provides 
// visit planner specific helper adapter methods
// ************************************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/VisitPlannerAdapter.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 12:59:16   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:32   mturner
//Initial revision.
//
//   Rev 1.12   Apr 05 2006 15:42:52   build
//Automatically merged from branch for stream0030
//
//   Rev 1.11.1.0   Mar 29 2006 11:15:30   RGriffith
//Wait Page Ehancement changes
//Resolution for 33: DEL 8.1 Workstream: Wait Pages
//
//   Rev 1.11   Feb 23 2006 17:19:12   RWilby
//Merged stream3129
//
//   Rev 1.10   Feb 10 2006 15:04:38   build
//Automatically merged from branch for stream3180
//
//   Rev 1.9.1.1   Dec 22 2005 10:54:54   tmollart
//Removed references to OldJourneyParameters.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.9.1.0   Dec 12 2005 17:16:24   tmollart
//Removed references to OldFindAMode.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.9   Nov 15 2005 16:13:32   asinclair
//IR Fix - ensure that Advanced options are not visible when user returns to input page
//Resolution for 3044: Visit Planner - Using New Search still displays Advanced options, but they are reset to the default values successfully
//
//   Rev 1.8   Nov 10 2005 11:50:30   jgeorge
//Updated to set AsyncCallState.AmbiguityPage to visit planner results if amending an itinerary.
//
//   Rev 1.7   Nov 10 2005 10:21:24   jbroome
//Updated to set SelectedItemJourneyIndex property of RouteSelectionControl
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.6   Nov 10 2005 10:16:42   jgeorge
//Changed to use AsyncCallState class instead of JourneyPlanStateData/JourneyPlanControlData
//
//   Rev 1.5   Nov 10 2005 09:21:00   asinclair
//Bug fix - set ambiguity mode to false when results are cleared down
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.4   Oct 28 2005 17:11:06   tolomolaiye
//Updates following code review and running fxcop
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.3   Oct 19 2005 16:04:38   jbroome
//Updated ClearDownRequestAndResults
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.2   Oct 18 2005 15:10:32   tolomolaiye
//Updated parameter names
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1   Oct 07 2005 16:10:26   tolomolaiye
//Initial Revision
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0   Oct 07 2005 10:05:42   tolomolaiye
//Initial revision.

using System;
using System.Text;
using System.Threading;
using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.VisitPlanRunner;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Web.Support;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Web.Controls;


namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// This adapter class provides visit planner specific helper/adapter methods. These methods are 
	/// provided to populate all the required properties of the controls and handle any visit 
	/// planner specific events and transitions.
	/// </summary>
	public class VisitPlannerAdapter : TDWebAdapter
	{
		/// <summary>
		/// Enumeration used to determine what type of search is performed
		/// This is used in method VisitPlannerWaitPageTransition
		/// </summary>
		public enum SearchType
		{
			Multiple,
			Single
		}

		#region Earlier/Later Route Selection

		/// <summary>
		/// Finds earlier journeys based on the details supplied
		/// </summary>
		/// <param name="itineraryManager">The current itineraryManager</param>
		/// <param name="segmentIndex">The index of the current segment of the journey</param>
		public void RouteSelectionEarlier(VisitPlannerItineraryManager itineraryManager, int segmentIndex)
		{
			ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];
			
			VisitPlannerWaitPageTransition(SearchType.Single);
			itineraryManager.ExtendJourneyResultEarlier(segmentIndex);
			
			VisitPlannerRunner runner = new VisitPlannerRunner();
			runner.RunAddJourneys(sessionManager);
		}

		/// <summary>
		/// Finds later journeys based on the details supplied
		/// </summary>
		/// <param name="itineraryManager">The current itineraryManager</param>
		/// <param name="segmentIndex">The index of the current segment of the journey</param>
		public void RouteSelectionLater(VisitPlannerItineraryManager itineraryManager, int segmentIndex)
		{
			ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];
			
			VisitPlannerWaitPageTransition(SearchType.Single);
			itineraryManager.ExtendJourneyResultLater(segmentIndex);

			VisitPlannerRunner runner = new VisitPlannerRunner();
			runner.RunAddJourneys(sessionManager);
		}
		#endregion

		#region Start and End Methods 

		/// <summary>
		/// This adapter method is provided to clear down all request and result information 
		/// used by visit planner.
		/// </summary>
		/// <param name="itineraryManager">The itineraryManager object</param>
		/// <param name="visitState">The ResultsPage object</param>
		public void ClearDownRequestAndResults(TDItineraryManager itineraryManager, ResultsPageState visitState)
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;

			itineraryManager.ResetItinerary();
			visitState.CurrentViewSelection = 0;
			
			if (sessionManager.JourneyResult != null)
			{
				sessionManager.JourneyResult.IsValid = false;
			}

			visitState.ResultsMode = ResultsModes.Summary;

			sessionManager.InputPageState.AmbiguityMode = false;
			sessionManager.InputPageState.AdvancedOptionsVisible = false;
		}

		/// <summary>
		/// This method calls validates the journey details entered and call 
		/// the wait page while the results are being processed 
		/// </summary>
		/// <param name="itineraryManager">The itineraryManager of the current journey</param>
		/// <returns>true if results are found or false if there are errors</returns>
		public bool ValidateAndSearch()
		{
			VisitPlannerWaitPageTransition(SearchType.Multiple);

			VisitPlannerRunner runner = new VisitPlannerRunner();
			return runner.ValidateAndRunInitialItinerary(TDSessionManager.Current);
		}
		
		/// <summary>
		/// Ensures that transition to the wait page is handled in a common way
		/// </summary>
		/// <param name="searchType">The search type taken from the SearchType ennumeration</param>
		public void VisitPlannerWaitPageTransition(SearchType searchType)
		{
			AsyncCallState acs = new VisitPlanState();

			// Determine refresh interval and resource string for the wait page
			acs.WaitPageRefreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.VisitPlanner"]);
			acs.WaitPageMessageResourceFile = "langStrings";
			acs.WaitPageMessageResourceId = "WaitPageMessage.VisitPlanner";

			acs.ErrorPage = PageId.VisitPlannerResults;
			acs.DestinationPage = PageId.VisitPlannerResults;

			if (searchType == SearchType.Multiple)
			{
				acs.AmbiguityPage = PageId.VisitPlannerInput;
				acs.WaitPageTimeout = Convert.ToInt32(Properties.Current["MultipleJourneyInput.WaitPageTimeoutSeconds"], TDCultureInfo.CurrentUICulture.NumberFormat);
			}
			else
			{
				acs.AmbiguityPage = PageId.VisitPlannerResults;
				acs.WaitPageTimeout = Convert.ToInt32(Properties.Current["RouteSelection.WaitPageTimeoutSeconds"], TDCultureInfo.CurrentUICulture.NumberFormat);
			}

			TDSessionManager.Current.AsyncCallState = acs;
		}
		#endregion

		#region Populate Visit Planner Controls

		/// <summary>
		/// Populate the details on the VisitPlannerRequestDetailsControl
		/// </summary>
		/// <param name="control">the VisitPlannerRequestDetailsControl</param>
		/// <param name="paramsVistPlan">The paramter used to populate the control</param>
		public void PopulateVisitPlannerRequestDetailsControl(VisitPlannerRequestDetailsControl control, TDJourneyParametersVisitPlan parameterVisitPlan)
		{
			StringBuilder visitDateTime = new StringBuilder();
			DisplayFormatAdapter displayAdapter = new DisplayFormatAdapter();
			int durationInMinutes;
			int hours;

			//from location
			control.FromLocation = parameterVisitPlan.GetLocation(0).Description;

			//first vist
			control.FirstVisitLocation = parameterVisitPlan.GetLocation(1).Description;

			//second visit
			control.SecondVisitLocation = parameterVisitPlan.GetLocation(2).Description;

			//visit date/time
			//Build up the date and time.
			visitDateTime.Append(parameterVisitPlan.OutwardDayOfMonth);
			visitDateTime.Append(" ");
			visitDateTime.Append(parameterVisitPlan.OutwardMonthYear);
			visitDateTime.Append(" ");
			visitDateTime.Append(parameterVisitPlan.OutwardHour);
			visitDateTime.Append(":");
			visitDateTime.Append(parameterVisitPlan.OutwardMinute);

			control.VisitDateTime = DisplayFormatAdapter.StandardDateAndTimeFormat(TDDateTime.Parse(visitDateTime.ToString(), Thread.CurrentThread.CurrentCulture));

			//options
			control.Options = displayAdapter.NonDefaultAdvancedOptionsDisplayText(parameterVisitPlan);
			
			//length of stay(1)
			durationInMinutes = parameterVisitPlan.GetStayDuration(0);
			hours = durationInMinutes / 60;
			control.FirstVisitLengthOfStay = hours.ToString(Thread.CurrentThread.CurrentCulture);

			//length of stay(2)
			durationInMinutes = parameterVisitPlan.GetStayDuration(1);
			hours = durationInMinutes / 60;
			control.SecondVisitLengthOfStay = hours.ToString(Thread.CurrentThread.CurrentCulture);

			//return to origin
			control.ReturnToOrigin = displayAdapter.BooleanToDisplayText(parameterVisitPlan.ReturnToOrigin);
		}

		/// <summary>
		/// Populates the JourneyLineControl object
		/// </summary>
		/// <param name="control">The journey line control</param>
		/// <param name="paramsVistPlan"></param>
		public void PopulateJourneyLineControl (JourneyLineControl control, TDJourneyParametersVisitPlan parameterVisitPlan)
		{
			control.ReturnToOrigin = parameterVisitPlan.ReturnToOrigin;

			//get the tdlocation collection
			TDLocation[] visitPlannerLocation = new TDLocation[3];

			for (int i = 0; i <= 2; i++) 
			{
				visitPlannerLocation[i] =  parameterVisitPlan.GetLocation(i);
			}

			control.DataSource = visitPlannerLocation;
		}

		/// <summary>
		/// Population of the RouteSelectionControl. The RouteSelectionControl object passed as 
		/// a parameter and populated from the current VisitPlannerItineraryManager object.
		/// </summary>
		/// <param name="control"></param>
		/// <param name="itineraryManager"></param>
		/// <param name="segmentIndex">The paramter used to populate the control</param>
		/// <returns></returns>
		public void PopulateRouteSelectionControl (RouteSelectionControl control, VisitPlannerItineraryManager itineraryManager, int segmentIndex)
		{
			ResultsAdapter visitResults = new ResultsAdapter();

			control.DataSource = visitResults.GetOutwardJourneyResults(itineraryManager, segmentIndex);
			control.SelectedItemJourneyIndex = visitResults.GetSelectedOutwardJourneyIndex(itineraryManager, segmentIndex);
			control.EnableNavigation = itineraryManager.ExtendSegmentPermitted(segmentIndex);
			control.CommandArgumentSegmentIndex = segmentIndex;
		}


		#endregion

	}
}