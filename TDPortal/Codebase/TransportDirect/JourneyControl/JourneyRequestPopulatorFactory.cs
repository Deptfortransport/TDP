//***********************************************
// NAME         : JourneyRequestPopulatorfactory.cs
// AUTHOR       : Richard Philpott
// DATE CREATED : 2006-02-15
// DESCRIPTION  : Factory class to create the appropriate 
//                subclass of JourneyRequestPopulator for 
//                a given ITDJourneyRequest.
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/JourneyRequestPopulatorFactory.cs-arc  $
//
//   Rev 1.2   Mar 10 2008 15:17:52   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev DevFactory   Jan 31 2008 14:00:00  mmodi
//Update to always send City to City requests to the CityToCityRequestPopulator
//
//   Rev 1.0   Nov 08 2007 12:23:52   mturner
//Initial revision.
//
//   Rev 1.2   Feb 28 2006 14:22:30   RPhilpott
//Add more comments, post code review.
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.1   Feb 27 2006 12:17:32   RPhilpott
//Assign to IR 0018
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.0   Feb 27 2006 12:15:56   RPhilpott
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
	/// Factory class to create the appropriate subclass of
	/// JourneyRequestPopulator for a given ITDJourneyRequest.
	/// Sealed because contains static method only.
	/// </summary>
	public sealed class JourneyRequestPopulatorFactory
	{
		// never instantiated
		private JourneyRequestPopulatorFactory()
		{
		}

		/// <summary>
		/// Instantiates and returns a JourneyRequestPopulator of the appropriate
		/// subclass to create the CJPRequests for a specific ITDJourneyRequest. 
		/// </summary>
		/// <param name="request">The ITDJourneyRequest for which the CJP request is required</param>
		/// <returns>A JourneyRequestPopulator of the appropriate subclass</returns>
		public static JourneyRequestPopulator GetPopulator(ITDJourneyRequest request)
		{
			JourneyRequestPopulator populator = null;

			if	(!request.IsTrunkRequest)
			{
				populator = new MultiModalJourneyRequestPopulator(request);
			}
			else if	(request.Modes.Length == 1 && request.Modes[0] == ModeType.Car)
			{
				populator = new MultiModalJourneyRequestPopulator(request);
			}
			else
			{
                // City to City has been updated to include cars and specific journey time values.
                // Therefore, always need it to go to the City to city request populator.
                // A City to City request will have Rail, Coach, Air, (and/or Car)

                ArrayList modesList = new ArrayList(request.Modes);

                if (((modesList.Contains(ModeType.Rail)) && (modesList.Contains(ModeType.Coach)) && (modesList.Contains(ModeType.Air)))
                    || (modesList.Contains(ModeType.Car))
                    )
                {
                    populator = new CityToCityJourneyRequestPopulator(request);
                }
                else
                {
                    populator = new TrunkJourneyRequestPopulator(request);
                }
			}
			
			return populator;
		}
	}
}
