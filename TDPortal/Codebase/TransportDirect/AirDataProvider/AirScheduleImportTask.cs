// ***********************************************
// NAME 		: AirScheduleImportTask.cs
// AUTHOR 		: Andrew Windley
// DATE CREATED : 17/05/2004
// DESCRIPTION 	: Implementation of class AirScheduleImportTask
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/AirScheduleImportTask.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:22   mturner
//Initial revision.
//
//   Rev 1.6   Mar 08 2005 13:37:56   esevern
//check-in on behalf of Richard Broddle.  Fix for vantive 3623300. Has been tested by Apps Support on BBP via temporary deployment of .dll, under Vantive no. 3646187. Should be added to Del 6.3.8  Maintenance release
//
//   Rev 1.6   Aug 27 2004 14:55:14   RBroddle
//Vantive 3623300 - modified PerformTask() to prevent TTBOAirBuilder & TTBOImportTask
//parts of import updating CJP air data should AirRouteMatrixImport fail 
//(eg due to unrecognised operator).
//
//   Rev 1.5   Aug 27 2004 14:55:14   CHosegood
//RTEL now issues ADFRTELProcessFailed instead of TTBOProcess failed.
//
//   Rev 1.4   Aug 16 2004 10:02:50   CHosegood
//Updated for new del 6 deliveries.

