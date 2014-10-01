//***********************************************
// NAME         : CityToCityJourneyRequestPopulator.cs
// AUTHOR       : Richard Philpott
// DATE CREATED : 2006-02-15
// DESCRIPTION  : Responsible for populating CJP JourneyRequests
//				  for city-to-city trunk journeys.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/CityToCityJourneyRequestPopulator.cs-arc  $
//
//   Rev 1.6   Sep 01 2011 10:43:16   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.5   Mar 14 2011 15:11:52   RPhilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.4   Dec 21 2010 14:05:02   apatel
//Code updated to request services for the day of travel starting from 01:00 on the current day to 01:00 the following day for Find a train, Find a flight and City to City requests
//Resolution for 5651: CCN 593 - Show 10 results or show all
//
//   Rev 1.3   Mar 10 2008 15:17:42   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev DevFactory Jan 31 2008 14:00:00 mmodi
//Updated to check for ITP flag when determining which logic to fall in to for Air requests
//
//   Rev 1.0   Nov 08 2007 12:23:38   mturner
//Initial revision.
//
//   Rev DevFactory Jan 20 2008 19:00:00 dgath
//CCN0382b City to City enhancements:
//Updated to use the changed journey planning search time window by passing
//a useOverrideTimespan value of true to the updated PopulateSingleTrunkRequest 
//and GetTrunkSearchTimes methods.
//Updated to create a Car CJP call object now included in city to city journey requests
//
//   Rev 1.5   Mar 29 2007 14:36:44   TMollart
//Modifications for ITP. Journey requests now only have on request place with multiple request stops for each airport terminal.
//
//   Rev 1.4   Mar 13 2007 15:04:42   tmollart
//Modifications for ITP.
//
//   Rev 1.3   Mar 03 2006 15:51:46   RPhilpott
//Updated to include new secondaryModeFilter parameter on CJP request (then commented it out until new CJP interface received).
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.2   Feb 28 2006 14:56:46   RPhilpott
//Comments updated post code review.
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
using TransportDirect.UserPortal.AirDataProvider;

using Logger = System.Diagnostics.Trace;


namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Responsible for populating CJP JourneyRequests
	///	for city-to-city trunk journeys.
	/// </summary>
	public class CityToCityJourneyRequestPopulator : JourneyRequestPopulator
	{
		/// <summary>
		/// Constructs a new CityToCityJourneyRequestPopulator
		/// </summary>
		/// <param name="request">Related ITDJourneyRequest</param>
		public CityToCityJourneyRequestPopulator(ITDJourneyRequest request)
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
            #region Get flag value for ITP
            bool useCombinedAir = false;
            
            try
            {
                useCombinedAir = (Properties.Current[JourneyControlConstants.UseCombinedAir] == "Y");
                useCombinedAir = (useCombinedAir && (TDRequest.OriginLocation.CityInterchanges != null) && (TDRequest.OriginLocation.CityInterchanges.Length > 0));
            }
            catch (NullReferenceException)	// property not set up yet -- default to combined air off 
            {
                useCombinedAir = false;
            }
            #endregion


            ArrayList cjpCalls = new ArrayList();

			JourneyRequest request = null;

            bool overrideTimespan = true;


            if (!bool.TryParse(Properties.Current["PublicJourney.Trunk.OverrideTimespan"], out overrideTimespan))
            {
                // set overrideTimespan for train and flight to true by default
                overrideTimespan = true;
            }
            

			foreach (ModeType mode in TDRequest.Modes)
            {
                if (mode == ModeType.Car)
                {
                    #region Car Mode requests
                    if (TDRequest.IsOutwardRequired)
                    {
                        request = PopulateSingleCarRequest(TDRequest, false,

                                                    referenceNumber, seqNo++,
                                                    sessionId, referenceTransaction,
                                                    userType, language, true);

                        cjpCalls.Add(new CJPCall(request, false, referenceNumber, sessionId));
                    }

                    if (TDRequest.IsReturnRequired)
                    {
                        request = PopulateSingleCarRequest(TDRequest, true,

                                                    referenceNumber, seqNo++,
                                                    sessionId, referenceTransaction,
                                                    userType, language, true);

                        cjpCalls.Add(new CJPCall(request, true, referenceNumber, sessionId));
                    }

                    #endregion
                }
                else
                {
                    #region Public Transport Mode requests
                    // For Rail, Coach modes, and Air mode where ITP flag set to false
                    if ((mode != ModeType.Air)
                        || ((mode == ModeType.Air) && (!useCombinedAir)))
                    {
                        if (TDRequest.IsOutwardRequired)
                        {
                            request = PopulateSingleTrunkRequest(TDRequest,
                                                                    mode, TDRequest.OutwardDateTime[0], false,
                                                                    referenceNumber, seqNo++,
                                                                    sessionId, referenceTransaction,
                                                                    userType, language, overrideTimespan);

                            cjpCalls.Add(new CJPCall(request, false, referenceNumber, sessionId));
                        }

                        if (TDRequest.IsReturnRequired)
                        {
                            request = PopulateSingleTrunkRequest(TDRequest,
                                                                mode, TDRequest.ReturnDateTime[0], true,
                                                                referenceNumber, seqNo++,
                                                                sessionId, referenceTransaction,
                                                                userType, language, overrideTimespan);

                            cjpCalls.Add(new CJPCall(request, true, referenceNumber, sessionId));
                        }
                    }
                    else
                    {
                        #region ITP Calls
                        ArrayList directOriginAirports = new ArrayList();
                        ArrayList directDestinationAirports = new ArrayList();

                        // Loop through each origin city interchange.
                        foreach (CityInterchange icOrigin in TDRequest.OriginLocation.CityInterchanges)
                        {
                            // If interchange is an origin
                            if (icOrigin.StationType == StationType.Airport)
                            {
                                CityAirport airportOrigin = (CityAirport)icOrigin;

                                foreach (CityInterchange icDestination in TDRequest.DestinationLocation.CityInterchanges)
                                {
                                    if (icDestination.StationType == StationType.Airport)
                                    {
                                        CityAirport airportDestination = (CityAirport)icDestination;

                                        #region Setup Direct Origin and Destination airports
                                        // Origin AND Destination "Use Direct" flags are set.
                                        // Build up a list of direct flight origin and destination airports
                                        // [TM] NOTE: We dont seem to check here that direct flights exist
                                        //            between the origin and the destination.
                                        if (airportOrigin.UseDirect && airportDestination.UseDirect)
                                        {
                                            if (!directOriginAirports.Contains(airportOrigin))
                                            {
                                                directOriginAirports.Add(airportOrigin);
                                            }

                                            if (!directDestinationAirports.Contains(airportDestination))
                                            {
                                                directDestinationAirports.Add(airportDestination);
                                            }

                                        }
                                        #endregion

                                        #region CombinedAir cjp calls
                                        // Origin AND Destination "Use Combined" flags are set.
                                        // Combined air request is required					
                                        if (airportOrigin.UseCombined && airportDestination.UseCombined)
                                        {
                                            // Do direct flights exist between origin and destination airport
                                            if (AreDirectFlights(airportOrigin.AirportNaptan, airportDestination.AirportNaptan))
                                            {

                                                request = PopulateCombinedAirRequest(TDRequest,
                                                    airportOrigin,
                                                    airportDestination,
                                                    TDRequest.OutwardDateTime[0], false,
                                                    referenceNumber, seqNo++,
                                                    sessionId, referenceTransaction,
                                                    userType, language, overrideTimespan);

                                                cjpCalls.Add(new CJPCall(request, false, referenceNumber, sessionId));
                                            }

                                            // Does the user require a return journey.
                                            if (TDRequest.IsReturnRequired)
                                            {
                                                // Do direct flights exist between the destination and origin airport (return flight)
                                                if (AreDirectFlights(airportDestination.AirportNaptan, airportOrigin.AirportNaptan))
                                                {
                                                    request = PopulateCombinedAirRequest(TDRequest,
                                                        airportDestination,
                                                        airportOrigin,
                                                        TDRequest.ReturnDateTime[0], true,
                                                        referenceNumber, seqNo++,
                                                        sessionId, referenceTransaction,
                                                        userType, language, overrideTimespan);

                                                    cjpCalls.Add(new CJPCall(request, true, referenceNumber, sessionId));
                                                }
                                            }
                                        }
                                        #endregion

                                    }
                                }
                            }
                        } // End foreach

                        #region DirectAir cjp calls
                        // Do we have Direct Origin and Direct Destiantion airports
                        if (directOriginAirports.Count > 0 && directDestinationAirports.Count > 0)
                        {
                            // Direct air request
                            // [TM] NOTE: We seem to do this as well as the ITP requests (above)
                            request = PopulateDirectAirRequest(TDRequest,
                                directOriginAirports, directDestinationAirports,
                                TDRequest.OutwardDateTime[0], false,
                                referenceNumber, seqNo++,
                                sessionId, referenceTransaction,
                                userType, language, overrideTimespan);

                            cjpCalls.Add(new CJPCall(request, false, referenceNumber, sessionId));

                            if (TDRequest.IsReturnRequired)
                            {
                                request = PopulateDirectAirRequest(TDRequest,
                                    directDestinationAirports, directOriginAirports,
                                    TDRequest.ReturnDateTime[0], true,
                                    referenceNumber, seqNo++,
                                    sessionId, referenceTransaction,
                                    userType, language, overrideTimespan);

                                cjpCalls.Add(new CJPCall(request, true, referenceNumber, sessionId));
                            }
                        }
                        #endregion

                        #endregion

                    } // End (!= ModeType.Air) if

                    #endregion
                }
            }

			return (CJPCall[])(cjpCalls.ToArray(typeof(CJPCall)));
        }

        #region Private methods

        private JourneyRequest PopulateSingleCarRequest(ITDJourneyRequest tdRequest,
            bool returnJourney,
            int referenceNumber,
            int seqNo,
            string sessionId,
            bool referenceTransaction,
            int userType,
            string language,
            bool useOverrideTimespan)
        {
            JourneyRequest cjpRequest = InitialiseNewRequest(referenceNumber, seqNo,
                                                                sessionId, referenceTransaction,
                                                                userType, language);

            bool privateRequired = true;
            bool publicRequired = false;

            TDDateTime[] searchTimes = new TDDateTime[] { new TDDateTime(tdRequest.OutwardDateTime[0].GetDateTime()), new TDDateTime(tdRequest.OutwardDateTime[0].GetDateTime()) };

            if (returnJourney)
            {
                #region Return journey
                cjpRequest.depart = !tdRequest.ReturnArriveBefore;

                if (tdRequest.ReturnArriveBefore)
                {
                    searchTimes = GetTrunkSearchTimes(tdRequest.ReturnDateTime[0].GetDateTime(), tdRequest.ReturnAnyTime, true, ModeType.Car, useOverrideTimespan, returnJourney);
                }
                else
                {
                    searchTimes = GetTrunkSearchTimes(tdRequest.ReturnDateTime[0].GetDateTime(), tdRequest.ReturnAnyTime, false, ModeType.Car, useOverrideTimespan, returnJourney);
                }

                cjpRequest.origin = tdRequest.ReturnOriginLocation.ToRequestPlace(
                    cjpRequest.depart ? searchTimes[0] : null, 
                    StationType.Undetermined, publicRequired, privateRequired, false);
                cjpRequest.destination = tdRequest.ReturnDestinationLocation.ToRequestPlace(
                    cjpRequest.depart ? null : searchTimes[1], 
                    StationType.Undetermined, publicRequired, privateRequired, false);
                #endregion
            }
            else
            {
                #region Outward journey
                cjpRequest.depart = !tdRequest.OutwardArriveBefore;

                if (tdRequest.OutwardArriveBefore)
                {
                    searchTimes = GetTrunkSearchTimes(tdRequest.OutwardDateTime[0].GetDateTime(), tdRequest.OutwardAnyTime, true, ModeType.Car, useOverrideTimespan, returnJourney);

                }
                else
                {
                    searchTimes = GetTrunkSearchTimes(tdRequest.OutwardDateTime[0].GetDateTime(), tdRequest.OutwardAnyTime, false, ModeType.Car, useOverrideTimespan, returnJourney);
                }
                
                cjpRequest.origin = tdRequest.OriginLocation.ToRequestPlace(
                    cjpRequest.depart ? searchTimes[0] : null, 
                    StationType.Undetermined, publicRequired, privateRequired, false);
                cjpRequest.destination = tdRequest.DestinationLocation.ToRequestPlace(
                    cjpRequest.depart ? null : searchTimes[1], StationType.Undetermined, publicRequired, privateRequired, false);

                #endregion
            }

            cjpRequest.parkNRide = false;
            cjpRequest.modeFilter = new Modes();
            cjpRequest.modeFilter.include = true;

            // Only add the Car mode
            ModeType[] modeTypes = { ModeType.Car };
            cjpRequest.modeFilter.modes = CreateModeArray(modeTypes);

            cjpRequest.privateParameters = SetPrivateParameters(tdRequest);

            // If locations have no TOIDs, log it. CJP will raise also raise error but does not specify TOIDs were missing
            if (tdRequest.OriginLocation.Toid.Length == 0)
            {
                string message = "City to City location, " + tdRequest.OriginLocation.Description + " has no TOIDs";
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, message));
            }

            if (tdRequest.DestinationLocation.Toid.Length == 0)
            {
                string message = "City to City location, " + tdRequest.DestinationLocation.Description + " has no TOIDs";
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, message));
            }

            return cjpRequest;
        }

        /// <summary>
		/// Determines if there exist any direct flights between 
		/// the specified origin and destination airports.
		/// </summary>
		/// <param name="origin">TDNaptan of origin airport</param>
		/// <param name="destination">TDNaptan of destination airport</param>
		/// <returns>true if there are direct flights, false otherwise</returns>
		private bool AreDirectFlights(TDNaptan origin, TDNaptan destination)
		{
			AirDataProvider.IAirDataProvider adp = (AirDataProvider.IAirDataProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];
			
			ArrayList validDestinations = adp.GetValidDestinationAirports(new Airport[] { adp.GetAirportFromNaptan(origin.Naptan) } );

			foreach (Airport valid in validDestinations)
			{
				if	(valid.GlobalNaptan == destination.Naptan)
				{
					return true;
				}
			}

			return false;
		}

		
		/// <summary>
		/// Create a single fully-populated CJP JourneyRequest object
		/// for a single air-only trunk request for a specified date.
		/// </summary>
		/// <param name="tdRequest"></param>
		/// <param name="originAirport"></param>
		/// <param name="destinationAirport"></param>
		/// <param name="inputDateTime"></param>
		/// <param name="returnJourney"></param>
		/// <param name="referenceNumber"></param>
		/// <param name="seqNo"></param>
		/// <param name="sessionId"></param>
		/// <param name="referenceTransaction"></param>
		/// <param name="userType"></param>
		/// <param name="language"></param>
		/// <returns>Populated CJP JourneyRequest</returns>
		private JourneyRequest PopulateDirectAirRequest(ITDJourneyRequest tdRequest,
															ArrayList originAirports,
															ArrayList destinationAirports,
															TDDateTime inputDateTime,
															bool returnJourney,
															int referenceNumber, 
															int seqNo,	
															string sessionId,
															bool referenceTransaction, 
															int userType, 
															string language,
                                                            bool overrideTimespan)
		{
			
			JourneyRequest cjpRequest = InitialiseNewRequest(referenceNumber, seqNo,
				sessionId, referenceTransaction,
				userType, language);

			cjpRequest.operatorFilter = CreateOperatorFilter(tdRequest.SelectedOperators, tdRequest.UseOnlySpecifiedOperators);

			TDDateTime[] searchTimes = null;

			if  (returnJourney)
			{
				cjpRequest.depart = !tdRequest.ReturnArriveBefore;

				if  (tdRequest.ReturnArriveBefore)
				{
                    searchTimes = GetTrunkSearchTimes(inputDateTime.GetDateTime(), tdRequest.ReturnAnyTime, true, ModeType.Air, overrideTimespan, returnJourney);
				}
				else
				{
                    searchTimes = GetTrunkSearchTimes(inputDateTime.GetDateTime(), tdRequest.ReturnAnyTime, false, ModeType.Air, overrideTimespan, returnJourney);
				}

			}
			else
			{
				cjpRequest.depart = !tdRequest.OutwardArriveBefore;

				if  (tdRequest.OutwardArriveBefore)
				{
                    searchTimes = GetTrunkSearchTimes(inputDateTime.GetDateTime(), tdRequest.OutwardAnyTime, true, ModeType.Air, overrideTimespan, returnJourney);
				}
				else
				{
                    searchTimes = GetTrunkSearchTimes(inputDateTime.GetDateTime(), tdRequest.OutwardAnyTime, false, ModeType.Air, overrideTimespan, returnJourney);
				}
			}

			TDLocation origin = new TDLocation();

			CityAirport ca = (CityAirport)originAirports[0];

			origin.Description = ca.AirportNaptan.Name;
			origin.GridReference = ca.AirportNaptan.GridReference;
			origin.Locality = ca.AirportNaptan.Locality;
			origin.NaPTANs = new TDNaptan[originAirports.Count]; 

			for (int i = 0; i < originAirports.Count; i++) 
			{
				origin.NaPTANs[i] = ((CityAirport)originAirports[i]).AirportNaptan;
			}

			TDLocation destination = new TDLocation();

			ca = (CityAirport)destinationAirports[0];

			destination.Description = ca.AirportNaptan.Name;
			destination.GridReference = ca.AirportNaptan.GridReference;
			destination.Locality = ca.AirportNaptan.Locality;
			destination.NaPTANs = new TDNaptan[destinationAirports.Count];

			for (int i = 0; i < destinationAirports.Count; i++) 
			{
				destination.NaPTANs[i] = ((CityAirport)destinationAirports[i]).AirportNaptan;
			}

			cjpRequest.origin = origin.ToRequestPlace(searchTimes[0], StationType.Airport, true, false, false);
			cjpRequest.destination = destination.ToRequestPlace(searchTimes[1], StationType.Airport, true, false, false);

			cjpRequest.parkNRide = false;

			TimeSpan intervalTimeSpan = searchTimes[1].GetDateTime() - searchTimes[0].GetDateTime();

			cjpRequest.publicParameters = SetPublicParametersForTrunk(tdRequest, intervalTimeSpan, ModeType.Air, returnJourney, true);

			cjpRequest.modeFilter = new Modes();
			cjpRequest.modeFilter.include = true;
			cjpRequest.modeFilter.modes = CreateModeArray(new ModeType[] { ModeType.Air } );

			return cjpRequest;
		}


		/// <summary>
		/// Create a single fully-populated CJP JourneyRequest object
		/// for a single combined-mode request for a specified date.
		/// </summary>
		/// <param name="tdRequest"></param>
		/// <param name="originAirport"></param>
		/// <param name="inputDateTime"></param>
		/// <param name="returnJourney"></param>
		/// <param name="referenceNumber"></param>
		/// <param name="seqNo"></param>
		/// <param name="sessionId"></param>
		/// <param name="referenceTransaction"></param>
		/// <param name="userType"></param>
		/// <param name="language"></param>
		/// <returns>Populated CJP JourneyRequest</returns>
		private JourneyRequest PopulateCombinedAirRequest(ITDJourneyRequest tdRequest,
																CityAirport originAirport,
																CityAirport destinationAirport,
																TDDateTime inputDateTime,
																bool returnJourney,
																int referenceNumber, 
																int seqNo,	
																string sessionId,
																bool referenceTransaction, 
																int userType, 
																string language,
                                                                bool overrideTimespan)
		{
			
			JourneyRequest cjpRequest = InitialiseNewRequest(referenceNumber, seqNo,
				sessionId, referenceTransaction,
				userType, language);

			cjpRequest.operatorFilter = CreateOperatorFilter(tdRequest.SelectedOperators, tdRequest.UseOnlySpecifiedOperators);

			TDDateTime[] searchTimes = null;

			StationType[] originStationTypes	  = GetStationTypes(originAirport);
			StationType[] destinationStationTypes = GetStationTypes(destinationAirport);

			if  (returnJourney)
			{
				cjpRequest.depart = !tdRequest.ReturnArriveBefore;

				if  (tdRequest.ReturnArriveBefore)
				{
                    searchTimes = GetTrunkSearchTimes(inputDateTime.GetDateTime(), tdRequest.ReturnAnyTime, true, ModeType.Air, overrideTimespan, returnJourney);
				}
				else
				{
                    searchTimes = GetTrunkSearchTimes(inputDateTime.GetDateTime(), tdRequest.ReturnAnyTime, false, ModeType.Air, overrideTimespan, returnJourney);
				}

				cjpRequest.origin = tdRequest.ReturnOriginLocation.ToRequestPlace(searchTimes[0], originStationTypes, true, false, false);
				cjpRequest.destination = tdRequest.ReturnDestinationLocation.ToRequestPlace(searchTimes[1], destinationStationTypes, true, false, false);
			}
			else
			{
				cjpRequest.depart = !tdRequest.OutwardArriveBefore;

				if  (tdRequest.OutwardArriveBefore)
				{
                    searchTimes = GetTrunkSearchTimes(inputDateTime.GetDateTime(), tdRequest.OutwardAnyTime, true, ModeType.Air, overrideTimespan, returnJourney);
				}
				else
				{
                    searchTimes = GetTrunkSearchTimes(inputDateTime.GetDateTime(), tdRequest.OutwardAnyTime, false, ModeType.Air, overrideTimespan, returnJourney);
				}
				cjpRequest.origin = tdRequest.OriginLocation.ToRequestPlace(searchTimes[0], originStationTypes, true, false, false);
				cjpRequest.destination = tdRequest.DestinationLocation.ToRequestPlace(searchTimes[1], destinationStationTypes, true, false, false);
			}

			cjpRequest.parkNRide = false;

			TimeSpan intervalTimeSpan = searchTimes[1].GetDateTime() - searchTimes[0].GetDateTime();

			cjpRequest.publicParameters = SetPublicParametersForTrunk(tdRequest, intervalTimeSpan, ModeType.Air, returnJourney, false);
			cjpRequest.publicParameters.algorithm = PublicAlgorithmType.Fastest;

			TDLocation via = new TDLocation();
			via.Description = originAirport.AirportNaptan.Name;
			via.GridReference = originAirport.AirportNaptan.GridReference;
			via.Locality = originAirport.AirportNaptan.Locality;
			via.NaPTANs = new TDNaptan[] { originAirport.AirportNaptan };

			// Get all of the terminal codes for the airport
			ArrayList naptanList = new ArrayList();

			AirportTerminalNaptans atn = new AirportTerminalNaptans();

			TDNaptan[] terminals = atn.GetTerminalNaptans(via.NaPTANs);

			ArrayList requestPlaces = new ArrayList();

			TDLocation requestPlace = new TDLocation();

			requestPlace.Description = originAirport.AirportNaptan.Name;
			requestPlace.GridReference = originAirport.AirportNaptan.GridReference;
			requestPlace.Locality = originAirport.AirportNaptan.Locality;
			requestPlace.NaPTANs = terminals;

			requestPlaces.Add( requestPlace );

			cjpRequest.publicParameters.vias = new RequestPlace[ requestPlaces.Count ];

			int i=0;

			foreach (TDLocation location in requestPlaces)
			{
				cjpRequest.publicParameters.vias[i] = location.ToRequestPlace(searchTimes[0], StationType.Airport, true, false, false, true);
				i++;
			}

			ArrayList modesList = new ArrayList(); // = new ArrayList(GetSecondaryModes(originAirport, destinationAirport));
			
			
			foreach (Mode mode in GetSecondaryModes(originAirport, destinationAirport))
			{
				modesList.Add( mode.mode );
			}

			// Add Air as a mode. This is needed to tell the CJP to route this
			// request via ITP
			modesList.Add( ModeType.Air );

			cjpRequest.modeFilter = new Modes();
			cjpRequest.modeFilter.include = true;
			cjpRequest.modeFilter.modes = CreateModeArray( (ModeType[])modesList.ToArray(typeof(ModeType)) );
				
			cjpRequest.secondaryModeFilter = new Modes();
			cjpRequest.secondaryModeFilter.include = true;
			cjpRequest.secondaryModeFilter.modes = GetSecondaryModes(originAirport, destinationAirport);

			return cjpRequest;
		}


		/// <summary>
		/// Gets the station types of the modes that can 
		/// be used to travel to or from the airport
		/// </summary>
		/// <param name="airport"></param>
		/// <returns>Array of station types</returns>
		private StationType[] GetStationTypes(CityAirport airport)
		{
			StationType[] results = new StationType[airport.CombinedModes.Length];			

			int i = 0;

			foreach (ModeType mode in airport.CombinedModes)
			{
				results[i++] = ModeToStationType(mode);
			}
			
			return results;
		}


		/// <summary>
		/// Gets the modes that can be used 
		/// to travel to or from the airport
		/// </summary>
		/// <param name="origin"></param>
		/// <param name="destination"></param>
		/// <returns>CJP Mode[] array</returns>
		private Mode[] GetSecondaryModes(CityAirport origin, CityAirport destination)
		{
			ArrayList modeTypes = new ArrayList();

			foreach (ModeType mode in origin.CombinedModes)
			{
				if	(!modeTypes.Contains(mode))
				{
					modeTypes.Add(mode);
				}
			}

			foreach (ModeType mode in destination.CombinedModes)
			{
				if	(!modeTypes.Contains(mode))
				{
					modeTypes.Add(mode);
				}
			}

			return CreateModeArray(modeTypes);
        }

        #endregion

        /// <summary>
		/// 
		/// </summary>
		/// <param name="naptan"></param>
		/// <returns></returns>
		public TDLocation GetLocationForAirport(TDNaptan naptan)
		{
			NaptanCacheEntry entry = NaptanLookup.Get(naptan.Naptan, string.Empty);
			TDLocation location = new TDLocation();
			location.Locality = entry.Locality;
			location.GridReference = entry.OSGR;
			return location;
		}

	}




}
