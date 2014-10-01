// *********************************************** 
// NAME         : AsyncCallState.cs
// AUTHOR       : Jonathan George
// DATE CREATED : 10/10/2005
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Common/AsyncCallState.cs-arc  $ 
//
//   Rev 1.1   Dec 05 2007 15:02:22   build
//Updated to remove warning from Nant build
//
//   Rev 1.0   Nov 08 2007 12:19:00   mturner
//Initial revision.
//
//   Rev 1.1   Apr 05 2006 15:43:02   build
//Automatically merged from branch for stream0030
//
//   Rev 1.0.1.0   Mar 29 2006 11:10:34   RGriffith
//Wait Page Ehancement changes
//Resolution for 33: DEL 8.1 Workstream: Wait Pages
//
//   Rev 1.0   Oct 21 2005 18:01:04   jgeorge
//Initial revision.
//
//   Rev 1.0   Oct 14 2005 15:13:38   jgeorge
//Initial revision.

using System;
using TransportDirect.Common;

namespace TransportDirect.Common
{
	/// <summary>
	/// Base class for classes which are used to control asynchronous calls along with the Wait page.
	/// </summary>
	[Serializable]
	public abstract class AsyncCallState : ITDSessionAware
	{
		#region Private variables

		/// <summary>
		/// The time at which the current CJP call should be considered to have timed out.
		/// Set by adding a value from the properties table to the time at which the status
		/// is set to InProgress.
		/// Only accessible indirectly through the IsTimeoutExpired property.
		/// Using DateTime rather than TDDateTime here because a fixed
		///  "Now" would mean that the expiry would never occur ...
		/// </summary>
		private DateTime expiryDateTime;

		/// <summary>
		/// The status of the current call
		/// </summary>
		private AsyncCallStatus status;

		/// <summary>
		/// The request ID of this call
		/// </summary>
		private Guid requestID;

		/// <summary>
		/// Keeps track if the state data has changed or not. As default it presumes it has been changed.
		/// </summary>
		private bool dirty = true;

		/// <summary>
		/// The page to direct to if validation fails
		/// </summary>
		private PageId ambiguityPage;

		/// <summary>
		/// The page to direct to if an error occurs
		/// </summary>
		private PageId errorPage;

		/// <summary>
		/// The results page to use
		/// </summary>
		private PageId destinationPage;

		/// <summary>
		/// Stores a non-default wait page timeout period in seconds
		/// </summary>
		private int waitPageTimeout = 0;

		/// <summary>
		/// Stores a non-default wait page refresh interval in seconds
		/// </summary>
		private int waitPageRefreshInterval = 0;

		/// <summary>
		/// Stores the file name of the wait page message resource file (eg: "langstrings")
		/// </summary>
		private string waitPageMessageResourceFile = "";

		/// <summary>
		/// Stores the resource identifier for the wait page message to be used
		/// </summary>
		private string waitPageMessageResourceId = "";

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="waitPageTimeout">Number of seconds after which the wait page should invoke the timeout processing</param>
		public AsyncCallState(int waitPageTimeout)
		{
			this.waitPageTimeout = waitPageTimeout;
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Read only. Returns true if the CJP call should be considered to have timed out.
		/// This is determined by the expiryDateTime property.
		/// </summary>
		public bool IsTimeOutExpired
		{
			get { return expiryDateTime < DateTime.Now; }
		}

		/// <summary>
		/// Read/write. ID of the current CJP request. Used for matching up request with results
		/// </summary>
		public Guid RequestID
		{
			get { return requestID; }
			set
			{
				requestID = value;
				dirty = true;
			}
		}

		/// <summary>
		/// Read/write. Status of the current call. When set to InProgress, the expiryDateTime private 
		/// variable is set, which allows the IsTimeOutExpired property to be used correctly.
		/// </summary>
		public AsyncCallStatus Status
		{
			get { return status; }
			set
			{
				status = value;

				if	(status == AsyncCallStatus.InProgress)
					expiryDateTime = DateTime.Now + new TimeSpan(0, 0, waitPageTimeout);  

				dirty = true;
			}
		}

		/// <summary>
		/// Read/write. The page to direct to if validation fails
		/// </summary>
		public PageId AmbiguityPage
		{
			get { return ambiguityPage; }
			set 
			{ 
				ambiguityPage = value; 
				dirty = true;
			}
		}

		/// <summary>
		/// Read/write. The page to direct to if an error occurs
		/// </summary>
		public PageId ErrorPage
		{
			get { return errorPage; }
			set 
			{
				errorPage = value;
				dirty = true;
			}
		}

		/// <summary>
		/// Read/write. The results page to use
		/// </summary>
		public PageId DestinationPage
		{
			get { return destinationPage; }
			set 
			{
				destinationPage = value; 
				dirty = true;
			}
		}

		
		/// <summary>
		/// Read/write. Stores a non-default wait page time out period in seconds
		/// </summary>
		public int WaitPageTimeout
		{
			get { return waitPageTimeout;  }
			set  
			{ 
				waitPageTimeout = value; 
				dirty = true;
			}
		}

		/// <summary>
		/// Read/write. Stores a non-default wait page refresh interval in seconds
		/// </summary>
		public int WaitPageRefreshInterval
		{
			get { return waitPageRefreshInterval; }
			set { waitPageRefreshInterval = value; }
		}

		/// <summary>
		/// Read/write. Returns true if the object has changed since last time IsDirty was set to false.
		/// </summary>
		public bool IsDirty
		{
			get { return dirty; }
			set { dirty = value; }
		}

		/// <summary>
		/// Read/write. Stores the file name of the wait page message resource file (eg: "langstrings")
		/// </summary>
		public string WaitPageMessageResourceFile
		{
			get { return waitPageMessageResourceFile; }
			set { waitPageMessageResourceFile = value; }
		}

		/// <summary>
		/// Read/write. Stores the resource identifier for the wait page message to be used
		/// </summary>
		public string WaitPageMessageResourceId
		{
			get { return waitPageMessageResourceId; }
			set { waitPageMessageResourceId = value; }
		}
		#endregion

		#region Methods

		/// <summary>
		/// Does any processing required for the current state and returns the PageId to transfer
		/// the user to. For example, if the current state is CompletedOK, some processing might take
		/// place followed by a transfer to a results page. Subclasses must implement this method.
		/// </summary>
		/// <returns>The page to which the user should be transferred.</returns>
		public abstract PageId ProcessState();

		#endregion
	}
}
