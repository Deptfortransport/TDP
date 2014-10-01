// *********************************************** 
// NAME			: EnvironmentalBenefitsData.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 21/09/2009
// DESCRIPTION	: Loads the environmental benefits data from the database
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnvironmentalBenefits/EnvironmentalBenefitsData.cs-arc  $
//
//   Rev 1.8   Dec 01 2009 11:17:18   mmodi
//Updated to convert junction number "8/9"
//Resolution for 5344: EBC - Hayes to Maidenhead throws an error screen
//
//   Rev 1.7   Oct 30 2009 13:03:30   mmodi
//Check for null road number before working
//Resolution for 5333: EBC - Aberdeen to Swansea journey fails
//
//   Rev 1.6   Oct 26 2009 10:04:24   mmodi
//Store pence value as per mile
//
//   Rev 1.5   Oct 20 2009 15:58:28   mmodi
//Make junction number lower before working with it. Corrects problem with Glasgow to London journey
//
//   Rev 1.4   Oct 15 2009 15:38:48   mmodi
//Remove spaces in the middle of the unknown road number before working with it
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.3   Oct 15 2009 13:21:58   mmodi
//Corrected convert junction number to decimal method
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.2   Oct 11 2009 20:48:06   mmodi
//Added logic to return unknown junction number for a duplication motorway junction road
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.1   Oct 11 2009 12:52:20   mmodi
//Updated
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.0   Oct 06 2009 13:58:48   mmodi
//Initial revision.
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.EnvironmentalBenefits
{
    /// <summary>
    /// Class to load the environmental benefits data from the database
    /// </summary>
    public class EnvironmentalBenefitsData
    {
        #region Private members

        // Should only be junction letters up to b, but use to d just in case
        private static char[] junctionLetters = "abcd".ToCharArray();
        
        // Erroneous junction characters
        private static char[] junctionCharacters = "/\\".ToCharArray();

        // Dictionarys to hold data
        Dictionary<RoadCategoryCostKey, double> ebcRoadCategoryCost;
        Dictionary<string, HighValueMotorwayData> ebcHighValueMotorway;
        Dictionary<HighValueMotorwayJunctionKey, HighValueMotorwayJunctionData> ebcHighValueMotorwayJunction;
        Dictionary<UnknownMotorwayJunctionKey, UnknownMotorwayJunctionData> ebcUnknownMotorwayJunction;

        #endregion

        #region Dictionary keys

        /// <summary>
        /// Structure used to define key for accessing data cached in ebcRoadCategoryCost dictionary
        /// </summary>
        private struct RoadCategoryCostKey
        {
            public EBCRoadCategory ebcRoadCategory;
            public EBCCountry ebcCountry;

            public RoadCategoryCostKey(EBCRoadCategory ebcRoadCategory, EBCCountry ebcCountry)
            {
                this.ebcRoadCategory = ebcRoadCategory;
                this.ebcCountry = ebcCountry;
            }
        }

        /// <summary>
        /// Structure used to define value data for storing data cached in ebcHighValueMotorway dictionary
        /// </summary>
        public struct HighValueMotorwayData
        {
            public string duplicateJunction;
            public bool allHighValue;

            public HighValueMotorwayData(string duplicateJunction, bool allHighValue)
            {
                this.duplicateJunction = duplicateJunction;
                this.allHighValue = allHighValue;
            }
        }

        /// <summary>
        /// Structure used to define key for accessing data cached in ebcHighValueMotorwayJunction dictionary
        /// </summary>
        private struct HighValueMotorwayJunctionKey
        {
            public string motorwayName;
            public decimal junctionStart;
            public decimal junctionEnd;

            public HighValueMotorwayJunctionKey(string motorwayName, decimal junctionStart, decimal junctionEnd)
            {
                this.motorwayName = motorwayName;
                this.junctionStart = junctionStart;
                this.junctionEnd = junctionEnd;
            }
        }

        /// <summary>
        /// Structure used to define value data for storing data cached in ebcHighValueMotorwayJunction dictionary
        /// </summary>
        public struct HighValueMotorwayJunctionData
        {
            public EBCCountry ebcCountry;
            public int distance;

            public HighValueMotorwayJunctionData(EBCCountry ebcCountry, int distance)
            {
                this.ebcCountry = ebcCountry;
                this.distance = distance;
            }
        }

        /// <summary>
        /// Structure used to define key for accessing data cached in ebcUnknownMotorwayJunction dictionary
        /// </summary>
        private struct UnknownMotorwayJunctionKey
        {
            public string motorwayName;
            public string joiningRoad;
            public decimal joiningJunction;

            public UnknownMotorwayJunctionKey(string motorwayName, string joiningRoad, decimal joiningJunction)
            {
                this.motorwayName = motorwayName;
                this.joiningRoad = joiningRoad;
                this.joiningJunction = joiningJunction;
            }
        }

        /// <summary>
        /// Structure used to define value data for storing data cached in ebcHighValueMotorwayJunction dictionary
        /// </summary>
        public struct UnknownMotorwayJunctionData
        {
            public string junctionEntry;
            public string junctionExit;

            public UnknownMotorwayJunctionData(string junctionEntry, string junctionExit)
            {
                this.junctionEntry = junctionEntry;
                this.junctionExit = junctionExit;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public EnvironmentalBenefitsData()
        {
            LoadData();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads the data and performs pre processing
        /// </summary>
        private void LoadData()
        {
            SqlDataReader reader;
            SqlHelper helper = new SqlHelper();

            try
            {
                // Initialise a SqlHelper and connect to the database.
                Logger.Write(new OperationalEvent(TDEventCategory.Business,
                    TDTraceLevel.Info, "Opening database connection to " + SqlHelperDatabase.DefaultDB.ToString()));
                helper.ConnOpen(SqlHelperDatabase.DefaultDB);
                
                #region Load data

                // Get EBC data
                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business,
                        TDTraceLevel.Verbose, "Loading Environmental Benefits data started"));
                }

                // Create temporary hashtables to load the data into first
                Dictionary<RoadCategoryCostKey, double> ebcRoadCategoryCostTemp = new Dictionary<RoadCategoryCostKey, double>();
                Dictionary<string, HighValueMotorwayData> ebcHighValueMotorwayTemp = new Dictionary<string, HighValueMotorwayData>();
                Dictionary<HighValueMotorwayJunctionKey, HighValueMotorwayJunctionData> ebcHighValueMotorwayJunctionTemp = new Dictionary<HighValueMotorwayJunctionKey, HighValueMotorwayJunctionData>();
                Dictionary<UnknownMotorwayJunctionKey, UnknownMotorwayJunctionData> ebcUnknownMotorwayJunctionTemp = new Dictionary<UnknownMotorwayJunctionKey, UnknownMotorwayJunctionData>();

                // Execute the stored procedures for each table

                #region Load

                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "Loading Environmental Benefits data - RoadCategoryCost"));

                // This returns the PencePerMile for RoadCategory, Country
                reader = helper.GetReader("GetEnvBenRoadCategoryCosts", new Hashtable());

                int roadCategoryColumnOrdinal = reader.GetOrdinal("RoadCategory");
                int countryColumnOrdinal = reader.GetOrdinal("Country");
                int penceColumnOrdinal = reader.GetOrdinal("PencePerMile");

                while (reader.Read())
                {
                    string roadCategory = reader.IsDBNull(roadCategoryColumnOrdinal) ? string.Empty : reader.GetString(roadCategoryColumnOrdinal);
                    string country = reader.IsDBNull(countryColumnOrdinal) ? string.Empty : reader.GetString(countryColumnOrdinal);
                    decimal pencePerMile = reader.IsDBNull(penceColumnOrdinal) ? -1 : reader.GetDecimal(penceColumnOrdinal);

                    AddRoadCategoryCost(ref ebcRoadCategoryCostTemp, roadCategory, country, Convert.ToDouble(pencePerMile));
                }
                reader.Close();


                
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "Loading Environmental Benefits data - HighValueMotorway"));

                // This returns high value motorways with a MotorwayName and AllHighValue flag
                reader = helper.GetReader("GetEnvBenHighValueMotorways", new Hashtable());

                int motorwayNameColumnOrdinal = reader.GetOrdinal("MotorwayName");
                int duplicateJunctionOrdinal = reader.GetOrdinal("DuplicateMotorwayJunction");
                int allHighValueColumnOrdinal = reader.GetOrdinal("AllHighValue");

                while (reader.Read())
                {
                    string motorwayName = reader.IsDBNull(motorwayNameColumnOrdinal) ? string.Empty : reader.GetString(motorwayNameColumnOrdinal);
                    string duplicateJunction = reader.IsDBNull(duplicateJunctionOrdinal) ? string.Empty : reader.GetString(duplicateJunctionOrdinal);
                    bool allHighValue = reader.IsDBNull(allHighValueColumnOrdinal) ? true : reader.GetBoolean(allHighValueColumnOrdinal);
                    
                    AddHighValueMotorway(ref ebcHighValueMotorwayTemp, motorwayName, duplicateJunction, allHighValue);
                }
                reader.Close();

                

                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "Loading Environmental Benefits data - HighValueMotorwayJunction"));

                // This returns the JunctionStart, JunctionEnd, Distance, Country, for each high value MotorwayName
                reader = helper.GetReader("GetEnvBenHighValueMotorwayJunctions", new Hashtable());

                motorwayNameColumnOrdinal = reader.GetOrdinal("MotorwayName");
                int junctionStartColumnOrdinal = reader.GetOrdinal("JunctionStart");
                int junctionEndColumnOrdinal = reader.GetOrdinal("JunctionEnd");
                int distanceColumnOrdinal = reader.GetOrdinal("Distance");
                countryColumnOrdinal = reader.GetOrdinal("Country");

                while (reader.Read())
                {
                    string motorwayName = reader.IsDBNull(motorwayNameColumnOrdinal) ? string.Empty : reader.GetString(motorwayNameColumnOrdinal);
                    string junctionStart = reader.IsDBNull(junctionStartColumnOrdinal) ? string.Empty : reader.GetString(junctionStartColumnOrdinal);
                    string junctionEnd = reader.IsDBNull(junctionEndColumnOrdinal) ? string.Empty : reader.GetString(junctionEndColumnOrdinal);
                    int distance = reader.IsDBNull(distanceColumnOrdinal) ? -1 : reader.GetInt32(distanceColumnOrdinal);
                    string country = reader.IsDBNull(motorwayNameColumnOrdinal) ? string.Empty : reader.GetString(countryColumnOrdinal);

                    AddEnvBenHighValueMotorwayJunction(ref ebcHighValueMotorwayJunctionTemp, motorwayName, junctionStart, junctionEnd, distance, country);
                }
                reader.Close();


                
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "Loading Environmental Benefits data - UnknownMotorwayJunction"));

                // This returns the unknown JunctionEntry, JunctionExit for each MotorwayName, JoiningRoad, JoiningJunction
                reader = helper.GetReader("GetEnvBenUnknownMotorwayJunctions", new Hashtable());

                motorwayNameColumnOrdinal = reader.GetOrdinal("MotorwayName");
                int joiningRoadColumnOrdinal = reader.GetOrdinal("JoiningRoad");
                int joiningJunctionColumnOrdinal = reader.GetOrdinal("JoiningJunction");
                int junctionEntryColumnOrdinal = reader.GetOrdinal("JunctionEntry");
                int junctionExitColumnOrdinal = reader.GetOrdinal("JunctionExit");

                while (reader.Read())
                {
                    string motorwayName = reader.IsDBNull(motorwayNameColumnOrdinal) ? string.Empty : reader.GetString(motorwayNameColumnOrdinal);
                    string joiningRoad = reader.IsDBNull(joiningRoadColumnOrdinal) ? string.Empty : reader.GetString(joiningRoadColumnOrdinal);
                    string joiningJunction = reader.IsDBNull(joiningJunctionColumnOrdinal) ? string.Empty : reader.GetString(joiningJunctionColumnOrdinal);
                    string junctionEntry = reader.IsDBNull(junctionEntryColumnOrdinal) ? string.Empty : reader.GetString(junctionEntryColumnOrdinal);
                    string junctionExit = reader.IsDBNull(junctionExitColumnOrdinal) ? string.Empty : reader.GetString(junctionExitColumnOrdinal);

                    AddEnvBenUnknownMotorwayJunction(ref ebcUnknownMotorwayJunctionTemp, motorwayName, joiningRoad, joiningJunction, junctionEntry, junctionExit);
                }
                reader.Close();

                #endregion

                InitialiseDataObjects();

                // Update the class instances to the temp instances of the data
                ebcRoadCategoryCost = ebcRoadCategoryCostTemp;
                ebcHighValueMotorway = ebcHighValueMotorwayTemp;
                ebcHighValueMotorwayJunction = ebcHighValueMotorwayJunctionTemp;
                ebcUnknownMotorwayJunction = ebcUnknownMotorwayJunctionTemp;

                // Record the fact that the data was loaded.
                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business,
                        TDTraceLevel.Verbose, "Loading Environmental Benefits data completed"));
                }

                #endregion Load data into hashtables
            }
            catch (Exception e)
            {
                // Catching the base Exception class because we don't want any possibility
                // of this raising any errors outside of the class in case it causes the
                // application to fall over. As long as the exception doesn't occur in 
                // the final block of code, which copies the new data into the module-level
                // hashtables and arraylists, the object will still be internally consistant,
                // although the data will be inconsistant with that stored in the database.
                // One exception to this: if this is the first time LoadData has been run,
                // the exception should be raised.
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "An error occurred whilst attempting to reload the Environmental Benefits data.", e));

                if (       ((ebcRoadCategoryCost == null) || (ebcRoadCategoryCost.Count == 0))
                        || ((ebcHighValueMotorway == null) || (ebcHighValueMotorway.Count == 0))
                        || ((ebcHighValueMotorwayJunction == null) || (ebcHighValueMotorwayJunction.Count == 0))
                        || ((ebcUnknownMotorwayJunction == null) || (ebcUnknownMotorwayJunction.Count == 0))
                    )
                {
                    throw;
                }
            }
            finally
            {
                //close the database connection
                if (helper.ConnIsOpen)
                {
                    helper.ConnClose();
                }
            }
        }

        /// <summary>
        /// Creates new instances of the objects used to hold the data
        /// </summary>
        private void InitialiseDataObjects()
        {
            if (ebcRoadCategoryCost == null)
            {
                ebcRoadCategoryCost = new Dictionary<RoadCategoryCostKey, double>();
            }

            if (ebcHighValueMotorway == null)
            {
                ebcHighValueMotorway = new Dictionary<string, HighValueMotorwayData>();
            }

            if (ebcHighValueMotorwayJunction == null)
            {
                ebcHighValueMotorwayJunction = new Dictionary<HighValueMotorwayJunctionKey, HighValueMotorwayJunctionData>();
            }

            if (ebcUnknownMotorwayJunction == null)
            {
                ebcUnknownMotorwayJunction = new Dictionary<UnknownMotorwayJunctionKey, UnknownMotorwayJunctionData>();
            }
        }

        #region Add to dictionary methods

        /// <summary>
        /// Creates and adds a road category cost item to the suppied dictionary.
        /// If roadCategory or country cannot be parsed in to their equivalent enums, an error is logged 
        /// and the item not added
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="roadCategory"></param>
        /// <param name="country"></param>
        /// <param name="pencePerMile"></param>
        private void AddRoadCategoryCost(ref Dictionary<RoadCategoryCostKey, double> dictionary,
            string roadCategory, string country, double pencePerMile)
        {
            try
            {
                // Check theres a road category and country, otherwise cannot create a key
                if (!string.IsNullOrEmpty(roadCategory) && !string.IsNullOrEmpty(country))
                {
                    // Create the key
                    EBCRoadCategory ebcRoadCategory = (EBCRoadCategory)Enum.Parse(typeof(EBCRoadCategory), roadCategory, true);

                    EBCCountry ebcCountry = (EBCCountry)Enum.Parse(typeof(EBCCountry), country, true);

                    RoadCategoryCostKey key = new RoadCategoryCostKey(ebcRoadCategory, ebcCountry);

                    // Add to the dictionary
                    dictionary.Add(key, pencePerMile);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error,
                    string.Format(
                    "Error occurred parsing and adding an EnvBenRoadCategoryCost item RoadCategory[{0}] Country[{1}] Pence[{2}]. Exception: {3}",
                    roadCategory,
                    country,
                    pencePerMile,
                    ex.Message)));
            }
        }

        /// <summary>
        /// Creates and adds a high value motorway item to the suppied dictionary.
        /// If motorwayName is emptry or null, then the item is not added
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="motorwayName"></param>
        /// <param name="duplicateJunction"></param>
        /// <param name="allHighValue"></param>
        private void AddHighValueMotorway(ref Dictionary<string, HighValueMotorwayData> dictionary, 
            string motorwayName, string duplicateJunction, bool allHighValue)
        {
            try
            {
                // Check theres data, otherwise cannot create a key
                if (!string.IsNullOrEmpty(motorwayName))
                {
                    // Create the key
                    string key = motorwayName.ToLower().Trim();

                    // Create the data
                    HighValueMotorwayData data = new HighValueMotorwayData(duplicateJunction, allHighValue);

                    // Add to the dictionary
                    dictionary.Add(key, data);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error,
                    string.Format(
                    "Error occurred parsing and adding an EnvBenHighValueMotorway item MotorwayName[{0}] AllHighValue[{1}]. Exception: {2}",
                    motorwayName,
                    allHighValue,
                    ex.Message)));
            }
        }

        /// <summary>
        /// Creates and adds a high value motorway junction item to the suppied dictionary.
        /// If motorwayName, junctionStart, junctionEnd are null, or the country cannot be parsed in to its 
        /// equivalent enum, an error is logged  and the item not added
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="motorwayName"></param>
        /// <param name="junctionStart"></param>
        /// <param name="junctionEnd"></param>
        /// <param name="distance"></param>
        /// <param name="country"></param>
        private void AddEnvBenHighValueMotorwayJunction(ref Dictionary<HighValueMotorwayJunctionKey, HighValueMotorwayJunctionData> dictionary,
            string motorwayName, string junctionStart, string junctionEnd, int distance, string country)
        {
            try
            {
                // Check theres data, otherwise cannot create a key
                if ((!string.IsNullOrEmpty(motorwayName)) && (!string.IsNullOrEmpty(junctionStart)) && (!string.IsNullOrEmpty(junctionEnd)))
                {
                    // Create the key
                    motorwayName = motorwayName.ToLower().Trim();
                    junctionStart = junctionStart.ToLower().Trim();
                    junctionEnd = junctionEnd.ToLower().Trim();
                
                    // Convert the junction to decimal
                    decimal decJunctionStart = ConvertJunctionNumberToDecimal(junctionStart);
                    decimal decJunctionEnd = ConvertJunctionNumberToDecimal(junctionEnd);

                    HighValueMotorwayJunctionKey key = new HighValueMotorwayJunctionKey(motorwayName, decJunctionStart, decJunctionEnd);

                    // Create the value
                    EBCCountry ebcCountry = (EBCCountry)Enum.Parse(typeof(EBCCountry), country, true);

                    HighValueMotorwayJunctionData data = new HighValueMotorwayJunctionData(ebcCountry, distance);

                    // Add to the dictionary
                    dictionary.Add(key, data);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error,
                    string.Format(
                    "Error occurred parsing and adding an EnvBenHighValueMotorwayJunction item MotorwayName[{0}] JunctionStart[{1}] JunctionEnd[{2}] Distance[{3}] Country[{4}]. Exception: {5}",
                    motorwayName,
                    junctionStart,
                    junctionEnd,
                    distance,
                    country,
                    ex.Message)));
            }
        }

        /// <summary>
        /// Creates and adds an unknown motorway junction item to the suppied dictionary.
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="motorwayName"></param>
        /// <param name="joiningRoad"></param>
        /// <param name="joiningJunction"></param>
        /// <param name="junctionEntry"></param>
        /// <param name="junctionExit"></param>
        private void AddEnvBenUnknownMotorwayJunction(ref Dictionary<UnknownMotorwayJunctionKey, UnknownMotorwayJunctionData> dictionary,
            string motorwayName, string joiningRoad, string joiningJunction, string junctionEntry, string junctionExit)
        {
            try
            {
                // Check theres data, otherwise cannot create a key
                if (!string.IsNullOrEmpty(motorwayName) && !string.IsNullOrEmpty(joiningRoad))
                {
                    // Create the key
                    motorwayName = motorwayName.ToLower().Trim();
                    joiningRoad = joiningRoad.ToLower().Trim();

                    // Remove any spaces in the middle e.g. M6 Toll, A38 (M)
                    joiningRoad = Regex.Replace(joiningRoad, @"\s", string.Empty);

                    // joiningJunction can be an empty string, this will be set to -1
                    decimal decJoiningJunction = ConvertJunctionNumberToDecimal(joiningJunction);

                    UnknownMotorwayJunctionKey key = new UnknownMotorwayJunctionKey(motorwayName, joiningRoad, decJoiningJunction);

                    // Create the value
                    junctionEntry = junctionEntry.ToLower().Trim();
                    junctionExit = junctionExit.ToLower().Trim();

                    UnknownMotorwayJunctionData data = new UnknownMotorwayJunctionData(junctionEntry, junctionExit);
                   
                    // Add to the dictionary
                    dictionary.Add(key, data);
                }
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error,
                    string.Format(
                    "Error occurred parsing and adding an EnvBenUnknownMotorwayJunction item MotorwayName[{0}] JoiningRoad[{1}] JoiningJunction[{2}] JunctionEntry[{3}] JunctionExit[{4}] . Exception: {5}",
                    motorwayName,
                    joiningRoad,
                    joiningJunction,
                    junctionEntry,
                    junctionExit,
                    ex.Message)));
            }
        }

        #endregion

        /// <summary>
        /// Converts a junction number (e.g. 1, 1a, 1b) into a decimal (e.g. 1, 1.1, 1.2)
        /// </summary>
        /// <param name="junctionNumber"></param>
        /// <returns></returns>
        private decimal ConvertJunctionNumberToDecimal(string junctionNumber)
        {
            decimal decJunctionNumber = -1;
            bool containsLetter = false;

            if (!string.IsNullOrEmpty(junctionNumber))
            {
                junctionNumber = junctionNumber.ToLower();

                // If the junction is a toll road junction, then it will normally be like T1, T2
                // Therefore, strip off the "T" and treat like a normal junction number
                if (junctionNumber.StartsWith("t"))
                {
                    junctionNumber = junctionNumber.Remove(0, 1);
                }

                // Loop through the erroneous junction characters, and strip off until left with the junction.
                // This fixes the M4 junction "8/9" number problem, the logic below attempts to use the 
                // second number, i.e. "9" as the junction.
                // *** Add to the characters array if any other errorneous characters in the junction number
                // should follow this logic ***
                for (int i = 0; i < junctionCharacters.Length; i++)
                {
                    char character = junctionCharacters[i];

                    if (junctionNumber.IndexOf(character) >= 0)
                    {
                        // Remove the character and keep the remainder of the string starting after the character
                        int index = junctionNumber.IndexOf(character) + 1;
                        junctionNumber = junctionNumber.Substring(index, junctionNumber.Length - index);
                    }
                }


                // Loop through each junction letter and see if the junctionNumber contains it
                for (int i = 0; i < junctionLetters.Length; i++)
                {
                    char letter = junctionLetters[i];

                    if (junctionNumber.IndexOf(letter) > 0)
                    {
                        // Set flag to not do the conversion when exiting loop
                        containsLetter = true;

                        // Remove the letter
                        junctionNumber = junctionNumber.Substring(0, junctionNumber.IndexOf(letter));

                        // Append the number equivalent of the letter
                        junctionNumber = junctionNumber + "." + (i + 1).ToString();

                        // Convert to the decimal
                        decJunctionNumber = Convert.ToDecimal(junctionNumber);

                        break;
                    }
                }

                // The junction number did not contain a letter, so just convert to decimal
                if (!containsLetter)
                {
                    decJunctionNumber = Convert.ToDecimal(junctionNumber);
                }
            }

            return decJunctionNumber;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Method to initiate a reload of the environmental benefits data
        /// </summary>
        public void ReloadData()
        {
            Logger.Write(new OperationalEvent(TDEventCategory.Business,
                TDTraceLevel.Verbose, "Re-loading Environmental Benefits data"));

            LoadData();
        }

        /// <summary>
        /// Method returns the environmental benefit pence per mile value
        /// </summary>
        /// <param name="ebcRoadCategory"></param>
        /// <param name="ebcCountry"></param>
        /// <returns></returns>
        public double GetRoadCategoryCost(EBCRoadCategory ebcRoadCategory, EBCCountry ebcCountry)
        {
            RoadCategoryCostKey key = new RoadCategoryCostKey(ebcRoadCategory, ebcCountry);

            if (ebcRoadCategoryCost.ContainsKey(key))
            {
                return ebcRoadCategoryCost[key];
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Method returns true if the supplied road number is in the High Value Motorway list
        /// </summary>
        /// <param name="roadNumber"></param>
        /// <returns></returns>
        public bool IsHighValueMotorway(string roadNumber)
        {
            if (roadNumber != null)
            {
                roadNumber = roadNumber.ToLower().Trim();
            }
            else
            {
                return false;
            }

            if (ebcHighValueMotorway.ContainsKey(roadNumber))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method returns true if the road numner is in the High Value Motorway list and
        /// it has a flag inidicating it is All High Value
        /// </summary>
        /// <param name="roadNumber"></param>
        /// <returns></returns>
        public bool IsAllHighValueMotorway(string roadNumber)
        {
            if (roadNumber != null)
            {
                roadNumber = roadNumber.ToLower().Trim();
            }
            else
            {
                return false;
            }
            
            if (ebcHighValueMotorway.ContainsKey(roadNumber))
            {
                HighValueMotorwayData data = ebcHighValueMotorway[roadNumber];

                return data.allHighValue;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Method returns the motroway road number if the supplied road number is in the 
        /// High value motorways data and contains an entry for a duplicate motorway junction
        /// </summary>
        /// <param name="roadNumber"></param>
        /// <returns></returns>
        public string GetDuplicateMotorwayRoadNumber(string roadNumber)
        {
            if (roadNumber != null)
            {
                roadNumber = roadNumber.ToLower().Trim();
            }
            else
            {
                return string.Empty;
            }

            if (ebcHighValueMotorway.ContainsKey(roadNumber))
            {
                HighValueMotorwayData data = ebcHighValueMotorway[roadNumber];

                return data.duplicateJunction;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Method returns the distance by country for the high value motorway (between the junctions specified)
        /// </summary>
        /// <param name="roadNumber"></param>
        /// <returns></returns>
        public Dictionary<EBCCountry, double> GetHighValueMotorwayJunctionsDistance(string roadNumber, string startJunction, string endJunction)
        {
            roadNumber = roadNumber.ToLower().Trim();
            startJunction = startJunction.ToLower().Trim();
            endJunction = endJunction.ToLower().Trim();
            
            // Because the junction is stored as a decimal in the dictionary (for easier sorting and filtering),
            // convert the junction number into a decimal
            decimal decStartJunction = ConvertJunctionNumberToDecimal(startJunction);
            decimal decEndJunction = ConvertJunctionNumberToDecimal(endJunction);

            // Ensure the start junction is lower than the end junction
            if (decStartJunction > decEndJunction)
            {
                decimal tempJunction = decStartJunction;
                decStartJunction = decEndJunction;
                decEndJunction = tempJunction;
            }

            double distanceEngland = 0;
            double distanceScotland = 0;
            double distanceWales = 0;

            // Loop through each junction pair and if it matches the roadnumber and is between
            // the start and end junction numbers, update the appropriate totals
            foreach (KeyValuePair<HighValueMotorwayJunctionKey, HighValueMotorwayJunctionData> kvp in ebcHighValueMotorwayJunction)
            {
                if (kvp.Key.motorwayName.Equals(roadNumber))
                {
                    // -1 indicates the junction is not valid
                    if ((kvp.Key.junctionStart >= decStartJunction) && (kvp.Key.junctionEnd <= decEndJunction)
                        && (kvp.Key.junctionStart != -1) && (kvp.Key.junctionEnd != -1))
                    {
                        switch (kvp.Value.ebcCountry)
                        {
                            case EBCCountry.England:
                                distanceEngland += kvp.Value.distance;
                                break;
                            case EBCCountry.Scotland:
                                distanceScotland += kvp.Value.distance;
                                break;
                            case EBCCountry.Wales:
                                distanceWales += kvp.Value.distance;
                                break;
                        }
                    }
                }
            }

            Dictionary<EBCCountry, double> distances = new Dictionary<EBCCountry, double>();

            distances.Add(EBCCountry.England, distanceEngland);
            distances.Add(EBCCountry.Scotland, distanceScotland);
            distances.Add(EBCCountry.Wales, distanceWales);

            return distances;
        }


        /// <summary>
        /// Returns the Unknown motorway junction number if it exists for the road number and joining road
        /// </summary>
        /// <param name="roadNumber"></param>
        /// <param name="joiningRoadNumber"></param>
        /// <param name="entry">bool to return the entry or exit junction number</param>
        /// <returns></returns>
        public string GetUnknownMotorwayJunction(string roadNumber, string joiningRoadNumber, string joiningJunction, bool entry)
        {
            string unknownJunctionNumber = string.Empty;

            // If road number is null, no point attempting any checks
            if (roadNumber != null)
            {
                roadNumber = roadNumber.ToLower().Trim();
            }
            else
            {
                return unknownJunctionNumber;
            }

            if (joiningRoadNumber != null)
            {
                joiningRoadNumber = joiningRoadNumber.ToLower().Trim();
                
                // Remove any spaces in the middle e.g. M6 Toll, A38 (M)
                joiningRoadNumber = Regex.Replace(joiningRoadNumber, @"\s", string.Empty);
            }

            UnknownMotorwayJunctionKey key = new UnknownMotorwayJunctionKey(roadNumber, joiningRoadNumber, -1);

            // Get the default unknown junction for this road/joiningroad keu
            if (ebcUnknownMotorwayJunction.ContainsKey(key))
            {
                UnknownMotorwayJunctionData data = ebcUnknownMotorwayJunction[key];

                unknownJunctionNumber = (entry) ? data.junctionEntry : data.junctionExit;
            }


            // Handling of duplicate motorway unknown junctions problem, see DN11001f section 6.5.
            // When the joining junction is a valid number, then we need to get all keys/data for this road and joining road number.
            // Then based on the joining junction, return the appropriate unknown junction number.
            // e.g. if there are two keys/data "M6" "M6 TOLL" "5" and "M6" "M6 TOLL" "-1", and user provides 
            // joining junction "3", then return the data held for the first key, otherwise default to the other.
            
            decimal decJoiningJunction = -1;

            // Convert the junction number into a decimal to allow comparison
            if (!string.IsNullOrEmpty(joiningJunction))
            {
                decJoiningJunction = ConvertJunctionNumberToDecimal(joiningJunction);
            }

            if (decJoiningJunction >= 0)
            {
                // Loop through each unknown motorway junction and if it matches the roadnumber and joiningroad, 
                // and the joining junction is not -1, then get the key
                foreach (KeyValuePair<UnknownMotorwayJunctionKey, UnknownMotorwayJunctionData> kvp in ebcUnknownMotorwayJunction)
                {
                    if ((kvp.Key.motorwayName.Equals(roadNumber)) && (kvp.Key.joiningRoad.Equals(joiningRoadNumber))
                        && (kvp.Key.joiningJunction != -1))
                    {
                        // A record exists for the joining junction comparison.
                        
                        // Determine if this is the unknown junction to return
                        if (decJoiningJunction < kvp.Key.joiningJunction)
                        {
                            UnknownMotorwayJunctionData data = kvp.Value;

                            unknownJunctionNumber = (entry) ? data.junctionEntry : data.junctionExit;
                        }

                        // ASSUMES THERE WILL ONLY BE ONE RECORD WHERE -1 IS NOT THE JOINING JUNCTION
                        break;
                    }
                }
            }

            return unknownJunctionNumber;
        }

        #endregion
    }
}
