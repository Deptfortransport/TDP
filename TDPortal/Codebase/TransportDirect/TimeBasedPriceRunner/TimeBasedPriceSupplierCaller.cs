// *********************************************** 
// NAME			: TimeBasedPriceSupplierCaller.cs
// AUTHOR		: J George
// DATE CREATED	: 21/10/05
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TimeBasedPriceRunner/TimeBasedPriceSupplierCaller.cs-arc  $
//
//   Rev 1.3   Mar 12 2009 10:24:14   pscott
//Re introduction of Session/Fares problem solution 
//IR 5228
//
//   Rev 1.3   Mar 09 2009 14:34:52   apatel
//Added waiting in the PriceItinerary method if the call is asynchronous to look for the web page got unloaded.
//Resolution for 5228: Fares/Session Overwrite Problem
//
//   Rev 1.2   Feb 16 2009 12:11:38   rbroddle
//Amended to re-price the itinerary if a match can be made on NLC code.
//Resolution for 5221: CCN0492 Return Fares Involving Grouped Stations
//
//   Rev 1.1   Feb 02 2009 18:53:14   rbroddle
//Amended to check if we got rail fares back with matching NLC codes
//Resolution for 5221: CCN0492 Return Fares Involving Grouped Stations
//
//   Rev 1.0   Nov 08 2007 12:50:20   mturner
//Initial revision.
//
//   Rev 1.8   Oct 16 2007 14:01:50   mmodi
//Amended to accept a request ID
//Resolution for 4448: Logging: Request ID is not included when Coach fares request made
//
//   Rev 1.7   May 25 2007 16:22:16   build
//Automatically merged from branch for stream4401
//
//   Rev 1.6.1.0   May 10 2007 16:52:14   mmodi
//Added code to handle new NX coach fares
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.6   May 03 2006 11:38:36   RPhilpott
//Move PricingRetailOptionsState to partition-specific deferred storage.
//Resolution for 4005: DD075: Discount card entered in Find Cheaper retained if switch back to search by time
//Resolution for 4040: DD075: City-to-city shows return fares if change mode and causes an error
//
//   Rev 1.5   Mar 14 2006 08:41:46   build
//Automatically merged from branch for stream3353
//
//   Rev 1.4.1.4   Mar 10 2006 15:33:52   tmollart
//Updated from code review comments.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4.1.3   Mar 10 2006 15:21:54   RGriffith
//Extra check to ensure that only set pricing itinerary if not in ExtendInProgress mode
//
//   Rev 1.4.1.2   Mar 07 2006 14:15:52   RGriffith
//Comments added for Tickets/Costs alterations
//
//   Rev 1.4.1.1   Mar 06 2006 17:12:08   RGriffith
//Changes for Tickets/Costs
//
//   Rev 1.4.1.0   Feb 22 2006 11:56:32   RGriffith
//Changes made for multiple asynchronous ticket/costing
//
//   Rev 1.4   Nov 18 2005 09:12:58   mguney
//The AsyncCallState is stored with CostBased partition name for SearchByPrice. This is handled when using it from the TimeBasedPriceSupplierCaller.
//Resolution for 2991: DN040: SBP Pricing extended journey
//
//   Rev 1.3   Nov 02 2005 19:04:48   RPhilpott
//Integration test fixes.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Nov 02 2005 16:45:08   RPhilpott
//Added CJSessionInfo to PricePricingUnit() method.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Nov 02 2005 09:41:44   RPhilpott
//Unit test fixes.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 28 2005 14:50:18   RPhilpott
//Initial revision.
//
//   Rev 1.0   Oct 21 2005 18:14:56   jgeorge
//Initial revision.

using System;
using System.Collections;
using System.Threading;
using System.Diagnostics;
using Logger = System.Diagnostics.Trace;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService;
using TransportDirect.Common.PropertyService.Properties;

using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.RBOGateway;
using Domain = TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.SessionManager;

using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.PricingRetail.CoachFares;

namespace TransportDirect.UserPortal.TimeBasedPriceRunner
{
	
	/// <summary>
	/// Component used to invoke a fares search. This can be done either
	/// synchronously or asynchronously depending on the particular itinerary 
	/// being priced.
	/// </summary>
	public class TimeBasedPriceSupplierCaller : MarshalByRefObject
	{
		public delegate void PriceItineraryDelegate(CJPSessionInfo sessionInfo, TDSessionPartition partition, PricingRetailOptionsState options, string requestID);
		public delegate void PriceOptionsArrayDelegate(CJPSessionInfo sessionInfo, TDSessionPartition partition, int pricingArrayCount);
		private delegate void JourneyPlanAsyncDelegate(CJPRequest request);

		private static readonly string PROPERTY_TIME_BASED_NRS_QUERY = "RetailBusinessObjects.RVBO.TimeBased";

