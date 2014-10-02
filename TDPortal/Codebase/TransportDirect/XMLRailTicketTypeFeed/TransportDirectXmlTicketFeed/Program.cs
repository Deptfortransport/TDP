// *********************************************** 
// NAME                 : Program.cs
// AUTHOR               : Steve Tsang
// DATE CREATED         : 26/06/2008
// DESCRIPTION			: XML Ticket Feed - Console Application
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/XMLRailTicketTypeFeed/TransportDirectXmlTicketFeed/Program.cs-arc  $ 
//
//   Rev 1.3   Dec 10 2008 10:24:42   mmodi
//Removed debugging line
//Resolution for 5118: RTTI XML Ticket Type feed - Logging updates
//
//   Rev 1.2   Oct 13 2008 16:45:02   build
//Automatically merged from branch for stream5014
//
//   Rev 1.1.1.0   Sep 19 2008 16:30:12   mmodi
//Updated to enable logging and return status code
//Resolution for 5118: RTTI XML Ticket Type feed - Logging updates

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Configuration;
using System.Xml;
using TransportDirect.Common.RailTicketType;
using TransportDirect.Common.Logging;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using Logger = System.Diagnostics.Trace;
using System.IO;

namespace TransportDirect.XmlTicketFeed
{
    /// <summary>
    /// Console application used for obtaining an XML Ticket feed from a URL and storing to database
    /// </summary>
    class Program
    {
        /// <summary>
        /// Starts the console application
        /// </summary>
        static int Main()
        {
            Console.WriteLine("Starting");

            int statusCode = 0;

            // Initialise
            statusCode = Initialise();
            
            if (statusCode == 0)
            {
                // Initialisation was ok
                try
                {
                    #region Get and save ticket types

                    string feedname = Properties.Current["TicketTypeFeed.FeedName"];
                    string filename = Properties.Current["TicketTypeFeed.XMLFeedSource"];

                    // Retrieve xml
                    Console.WriteLine("Retrieving xml from " + filename);

                    if (TDTraceSwitch.TraceVerbose)
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, "XML Ticket Feed - Retrieving xml from " + filename));

                    XmlDocument doc = HttpAccess.Instance.GetXMLFromHttp(filename);

                    // Convert xml
                    Console.WriteLine("Converting xml into tickets");

                    if (TDTraceSwitch.TraceVerbose)
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, "XML Ticket Feed - Converting xml into tickets"));

                    Tickets tickets = TicketParser.Instance.ConvertXMLToTickets(doc);


                    // Save xml
                    Console.WriteLine("Saving tickets to database");

                    if (TDTraceSwitch.TraceVerbose)
                        Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, "XML Ticket Feed - Saving tickets to database"));

                    tickets.SaveToDB();

                    #endregion

                    // Write back the new current feed time to the database 
                    if (!WriteNewTime(DateTime.Now, filename, feedname))
                    {
                        OperationalEvent logEvent = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error,
                            "Data Feed time was not successfully written to database for " + feedname);
                        Logger.Write(logEvent);
                    }

                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, "XML Ticket Feed - Import completed successfully"));

                    // Indicate success
                    statusCode = 0;

                }
                catch (TDException tdex)
                {
                    if (!tdex.Logged)
                        Console.WriteLine("XML Ticket Feed - TDException thrown: " + tdex.ToString());

                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "XML Ticket Feed - TDException thrown: " + tdex.Message));

                    statusCode = (int)tdex.Identifier;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("XML Ticket Feed - Exception thrown: " + ex.Message);

                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "XML Ticket Feed - Exception thrown: " + ex.Message));

                    statusCode = 1;
                }
            }
            
            Console.WriteLine("Exiting with errorcode " + statusCode);

            return statusCode;
        }

        /// <summary>
        /// Sets up the required services for the XML Ticket Feed application
        /// </summary>
        /// <returns>int 0 - Services started without error; int > 0 - error occured when starting services</returns>
        static int Initialise()
        {
            #region initialise
            
            TextWriterTraceListener logTextListener = null;

            // Get all the services needed for this program initialised
            try
            {
                // initialise .NET file trace listener for use prior to TDTraceListener
                string logfilePath = ConfigurationManager.AppSettings["DefaultLogFilepath"];
                Stream logFile = File.Create(logfilePath + "\\" + ConfigurationManager.AppSettings["propertyservice.applicationid"] + ".txt");
                logTextListener = new System.Diagnostics.TextWriterTraceListener(logFile);
                Trace.Listeners.Add(logTextListener);
                
                // Use the initialisation helper
                TDServiceDiscovery.Init(new TicketFeedInitialisation());

            }
            catch (TDException tdex)
            {
                string message = "XML Ticket Feed - Initialisation procedure failed: " + tdex.ToString();
                
                if (!tdex.Logged)
                    Console.WriteLine(message);

                Trace.WriteLine(message);

                return (int)tdex.Identifier; // indicates failure
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Trace.WriteLine(ex.Message);

                return 1; // indicates failure
            }
            finally
            {
                if (logTextListener != null)
                {
                    logTextListener.Flush();
                    logTextListener.Close();
                    Trace.Listeners.Remove(logTextListener);
                }
            }

            if (TDTraceSwitch.TraceVerbose)
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, "XML Ticket Feed - Initialise completed"));

            return 0; // status code 0 indicates success
                
            #endregion
        }

        /// <summary>
        /// Writes new file name and prepared time to ftp table.
        /// </summary>
        /// <param name="currentFeedDatetime">Prepared time</param>
        /// <param name="filename">file name</param>
        /// <param name="feedname">feed name</param>
        /// <returns></returns>
        static bool WriteNewTime(DateTime currentFeedDatetime, string filename, string feedname)
        {
            #region Log success

            bool wroteOk = false;

            SqlHelper sql = new SqlHelper();

            try
            {
                // Build the SQL query and then use it to retrieve the correct config data.
                string sqlQueryString;
                sqlQueryString = "UPDATE ftp_configuration set "
                    + "data_feed_datetime = '" + currentFeedDatetime.ToString("yyyy-MM-dd hh:mm:ss") + "', "
                    + "data_feed_filename = '" + filename + "' "
                    + "WHERE data_feed = '" + feedname + "' ";
                    
                //open connection to the database
                sql.ConnOpen(SqlHelperDatabase.DefaultDB);
                sql.Execute(sqlQueryString);
                sql.ConnClose();

                wroteOk = true;
            }
            finally
            {
                sql.ConnClose();
            }

            return wroteOk;

            #endregion
        }
    }
}
