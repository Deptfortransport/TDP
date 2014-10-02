// *********************************************************
// NAME			: ReplanItineraryManager.cs
// AUTHOR		: Richard Hopkins
// DATE CREATED	: 16/12/2005
// DESCRIPTION	: Itinerary Manager for Replan Journey 
//                functionality
// ********************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/ReplanItineraryManager.cs-arc  $
//
//   Rev 1.3   Dec 05 2012 13:59:12   mmodi
//Supress unnecessary warnings during compile
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.2   Mar 14 2011 15:11:58   rphilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.1   Feb 09 2009 15:18:58   mmodi
//Updated code to apply Routing Guide Sections logic to the replanned Public Journey
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.0   Nov 08 2007 12:48:34   mturner
//Initial revision.
//
//   Rev 1.34   Jun 06 2006 18:06:28   rphilpott
//Changes to prevent unnecessary creation of new PricingRetailOptionsState objects when pricing extended/replanned journeys that include car sections.
//Resolution for 4053: DN068: Amend tool when used on Extend pages loses selected values
//
//   Rev 1.33   May 16 2006 11:33:42   rphilpott
//Allow for no return itinerary in overlapping journeys check.
//Resolution for 4081: DN068 Replan: Error when replanning part of outward-only journey
//
//   Rev 1.32   May 03 2006 11:38:00   rwilby
//Added HasOverlappingJourneys property.
//Resolution for 4056: DN068: Replan - No warning message is shown when a time overlap occurs
//
//   Rev 1.31   May 02 2006 15:24:10   COwczarek
//Modified BuildJourneyRequest method in
//ReplanItineraryManager to only use location details of journey
//selected for replan rather than original journey request.
//Resolution for 3676: DN068 Replan: No car replacement journeys found for a journey from Brighton to Sunderland
//
//   Rev 1.30   Apr 28 2006 10:55:40   RPhilpott
//Bug in return journey handling in last fix.
//Resolution for 4035: DN068 Replan: Replacing PT journey with Car uses wrong time
//
//   Rev 1.29   Apr 28 2006 10:33:46   RPhilpott
//Use times from actual journey being replaced, not original request times, when replacing an entire PT journey with car.
//Resolution for 4035: DN068 Replan: Replacing PT journey with Car uses wrong time
//
//   Rev 1.28   Apr 27 2006 16:34:04   RPhilpott
//Correct handling of Replan when in cost-based partition. 
//Resolution for 4012: DD075: Server error viewing costs for PT journey replaced by car
//
//   Rev 1.27   Apr 25 2006 14:06:12   tmollart
//Modified code to populate toids for car replans. Partial fix for IR3676.
//Resolution for 3676: DN068 Replan: No car replacement journeys found for a journey from Brighton to Sunderland
//
//   Rev 1.27   Apr 25 2006 13:42:34   tmollart
//Modified code to get Toids for locations that dont have the. Tidied up the code in places. This is a partial fix for IR3676.
//Resolution for 3676: DN068 Replan: No car replacement journeys found for a journey from Brighton to Sunderland
//
//   Rev 1.26   Apr 19 2006 12:01:12   mdambrine
//a check is being done if the locality of both the origin and the destination is known.
//
//In case it isen't a call to the findnearestlocality is done and the planning is continued.
//Resolution for 3921: DN068 Replan: Refine car journey - cannot replace part of car journey with PT
//
//   Rev 1.25   Apr 07 2006 19:36:58   rhopkins
//Corrected the logic used to calculate the date for the replan request.  This will now cope with Frequency and Continuous legs and also with checkin/exit times.
//Resolution for 3674: DN068 Replan:  Car segment of replanned journey departing at incorrect time
//Resolution for 3677: DN068: Extend, Replan & Adjust: Error attempting to Replan last leg of a PT journey
//Resolution for 3679: DN068 Replan:  Replanning flight legs with car result in Overlapping times
//Resolution for 3718: DN068 Replan: Replanning a walk leg following a bus service gives no journey options
//Resolution for 3837: DN068 Replan:  Error when replanning journey legs when not at the beginning or end of the journey
//
//   Rev 1.24   Apr 06 2006 14:31:54   RGriffith
//Fix to get map screens to load correctly from Replan pages
//
//   Rev 1.23   Mar 29 2006 10:57:58   tmollart
//Modified methods to allow use with unit tests.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.22   Mar 23 2006 13:54:52   NMoorhouse
//Added properties to indicate whether outward and/or return journeys have been replanned
//Resolution for 3663: Extend, Replan & Adjust: Replan a return journey and result allows to replan return again
//
//   Rev 1.21   Mar 22 2006 20:27:38   rhopkins
//Minor FxCop fixes.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.20   Mar 22 2006 16:32:02   rhopkins
//Minor code-review fixes.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.19   Mar 17 2006 15:59:36   tmollart
//Changed method that builds journey result so that it populated avoid/include roads with empty string array if not already populated.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.18   Mar 14 2006 17:16:58   tmollart
//Post merge fixes. Stream 3353.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.17   Mar 13 2006 20:09:04   rhopkins
//Corrections to issues discovered during Nunit testing.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.16   Mar 10 2006 14:31:20   pcross
//Replan results summary now returns all modes (not just a unique list)
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.15   Mar 10 2006 14:26:32   tmollart
//Updates from code review.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.14   Mar 10 2006 11:47:12   NMoorhouse
//Ensure correct journey is selected from front end selected segment
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.13   Mar 09 2006 09:33:06   tmollart
//Updates from FXCop.
//
//   Rev 1.12   Mar 08 2006 17:03:26   NMoorhouse
//Get the defualt fuel type and car size from data services
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.11   Mar 08 2006 13:00:44   RGriffith
//Fix to pick correct start/end return locations and to set correct pricing options state
//
//   Rev 1.10   Mar 08 2006 10:46:52   tmollart
//Work in progress.
//
//   Rev 1.9   Mar 07 2006 16:24:20   NMoorhouse
//Set defaults for Fuel values
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.8   Mar 06 2006 17:23:12   RGriffith
//Changes made for tickets/costs
//
//   Rev 1.7   Mar 03 2006 16:02:48   tmollart
//Added ResetToInitialJourney method.
//
//   Rev 1.6   Mar 01 2006 13:07:04   RGriffith
//Changes for use with Tickets and Costs page
//
//   Rev 1.5   Feb 28 2006 18:05:34   NMoorhouse
//Added override methods InitialiseSearch and ClearItinerary
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Feb 22 2006 12:19:30   NMoorhouse
//new methods required for replan
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Feb 17 2006 14:15:24   NMoorhouse
//Updates (by myself, Tim Mollart and Richard Hopkins) to support Replan Details and Maps
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Feb 15 2006 17:22:18   tmollart
//Work in progress.
//
//   Rev 1.1   Feb 07 2006 19:52:22   tmollart
//Work in progress.
//
//   Rev 1.0   Dec 20 2005 20:03:24   rhopkins
//Initial revision.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//