using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Datagateway.Framework;
using TransportDirect.Common.DatabaseInfrastructure;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.AirDataProvider
{
	/// <summary>
	/// Data Gateway import task invoked via the Data Gateway framework.
	/// Its purpose is to coordinate the 4-stage air schedule import process
	/// by calling 3rd party utilities and other import tasks.
	/// </summary>
    public class AirScheduleImportTask : ImportTask
    {
		#region Static properties
        // Delimeter for the parameters passed in
        private const char PARAM_DELIMITER = ' ';
		#endregion

		#region Constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="feed">The datafeed ID</param>
        /// <param name="params1">Parameters passed to the task</param>
        /// <param name="params2">Parameters passed to the task</param>
        /// <param name="utility">External executable used to perform the import if required</param>
        /// <param name="processingDirectory">The directory holding the data while the task is executing</param>
        public AirScheduleImportTask(string feed, string params1, string params2, string utility, string processingDirectory) 
            : base(feed, params1, params2, utility, processingDirectory)
        {
        }
		#endregion

		#region Overridden protected methods
        /// <summary>
        /// Override the base class PerformTask method. This method calls
        /// in turn the processes: RTEL, TTBOAirBuilder, TTBOImportTask
        /// and AirRouteMatrixImport.
        /// </summary>
        /// <returns>An int representing the return status</returns>
        protected override int PerformTask()
        {
            // Assume that everything will be successful.
            int result = 0;

            // Call RTEL process to generate inputs for TTBOAirBuilder and 
            // AirRouteMatrixImport (a .cif and .xml respectively)
            // Takes configuration from registry keys so no parameters passed
            result = CallRTELProcess();

            if (result == 0)
            {
                //RTEL succeeded, call the AirOperatorTask to convert the csv to an XML file for
                //input to the air route matrix task
                result = CallAirOperatorImport();

                if (result == 0)
                {

                    //AirOperatorImport succeeded, call AirRouteMatrixImport
                    result = CallAirRouteMatrixImport();

					if (result == 0)
					{
						// AirRouteMatrixImport succeeded, call TTBOAirBuilder.
						// Takes configuration from .ini files so requires no parameters passed
						result = CallTTBOAirBuilder();

						if (result == 0)
						{
							// TTBOAirBuilder succeeded, call TTBOImportTask,
							// passing it the relevant parameters from properties
							// Takes configuration from properties based on feedname so no parameters passed
							result = CallTTBOImportTask();

                            if (result == 0)
                            {
								#region successActions
                                //Success! Yippie!  Change the ChangeNotificaitonTable so the ChangeNotificaitonTable picks up
                                //the changes to the database.
                                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, 
                                    "Air schedule import task SUCCESS"));
                                SqlHelper helper = new SqlHelper();

                                Hashtable htParameters = new Hashtable();
                                Hashtable htTypes = new Hashtable();

                                htParameters.Add( "@tableList", "FlightRoutes,Operators" );
                                htTypes.Add( "@tableList", SqlDbType.VarChar  );

                                try 
                                {
                                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
                                        "Air schedule import opening connection for ChangeDataNotifcation update"));
                                    helper.ConnOpen( SqlHelperDatabase.AirDataProviderDB );

                                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
                                        "Air schedule updating ChangeDataNotifcation tables"));

									helper.Execute( "UpdateChangeNotificationTable", htParameters, htTypes );

                                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
                                        "Air schedule import closing connection for ChangeDataNotifcation update"));
                                    helper.ConnClose();
                                }
                                catch ( Exception e ) 
                                {
                                    result = (int)TDExceptionIdentifier.ADFDataChangeNotificaitonError;
                                    Logger.Write( new OperationalEvent(TDEventCategory.Business,
                                        TDTraceLevel.Error,
                                        "Error updating change notification tables: " + e.Message) );
                                }
                                finally 
                                {
                                    if ( helper.ConnIsOpen )
                                        helper.ConnClose();
                                }
								#endregion 
                            }
							else 
							{
								// Set error code to indicate TTBOImportTask failure
								result = (int)TDExceptionIdentifier.ADFTTBOImportTaskFailed;
							}
						}
						else 
						{
							// Set error code to indicate TTBOAirBuilder failure
							result = (int)TDExceptionIdentifier.ADFTTBOAirBuilderFailed;
						}
					}
					else
					{
						// AirRouteMatrix update failed.
						result = (int)TDExceptionIdentifier.ADFAirRouteMatrixImportFailedManualTTBORollbackRequired;
					} 
                }
                else 
                {
                    // AirOperatorImport update failed.
                    result = (int)TDExceptionIdentifier.ADFAirRouteMatrixImportFailedManualTTBORollbackRequired;
                }
            }
            else
            {
                // Set error code to indicate RTEL failure
                result = (int)TDExceptionIdentifier.ADFRTELProcessFailed;
            }
            return result;
        }
        /// <summary>
        /// Override of LogStart method
        /// </summary>
        protected override void LogStart()
        {
            Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, 
                "Air Schedule update begun for feed "+dataFeed));			
        }

        /// <summary>
        /// Override of LogFinish method
        /// </summary>
        protected override void LogFinish(int retCode)
        {
            if (retCode != 0) 
            {
                // Log failure
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, 
                    retCode.ToString()+ ": Air Schedule update failed for feed " +dataFeed));
            }
            else
            {
                // Log success
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, 
                    "Air Schedule update succeeded for feed " +dataFeed));			
            }
        }
		#endregion

		#region Private methods
        /// <summary>
        /// Call the RTEL process to create required output files
        /// </summary>
        /// <returns></returns>
        private int CallRTELProcess()
        {
            // Assume that everything will be successful
            int result = 0;

            if ( TDTraceSwitch.TraceInfo ) 
                Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, "RTEL process starting"));

            DirectoryInfo directory;
            FileInfo[] files;

            try
            {
                //Create the output directory if it does not exist
                directory = new DirectoryInfo( Properties.Current["datagateway.rtel.output.path"] );
                if (!directory.Exists) 
                {
                    if ( TDTraceSwitch.TraceVerbose ) 
                        Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "Creating RTEL output directory"));
                    directory.Create();
                }

                //Remove any existing files generated by an old RTEL process
                files = directory.GetFiles();
                foreach ( FileInfo file in files ) 
                {
                    //If the file is output of the RTEL process
                    if ( ( file.Name.Equals(Properties.Current["datagateway.rtel.output.cif"]) )
                        || ( file.Name.Equals(Properties.Current["datagateway.rtel.output.xml"]) ) 
                        || ( file.Name.ToUpper().StartsWith( Properties.Current[ "datagateway.rtel.output.prefix" ])) )
                    {
                        if ( TDTraceSwitch.TraceVerbose ) 
                            Logger.Write( new OperationalEvent( TDEventCategory.Business, TDTraceLevel.Verbose, new StringBuilder( "Attempting to remove file [" ) .Append( file.FullName ) .Append( "]" ).ToString() ));

                        //Make sure the file is not read only
                        File.SetAttributes( file.FullName, FileAttributes.Normal );
                        //Delete the file.
                        file.Delete();
                    }
                }

                if ( TDTraceSwitch.TraceVerbose ) 
                    Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "Calling RTEL process"));

                Process p = new Process();

                // Set up process attributes.
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = false;
                p.StartInfo.FileName = Properties.Current["datagateway.rtel.process"];

                // Start the RTEL process
                p.Start();
                p.WaitForExit();
                result = p.ExitCode;

                if ( TDTraceSwitch.TraceVerbose ) 
                    Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, new StringBuilder( "RTEL completed with a return code [" ).Append( result ).Append( "]" ).ToString()));
            }
            catch(Exception e)
            {
                result = (int)TDExceptionIdentifier.ADFRTELProcessFailed;
                OperationalEvent oe = new OperationalEvent(TDEventCategory.Business,
                    TDTraceLevel.Error,
                    new StringBuilder( "RTEL returned unexpected error code[ ").Append(result.ToString()).Append("]: ").Append( e.Message).ToString() );
                Logger.Write(oe);
            }

            if ( TDTraceSwitch.TraceInfo ) 
                Logger.Write( new OperationalEvent(TDEventCategory.Business,
                    TDTraceLevel.Info, "Renaming RTEL output files") );

            directory = new DirectoryInfo( Properties.Current["datagateway.rtel.output.path"] );
            files = directory.GetFiles();
            IEnumerator enu = files.GetEnumerator();

            bool xmlFound = false;
            bool cifFound = false;

            while ( enu.MoveNext() && ( !xmlFound || !cifFound )  )
            {
                //If this file is the cif output of the RTEL process rename
                //it to the TTBO Air builder expected input filename
                if ( ( ( (FileInfo) enu.Current ).Extension.ToLower().Equals( ".cif" ) )
                    && ( ( (FileInfo) enu.Current ).Name.ToUpper().StartsWith(Properties.Current[ "datagateway.rtel.output.prefix" ])) )
                {
                    if ( TDTraceSwitch.TraceVerbose ) 
                    {
                        Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                            new StringBuilder("Renaming [").Append( ( (FileInfo) enu.Current ).FullName ).Append("] to [").Append(Properties.Current["datagateway.rtel.output.cif"]).Append("]").ToString()) );
                    }

                    cifFound = true;
                    ( (FileInfo) enu.Current ).MoveTo(
                        ( (FileInfo) enu.Current ).Directory + @"\" + Properties.Current["datagateway.rtel.output.cif"] );
                }

                //If this file is the cif output of the RTEL process rename
                //it to the TTBO Air builder expected input filename
                if ( ( ( (FileInfo) enu.Current ).Extension.ToLower().Equals( ".xml" ) )
                    && ( ( (FileInfo) enu.Current ).Name.ToUpper().StartsWith(Properties.Current[ "datagateway.rtel.output.prefix" ])) )
                {
                    if ( TDTraceSwitch.TraceVerbose ) 
                    {
                        Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose,
                            new StringBuilder("Renaming [").Append( ( (FileInfo) enu.Current ).FullName ).Append("] to [").Append(Properties.Current["datagateway.rtel.output.xml"]).Append("]").ToString()) );
                    }

                    xmlFound = true;
                    ( (FileInfo) enu.Current ).MoveTo(
                        ( (FileInfo) enu.Current ).Directory + @"\" + Properties.Current["datagateway.rtel.output.xml"] );
                }
            }

            if ( !xmlFound ) 
            {
                result = (int) TDExceptionIdentifier.ADFRtelXmlOutputNotFound;
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Rtel XML Output Not Found"));
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, new StringBuilder( "RTEL cif file [").Append( Properties.Current["datagateway.rtel.output.xml"] )
                    .Append( "] output not found." ).ToString()));
            }

            if ( !cifFound ) 
            {
                result = (int) TDExceptionIdentifier.ADFRtelCifOutputNotFound;
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, new StringBuilder( "RTEL cif file [").Append( Properties.Current["datagateway.rtel.output.cif"] )
                    .Append( "] output not found." ).ToString()));
            }

            if ( TDTraceSwitch.TraceVerbose ) 
                Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, new StringBuilder( "RTEL process completed with result [" ).Append( result ) .Append("].").ToString()));

            return result;
        }

		/// <summary>
		/// Call the TTBOAirBuilder to create the TTBO database
		/// </summary>
		/// <returns></returns>
		private int CallTTBOAirBuilder()
		{
			// Assume that everything will be successful
			int statusCode = 0;

            if ( TDTraceSwitch.TraceInfo ) 
                Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, "TTBO Air Build process starting"));

			// Retrieve the relevant parameters from properties
			string ttboAirBuilderParameters = Properties.Current["datagateway.ttbo.airbuilder.process"];

			if(IsValidAirBuilderParameter(ttboAirBuilderParameters))
			{
				try
                {
                    if ( TDTraceSwitch.TraceVerbose ) 
                        Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "Calling TTBO Air builder process"));

					Process p = new Process();

					// Set up process attributes.
					p.StartInfo.UseShellExecute = false;
					p.StartInfo.CreateNoWindow = false; 
					p.StartInfo.FileName = GetAirBuilderFilename(ttboAirBuilderParameters);
					p.StartInfo.WorkingDirectory = GetAirBuilderWorkingDirectory(ttboAirBuilderParameters);
					// Start the TTBOAirBuilder
					p.Start();
					p.WaitForExit();
					// As we are calling a batch script the p.ExitCode is unreliable
					// Therefore, inspect the log file created and return exit code as appropriate
					statusCode = ParseAirBuilderLogForStatus();

                    if ( TDTraceSwitch.TraceVerbose ) 
                        Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, new StringBuilder( "TTBO Air builder process completed and returned code [").Append( statusCode ).Append( "]" ).ToString()));
				}
				catch(Exception e)
				{
					statusCode = (int)TDExceptionIdentifier.ADFTTBOAirBuilderUnexpectedError;
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Business,
                        TDTraceLevel.Error,
                        new StringBuilder( "TTBO air builder returned unexpected error code[ ").Append(statusCode.ToString()).Append("]: ").Append( e.Message).ToString() );
                    Logger.Write(oe);
				}
			}
			else
			{
				statusCode = (int)TDExceptionIdentifier.ADFTTBOAirBuilderInvalidParameters;
                OperationalEvent oe = new OperationalEvent(TDEventCategory.Business,
                    TDTraceLevel.Error,
                    new StringBuilder( "TTBO air builder has invalid parameters return code =").Append(statusCode.ToString()).ToString() );
                Logger.Write(oe);
			}

            if ( TDTraceSwitch.TraceVerbose ) 
                Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "TTBO Air Build process completed"));

			return statusCode;
		}

		/// <summary>
		/// Call the TTBOImportTask to load the TTBO database 
		/// created by the TTBOAirBuilder
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns></returns>
        private int CallTTBOImportTask()
        {
            // Assume that everything will be successful
            int result = 0;

            if ( TDTraceSwitch.TraceInfo ) 
                Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Info, "TTBO update task starting"));

            FileInfo importFile = new FileInfo( @Properties.Current["datagateway.ttbo.airbuilder.output"] );

            if ( !importFile.Exists ) 
            {
                result = (int)TDExceptionIdentifier.ADFTTBOInputFileNotFound;

                OperationalEvent oe = new OperationalEvent(TDEventCategory.Business,
                    TDTraceLevel.Error,
                    new StringBuilder( "TTBO import task input file[").Append(@Properties.Current["datagateway.ttbo.airbuilder.output"]).Append("] does not exist").ToString() );
                Logger.Write(oe);
            }
            else
            {
                TTBOImportTask ttboImport = new TTBOImportTask( Properties.Current["datagateway.airbackend.schedules.feedname"], string.Empty, string.Empty, string.Empty, string.Empty );
                result = ttboImport.Run( importFile.FullName );
            }

            if ( TDTraceSwitch.TraceVerbose ) 
                Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, new StringBuilder( "TTBO update task completed with return code [" ).Append( result ).Append( "]" ).ToString() ));

            return result;
        }

        /// <summary>
        /// Call the AirRouteMatrixImport to update the portal operator data
        /// </summary>
        /// <returns>0 if successful, otherwise an error code</returns>
        private int CallAirOperatorImport()
        {
            // Assume that everything will be successful
            int result = 0;

            if ( TDTraceSwitch.TraceVerbose ) 
                Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "Air Operator Import Task update task starting"));

            try 
            {
                AirOperatorImportTask operatorImport = new AirOperatorImportTask(Properties.Current["datagateway.airbackend.airoperator.feedname"], "", "", "", "" );
                result = operatorImport.Run( Properties.Current["datagateway.data.live.directory"] + @"\airOperators.csv" );
            } 
            catch ( TDException tde ) 
            {
                result = (int)tde.Identifier;
                Logger.Write(new OperationalEvent(TDEventCategory.Business, 
                    TDTraceLevel.Error, new StringBuilder( "An error code [" ).Append(result).Append("] occured while calling air operator import task: " ).Append( tde.Message ).ToString(), tde));
            }
            catch ( Exception e ) 
            {
                result = (int) TDExceptionIdentifier.ADFAirOperatorUnexpectedError;
                Logger.Write(new OperationalEvent(TDEventCategory.Business, 
                    TDTraceLevel.Error, new StringBuilder( "Unexpected error occured while calling air operator import task: " ).Append( e.Message ).ToString(), e));
            }

            if ( TDTraceSwitch.TraceVerbose ) 
                Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "Air Operator Import Task update task completed"));

            return result;
        }

		/// <summary>
		/// Call the AirRouteMatrixImport to update the portal data sets
		/// </summary>
        /// <returns>0 if successful, otherwise an error code</returns>
		private int CallAirRouteMatrixImport()
		{
			// Assume that everything will be successful
			int result = 0;

            if ( TDTraceSwitch.TraceVerbose ) 
                Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "Air route matrix Import Task update task starting"));

