// *********************************************** 
// NAME             : AddressPostcodeGazetteer.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: Gazetteer for addesses and Postcode
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
    /// Gazetteer for addesses and Postcode
    /// </summary>
    [Serializable()]
    public class AddressPostcodeGazetteer : TDPGazetteer
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sessionID"></param>
        /// <param name="userLoggedOn"></param>
        public AddressPostcodeGazetteer(string sessionID, bool userLoggedOn)
            : base(sessionID, userLoggedOn)
        {
            SetGazetteerId("locationservice.postcodegazetteerid");

            SetMinScore("locationservice.addresspostcode.minscore");

            xmlHandler = new XmlGazetteerHandler(userLoggedOn, sessionID, minScore);
        }

        #endregion

        #region ITDPGazetteer implementation

        /// <summary>
        /// Method searching a location from a string.
        /// </summary>
        /// <param name="text">input text string</param>
        /// <param name="fuzzy">indicates if search should be fuzzy</param>
        /// <returns>Returns a LocationQueryResult representing the result of the query</returns>
        public override LocationQueryResult FindLocation(string text, bool fuzzy)
        {
            bool usePostcode, usePartPostcode, useAddress;

            PopulateGazetteerId(text, out usePostcode, out usePartPostcode, out useAddress);

            DateTime submitted = DateTime.Now;

            // if the input text consists of just a postcode : Call the PostcodeMatch() method
            if (usePostcode)
            {
                string result = GazopsWebService.PostcodeMatch(
                                    text,
                                    gazetteerId,
                                    string.Empty,
                                    string.Empty);
                Logging(submitted, true);

                return xmlHandler.ReadPostcodeMatchResult(result);
            }
            // else if the input text consists of just a partial postcode : Call the PartPostcodeMatch() method
            else if (usePartPostcode)
            {
                string result = GazopsWebService.PostcodeMatch(
                    text,
                    gazetteerId,
                    string.Empty,
                    string.Empty);
                Logging(submitted, true);

                return xmlHandler.ReadPostcodeMatchResult(result);

            }
            // else if the input text consists of an address: Call the DrillDownAddressQuery() method
            else if (useAddress)
            {
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
                Logging(submitted, false);

                return xmlHandler.ReadDrillDownResult(result, String.Empty);
            }
            // else the query text is a single word address, hence "too vague" to search
            else
            {
                LocationQueryResult lqr = new LocationQueryResult("");
                lqr.IsVague = true;
                lqr.IsSingleWordAddress = true;
                return lqr;
            }
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

                throw new TDPException(oe.Message, true, TDPExceptionIdentifier.LSAddressDrillLacksChildren);
            }

            bool usePostcode, usePartPostcode, useAddress;

            PopulateGazetteerId(text, out usePostcode, out usePartPostcode, out useAddress);

            // Should only get here for Addresses (not postcode/partial postcode)
            if (usePostcode || usePartPostcode)
            {
                OperationalEvent oe = new OperationalEvent(TDPEventCategory.Business, sessionID, TDPTraceLevel.Error,
                    "Unable to drill into a postcode only location");
                Logger.Write(oe);

                throw new TDPException(oe.Message, true, TDPExceptionIdentifier.LSPostCodeDrillInvalid);
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
            Logging(submitted, false);

            return xmlHandler.ReadDrillDownResult(result, pickList.Current);
        }


        /// <summary>
        /// Retrieves the Location details from a given choice
        /// </summary>
        /// <param name="text">Input text string</param>
        /// <param name="fuzzy">Indicates if search is fuzzy</param>
        /// <param name="pickList">Gives the list entry to drill down with</param>
        /// <param name="queryRef">Gives the query reference</param>
        /// <param name="choice">LocationChoice </param>
        /// <returns>Returns a populated location</returns>
        public override TDPLocation GetLocationDetails(
                                string text,
                                bool fuzzy,
                                string pickList,
                                string queryRef,
                                LocationChoice choice)
        {
            if (choice.HasChilden)
            {
                OperationalEvent oe = new OperationalEvent(TDPEventCategory.Business, sessionID, TDPTraceLevel.Error,
                    "Unable to get location details for a choice that still has children");
                Logger.Write(oe);

                throw new TDPException(oe.Message, true, TDPExceptionIdentifier.LSAddressPostCodeGazetteerHasChildren);
            }
                        
            bool usePostcode, usePartPostcode, useAddress;

            PopulateGazetteerId(text, out usePostcode, out usePartPostcode, out useAddress);

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
                Logging(submitted, false);

                LocationQueryResult result = xmlHandler.ReadFinalDrillDownResult(gazopsResult, pickList);
                choice = (LocationChoice)result.LocationChoiceList[0];
            }

            string address = string.Empty;

            #region Find street address for poulating toids

            // For address gazetteer, set street address, 
            // and divide easting and northing by 10 (mmodi, why?)
            if (useAddress)
            {
                choice.OSGridReference.Easting /= 10;
                choice.OSGridReference.Northing /= 10;

                // Set address used to populate toids
                address = choice.Description;
            }

            // For postcode gazetteer, find an street address to populate toids from
            if (usePostcode)
            {
                GisQuery gisQuery = TDPServiceDiscovery.Current.Get<GisQuery>(ServiceDiscoveryKey.GisQuery);

                string[] streets = gisQuery.GetStreetsFromPostCode(text);

                StringBuilder streetList = new StringBuilder();

                foreach (string street in streets)
                {
                    if (streetList.Length > 0)
                        streetList.Append(',');

                    streetList.Append(street);
                }

                // Set address used to populate toids
                address = streetList.ToString();
            }

            #endregion

            // Get locality and toids
            LocationService locationService = TDPServiceDiscovery.Current.Get<LocationService>(ServiceDiscoveryKey.LocationService);

            string locality = LocationServiceHelper.FindLocality(choice.OSGridReference, null);
            List<string> toids = LocationServiceHelper.FindToids(choice.OSGridReference, address, true);

            TDPLocationType locationType = (useAddress) ? TDPLocationType.Address : TDPLocationType.Postcode; 

            // Create the address postcode location
            TDPLocation location = new TDPLocation(
                (useAddress) ? choice.Description.AddressFormat() : choice.Description.Replace(" ", string.Empty), // Remove spaces from postcode as this becomes the location id
                choice.Description.AddressFormat(),
                locality,
                toids,
                new List<string>(),
                string.Empty,
                locationType,
                locationType,
                choice.OSGridReference,
                choice.OSGridReference,
                false,
                false,
                0,
                0,
                (!string.IsNullOrEmpty(choice.PicklistValue)) ? choice.PicklistValue : choice.Naptan );

            return location;
        }

        /// <summary>
        /// Returns if the gazetteer supports hierarchic search
        /// </summary>
        public override bool SupportHierarchicSearch
        {
            get { return false; }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Populates the gazetteer id based on the search text, i.e. Postcode, Part Postcode, Address
        /// </summary>
        private void PopulateGazetteerId(string text, out bool usePostcode, out bool usePartPostcode, out bool useAddress)
        {
            usePostcode = false;
            usePartPostcode = false;
            useAddress = false;

            // if the input text consists of just a postcode : Call the PostcodeMatch() method
            if (text.IsValidPostcode())
            {
                SetGazetteerId("locationservice.postcodegazetteerid");
                
                usePostcode = true;
            }
            // else if the input text consists of just a partial postcode : Call the PartPostcodeMatch() method
            else if (text.IsValidPartPostcode())
            {
                SetGazetteerId("locationservice.partpostcodegazetteerid");

                usePartPostcode = true;

            }
            // else if the input text consists of an address: Call the DrillDownAddressQuery() method
            else if (text.IsNotSingleWord())
            {
                SetGazetteerId("locationservice.addressgazetteerid");

                useAddress = true;
            }

            if (TDPTraceSwitch.TraceVerbose)
            {
                OperationalEvent log = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                    string.Format("Using {0} with gazetterId[{1}] and minimumScore[{2}]", this.GetType().Name, gazetteerId, minScore));
                Logger.Write(log);
            }
        }

        /// <summary>
        /// Method called for logging purpose
        /// </summary>
        /// <param name="isPostcodeMatch">indicates if the method PostcodeMatch is called</param>
        private void Logging(DateTime submitted, bool isPostcodeMatch)
        {
            if (!isPostcodeMatch)
            {
                LogGazetteerEvent(GazetteerEventCategory.GazetteerAddress, submitted);
            }
            else
            {
                LogGazetteerEvent(GazetteerEventCategory.GazetteerPostCode, submitted);
            }
        }

        #endregion
    }
}
