using System;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Summary description for TDSessionSerializableObjects.
	/// </summary>
	[Serializable(), CLSCompliant(false)]
	public class TDSessionSerializableObjects
	{
		// Deferrable Session objects

		/// <summary>
		/// _ItineraryManager is the Serializable version of ItineraryManager.
		/// </summary>
		private TDItineraryManager _ItineraryManager = null;

		/// <summary>
		/// _JourneyRequest is the Serializable version of JourneyRequest.
		/// </summary>
		private ITDJourneyRequest _JourneyRequest = null;

		/// <summary>
		/// _JourneyResult is the Serializable version of JourneyResult.
		/// </summary>
		private ITDJourneyResult _JourneyResult = null;

		/// <summary>
		/// _AJourneyResult is the Serializable version of JourneyResult.
		/// </summary>
		private ITDJourneyResult _AJourneyResult = null;

		/// <summary>
		/// _JourneyViewState is the Serializable version of JourneyViewState.
		/// </summary>
		private TDJourneyViewState _JourneyViewState = null;

		/// <summary>
		/// _JourneyAdjustState is the Serializable version of JourneyAdjustState.
		/// </summary>
		private TDCurrentAdjustState _JourneyAdjustState = null;

		/// <summary>
		/// _JourneyPlanControlData is the Serializable version of JourneyPlanControlData.
		/// </summary>
		private JourneyPlanControlData _JourneyPlanControlData = null;

		/// <summary>
		/// _JourneyPlanStateData is the Serializable version of JourneyPlanStateData.
		/// </summary>
		private JourneyPlanStateData _JourneyPlanStateData = null;

        /// <summary>
        /// _JourneyMapState is the Serializable version of JourneyMapState
        /// </summary>
        private JourneyMapState _JourneyMapState = null;

		/// <summary>
		/// _JourneyMapState is the Serializable version of JourneyMapState
		/// </summary>
		private JourneyMapState _OutwardJourneyMapState = null;

		/// <summary>
		/// SessionId of the session to which these objects belong
		/// </summary>
		private string _SessionID = string.Empty;

		public TDSessionSerializableObjects()
		{
			_SessionID = System.Web.HttpContext.Current.Session.SessionID.ToString();
		}

		public TDSessionSerializableObjects( string SessionID ) 
		{ 
			_SessionID = SessionID;
		}

		/// <summary>
		/// Read property gives string version of the session ID for these objects
		/// </summary>
		public string CurrentSessionID { get { return _SessionID; }	}

		/// <summary>
		/// Read/Write property gives access to a Serializable version of ItineraryManager
		/// </summary>
		public TDItineraryManager ItineraryManager
		{
			get { return _ItineraryManager; }
			set { _ItineraryManager = value; }
		}

		/// <summary>
		/// Read/Write property gives access to a Serializable version of JourneyRequest
		/// </summary>
		public ITDJourneyRequest DeferableJourneyRequest
		{
			get { return _JourneyRequest; }
			set { _JourneyRequest = value; }
		}

		/// <summary>
		/// Read/Write property gives access to a Serializable version of JourneyResult
		/// </summary>
		public ITDJourneyResult DeferableJourneyResult
		{
			get { return _JourneyResult; }
			set { _JourneyResult = value; }
		}

		/// <summary>
		/// Read/Write property gives access to a Serializable version of Itinerary JourneyResults
		/// </summary>
		public ITDJourneyResult ItineraryResult
		{
			get { return _ItineraryManager.JourneyResult; }
			set { _ItineraryManager.JourneyResult = value; }
		}

		/// <summary>
		/// Read/Write property gives access to a Serializable version of amended AmendedJourneyResult
		/// </summary>
		public ITDJourneyResult DeferableAmendedJourneyResult
		{
			get { return _AJourneyResult; }
			set { _AJourneyResult = value; }
		}

		/// <summary>
		/// Read/Write property gives access to a Serializable version of JourneyViewState
		/// </summary>
		public TDJourneyViewState DeferableJourneyViewState
		{
			get { return _JourneyViewState; }
			set { _JourneyViewState = value; }
		}

		/// <summary>
		/// Read/Write property gives access to a Serializable version of JourneyAdjustState
		/// </summary>
		public TDCurrentAdjustState DeferableJourneyAdjustState
		{
			get { return _JourneyAdjustState; }
			set { _JourneyAdjustState = value; }
		}

		/// <summary>
		/// Read/Write property gives access to a Serializable version of JourneyPlanControlData
		/// </summary>
		public JourneyPlanControlData DeferableJourneyPlanControlData
		{
			get { return _JourneyPlanControlData; }
			set { _JourneyPlanControlData = value; }
		}

		/// <summary>
		/// Read/Write property gives access to a Serializable version of JourneyPlanStateData
		/// </summary>
		public JourneyPlanStateData DeferableJourneyPlanStateData
		{
			get { return _JourneyPlanStateData; }
			set { _JourneyPlanStateData = value; }
		}

        /// <summary>
        /// Read/Write property gives access to a Serializable version of JourneyMapState
        /// </summary>
        public JourneyMapState DeferableJourneyMapState
        {
            get { return _JourneyMapState; }
            set { _JourneyMapState = value; }
        }

		/// <summary>
		/// Read/Write property gives access to a Serializable version of OutwardJourneyViewState
		/// </summary>
		public JourneyMapState DeferableOutwardJourneyMapState
		{
			get { return _OutwardJourneyMapState; }
			set { _OutwardJourneyMapState = value; }
		}


	}
}
