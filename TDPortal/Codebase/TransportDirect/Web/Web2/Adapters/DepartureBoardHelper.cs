// *********************************************** 
// NAME                 : DepartureBoardHelper.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 10/09/2009 
// DESCRIPTION  		: Provide methods to build departure board service request
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/DepartureBoardHelper.cs-arc  $
//
//   Rev 1.5   Dec 17 2009 11:05:26   apatel
//make the showcallingstops property to be true by default.
//Resolution for 5349: Issue with Gloucester, off market parade, bus station, bay I not showing departure staions
//
//   Rev 1.4   Dec 04 2009 14:59:32   apatel
//Departure board changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.3   Nov 22 2009 15:57:56   pghumra
//Stop Information departure board changes
//Resolution for 5338: Stop Information Code Fixes
//
//   Rev 1.2   Nov 18 2009 16:00:26   pghumra
//Code fixes for TDP release patch 10.8.2.3 - Stop Information page
//Resolution for 5338: Stop Information Code Fixes
//
//   Rev 1.1   Sep 14 2009 15:23:32   apatel
//Departure Board Service helper class
//Resolution for 5315: CCN526 Stop Information Page Landing

using System;
using System.Collections.Generic;
using System.Web;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.Web.Adapters
{
    public class DepartureBoardHelper
    {

        #region Public Methods
        public DBSRequest CreateDepartureBoardServiceRequest(string transactionId, string stopCode, TDCodeType stopCodeType, int range, TDDateTime tdDatetime, bool showDepartures)
        {
            DBSRequest request = new DBSRequest();

            request.OriginLocation = CreateDBSLocation(stopCode, stopCodeType);

            // Departure board shows arrivals and departures for the origin stop
            request.DestinationLocation = null;

            request.JourneyTimeInformation = CreateDBSTimeRequest(tdDatetime);

            request.TransactionId = transactionId;
            request.RangeType = DBSRangeType.Sequence;
            request.Range = range;
            request.ShowDepartures = showDepartures;
            request.ShowCallingStops = true;

            return request;
        }

        
        #endregion

        #region Private Methods
        /// <summary>
        /// Creates the DBSLocation object for the CRS or SMS code provided
        /// </summary>
        /// <param name="stopCode">CRS or SMS code</param>
        /// <param name="stopCodeType">Stop code type CRS, SMS, etc.</param>
        /// <returns></returns>
        private DBSLocation CreateDBSLocation(string stopCode, TDCodeType stopCodeType)
        {
            DBSLocation dbsLocation = new DBSLocation();

            if (string.IsNullOrEmpty(stopCode))
                return null;

            dbsLocation.Code = stopCode;
            dbsLocation.Type = stopCodeType;
            dbsLocation.Valid = true;

            if (stopCodeType == TDCodeType.NAPTAN)
            {
                dbsLocation.NaptanIds = new string[] { stopCode };
                NaptanCacheEntry nce = NaptanLookup.Get(dbsLocation.NaptanIds[0], string.Empty);
                dbsLocation.Locality = nce.Locality;
                
            
            }
            return dbsLocation;  
        }

        /// <summary>
        /// Creates DBSTimeRequest object out of the date time provided
        /// </summary>
        /// <param name="tdDatetime">TD date time object</param>
        /// <returns></returns>
        private DBSTimeRequest CreateDBSTimeRequest(TDDateTime timeRequest)
        {
            DBSTimeRequest dbsTimeRequest = new DBSTimeRequest();
            if (timeRequest == null)
            {
                timeRequest = new TDDateTime();
            }
            dbsTimeRequest.Hour = timeRequest.Hour;
            dbsTimeRequest.Minute = timeRequest.Minute;
            dbsTimeRequest.Type = TimeRequestType.Now;
            return dbsTimeRequest;
        }

        #endregion
    }
}
