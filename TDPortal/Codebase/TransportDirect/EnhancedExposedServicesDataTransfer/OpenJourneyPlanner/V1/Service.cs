// *********************************************** 
// NAME                 : Service.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: This class represents a service
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/Service.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:46   mturner
//Initial revision.
//
//   Rev 1.0   Apr 06 2006 16:21:54   COwczarek
//Initial revision.
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1 
{

    /// <summary>
    /// This class represents a service
    /// </summary>
    [Serializable]
    public class Service
    {

        private Days[] daysOfOperation;
        private string destinationBoard;
        private DateTime expiryDate;
        private DateTime firstDateOfOperation;
        private bool openEnded;
        private string operatorCode;
        private string operatorName;
        private string privateID;
        private string retailId;
        private string serviceNumber;
        private string timetableLinkURL;

        public Service(){}

        /// <summary>
        /// Read/write property that is an array of days when the service typically runs.
        /// </summary>
        public Days[] DaysOfOperation
        {
            get {  return daysOfOperation; }
            set { daysOfOperation = value; }    
        }
    
        /// <summary>
        /// Read/write property that is the destination as shown on the front of the vehicle 
        /// (blank if unavailable).
        /// </summary>
        public string DestinationBoard
        {
            get {  return destinationBoard; }
            set { destinationBoard = value; }    
        }

        /// <summary>
        /// Read/write property that is the expiry date of the service if not open ended.
        /// </summary>
        public DateTime ExpiryDate
        {
            get {  return expiryDate; }
            set { expiryDate = value; }    
        }
    
        /// <summary>
        /// Read/write property that is the first date that this service operates (0 if unavailable).
        /// </summary>
        public DateTime FirstDateOfOperation
        {
            get {  return firstDateOfOperation; }
            set { firstDateOfOperation = value; }    
        }
    
        /// <summary>
        /// Read/write property that is false, if an expiry date is known for the service, true otherwise.
        /// </summary>
        public bool OpenEnded
        {
            get {  return openEnded; }
            set { openEnded = value; }    
        }
    
        /// <summary>
        /// Read/write property that is the code of the operator that provides the service.
        /// </summary>
        public string OperatorCode
        {
            get {  return operatorCode; }
            set { operatorCode = value; }    
        }
    
        /// <summary>
        /// Read/write property that is the customer facing name of the operator
        /// </summary>
        public string OperatorName
        {
            get {  return operatorName; }
            set { operatorName = value; }    
        }

        /// <summary>
        /// Read/write property that is the train UID. Present for rail only journeys.
        /// </summary>    
        public string PrivateID
        {
            get {  return privateID; }
            set { privateID = value; }    
        }
    
        /// <summary>
        /// Read/write property that is the retail id for a train. Present for rail only journeys.
        /// </summary>
        public string RetailId
        {
            get {  return retailId; }
            set { retailId = value; }    
        }
    
        /// <summary>
        /// Read/write property that is the service number as shown on the front of the vehicle 
        /// (blank if unavailable). For flight only journeys, this is the flight number.
        /// </summary>
        public string ServiceNumber
        {
            get {  return serviceNumber; }
            set { serviceNumber = value; }    
        }
 
        /// <summary>
        /// Read/write property that is a URL which can be used to display a timetable in a browser window.
        /// </summary>
        public string TimetableLinkUrl
        {
            get {  return timetableLinkURL; }
            set { timetableLinkURL = value; }    
        }
    }
 
}