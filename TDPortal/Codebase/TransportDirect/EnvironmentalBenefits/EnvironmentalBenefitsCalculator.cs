// *********************************************** 
// NAME			: EnvironmentalBenefitsCalculator.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 21/09/2009
// DESCRIPTION	: Class to perform the environmental benefit calculations
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnvironmentalBenefits/EnvironmentalBenefitsCalculator.cs-arc  $
//
//   Rev 1.8   Dec 01 2009 11:18:10   mmodi
//Updated to calculate benefit when high value motorway contains a stopover section (e.g. bridge)
//Resolution for 5343: EBC - Bristol to Pembroke journey involving bridge fails to calculate EBC correctly
//
//   Rev 1.7   Nov 27 2009 09:55:20   rbroddle
//Fix for M6 problem usd 6277660
//Resolution for 5342: M6 Problems in EBC
//
//   Rev 1.6   Nov 18 2009 11:35:48   mmodi
//Updated logic for two concurrent motorway sections where there is no exit junction number
//Resolution for 5336: EBC: Bristol to Paisley M6 motorway does not show high value charge
//
//   Rev 1.5   Oct 26 2009 10:04:48   mmodi
//Calculate benefit using mile distance value
//
//   Rev 1.4   Oct 15 2009 16:57:24   mmodi
//Updated logic for changing motorways using junction or slip road
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.3   Oct 15 2009 13:24:52   mmodi
//Updated logic to handle to concurrent same motorway drive sections issue
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Oct 11 2009 20:49:42   mmodi
//Added logic for duplicate motorway junction roads
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Oct 11 2009 12:54:46   mmodi
//Updated calculation logic
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.0   Oct 06 2009 13:58:46   mmodi
//Initial revision.
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.EnvironmentalBenefits
{
    /// <summary>
    /// Class to perform the environmental benefit calculations
    /// </summary>
    public class EnvironmentalBenefitsCalculator
    {
        #region Private members

        // The data needed for the calculations
        private EnvironmentalBenefitsData ebcData;

        // Used to remove the prefix on toids
        private const string TOID_PREFIX = "JourneyControl.ToidPrefix";
        private string toidPrefix;

        double metresToMilesConversionFactory = 0;

        #endregion

        #region Private structs

        /// <summary>
        /// Struct used to define an EBCRoadCategory EBCCountry dictionary key
        /// </summary>
        private struct EBCRoadCountryKey
        {
            public EBCRoadCategory ebcRoadCategory;
            public EBCCountry ebcCountry;

            public EBCRoadCountryKey(EBCRoadCategory ebcRoadCategory, EBCCountry ebcCountry)
            {
                this.ebcRoadCategory = ebcRoadCategory;
                this.ebcCountry = ebcCountry;
            }
        }
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public EnvironmentalBenefitsCalculator()
        {
            ebcData = new EnvironmentalBenefitsData();
            
            SetupToidPrefix();

            SetupMilesConversionFactor();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Sets the toid prefix value
        /// </summary>
        private void SetupToidPrefix()
        {
            toidPrefix = Properties.Current[TOID_PREFIX];

            if (string.IsNullOrEmpty(toidPrefix))
            {
                toidPrefix = string.Empty;
            }
        }

        /// <summary>
        /// Sets the miles conversion factor
        /// </summary>
        private void SetupMilesConversionFactor()
        {
            try
            {
                // Use the same convesion factor used in the UI 
                double conversionFactor =
                    Convert.ToDouble(Properties.Current["Web.Controls.MileageConverter"], TDCultureInfo.CurrentUICulture.NumberFormat);

                metresToMilesConversionFactory = conversionFactor;
            }
            catch
            {
                // Property should exists but if missing set to 1609 
                // (this is the value used in the MeasurementConversion class)
                metresToMilesConversionFactory = 1609;                
            }
        }

        /// <summary>
        /// Returns an array of EnvironmentalBenefit objects for the RoadJourney.
        /// EnvironmentalBbenfit objects are only created for those EBCRoadCategory and EBCCountry travelled 
        /// by the road journey.
        /// </summary>
        /// <param name="roadJourney"></param>
        /// <param name="sessionId">Used for logging only</param>
        /// <returns></returns>
        private EnvironmentalBenefit[] CalculateEnvironmentalBenefitsForRoadJourney(RoadJourney roadJourney, string sessionId)
        {
            ArrayList environmentalBenefitArray = new ArrayList();

            if (roadJourney != null)
            {
                // Create a temp dictionary to hold the distance values for the Road/Country combinations
                Dictionary<EBCRoadCountryKey, double> dictionaryDistance = new Dictionary<EBCRoadCountryKey,double>();

                // Get an instance of the GISQuery
                IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

                // An index variable is used because the UpdateDistances.. method may require the loop to skip
                // a road journey detail. This can happen if two concurrent rjd's needed to be merged.
                int currentIndex = -1;

                // Go through each RoadJourneyDetail calculating the distances
                for (int i = 0; i < roadJourney.Details.Length; i++)
                {
                    currentIndex++;

                    RoadJourneyDetail rjd = roadJourney.Details[currentIndex];

                    UpdateDistancesForRoadJourneyDetail(ref dictionaryDistance, rjd, ref currentIndex, roadJourney.Details, gisQuery, sessionId);

                    // The method has changed the index
                    if (i != currentIndex)
                    {
                        i = currentIndex;
                    }
                }

                // Now calculate the benefit amount and create an EnvironmentalBenefit object
                foreach (KeyValuePair<EBCRoadCountryKey, double> kvp in dictionaryDistance)
                {
                    EBCRoadCountryKey key = kvp.Key;
                    double distance = kvp.Value;

                    double benefitAmount = CalculateEnvironmentalBenefit(key.ebcRoadCategory, key.ebcCountry, distance);

                    EnvironmentalBenefit environmentalBenefit = new EnvironmentalBenefit(
                        key.ebcRoadCategory, key.ebcCountry, distance, benefitAmount);

                    environmentalBenefitArray.Add(environmentalBenefit);

                    LogEnvironmentalBenefit(sessionId, environmentalBenefit);
                }
            }

            // Convert to the return object type
            return (EnvironmentalBenefit[])environmentalBenefitArray.ToArray(typeof(EnvironmentalBenefit));
        }

        /// <summary>
        /// Method which determines the EBCRoadCategory for the current RoadJourneyDetail provided.
        /// Based on the road category, other methods are called to calculate and update the distances 
        /// held in the Distance dictionary provided
        /// </summary>
        /// <param name="dictionaryDistance"></param>
        /// <param name="roadJourneyDetail"></param>
        /// <param name="gisQuery"></param>
        private void UpdateDistancesForRoadJourneyDetail(ref Dictionary<EBCRoadCountryKey, double> dictionaryDistance,
            RoadJourneyDetail roadJourneyDetail, ref int currentIndex, RoadJourneyDetail[] roadJourneyDetails, 
            IGisQuery gisQuery, string sessionId)
        {
            // Not calculating EB for stopover sections
            if (!roadJourneyDetail.IsStopOver)
            {
                // Flag to indicate if the HighValueMotorway special distance logic is to be used
                bool usePartialHighValueMotorwayLogic = false;

                // Determine this detail's road category
                EBCRoadCategory ebcRoadCategory = EnvironmentalBenefitsHelper.GetEBCRoadCategory(roadJourneyDetail.RoadNumber);

                #region Logic to determine type of Motorway

                if (IsRoadHighValueMotorway(roadJourneyDetail))
                {
                    // This is a HighValue, check if it is All HighValue
                    if (IsAllHighValueMotorway(roadJourneyDetail.RoadNumber))
                    {
                        ebcRoadCategory = EBCRoadCategory.MotorwayHigh;
                    }
                    else
                    {
                        // This section goes on a motorway, but not all of it is high value.
                        // Therefore must calculate how much of it is high value, and set distances accordingly
                        usePartialHighValueMotorwayLogic = true;
                    }
                }

                #endregion

                // Calculate and update distances
                if (!usePartialHighValueMotorwayLogic)
                {
                    // This detail is all on one road category, so can calculate distances using standard logic
                    CalculateDistances(ref dictionaryDistance, ebcRoadCategory, roadJourneyDetail, currentIndex, gisQuery, sessionId);
                }
                else
                {
                    // This detail goes on standard AND high value motorway, so use the special motorway logic
                    CalculateDistancesHighValueMotorway(ref dictionaryDistance, roadJourneyDetail, ref currentIndex, roadJourneyDetails, gisQuery, sessionId);
                }
            }
        }

        /// <summary>
        /// Method to calculate the distance travelled in each country for this RoadJourneyDetail.
        /// The method performs a GIS query using the TOIDs in the detail to identify the distance by country.
        /// The Distance dictionary totals are then updated using an EBCRoadCategory and country key.
        /// </summary>
        /// <param name="dictionaryDistance"></param>
        /// <param name="ebcRoadCategory"></param>
        /// <param name="roadJourneyDetail"></param>
        private void CalculateDistances(ref Dictionary<EBCRoadCountryKey, double> dictionaryDistance, EBCRoadCategory ebcRoadCategory, 
            RoadJourneyDetail roadJourneyDetail, int currentIndex, IGisQuery gisQuery, string sessionId)
        {
            // Remove prefix from toids
            string[] toids = EnvironmentalBenefitsHelper.RemoveToidPrefix(roadJourneyDetail.Toid, toidPrefix);
            
            // Call GIS query method to find countries/distances for the toids in this detail
            CountryDistances[] countryDistances = gisQuery.GetDistancesForTOIDs(toids);

            // Add distance totals for each country returned in query
            double distanceEngland = 0;
            double distanceScotland = 0;
            double distanceWales = 0;

            UpdateDistancesForCountries(countryDistances, roadJourneyDetail.Distance, 
                out distanceEngland, out distanceScotland, out distanceWales);

            #region Log for debugging

            LogDistances(sessionId, currentIndex, ebcRoadCategory, 
                roadJourneyDetail.RoadNumber, string.Empty, string.Empty,
                distanceEngland, distanceScotland, distanceWales, roadJourneyDetail.Distance);

            double totalDistance = distanceEngland + distanceScotland + distanceWales;
            LogDetailTotalDistance(sessionId, currentIndex, totalDistance, roadJourneyDetail.Distance);
                       

            #endregion

            #region Update totals in dictionary

            // Update the distance counts in dictionary for the EBCRoadCategory provided and each country
            AddDistanceToDictionary(ref dictionaryDistance, ebcRoadCategory, EBCCountry.England, distanceEngland);
            AddDistanceToDictionary(ref dictionaryDistance, ebcRoadCategory, EBCCountry.Scotland, distanceScotland);
            AddDistanceToDictionary(ref dictionaryDistance, ebcRoadCategory, EBCCountry.Wales, distanceWales);
            
            #endregion
        }

        /// <summary>
        /// Method which identifies the distance travelled on a Motorway which is High Value and Standard.
        /// The Distance dictionary is then updated with the calculated distances for the 
        /// appropriate EBCRoadCategory and EBCCountry the motorway travels
        /// </summary>
        /// <param name="dictionaryDistance"></param>
        /// <param name="roadJourneyDetail"></param>
        private void CalculateDistancesHighValueMotorway(ref Dictionary<EBCRoadCountryKey, double> dictionaryDistance, 
            RoadJourneyDetail roadJourneyDetail, ref int currentIndex, RoadJourneyDetail[] roadJourneyDetails, 
            IGisQuery gisQuery, string sessionId)
        {
            string currentMotorwayRoadNumber = roadJourneyDetail.RoadNumber;
            string entryJunctionNumber = string.Empty;
            string exitJunctionNumber = string.Empty;

            // Used to track if index number is changed by the GetEntryAndExitJunctionNumbers method
            int tempIndex = currentIndex;

            // Find the junction numbers this motorway section is travelling between
            bool junctionNumbersFound = GetEntryAndExitJunctionNumbers(roadJourneyDetail, ref currentIndex, roadJourneyDetails,
                out entryJunctionNumber, out exitJunctionNumber);

            // If the entry and exit junction number cannot be determined, then calculate as a standard motorway
            if (!junctionNumbersFound)
            {
                // The method has altered the current index (a concurrent motorway drive section scenario was found)
                // But because both junction numbers were not found, reset the current index back to what it was
                if (tempIndex != currentIndex)
                {
                    currentIndex = tempIndex;
                }

                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    string.Format("Environmental Benefits Calculation - SessionId[{0}]. Unable to determine start and end junctions for road journey detail (DetailIndex[{1}] " +
                    "RoadNumber[{2}] JunctionStart[{3}] JunctionEnd[{4}] )",
                sessionId,
                currentIndex.ToString().PadLeft(2),
                currentMotorwayRoadNumber.PadRight(5),
                entryJunctionNumber.PadRight(3),
                exitJunctionNumber.PadRight(3)
                )));

                CalculateDistances(ref dictionaryDistance, EBCRoadCategory.MotorwayStandard, roadJourneyDetail, currentIndex, gisQuery, sessionId);
            }
            else
            {
                // Based on the entry and exit junction numbers, calculate the distance travelled 
                // on High Value motorways
                Dictionary<EBCCountry, double> highValueDistances = GeyHighValueMotorwayJunctionsDistance(currentMotorwayRoadNumber, entryJunctionNumber, exitJunctionNumber);

                int detailDistance = roadJourneyDetail.Distance;

                #region GIS Query call to get country distances

                // Remove prefix from toids
                string[] toids = EnvironmentalBenefitsHelper.RemoveToidPrefix(roadJourneyDetail.Toid, toidPrefix);

                // Get the actual countries/distances using the toids in this detail
                CountryDistances[] countryDistances = gisQuery.GetDistancesForTOIDs(toids);

                // Place into a temp array (in case need to merge in another countrydistances array)
                ArrayList listCountryDistances = new ArrayList(countryDistances);
                
                // The GetEntryAndExitJunctionNumbersmethod has altered the current index (a concurrent motorway drive section scenario was found)
                if (tempIndex != currentIndex)
                {
                    // The currentIndex is now the next or next + 1 road journey detail index as we've gone searching for exit junction
                    while (tempIndex < currentIndex)
                    {
                        //move to next rjd
                        tempIndex++;
                        // Need to merge the next road journey detail distance in to the current,
                        // but check there is a distance (as it could be a stopover which has no toids, 
                        // hence no distance)
                        if (roadJourneyDetails[tempIndex].Distance > 0)
                        {
                            detailDistance += roadJourneyDetails[tempIndex].Distance;

                            // Need to get the distances for the next road journey detail and merge in to this current rjd
                            toids = EnvironmentalBenefitsHelper.RemoveToidPrefix(roadJourneyDetails[tempIndex].Toid, toidPrefix);

                            CountryDistances[] countryDistances2 = gisQuery.GetDistancesForTOIDs(toids);

                            listCountryDistances.AddRange(countryDistances2);
                        }
                        
                        // Log message indicating a merge has happened
                        LogRoadJourneyDetailMerged(sessionId, (tempIndex - 1), currentIndex);
                    }
                }

                #endregion

                // Distance variables for standard and high value motorways
                double distanceEngland = 0;
                double distanceScotland = 0;
                double distanceWales = 0;
                double distanceEnglandHigh = 0;
                double distanceScotlandHigh = 0;
                double distanceWalesHigh = 0;

                UpdateDistancesForCountries((CountryDistances[])listCountryDistances.ToArray(typeof(CountryDistances)), 
                    detailDistance, out distanceEngland, out distanceScotland, out distanceWales);
                
                #region Adjust the standard/high distances

                // If the calculated high value motorway distance is greater than the distance from the toids,
                // log a message and set distances to ensure the toids distance is used instead
                foreach (KeyValuePair<EBCCountry, double> kvp in highValueDistances)
                {
                    double distanceHigh = kvp.Value;
                    
                    switch (kvp.Key)
                    {
                        case EBCCountry.England:
                            if (distanceHigh > distanceEngland)
                            {
                                LogHighValueMotorwayDistance(sessionId, EBCCountry.England, distanceHigh, distanceEngland);
                                
                                // Use the distance caluculated using the toids
                                distanceEnglandHigh = distanceEngland;
                                distanceEngland = 0;
                            }
                            else
                            {
                                distanceEnglandHigh = distanceHigh;
                                distanceEngland -= distanceHigh;
                            }
                            break;

                        case EBCCountry.Scotland:
                            if (distanceHigh > distanceScotland)
                            {
                                LogHighValueMotorwayDistance(sessionId, EBCCountry.Scotland, distanceHigh, distanceScotland);

                                // Use the distance caluculated using the toids
                                distanceScotlandHigh = distanceScotland;
                                distanceScotland = 0;
                            }
                            else
                            {
                                distanceScotlandHigh = distanceHigh;
                                distanceScotland -= distanceHigh;
                            }
                            break;

                        case EBCCountry.Wales:
                            if (distanceHigh > distanceWales)
                            {
                                LogHighValueMotorwayDistance(sessionId, EBCCountry.Wales, distanceHigh, distanceWales);

                                // Use the distance caluculated using the toids
                                distanceWalesHigh = distanceWales;
                                distanceWales = 0;
                            }
                            else
                            {
                                distanceWalesHigh = distanceHigh;
                                distanceWales -= distanceHigh;
                            }
                            break;
                    }
                }

                #endregion

                #region Log for debugging

                LogDistances(sessionId, currentIndex, EBCRoadCategory.MotorwayStandard, 
                    currentMotorwayRoadNumber, entryJunctionNumber, exitJunctionNumber,
                    distanceEngland, distanceScotland, distanceWales, detailDistance);
                LogDistances(sessionId, currentIndex, EBCRoadCategory.MotorwayHigh, 
                    currentMotorwayRoadNumber, entryJunctionNumber, exitJunctionNumber,
                    distanceEnglandHigh, distanceScotlandHigh, distanceWalesHigh, detailDistance);

                double totalDistance = distanceEngland + distanceScotland + distanceWales
                    + distanceEnglandHigh + distanceScotlandHigh + distanceWalesHigh;
                LogDetailTotalDistance(sessionId, currentIndex, totalDistance, detailDistance);

                #endregion

                #region Update totals in dictionary

                AddDistanceToDictionary(ref dictionaryDistance, EBCRoadCategory.MotorwayStandard, EBCCountry.England, distanceEngland);
                AddDistanceToDictionary(ref dictionaryDistance, EBCRoadCategory.MotorwayStandard, EBCCountry.Scotland, distanceScotland);
                AddDistanceToDictionary(ref dictionaryDistance, EBCRoadCategory.MotorwayStandard, EBCCountry.Wales, distanceWales);

                AddDistanceToDictionary(ref dictionaryDistance, EBCRoadCategory.MotorwayHigh, EBCCountry.England, distanceEnglandHigh);
                AddDistanceToDictionary(ref dictionaryDistance, EBCRoadCategory.MotorwayHigh, EBCCountry.Scotland, distanceScotlandHigh);
                AddDistanceToDictionary(ref dictionaryDistance, EBCRoadCategory.MotorwayHigh, EBCCountry.Wales, distanceWalesHigh);
                
                #endregion
                
            }
        }

        /// <summary>
        /// Method which uses the road journey details to identify the entry and exit junction number 
        /// for the road. This assumes the RoadJourneyDetail is for a motorway section.
        /// If the entry or exit junction number cannot be determined, then false is returned.
        /// The currentIndex value will be incremented by 1 or more if this method detects a scenario where
        /// there are two or more concurrent road journey details traveling on the same motorway. This must be done
        /// to enable the entry and exit junction numbers for the motorway drive section to be found.
        /// Where this happens the calling method will need to merge the current and next road 
        /// jounrey details together.
        /// </summary>
        private bool GetEntryAndExitJunctionNumbers(RoadJourneyDetail roadJourneyDetail, ref int currentIndex,
            RoadJourneyDetail[] roadJourneyDetails,
            out string entryJunctionNumber, out string exitJunctionNumber)
        {
            string currentMotorwayRoadNumber = roadJourneyDetail.RoadNumber;

            // junction numbers
            entryJunctionNumber = string.Empty;
            exitJunctionNumber = string.Empty;

            // road numbers, used if we have to access the "unknown junction numbers" data
            string previousRoadNumber = string.Empty;
            string nextRoadNumber = string.Empty;

            #region Determine the entry Junction number

            // Get previous RoadJourneyDetail
            RoadJourneyDetail previousRJD = null;
            if (currentIndex > 0)
            {
                previousRJD = roadJourneyDetails[currentIndex - 1];
            }

            if (previousRJD != null)
            {
                if (previousRJD.IsJunctionSection)
                {
                    if (previousRJD.RoadNumber == currentMotorwayRoadNumber)
                    {
                        // The road numbers match, so joined and driving along a single motorway,
                        // e.g. joined on to M4 at junction 49
                        if ((previousRJD.JunctionAction == JunctionType.Entry)
                            ||
                            (previousRJD.JunctionAction == JunctionType.Merge))
                        {
                            if (!previousRJD.JunctionDriveSectionWithoutJunctionNo)
                            {
                                entryJunctionNumber = previousRJD.JunctionNumber;
                            }
                        }
                    }
                    else
                    {
                        // The road numbers don't match, so likely to be merging from one motorway 
                        // to another, e.g. M5 to M42.
                        // So use the RoadNumber to lookup from the unknown junctions data
                        previousRoadNumber = previousRJD.RoadNumber;
                    }
                }
                else if (previousRJD.JunctionDriveSectionWithoutJunctionNo)
                {
                    // The JunctionDriveSection has an unknown junction number, therefore look at the road 
                    // we enter from and use that in the lookup from the unknown junctions data

                    // Ensure that we can get to the previous-previous rjd, otherwise cannot determine the entry junction number
                    if ((currentIndex - 2) >= 0)
                    {
                        RoadJourneyDetail prevprevRJD = roadJourneyDetails[currentIndex - 2];

                        if (!prevprevRJD.IsJunctionSection && !prevprevRJD.IsStopOver)
                        {
                            previousRoadNumber = prevprevRJD.RoadNumber;
                        }
                        else if (prevprevRJD.IsJunctionSection && prevprevRJD.IsSlipRoad
                            && (prevprevRJD.RoadNumber != currentMotorwayRoadNumber)) // e.g. for M25 on to the M1
                        {
                            previousRoadNumber = prevprevRJD.RoadNumber;
                        }
                    }
                }
                else if (!previousRJD.IsStopOver)
                {
                    EBCRoadCategory roadCategory = EnvironmentalBenefitsHelper.GetEBCRoadCategory(previousRJD.RoadNumber);

                    // If the previous rjd is a drive section, and is on a different motorway, then the journey 
                    // has transitioned from another motorway without a JunctionDriveSection, e.g. M6 TOLL to M42
                    if ((roadCategory == EBCRoadCategory.MotorwayStandard)
                        &&
                        (previousRJD.RoadNumber != currentMotorwayRoadNumber))
                    {
                        previousRoadNumber = previousRJD.RoadNumber;
                    }
                    // Else there are two details in a row which say to drive on this same motorway.
                    // Shouldn't get to this scenario here because the Determine exit junction number will have
                    // detected this and skipped over this drive section on the next loop iteration
                    
                }
            }

            #endregion

            #region Determine the exit Junction number

            RoadJourneyDetail nextRJD = null;
            if (currentIndex < (roadJourneyDetails.Length - 1))
            {
                nextRJD = roadJourneyDetails[currentIndex + 1];
            }

            if (nextRJD != null)
            {
                if (nextRJD.IsJunctionSection)
                {
                    if (nextRJD.RoadNumber == currentMotorwayRoadNumber)
                    {
                        // The road numbers match, so joined and driving along a single motorway,
                        // e.g. joined on to M4 at junction 24
                        if ((nextRJD.JunctionAction == JunctionType.Exit)
                            ||
                            (nextRJD.JunctionAction == JunctionType.Merge))
                        {
                            if (!nextRJD.JunctionDriveSectionWithoutJunctionNo)
                            {
                                exitJunctionNumber = nextRJD.JunctionNumber;
                            }
                        }
                    }
                    else
                    {
                        // The road numbers don't match, so likely to be merging from one motorway 
                        // to another, e.g. M42 to M5.
                        // So use the RoadNumber to lookup from the unknown junctions data
                        nextRoadNumber = nextRJD.RoadNumber;
                    }
                }
                else if (nextRJD.JunctionDriveSectionWithoutJunctionNo)
                {
                    // The JunctionDriveSection has an unknown junction number, therefore look at the road 
                    // we exit onto and use that in the lookup from the unknown junctions data

                    // Ensure that we can get to the next-next rjd, otherwise cannot determine the exit junction number
                    if ((currentIndex + 2) < roadJourneyDetails.Length)
                    {
                        RoadJourneyDetail nextnextRJD = roadJourneyDetails[currentIndex + 2];

                        if (!nextnextRJD.IsJunctionSection && !nextnextRJD.IsStopOver)
                        {
                            nextRoadNumber = nextnextRJD.RoadNumber;
                        }
                    }
                }
                else if (!nextRJD.IsStopOver)
                {
                    EBCRoadCategory roadCategory = EnvironmentalBenefitsHelper.GetEBCRoadCategory(nextRJD.RoadNumber);

                    // If the next rjd is a drive section, and is on a different motorway, then the journey 
                    // has transitioned to another motorway without a JunctionDriveSection, e.g. M42 to M6 TOLL
                    if ((roadCategory == EBCRoadCategory.MotorwayStandard)
                        &&
                        (nextRJD.RoadNumber != currentMotorwayRoadNumber))
                    {
                        nextRoadNumber = nextRJD.RoadNumber;
                    }
                    // Else there are two or more details in a row which say to drive on this same motorway
                    else if (roadCategory == EBCRoadCategory.MotorwayStandard)
                    {
                        // Move to the next two rjd's and attempt to get their Junction numbers
                        int[] nextCouple = new int[] {2, 3};

                        foreach (int x in nextCouple)
                        {
                            if ((currentIndex + x) < roadJourneyDetails.Length && string.IsNullOrEmpty(exitJunctionNumber))
                            {
                                RoadJourneyDetail nextnextRJD = roadJourneyDetails[currentIndex + x];

                                if (nextnextRJD.IsJunctionSection && !nextnextRJD.IsStopOver
                                    && !nextnextRJD.JunctionDriveSectionWithoutJunctionNo)
                                {
                                    exitJunctionNumber = nextnextRJD.JunctionNumber;
                                }
                                else if (!nextnextRJD.IsJunctionSection && !nextnextRJD.IsStopOver)
                                //     && !nextnextRJD.JunctionDriveSectionWithoutJunctionNo) //RB Might actually be this
                                {
                                    // This is a scenario where the Motorway just comes off on to another road/motorway
                                    // without a Junction section! e.g. M6 on to the A74 (M)
                                    // But if a junction number exists, should be ok to use
                                    if (!string.IsNullOrEmpty(nextnextRJD.JunctionNumber))
                                    {
                                        exitJunctionNumber = nextnextRJD.JunctionNumber;
                                    }
                                    // otherwise attempt to get the RoadNumber and lookup from the Unknown junctions data if no duplicate issue
                                    else if ( !string.IsNullOrEmpty(nextnextRJD.RoadNumber) 
                                        && string.IsNullOrEmpty(GetDuplicateMotorwayRoadNumber(nextnextRJD.RoadNumber)) 
                                        && GetDuplicateMotorwayRoadNumber(roadJourneyDetail.RoadNumber) != nextnextRJD.RoadNumber )
                                    {
                                        nextRoadNumber = nextnextRJD.RoadNumber;
                                        exitJunctionNumber = GetUnknownMotorwayJunction(currentMotorwayRoadNumber, nextRoadNumber, string.Empty, false);
                                    }
                                }
                                if (!string.IsNullOrEmpty(exitJunctionNumber))
                                {
                                    // Successfully found an exitJunctionNumber. Therefore need to move the
                                    // currentIndex along to ensure this detail is not used in the next 
                                    // loop iteration. The next rjd distances/toids will have to be merged
                                    // into the current rjd. This is indicated by moving on the currentIndex number
                                    // which the calling method must then detect and handle.
                                    currentIndex = currentIndex + (x -1);
                                }
                            }
                        }
                    }
                }
                else if (nextRJD.IsStopOver)
                {
                    #region Handle motorway -> stopover -> motorway logic

                    // While on a motorway, if the next rjd is a StopOver section, then likely to be a toll charge
                    // so skip over this and move to the next detail

                    // Ensure that we can get to the next-next rjd, otherwise cannot determine the exit junction number
                    if ((currentIndex + 2) < roadJourneyDetails.Length)
                    {
                        RoadJourneyDetail nextnextRJD = roadJourneyDetails[currentIndex + 2];

                        // The rjd after the stopover should be to continue on the same motorway
                        if ((!nextnextRJD.IsStopOver) && (nextnextRJD.RoadNumber == currentMotorwayRoadNumber))
                        {
                            // If the next next rjd is a drive section, and is on the same motorway, then the journey 
                            // has continued along the same motorway, and therefore we should check the next next next rjd,
                            // e.g. when travelling on M6 Toll -> toll charge -> M6 Toll -> M42, 
                            // or M4 -> Severn bridge charge -> M4 -> A48
                            if ((currentIndex + 3) < roadJourneyDetails.Length)
                            {
                                RoadJourneyDetail nextnextnextRJD = roadJourneyDetails[currentIndex + 3];

                                if ((nextnextnextRJD.IsJunctionSection) && (!nextnextnextRJD.JunctionDriveSectionWithoutJunctionNo)
                                    && (nextnextnextRJD.RoadNumber == currentMotorwayRoadNumber))
                                {
                                    // The road numbers match, so joined and driving along a single motorway,
                                    // and there is an exit junction number
                                    exitJunctionNumber = nextnextnextRJD.JunctionNumber;
                                }
                                // Assume there are no other scenarios of rjd, if so add else's here

                                if (!string.IsNullOrEmpty(exitJunctionNumber))
                                {
                                    // Successfully found an exitJunctionNumber. Therefore need to move the
                                    // currentIndex along to ensure these details are skipped. 
                                    // The skipped rjd distances/toids will have to be merged
                                    // into the current rjd. This is indicated by moving on the currentIndex number
                                    // which the calling method must then detect and handle.
                                    currentIndex = currentIndex + 2; // + stopover rjd + next motorway rjd
                                }
                            }
                        }
                    }

                    #endregion
                }
            }

            #endregion

            #region Get the unknown junction numbers

            string duplicateMotorway = GetDuplicateMotorwayRoadNumber(currentMotorwayRoadNumber);

            // Determine if need handling for motorway duplicate connection junction
            if (!string.IsNullOrEmpty(duplicateMotorway))
            {
                // This motorway has a duplicate motorway connecting junctions, so supply the 
                // entry/exit junction number as required to the lookup

                // Assume both sides of this motorway drive section are not unknown
                if ((!string.IsNullOrEmpty(entryJunctionNumber)) && (string.IsNullOrEmpty(exitJunctionNumber)))
                {
                    // The end junction num of this drive section has not been set, so supply the start junction number
                    exitJunctionNumber = GetUnknownMotorwayJunction(currentMotorwayRoadNumber, nextRoadNumber, entryJunctionNumber, false);
                }
                else if ((!string.IsNullOrEmpty(exitJunctionNumber)) && (string.IsNullOrEmpty(entryJunctionNumber)))
                {
                    // The start junction  num of this drive section has not been set, so supply the end junction number
                    entryJunctionNumber = GetUnknownMotorwayJunction(currentMotorwayRoadNumber, previousRoadNumber, exitJunctionNumber, true);
                }
                else
                {
                    // Both start and end junction numbers are not known, should not calculate as high value
                    // as cannot gurantee this motorway section goes through a high value section.
                }
            }
            else
            {
                // This is not a high value motorway which has duplicate motorway unknown junction numbers, 
                // so can get the default unknown junction number for this motorway
                if (string.IsNullOrEmpty(entryJunctionNumber) && !string.IsNullOrEmpty(previousRoadNumber))
                {
                    entryJunctionNumber = GetUnknownMotorwayJunction(currentMotorwayRoadNumber, previousRoadNumber, string.Empty, true);
                }
                if (string.IsNullOrEmpty(exitJunctionNumber) && !string.IsNullOrEmpty(nextRoadNumber))
                {
                    exitJunctionNumber = GetUnknownMotorwayJunction(currentMotorwayRoadNumber, nextRoadNumber, string.Empty, false);
                }
            }

            #endregion

            // If unable to determine the entry or exit junction number return false, the above logic failed
            if ((string.IsNullOrEmpty(entryJunctionNumber)) || (string.IsNullOrEmpty(exitJunctionNumber)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Method calculates the environmental benefit amount using the factor from 
        /// the EnvironmentalBenefitsData cache for the EBCRoadCategory/EBCCountry against the distance
        /// </summary>
        /// <param name="ebcRoadCategory"></param>
        /// <param name="ebcCountry"></param>
        /// <param name="distance">Distance in metres</param>
        /// <returns>Benefit amount in pence</returns>
        private double CalculateEnvironmentalBenefit(EBCRoadCategory ebcRoadCategory, EBCCountry ebcCountry, double distance)
        {
            // Calculation should always be done against the Miles value (not the metres). This is because
            // the end user if manually calculating, they will calculating using a Mile distance value

            // Get the factor to use
            double pencePerMileFactor = ebcData.GetRoadCategoryCost(ebcRoadCategory, ebcCountry);

            // Convert distance to miles
            double distanceMiles = (double)distance / metresToMilesConversionFactory;

            // Round distance to 2.d.p before calculating
            distanceMiles = Math.Round(distanceMiles, 2);

            double benefitAmount = (double)distanceMiles * pencePerMileFactor;

            return benefitAmount;
        }

        #region Helper methods

        /// <summary>
        /// Method to determine if the supplied RoadJourneyDetail is on a HighValueMotorway
        /// </summary>
        /// <param name="roadJourneyDetail"></param>
        /// <returns></returns>
        private bool IsRoadHighValueMotorway(RoadJourneyDetail roadJourneyDetail)
        {
            bool isHighValue = false;

            if (roadJourneyDetail != null)
            {
                // Determine this detail's road category
                EBCRoadCategory ebcRoadCategory = EnvironmentalBenefitsHelper.GetEBCRoadCategory(roadJourneyDetail.RoadNumber);

                if (ebcRoadCategory == EBCRoadCategory.MotorwayStandard)
                {
                    // Check if this is a HighValue motorway (junction sections cannot be HighValue motorways)
                    if ((!roadJourneyDetail.IsJunctionSection) && (!roadJourneyDetail.JunctionDriveSectionWithoutJunctionNo))
                    {
                        if (IsHighValueMotorway(roadJourneyDetail.RoadNumber))
                        {
                            isHighValue = true;
                        }
                    }
                }
            }

            return isHighValue;
        }

        #region EBC DATA

        /// <summary>
        /// Method uses the EnvironmentalBenefitsData cache to determine if the provided road number is 
        /// a HighValueMotorway
        /// </summary>
        /// <param name="roadJourneyDetail"></param>
        /// <returns></returns>
        private bool IsHighValueMotorway(string roadNumber)
        {
            if (ebcData.IsHighValueMotorway(roadNumber))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Method uses the EnvironmentalBenefitsData cache to determine if the provided Motorway road number is
        /// all a high value motorway. Returns true if it is or false if not
        /// Assumes the provided road number IS a motorway road
        /// </summary>
        /// <param name="roadNumber"></param>
        /// <returns></returns>
        private bool IsAllHighValueMotorway(string roadNumber)
        {
            return ebcData.IsAllHighValueMotorway(roadNumber);
        }

        /// <summary>
        /// Method uses the EnvironmentalBenefitsData cache to determine if the provided Motorway road number
        /// has a duplicate motorway junction connection. 
        /// This is to allow the caller to provide a joining junction number to ensure the correct unknown junction
        /// number is returned in the method GetUnknownMotorwayJunction().
        /// </summary>
        /// <param name="roadNumber"></param>
        /// <returns></returns>
        private string GetDuplicateMotorwayRoadNumber(string roadNumber)
        {
            return ebcData.GetDuplicateMotorwayRoadNumber(roadNumber);
        }

        /// <summary>
        /// Method uses the EnvironmentalBenefitsData cache to return the distances by country 
        /// for the high value motorway (between the junctions specified)
        /// </summary>
        /// <param name="roadNumber"></param>
        /// <returns></returns>
        private Dictionary<EBCCountry, double> GeyHighValueMotorwayJunctionsDistance(string roadNumber, string startJunction, string endJunction)
        {
            return ebcData.GetHighValueMotorwayJunctionsDistance(roadNumber, startJunction, endJunction);
        }

        /// <summary>
        /// Method uses the EnvironmentalBenefitsData cache to return the Unknown junction number for the 
        /// specified road number and joining road. The Entry flag determines if the entry or exit junction
        /// number is returned
        /// </summary>
        /// <param name="roadNumber"></param>
        /// <param name="joiningRoadNumber"></param>
        /// <param name="entry"></param>
        /// <returns></returns>
        private string GetUnknownMotorwayJunction(string roadNumber, string joiningRoadNumber, string joiningJunction, bool entry)
        {
            return ebcData.GetUnknownMotorwayJunction(roadNumber, joiningRoadNumber, joiningJunction, entry);
        }

        #endregion

        #region Distances

        /// <summary>
        /// Method which takes an array of CountryDistances and updates the distance totals for each country.
        /// The distances are adjusted accordingly to be that of the RoadJourneyDetail distance
        /// </summary>
        /// <param name="countryDistances"></param>
        /// <param name="distanceEngland"></param>
        /// <param name="distanceScotland"></param>
        /// <param name="distanceWales"></param>
        private void UpdateDistancesForCountries(CountryDistances[] countryDistances, int roadJourneyDetailDistance,
            out double distanceEngland, out double distanceScotland, out double distanceWales)
        {
            distanceEngland = 0;
            distanceScotland = 0;
            distanceWales = 0;

            #region Total up distances for each country

            // Loop through distances and update each country total
            foreach (CountryDistances countryDistance in countryDistances)
            {
                if (countryDistance.EnglandDist > 0)
                {
                    distanceEngland += countryDistance.EnglandDist;
                }

                if (countryDistance.ScotlandDist > 0)
                {
                    distanceScotland += countryDistance.ScotlandDist;
                }

                if (countryDistance.WalesDist > 0)
                {
                    distanceWales += countryDistance.WalesDist;
                }
            }

            #endregion

            #region Adjust distances if needed

            // If the toids are not in the same country and/or the total distance is different to 
            // the RoadJourneyDetail distance, then need to apply logic to adjust the distance as per DN sec 6.6
            if ((distanceEngland + distanceScotland + distanceWales) != roadJourneyDetailDistance)
            {
                if (distanceEngland > distanceScotland)
                {
                    if (distanceEngland > distanceWales)
                    {
                        // England has the greatest total so needs adjusting
                        distanceEngland = (roadJourneyDetailDistance - (distanceScotland + distanceWales));
                    }
                    else
                    {
                        // Wales has the greatest total so needs adjusting
                        distanceWales = (roadJourneyDetailDistance - (distanceScotland + distanceEngland));
                    }
                }
                else
                {
                    if (distanceScotland > distanceWales)
                    {
                        // Scotland has the greatest total so needs adjusting
                        distanceScotland = (roadJourneyDetailDistance - (distanceWales + distanceEngland));

                    }
                    else
                    {
                        // Wales has the greatest total so needs adjusting
                        distanceWales = (roadJourneyDetailDistance - (distanceScotland + distanceEngland));
                    }
                }
            }

            #endregion
        }

        /// <summary>
        /// Method which adds the supplied distance (if greater than 0) to the appropriate distance country key 
        /// in the Distance dictionary
        /// </summary>
        /// <param name="dictionaryDistance"></param>
        /// <param name="ebcRoadCategory"></param>
        /// <param name="ebcCountry"></param>
        /// <param name="distance"></param>
        private void AddDistanceToDictionary(ref Dictionary<EBCRoadCountryKey, double> dictionaryDistance, 
            EBCRoadCategory ebcRoadCategory, EBCCountry ebcCountry, double distance)
        {
            if (distance > 0)
            {
                EBCRoadCountryKey key = new EBCRoadCountryKey(ebcRoadCategory, ebcCountry);

                if (dictionaryDistance.ContainsKey(key))
                {
                    double totalDistance = dictionaryDistance[key];
                    totalDistance += distance;

                    dictionaryDistance[key] = totalDistance;
                }
                else
                {
                    dictionaryDistance.Add(key, distance);
                }
            }
        }

        #endregion

        #region Logging

        /// <summary>
        /// Logs the calculated distances for the road journey detail
        /// </summary>
        private void LogDistances(string sessionId, int index, EBCRoadCategory ebcRoadCategory,
            string roadNumber, string junctionStart, string junctionEnd,
            double distanceEngland, double distanceScotland, double distanceWales, 
            double detailDistance)
        {
            
            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                string.Format(
                "Environmental Benefits Calculation - SessionId[{0}]. Calculated distances( DetailIndex[{1}] EBCRoadCategory[{2}] " + 
                    "RoadNumber[{3}] JunctionStart[{4}] JunctionEnd[{5}] England[{6}] Scotland[{7}] Wales[{8}] DetailDistance[{9}] )",
                sessionId,
                index.ToString().PadLeft(2),
                ebcRoadCategory.ToString().PadRight(16),
                roadNumber.PadRight(5),
                junctionStart.PadRight(3),
                junctionEnd.PadRight(3),
                distanceEngland.ToString().PadLeft(5),
                distanceScotland.ToString().PadLeft(5),
                distanceWales.ToString().PadLeft(5),
                detailDistance.ToString().PadLeft(5))));
        }

        /// <summary>
        /// Logs a message if the total distance calculated is not the same as the detail distance
        /// </summary>
        private void LogDetailTotalDistance(string sessionId, int index, double totalDistance, double detailDistance)
        {
            if (totalDistance != detailDistance)
            {
                // Total distance and Road journey detail are different
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    string.Format(
                    "Environmental Benefits Calculation - SessionId[{0}]. For road journey detail index[{1}], the calculated distance[{2}] did not equal the detail distance[{3}]",
                    sessionId,
                    index.ToString().PadLeft(2),
                    totalDistance,
                    detailDistance)));
            }
        }

        /// <summary>
        /// Logs a message indicating the calculated high value motorway distance for the country is greater than 
        /// the distance for country using the toids
        /// </summary>
        private void LogHighValueMotorwayDistance(string sessionId, EBCCountry ebcCountry, double distanceHighMotorway, double distanceCountry)
        {
            string message = string.Format(
                                    "Environmental Benefits Calculation - SessionId[{0}]. High value distance[{1}] for country[{2}] was " 
                                    + "greater than toids distance [{3}]; toids distance has been used",
                                    sessionId,
                                    distanceHighMotorway,
                                    ebcCountry.ToString(),
                                    distanceCountry);

            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, message));

            // No longer need to throw an exception
            //throw new TDException(message, false, TDExceptionIdentifier.EBCErrorCalculatingHighValueMotorwayDistance);
        }

        /// <summary>
        /// Logs a message indicating the specified road journey detail index has been merged in to the mergedIndex
        /// </summary>
        private void LogRoadJourneyDetailMerged(string sessionId, int index, int mergedIndex)
        {
            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                string.Format(
                "Environmental Benefits Calculation - SessionId[{0}]. Road journey detail DetailIndex[{1}] has been merged into DetailIndex[{2}]",
                sessionId,
                index.ToString().PadLeft(2),
                mergedIndex.ToString().PadLeft(2))));
        }

        /// <summary>
        /// Logs the EnvironmentalBenefit
        /// </summary>
        /// <param name="eb"></param>
        private void LogEnvironmentalBenefit(string sessionId, EnvironmentalBenefit eb)
        {
            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                string.Format(
                "Environmental Benefits Calculation - SessionId[{0}]. Environmental Benefit object( {1} )",
                sessionId,
                eb.ToString())));
        }

        /// <summary>
        /// Logs a message showing the distance of the road journey compared to the environmental benefits
        /// distance calculated
        /// </summary>
        private void LogRoadJourneyDistance(string sessionId, RoadJourney roadJourney, EnvironmentalBenefits ebs)
        {
            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                string.Format("Environmental Benefits Calculation - SessionId[{0}]. Road journey distance[{1}] Environmental Benefits total distance[{2}]",
                sessionId,
                roadJourney.TotalDistance.ToString(),
                ebs.GetTotalBenefitDistance(EBCRoadCategory.None, EBCCountry.None).ToString())));
        }

        #endregion

        #endregion

        #endregion

        #region Public methods

        /// <summary>
        /// Method which triggers the re-loading of this objects internal data used in the calculations
        /// </summary>
        public void ReloadData()
        {
            ebcData.ReloadData();
        }

        /// <summary>
        /// Method to calculate the environmental benefits for the supplied road journey
        /// </summary>
        /// <param name="roadJourney">RoadJourney to calculate benefits for</param>
        /// <param name="sessionId">Session ID used for logging</param>
        /// <returns></returns>
        public EnvironmentalBenefits GetEnvironmentalBenefits(RoadJourney roadJourney, string sessionId)
        {
            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
            "Environmental Benefits calculation started for the road journey. SessionId[" + sessionId+"]"));

            EnvironmentalBenefits environmentalBenefits = new EnvironmentalBenefits();
            
            if (roadJourney != null)
            {
                // Used to log time taken for calculation
                DateTime startTime = DateTime.Now;

                try
                {
                    // Call the calculation method
                    EnvironmentalBenefit[] environmentalBenefitArray = CalculateEnvironmentalBenefitsForRoadJourney(roadJourney, sessionId);

                    // Package the result into its wrapper
                    environmentalBenefits = new EnvironmentalBenefits(roadJourney.RouteNum, environmentalBenefitArray);

                    #region Log for debugging

                    // Log totals for debugging
                    LogRoadJourneyDistance(sessionId, roadJourney, environmentalBenefits);

                    // Log verbose message
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    "Environmental Benefits calculation completed for the road journey. SessionId[" + sessionId + "] Success[True]"));

                    #endregion

                    // Log succesful completion event of ebc calculation for reporting
                    EBCCalculationEvent ebcEvent = new EBCCalculationEvent(startTime, true, sessionId);
                    Logger.Write(ebcEvent);
                }
                catch (Exception ex)
                {
                    #region Log for debugging

                    // Log operational error
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,
                        string.Format("Error occurred calculating the Environmental Benefits for a road journey. Exception: {0}. Stacktrace: {1}.",
                        ex.Message,
                        ex.StackTrace)));

                    // Log verbose mesage
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                    "Environmental Benefits calculation completed for the road journey. SessionId[" + sessionId + "] Success[False]"));

                    #endregion

                    // Log failed completion event of ebc calculation for reporting
                    EBCCalculationEvent ebcEvent = new EBCCalculationEvent(startTime, false, sessionId);
                    Logger.Write(ebcEvent);
                }
            }
            
            return environmentalBenefits;
        }

        /// <summary>
        /// Method to determine if the specified road journey detail travels on a section of high value motorway
        /// </summary>
        /// <returns>True if the road journey detail travels on a section of high value motorway</returns>
        public bool GoesThroughHighValueMotorwaySection(int currentIndex, RoadJourneyDetail[] roadJourneyDetails)
        {
            bool travelsOnHighValueMotorway = false;

            // Check details have been provided, and the specified index exists
            if ((roadJourneyDetails != null) && (roadJourneyDetails.Length >= currentIndex))
            {
                // Get the road journey detail to be looked at
                RoadJourneyDetail roadJourneyDetail = roadJourneyDetails[currentIndex];

                if (roadJourneyDetail != null)
                {
                    // Stopover section cannot be high value
                    if (!roadJourneyDetail.IsStopOver)
                    {
                        if (IsRoadHighValueMotorway(roadJourneyDetail))
                        {
                            // This is a HighValue, check if it is All HighValue
                            if (IsAllHighValueMotorway(roadJourneyDetail.RoadNumber))
                            {
                                travelsOnHighValueMotorway = true;
                            }
                            else
                            {
                                // This section goes on a motorway, but not all of it is high value.
                                // Therefore must determine if this section goes through a high value section of the motorway

                                // junction numbers
                                string entryJunctionNumber = string.Empty;
                                string exitJunctionNumber = string.Empty;

                                // Find the junction numbers this motorway section is travelling between
                                bool junctionNumbersFound = GetEntryAndExitJunctionNumbers(roadJourneyDetail, ref currentIndex, roadJourneyDetails,
                                    out entryJunctionNumber, out exitJunctionNumber);

                                #region Concurrent motorway road journey details issue handling

                                if (!junctionNumbersFound)
                                {
                                    // If this detail could not be found, then this might be the second or third detail in the 
                                    // "2/3 concurrent road journey details traveling on the same motorway" issue. The 
                                    // 2nd/3rd detail does not get flagged as high value as it should have been merged
                                    // into the first detail.
                                    // So submit the previous 2 details and check if the index is incremented,
                                    // thus indicating this scenario.
                                    int[] prevCouple = new int[] { currentIndex - 2, currentIndex - 1 };

                                    // Need to store the entry and exit numbers in a temp variable because
                                    // if moving backwards through thr RJD, one of them may be a stopover section
                                    // e.g. Severn bridge, which would not return an entry/exit junction
                                    string tempEntryJunctionNumber = string.Empty;
                                    string tempExitJunctionNumber = string.Empty;

                                    foreach (int prevIndex in prevCouple)
                                    {
                                        if (roadJourneyDetails.Length >= prevIndex)
                                        {
                                            RoadJourneyDetail prevRJD = roadJourneyDetails[prevIndex];

                                            //copy the prevIndex so we can pass it by ref
                                            int prevIndexRef = prevIndex;
                                            bool prevJunctionNumbersFound = GetEntryAndExitJunctionNumbers(prevRJD, ref prevIndexRef, roadJourneyDetails,
                                                out tempEntryJunctionNumber, out tempExitJunctionNumber);

                                            // Save the junction numbers
                                            if (!string.IsNullOrEmpty(tempEntryJunctionNumber))
                                            {
                                                entryJunctionNumber = tempEntryJunctionNumber;
                                            }

                                            if (!string.IsNullOrEmpty(tempExitJunctionNumber))
                                            {
                                                exitJunctionNumber = tempExitJunctionNumber;
                                            }

                                            if ((prevJunctionNumbersFound) && (currentIndex <= prevIndexRef))
                                            {
                                                // junction numbers found and the index has been incremented,
                                                // above issue has been detected, so OK to use the found junction numbers
                                                junctionNumbersFound = true;
                                            }
                                        }
                                    }
                                }

                                #endregion

                                // If the entry and exit junction number cannot be determined, then default to not high value
                                if (!junctionNumbersFound)
                                {
                                    travelsOnHighValueMotorway = false;
                                }
                                else
                                {
                                    string currentMotorwayRoadNumber = roadJourneyDetail.RoadNumber;

                                    // Based on the entry and exit junction numbers, calculate the distance travelled 
                                    // on High Value motorways
                                    Dictionary<EBCCountry, double> highValueDistances = GeyHighValueMotorwayJunctionsDistance(currentMotorwayRoadNumber, entryJunctionNumber, exitJunctionNumber);

                                    // If there is any high value distance > 0 then this section has travelled on a high value motorway                               
                                    foreach (KeyValuePair<EBCCountry, double> kvp in highValueDistances)
                                    {
                                        double distanceHigh = kvp.Value;

                                        if (distanceHigh > 0)
                                        {
                                            travelsOnHighValueMotorway = true;

                                            break;
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }

            return travelsOnHighValueMotorway;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read only. Returns the EnvironmentalBenefitsData object associated with this calculator.
        /// Only used for NUnit testing.
        /// </summary>
        public EnvironmentalBenefitsData EBCData
        {
            get { return ebcData; }
        }

        #endregion
    }
}
