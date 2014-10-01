//***********************************************
// NAME         : TrunkJourneyRequestPopulator.cs
// AUTHOR       : Richard Philpott
// DATE CREATED : 2006-02-15
// DESCRIPTION  : Responsible for populating CJP JourneyRequests
//				  for trunk journeys that are not city-to-city. 
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/TrunkJourneyRequestPopulator.cs-arc  $
//
//   Rev 1.4   Sep 01 2011 10:43:26   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.3   Dec 21 2010 14:05:04   apatel
//Code updated to request services for the day of travel starting from 01:00 on the current day to 01:00 the following day for Find a train, Find a flight and City to City requests
//Resolution for 5651: CCN 593 - Show 10 results or show all
//
//   Rev 1.2   Mar 10 2008 15:18:12   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:24:02   mturner
//Initial revision.
//
//   Rev 1.2   Feb 28 2006 14:56:44   RPhilpott
//Comments updated post code review.
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.1   Feb 27 2006 12:17:30   RPhilpott
//Assign to IR 0018
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.0   Feb 27 2006 12:15:58   RPhilpott
//Initial revision.
//

using System;
using System.Collections;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;

using TransportDirect.JourneyPlanning.CJP;
using TransportDirect.JourneyPlanning.CJPInterface;

using TransportDirect.UserPortal.LocationService;

using Logger = System.Diagnostics.Trace;


namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Responsible for populating CJP JourneyRequests
	///	for trunk journeys that are not city-to-city. 
	/// </summary>
	public class TrunkJourneyRequestPopulator : JourneyRequestPopulator
	{
		/// <summary>
		/// Constructs a new TrunkJourneyRequestPopulator
		/// </summary>
		/// <param name="request">Related ITDJourneyRequest</param>
		public TrunkJourneyRequestPopulator(ITDJourneyRequest request)
		{
			TDRequest = request;
		}

		/// <summary>
		/// Creates the CJPRequest objects needed to call the CJP for the current 
		/// ITDJourneyRequest and returns them encapsulated in an array of CJPCall 
		/// objects.
		/// </summary>
		/// <param name="referenceNumber"></param>
		/// <param name="seqNo"></param>
		/// <param name="sessionId"></param>
		/// <param name="referenceTransaction"></param>
		/// <param name="userType"></param>
		/// <param name="language"></param>
		/// <returns>Array of CJPCall objects</returns>
		public override CJPCall[] PopulateRequests(int referenceNumber, 
														int seqNo,	
														string sessionId,
														bool referenceTransaction, 
														int userType, 
														string language)
		{
			ArrayList cjpCalls = new ArrayList();

			JourneyRequest request = null;

            bool overrideTimespan = false;

            if (TDRequest.FindAMode == FindAPlannerMode.Train
                || TDRequest.FindAMode == FindAPlannerMode.Flight)
            {
                if (!bool.TryParse(Properties.Current[string.Format("PublicJourney.{0}.OverrideTimespan", TDRequest.FindAMode)], out overrideTimespan))
                {
                    // set overrideTimespan for train and flight to true by default
                    overrideTimespan = true;
                }
            }

            foreach (ModeType mode in TDRequest.Modes)
			{
                if (TDRequest.IsOutwardRequired)
                {
                    foreach (TDDateTime inputTime in TDRequest.OutwardDateTime)
                    {
                        request = PopulateSingleTrunkRequest(TDRequest,
                                                            mode, inputTime, false,
                                                            referenceNumber, seqNo++,
                                                            sessionId, referenceTransaction,
                                                            userType, language, overrideTimespan);

                        cjpCalls.Add(new CJPCall(request, false, referenceNumber, sessionId));
                    }
                }

				if	(TDRequest.IsReturnRequired)
				{
					foreach (TDDateTime inputTime in TDRequest.ReturnDateTime)
					{
						request = PopulateSingleTrunkRequest(TDRequest, 
							mode, inputTime, true,
							referenceNumber, seqNo++,
							sessionId, referenceTransaction,
                            userType, language, overrideTimespan);

						cjpCalls.Add(new CJPCall(request, true, referenceNumber, sessionId));
					}
				}
			}

			return (CJPCall[])(cjpCalls.ToArray(typeof(CJPCall))); 
		}
	}
}
