// ***********************************************
// NAME 		: JourneyPlanStateData.cs
// AUTHOR 		: Jonathan George
// DATE CREATED : 14/10/2004
// DESCRIPTION 	: Holds data relating to the state of a call to the CJP
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/JourneyPlanStateData.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:32   mturner
//Initial revision.
//
//   Rev 1.2   Nov 01 2005 15:12:16   build
//Automatically merged from branch for stream2638
//
//   Rev 1.1.1.0   Aug 31 2005 15:38:36   jbroome
//Added new WaitPageTimeOut property
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1   Jan 26 2005 15:53:06   PNorell
//Support for partitioning the session information.
//
//   Rev 1.0   Oct 15 2004 12:26:34   jgeorge
//Initial revision.

using System;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Holds data relating to the state of a call to the CJP.
	/// This class should always be deferred to the database as soon as it is changed.
	/// This will avoid problems with the results being overwritten.
	/// </summary>
	[Serializable()]
	public class JourneyPlanStateData : ITDSessionAware
	{

		#region Private variables
		// using DateTime rather than TDDateTime here because a fixed
		//  "Now" would mean that the expiry would never occur ...
		
		/// <summary>
		/// The time at which the current CJP call should be considered to have timed out.
		/// Set by adding a value from the properties table to the time at which the status
		/// is set to InProgress.
		/// Only accessible indirectly through the IsTimeoutExpired property.
		/// <seealso cref="IsTimeoutExpired"/>
		/// </summary>
		private DateTime expiryDateTime;

		/// <summary>
		/// The status of the current CJP call
		/// </summary>
		private CJPCallStatus status;

		/// <summary>
		/// The request ID of this call to the CJP
		/// </summary>
		private Guid cjpRequestID;

		/// <summary>
		/// Keeps track if the state data has changed or not.
		/// As default it presumes it has been changed.
		/// </summary>
		private bool dirty = true;

		/// <summary>
		/// Stores a non-default wait page timeout period in seconds
		/// </summary>
		private int waitPageTimeout = 0;

		#endregion

		#region Constructor/destructor

		/// <summary>
		/// Default constructor. Does nothing
		/// </summary>
		public JourneyPlanStateData()
		{
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Read-only property
		/// Returns true if the CJP call should be considered to have timed out.
		/// This is determined by the expiryDateTime property.
		/// </summary>
		public bool IsTimeOutExpired
		{
			get { return expiryDateTime < DateTime.Now; }
		}

		/// <summary>
		/// Read/write property
		/// ID of the current CJP request. Used for matching up request with results
		/// </summary>
		public Guid CJPRequestID
		{
			get
			{
				return cjpRequestID;
			}
			set
			{
				cjpRequestID = value;
				dirty = true;
			}
		}

		/// <summary>
		/// Read/write property
		/// Status of the current call. When set to InProgress, the expiryDateTime private variable
		/// is set, which allows the IsTimeOutExpired property to be used correctly.
		/// </summary>
		public CJPCallStatus Status
		{
			get
			{
				return status;
			}
			set
			{
				status = value;

				if	(status == CJPCallStatus.InProgess)
				{
					// If WaitPageTimeout has not been set, then use default value
					int secs = (waitPageTimeout == 0) ? Int32.Parse(Properties.Current["JourneyControl.WaitPageTimeoutSeconds"]) : waitPageTimeout;
					expiryDateTime = DateTime.Now + new TimeSpan(0, 0, secs);  
				}

				dirty = true;
			}
		}
        
		/// <summary>
		/// Public Read/write Boolean property.
		/// Keeps track if the state data has changed or not.
		/// As default it presumes it has been changed.
		/// </summary>
		public bool IsDirty
		{
			get
			{
				return dirty;
			}
			set
			{
				dirty = value;
			}
		}

        /// <summary>
        /// Read-write int property
        /// Stores a non-default wait page 
        /// time out period in seconds
        /// </summary>
		public int WaitPageTimeout
		{
			get 
			{
				return waitPageTimeout; 
			}
			set 
			{ 
				waitPageTimeout = value; 
			}
		}
		
		#endregion
	}

}
