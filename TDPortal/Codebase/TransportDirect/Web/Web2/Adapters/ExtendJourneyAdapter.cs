// *********************************************** 
// NAME                 : ExtendJourneyAdapter
// AUTHOR               : Paul Cross
// DATE CREATED         : 18/01/2006
// DESCRIPTION			: Helper class for methods unique to the Extend Journey facility
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/ExtendJourneyAdapter.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 12:58:56   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:16   mturner
//Initial revision.
//
//   Rev 1.6   Apr 03 2006 13:06:50   RGriffith
//IR3758 Fix: Return car journey locations are now populated in correct order
//
//   Rev 1.5   Mar 31 2006 09:53:38   pcross
//Populating the car journey locations array backwards for the return journey to get the locations in the right order for the extend journey line control
//Resolution for 3757: Journey line control has return journeys listed backwards
//
//   Rev 1.4   Mar 20 2006 18:01:04   pcross
//Code review updates
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Mar 14 2006 19:49:56   NMoorhouse
//fix stream3353 merge issues
//
//   Rev 1.2   Feb 21 2006 12:10:52   asinclair
//Added ExtendValidateAndSearch method
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Jan 27 2006 12:36:30   pcross
//Tweak to explicitly use ExtendItineraryManager
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Jan 19 2006 10:32:26   pcross
//Initial revision.

using System;
using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.JourneyPlanRunner;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Adapter for methods unique to the Extend Journey facility
	/// </summary>
	public class ExtendJourneyAdapter : TDWebAdapter
	{
		/// <summary>
		/// Constructor for ExtendJourneyAdapter
		/// </summary>
		public ExtendJourneyAdapter()
		{
		}

		/// <summary>
		/// Populates the outward and return itinerary locations of the ExtendJourneyLineControl
		/// </summary>
		/// <param name="control"></param>
		public void PopulateExtendJourneyLineControl(ExtendJourneyLineControl control)
		{

			// Get itinerary data to populate the control
			TDItineraryManager itineraryManager = ExtendItineraryManager.Current;
			
			if (itineraryManager.Length > 0)
			{
				// Get an array of journeys in the itinerary			
				Journey[] outwardJourneys = itineraryManager.OutwardJourneyItinerary;
				// And returns
				Journey[] returnJourneys = itineraryManager.ReturnJourneyItinerary;
			

				// Set up a blank outward journey location array and then populate with the
				// outwardJourneys array
				TDLocation[] outwardLocations = new TDLocation[outwardJourneys.Length + 1];
				int i = 0;

				// Loop around the journeys and create a list of the outward locations
				foreach (Journey outwardJourney in outwardJourneys)
				{
					if (outwardJourney.Type == TDJourneyType.RoadCongested)		// RoadCongested just means any road journey
					{
						outwardLocations[i] = itineraryManager.SpecificJourneyRequest(i).OriginLocation;

						if (i == outwardJourneys.Length - 1)
							outwardLocations[i + 1] = itineraryManager.SpecificJourneyRequest(i).DestinationLocation;
					}
					else
					{
						outwardLocations[i] = ((PublicJourneyDetail)((PublicJourney)outwardJourney).Details[0]).LegStart.Location;

						// Add the final destination to the locations array
						if (i == outwardJourneys.Length - 1)
						{
							PublicJourney publicOutwardJourney = (PublicJourney)outwardJourney;
							PublicJourneyDetail publicOutwardJourneyDetail = (PublicJourneyDetail)publicOutwardJourney.Details[publicOutwardJourney.Details.Length - 1];
							outwardLocations[i + 1] = publicOutwardJourneyDetail.LegEnd.Location;
						}
					}

					i++;
				}

				// Repeat for return journeys if there are any
				// Return journey location array
				TDLocation[] returnLocations = null;
				if (returnJourneys.Length  > 0)
				{
					returnLocations = new TDLocation[returnJourneys.Length + 1];
					i = 0;

					foreach (Journey returnJourney in returnJourneys)
					{
						if (returnJourney.Type == TDJourneyType.RoadCongested)
						{
							if (i == 0)
								returnLocations[0] = itineraryManager.SpecificJourneyRequest(returnLocations.Length - 2).ReturnOriginLocation;
							else
								returnLocations[i] = itineraryManager.SpecificJourneyRequest((returnLocations.Length - 1) - i).ReturnDestinationLocation;

							if (i == returnJourneys.Length - 1)
								returnLocations[i + 1] = itineraryManager.SpecificJourneyRequest(0).ReturnDestinationLocation;
						}
						else
						{
							returnLocations[i] = ((PublicJourneyDetail)((PublicJourney)returnJourney).Details[0]).LegStart.Location;

							// Add the final destination to the locations array
							if (i == returnJourneys.Length - 1)
							{
								PublicJourney publicReturnJourney = (PublicJourney)returnJourney;
								PublicJourneyDetail publicReturnJourneyDetail = (PublicJourneyDetail)publicReturnJourney.Details[publicReturnJourney.Details.Length - 1];
								returnLocations[i + 1] = publicReturnJourneyDetail.LegEnd.Location;
							}
						}

						i++;
					}
				}

				// Add the itinerary location arrays to the control
				control.OutwardItineraryLocations = outwardLocations;
				control.ReturnItineraryLocations = returnLocations;
				control.ExtendInProgress = itineraryManager.ExtendInProgress;

				// If an extension is in progress, set extension location properties
				if (itineraryManager.ExtendInProgress)
				{
					// Set that property of control
					control.ExtendEndOfItinerary = itineraryManager.ExtendEndOfItinerary;

					// Get the extension location from the journey request in session manager
					if (itineraryManager.ExtendEndOfItinerary)
						control.ExtensionLocation = TDSessionManager.Current.JourneyRequest.DestinationLocation;
					else
						control.ExtensionLocation = TDSessionManager.Current.JourneyRequest.OriginLocation;

				}
			}

		}

		/// <summary>
		/// This method calls validates the journey details entered and call 
		/// the wait page while the results are being processed 
		/// </summary>
		/// <param name="itineraryManager">The itineraryManager of the current journey</param>
		/// <returns>true if results are found or false if there are errors</returns>
		public bool ExtendValidateAndSearch()
		{
			JourneyPlanRunner.JourneyPlanRunner  runner = new JourneyPlanRunner.JourneyPlanRunner(Global.tdResourceManager);
			return runner.ValidateAndRun(
				TDSessionManager.Current,
				TDSessionManager.Current.JourneyParameters,
				TDPage.GetChannelLanguage(TDPage.SessionChannelName),
				true);
		}		
	}
}
