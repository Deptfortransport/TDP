// *********************************************** 
// NAME                 : StopEventManager.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 05/01/2005
// DESCRIPTION  : Actual Implementation of IStopEventManager interface
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/StopEventManager/StopEventManager.cs-arc  $
//
//   Rev 1.1   Feb 17 2010 16:42:26   RHopkins
//Pass OperatorCode in StopEvent request
//Resolution for 5403: Pass OperatorCode in StopEvents request
//
//   Rev 1.0   Nov 08 2007 12:21:44   mturner
//Initial revision.
//
//   Rev 1.3   Mar 15 2005 15:03:30   schand
//Fixed SortCJPResult
//
//   Rev 1.2   Mar 15 2005 13:52:24   schand
//Added SortCJPResult() to sort CJP result by datetime
//
//   Rev 1.1   Mar 01 2005 10:26:16   passuied
//added/ fixed logging!
//
//   Rev 1.0   Feb 28 2005 16:23:58   passuied
//Initial revision.
//
//   Rev 1.9   Feb 23 2005 15:17:24   passuied
//added sorting for a results for LastService request
//
//   Rev 1.8   Feb 16 2005 14:54:20   passuied
//Change in interface and behaviour of time Request
//
//possibility to plan in the past within configurable time window
//
//   Rev 1.7   Feb 11 2005 11:08:10   passuied
//changes to comply to the new cjp
//
//   Rev 1.6   Feb 02 2005 18:37:46   passuied
//added logging for StopEventRequest
//
//   Rev 1.5   Jan 19 2005 14:01:48   passuied
//added more validation + changed UT to allow destination to be optional
//
//   Rev 1.4   Jan 14 2005 10:21:36   passuied
//changes in interface
//
//   Rev 1.3   Jan 12 2005 14:52:10   passuied
//changes related to new MessageId enum
//
//   Rev 1.2   Jan 11 2005 16:33:34   passuied
//changes regarding calling stops
//
//   Rev 1.1   Jan 11 2005 13:42:12   passuied
//backed up version
//
//   Rev 1.0   Jan 05 2005 16:52:16   passuied
//Initial revision.

using System;
using System.Collections;
using System.Threading;
using System.Globalization;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.ReportDataProvider.TDPCustomEvents;

namespace TransportDirect.UserPortal.DepartureBoardService.StopEventManager
{
	/// <summary>
	/// Actual Implementation of IStopEventManager interface
	/// </summary>
	public class StopEventManager : IStopEventManager
	{	private bool isDeparture ; // class varaible to store arrival/departure 

		/// <summary>
		/// Default constructor
		/// </summary>
		public StopEventManager()
		{
		}

		
		#region Public Methods
		/// <summary>
		/// Trip level request method for DepartureBoard information
		/// </summary>
		/// <param name="token">Authentications token</param>
		/// <param name="originLocation">naptanId of Origin</param>
		/// <param name="destinationLocation">naptanId of Destination</param>
		/// <param name="type">type of DepartureBoard requested</param>
		/// <param name="serviceNumber"> requested service number (optional)</param>
		/// <param name="time"> start time</param>
		/// <param name="rangeType">type of the range for the request (sequence, interval)</param>
		/// <param name="range"> max number of returned results</param>
		/// <param name="showDepartures"> show departure times / show arrival times</param>
		/// <param name="showCallingStops">show calling stops if available</param>
		/// <returns></returns>
		public DBSResult GetDepartureBoardTrip(
			DBSLocation originLocation,
			DBSLocation destinationLocation,
            string operatorCode,
            string serviceNumber,
			DBSTimeRequest time,
			DBSRangeType rangeType,
			int range,
			bool showDepartures,
			bool showCallingStops)
		{
            EventRequest request = null;
			StopEventResult result = null;

			// store arrival/departure information 
			isDeparture =  showDepartures ;

			// Make a special request if optional destinationLocation is null
			// build a SER with no location filter with origin always populated:
			// Build the request with originLocation as Requested stop if showDepartures is true
			// use destination as a filter
			if (showDepartures || destinationLocation == null)
			{
				request = StopEventConverter.BuildStopEventRequest(
					originLocation,
                    operatorCode,
					serviceNumber, 
					showDepartures, 
					showCallingStops, 
					destinationLocation, 
					time,
					rangeType,
					range);
			}
			else
			{
				request = StopEventConverter.BuildStopEventRequest(
					destinationLocation,
                    operatorCode,
					serviceNumber, 
					showDepartures, 
					showCallingStops, 
					originLocation, 
					time,
					rangeType,
					range);
			}

			try
			{
				result = CallCJP( request);
			}
				// if exception thrown, create DBSMessage(s) and Build a DBSResult without stopEvents
			catch (TDException tde)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Business,
					TDTraceLevel.Error,
					string.Format(Messages.CallCJPException, request.requestID, tde.Message));

