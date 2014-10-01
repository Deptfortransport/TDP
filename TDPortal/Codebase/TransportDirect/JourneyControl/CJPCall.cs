// ***********************************************
// NAME         : CJPCall.cs
// AUTHOR       : Richard Philpott
// DATE CREATED : 2006-02-15
// DESCRIPTION  : Represents a single invocation 
//				  of the CJP by the Portal.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/CJPCall.cs-arc  $
//
//   Rev 1.1   Mar 10 2008 15:17:42   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:23:38   mturner
//Initial revision.
//
//   Rev 1.1   Feb 27 2006 12:17:32   RPhilpott
//Assign to IR 0018
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.0   Feb 27 2006 12:15:56   RPhilpott
//Initial revision.
//

using System;
using System.IO;
using System.Threading;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

using TransportDirect.JourneyPlanning.CJP;
using TransportDirect.JourneyPlanning.CJPInterface;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Represents a single invocation of the CJP by the Portal.	
	/// </summary>
	public class CJPCall
	{
		private delegate CJPResult JourneyPlanAsyncDelegate(CJPRequest request);

		private JourneyRequest request;
		private bool isReturn;
		private bool success;

		private int referenceNumber;
		private string sessionId;

		private JourneyPlanAsyncDelegate cjpDelegate;
		private IAsyncResult cjpDelegateASR;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="request">the JourneyRequest to be passed as input to the CJP</param>
		/// <param name="isReturn">true if for return journey, false for outward</param>
		/// <param name="referenceNumber">Used by CJP for logging</param>
		/// <param name="sessionId">Used by CJP for logging</param>
		public CJPCall(JourneyRequest request, bool isReturn, int referenceNumber, string sessionId)
		{
			this.request  = request;
			this.isReturn = isReturn;
			this.referenceNumber = referenceNumber;
			this.sessionId = sessionId;
		}

		/// <summary>
		/// Initiates an asynchronous call to the CJP itself.
		/// </summary>
		/// <returns>
		/// A WaitHandle that will be used to determine when the call 
		/// has completed, or null if the call generated an exception.
		/// </returns>
		public WaitHandle InvokeCJP()
		{
			try
			{
				ICJP cjp = (ICJP) TDServiceDiscovery.Current[ServiceDiscoveryKey.Cjp];
				cjpDelegate = new JourneyPlanAsyncDelegate(cjp.JourneyPlan);
				cjpDelegateASR = cjpDelegate.BeginInvoke(request, null, null);
				return cjpDelegateASR.AsyncWaitHandle;
			}
			catch (Exception e)
			{
				OperationalEvent oe = new OperationalEvent
					(TDEventCategory.CJP, TDTraceLevel.Error, request.requestID + " Exception on CJP call", e, sessionId);
				Logger.Write(oe);
				return null;
			}
		}

		/// <summary>
		/// Read-only - the result of the cjp call (or null if not sucessful)
		/// </summary>
		public JourneyResult CJPResult
		{
			get
			{
				try
				{
					if  (cjpDelegateASR.IsCompleted)
					{
						success = true;
                        return (JourneyResult)cjpDelegate.EndInvoke(cjpDelegateASR);
					}
					else
					{
						Logger.Write(new OperationalEvent
							(TDEventCategory.CJP,
							TDTraceLevel.Error,
							request.requestID + " CJP call timed out",
							null,
							sessionId));

						success = false;
						return null;
					}
				}
				catch (Exception e)
				{
					OperationalEvent oe = new OperationalEvent
						(TDEventCategory.CJP, TDTraceLevel.Error, request.requestID +  " Exception after CJP call", e, sessionId);
					Logger.Write(oe);
					success = false;
					return null;
				}

			}
		}

		/// <summary>
		/// Read-only - the JourneyRequest used to call the CJP  
		/// </summary>
		public JourneyRequest CJPRequest
		{
			get { return request; }
		}

		/// <summary>
		/// Read-only - true if request was for return journey, false if outward 
		/// </summary>
		public bool IsReturnJourney
		{
			get { return isReturn; }
		}

		/// <summary>
		/// Read-only - true if CJP call was successful
		/// </summary>
		public bool IsSuccessful
		{
			get { return success; }
		}

		/// <summary>
		/// Read-only - the request-id 
		/// Portal query reference, plus 4-digit suffix to make unique per-request
		/// </summary>
		public string RequestId
		{
			get { return request.requestID; }
		}
	}
}