//            if ( TDTraceSwitch.TraceVerbose ) 
//            {
//                IList operators;
//                IList airports;
//
//                operators = new ArrayList();
//                airports = new ArrayList();
//
//                XmlReader reader;
//                reader = new XmlTextReader( new StringBuilder(@Properties.Current["datagateway.rtel.output.path"].TrimEnd( '/' )).Append( @"\" ).Append( @Properties.Current["datagateway.rtel.output.xml"]).ToString() );
//
//                while ( reader.Read() ) 
//                {
//                    switch ( reader.NodeType ) 
//                    {
//                        case XmlNodeType.Element:
//                            if (reader.HasAttributes) 
//                            {
//                                for (int i = 0; i < reader.AttributeCount; i++) 
//                                {
//                                    reader.MoveToAttribute( i );
//
//                                    if ( reader.Name.Equals( "origin" ) || reader.Name.Equals( "destination" ) ) 
//                                        if (!airports.Contains( reader.Value ) ) 
//                                            airports.Add( reader.Value );
//
//                                    if ( reader.Name.Equals( "operator" ) ) 
//                                        if (!operators.Contains( reader.Value ) ) 
//                                            operators.Add( reader.Value );
//                                }
//                            }
//                            break;
//                        default:
//                            break;
//                    }
//                }
//
//                IEnumerator en = airports.GetEnumerator();
//                while ( en.MoveNext() ) 
//                {
//                    //Debug.WriteLine( "INSERT INTO tempAirports(code) VALUES ('" +en.Current +"')" );
//                    Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "INSERT INTO tempAirports(code) VALUES ('" +en.Current +"')"));
//                }
//                Debug.WriteLine( string.Empty);
//
//                en = operators.GetEnumerator();
//                while ( en.MoveNext() ) 
//                {
//                    //Debug.WriteLine( "INSERT INTO tempOperators(code) VALUES ('" +en.Current +"')" );
//                    Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "INSERT INTO tempOperators(code) VALUES ('" +en.Current +"')"));
//                }
//            }

            AirRouteMatrixImportTask routeImport = new AirRouteMatrixImportTask(Properties.Current["datagateway.airbackend.routematrix.feedname"], "", "", "", "" );
            result = routeImport.Run( new StringBuilder(@Properties.Current["datagateway.rtel.output.path"].TrimEnd( '/' )).Append( @"\" ).Append( @Properties.Current["datagateway.rtel.output.xml"]).ToString() );

            if ( TDTraceSwitch.TraceVerbose ) 
                Logger.Write( new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, "Air route matrix Import Task update task completed with return code " + result ));

			return result;
		}

		/// <summary>
		/// Confirm that we can split supplied parameter into two to extract 
		/// the TTBOAirBuilder filename and working directory
		/// </summary>
		/// <param name="parameters"></param>
		/// <returns>True, if parameter is valid format</returns>
		private bool IsValidAirBuilderParameter(string parameters)
		{
			// Split the parameter string
			string[] subparameters = parameters.Split(new char[]{PARAM_DELIMITER});
			// We expect to get two parameters
			return (subparameters.Length == 2);
		}

		/// <summary>
		/// Gets the TTBOAirBuilder filename string
		/// </summary>
		/// <param name="parameters"></param>
		private string GetAirBuilderFilename(string parameters)
		{
			// Split the parameter string, return first string
			return parameters.Split(new char[]{PARAM_DELIMITER})[0];
		}

		/// <summary>
		/// Gets the TTBOAirBuilder working directory
		/// </summary>
		/// <param name="parameters"></param>
		private string GetAirBuilderWorkingDirectory(string parameters)
		{
			// Split the parameter string, return second string
			return parameters.Split(new char[]{PARAM_DELIMITER})[1];
		}

		/// <summary>
		/// Parses the TTBOAirBuilder makelog for status
		/// </summary>
		/// <returns></returns>
		private int ParseAirBuilderLogForStatus()
		{
			// Assume success
			int statusCode = 0;
			// Read the log file to parse for status
			string data = GetAirBuilderLogData();

			if (data != null)
			{
				// TTBOAirBuilder executed successfully if log contains 'Ready!'
				int index = data.IndexOf("Ready!");

				if (index == -1)
				{
					statusCode = (int)TDExceptionIdentifier.ADFTTBOAirBuilderReportedNotReady;
					OperationalEvent oe = new OperationalEvent(TDEventCategory.Business,
						TDTraceLevel.Error, statusCode.ToString()+ ": TTBOAirBuilder log file indicates failure");
					Logger.Write(oe);
				}
			}
			else
			{
				statusCode = (int)TDExceptionIdentifier.ADFTTBOAirBuilderLogReadFailed;
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Business,
					TDTraceLevel.Error, statusCode.ToString()+ ": TTBOAirBuilder log file may not exist or be empty");
				Logger.Write(oe);
			}

			return statusCode;
		}

		/// <summary>
		/// Gets the contents of the TTBOAirBuilder log file
		/// </summary>
		/// <returns></returns>
		private string GetAirBuilderLogData()
		{
			// Retrieve log filename from properties
			string filename = Properties.Current["datagateway.ttbo.airbuilder.log"];
			// Open log file
			StreamReader reader = File.OpenText(filename);
			// The log file is terse, therefore fetch entire log as one string
			string log = reader.ReadToEnd();
			// Close reader and return log
			reader.Close();

			return log;
		}
		#endregion
	}
}
