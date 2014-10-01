// *********************************************** 
// NAME                 : CJPCall.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 19/04/2010
// DESCRIPTION          : Class which invokes the call to the CJP
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerCaller/CJPCall.cs-arc  $ 
//
//   Rev 1.0   Apr 20 2010 16:39:22   mmodi
//Initial revision.
//Resolution for 5515: Utility - Application to send request directly to CJP
//
//   Rev 1.0   Apr 19 2010 15:17:04   mmodi
//Initial revision.
//Resolution for 5515: Utility - Application to send request directly to CJP

using System;
using System.Collections;
using System.IO;
using System.Threading;
using TransportDirect.JourneyPlanning.CJP;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace JourneyPlannerCaller
{
	/// <summary>
    /// Class which invokes the call to the CJP
	/// </summary>
	public class CJPCall
    {
        #region Private members

        private delegate CJPResult JourneyPlanAsyncDelegate(CJPRequest request);

		private JourneyRequest request;
		private bool success = false;

		private JourneyPlanAsyncDelegate cjpDelegate;
		private IAsyncResult cjpDelegateASR;

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="request">the JourneyRequest to be passed as input to the CJP</param>
		public CJPCall(JourneyRequest request)
		{
			this.request  = request;
        }

        #endregion

        #region Public methods

        /// <summary>
		/// Initiates an asynchronous call to the CJP itself.
		/// </summary>
		/// <returns>
		/// A WaitHandle that will be used to determine when the call 
		/// has completed, or null if the call generated an exception.
		/// </returns>
		public WaitHandle InvokeCJP(ICJP cjp)
		{
			try
			{
				cjpDelegate = new JourneyPlanAsyncDelegate(cjp.JourneyPlan);
				cjpDelegateASR = cjpDelegate.BeginInvoke(request, null, null);
				
                return cjpDelegateASR.AsyncWaitHandle;
			}
			catch (Exception ex)
			{
                FileLogger.LogMessage(
                    string.Format("Exception on CJP call. RequestId[{0}]. \n Exception Message: {1} \n StackTrace {2}",
                    request.requestID, ex.Message, ex.StackTrace));
				
				return null;
			}
        }

        #endregion

        #region Public properties

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
                        FileLogger.LogMessage(
                            string.Format("CJP call timed out. RequestId[{0}].",
                            request.requestID));
                        						
						success = false;
						return null;
					}
				}
				catch (Exception ex)
				{
                    FileLogger.LogMessage(
                        string.Format("Exception after CJP call. RequestId[{0}]. \n Exception Message: {1} \n StackTrace {2}",
                        request.requestID, ex.Message, ex.StackTrace));

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
		/// Read-only - true if CJP call was successful
		/// </summary>
		public bool IsSuccessful
		{
			get { return success; }
		}

		/// <summary>
		/// Read-only - the request-id 
		/// </summary>
		public string RequestId
		{
			get { return request.requestID; }
        }

        #endregion
    }
}
