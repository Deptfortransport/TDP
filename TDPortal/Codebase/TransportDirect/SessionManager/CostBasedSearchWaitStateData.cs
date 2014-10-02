// *********************************************** 
// NAME         : CostBasedSearchWaitStateData.cs
// AUTHOR       : Tim Mollart
// DATE CREATED : 22/12/2004
// DESCRIPTION  : Class for cost based search wait state data.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/CostBasedSearchWaitStateData.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:48:20   mturner
//Initial revision.
//
//   Rev 1.8   Mar 30 2005 15:58:20   jmorrissey
//Fixed bug with ExpiryDateTime not working
//
//   Rev 1.7   Mar 15 2005 08:40:52   tmollart
//Implemented CostSearchWaiteControlData and CostSearchWaitStateData properties.
//
//   Rev 1.6   Mar 08 2005 13:23:48   rscott
//DEL 7 - new method added IsTimeoutExpired
//
//   Rev 1.5   Feb 21 2005 14:42:46   jmorrissey
//Added requestID
//
//   Rev 1.4   Feb 14 2005 14:55:02   jmorrissey
//Updated to set expiryDateTime if search status is InProgress
//
//   Rev 1.3   Jan 19 2005 12:03:02   jmorrissey
//Added Operation property which indicates which CostSearchRunner method has been called.
//
//   Rev 1.2   Jan 17 2005 15:52:48   tmollart
//Added [Serializabe] directive.
//
//   Rev 1.1   Jan 06 2005 16:01:54   jmorrissey
//Fixed naming problem with ExpiryDateTime property
//
//   Rev 1.0   Dec 22 2004 15:29:24   tmollart
//Initial revision.

using System;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Wait state data for cost based searches
	/// </summary>
	[CLSCompliant(false)]
	[Serializable]
	public class CostBasedSearchWaitStateData : TDSessionAware
	{

		#region Private Member Declaration

		//the expiry time of the current cost search result in the session
		private DateTime expiryDateTime;

		//the status of the current cost search request
		private CostBasedSearchStatus status;

		//the operation invoked for the current cost search i.e. fares or services search
		private CostSearchOperation operation;

		/// <summary>
		/// Returns true if the expiryDateTime less than DateTime.Now.
		/// This is determined by the expiryDateTime property.
		/// </summary>
		public bool IsTimeOutExpired
		{
			get { return expiryDateTime < DateTime.Now; }
		}

		/// <summary>
		/// The request ID of this cost search
		/// </summary>
		private Guid requestID;

		#endregion
				
		public CostBasedSearchWaitStateData()
		{
		}
		
		#region Public Properties

		/// <summary>
		/// Read/Write Property. Gets/Sets Expiry date time
		/// </summary>
		public DateTime ExpiryDateTime
		{
			get {return expiryDateTime;}
			set 
			{
				expiryDateTime = value;
				IsDirty = true;
			}
		}

		/// <summary>
		/// Read/Write Property. 
		/// Status of the current call. When set to InProgress, the expiryDateTime private variable
		/// is set, which allows the IsTimeOutExpired property to be used correctly.
		/// </summary>
		public CostBasedSearchStatus Status
		{
			get {return status;}
			set 
			{
				status = value;

				if	(status == CostBasedSearchStatus.InProgress)
				{
					int secs = Int32.Parse(Properties.Current["CostSearch.WaitPageTimeoutSeconds"]);
					expiryDateTime = DateTime.Now.Add(new TimeSpan(0,0,secs));  
				}
				IsDirty = true;
			}
		}

		/// <summary>
		/// Read/Write Property. Gets/Sets operation
		/// </summary>
		public CostSearchOperation Operation
		{
			get {return operation;}
			set 
			{
				operation = value;
				IsDirty = true;
			}
		}		

		/// <summary>
		/// ID of the current cost search request. Used for matching up request with results
		/// </summary>
		public Guid RequestID
		{
			get
			{
				return requestID;
			}
			set
			{
				requestID = value;
				IsDirty = true;
			}
		}

		#endregion
	}
}
