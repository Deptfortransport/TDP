// ***********************************************
// NAME 		: AdjustRoute.cs
// AUTHOR 		: Peter Norell
// DATE CREATED : 20/08/2003
// DESCRIPTION 	: Business class to adjust a route
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/AdjustRoute.cs-arc  $
//
//   Rev 1.3   Feb 09 2009 15:16:28   mmodi
//Updated code to apply Routing Guide Sections logic to the adjusted Public Journey
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.2   Mar 10 2008 15:17:38   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:23:36   mturner
//Initial revision.
//
//   Rev 1.20   Apr 29 2006 13:43:40   mdambrine
//When we are in adjust we pass for trunkplanning we need to pass a sequence in order to avoid an interval in as search type
//Resolution for 3657: DN068: Extend, Replan & Adjust: Adjusting flights
//Resolution for 3726: DN068 Adjust: No adjusted journey option offered from Find A Flight
//
//   Rev 1.19   Apr 25 2006 17:01:36   tmollart
//Removed via location code in set route contstraints. See IR for details.
//Resolution for 3760: DN068: Adjust Journey: Intermediates issue
//
//   Rev 1.18   Apr 20 2006 17:19:36   RPhilpott
//Ensure that correct RequestPlaceType is set for origin and destination locations.
//Resolution for 3813: DN068 Adjust: unable to show earlier/later services from Edinburgh address to Glasgow attraction
//Resolution for 3929: DN068 Adjust: 'Unable to find journey options' on Adjusted Journey Details when locations are postcodes
//
//   Rev 1.17   Apr 19 2006 16:03:06   mdambrine
//add some extra code to populate the locality if it is missing and also change the type to coordinate when the searchtype is AddressPostCode.
//Resolution for 3929: DN068 Adjust: 'Unable to find journey options' on Adjusted Journey Details when locations are postcodes
//
//   Rev 1.16   Mar 30 2006 09:59:12   pcross
//Updated adjust request location to use locations in results, not original request locations (which could have baggage eg from city to city searches)
//Resolution for 3727: DN068 Adjust: Adjusted journey option ends at the wong location
//
//   Rev 1.15   Mar 22 2006 11:09:08   pcross
//Updates to logic which calculates new arrive by / leave after time for 'spend at least x mins at' type adjusts.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.14   Mar 21 2006 11:09:40   pcross
//Corrections to adjusting non-timed legs
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.13   Mar 14 2006 08:41:36   build
//Automatically merged from branch for stream3353
//
//   Rev 1.12.1.3   Mar 13 2006 16:32:46   tmollart
//Updated after code review.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.12.1.2   Mar 10 2006 14:33:46   pcross
//Handles non-timed legs (not just walk mode as before)
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.12.1.1   Mar 02 2006 14:51:18   pcross
//Handling walk legs
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.12.1.0   Feb 16 2006 09:59:12   pcross
//Changes to allow a user-selected minimum time value to be passed in to adjust routine.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.12   Aug 24 2005 16:04:34   RPhilpott
//Changes to allow OSGR of first or last location to be replaced by OSGR of request origin/destination in rare case where firs tor last naptan in journey has no supplied OSGR and is not in Stops database.
//Resolution for 2662: DN062:  Map with non-consecutive nodes with no co-ordinates functioning incorrectly
//
//   Rev 1.11   Mar 01 2005 16:55:30   rscott
//Del 7 - Cost Based Search Incremental Design Changes
//
//   Rev 1.10   Feb 23 2005 16:40:34   rscott
//DEL7 Update - TDDateTime OutwardDateTime and ReturnDateTime changed to TDDateTime[] arrays.
//
//   Rev 1.9   Jan 19 2005 14:45:24   RScott
//DEL 7 - PublicViaLocation removed and PublicViaLocations[ ], PublicSoftViaLocations[ ], PublicNotViaLocations[] added.
//
//   Rev 1.8   Nov 24 2003 10:52:58   kcheung
//Removed commented out code.
//
//   Rev 1.7   Nov 24 2003 10:50:30   kcheung
//Fixed so that car is not included as a mode in the amended request.
//
//   Rev 1.6   Nov 13 2003 17:12:02   kcheung
//Fixed return bug 
//
//   Rev 1.5   Sep 25 2003 11:46:52   PNorell
//Fixed bug with setting correct type for the adjusted journey
//
//   Rev 1.4   Sep 09 2003 15:05:00   PNorell
//Updated and removed TODO statement to do with journey times.
//
//   Rev 1.3   Sep 01 2003 16:28:38   jcotton
//Updated: RouteNum
//
//   Rev 1.2   Aug 29 2003 10:42:52   kcheung
//Updated made after TDTimeSearchType was replaced by a boolean.
//
//   Rev 1.1   Aug 28 2003 17:58:40   kcheung
//Updated because property to return Journey array no longer exists in TDJourneyResult
//
//   Rev 1.0   Aug 27 2003 10:50:08   PNorell
//Initial Revision
using System;
using System.Collections;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.AirDataProvider;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Business class to adjust a route
	/// </summary>
	public class AdjustRoute : ITDAdjustRoute
	{

		#region Constants

		/// <summary>
		/// Matches the timings dropdown value for generic Arrive Earlier search
		/// </summary>
		private const string ArriveEarlier = "ArriveEarlier";

		/// <summary>
		/// Matches the timings dropdown value for generic Leave Later search
		/// </summary>
		private const string LeaveLater = "LeaveLater";
		
		
		/// <summary>
		/// "Naptan" values sometimes returned by TLs/CJP for origin and destination
		/// when these are actually coordinate locations ... 
		/// </summary>
		private const string OriginNaptan		= "Origin";
		private const string DestinationNaptan	= "Destination";

		#endregion

		#region ITDAdjustRoute Interface methods
		/// <summary>
		/// Builds a journey request that should be sent to the CJP and updates the Adjust state accordingly.
		/// </summary>
		/// <param name="adjustState">The current adjusted state</param>
		/// <returns>the new request being used</returns>
		public ITDJourneyRequest BuildJourneyRequest( TDCurrentAdjustState adjustState )
		{
			// collect nessecary data from the state objects
			// split journey correctly
			PublicJourneyDetail[] frontPart = null;
			PublicJourneyDetail[] rearPart = null;
			
			SplitPublicRoute( adjustState.AmendedJourney, adjustState.SelectedRouteNode, out frontPart, out rearPart );
			// Which selection state
			bool early = adjustState.SelectedRouteNodeSearchType == true;
			// Which part is to keep and which is to replan
			PublicJourneyDetail[] keep = early ? rearPart : frontPart ;
			PublicJourneyDetail[] replan = early ? frontPart : rearPart;

			// chunk in part to keep into the amend state for later splicing
			adjustState.RemainingRouteSegment = keep;
			
			// make new request based upon original and the parts that need to be replanned
			ITDJourneyRequest request = SetRouteConstraints(early, adjustState.CurrentAmendmentType, adjustState.OriginalJourneyRequest, replan, keep, adjustState.MinimumTime);			

			// Update sequence number with higher number than technically should be returned by wrapper
			// This to ensure it will be able to resolve simultaneous amendments for the same reference number.
			adjustState.JourneyReferenceSequence += 10;
			
			return request;
		}

		/// <summary>
		/// Splices the recieved journey with the original into one adjusted journey.
		/// </summary>
		/// <param name="result">The recieved results</param>
		/// <param name="adjustState">The state of adjustment</param>
		/// <returns>The spliced public journey</returns>
		public PublicJourney BuildAmendedJourney( PublicJourney originalPJ, ITDJourneyResult result, TDCurrentAdjustState adjustState) 
		{						
			// Join the new segment with the result (appended or prepended depending on the type of search)
            JourneySummaryLine[] summary = result.OutwardJourneySummary( adjustState.SelectedRouteNodeSearchType );
			int i = summary[0].JourneyIndex;

            PublicJourneyDetail[] original = adjustState.RemainingRouteSegment;
            PublicJourneyDetail[] amended = result.OutwardPublicJourney(i).Details;
            bool amendFirst = adjustState.SelectedRouteNodeSearchType == true;

			PublicJourneyDetail[] current = JoinPublicRoute(original, amended, amendFirst);

			// Construct new journey object
			PublicJourney currentPJ = new PublicJourney( adjustState.AmendedJourney.JourneyIndex, current, TDJourneyType.PublicAmended, 0 );

            // Update the routeing guide sections for this journey
            currentPJ = UpdateRoutingGuideSections(originalPJ, result.OutwardPublicJourney(i), currentPJ, original, amended, amendFirst);

            return currentPJ;
		}

		#endregion

		#region Private/protected methods
		/// <summary>
		/// Splits a journey into two parts (front and rear) at a given splitpoint
		/// </summary>
		/// <param name="journeyToSplit">The journey that should be split</param>
		/// <param name="nodeSplitPoint">The split point </param>
		/// <param name="frontPart">The front part excludes the leg following the split point</param>
		/// <param name="rearPart">The rear part</param>
		protected void SplitPublicRoute( PublicJourney journeyToSplit, uint nodeSplitPoint, out PublicJourneyDetail[] frontPart, out PublicJourneyDetail[] rearPart)
		{
			// Locate split point, calculate size for both of the out parameters
			int totalLength = journeyToSplit.Details.Length;

			// Front part, excluding the leg following the split point
			frontPart = new PublicJourneyDetail[ nodeSplitPoint ];
			// Rear part, includes the leg following the split point
			rearPart = new PublicJourneyDetail[ totalLength - nodeSplitPoint ];

			// Copy appropriate data into each array.
			System.Array.Copy( journeyToSplit.Details, 0, frontPart, 0, (int)nodeSplitPoint );
			System.Array.Copy( journeyToSplit.Details, (int)nodeSplitPoint, rearPart, 0, rearPart.Length );
		}

		/// <summary>
		/// Copies the route constants, keeping the nessecary ones from the original request and changing the rest according
		/// to the amending route
		/// </summary>
		/// <param name="early">Indicates the adjust is to arrive earlier</param>
		/// <param name="tda">Outward or return journey</param>
		/// <param name="originalRequest">The Original request</param>
		/// <param name="routeToAmend">The amending route</param>
		/// <param name="routeToKeep">The route to keep</param>
		/// <param name="MinimumTime">The amount of time to adjust by. If 0, then adjust by default from properties table</param>
		/// <returns>The new journey request parameters</returns>
		/// <remarks>Where I have written walk in comments in this proc I actually mean any non-timed journey
		/// (eg frequency based buses)</remarks>
		protected ITDJourneyRequest SetRouteConstraints(bool early, TDAmendmentType tda, ITDJourneyRequest originalRequest, PublicJourneyDetail[] routeToAmend, PublicJourneyDetail[] routeToKeep, int minimumTime)
		{
			ITDJourneyRequest amendRequest = new TDJourneyRequest();
			
			// Replanning will never require a return journey.
			amendRequest.IsReturnRequired = false;
			// Return arrive before and return date time is left unchanged since no return 
			// journey is required.
			// amendRequest.ReturnArriveBefore;
			// amendRequest.ReturnDateTime;

			// Start of route is either the original request starting location(s) or the first location node of the 
			// route to amend. For journeys to arrive earlier to the node, the Original request start is used, 
			// otherwise the first node location is used as start and vice versa for destination
			bool outward = tda == TDAmendmentType.OutwardJourney;
			bool locFrom = outward ? originalRequest.AlternateLocationsFrom : !originalRequest.AlternateLocationsFrom;
			

			// First always make sure we copy alternative locations if they are needed
			if ( originalRequest.AlternateLocations != null && early == locFrom) 
			{
				amendRequest.AlternateLocations = originalRequest.AlternateLocations;
				amendRequest.AlternateLocationsFrom = originalRequest.AlternateLocationsFrom;
			}
			
			// Assign the start route
			if( early )		// arrive before
			{
				amendRequest.OriginLocation = routeToAmend[0].LegStart.Location;
				amendRequest.DestinationLocation = routeToAmend[ routeToAmend.Length - 1 ].LegEnd.Location;
				amendRequest.OutwardArriveBefore = true;

				int additionalTime = 0;
				if (minimumTime > 0)	// adjust time specified
				{
					additionalTime = minimumTime;
				}
				else
				{
					// Reduce x minutes according to the local setting in
					// TransportDirect.JourneyControl.AdjustJourney.TimeAdjustment
					additionalTime = GetAdditionalTimePropertyValue();
				}
				
				TimeSpan ts = new TimeSpan(0, additionalTime,0);
				amendRequest.OutwardDateTime = new TDDateTime[1];

				
				if (minimumTime == 0)	// 'find previous service arriving at' scenario
				{
					// Adjusted arrive by time = latest previous PT leg arrival time + walk duration(s) to that point - adjustment
					TDDateTime PTLegArrivalTime = null;
					int legDuration;
					int sumNonTimedLegDuration = 0;
					
					for (int i = routeToAmend.Length - 1; i >=0; i--)
					{
						if (!(routeToAmend[i] is PublicJourneyTimedDetail))
						{
							// Note that duration is in seconds unless it is a frequency leg
							PublicJourneyFrequencyDetail pjDetail = (routeToAmend[i] as PublicJourneyFrequencyDetail);
							if (pjDetail != null)
								legDuration = pjDetail.MaxDuration * 60;
							else
								legDuration = routeToAmend[i].Duration;

							// Add the leg duration to the running total
							sumNonTimedLegDuration += legDuration;
						}
						else
						{
							// We have found a prior PT leg - get the arrival time
							PTLegArrivalTime = routeToAmend[i].LegEnd.ArrivalDateTime;
							break;
						}
					}
					
					// Found a prior PT leg - get the new outward time from the PT leg arrival plus walk duration minus a minute
					TimeSpan totalNonTimedLegDuration = new TimeSpan(0,0,sumNonTimedLegDuration);
					if (PTLegArrivalTime != null)
					{
						amendRequest.OutwardDateTime[0] = PTLegArrivalTime.Add(totalNonTimedLegDuration - ts);
					}
					else	// No PT leg found. New outward is origin plus walk duration(s) minus adjust time
					{
						amendRequest.OutwardDateTime[0] = ((PublicJourneyDetail)routeToAmend[0]).StartTime.Add(totalNonTimedLegDuration - ts);
					}
				}
				else	// 'allow at least x minutes at' scenario
				{
					// Adjusted arrive by time = earliest following PT leg leave time - walk duration(s) to that point - adjustment
					TDDateTime PTLegLeaveTime = null;
					int legDuration;
					int sumNonTimedLegDuration = 0;

					for (int i = 0; i < routeToKeep.Length; i++)
					{
						if (!(routeToKeep[i] is PublicJourneyTimedDetail))
						{
							// Note that duration is in seconds unless it is a frequency leg
							PublicJourneyFrequencyDetail pjDetail = (routeToKeep[i] as PublicJourneyFrequencyDetail);
							if (pjDetail != null)
								legDuration = pjDetail.MaxDuration * 60;
							else
								legDuration = routeToKeep[i].Duration;

							// Add the leg duration to the running total
							sumNonTimedLegDuration += legDuration;
						}
						else
						{
							// We have found a following PT leg - get the leave time
							PTLegLeaveTime = routeToKeep[i].LegStart.DepartureDateTime;
							break;
						}
					}
					
					// Found a following PT leg - get the new outward time from the PT leg leave time minus walk duration minus a minute
					TimeSpan totalNonTimedLegDuration = new TimeSpan(0,0,sumNonTimedLegDuration);
					if (PTLegLeaveTime != null)
					{
						amendRequest.OutwardDateTime[0] = PTLegLeaveTime.Subtract(totalNonTimedLegDuration + ts);
					}
					else	// No PT leg found. New outward is destination minus walk duration(s) minus adjust time
					{
						amendRequest.OutwardDateTime[0] = ((PublicJourneyDetail)routeToKeep[routeToKeep.Length-1]).EndTime.Subtract(totalNonTimedLegDuration + ts);
					}
				}

			}
			else		// leave later
			{
				amendRequest.OriginLocation = routeToAmend[0].LegStart.Location;
				amendRequest.DestinationLocation = routeToAmend[routeToAmend.Length - 1].LegEnd.Location;
				amendRequest.OutwardArriveBefore = false;

				int additionalTime = 0;
				if (minimumTime > 0)	// adjust time specified
				{
					additionalTime = minimumTime;
				}
				else
				{
					// Add x minutes according to the local setting in
					// TransportDirect.JourneyControl.AdjustJourney.TimeAdjustment
					additionalTime = GetAdditionalTimePropertyValue();
				}

				TimeSpan ts = new TimeSpan(0,additionalTime,0);
				amendRequest.OutwardDateTime = new TDDateTime[1];


				if (minimumTime == 0)	// 'find next service leaving from' scenario
				{
					// Adjusted leave after time = earliest next PT leg departure time - walk duration(s) + adjustment
					TDDateTime PTLegDepartTime = null;
					int legDuration;
					int sumNonTimedLegDuration = 0;

					for (int i = 0; i < routeToAmend.Length; i++)
					{
						if (!(routeToAmend[i] is PublicJourneyTimedDetail))
						{
							// Note that duration is in seconds unless it is a frequency leg
							PublicJourneyFrequencyDetail pjDetail = (routeToAmend[i] as PublicJourneyFrequencyDetail);
							if (pjDetail != null)
								legDuration = pjDetail.MaxDuration * 60;
							else
								legDuration = routeToAmend[i].Duration;

							// Add the leg duration to the running total
							sumNonTimedLegDuration += legDuration;
						}
						else
						{
							// We have found a subsequent PT leg - get the leave time
							PTLegDepartTime = routeToAmend[i].LegStart.DepartureDateTime;
							break;
						}
					}
					
					// Found a subsequent PT leg - get the new outward time from the PT leg departure minus walk duration plus adjust time
					TimeSpan totalNonTimedLegDuration = new TimeSpan(0,0,sumNonTimedLegDuration);
					if (PTLegDepartTime != null)
					{
						amendRequest.OutwardDateTime[0] = PTLegDepartTime.Subtract(totalNonTimedLegDuration - ts);
					}
					else	// No PT leg found. New outward is destination arrival minus walk duration plus a minute
					{
						amendRequest.OutwardDateTime[0] = ((PublicJourneyDetail)routeToAmend[routeToAmend.Length - 1]).EndTime.Subtract(totalNonTimedLegDuration - ts);
					}
				}
				else	// 'allow at least x minutes at' scenario
				{
					// Adjusted leave after time = latest previous PT leg arrival time + walk duration(s) to that point + adjustment
					TDDateTime PTLegArriveTime = null;
					int legDuration;
					int sumNonTimedLegDuration = 0;

					for (int i = routeToKeep.Length - 1; i >=0; i--)
					{
						if (!(routeToKeep[i] is PublicJourneyTimedDetail))
						{
							// Note that duration is in seconds unless it is a frequency leg
							PublicJourneyFrequencyDetail pjDetail = (routeToKeep[i] as PublicJourneyFrequencyDetail);
							if (pjDetail != null)
								legDuration = pjDetail.MaxDuration * 60;
							else
								legDuration = routeToKeep[i].Duration;

							// Add the leg duration to the running total
							sumNonTimedLegDuration += legDuration;
						}
						else
						{
							// We have found a previous PT leg - get the arrival time
							PTLegArriveTime = routeToKeep[i].LegEnd.ArrivalDateTime;
							break;
						}
					}
					
					// Found a previous PT leg - get the new outward time from the PT leg arrival time plus walk duration plus a minute
					TimeSpan totalNonTimedLegDuration = new TimeSpan(0,0,sumNonTimedLegDuration);
					if (PTLegArriveTime != null)
					{
						amendRequest.OutwardDateTime[0] = PTLegArriveTime.Add(totalNonTimedLegDuration + ts);
					}
					else	// No PT leg found. New outward is origin plus walk duration(s) plus adjust time
					{
						amendRequest.OutwardDateTime[0] = ((PublicJourneyDetail)routeToKeep[0]).StartTime.Add(totalNonTimedLegDuration + ts);
					}

				}
				
			}

			
			//if the origin or destination locality is not populated, do that appopriately
			IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

			if (amendRequest.OriginLocation.Locality.Length == 0)
			{
				//set the locality					
				amendRequest.OriginLocation.Locality = gisQuery.FindNearestLocality(amendRequest.OriginLocation.GridReference.Easting, 
																						amendRequest.OriginLocation.GridReference.Northing);													
			}			

			if (amendRequest.DestinationLocation.Locality.Length == 0)
			{
				//set the locality					
				amendRequest.DestinationLocation.Locality = gisQuery.FindNearestLocality(amendRequest.DestinationLocation.GridReference.Easting, 
																						 amendRequest.DestinationLocation.GridReference.Northing);
			}

			if	(amendRequest.OriginLocation.NaPTANs.Length > 0 
				&& amendRequest.OriginLocation.NaPTANs[0].Naptan.Length > 0
				&& amendRequest.OriginLocation.NaPTANs[0].Naptan != OriginNaptan 
				&& amendRequest.OriginLocation.NaPTANs[0].Naptan != DestinationNaptan)
			{
				amendRequest.OriginLocation.RequestPlaceType = RequestPlaceType.NaPTAN;
			}
			else
			{
				amendRequest.OriginLocation.RequestPlaceType = RequestPlaceType.Coordinate;
			}

			if	(amendRequest.DestinationLocation.NaPTANs.Length > 0 
				&& amendRequest.DestinationLocation.NaPTANs[0].Naptan.Length > 0
				&& amendRequest.DestinationLocation.NaPTANs[0].Naptan != "Origin" 
				&& amendRequest.DestinationLocation.NaPTANs[0].Naptan != "Destination")
			{
				amendRequest.DestinationLocation.RequestPlaceType = RequestPlaceType.NaPTAN;
			}
			else
			{
				amendRequest.DestinationLocation.RequestPlaceType = RequestPlaceType.Coordinate;
			}


			ArrayList requestModes = new ArrayList();

			bool IsAirMode = false;

			foreach(ModeType mode in originalRequest.Modes)
			{
				// Add the mode only if it is not car
				if(mode != ModeType.Car)
					requestModes.Add(mode);
				if(mode == ModeType.Air)
					IsAirMode = true;
			}

			amendRequest.Modes = new ModeType[requestModes.Count];
			amendRequest.Modes = (ModeType[])requestModes.ToArray(typeof(ModeType));

			// Interchange and walking speeds
			amendRequest.InterchangeSpeed = originalRequest.InterchangeSpeed;
			amendRequest.WalkingSpeed = originalRequest.WalkingSpeed;
			amendRequest.MaxWalkingTime = originalRequest.MaxWalkingTime;

			// Road information not nessecary since no replaning of road journeys
			// Kept here for later park & ride requirements
			amendRequest.DrivingSpeed = originalRequest.DrivingSpeed;
			amendRequest.AvoidMotorways = originalRequest.AvoidMotorways;
			amendRequest.AvoidRoads = originalRequest.AvoidRoads;
			
			// Algorithms
			amendRequest.PrivateAlgorithm = originalRequest.PrivateAlgorithm;
			amendRequest.PublicAlgorithm = originalRequest.PublicAlgorithm;

			// copy over the isTrunkRequest from the original request
			amendRequest.IsTrunkRequest = originalRequest.IsTrunkRequest;
			amendRequest.ExtraCheckinTime = originalRequest.ExtraCheckinTime;
			if (amendRequest.IsTrunkRequest) amendRequest.Sequence = 1;

			//If we are in airmode then we need to check if the airport that we are adjusting on has other terminals
			//that could be used in our request. e.g. Manchester has 3 terminals, if we only pass terminal3 it will
			//only find the flights departing from this terminal.
			if(IsAirMode)
			{
				IAirDataProvider airDataProvider = (IAirDataProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];
				
				for(int i=0; i < amendRequest.OriginLocation.NaPTANs.Length;i++)
				{
					if (amendRequest.OriginLocation.NaPTANs[i].StationType == StationType.Airport
						|| amendRequest.OriginLocation.NaPTANs[i].StationType == StationType.AirportNoGroup)
					{
						amendRequest.OriginLocation.NaPTANs[i].Naptan = airDataProvider.GetAirportFromNaptan(amendRequest.OriginLocation.NaPTANs[i].Naptan).GlobalNaptan;												
					}
				}
				
				for(int i=0; i < amendRequest.DestinationLocation.NaPTANs.Length;i++)
				{

					if (amendRequest.DestinationLocation.NaPTANs[i].StationType == StationType.Airport
						|| amendRequest.DestinationLocation.NaPTANs[i].StationType == StationType.AirportNoGroup)
					{
						amendRequest.DestinationLocation.NaPTANs[i].Naptan = airDataProvider.GetAirportFromNaptan(amendRequest.DestinationLocation.NaPTANs[i].Naptan).GlobalNaptan;			
					}
				}
			}

            // Set the Routing guide flags from the original request
            amendRequest.RoutingGuideInfluenced = originalRequest.RoutingGuideInfluenced;
            amendRequest.RoutingGuideCompliantJourneysOnly = originalRequest.RoutingGuideCompliantJourneysOnly;

			return amendRequest;
		}

		/// <summary>
		/// Gets the value for the additional time to adjust by from the property table
		/// </summary>
		/// <returns></returns>
		private int GetAdditionalTimePropertyValue()
		{
			int additionalTime = Int32.Parse(Properties.Current["TransportDirect.JourneyControl.AdjustJourney.TimeAdjustment"]);

			return additionalTime;
		}

		/// <summary>
		/// Merges two journey details into one.
		/// </summary>
		/// <param name="frontPart">The original part of the journey details that is unchanged</param>
		/// <param name="rearPart">The amended journey details</param>
		/// <param name="amendFirst">If the amendedment is before or after the original details supplied</param>
		/// <returns>The merged journeys</returns>
		protected PublicJourneyDetail[] JoinPublicRoute( PublicJourneyDetail[] original, PublicJourneyDetail[] amended, bool amendFirst)
		{
			// Establish the order
			PublicJourneyDetail[] frontPart = amendFirst ? amended : original;
			PublicJourneyDetail[] rearPart = amendFirst ? original : amended;
			
			// Allocate journey that should be returned
			PublicJourneyDetail[] journey = new PublicJourneyDetail[ frontPart.Length + rearPart.Length ];

			// Merge journey details
			System.Array.Copy( frontPart, 0, journey, 0, frontPart.Length );
			System.Array.Copy( rearPart, 0, journey, frontPart.Length, rearPart.Length );
			return journey;
		}

        /// <summary>
        /// Sets up the routing guide sections for the adjusted public journey, 
        /// keeping the nessecary ones from the original journey and using the new ones from the amended journey
        /// </summary>
        /// <param name="originalPJ">The original public journey - used to obtain the original routing guide sections</param>
        /// <param name="adjustedPJ">The adjusted public journey - used to obtain the new routing guide sections</param>
        /// <param name="currentPJ">The current public journey to add the updated routing guide sections to</param>
        /// <param name="originalPJD">The unadjusted public journey details</param>
        /// <param name="amendedPJD">The adjusted public journey details</param>
        /// <param name="amendFirst">Flag indicating if the first part of the journey is amended, e.g. adjusted part + original part</param>
        /// <returns></returns>
        private PublicJourney UpdateRoutingGuideSections(PublicJourney originalPJ, PublicJourney adjustedPJ, PublicJourney currentPJ, PublicJourneyDetail[] originalPJD, PublicJourneyDetail[] amendedPJD, bool amendFirst)
        {
            // Set up the routing guide sections.
            // We can not be confident that an amended journey is routing guide complient. 
            // So a number of checks are made and appropriate routing guide sections are created.
            bool hasRailLegBeenModified = false;
            int railLegCount = 0;

            // (i)  Has the adjust changed the rail journey (loop through only the adjusted legs and check for rail mode)
            // (ii) Is there only one Rail leg in the journey (add up all rail mode legs)
            foreach (PublicJourneyDetail pjd in amendedPJD)
            {
                if ((pjd.Mode == ModeType.Rail) || (pjd.Mode == ModeType.RailReplacementBus))
                {
                    // a rail leg has been modified
                    hasRailLegBeenModified = true;

                    railLegCount++;
                }
            }

            // (ii) Is there only one Rail leg in the journey (add up all rail mode legs)
            foreach (PublicJourneyDetail pjd in originalPJD)
            {
                if ((pjd.Mode == ModeType.Rail) || (pjd.Mode == ModeType.RailReplacementBus))
                {
                    railLegCount++;
                }
            }

            // Set up the new routing guides for the journey

            // 1) If no rail leg has been modified, then we can copy the original Routing guide section 
            // into the new journey
            if (!hasRailLegBeenModified)
            {
                currentPJ.RoutingGuideSections = originalPJ.RoutingGuideSections;
                currentPJ.RoutingGuideCompliantJourney = originalPJ.RoutingGuideCompliantJourney;
            }
            else // The rail leg part of the journey has been modified:
            {
                // 2) If there is only one rail leg in the journey, then ok to keep the new Routing guide section
                // associated with it (CJP returned new routing guide sections for the adjusted journey)
                if (railLegCount == 1)
                {
                    RoutingGuideSection[] amendedJourneyRGS = adjustedPJ.RoutingGuideSections;
                    RoutingGuideSection[] newJourneyRGS;

                    // Need to remap the RoutingGuideSection leg IDs to our new public journey legs
                    if (amendFirst)
                    {
                        // The amended section is at the start of the journey, so RGS corresponds to the correct legs
                        newJourneyRGS = amendedJourneyRGS;
                    }
                    else
                    {
                        // The amended section is at the end of the journey, so add the first part legs on to the RGS
                        newJourneyRGS = new RoutingGuideSection[amendedJourneyRGS.Length];

                        // How many legs are in the preceeding (unadjusted) journey section
                        int originalLegsCount = originalPJD.Length;

                        // Go through all the Routing guide sections and update the leg indexes
                        for (int j = 0; j < amendedJourneyRGS.Length; j++)
                        {
                            RoutingGuideSection rgs = amendedJourneyRGS[j];

                            int[] newLegs = new int[rgs.Legs.Length];

                            for (int k = 0; k < rgs.Legs.Length; k++)
                            {
                                newLegs[k] = (rgs.Legs[k] + originalLegsCount);
                            }

                            newJourneyRGS[j] = new RoutingGuideSection(rgs.Id, newLegs, rgs.Compliant);
                        }
                    }

                    // Assign the updated Routing guide sections to the current journey
                    currentPJ.RoutingGuideSections = newJourneyRGS;
                    currentPJ.RoutingGuideCompliantJourney = adjustedPJ.RoutingGuideCompliantJourney;
                }
                else if (railLegCount > 1)
                {
                    // 3) Otherwise if there are multiple rail legs, then only show the rail fares (using the original 
                    // routing guide sections) if the legs match the original journey 
                    // (i.e. as long as journey continues to do A->B->C rather than A->D->C)
                    // Don't care if some rail legs were adjusted and some were not. As long as the complete journey has
                    // the same legs.
                    bool matchingJourney = true;

                    if ((originalPJ.Details.Length == currentPJ.Details.Length) && (currentPJ.Details.Length >= 1))
                    {
                        for (int l = 0; l < currentPJ.Details.Length; l++)
                        {
                            TDLocation origLegStart = originalPJ.Details[l].LegStart.Location;
                            TDLocation origLegEnd   = originalPJ.Details[l].LegEnd.Location;
                            TDLocation newLegStart  = currentPJ.Details[l].LegStart.Location;
                            TDLocation newLegEnd    = currentPJ.Details[l].LegEnd.Location;
                            ModeType origMode       = originalPJ.Details[l].Mode;
                            ModeType newMode        = currentPJ.Details[l].Mode;

                            // If original/new leg starts don't match, and the orignal/new leg ends don't match,
                            // then not a matching journey.
                            // Also ensure leg modes are the same.
                            if (!
                                (  (origLegStart.IsMatchingNaPTANGroup(newLegStart))
                                && (origLegEnd.IsMatchingNaPTANGroup(newLegEnd))
                                && (origMode == newMode))
                                )
                            {
                                matchingJourney = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        // Different number of legs, therefore not a matching journey
                        matchingJourney = false;
                    }

                    if (matchingJourney)
                    {
                        // The journeys are the "same" so we can reuse the original journey's routing guide section
                        currentPJ.RoutingGuideSections = originalPJ.RoutingGuideSections;
                        currentPJ.RoutingGuideCompliantJourney = originalPJ.RoutingGuideCompliantJourney;
                    }
                    else
                    {
                        // Different journey, clear routing guide so individual rail legs are fared
                        currentPJ.RoutingGuideSections = new RoutingGuideSection[0];
                        currentPJ.RoutingGuideCompliantJourney = false;
                    }

                }
            }

            return currentPJ;
        }

		#endregion
	}
}
