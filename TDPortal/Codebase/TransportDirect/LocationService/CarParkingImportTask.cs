// *********************************************** 
// NAME                 : CarParkingImportTask.cs
// AUTHOR               : Tim Mollart
// DATE CREATED         : 11 August 2006
// DESCRIPTION  		: Imports a Car Park XML file to the database 
//                        using a SQL Server stored procedure
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/CarParkingImportTask.cs-arc  $
//
//   Rev 1.4   Feb 18 2009 13:34:48   apatel
//Importer updated to chunk the Car Park xml to smaller car park xml to pass to sql server. Also, its updated to pass firstfeed and lastfeed boolean values to the ImportingCarParkData stored procedure. Cleanup database procedure running at the first file been removed.
//Resolution for 5254: Car Parking Importer Update
//
//   Rev 1.3   Mar 10 2008 15:18:46   mturner
//Initial Del10 Codebase from Dev Factory
//
//  Rev Dev Factory   Feb 11 2008 18:15:0   mmodi
//Reordered car parking Run method to validate data, then clean up and run OS Map loader
//
//  Rev DevFactory Feb 03 2008 22:16:00 apatel  
//  Modified imported to go through all the files even there is an error and made it to show error at the end.
// 
//   Rev Devfactory Jan 26 16:14:00 apatel
//   CCN 0426 Character Encoding Validation Check for data files added in run method.
//   Run Method modified to run procedure to clean up data base tables only when importing the first line to database.
//   CleanUpDatabaseFiles() method added to clean up data base tables.
//   news static boolean variable firsttime added and constant string CleanUpStoredProcedureKey added.
//
//   Rev 1.0   Nov 08 2007 12:25:02   mturner
//Initial revision.
//
//   Rev 1.3   Dec 04 2006 13:36:00   tmollart
//Modified to have a command time out value.
//Resolution for 4282: Car Parking data importer timeouts after 30 seconds and does not complete.
//
//   Rev 1.2   Sep 21 2006 14:51:34   tmollart
//Modications made so OS Map Loader called on method called.
//Resolution for 4192: The carpark data importer elements need to be integrated
//
//   Rev 1.1   Sep 20 2006 14:49:02   tmollart
//Modified class to call a batch file to load OS Map data.
//Resolution for 4192: The carpark data importer elements need to be integrated
//
//   Rev 1.0   Aug 21 2006 16:45:58   tmollart
//Initial revision.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using System.Data;
using System.Data.SqlClient;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Datagateway.Framework;
using TransportDirect.Common.PropertyService.Properties;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Summary description for CarParkingImportTask.
	/// </summary>
	public class CarParkingImportTask : DatasourceImportTask
	{
        private enum CarParkingTableEnum
        {
            CarParkingAttractions,
            CarParkType,
            LinkedNaPTANs,
            Spaces,
            Concessions,
            PaymentType,
            NPTGLocality, 
            AdditionalData,//the order of the table enum values above this should never change
            AccessPoints,
            CarParkOperator,
            TrafficNewsRegion,
            Attractions,
            Facilities,
            Calendar,
            ParkAndRideScheme,
            NPTGAdminDistrict,
            SpaceType,
            CarParkChargeType,
            CarParkCharges,
            OpeningTimes,
            SpaceAvailability,
            CarParkingFacilities,
            PaymentMethod

            
        }

		/// <summary>
		/// Feed Description
		/// </summary>
		private const string feedDescription = "carparking";

		/// <summary>
		/// Name of the datafeed for error reporting.
		/// </summary>
		private const string logDescription = "Car Park Import" ;

		/// <summary>
		/// Holds the parameter passed in to the constuctor for use in the Run method.
		/// </summary>
		private string parameter1;

        /// <summary>
        /// Boolean value to check if the Run method is ran for first time.
        /// </summary>
        private static bool firstTimeRunDatabaseParameter1 = true;
        private static bool firstFeed = true;

       
        /// <summary>
        /// Boolean value to check if the Run method is running for the last time
        /// </summary>
        private bool lastFeed = true;

        private string commandDefaultTimeOutKey = "datagateway.default.sqlimport.sqlcommandtimeout";
        private string carParksToPassSqlServer = "datagateway.sqlimport.carparking.carparksinxmltopasssql";

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="feed"></param>
		/// <param name="params1"></param>
		/// <param name="params2"></param>
		/// <param name="utility"></param>
		/// <param name="processingDirectory"></param>
		public CarParkingImportTask(string feed, string params1, string params2, string utility, string processingDirectory) : base(feed, params1, params2, utility, processingDirectory)
		{
			// Get the name of this import
			string feedName = Properties.Current["datagateway.sqlimport.carparking.feedname"];					    
			
			// Parse the inherited keys and populate them with feed specific details.
			xmlschemaLocationKey = string.Format(CultureInfo.InvariantCulture, xmlschemaLocationKey, feedDescription);
			xmlNamespaceKey = string.Format(CultureInfo.InvariantCulture, xmlNamespaceKey, feedDescription);
			databaseKey = string.Format(CultureInfo.InvariantCulture, databaseKey, feedDescription);
			storedProcedureKey = string.Format(CultureInfo.InvariantCulture, storedProcedureKey, feedDescription);
			commandTimeOutKey = string.Format(CultureInfo.InvariantCulture, commandTimeOutKey, feedDescription);

			// Store the parameter in the class
			parameter1 = params1;

			// Check the feed name
			if ( !dataFeed.Equals(feedName)) 
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, logDescription + " unexpected feed name: [" + dataFeed + "]"));			
			}
		}

        /// <summary>
        /// overloaded run method to accept extra parameter last to confirm if the current file is the last file.
        /// </summary>
        /// <param name="filename">name of the file to process</param>
        /// <param name="last">if true the current file to process is the last file in the feed</param>
        /// <returns></returns>
        public int Run(string filename, bool last)
        {
            lastFeed = last;
            return Run(filename);
        }

		/// <summary>
		/// Overridden Run method. For reference only and just calls run on the base class after running the OS Map Loader.
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public override int Run(string filename)
		{
            
			int statusCode = 0;

            
            // validates the whole file line by line for character data.
            statusCode = ValidateCharacterEncoding(filename);


            
            if (statusCode != 0)
            {
                return statusCode;
            }


            // Call the batch file passed in param 1 only once, will be the OS Map car park data importer
            if (firstTimeRunDatabaseParameter1)
            {
                firstTimeRunDatabaseParameter1 = false;

                if (!string.IsNullOrEmpty(parameter1))
                {
                    Process p = new Process();
                    p.StartInfo.UseShellExecute = true;
                    p.StartInfo.CreateNoWindow = true;
                    p.StartInfo.FileName = parameter1;
                    p.Start();
                    p.WaitForExit();
                    statusCode = p.ExitCode;
                }
            }
            
			if (statusCode != 0)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, logDescription + "OS Map Loader call failed with a status code of " + statusCode.ToString()));
                return 1;
			}
			else
			{

				// Included for clarity only. Calls base class run method.
				return base.Run(filename);
                
			}
		}


        /// <summary>
        /// Reads each line of data file and gets the data out of it ignoring xml elements, comments.
        /// then for each piece of data it gets the character and checks if its between ascii values 32 and 127
        /// </summary>
        /// <param name="filename">Name of the data file</param>
        /// <returns>0 if valid else 4027</returns>
        private int ValidateCharacterEncoding(string filename)
        {
            int statusCode = 0;

            char[] testCharArray;

            //Read file in a stream reader;
            using (StreamReader reader = new StreamReader(ProcessingDirectory + "\\" + filename, Encoding.UTF8))
            {
                

                int rowCount = 0; // this will trace the current line number

                while (!reader.EndOfStream)
                {
                    string teststring = reader.ReadLine();

                    rowCount++;

                    //Regex pattern which will grab only data part of the line.
                    string regPattern = @"<\w+>(.*?)</\w+>";

                    Regex testreg = new Regex(regPattern);
                    Regex testreghelper = new Regex(regPattern);

                    MatchCollection matches = testreg.Matches(teststring);

                    foreach (Match match in matches)
                    {
                        if (match.Success)
                        {
                            foreach (Group gp in match.Groups)
                            {
                                
                                // Checkin again just to make sure we not get the complete outer xml for the current xml line.
                                if (!testreghelper.IsMatch(gp.Value))
                                {
                                    string sp = gp.Value;
                                    testCharArray = sp.ToCharArray();

                                    foreach (char testChar in testCharArray)
                                    {
                                        // if not valid char log error and change status code to non zero value;
                                        if (NotValidChar(testChar))
                                        {
                                            //log error at line no and column no
                                            int columnno = teststring.IndexOf(testChar, 0);
                                            string errorMsg = "Error at line no " + rowCount + " at column no " + columnno + " with char "+ testChar +"; Data: " + teststring;

                                            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, logDescription + "Car Park Data feed encoding validation failed in file"+ filename + " with " + errorMsg));
                                            statusCode = (int)TDExceptionIdentifier.DGInputXMLFileHasInvalidCharacters;

                                        }
                                    }
                                }

                            }
                        }
                    }

                }
            }
            return statusCode;
        }


        /// <summary>
        /// Checks if the char parameter is between 32 and 127
        /// </summary>
        /// <param name="testChar">char to check </param>
        /// <returns>true if char is invalid</returns>
        private bool NotValidChar(char testChar)
        {
            bool notvalid = false;

            int charno = (int)testChar;

            if (charno < 32 || charno > 127)
            {
                notvalid = true;
            }

            return notvalid;
        }

        /// <summary>
        /// Override the base class PerformTask method.
        /// </summary>
        /// <returns></returns>
        protected override int PerformTask()
        {
            int result = 0;

            //Extract the XML string from the file
            string xmlString = LoadXML();

            //Validate the XML file against the XSD schema specified in the XML file
            ValidateXML();

            //Update the xml to remove attributes that SQL Server will get confused by
            xmlString = UpdateXML(xmlString);

            

            //If the process has been successful so far.
            if (result == 0)
            {
                List<string> carParkXmlStrings = GetCarParkXmlStrings(xmlString);

                int count = 0;

                
                foreach (string carParkXml in carParkXmlStrings)
                {
                    count++;

                    string xmlStringPart = string.Format("<{0}>{1}</{2}>", "CarParkDataImport", carParkXml, "CarParkDataImport");

                    //Upload the XML into the database

                    if (lastFeed && count == carParkXmlStrings.Count)
                    {
                        result = ImportDataIntoSQLServer(xmlStringPart, true);
                    }
                    else
                    {
                        result = ImportDataIntoSQLServer(xmlStringPart, false);
                    }

                    if (result != 0)
                    {
                        break;
                    }

                }

              
               
               
            }

            
            //If we have reached this point then the import was successful
            return result;
        }

       /// <summary>
       /// Generates a list of strings with each string representing smaller number of car parks from the carparks included
       /// in the xml string representing xml file feed.
       /// 
       /// Depend on the value of the property set for carParkstoPassSqlServer it generates a string with number of carparks and adds to the list.
       /// 
       /// The method uses regular expressions to separate each car park from the car park xml.
       /// </summary>
       /// <param name="xmlString">Car Park xml string</param>
       /// <returns>list of strings each representing smaller part of the car park xml</returns>
        private List<string> GetCarParkXmlStrings(string xmlString)
        {
            int count = 0;

            string carParkXmlString = string.Empty;

            List<string> carParkXmlStrings = new List<string>();

            int carParksToPass = 500;

            string xmlParts = Properties.Current[carParksToPassSqlServer];

            int.TryParse(xmlParts, out carParksToPass);

            //Regex pattern which will grab only data part of the line.
            string regPattern = @"<CarPark>(.*?\s*)</CarPark>";

            Regex testreg = new Regex(regPattern,RegexOptions.Multiline | RegexOptions.Singleline);

            MatchCollection matches = testreg.Matches(xmlString);

            foreach (Match match in matches)
            {
               count++;

               carParkXmlString += match.ToString();

               if (count % carParksToPass == 0 || count == matches.Count)
               {
                    carParkXmlStrings.Add(carParkXmlString);

                    carParkXmlString = string.Empty;
               }
            }

            return carParkXmlStrings;

        }


        /// <summary>
        /// Import the XML into the database. The XML string is passed directly
        /// into a stored procedure. The stored procedure does all the work
        /// extracing the appropriate data from the XML into the database tables.
        /// </summary>
        /// <param name="XMLString">XML string to import car parks from</param>
        /// <param name="lastFeed">boolean value representing if the xmlString is last in the feed</param>
        protected int ImportDataIntoSQLServer(string XMLString, bool lastFeed)
        {
            int result = 0;
            int commandTimeOut = -1;


            SqlHelperDatabase datasource;
            string database = Properties.Current[databaseKey];
            string storedProcedureName = Properties.Current[storedProcedureKey];

            // Assess if a command time out needs to be used - if not the DB instance default will be used
            if (Properties.Current[commandTimeOutKey] != null)
            {
                //use import specific timeout if present
                commandTimeOut = int.Parse(Properties.Current[commandTimeOutKey]);
            }
            else if (Properties.Current[commandDefaultTimeOutKey] != null)
            {
                //alternatively use the generic property for all gateway imports if present
                commandTimeOut = int.Parse(Properties.Current[commandDefaultTimeOutKey]);
            }

            if (string.Empty.Equals(database))
            {
                result = (int)TDExceptionIdentifier.DGStoredProcedureUnspecified;
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Database not specified in Properties"));
            }

            if ((result == 0) && (string.Empty.Equals(storedProcedureName)))
            {
                result = (int)TDExceptionIdentifier.DGDatabaseUnspecified;
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Stored procedure not specified in Properties"));
            }

            if (result == 0)
            {
                datasource = (SqlHelperDatabase)Enum.Parse(typeof(SqlHelperDatabase), database, true);

                //Object for managing database calling
                SqlHelper sqlHelper = new SqlHelper();

                //Open the database connection
                try
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, "Connecting to " + datasource.ToString() + " database."));
                    sqlHelper.ConnOpen(datasource);
                }
                catch (Exception e)
                {
                    result = (int)TDExceptionIdentifier.DGImportDBConnectionError;
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Import database connection error: " + e.Message));
                }

                if (result == 0)
                {
                    //Call the stored procedure
                    try
                    {
                        Hashtable hashParams = new Hashtable();
                        hashParams.Add("@XML", XMLString);
                        hashParams.Add("@LastFeed", lastFeed);
                        hashParams.Add("@FirstFeed", firstFeed);

                        firstFeed = false;
                       
                        Hashtable hashTypes = new Hashtable();

                        //The parameter must be set as database type of Text otherwise it will not work
                        hashTypes.Add("@XML", SqlDbType.Text);
                        hashTypes.Add("@LastFeed", SqlDbType.Bit);
                        hashTypes.Add("@FirstFeed", SqlDbType.Bit);

                        if (TDTraceSwitch.TraceVerbose)
                        {
                            IDictionaryEnumerator myEnumerator = hashParams.GetEnumerator();
                            while (myEnumerator.MoveNext())
                                Logger.Write(new OperationalEvent(TDEventCategory.Business,
                                    TDTraceLevel.Verbose, "Element:: " + string.Format("\t{0}:\t{1}", myEnumerator.Key, myEnumerator.Value)));
                        }

                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, "Calling DatasourceImportTask SP on " + database + " database."));

                        // Execute one type of command if the command time out is set or
                        // the overridden method if its not
                        if (commandTimeOut == -1)
                        {
                            sqlHelper.Execute(storedProcedureName, hashParams, hashTypes);
                        }
                        else
                        {
                            sqlHelper.Execute(storedProcedureName, hashParams, hashTypes, commandTimeOut);
                        }

                    }
                    catch (Exception e)
                    {
                        result = (int)TDExceptionIdentifier.DGImportStoredProcedureError;
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Import stored procedure[" + storedProcedureName + "] calling error: " + e.Message));
                        throw new TDException("Import stored procedure[" + storedProcedureName + "] calling error: " + e.Message, e, true, TDExceptionIdentifier.DGImportStoredProcedureError);
                    }
                }
            }
            return result;
        }

	}
}
