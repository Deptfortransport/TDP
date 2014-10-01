// *********************************************** 
// NAME                 : DepartureBoardServiceInternalRequest.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 20/01/2006 
// DESCRIPTION  		: Class for Holding DBS request Parameter in one class 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/DepartureBoard/V1/DepartureBoardServiceInternalRequest.cs-arc  $ 
//
//   Rev 1.1   Feb 17 2010 16:42:30   RHopkins
//Pass OperatorCode in StopEvent request
//Resolution for 5403: Pass OperatorCode in StopEvents request
//
//   Rev 1.0   Nov 08 2007 12:22:20   mturner
//Initial revision.
//
//   Rev 1.0   Jan 27 2006 16:30:46   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.2   Jan 20 2006 16:24:20   schand
//Added more comments and Serialization attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Dec 22 2005 11:33:22   asinclair
//Check in of Sanjeev's Work In Progress code
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements

using System;
using TransportDirect.UserPortal.DepartureBoardService;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.DepartureBoard.V1
{
	/// <summary>
	/// Class for Holding DBS request Parameter in one class 
	/// </summary>
	[System.Serializable]
	public class DepartureBoardServiceInternalRequest
	{
		#region  Private Members
		private DBSLocation originLocation;
		private DBSLocation destinationLocation;
		private DBSTimeRequest journeyTimeInformation;
		private DBSRangeType rangeType;               
		private int range;                            
		private bool showDepartures;                  
		private bool showCallingStops;
        private string operatorCode;
        private string serviceNumber;
        #endregion        
        
		#region Constructor
		public DepartureBoardServiceInternalRequest()
		{
			originLocation = new DBSLocation();
			destinationLocation = new DBSLocation(); 
			journeyTimeInformation = new DBSTimeRequest();
            operatorCode = string.Empty;
            serviceNumber = string.Empty;
        }
		#endregion

		#region  Public Properties
		/// <summary>
		/// Read-Write property for OriginLocation
		/// </summary>
		public DBSLocation OriginLocation
		{
			get{ return originLocation;}
			set{ originLocation = value;}
		}

		/// <summary>
		/// Read-Write property for DestinationLocation
		/// </summary>
		public DBSLocation DestinationLocation
		{
			get{ return destinationLocation;}
			set{ destinationLocation = value;}
		}

		/// <summary>
		/// Read-Write property for JourneyTimeInformation
		/// </summary>
		public DBSTimeRequest JourneyTimeInformation
		{
			get{ return journeyTimeInformation;}
			set{ journeyTimeInformation = value;}
		}

		/// <summary>
		/// Read-Write property for RangeType
		/// </summary>
		public DBSRangeType RangeType
		{
			get{ return rangeType;}
			set{ rangeType = value;}
		}

		/// <summary>
		/// Read-Write property for Range
		/// </summary>
		public int Range
		{
			get{ return range;}
			set{ range = value;}
		}

		/// <summary>
		/// Read-Write property for ShowDepartures
		/// </summary>
		public bool ShowDepartures
		{
			get{ return showDepartures;}
			set{ showDepartures = value;}
		}

		/// <summary>
		/// Read-Write property for ShowCallingStops
		/// </summary>
		public bool ShowCallingStops
		{
			get{ return showCallingStops;}
			set{  showCallingStops = value;}
		}

        /// <summary>
        /// Read-Write property for Operator Code
        /// </summary>
        public string OperatorCode
        {
            get { return operatorCode; }
            set { operatorCode = value; }
        }

        /// <summary>
        /// Read-Write property for ServiceNumber
        /// </summary>
        public string ServiceNumber
        {
            get { return serviceNumber; }
            set { serviceNumber = value; }
        }
        #endregion

	}
}

