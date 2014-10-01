// *********************************************** 
// NAME             : LocalityGazetteer.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: Gazetteer for Localities
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Text;
using TDP.Common.EventLogging;
using TDP.Common.Extenders;
using TDP.Common.LocationService.GIS;
using TDP.Common.PropertyManager;
using TDP.Common.ServiceDiscovery;
using TDP.Reporting.Events;
using Logger = System.Diagnostics.Trace;

namespace TDP.Common.LocationService.Gazetteer
{
    /// <summary>
    /// Gazetteer for Localities
    /// </summary>
    [Serializable()]
    public class LocalityGazetteer : TDPGazetteer
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sessionID"></param>
        /// <param name="userLoggedOn"></param>
        public LocalityGazetteer(string sessionID, bool userLoggedOn)
            : base(sessionID, userLoggedOn)
        {
            SetGazetteerId("locationservice.localitygazetteerid");
            
            SetMinScore("locationservice.locality.minscore");

            xmlHandler = new XmlGazetteerHandler(userLoggedOn, sessionID, minScore);

            if (TDPTraceSwitch.TraceVerbose)
            {
                OperationalEvent log = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                    string.Format("Using {0} with gazetterId[{1}] and minimumScore[{2}]", this.GetType().Name, gazetteerId, minScore));
                Logger.Write(log);
            }
        }

        #endregion

        #region ITDGazetteer implementation

        /// <summary>
        /// Method searching a location from a string.
        /// </summary>
        /// <param name="text">input text string</param>
        /// <param name="fuzzy">indicates if search should be fuzzy</param>
        /// <returns>Returns a LocationQueryResult representing the result of the query</returns>
        public override LocationQueryResult FindLocation(string text, bool fuzzy)
        {
            DateTime submitted = DateTime.Now;
            String result = GazopsWebService.DrillDownAddressQuery(
                text,
                string.Empty,
                string.Empty,
                string.Empty,
                fuzzy,
                gazetteerId,
                string.Empty,
                string.Empty
                );
            LogGazetteerEvent(GazetteerEventCategory.GazetteerLocality, submitted);

            return xmlHandler.ReadDrillDownResult(result, string.Empty);
        }

        /// <summary>
        /// Calls the gazetteer DrillDown method
        /// </summary>
        /// <param name="text">Input text string</param>
        /// <param name="fuzzy">Indicates if search is fuzzy</param>
        /// <param name="sPickList">gives the list entry to drill down with</param>
        /// <param name="queryRef">Gives the query reference</param>
        /// <param name="choice">choice</param>
        /// <returns>Returns a LocationQueryResult representing the result of the query</returns>
        public override LocationQueryResult DrillDown(
            string text,
            bool fuzzy,
            string sPickList,
            string queryRef,
            LocationChoice choice)
        {
            if (!choice.HasChilden)
            {
                OperationalEvent oe = new OperationalEvent(TDPEventCategory.Business, sessionID, TDPTraceLevel.Error,
                    "Unable to drill into a choice without children");
                Logger.Write(oe);

                throw new TDPException(oe.Message, true, TDPExceptionIdentifier.LSLocalityLacksChildren);
            }

            PickList pickList;

            if (sPickList == string.Empty)
                pickList = new PickList();
            else
                pickList = new PickList(sPickList);

            pickList.Add(choice.PicklistCriteria, choice.PicklistValue);

            DateTime submitted = DateTime.Now;
            String result = GazopsWebService.DrillDownAddressQuery(
                                                        text,
                                                        pickList.Current,
                                                        string.Empty,
                                                        queryRef,
                                                        fuzzy,
                                                        gazetteerId,
                                                        string.Empty,
                                                        string.Empty);
            LogGazetteerEvent(GazetteerEventCategory.GazetteerLocality, submitted);

            return xmlHandler.ReadDrillDownResult(result, pickList.Current);
        }

        /// <summary>
        /// Retrieves the Location details from a given choice
        /// </summary>
        /// <param name="text">Input text string</param>
        /// <param name="fuzzy">Indicates if search is fuzzy</param>
        /// <param name="pickList">Gives the list entry to drill down with</param>
        /// <param name="queryRef">Gives the query reference</param>
        /// <param name="choice">LocationChoice</param>
        /// <returns>Returns a populated location</returns>
        public override TDPLocation GetLocationDetails(
                                string text,
                                bool fuzzy,
                                string pickList,
                                string queryRef,
                                LocationChoice choice)
        {
            // Because it supports hierarchical searches, it can drill down whether it has children or not

            // If Easting or Northing have not been populated
            if (choice.OSGridReference.Easting == -1 || choice.OSGridReference.Northing == -1)
            {
                DateTime submitted = DateTime.Now;
                String gazopsResult = GazopsWebService.DrillDownAddressQuery(
                                        text,
                                        pickList,
                                        choice.PicklistValue,
                                        queryRef,
                                        fuzzy,
                                        gazetteerId,
                                        string.Empty,
                                        string.Empty);
                LogGazetteerEvent(GazetteerEventCategory.GazetteerLocality, submitted);

                LocationQueryResult result = xmlHandler.ReadFinalDrillDownResult(gazopsResult, pickList);
                choice = (LocationChoice)result.LocationChoiceList[0];
            }

            // Get toids
            LocationService locationService = TDPServiceDiscovery.Current.Get<LocationService>(ServiceDiscoveryKey.LocationService);

            List<string> toids = LocationServiceHelper.FindToids(choice.OSGridReference, string.Empty, false);

            // Create the address postcode location
            TDPLocation location = new TDPLocation(
                choice.Description,
                choice.Description,
                choice.Locality,
                toids,
                new List<string>(),
                string.Empty,
                TDPLocationType.Locality,
                TDPLocationType.Locality,
                choice.OSGridReference,
                choice.OSGridReference,
                false,
                false,
                0,
                0,
                string.Empty);

            return location;
        }

        /// <summary>
        /// Returns if the gazetteer supports hierarchic search
        /// </summary>
        public override bool SupportHierarchicSearch
        {
            get { return true; }
        }

        #endregion
    }
}
