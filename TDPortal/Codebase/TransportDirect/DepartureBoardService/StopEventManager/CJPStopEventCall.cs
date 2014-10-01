// *********************************************** 
// NAME                 : CJPStopEventCall.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 05/01/2005
// DESCRIPTION  : Class responsible for calling the CJP
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/StopEventManager/CJPStopEventCall.cs-arc  $
//
//   Rev 1.1   Mar 23 2010 10:51:04   apatel
//Updated GetResult() method, so when the cjp result come with only messages the code doesn't throw invalid cast exception. Instead it makes empyt stopeventresult object with messages returned from cjp result object
//Resolution for 5477: StopEvent Result invalid cast exception
//
//   Rev 1.0   Nov 08 2007 12:21:44   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:23:56   passuied
//Initial revision.
//
//   Rev 1.4   Feb 24 2005 14:19:56   passuied
//Changes for FxCop
//
//   Rev 1.3   Feb 11 2005 11:27:02   passuied
//used value of exception in Invoke CJP try catch.
//
//   Rev 1.2   Feb 11 2005 11:08:18   passuied
//changes to comply to the new cjp
//
//   Rev 1.1   Jan 11 2005 13:40:38   passuied
//backed up version
//
//   Rev 1.0   Jan 05 2005 16:52:00   passuied
//Initial revision.

using System;
using System.Threading;

using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.JourneyPlanning.CJP;
using System.Text;

namespace TransportDirect.UserPortal.DepartureBoardService.StopEventManager
{
	/// <summary>
	/// Class responsible for calling the CJP
	/// </summary>
	public class CJPStopEventCall
	{
		private delegate CJPResult StopEventAsyncDelegate ( CJPRequest request);
		
		private EventRequest eventRequest = null;
		private StopEventAsyncDelegate cjpDelegate = null;
		private IAsyncResult asyncResult = null;
		private bool success = false;

		/// <summary>
		/// Default Constructor
		/// </summary>
		/// <param name="request">request to submit</param>
		public CJPStopEventCall(EventRequest request)
		{
			eventRequest = request;
		}

		/// <summary>
		/// Read only Property. Indicates wether call was successful or not.
		/// </summary>
		public bool IsSuccessful
		{
			get{ return success;}
		}

		/// <summary>
		/// Methods that invokes the CJP asynchronously and return the WaitHandle associated with the call.
		/// </summary>
		/// <returns>WaitHandle associated with the call</returns>
		public WaitHandle InvokeCJP()
		{
			try
			{
				object obj = TDServiceDiscovery.Current[ServiceDiscoveryKey.Cjp];
				ICJP cjp = (ICJP) obj;
				cjpDelegate = new StopEventAsyncDelegate(cjp.JourneyPlan);
				asyncResult = cjpDelegate.BeginInvoke(eventRequest, null, null);
				return asyncResult.AsyncWaitHandle;
			}
			catch (Exception e)
			{
				OperationalEvent oe = new OperationalEvent
					(TDEventCategory.CJP, TDTraceLevel.Error, eventRequest.requestID + " Exception on CJP call: " +e.Message);
				Logger.Write(oe);
				return null;
			}
		}

		/// <summary>
		/// Method that returns the StopEventResult associated with the CJP call.
		/// </summary>
		/// <returns>StopEventResult object returned by CJP. null if error!</returns>
		public StopEventResult GetResult()
		{
			try
			{
				if	(asyncResult.IsCompleted)
				{
                    success = true;
                    CJPResult result = cjpDelegate.EndInvoke(asyncResult);
                    if (result != null && result is StopEventResult)
                    {
                       
                        return (StopEventResult)result;
                    }
                    
                    if (result.messages.Length > 0)
                    {
                        StringBuilder messageBuilder = new StringBuilder();

                        foreach (Message m in result.messages)
                        {
                            messageBuilder.AppendFormat("{0}:{1} \t", m.code, m.description);
                        }
                        OperationalEvent oe = new OperationalEvent
                            (TDEventCategory.CJP, TDTraceLevel.Warning, eventRequest.requestID + " CJP call come back with messages : " + messageBuilder.ToString());
                        
                        Logger.Write(oe);

                        StopEventResult seResult = new StopEventResult();
                        seResult.messages = result.messages;

                        success = true;
                        return seResult;
                    }

                    success = false;
                    return null;
				}
				else
				{
					Logger.Write(new OperationalEvent
						(TDEventCategory.CJP,
						TDTraceLevel.Error,
						eventRequest.requestID + " CJP call timed out"));

					success = false;
					return null;
				}
			}
			catch (ArgumentException)
			{
				OperationalEvent oe = new OperationalEvent
					(TDEventCategory.CJP, TDTraceLevel.Error, eventRequest.requestID +  " Exception after CJP call");
				Logger.Write(oe);
				success = false;
				return null;
			}
            catch (Exception ex) // log a generic exceptions
            {
                OperationalEvent oe = new OperationalEvent
                    (TDEventCategory.CJP, TDTraceLevel.Error, eventRequest.requestID + " Exception after CJP call. StackTrace: " + ex.StackTrace );
                Logger.Write(oe);
                success = false;
                return null;
            }
		}
	}
}