using System;
using System.Text;
using System.Collections;
using System.Globalization;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.Presentation.InteractiveMapping;

using TDPublicJourney = TransportDirect.UserPortal.JourneyControl.PublicJourney;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Class that implements an ItineraryManager to support the Replan facility
	/// </summary>
	[Serializable(), CLSCompliant(false)]
	public class ReplanItineraryManager : TDItineraryManager
	{

		#region Private Members

		/// <summary>
		/// Original outward journey.
		/// </summary>
		private Journey originalOutwardJourney;
		
		/// <summary>
		/// Original return journey.
		/// </summary>
		private Journey originalReturnJourney;

		/// <summary>
		/// Outward itinerary array.
		/// </summary>
		private ArrayList outwardReplannedItinerary;
		
		/// <summary>
		/// Return itinerary array.
		/// </summary>
		private ArrayList returnReplannedItinerary;

		/// <summary>
		/// Outward pricing data.
		/// </summary>
		private PricingRetailOptionsState[] outwardOptions;

		/// <summary>
		/// Return pricing data.
		/// </summary>
		private PricingRetailOptionsState[] returnOptions;

		/// <summary>
		/// Journey that will be replanned.
		/// </summary>
		private Journey journeyForReplan;

		/// <summary>
		/// Request generated for the outward replan
		/// </summary>
		private ITDJourneyRequest outwardReplanRequest;

		/// <summary>
		/// Request generated for the return replan
		/// </summary>
		private ITDJourneyRequest returnReplanRequest;

		/// <summary>
		/// Flag to indicate if the Outward journey has been replanned
		/// </summary>
		private bool outwardReplanned;

		/// <summary>
		/// Flag to indicate if the Return journey has been replanned
		/// </summary>
		private bool returnReplanned;

		/// <summary>
		/// When a request is built this stores if the request was an
		/// arrive before or a leave after.
		/// </summary>
		bool arriveBefore;

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor. 
		/// </summary>
		public ReplanItineraryManager()
		{
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Creates the replan itinerary from the outwward and return journeys
		/// current held in the supplied session manager instance. Note: That this
		/// method will automatically create the required replan page state.
		/// </summary>
		///<param name="sessionManager">Session manager to get data from.</param>
		///<param name="outward">Outward or return journey.</param>
		public void CreateItinerary(ITDSessionManager sessionManager, bool outward)
		{

			ReplanPageState pageState;

			// Populate outward journey
			if (originalOutwardJourney == null)
			{
				if (sessionManager.JourneyViewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal)
				{
					originalOutwardJourney = sessionManager.JourneyResult.OutwardPublicJourney(sessionManager.JourneyViewState.SelectedOutwardJourneyID);
				}
				else
				{
					originalOutwardJourney = sessionManager.JourneyResult.OutwardRoadJourney();
				}
			}

			// Populate return journey
			if (originalReturnJourney == null)
			{
				if (sessionManager.JourneyResult.ReturnPublicJourneyCount > 0 || sessionManager.JourneyResult.ReturnRoadJourneyCount > 0)
				{
					if (sessionManager.JourneyViewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal)
					{
						originalReturnJourney = sessionManager.JourneyResult.ReturnPublicJourney(sessionManager.JourneyViewState.SelectedReturnJourneyID);
					}
					else
					{
						originalReturnJourney = sessionManager.JourneyResult.ReturnRoadJourney();
					}
				}
			}

			// Create outward and return itinerarys
			if ((outwardReplannedItinerary == null) || (outwardReplannedItinerary.Count == 0))
			{
				if (originalOutwardJourney != null)
				{
					outwardReplannedItinerary = new ArrayList();
					outwardReplannedItinerary.Add(originalOutwardJourney);
				}
			}

			if ((returnReplannedItinerary == null) || (returnReplannedItinerary.Count == 0))
			{
				if (originalReturnJourney != null)
				{
					returnReplannedItinerary = new ArrayList();
					returnReplannedItinerary.Add(originalReturnJourney);
				}
			}


			// Create replan page state.
			if ( !(sessionManager.InputPageState is ReplanPageState) )
			{
				sessionManager.InputPageState = new ReplanPageState();
			}

			pageState = (ReplanPageState)sessionManager.InputPageState;

			if (pageState.OriginalRequest == null)
			{
				pageState.OriginalRequest = sessionManager.JourneyRequest;
			}

			if (outward)
			{
				journeyForReplan = originalOutwardJourney;
				pageState.CurrentAmendmentType = TDAmendmentType.OutwardJourney;
			}
			else
			{
				journeyForReplan = originalReturnJourney;
				pageState.CurrentAmendmentType = TDAmendmentType.ReturnJourney;
			}

			pageState.JourneySelectedForReplan = this.journeyForReplan;
			pageState.ReplanStartJourneyDetailIndex = -1;
			pageState.ReplanEndJourneyDetailIndex = -1;

			currentJourneyViewState = sessionManager.JourneyViewState;

			// Create new outward and return Pricing options as appropriate (of fixed length)
			outwardOptions = new PricingRetailOptionsState[outwardReplannedItinerary.Count];
			if (returnReplannedItinerary != null)
			{
				returnOptions = new PricingRetailOptionsState[returnReplannedItinerary.Count];
			}

			base.pricingDataComplete = false;

		}

		/// <summary>
		/// Initialise Search. Calls base class implentation
		/// to reset all required objects for a new search
		/// </summary>
		protected override void InitialiseSearch()
		{
			TDJourneyParameters newJourneyParameters = new TDJourneyParametersMulti();
			TDSessionManager.Current.JourneyParameters = newJourneyParameters;

			base.InitialiseSearch ();
		}


		/// <summary>
		/// Clears the current itinerary and reinitialises the Replan Itinerarys
		/// </summary>
		protected override void ClearItinerary()
		{
			// Clear the Replan Itinerarys
			outwardReplannedItinerary = new ArrayList();
			returnReplannedItinerary = new ArrayList();

			// Clear original data
			originalOutwardJourney = null;
			originalReturnJourney = null;

			ReplanPageState pageState = TDSessionManager.Current.InputPageState as ReplanPageState;
			if (pageState != null)
			{
				pageState.OriginalRequest = null;
			}

			base.ClearItinerary ();
		}

		/// <summary>
		/// Resets itinerary to inital journey.
		/// </summary>
		public override void ResetToInitialJourney()
		{
			// For replan just a matter of clearing the itinerary as journey should
			// still remain in session manager.
			ClearItinerary();
		}
		
		/// <summary>
		/// Generate a journey request for replanning a journey.
		/// </summary>
		/// <param name="originalRequest">Journey request used for original journey.</param>
		/// <param name="startIndex">Start index of journey leg to replan.</param>
		/// <param name="endIndex">End index of journey leg to replan.</param>
		/// <param name="outward">Outward or return journy.</param>
		/// <returns>Journey requst for replanning the journey.</returns>
		public ITDJourneyRequest BuildReplanJourneyRequest(
			ITDJourneyRequest originalRequest, 
			int startIndex,
			int endIndex,
			bool outward)
		{
			
			TDJourneyRequest request = new TDJourneyRequest();			// New Journey Request that will be generated.
			
			TDLocation startLocation;	// Start and end location of the journey								
			TDLocation endLocation;
			
			// Cast passed in journey as both of the available type of journeys. Call
			// get GetJourneyForReplan returns the journey that the user wants to replan
			// depending on if they have chose the outward or return journey.
			TDPublicJourney publicJourney = this.journeyForReplan as TDPublicJourney;
			RoadJourney roadJourney = this.journeyForReplan as RoadJourney;

			request.OutwardDateTime = new TDDateTime[1];

			// Required to populate missing bits in locations.
			IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

			// Replanning as a Road Journey (Public Transport journey exists)			
			if (publicJourney != null)
			{
				int lastDetailIndex = publicJourney.Details.Length - 1;
				// The journey is a public transport journey. We want to turn
				// this into a road journey.
				request.Modes = new ModeType[] { ModeType.Car };

				// Logic to work out when journey time and arrive by / leave after status.
				// Work out if journey is leave after by looking at relevant attribute
				// of the original request. May be overwritten if required.
				if (outward)
				{
					request.OutwardArriveBefore = originalRequest.OutwardArriveBefore;

					if (originalRequest.OutwardArriveBefore)
					{
						request.OutwardDateTime[0] = publicJourney.Details[lastDetailIndex].LegEnd.ArrivalDateTime;
					}
					else
					{
						request.OutwardDateTime[0] = publicJourney.Details[0].LegStart.DepartureDateTime;
					}
				}
				else
				{
					request.OutwardArriveBefore = originalRequest.ReturnArriveBefore;

					if (originalRequest.ReturnArriveBefore)
					{
						request.OutwardDateTime[0] = publicJourney.Details[lastDetailIndex].LegEnd.ArrivalDateTime;
					}
					else
					{
						request.OutwardDateTime[0] = publicJourney.Details[0].LegStart.DepartureDateTime;
					}
				}

				// START LOCATION

				// If the replan start is details element 0 then use the origin of the first leg
                // rather than the destination of a preceeding leg
				if (startIndex == 0)
				{

                    startLocation = publicJourney.Details[startIndex].LegStart.Location;
                    
					// If endIndex not the end of the journey then arrive
					// by start of non-replanned Public Transport section.
					if (endIndex < lastDetailIndex)
					{
						request.OutwardArriveBefore = true;
					}
				}
				else
				{
					// If the replan start is details element greater than zero then the start
					// location is always the END of the previous leg. That is for example the
					// station that they will alight at after the previous leg.
					startLocation = publicJourney.Details[startIndex - 1].LegEnd.Location;
				}

				// END LOCATION

				// If replan end is details element LAST then use the destination of the last leg rather
                // than the origin of a succeeding leg
				if (endIndex == lastDetailIndex)
				{

                    endLocation = publicJourney.Details[endIndex].LegEnd.Location;

					// If startIndex not the start of the journey then depart 
					// after the end of the non-replanned Public Transport section.
					if (startIndex > 0)
					{
						request.OutwardArriveBefore = false;
					}
				}
				else
				{
					endLocation = publicJourney.Details[endIndex + 1].LegStart.Location;
				}

				// If we are not replanning the whole journey then we need to determine the correct time to use
				if ((startIndex > 0) || (endIndex < lastDetailIndex))
				{
					request.OutwardDateTime[0] = CalculateReplanRequestTime(publicJourney.Details, startIndex, endIndex, request.OutwardArriveBefore);
				}

				// Ensure TOIDS are popualted. This requires a grid reference. We wont have got this
				// far if the chosen locations dont have grid references as the user will have been
				// stopped at JourneyReplanInput.
				startLocation.PopulateToids();
				endLocation.PopulateToids();

				// Set locations onto request object and set additional properties that
				// are required on the request object.
				request.OriginLocation = startLocation;
				request.DestinationLocation = endLocation;
				request.VehicleType = VehicleType.Car;
			}

			// Replanning as a Public Transport journey (Road Journey exists)
			if (roadJourney != null)
			{
				// The journey is a road journey
				request.Modes = new ModeType[] { ModeType.Air, ModeType.Bus, ModeType.Coach, ModeType.Ferry, ModeType.Metro, ModeType.Rail, ModeType.Tram, ModeType.Underground};

				// This journey needs to be based on the original request and not on the
				// journey as with Public Transport
				if (outward)
				{
					// Replanning the outward journey
					request.OriginLocation = originalRequest.OriginLocation;
					request.DestinationLocation = originalRequest.DestinationLocation;
					request.OutwardArriveBefore = originalRequest.OutwardArriveBefore;
					request.OutwardDateTime = originalRequest.OutwardDateTime;
				}
				else
				{
					// Replanning the return journey
					request.OriginLocation = originalRequest.DestinationLocation;
					request.DestinationLocation = originalRequest.OriginLocation;
					request.OutwardArriveBefore = originalRequest.ReturnArriveBefore;
					request.OutwardDateTime = originalRequest.ReturnDateTime;
				}		
		
				// If the locality for the locations has not been set then look it up.
				if (request.OriginLocation.Locality.Length == 0)
				{
					request.OriginLocation.Locality = gisQuery.FindNearestLocality(request.OriginLocation.GridReference.Easting, request.OriginLocation.GridReference.Northing);
				}

				if (request.DestinationLocation.Locality.Length == 0)
				{
					request.DestinationLocation.Locality = gisQuery.FindNearestLocality(request.DestinationLocation.GridReference.Easting, request.DestinationLocation.GridReference.Northing);
				}
			}

			// Non mode specific propertys
			request.IsReturnRequired = false;
			request.AlternateLocations = null;
			request.AlternateLocationsFrom = false;
			request.InterchangeSpeed = originalRequest.InterchangeSpeed;
			request.WalkingSpeed = originalRequest.WalkingSpeed;
			request.MaxWalkingTime = originalRequest.MaxWalkingTime;
			request.DrivingSpeed = originalRequest.DrivingSpeed;
			request.AvoidMotorways = originalRequest.AvoidMotorways;
            request.BanUnknownLimitedAccess = originalRequest.BanUnknownLimitedAccess;
			request.PrivateAlgorithm = originalRequest.PrivateAlgorithm;
			request.PublicAlgorithm = originalRequest.PublicAlgorithm;

			// Populate avoid roads string array if not already done.
			request.AvoidRoads = ( originalRequest.AvoidRoads == null ? new string[0] : originalRequest.AvoidRoads );
			request.IncludeRoads = ( originalRequest.IncludeRoads == null ? new string[0] : originalRequest.IncludeRoads );

			// If fuel consumption was not set up when the original journey request was
			// created then it needs to be done here or road planning wont work.
			IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			string defaultCarSize  = string.Empty;
			string defaultFuelType = string.Empty;
			
			if (originalRequest.FuelConsumption != null)
			{
				request.FuelConsumption = originalRequest.FuelConsumption;
			}
			else
			{
				defaultCarSize  = ds.GetDefaultListControlValue(DataServiceType.ListCarSizeDrop);
				defaultFuelType = ds.GetDefaultListControlValue(DataServiceType.ListFuelTypeDrop);
				CarCostCalculator costCalculator = (CarCostCalculator)TDServiceDiscovery.Current[ ServiceDiscoveryKey.CarCostCalculator ];	
				request.FuelConsumption = costCalculator.GetFuelConsumption(defaultCarSize, defaultFuelType).ToString(CultureInfo.InvariantCulture);
			}

			if (originalRequest.FuelPrice != null)
			{
				request.FuelPrice = originalRequest.FuelPrice;
			}
			else
			{
				if (defaultFuelType.Length == 0)
				{
					defaultFuelType = ds.GetDefaultListControlValue(DataServiceType.ListFuelTypeDrop);
				}
				CarCostCalculator costCalculator = (CarCostCalculator)TDServiceDiscovery.Current[ ServiceDiscoveryKey.CarCostCalculator ];	
				request.FuelPrice = costCalculator.GetFuelCost(defaultFuelType).ToString(CultureInfo.InvariantCulture);
			}

			// Store depart after / arrive before status
			arriveBefore = request.OutwardArriveBefore;

			// This is needed because we will need to come back to this object later
			// on and known which part of the original journey was replanned so a 
			// a reference is maintained on this object.
			if (outward)
			{
				outwardReplanRequest = request;
			}
			else
			{
				returnReplanRequest = request;
			}

            // Set the Routing guide flags from the original request
            request.RoutingGuideInfluenced = originalRequest.RoutingGuideInfluenced;
            request.RoutingGuideCompliantJourneysOnly = originalRequest.RoutingGuideCompliantJourneysOnly;

			return request;
		}


		/// <summary>
		/// Intserts a given journey result into the replan itinerary. This will use the best
		/// journey from the given journey result.
		/// </summary>
		/// <param name="result">Journey result object to use.</param>
		/// <param name="startIndex">Replan start index.</param>
		/// <param name="endIndex">Replan end index.</param>
		/// <param name="outward">Outward or return.</param>
		public void InsertReplan(ITDJourneyResult result, int startIndex, int endIndex, bool outward)
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;
			PublicJourneyDetail[] publicJourneyDetails;
			JourneyControl.PublicJourney newJourney;

			Journey targetJourneyForReplan;
			int index = 0;
			int routeNumber = SqlHelper.GetRefNumInt() * 200;

			//Add journeys to map hand off database.				
			ITDMapHandoff handoff = (ITDMapHandoff) TDServiceDiscovery.Current[ServiceDiscoveryKey.TDMapHandoff];

			ArrayList newItinerary = new ArrayList();

			if (outward)
			{
				targetJourneyForReplan = this.originalOutwardJourney;
			}
			else
			{
				targetJourneyForReplan = this.originalReturnJourney;
			}

			// If we are dealing with a public journey the then a new itinerary needs
			// to be constructed contining all segements of the original and the replanned
			// portion. Otherwise the entire itinerary needs to be replaced with the new
			// public transport journey.
			if (targetJourneyForReplan is TDPublicJourney)
            {
                #region Add start of Public transport journey

                // Insert first section of journey.
				// If the replanned journey is not right at the start we need to create a journey for the
				// first part of the journey
				if (startIndex > 0)
				{
					publicJourneyDetails = new PublicJourneyDetail[startIndex];
					Array.Copy(targetJourneyForReplan.JourneyLegs, 0, publicJourneyDetails, 0, startIndex);

					int lastDetailIndex = publicJourneyDetails.Length - 1;

					// Ensure that arrival time is correct for Frequency or Continuous leg at end of first segment
					if ((publicJourneyDetails[lastDetailIndex] is PublicJourneyFrequencyDetail)
						|| (publicJourneyDetails[lastDetailIndex] is PublicJourneyContinuousDetail))
					{
						publicJourneyDetails[lastDetailIndex].LegEnd.ArrivalDateTime = CalculateDepartOrArrivalTime(publicJourneyDetails, false);
					}

					newJourney = new JourneyControl.PublicJourney(index++, publicJourneyDetails, TDJourneyType.PublicOriginal, ++routeNumber);

                    newJourney = UpdateRoutingGuideSections((JourneyControl.PublicJourney)targetJourneyForReplan, newJourney, 0);

					handoff.SaveJourneyResult(sessionManager.Session.SessionID, newJourney);

					newItinerary.Add(newJourney);
                }

                #endregion

                #region Add Car journey 
                
                // Insert replanned journey into the new itinerary.	
				newItinerary.Add(result.OutwardRoadJourney());

                #endregion

                #region Add end of Public transport journey

                // Insert rest of journey
				if (endIndex < targetJourneyForReplan.JourneyLegs.Length - 1)
				{
					int length = (targetJourneyForReplan.JourneyLegs.Length - endIndex) -1;

					publicJourneyDetails = new PublicJourneyDetail[length];
					Array.Copy(targetJourneyForReplan.JourneyLegs, endIndex + 1, publicJourneyDetails, 0, length);

					// Ensure that departure time is correct for Frequency or Continuous leg at start of last segment
					if ((publicJourneyDetails[0] is PublicJourneyFrequencyDetail) || (publicJourneyDetails[0] is PublicJourneyContinuousDetail))
					{
						publicJourneyDetails[0].LegStart.DepartureDateTime = CalculateDepartOrArrivalTime(publicJourneyDetails, true);
					}

					newJourney = new JourneyControl.PublicJourney(index++, publicJourneyDetails, TDJourneyType.PublicOriginal, ++routeNumber);

                    newJourney = UpdateRoutingGuideSections((JourneyControl.PublicJourney)targetJourneyForReplan, newJourney, (endIndex + 1));

					handoff.SaveJourneyResult(sessionManager.Session.SessionID, newJourney);

					newItinerary.Add(newJourney);
                }

                #endregion


            }
			else
            {
                #region Replace Car journey with best Public Transport journey
                // If we have replanned for a public transport journey then we need to
				// only need to return one of the (possibly) many available public transport
				// journeys available.
				if (result.OutwardPublicJourneyCount > 1)
				{
					// Dependant on if this was a depart after / arrive before 
					// journey then we need to sort the journeys in the correct
					// order prior to removing the ones not needed.
					IComparer comparer;

					if (outward)
					{
						comparer = new DepartAfterComparer();
					}
					else
					{
						comparer = new ArriveBeforeComparer();
					}

					// Sort the journeys in the required order.
					result.OutwardPublicJourneys.Sort((IComparer)comparer);

					// Now journesy have been sorted the array of returned journeys
					// can be trimmed to leave one remaining. Start at index one and remove
					// all but one
					result.OutwardPublicJourneys.RemoveRange(1, result.OutwardPublicJourneyCount - 1);
				}

				// Add the only remaining journey to the itinerary array list.
				newItinerary.Add(result.OutwardPublicJourneys[0]);

				// Pass new journey details to ESRI database for subsequent map display.
				handoff.SaveJourneyResult(TDSessionManager.Current.Session.SessionID, (JourneyControl.PublicJourney)result.OutwardPublicJourneys[0]);
                #endregion
            }

            #region Assign the new itinerary
            // Replace outward/return itinerary with new itinerary
			if (outward)
			{
				outwardReplanned = true;
				outwardReplannedItinerary = newItinerary;
				// Create new outward Pricing options (of fixed length)
				outwardOptions = new PricingRetailOptionsState[newItinerary.Count];
			}
			else
			{
				returnReplanned = true;
				returnReplannedItinerary = newItinerary;
				// Create new return Pricing options (of fixed length)
				returnOptions = new PricingRetailOptionsState[newItinerary.Count];
            }
            #endregion

            // Set the pricing as incomplete so that fares are recalculated
			pricingDataComplete = false;
		}


		/// <summary>
		/// Calculate the Depart or Arrival time for the selected legs of the journey,
		/// based on a known fixed time.
		/// This ensures that Frequency or Continuous legs are correctly timed.
		/// </summary>
		/// <param name="details">The legs of the journey for the portion that needs to be timed</param>
		/// <param name="getDepartTime">If True then calculate the Depart Time; If False then calculate the Arrival Time</param>
		/// <returns></returns>
		private TDDateTime CalculateDepartOrArrivalTime(PublicJourneyDetail[] details, bool getDepartTime)
		{
			PublicJourneyFrequencyDetail frequencyDetail;
			PublicJourneyContinuousDetail continuousDetail;

			int minutes = 0;
			bool timedDetailHit = false;
			TDDateTime dateTime = new TDDateTime();

			int lastDetailIndex = details.Length - 1;

			int indexShift = (getDepartTime) ? +1 : -1;
			int firstIndex = (getDepartTime) ? 0 : lastDetailIndex;
			int outOfBoundsIndex = (getDepartTime) ? details.Length : -1;  // outOfBoundsIndex should be 1 beyond array bounds, in direction of search

			// Loop through the details in until you find a
			// TimedDetail or hit the start/end of the details array.
			for ( int index = firstIndex; !timedDetailHit && (index != outOfBoundsIndex); index += indexShift)
			{
				frequencyDetail = details[index] as PublicJourneyFrequencyDetail;
				if (frequencyDetail != null)
				{
					// Freqency-based leg, so allow maximum necessary time
					minutes = minutes + frequencyDetail.MaxDuration;  // Duration of Continuous Detail is in minutes
				}
				else
				{
					continuousDetail = details[index] as PublicJourneyContinuousDetail;
					if (continuousDetail != null)
					{
						// Duration-based leg, so allow necessary time
						minutes = minutes + (continuousDetail.Duration / 60);  // Duration of Continuous Detail is in seconds
					}
					else
					{
						// Time-based leg, so use absolute time of service as basis for calculating required time
						timedDetailHit = true;
						if (getDepartTime)
						{
							if (details[index].CheckInTime != null)
							{
								dateTime = details[index].CheckInTime;
							}
							else
							{
								dateTime = details[index].LegStart.DepartureDateTime;
							}
						}
						else
						{
							if (details[index].ExitTime != null)
							{
								dateTime = details[index].ExitTime;
							}
							else
							{
								dateTime = details[index].LegEnd.ArrivalDateTime;
							}
						}
					}
				}
			}

			if (!timedDetailHit)
			{
				// We didn't find a non-frequency detail so use absolute time from opposite end of array
				dateTime = (getDepartTime) ? details[lastDetailIndex].LegEnd.ArrivalDateTime : details[0].LegStart.DepartureDateTime;
			}

			if (getDepartTime)
			{
				return dateTime.AddMinutes(minutes * -1);
			}
			else
			{
				return dateTime.AddMinutes(minutes);
			}
		}


		/// <summary>
		/// Calculate the time to be used for the Replan Request.
		/// </summary>
		/// <param name="details">The legs of the journey</param>
		/// <param name="startIndex">The index of the first leg to be replanned</param>
		/// <param name="endIndex">The index of the last leg to be replanned</param>
		/// <param name="arriveBefore">True if the journey will be replanned as an ArriveBy</param>
		/// <returns></returns>
		private TDDateTime CalculateReplanRequestTime(PublicJourneyDetail[] details, int startIndex, int endIndex, bool arriveBefore)
		{
			PublicJourneyDetail[] detailsExtract;

			int extractLength;
			int lastDetailIndex = details.Length - 1;

			if (arriveBefore)
			{
				extractLength = lastDetailIndex - endIndex;
				detailsExtract = new PublicJourneyDetail[extractLength];

				Array.Copy(details, endIndex + 1, detailsExtract, 0, extractLength);

				return CalculateDepartOrArrivalTime(detailsExtract, true);
			}
			else
			{
				extractLength = startIndex;
				detailsExtract = new PublicJourneyDetail[extractLength];

				Array.Copy(details, 0, detailsExtract, 0, extractLength);

				return CalculateDepartOrArrivalTime(detailsExtract, false);
			}
		}


		/// <summary>
		/// Read only. Returns the journey that the user has selected to replan.
		/// </summary>
		public Journey JourneyForReplan
		{
			get { return journeyForReplan; }

		}

		/// <summary>
		/// Read-only property. The request that generated the original journey, prior to replanning.
		/// </summary>
		public ITDJourneyRequest OriginalRequest
		{
			get { return  ((ReplanPageState)TDSessionManager.Current.InputPageState).OriginalRequest; }
		}

		/// <summary>
		/// Creates journey summary lines for current Replan itinerary.
		/// </summary>
		/// <returns>Array of journey summary lines.</returns>
		public override JourneySummaryLine[] FullItinerarySummary()
		{

			JourneySummaryLine[] result;
			int resultArraySize = 0;
			int index = 0;

			// Add one element to the array size that will be used for the outward
			// and return journeys if they exist.
			if (outwardReplannedItinerary != null)
			{
				resultArraySize ++;
			}

			if (returnReplannedItinerary != null)
			{
				resultArraySize ++;
			}

			// Create results array of correct size.
			result = new JourneySummaryLine[resultArraySize];

			// Create a journey summary line for the outward journey.
			if (this.outwardReplannedItinerary != null)
			{
				result[index++] = GenerateJourneySummaryLine(outwardReplannedItinerary);
			}

			// Create a journey summary line for the return journey.
			if (this.returnReplannedItinerary != null)
			{
				result[index++] = GenerateJourneySummaryLine(returnReplannedItinerary);
			}

			return result;			
		}


		/// <summary>
		/// Generates journey summary line for a given target replan itinerary.
		/// </summary>
		/// <param name="targetItinerary">Replan itinerary (outward/return) to use.</param>
		/// <returns>Journey summary line.</returns>
		internal JourneySummaryLine GenerateJourneySummaryLine (ArrayList targetItinerary)
		{

			int itineraryLast = targetItinerary.Count - 1;
			int legsLast = ((Journey)targetItinerary[itineraryLast]).JourneyLegs.Length - 1;

			Journey[] journeys = (Journey[])targetItinerary.ToArray(typeof(Journey));

			return new JourneySummaryLine(
					-1,
					((Journey)targetItinerary[0]).JourneyLegs[0].LegStart.Location.Description,
					((Journey)targetItinerary[itineraryLast]).JourneyLegs[legsLast].LegEnd.Location.Description,
					TDJourneyType.Itinerary,
					GetModes(journeys),
					GetInterchangeCount(journeys),
					((Journey)targetItinerary[0]).JourneyLegs[0].LegStart.DepartureDateTime,
					((Journey)targetItinerary[itineraryLast]).JourneyLegs[legsLast].LegEnd.ArrivalDateTime,
					GetRoadDistance(journeys),
					"-1",
					GetOperatorNames(journeys)
					);
		}


		/// <summary>
		/// Get an array of mode types use for a given replan itinerary.
		/// </summary>
		/// <param name="itinerary"></param>
		/// <returns></returns>
		private ModeType[] GetModes(Journey[] journeys)
		{
			ArrayList modeList = new ArrayList();
			
			foreach (Journey targetJourney in journeys)
			{
				foreach (JourneyLeg leg in targetJourney.JourneyLegs)
				{
					modeList.Add(leg.Mode);
				}
			}

			return (ModeType[])modeList.ToArray(typeof(ModeType));
		}


		/// <summary>
		/// Get a count of the number of interchanges on specified
		/// itinerary.
		/// </summary>
		/// <param name="itinerary">Replan itinerary to use.</param>
		/// <returns>Interchange count.</returns>
		private int GetInterchangeCount(Journey[] journeys)
		{
			int count = 0;

			foreach (Journey journey in journeys)
			{
				// Changes are count of legs less one. e.g. For six legs you
				// would make five changes.
				count = count + journey.JourneyLegs.Length - 1;
			}

			return count;
		}


		/// <summary>
		/// Returns the total road distance for the specified replan itinerary.
		/// </summary>
		/// <param name="itinerary">Replan itinerary to use.</param>
		/// <returns>Total road distance.</returns>
		private int GetRoadDistance(Journey[] journeys)
		{
			int distance = 0;

			foreach (Journey journey in journeys)
			{
				if (journey.Type == TDJourneyType.RoadCongested)
				{
					distance = distance + ((RoadJourney)journey).TotalDistance;
				}
			}

			return distance;
		}


		/// <summary>
		/// Returns an array of operator names for a replan itinerary.
		/// </summary>
		/// <param name="itinerary">Replan itinerary to use.</param>
		/// <returns>Array of operator names.</returns>
		private string[] GetOperatorNames(Journey[] journeys)
		{
			ArrayList operatorNames = new ArrayList();
			PublicJourneyDetail[] publicDetails;
			
			foreach (Journey journey in journeys)
			{
				if (journey.Type == TDJourneyType.PublicOriginal)
				{
					publicDetails = (PublicJourneyDetail[])journey.JourneyLegs;
						
					if (publicDetails != null)
					{
						foreach (PublicJourneyDetail detail in publicDetails)
						{
							if (detail.Services != null)
							{
								foreach (ServiceDetails service in detail.Services)
								{
									if (!operatorNames.Contains(service.OperatorName))
										operatorNames.Add(service.OperatorName);
								}
							}
						}
					}
				}
			}

			return (string[])operatorNames.ToArray(typeof(string));
		}


		/// <summary>
		/// Returns the outward date time. This is obtained from the outward replan
		/// itinerary and the first leg of the first journey within that itinerary.
		/// </summary>
		/// <returns>Date/Time object.</returns>
		public override TDDateTime OutwardDepartDateTime()
		{
			return ((Journey)outwardReplannedItinerary[0]).JourneyLegs[0].LegStart.DepartureDateTime;
		}


		/// <summary>
		/// Returns departure date and time for return journey. See above.
		/// </summary>
		/// <returns>Date/Time Object</returns>
		public override TDDateTime ReturnDepartDateTime()
		{
			TDDateTime departDateTime = null;

			if (returnReplannedItinerary != null)
			{
				departDateTime = ((Journey)returnReplannedItinerary[0]).JourneyLegs[0].LegStart.DepartureDateTime;
			}

			return departDateTime;
		}


		/// <summary>
		/// Returns depart location of outward journey.
		/// </summary>
		/// <returns></returns>
		public override TDLocation OutwardDepartLocation()
		{
			return ((Journey)outwardReplannedItinerary[0]).JourneyLegs[0].LegStart.Location;	
		}

		/// <summary>
		/// Returns depart location of return journey.
		/// </summary>
		/// <returns></returns>
		public override TDLocation ReturnDepartLocation()
		{
			
			TDLocation departLocation = null;

			if (returnReplannedItinerary != null)
			{
				departLocation = ((Journey)returnReplannedItinerary[0]).JourneyLegs[0].LegStart.Location;
			}

			return departLocation;
				
		}


		/// <summary>
		/// Returns a specific journey viewstate.
		/// </summary>
		/// <param name="segmentIndex">For correct method signature. Not important.</param>
		/// <returns>Journey View State object</returns>
		public override TDJourneyViewState SpecificJourneyViewState(int segmentIndex)
		{
			return currentJourneyViewState;
		}


		/// <summary>
		/// Returns selected Outward journey for the specified segment
		/// </summary>
		public override Journey GetOutwardJourney(int segmentIndex)
		{
			return (Journey)outwardReplannedItinerary[segmentIndex];
		}


		/// <summary>
		/// Returns selected Return journey derived from a specified segment
		/// </summary>
		public override Journey GetReturnJourney(int segmentIndex)
		{
			//The segmentIndex is a reverse of the correct order for return 
			//ensure the correct journey is actually returned (by inverting) 
			return (Journey)returnReplannedItinerary[returnReplannedItinerary.Count -1 -segmentIndex];
		}

		/// <summary>
		/// Private method that recurses the itinerary array and then returns the
		/// pricing data for the whole Itinerary.
		/// </summary>
		/// <returns>Array (possibly zero length) of journey pricings</returns>
		public override PricingRetailOptionsState[] GetItineraryPricing()
		{
			// Set up journey arrays
			ArrayList pricings = new ArrayList();
			Journey[] outwardJourneys = OutwardJourneyItinerary;
			Journey[] returnJourneys = ReturnJourneyItinerary;

			// Recurse through all outward journeys to set up pricing options
			for (int i=0; i < outwardJourneys.Length; i++)
			{
				bool recalculate = false;
				
				// Determine if the outward pricing options need to be recalculated
				if (outwardOptions[i] == null)
				{
					recalculate = true;
				}
				else if (outwardOptions[i].JourneyItinerary != null)
				{
					if (outwardOptions[i].JourneyItinerary.OutwardJourney.Type != TDJourneyType.RoadCongested)
					{
						recalculate = !outwardOptions[i].JourneyItinerary.FaresInitialised;
					}
				}
				else
				{
					recalculate = true;
				}
			
				// If pricing Options need to be recalculated - create new outward Pricing Options and Get PricingOptionsState
				if (recalculate)
				{
					outwardOptions[i] = new PricingRetailOptionsState();
					outwardOptions[i].ItinerarySegment = i;
					outwardOptions[i].OverrideItineraryType = ItineraryType.Single;
					outwardOptions[i].GetPricingRetailOptionsState(FindFareBuyOption.OutwardSingle,
						outwardJourneys[i], null, true);
				}
				// Add the pricingRetailsStateOptions to the return array
				pricings.Add(outwardOptions[i]);
			}

			// Recurse through all return journeys to set up pricing options
			for (int i=0; i < returnJourneys.Length; i++)
			{
				bool recalculate = false;
				
				// Determine if the pricing options need to be recalculated
				if (returnOptions[i] == null)
				{
					recalculate = true;
				}
				else
					if (returnOptions[i].JourneyItinerary != null)
				{
                    if (returnOptions[i].JourneyItinerary.ReturnJourney.Type != TDJourneyType.RoadCongested)
                    {
					recalculate = !returnOptions[i].JourneyItinerary.FaresInitialised;
				}
				}
				else
				{
					recalculate = true;
				}
			
				// If pricing Options need to be recalculated - create new return Pricing Options and Get PricingOptionsState
				if (recalculate)
				{
					returnOptions[i] = new PricingRetailOptionsState();
					returnOptions[i].ItinerarySegment = i;
					returnOptions[i].OverrideItineraryType = ItineraryType.Single;
					returnOptions[i].GetPricingRetailOptionsState(FindFareBuyOption.ReturnSingle,
						null, returnJourneys[i], true);
				}
				// Add the pricingRetailsStateOptions to the return array
				pricings.Add(returnOptions[i]);
			}

			//Check to see if the arraylist has been populated with anything and return array as appropriate.
			if (pricings.Count == 0)
			{
				return new PricingRetailOptionsState[0];
			} 
			else
			{
				return (PricingRetailOptionsState[])pricings.ToArray(typeof(PricingRetailOptionsState));
			}
		}

		/// <summary>
		/// Private method that recurses the itinerary array and 
		/// copies in the corresponding pricing data.
		/// </summary>
		public override void SetItineraryPricing(PricingRetailOptionsState[] pricings, bool complete)
		{
			Journey[] outwardJourneys = OutwardJourneyItinerary;
			Journey[] returnJourneys = ReturnJourneyItinerary;

			// Ensure passed in pricingOptions array is of same length as the current outward + current return journeys
			if (pricings.Length == (outwardJourneys.Length + returnJourneys.Length))
			{
				// Extract the outward journey pricing options from the passed in pricing options
				for (int i=0; i < outwardJourneys.Length; i++)
				{
					outwardOptions[i] = pricings[i];
				}

				// Extract the return journey pricing options from the passed in pricing options
				for (int i=0; i < returnJourneys.Length; i++)
				{
					returnOptions[i] = pricings[outwardJourneys.Length + i];
				}
			}

			// Determine if pricingData is complete
			pricingDataComplete = complete;
		}
		#endregion

        #region Private Methods

        /// <summary>
		/// Gets Toids for supplied location object.
		/// </summary>
		/// <param name="location">TDLocation object for which you want to find Toids.</param>
		/// <returns>String array containing toids.</returns>
		private string[] GetLocationToids(TDLocation location)
		{

			IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

			QuerySchema schema = gisQuery.FindNearestITNs(location.GridReference.Easting, location.GridReference.Northing);

			StringBuilder logMsg = new StringBuilder();

			if (TDTraceSwitch.TraceVerbose)
			{
				logMsg.Append("ReplanItineraryManager.GetLocationToids : description = " + location.Description + " locality = " + location.Locality + " ");
				logMsg.Append(schema.ITN.Rows.Count + " TOIDs: ");
			}

			string[] toids = new string[schema.ITN.Rows.Count];

			for (int i=0; i < schema.ITN.Rows.Count; i++)
			{
				QuerySchema.ITNRow row = (QuerySchema.ITNRow) schema.ITN.Rows[i];
				toids[i] = row.toid;
				
				if (TDTraceSwitch.TraceVerbose)
				{
					logMsg.Append(row.toid + " ");
				}
			}

			if (TDTraceSwitch.TraceVerbose)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, logMsg.ToString()));
			}

			return toids;
        }

        /// <summary>
        /// Populates the routing guide sections for the current journey using the original journey.
        /// Assumes the current journey is built using legs from the original journey and therefore the 
        /// routing guides sections can be copied in to the new.
        /// An offset value (which is the leg index at which current journey was created from the original)
        /// is needed to map the original routing guide section legs in to the current journey routing guide 
        /// section legs.
        /// </summary>
        /// <param name="originalPJ"></param>
        /// <param name="currentPJ"></param>
        /// <param name="legsOffset"></param>
        /// <returns></returns>
        private JourneyControl.PublicJourney UpdateRoutingGuideSections(JourneyControl.PublicJourney originalPJ, JourneyControl.PublicJourney currentPJ, int legsOffset)
        {
            // Using the orignal journey, populate the RoutingGuideSections of the current journey, 
            // where the current journey contains all the legs specified by the orignal RGS

            ArrayList newRGSarray = new ArrayList();
            bool allRGSvalid = true;

            foreach (RoutingGuideSection rgs in originalPJ.RoutingGuideSections)
            {
                bool currentJourneyContainsAllLegs = true;
                

                foreach (int legIndex in rgs.Legs)
                {
                    // Does the current journey contain all the legs specified by the RGS
                    if ((legIndex >= legsOffset) && 
                        (legIndex < (currentPJ.Details.Length + legsOffset))
                       )
                    {
                        // This leg is in the currentJourney. Do nothing
                    }
                    else
                    {
                        currentJourneyContainsAllLegs = false;

                        // Global setting which sets the overall journey routing guide compliant staus
                        allRGSvalid = false;
                        break;
                    }
                }

                if (currentJourneyContainsAllLegs)
                {
                    // Ok to retain this RGS
                    newRGSarray.Add(rgs);

                    // If any RGS in this journey are not compliant, the new journey can not be compliant
                    if (!rgs.Compliant)
                    {
                        allRGSvalid = false;
                    }
                }
            }

            // Final check, if we've failed to create any RGS's for this journey, then check
            // if this currentJourney only has one leg of mode Rail. If true then we can say that it is 
            // routing guide valid and add the leg
            if ((newRGSarray.Count == 0) && (currentPJ.HasOnlyOneRailLeg()))
            {
                allRGSvalid = true;

                // Create a valid RGS for this leg
                for (int i = 0; i < currentPJ.Details.Length; i++)
                {
                    if ((currentPJ.Details[i].Mode == ModeType.Rail)
                        ||
                        (currentPJ.Details[i].Mode == ModeType.RailReplacementBus))
                    {
                        // This is the rail leg, create the RGS
                        newRGSarray.Add(new RoutingGuideSection(0, new int[1] { i }, true));

                        // Update this leg's routing guide status
                        currentPJ.Details[i].RoutingGuideCompliant = true;
                        currentPJ.Details[i].RoutingGuideSectionIndex = 0;
                        break;
                    }
                }
            }

            // Assign the RGS to the journey
            currentPJ.RoutingGuideSections = (RoutingGuideSection[])newRGSarray.ToArray(typeof(RoutingGuideSection));
            currentPJ.RoutingGuideCompliantJourney = allRGSvalid;
            
            return currentPJ;
        }

        

        #endregion

        #region Public Properties

        /// <summary>
		/// Read only. Returns length of itinerary. Max of outward or return.
		/// </summary>
		public override int Length
		{
			get 
			{
				int outwardCount = (outwardReplannedItinerary != null ? outwardReplannedItinerary.Count : 0);
				int returnCount = (returnReplannedItinerary != null ? returnReplannedItinerary.Count : 0);

				return Math.Max(outwardCount, returnCount);
			}
		}


		/// <summary>
		/// Read Only. Returns length of outward itinerary.
		/// </summary>
		public override int OutwardLength
		{
			get { return outwardReplannedItinerary.Count; }
		}

		/// <summary>
		/// Read only. Returns length of return itinerary.
		/// </summary>
		public override int ReturnLength
		{
			get { return (returnReplannedItinerary != null ? returnReplannedItinerary.Count : 0); }
		}

		/// <summary>
		/// Get Property to determine if current outward journey is public
		/// </summary>
		public override bool OutwardIsPublic
		{
			get 
			{
				// Note - if there is more than one segment, at least one of them must be public
				if (OutwardLength > 1)
				{
					return true;
				}
				else
				{
					return outwardReplannedItinerary[0] is JourneyControl.PublicJourney;
				}
			}
		}

		/// <summary>
		/// Get Property to determine if current return journey is public
		/// </summary>
		public override bool ReturnIsPublic
		{
			get 
			{
				// Note - if there is more than one segment, at least one of them must be public
				if (ReturnLength > 1)
				{
					return true;
				}
				else if (ReturnLength == 0)
				{
					return false;
				}
				else
				{
					return returnReplannedItinerary[0] is JourneyControl.PublicJourney;
				}
			}
		}

		/// <summary>
		/// Read-only property
		/// Overridden as always dealing with Full Itinerary 
		/// in Replan, regardless of segment selected 
		/// </summary>
		public override bool FullItinerarySelected
		{
			get
			{
				return true;
			}
		}

		
		/// <summary>
		/// Read Only. Returns array of journeys that make up the outward itinerary.
		/// If none exist then an empty array is returned.
		/// </summary>
		public override Journey[] OutwardJourneyItinerary
		{
			get
			{
				if (outwardReplannedItinerary != null)
				{
					return (Journey[])outwardReplannedItinerary.ToArray(typeof(Journey));

				}
				else
				{
					// If the itinerary is empty then return an empty
					// array of journeys
					return new Journey[0];
				}
			}
		}


		/// <summary>
		/// Read Only. Returns array of journeys that make up the return itinerary.
		/// If none exist then an empty array is returned.
		/// </summary>
		public override Journey[] ReturnJourneyItinerary
		{
			get
			{
				if (returnReplannedItinerary != null)
				{
					return (Journey[])returnReplannedItinerary.ToArray(typeof(Journey));

				}
				else
				{
					// If the itinerary is empty then return an empty
					// array of journeys
					return new Journey[0];
				}
			}
		}


		/// <summary>
		/// Read only property - get the ItineraryManagerMode
		/// </summary>
		public override ItineraryManagerMode ItineraryMode
		{
			get {return ItineraryManagerMode.Replan;}
		}


		/// <summary>
		/// Read only property - gets the flag to indicate whether the outward journey has been replanned
		/// </summary>
		public bool OutwardReplanned
		{
			get {return outwardReplanned;}
		}


		/// <summary>
		/// Read only property - gets the flag to indicate whether the return journey has been replanned
		/// </summary>
		public bool ReturnReplanned
		{
			get {return returnReplanned;}
		}

		/// <summary>
		/// Read only property. Boolean HasOverlappingJourneys.
		/// Specifies whether the itinerary has journeys date/times that overlap.
		/// </summary>
		public bool HasOverlappingJourneys
		{
			get
			{
				//IR4056: Property created.
				//Implementation  notes:
				//1.We don't need to check the last journey as it won't connect to another.
				//2.To test for overlapping journeys; we test that the journey's End leg
				//ArrivalDateTime is greater than the Next journey's starting DepartureDateTime.
				//3.Property returns true if we find Overlapping Journeys for outward or return Itineraries.

				for( int i = 0; i < outwardReplannedItinerary.Count -1;i++)
				{
					if (((Journey)outwardReplannedItinerary[i]).JourneyLegs[((Journey)outwardReplannedItinerary[i]).JourneyLegs.Length -1].LegEnd.ArrivalDateTime
						> ((Journey)outwardReplannedItinerary[i+1]).JourneyLegs[0].LegStart.DepartureDateTime)
					{
						return true;
					}
				}

				if	(returnReplannedItinerary != null && returnReplannedItinerary.Count > 0)
				{
					for( int i = 0; i < returnReplannedItinerary.Count - 1; i++)
					{
						if (((Journey)returnReplannedItinerary[i]).JourneyLegs[((Journey)returnReplannedItinerary[i]).JourneyLegs.Length -1].LegEnd.ArrivalDateTime
							> ((Journey)returnReplannedItinerary[i+1]).JourneyLegs[0].LegStart.DepartureDateTime)
						{
							return true;
						}
					}
				}

				return false;
			}
		}
		#endregion

		#region Obsolete Public Methods

#pragma warning disable 809
        [Obsolete("Method obsolete in this implementation")]
		public override void AddExtensionToItinerary()
		{
			throw new NotImplementedException("Method obsolete in implementation");
        }
#pragma warning restore 809

        #endregion Obsolete Public Methods
    }
}