		/// <summary>
		/// Constructor for the TimeBasedPriceSupplierCaller
		/// </summary>
		public TimeBasedPriceSupplierCaller()
		{ }

		#region Helper methods to establish whether to call synchronously or asynchronously

		/// <summary>
		/// Determines whether or not the call to Price should be made asynchronously.
		/// </summary>
		/// <param name="journeyItinerary">The itinerary to check.</param>
		/// <returns>True if the pricing should be done asynchronously, false otherwise.</returns>
		private bool ShouldCallBeAsynchronous(Domain.Itinerary journeyItinerary)
		{
			// Determine whether or not the call should be asynchronous
			bool callAsynchronous = PricingUnitsRequireAsynchronousCall(journeyItinerary.OutwardUnits);
			
			// If we don't already know that the call should be asynchronous, check the inbound legs
			if (!callAsynchronous)
			{
				callAsynchronous = PricingUnitsRequireAsynchronousCall(journeyItinerary.ReturnUnits);
			}

			return callAsynchronous;
		}

		/// <summary>
		/// Checks a list of pricing units to establish whether any of them require an
		/// asynchronous call.
		/// </summary>
		/// <param name="pricingUnits">The list of units to check</param>
		/// <returns>True if the list of units should be priced asynchronously, false otherwise.</returns>
		private bool PricingUnitsRequireAsynchronousCall(IList pricingUnits)
		{
			// Iterate through the units checking each for need for asynchronous call
			foreach (TransportDirect.UserPortal.PricingRetail.Domain.PricingUnit unit in pricingUnits)
			{
				if (IsCoachUnit(unit) || IsNRSServiceCallRequired(unit))
				{
					return true;
				}
			}

			// No units requiring an asynchronous call have been found so return true
			return false;
		}

		/// <summary>
		/// Checks whether the pricing unit is for a coach journey 
		///  (note that some TL's may return a mode of Bus for a coach service)
		/// </summary>
		/// <param name="unit">The unit to check.</param>
		/// <returns>True if the unit is for a coach journey, false otherwise.</returns>
		private bool IsCoachUnit(Domain.PricingUnit unit)
		{
			return (unit.Mode == ModeType.Coach || unit.Mode == ModeType.Bus);
		}
        
		/// <summary>
		/// Checks whether the pricing unit will require a call to the NRS service.
		/// </summary>
		/// <param name="unit">The unit to check.</param>
		/// <returns>True if the unit requires a call to the NRS service.</returns>
		private bool IsNRSServiceCallRequired(Domain.PricingUnit unit)
		{
			if	(Properties.Current[PROPERTY_TIME_BASED_NRS_QUERY] != "Y")
			{
				return false;	// all time-based NRS checking turned off
			}

			if (unit.Mode == ModeType.Rail || unit.Mode == ModeType.RailReplacementBus)
			{
				// Check outbound legs first
				foreach (PublicJourneyDetail leg in unit.OutboundLegs)
				{
					VehicleFeatureToDtoConvertor converter = new VehicleFeatureToDtoConvertor(leg.GetVehicleFeatures());
					
					if (converter.Reservations.Length > 0 && converter.Reservations != " ")
					{
						return true;
					}
				}

				// Not found, so check inbound legs
				foreach (PublicJourneyDetail leg in unit.InboundLegs)
				{
					VehicleFeatureToDtoConvertor converter = new VehicleFeatureToDtoConvertor(leg.GetVehicleFeatures());
					
					if (converter.Reservations.Length > 0 && converter.Reservations != " ")
					{
						return true;
					}
				}

				// Not found so return false
				return false;
			}
			
			return false; 
		}

		#endregion

		#region Helper methods to actually make the call

