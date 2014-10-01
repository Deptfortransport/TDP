// *********************************************** 
// NAME                 : DBSRequest.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 10/09/2009 
// DESCRIPTION  		: Departure Board Service Request Class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/DepartureBoardFacade/DBSRequest.cs-arc  $
//
//   Rev 1.2   Feb 17 2010 16:42:22   RHopkins
//Pass OperatorCode in StopEvent request
//Resolution for 5403: Pass OperatorCode in StopEvents request
//
//   Rev 1.1   Sep 14 2009 15:26:06   apatel
//Departure Board Service request class
//Resolution for 5315: CCN526 Stop Information Page Landing

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade
{
    /// <summary>
    /// Departure Board Service Request Class
    /// </summary>
    public class DBSRequest
    {
        #region Private Fields
        private string transactionId;
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
		
		/// <summary>
		/// Default constructor to initialise varaible to their default state
		/// </summary>
		public DBSRequest()
		{
            operatorCode = string.Empty;
			serviceNumber = string.Empty; 
			rangeType = DBSRangeType.Sequence;
			originLocation = null;
			destinationLocation = null;
		}
		#endregion

        #region Public Properties
        /// <summary>
        /// Read-Write property for transaction id of the request
        /// </summary>
        public string TransactionId
        {
            get { return transactionId; }
            set { transactionId = value; }
        }

        /// <summary>
        /// Read-Write property for Origin Location (From)
        /// </summary>
        public DBSLocation OriginLocation
        {
            get { return originLocation; }
            set { originLocation = value; }
        }

        /// <summary>
        /// Read-Write property for Destination Location (To)
        /// </summary>
        public DBSLocation DestinationLocation
        {
            get { return destinationLocation; }
            set { destinationLocation = value; }
        }


        /// <summary>
        /// Read-Write property for Departure Board Service TimeRequest 
        /// </summary>
        public DBSTimeRequest JourneyTimeInformation
        {
            get { return journeyTimeInformation; }
            set { journeyTimeInformation = value; }
        }

        /// <summary>
        /// Read-Write property for Departure Board Service Range Type 
        /// </summary>
        public DBSRangeType RangeType
        {
            get { return rangeType; }
            set { rangeType = value; }
        }


        /// <summary>
        /// Read-Write property for Departure Board Service Range 
        /// </summary>
        public int Range
        {
            get { return range; }
            set { range = value; }
        }

        /// <summary>
        /// Read-Write property for Departure Board Service deparure or arrival
        /// </summary>
        public bool ShowDepartures
        {
            get { return showDepartures; }
            set { showDepartures = value; }
        }


        /// <summary>
        /// Read-Write property for Departure Board Operator Code
        /// </summary>
        public string OperatorCode
        {
            get { return operatorCode; }
            set { operatorCode = value; }
        }


        /// <summary>
        /// Read-Write property for Departure Board Service Number
        /// </summary>
        public string ServiceNumber
        {
            get { return serviceNumber; }
            set { serviceNumber = value; }
        }


        /// <summary>
        /// Read-Write property to show Departure Board Service calling points
        /// </summary>
        public bool ShowCallingStops
        {
            get { return showCallingStops; }
            set { showCallingStops = value; }
        }
        #endregion
    }
}