				Logger.Write(oe);

				DBSMessage message = new DBSMessage();
				if (tde.Identifier == TDExceptionIdentifier.DBSCJPTimeout)
				{
					message.Code = (int)DBSMessageIdentifier.CJPTimeout;
					message.Description = tde.Message;
				}

				// add conditions if more identifiers


				return StopEventConverter.BuildDBSResult(new DBSMessage[]{message});

			}
			catch (Exception e)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Business,
					TDTraceLevel.Error,
					string.Format(Messages.CallCJPException, request.requestID, e.Message));

				Logger.Write(oe);

				DBSMessage message = new DBSMessage();
				message.Code = (int)DBSMessageIdentifier.CJPCallException;
				message.Description = string.Format(Messages.CallCJPException, request.requestID, e.Message);

				return StopEventConverter.BuildDBSResult(new DBSMessage[]{message});

			
			}
			
			// If result is null, log it and create message
			if (result == null)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Business,
					TDTraceLevel.Error,
					string.Format(Messages.CallCJPNull, request.requestID));

				Logger.Write(oe);

				DBSMessage message = new DBSMessage();
				message.Code = (int)DBSMessageIdentifier.CJPAccessNullResult; // see Message codes spreadsheet for detail
				message.Description = string.Format(Messages.CallCJPNull, request.requestID);

				return StopEventConverter.BuildDBSResult(new DBSMessage[]{message});
			}
			else
			{
				DBSResult dbsResult = StopEventConverter.BuildDBSResult(result, showCallingStops);

				// If we had a Last Service request then sort the results in ascending order
				if (time.Type == TimeRequestType.Last)
				{
					ArrayList listStopEvents = new ArrayList(dbsResult.StopEvents);
					listStopEvents.Sort(new DBSStopEventComparer(showDepartures));
					dbsResult.StopEvents = (DBSStopEvent[])listStopEvents.ToArray(typeof(DBSStopEvent));
				}
				return dbsResult;
			}
			

		}

		/// <summary>
		/// Stop level request method for DepartureBoard Information
		/// </summary>
		/// <param name="token">Authentication token</param>
		/// <param name="stopNaptan">NaptanId of the stop</param>
		/// <param name="type">type of departure board requested</param>
		/// <param name="serviceNumber"> service number requested (optional)</param>
		/// <param name="showDepartures"> show departure times / arrival times</param>
		/// <param name="showCallingStops"> show calling stops if available</param>
		/// <returns></returns>
		public DBSResult GetDepartureBoardStop(
			DBSLocation stopLocation,
            string operatorCode,
            string serviceNumber,
            bool showDepartures,
			bool showCallingStops)
		{
			
			// store arrival/departure information 
			isDeparture =  showDepartures ;

			StopEventResult result = null;
			EventRequest request = StopEventConverter.BuildStopEventRequest(
															stopLocation,
                                                            operatorCode,
															serviceNumber, 
															showDepartures, 
															showCallingStops);

			try
			{
				result = CallCJP(request);
			}
				// if exception thrown, create DBSMessage(s) and Build a DBSResult without stopEvents
			catch (TDException tde)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Business,
					TDTraceLevel.Error,
					string.Format(Messages.CallCJPException, request.requestID, tde.Message));

				Logger.Write(oe);

				DBSMessage message = new DBSMessage();
				if (tde.Identifier == TDExceptionIdentifier.DBSCJPTimeout)
				{
					message.Code = (int)DBSMessageIdentifier.CJPTimeout;
					message.Description = tde.Message;
				}

				// add conditions if more identifiers


				return StopEventConverter.BuildDBSResult(new DBSMessage[]{message});

			}
			catch (Exception e)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Business,
					TDTraceLevel.Error,
					string.Format(Messages.CallCJPException, request.requestID, e.Message));

				Logger.Write(oe);

				DBSMessage message = new DBSMessage();
				message.Code = (int)DBSMessageIdentifier.CJPCallException; // see Message codes spreadsheet for detail
				message.Description = string.Format(Messages.CallCJPException, request.requestID, e.Message);

				return StopEventConverter.BuildDBSResult(new DBSMessage[]{message});

			
			}

			// If result is null, log it and create message
			if (result == null)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Business,
					TDTraceLevel.Error,
					string.Format(Messages.CallCJPNull, request.requestID));

				Logger.Write(oe);

				DBSMessage message = new DBSMessage();
				message.Code = (int)DBSMessageIdentifier.CJPAccessNullResult; // see Message codes spreadsheet for detail
				message.Description = string.Format(Messages.CallCJPNull, request.requestID);

				return StopEventConverter.BuildDBSResult(new DBSMessage[]{message});
			}
			else
				return StopEventConverter.BuildDBSResult(result, showCallingStops);
		}

		#endregion


		#region CJP handling Methods
		/// <summary>
		/// Method that submit a request to the CJP asynchronously and wait for it return if less than 
		/// timeout
		/// </summary>
		/// <param name="request">StopEvent request to submit</param>
		/// <returns>StopEventResult object returned by CJP</returns>
		private StopEventResult CallCJP ( EventRequest request)
		{
			// Create a CJPStopEvent call object that will handle the async call.
			CJPStopEventCall cjpCall = new CJPStopEventCall(request);
		
			 
			// variables for logging
			DateTime submitted = DateTime.Now;

			// Invoke CJP
			WaitHandle wh = cjpCall.InvokeCJP();

			// Get timeout from properties
			string sTimeOut = Properties.Current[Keys.CJPTimeOutKey];
			int timeOut = 30000; // default timeout set to 30000 ms
			
			// if property not in Db, raise warning
			if (sTimeOut.Length != 0)
			{
				timeOut = Convert.ToInt32(sTimeOut, CultureInfo.InvariantCulture.NumberFormat);
			}
			else
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Infrastructure,
					TDTraceLevel.Warning,
					string.Format(Messages.MissingProperty,Keys.CJPTimeOutKey));
				Logger.Write(oe);
			}

			// Wait till it returns or until time out reached.
			wh.WaitOne(timeOut, false);
		
			// determins which type is the request for logging
			bool isFirstLastServiceRequest = (request is FirstLastServiceRequest);
			StopEventRequestType type = isFirstLastServiceRequest? StopEventRequestType.First : StopEventRequestType.Time;

			if (isFirstLastServiceRequest)
			{
				FirstLastServiceRequest flReq = (FirstLastServiceRequest)request;
				type = (flReq.type == FirstLastServiceRequestType.First)? StopEventRequestType.First : StopEventRequestType.Last;
			}

			
							

			// If successful return result, otherwise throw exception (CJPTimeout)
			StopEventResult result = cjpCall.GetResult();
			
			Logger.Write( new StopEventRequestEvent(
				request.requestID,
				submitted,
				type,
				cjpCall.IsSuccessful));

			if (cjpCall.IsSuccessful)
			{
                SortCJPResult(result);
				return result;
			}
				
			else 
			{
                OperationalEvent oe = new OperationalEvent(
					TDEventCategory.ThirdParty,
					TDTraceLevel.Error,
					string.Format(Messages.CJPTimeout, request.requestID));

				Logger.Write(oe);

				throw new TDException(string.Format(Messages.CJPTimeout, request.requestID),true, TDExceptionIdentifier.DBSCJPTimeout); 
			}

		}
		/// <summary>
		/// Method to sort the result by arrival/departure time
		/// </summary>
		/// <param name="sr">StopEventResult which  contains collection of StopEvent</param>
		private void SortCJPResult(StopEventResult sr)
		{	
			try
			{
				if (sr.stopEvents != null && sr.stopEvents.Length != 0 )
				{	
					//Array resultArray= new Array(sr.stopEvents.Length)  ;
					Array resultArray= Array.CreateInstance(typeof(StopEvent), sr.stopEvents.Length);    
					sr.stopEvents.CopyTo(resultArray,0);
					Array.Sort(resultArray, new StopEventDateComparer(isDeparture));   
					sr.stopEvents = (StopEvent[])resultArray; 				 
				}
			}
			catch(ArgumentException aEx)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.ThirdParty,
					TDTraceLevel.Error, Messages.StopEventArgumentException +  "  " + aEx.Message );

				Logger.Write(oe);
			}
		}
		#endregion

	}

}