		/// <summary>
		/// Overloaded method for DoAsynchronousCall(CJPSessionInfo sessionInfo, TDSessionPartition partition, PricingRetailOptionsState options, TDSessionSerializer serializer)
		/// </summary>
		/// <param name="sessionInfo">CJPSessionInfo object containing the user session information.</param>
		/// <param name="partition">Partition to use when obtaining the session item.</param>
		/// <param name="pricingArrayCount">Index value to price the specific itinerary array item</param>
		/// <returns>AsyncCallStatus indicating the state of the call.</returns>
		private AsyncCallStatus DoAsynchronousCall(CJPSessionInfo sessionInfo, TDSessionPartition partition, int pricingArrayCount)
		{
			try
			{
				PriceOptionsArrayDelegate pricingDelegate = new PriceOptionsArrayDelegate(PriceOptionsArray);
				pricingDelegate.BeginInvoke(sessionInfo, partition, pricingArrayCount, null, null);
				return AsyncCallStatus.InProgress;
			}
			catch (TDException e)
			{
				if (!e.Logged)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, e.Message, e, sessionInfo.SessionId));
				}
				return AsyncCallStatus.NoResults;
			}
			catch (Exception e)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, e.Message, e, sessionInfo.SessionId));
				return AsyncCallStatus.NoResults;
			}
		}

		/// <summary>
		/// Invokes the pricing method synchronously.
		/// </summary>
		/// <param name="sessionInfo">CJPSessionInfo object containing the user session information.</param>
		/// <param name="partition">Partition to use when obtaining the session item.</param>
		/// <param name="options">The pricing retail options state object for the user.</param>
		/// <returns>AsyncCallStatus indicating the state of the call.</returns>
		private AsyncCallStatus DoSynchronousCall(CJPSessionInfo sessionInfo, TDSessionPartition partition, PricingRetailOptionsState options, TDSessionSerializer serializer)
		{
			return DoSynchronousCall(sessionInfo, partition, options, serializer, string.Empty);
		}

		/// <summary>
		/// Invokes the pricing method synchronously.
		/// </summary>
		/// <param name="sessionInfo">CJPSessionInfo object containing the user session information.</param>
		/// <param name="partition">Partition to use when obtaining the session item.</param>
		/// <param name="options">The pricing retail options state object for the user.</param>
		/// <returns>AsyncCallStatus indicating the state of the call.</returns>
		private AsyncCallStatus DoSynchronousCall(CJPSessionInfo sessionInfo, TDSessionPartition partition, PricingRetailOptionsState options, TDSessionSerializer serializer, string requestID)
		{
			try
			{
				this.PriceItinerary(sessionInfo, partition, options, requestID);
				serializer.SerializeSessionObjectAndSave(sessionInfo.SessionId, TDSessionManager.KeyPricingRetailOptions, options);
				return AsyncCallStatus.CompletedOK;
			}
			catch (TDException e)
			{
				if (!e.Logged)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, e.Message, e, sessionInfo.SessionId));
				}
				return AsyncCallStatus.NoResults;
			}
			catch (Exception e)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, e.Message, e, sessionInfo.SessionId));
				return AsyncCallStatus.NoResults;
			}
		}

		/// <summary>
		/// Invokes the pricing method asynchronously.
		/// </summary>
		/// <param name="sessionInfo">CJPSessionInfo object containing the user session information.</param>
		/// <param name="partition">Partition to use when obtaining the session item.</param>
		/// <returns>AsyncCallStatus indicating the state of the call.</returns>
		private AsyncCallStatus DoAynchronousCall(CJPSessionInfo sessionInfo, TDSessionPartition partition)
		{
			return DoAynchronousCall(sessionInfo, partition, string.Empty);
		}

		/// <summary>
		/// Invokes the pricing method asynchronously.
		/// </summary>
		/// <param name="sessionInfo">CJPSessionInfo object containing the user session information.</param>
		/// <param name="partition">Partition to use when obtaining the session item.</param>
		/// <returns>AsyncCallStatus indicating the state of the call.</returns>
		private AsyncCallStatus DoAynchronousCall(CJPSessionInfo sessionInfo, TDSessionPartition partition, string requestID)
		{
			try
			{
				PriceItineraryDelegate pricingDelegate = new PriceItineraryDelegate(PriceItinerary);
				pricingDelegate.BeginInvoke(sessionInfo, partition, null, requestID, null, null);
				return AsyncCallStatus.InProgress;
			}
			catch (TDException e)
			{
				if (!e.Logged)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, e.Message, e, sessionInfo.SessionId));
				}
				return AsyncCallStatus.NoResults;
			}
			catch (Exception e)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, e.Message, e, sessionInfo.SessionId));
				return AsyncCallStatus.NoResults;
			}
		}
		#endregion

		#region Methods that do the work

		/// <summary>
		/// Prices the itinerary for an array element. Calls DoAsynchronousCall and passes in the index value
		/// </summary>
		/// <param name="sessionInfo">CJPSessionInfo object containing the user session information.</param>
		/// <param name="partition">Partition to use when obtaining the session item.</param>
		/// <param name="pricingArrayCount">Index value to price the specific itinerary array item</param>
		/// <returns>AsyncCallStatus indicating the state of the call.</returns>
		public AsyncCallStatus PriceItinerary(CJPSessionInfo sessionInfo, TDSessionPartition partition, int pricingArrayCount)
		{
			return DoAsynchronousCall(sessionInfo, partition, pricingArrayCount);
		}

		/// <summary>
		/// Prices the itinerary for an array element. This may be done either synchronously or asynchronously depending
		/// on the itinerary being priced. Now calls common code found in PriceOptions method
		/// </summary>
		/// <param name="sessionInfo">CJPSessionInfo object containing the user session information.</param>
		/// <param name="partition">Partition to use when obtaining the session item.</param>
		/// <param name="pricingArrayCount">Index value to price the specific itinerary array item</param>
		/// <returns>AsyncCallStatus indicating the state of the call.</returns>
		[MTAThread]
		private void PriceItinerary(CJPSessionInfo sessionInfo, TDSessionPartition partition, PricingRetailOptionsState options, string requestID)
		{
			AsyncCallStatus status = AsyncCallStatus.InProgress;
	
			TDSessionSerializer serializer = new TDSessionSerializer(sessionInfo.OriginAppDomainFriendlyName);

			if (options == null)
			{
				options = (PricingRetailOptionsState)serializer.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyPricingRetailOptions);
			}

			// Old code here moved into PriceOptions method
			status = PriceOptions(sessionInfo, ref options, requestID);

            //CCN492 We may now have rail fares back with matching NLC codes - we need to check for PU match again,
            // reset the override itinerary type and do the fare calls again if it turns out it's actually a return
            if (options.OverrideItineraryType == Domain.ItineraryType.Single
                && options.JourneyItinerary.Type == Domain.ItineraryType.Single)
            {
                options.JourneyItinerary.CheckMatching(false);
                if (options.JourneyItinerary.Type == Domain.ItineraryType.Return)
                //If we have a return now that means the pricing units were found to match on locations
                //after the initial fare call, so reprice the options object as a return...
                {
                    options.OverrideItineraryType = Domain.ItineraryType.Return;
                    status = PriceOptions(sessionInfo, ref options, requestID);
                }
            }

            // Wait to see if the page got unloaded by checking the ASPSessionState Locked value getting written in the 
            // ASPStateTempSession table in ASPState database
            if (ShouldCallBeAsynchronous(options.JourneyItinerary))
            {
                // Get the web server lock status
                bool webServerHoldsTheLock = serializer.GetSessionLockStatus(sessionInfo.SessionId);

                int count = 0;
                int appServerWaitInMiliSecondsForWebServer = 100;
                int appServerWaitCountForWebServer = 30;

                int.TryParse(Properties.Current["TimeBasedPriceSupplier.AppServerWaitInMiliSecondsForWebServer"], out appServerWaitInMiliSecondsForWebServer);
                int.TryParse(Properties.Current["TimeBasedPriceSupplier.AppServerWaitCountForWebServer"], out appServerWaitCountForWebServer);

                // while web server working on page just wait.
                while (webServerHoldsTheLock)
                {
                    count++;
                    System.Threading.Thread.Sleep(appServerWaitInMiliSecondsForWebServer);
                    webServerHoldsTheLock = serializer.GetSessionLockStatus(sessionInfo.SessionId);

                    // In the event of webserver session for some reason broken make sure appserver doesn't get locked down
                    if (count == appServerWaitCountForWebServer)
                    {
                        break;
                    }
                }
            }

			// Update the PricingRetailOptionsState in deferred storage
			serializer.SerializeSessionObjectAndSave(sessionInfo.SessionId, partition, TDSessionManager.KeyPricingRetailOptions, options);
			
			Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "PricingRetailOptions updated in deferred storage - FaresInitialised = " + options.JourneyItinerary.FaresInitialised));

			// Update the status in deferred storage
			AsyncCallState state = (AsyncCallState)serializer.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyAsyncCallState);

			if (state == null)
			{
				partition = TDSessionPartition.CostBased;
				state = (AsyncCallState)serializer.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyAsyncCallState);
			}

			state.Status = status;
			serializer.SerializeSessionObjectAndSave(sessionInfo.SessionId, partition, TDSessionManager.KeyAsyncCallState, state);

			if (TDTraceSwitch.TraceVerbose)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "AsyncStatus updated in deferred storage"));
			}
		}


		/// <summary>
		/// Prices the itinerary. This may be done either synchronously or asynchronously depending
		/// on the itinerary being priced.
		/// </summary>
		/// <param name="sessionInfo">CJPSessionInfo object containing the user session information.</param>
		/// <param name="partition">Partition to use when obtaining the session item.</param>
		/// <returns>AsyncCallStatus indicating the state of the call.</returns>
		public AsyncCallStatus PriceItinerary(CJPSessionInfo sessionInfo, TDSessionPartition partition)
		{
			return PriceItinerary(sessionInfo, partition, string.Empty);
		}

		/// <summary>
		/// Prices the itinerary. This may be done either synchronously or asynchronously depending
		/// on the itinerary being priced.
		/// </summary>
		/// <param name="sessionInfo">CJPSessionInfo object containing the user session information.</param>
		/// <param name="partition">Partition to use when obtaining the session item.</param>
		/// <returns>AsyncCallStatus indicating the state of the call.</returns>
		public AsyncCallStatus PriceItinerary(CJPSessionInfo sessionInfo, TDSessionPartition partition, string requestID)
		{
			TDSessionSerializer serializer = new TDSessionSerializer(sessionInfo.OriginAppDomainFriendlyName);
			
			// Load the PricingRetailOptionState and AsyncCallState objects
			PricingRetailOptionsState options = (PricingRetailOptionsState)serializer.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyPricingRetailOptions);

			// Make the call
			AsyncCallStatus status = AsyncCallStatus.None;

			if (ShouldCallBeAsynchronous(options.JourneyItinerary))
			{
				status = DoAynchronousCall(sessionInfo, partition, requestID);
			}
			else
			{
				status = DoSynchronousCall(sessionInfo, partition, options, serializer);
			}

			// Update the status in deferred storage
			AsyncCallState state = (AsyncCallState)serializer.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyAsyncCallState);
			//For Search By Price, TimeBased features are used. 
			//The AsyncCallState is stored with CostBased partition name for SearchByPrice.
			//That's why if the state is null, this means that this method is being used by SearchByPrice.
			//So the partition should be CostBased to get the right object.
			if (state == null)
			{
				partition = TDSessionPartition.CostBased;
				state = (AsyncCallState)serializer.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyAsyncCallState);
			}

			state.Status = status;
			serializer.SerializeSessionObjectAndSave(sessionInfo.SessionId, partition, TDSessionManager.KeyAsyncCallState, state);
			
			return status;
		}

		/// <summary>
		/// Common code stripped from PriceItinerary(CJPSessionInfo, TDSessionPartition, PricingRetailOptionsState).
		/// Invokes the FareCall class to carry out the pricing of an individual PricingRetailOptionsState
		/// </summary>
		/// <param name="sessionInfo">CJPSessionInfo object containing the user session information.</param>
		/// <param name="options">PricingRetatilOptionsState to set pricing units for</param>
		/// <returns>AsyncCallStatus indicating the state of the call.</returns>
		private AsyncCallStatus PriceOptions(CJPSessionInfo sessionInfo, ref PricingRetailOptionsState options, string requestID)
		{
			AsyncCallStatus status = AsyncCallStatus.InProgress;

			ArrayList fareCallList = new ArrayList(options.JourneyItinerary.OutwardUnits.Count + options.JourneyItinerary.ReturnUnits.Count);

			int puIndex = 0;
			int fareRequestIncrement = 0;

			// Add an increment to requestID for each fare call made

			foreach (Domain.PricingUnit unit in options.JourneyItinerary.OutwardUnits)
			{
				FareCall fareCall = new FareCall(unit, options.Discounts, false, puIndex, sessionInfo, (requestID + fareRequestIncrement.ToString("00")));
				fareCallList.Add(fareCall);
				puIndex++;
				fareRequestIncrement++;
			}

			puIndex = 0;

			foreach (Domain.PricingUnit unit in options.JourneyItinerary.ReturnUnits)
			{
				FareCall fareCall = new FareCall(unit, options.Discounts, true, puIndex, sessionInfo, (requestID + fareRequestIncrement.ToString("00")));
				fareCallList.Add(fareCall);
				puIndex++;
				fareRequestIncrement++;
			}

			bool fareCallFailed = false;

			WaitHandle[] wh = new WaitHandle[fareCallList.Count];

			int callCount = 0;

			foreach (FareCall fareCall in fareCallList)
			{
				wh[callCount] = fareCall.InvokeFareSupplier();

				if	(wh[callCount] == null)
				{
					fareCallFailed = true;
				}

				callCount++;
			}

			if	(!fareCallFailed)
			{

				int fareCallTimeOut = Int32.Parse(Properties.Current[JourneyControlConstants.CJPTimeoutMillisecs]);

				int startTime, endTime;
				
				foreach (ManualResetEvent mre in wh)
				{
					startTime = (int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
					mre.WaitOne(fareCallTimeOut, false);
					endTime = (int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
					fareCallTimeOut -= (endTime - startTime);
				}

				foreach (FareCall fareCall in fareCallList)
				{
					Domain.PricingUnit resultPricingUnit = fareCall.GetResult();

					if	(fareCall.IsSuccessful)
					{
						// we have at least one unit priced successfully, so update status ... 
						status = AsyncCallStatus.CompletedOK;
						options.JourneyItinerary.FaresInitialised = true;
						
						if	(!fareCall.IsReturn)
						{
							options.JourneyItinerary.OutwardUnits[fareCall.Index] = resultPricingUnit;
						}
						else
						{
							options.JourneyItinerary.ReturnUnits[fareCall.Index] = resultPricingUnit;
						}

						#region New NX Coach fares change
						// Changes to the NX Coach fares interface means return fares must be combined
						if (resultPricingUnit.Mode == ModeType.Coach)
						{
							if ((resultPricingUnit.MatchingReturn) && (resultPricingUnit.CoachFaresReturnComponent))
							{
								CoachFareFilterAndMergeHelper coachFareHelper = new CoachFareFilterAndMergeHelper();

								if	(fareCall.IsReturn)
								{
									Domain.PricingUnit outwardPricingUnit = (Domain.PricingUnit)options.JourneyItinerary.OutwardUnits[fareCall.Index];
									if (outwardPricingUnit.CoachFaresReturnComponent)
									{
										Domain.PricingResult returnPricingResult = coachFareHelper.CombineReturnCoachFares(outwardPricingUnit, resultPricingUnit);

										outwardPricingUnit.SetFaresReturn(returnPricingResult);
										resultPricingUnit.SetFaresReturn(returnPricingResult);

										options.JourneyItinerary.OutwardUnits[fareCall.Index] = outwardPricingUnit;
										options.JourneyItinerary.ReturnUnits[fareCall.Index] = resultPricingUnit;
									}
								}
								else
								{
									Domain.PricingUnit returnPricingUnit = (Domain.PricingUnit)options.JourneyItinerary.ReturnUnits[fareCall.Index];
									if (returnPricingUnit.CoachFaresReturnComponent)
									{
										Domain.PricingResult returnPricingResult = coachFareHelper.CombineReturnCoachFares(resultPricingUnit, returnPricingUnit);

										resultPricingUnit.SetFaresReturn(returnPricingResult);
										returnPricingUnit.SetFaresReturn(returnPricingResult);									

										options.JourneyItinerary.OutwardUnits[fareCall.Index] = resultPricingUnit;;
										options.JourneyItinerary.ReturnUnits[fareCall.Index] = returnPricingUnit;
									}

								}




							}
						}

						#endregion

					}
				}
			}
			return status;
		}


		/// <summary>
		/// Prices the itinerary for a PricingRetailOptionsstate array. 
		/// This may be done either synchronously or asynchronously depending on the itinerary being priced.
		/// </summary>
		/// <param name="sessionInfo">CJPSessionInfo object containing the user session information.</param>
		/// <param name="partition">Partition to use when obtaining the session item.</param>
		/// <param name="pricingArrayCount">Index value to price the specific itinerary array item</param>
		private void PriceOptionsArray(CJPSessionInfo sessionInfo, TDSessionPartition partition, int pricingArrayCount)
		{
			TDSessionSerializer serializer = new TDSessionSerializer(sessionInfo.OriginAppDomainFriendlyName);

			TDItineraryManager itineraryManager = (TDItineraryManager)serializer.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyItineraryManager);

			// Load the PricingRetailOptionState and AsyncCallState objects
			PricingRetailOptionsState[] options = DeserializeOptions(sessionInfo, partition, pricingArrayCount);

			ArrayList pricingCallList = new ArrayList();

			// Add all PricingRetailOptionsState elements to the array list used for processing
			for (int i = 0; i < options.Length; i++)
			{
				pricingCallList.Add(new PricingCall(sessionInfo, partition, i));
			}

			// Create new WaitHandle array
			WaitHandle[] waitHandle = new WaitHandle[pricingCallList.Count];

			// For each element in PricingRetailOptionsState[] create a PricingCall asynchronous element
			// and invoke it's pricing method
			for (int i = 0; i < pricingCallList.Count; i++)
			{
				PricingCall currentItem = (PricingCall)pricingCallList[i];
				waitHandle[i] = currentItem.InvokePricing();
			}

			// Get timeout properties
			int pricingCallTimeOut = Int32.Parse(Properties.Current[JourneyControlConstants.CJPTimeoutMillisecs]);

			// Force a wait in milliseconds for each wait handle event
			int startTime, endTime;
			foreach (ManualResetEvent mre in waitHandle)
			{
				startTime = (int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
				mre.WaitOne(pricingCallTimeOut, false);
				endTime = (int)(DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
				pricingCallTimeOut -= (endTime - startTime);
			}

			// Get result of each asynchronous PricingCall
			bool complete = true;
			AsyncCallStatus priceCallStatus;
			foreach (PricingCall priceCallItem in pricingCallList)
			{
				priceCallStatus = priceCallItem.GetResult();

				// If PricingCall item is not successful then flag process as incomplete
				if (!priceCallItem.IsSuccessful)
				{
					complete = false;
				}
			}
			
			// Deserialize the pricing options array.
			PricingRetailOptionsState[] deserializedOptions = DeserializeOptions(sessionInfo, partition, pricingArrayCount);

			// Save the pricing array itinerary in the itinerary manager (if using an itinerary manager)
			if ((itineraryManager.Length > 0) && !itineraryManager.ExtendInProgress)
			{
				itineraryManager.SetItineraryPricing(deserializedOptions, complete);
			}
			else
			{
				// else serialize the session objects and save the pricing options
				serializer.SerializeSessionObjectAndSave(sessionInfo.SessionId, partition, TDSessionManager.KeyPricingRetailOptions, deserializedOptions[0]);
				itineraryManager.PricingDataComplete = complete;
			}
			serializer.SerializeSessionObjectAndSave(sessionInfo.SessionId, partition, TDSessionManager.KeyItineraryManager, itineraryManager);

			// Determine the Aysnchronous call status
			AsyncCallState stateData = (AsyncCallState)serializer.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyAsyncCallState);
			stateData.Status = AsyncCallStatus.CompletedOK;
			serializer.SerializeSessionObjectAndSave(sessionInfo.SessionId, partition, TDSessionManager.KeyAsyncCallState, stateData);
		}
		
		/// <summary>
		/// Prices the itinerary for an individual PricubgRetailOptionsState element. 
		/// This method may be called synchronously or asynchronously as required.
		/// </summary>
		/// <param name="sessionInfo">CJPSessionInfo object containing the user session information.</param>
		/// <param name="partition">Partition to use when obtaining the session item.</param>
		/// <param name="pricingArrayIndex">Index value to price the specific itinerary array item</param>
		[MTAThread]
		public AsyncCallStatus PriceOptionsArrayElement(CJPSessionInfo sessionInfo, TDSessionPartition partition, int pricingArrayIndex)
		{
			AsyncCallStatus status = AsyncCallStatus.InProgress;
			
			TDSessionSerializer serializer = new TDSessionSerializer(sessionInfo.OriginAppDomainFriendlyName);

			PricingRetailOptionsState options = (PricingRetailOptionsState)serializer.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyPricingRetailOptionsArray, pricingArrayIndex);

			status = PriceOptions(sessionInfo, ref options, string.Empty);
			
			serializer.SerializeSessionObjectAndSave(sessionInfo.SessionId, partition, TDSessionManager.KeyPricingRetailOptionsArray, options, pricingArrayIndex);

			return status;
		}

		/// <summary>
		/// Method to deserialize the PricingRetailOptionsState array
		/// </summary>
		/// <param name="sessionInfo">CJPSessionInfo object containing the user session information.</param>
		/// <param name="partition">Partition to use when obtaining the session item.</param>
		/// <param name="pricingArrayIndex">Index value to price the specific itinerary array item</param>
		/// <returns>Deserialized PricingRetailOptionsState array</returns>
		private PricingRetailOptionsState[] DeserializeOptions(CJPSessionInfo sessionInfo, TDSessionPartition partition, int pricingArrayCount)
		{
			TDSessionSerializer serializer = new TDSessionSerializer(sessionInfo.OriginAppDomainFriendlyName);
			PricingRetailOptionsState[] options = new PricingRetailOptionsState[pricingArrayCount];

			for (int i=0; i < pricingArrayCount; i++)
			{
				options[i] = (PricingRetailOptionsState)serializer.RetrieveAndDeserializeSessionObject(sessionInfo.SessionId, partition, TDSessionManager.KeyPricingRetailOptionsArray, i);
			}

			return options;
		}
		#endregion
	}


	/// <summary>
	/// FareCall class. Asynchronous Delegate to actually do the pricing calls
	/// </summary>
	class FareCall
	{
		private delegate Domain.PricingUnit FareSupplierAsyncDelegate(Domain.PricingUnit pricingUnit, Domain.Discounts discounts, CJPSessionInfo sessionInfo, string requestID);

		private CJPSessionInfo sessionInfo = null;
		private bool success = false;
		private Domain.PricingUnit pricingUnit = null;
		private Domain.Discounts discounts = null;
		private bool isReturn = false;
		private int index = 0;
		private string requestID = string.Empty;

		private FareSupplierAsyncDelegate fareDelegate = null;
		private IAsyncResult fareDelegateASR = null;

		public FareCall(Domain.PricingUnit pricingUnit, Domain.Discounts discounts, bool isReturn, int index, CJPSessionInfo sessionInfo, string requestID)
		{
			this.pricingUnit = pricingUnit;
			this.discounts = discounts;
			this.sessionInfo = sessionInfo;
			this.index = index;
			this.isReturn = isReturn;
			this.requestID = requestID;
		}

		public WaitHandle InvokeFareSupplier()
		{
			try
			{
				Domain.ITimeBasedFareSupplierFactory factory = (Domain.ITimeBasedFareSupplierFactory)TDServiceDiscovery.Current[ServiceDiscoveryKey.TimeBasedFareSupplier];
				Domain.ITimeBasedFareSupplier supplier = factory.GetSupplier(pricingUnit.Mode);
				fareDelegate = new FareSupplierAsyncDelegate(supplier.PricePricingUnit);
				fareDelegateASR = fareDelegate.BeginInvoke(pricingUnit, discounts, sessionInfo, requestID, null, null);
				return fareDelegateASR.AsyncWaitHandle;
			}
			catch (Exception e)
			{
				OperationalEvent oe = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, "Exception on FareSupplier call", e, sessionInfo.SessionId);
				Logger.Write(oe);
				return null;
			}
		}


		public Domain.PricingUnit GetResult()
		{
			Domain.PricingUnit result = null;

			try
			{
				if	(fareDelegateASR.IsCompleted)
				{
					success = true;
					result = (Domain.PricingUnit)fareDelegate.EndInvoke(fareDelegateASR);
				}
				else
				{
					Logger.Write(new OperationalEvent
						(TDEventCategory.Business,
						TDTraceLevel.Error,
						"FareSupplier call timed out",
						null,
						sessionInfo.SessionId));

					success = false;
				}
			}
			catch (Exception e)
			{
				OperationalEvent oe = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, "Exception after FareSupplier call", e, sessionInfo.SessionId);
				Logger.Write(oe);
				success = false;
			}

			return result;
		}

		/// <summary>
		/// Read Only. Returns success status.
		/// </summary>
		public bool IsSuccessful
		{
			get { return success; }
		}

		/// <summary>
		/// Read Only. Returns the index used in the fare call.
		/// </summary>
		public int Index
		{
			get { return index; }
		}
		
		/// <summary>
		/// Read Only. Returns if return was specified on call to FareSupplierAsyncDelegate.
		/// </summary>
		public bool IsReturn
		{
			get { return isReturn; }
		}
	}

	/// <summary>
	/// PricingCall class - Asynchronous caller which can be invoked on each PricingRetailOptionsState to call
	/// the PriceOptionsArrayElement method in the TimeBasedPriceSupplierCaller
	/// </summary>
	class PricingCall
	{
		// Pricing Delegate used for aysnchronous calls
		private delegate AsyncCallStatus PriceItineraryDelegate(CJPSessionInfo sessionInfo, TDSessionPartition partition, int pricingArrayIndex);

        private CJPSessionInfo sessionInfo;
		private TDSessionPartition partition;
		private int pricingArrayIndex;

		private bool success = false;
		private TimeBasedPriceSupplierCaller priceItinerary;
		private PriceItineraryDelegate pricingDelegate = null;
		private IAsyncResult pricingDelegateASR = null;

		/// <summary>
		/// Constructor class for the Aysnchronous Pricing Call object. Sets up approrpiate input info
		/// </summary>
		/// <param name="sessionInfo"></param>
		/// <param name="partition"></param>
		/// <param name="pricingArrayIndex"></param>
		public PricingCall(CJPSessionInfo sessionInfo, TDSessionPartition partition, int pricingArrayIndex)
		{
			this.sessionInfo = sessionInfo;
			this.partition = partition;
			this.pricingArrayIndex = pricingArrayIndex;
		}

		/// <summary>
		/// Invokes the Asynchronous method. Calls PriceOptionsArrayElement in the 
		/// </summary>
		/// <returns></returns>
		public WaitHandle InvokePricing()
		{
			try
			{
				priceItinerary = new TimeBasedPriceSupplierCaller();
				// Invoke TimeBasedPriceSupplierCaller.PriceOptionsArrayElement method
				pricingDelegate = new PriceItineraryDelegate(priceItinerary.PriceOptionsArrayElement);
				pricingDelegateASR = pricingDelegate.BeginInvoke(sessionInfo, partition, pricingArrayIndex, null, null);
				// Return wait handle
				return pricingDelegateASR.AsyncWaitHandle;
			}
			catch (Exception e)
			{
				OperationalEvent oe = new OperationalEvent
					(TDEventCategory.PricingCall, TDTraceLevel.Error, "Exception on Pricing Call", e, sessionInfo.SessionId);
				Logger.Write(oe);
				return null;
			}
		}

		/// <summary>
		/// Method to determine if the Asynchronous call was successful
		/// </summary>
		/// <returns></returns>
		public AsyncCallStatus GetResult()
		{
			try
			{
				// Determine if Pricing Delegate completed successfully
				if (pricingDelegateASR.IsCompleted)
				{
					success = true;
					// Return end invoke call status result
					return (AsyncCallStatus) pricingDelegate.EndInvoke(pricingDelegateASR);
				}
				else
				{
					// if unsuccessful - log as timed out
					Logger.Write(new OperationalEvent
						(TDEventCategory.PricingCall,
						TDTraceLevel.Error,
						"PricingCall timed out",
						null,
						sessionInfo.SessionId));

					success = false;
					return AsyncCallStatus.TimedOut;
				}
			}
			catch (Exception e)
			{
				OperationalEvent oe = new OperationalEvent
					(TDEventCategory.PricingCall, TDTraceLevel.Error, "Exception after Pricing Call", e, sessionInfo.SessionId);
				Logger.Write(oe);
				success = false;
				return AsyncCallStatus.None;
			}
		}

		/// <summary>
		/// Property to determine if the Asynchronous call was successful.
		/// </summary>
		public bool IsSuccessful
		{
			get { return success; }
		}
	}
}
