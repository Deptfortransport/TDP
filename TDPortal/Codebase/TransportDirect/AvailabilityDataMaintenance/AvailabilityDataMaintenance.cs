// *********************************************** 
// NAME			: AvailabilityDataMaintenance.cs
// AUTHOR		: James Broome
// DATE CREATED	: 26/01/2005
// DESCRIPTION	: Handles main processing of console application
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AvailabilityDataMaintenance/AvailabilityDataMaintenance.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:50   mturner
//Initial revision.
//
//   Rev 1.2   Mar 21 2005 10:52:50   jbroome
//Minor updates after code review
//
//   Rev 1.1   Feb 17 2005 14:48:44   jbroome
//Updated internal maintenance routines
//Resolution for 1923: DEV Code Review : Availability Estimator
//
//   Rev 1.0   Feb 08 2005 10:38:16   jbroome
//Initial revision.

using System;
using System.Text;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Globalization;
using System.Collections;
using System.Configuration;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.AvailabilityDataMaintenance
{
	/// <summary>
	/// Main class of the application. Provides the entry point
	/// for the application, which calls the necessary maintenance
	/// routine.
	/// </summary>
	class AvailabilityDataMaintenance
	{
		
		#region Private members

		// string constants used to call maintenance routines
		private const string archiveHistory		= "/arc_hist";
		private const string deleteUnavailable	= "/del_unav";
		private const string exportProfile		= "/exp_prof";
		private const string help				= "/help";

		// Stored Procs
		private const string SPGetAvailabilityHistory =		"GetAvailabilityHistory";
		private const string SPDeleteAvailabilityHistory =	"DeleteAvailabilityHistory";
		private const string SPGetProductProfiles =			"GetProductProfiles";
		private const string SPDeleteUnavailableProducts =	"DeleteUnavailableProducts";

		// Params
		private const string paramXml =	"@Xml"; 
		private const string paramHistoryId = "@HistoryId";

		#endregion

		#region Main entry point

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static int Main(string[] args)
		{
			int returnCode = 0;
			
			// Uncomment this code for debugging purposes
			// Console.WriteLine("Attach debugger to process and press enter...");
			// Console.ReadLine();

			Console.WriteLine("Starting...\n");

			try
			{
				TDServiceDiscovery.Init(new AvailabilityDataMaintenanceInitialisation());
				
				AvailabilityDataMaintenance app = new AvailabilityDataMaintenance();
				
				if ((args.Length) > 0 && (args[0].Trim().Length > 0) )
				{
					// Possible args are:
					// /arc_hist - Archives AvailabilityHistory data
					// /del_unav - Deletes UnavailabileProducts data
					// /exp_prof - Exports ProductProfile data
					// /help - Displays help text

					if (String.Compare(args[0], help, true, CultureInfo.CurrentCulture ) == 0)
					{
						Console.WriteLine(Messages.HelpMessage);
						returnCode = 0;
					}
					else if (String.Compare( args[0], archiveHistory, true, CultureInfo.CurrentCulture ) == 0)
					{
						returnCode = app.ArchiveAvailabilityHistory();
					}
					else if (String.Compare( args[0], deleteUnavailable, true, CultureInfo.CurrentCulture ) == 0)
					{
						returnCode = app.DeleteUnavailableProducts();
					}
					else if (String.Compare( args[0], exportProfile, true, CultureInfo.CurrentCulture ) == 0)
					{
						returnCode = app.ExportProductProfiles();
					}
					else
					{
						// Invalid argument specified
						Console.WriteLine(Messages.InvalidArgs + Messages.HelpMessage);
						Console.ReadLine();
						returnCode = (int)TDExceptionIdentifier.AEInvalidArguments;
					}
				}
				else
				{
					// No arguments specified - do not know which utility to run
					Console.WriteLine(Messages.NoArgs + Messages.HelpMessage);
					Console.ReadLine();
					returnCode = (int)TDExceptionIdentifier.AENoArguments;
				}
			}
			catch (TDException tdEx)
			{
				// Log error (cannot assume that TD listener has been initialised)
				if (!tdEx.Logged)
				{
					Trace.Write(String.Format(CultureInfo.CurrentCulture, Messages.Util_Failed, tdEx.Message, tdEx.Identifier));
				}
				Console.Write(String.Format(CultureInfo.CurrentCulture, Messages.Util_Failed, tdEx.Message, tdEx.Identifier));

				returnCode = (int)tdEx.Identifier;
			}
			Console.WriteLine("Exiting with errorcode "+ returnCode);
			return returnCode;

		}

		# endregion

		#region Private Methods
	
		/// <summary>
		/// Performs the task of writing the Availability History data 
		/// to a text file, then deleting the data from the database
		/// </summary>
		/// <returns>Number of records successfully archived</returns>
		private int ArchiveAvailabilityHistory()
		{
			SqlHelper sqlHelper = new SqlHelper();
			SqlDataReader drResults = null;
			int archiveStatus = 0;
			StringBuilder sb = new StringBuilder();
			char comma = ',';
			int i = 0;
			int rowId = 0;
			string filePath = Properties.Current["AvailabilityDataMaintenance.AvailabilityHistory.ArchiveDirectory"];
			// Filename created using datetime stamp
			string fileName = filePath + DateTime.Now.ToString("yyyyMMddhhmmss", CultureInfo.CurrentCulture) + ".txt";	
		
			try
			{
				// Open DB connection 
				sqlHelper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);

				// Call Stored Procedure
				drResults = sqlHelper.GetReader(SPGetAvailabilityHistory);
			
				#region Set up text file
				// Create text file and assign it a stream writer
				StreamWriter sw = File.CreateText(fileName);
				// Format column headers
				for (i = 0; i < drResults.FieldCount; i++)
				{					
					sb.Append(drResults.GetName(i).ToString(CultureInfo.InvariantCulture));
					if (i!= drResults.FieldCount-1) 
						sb.Append(comma);
				}

				// Add a line break
				sb.Append("\r");

				// Write column headers 
				sw.WriteLine(sb.ToString());									 

				// Reinitialize the string builder for data.
				sb = new StringBuilder();

				# endregion

				// Read the data records 
				while (drResults.Read())
				{
					// Store Id of row so we can delete it later
					rowId = (int)drResults.GetValue(0);
					for (i = 0; i < drResults.FieldCount; i++)
					{
						sb.Append(drResults.GetValue(i));
						if (i != drResults.FieldCount-1)
							sb.Append(comma);
					}
		
					// Write the data to the file...
					sw.WriteLine(sb.ToString());
					// ... then delete the record from the DB
					archiveStatus = DeleteAvailabilityHistory(rowId);

					sb = new StringBuilder();
		
				}

				// Close the stream 
				sw.Close();
			}
			catch (SqlException se)
			{
				// Error has occured in stored procedure - this needs to be logged.
				archiveStatus = (int)TDExceptionIdentifier.AEStoredProcedureError;
				Logger.Write( new OperationalEvent( TDEventCategory.Database, TDTraceLevel.Verbose, "Error in GetAvailabilityHistory stored procedure. Unable to retrieve query. " + se.Message ));
			}
			catch(IOException ie)
			{
				// Error has occured writing to file - this needs to be logged
				Logger.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Error creating Availability History archive file. " + ie.Message ));
				archiveStatus = (int)TDExceptionIdentifier.AEFileCreationError;
			}
			finally
			{
				//Close DB connection
				sqlHelper.ConnClose();
			}
			
			return archiveStatus;
		}

		/// <summary>
		/// Performs the task of deleting a row in the Availability History
		/// database table by calling the necessary stored proc and handling errors
		/// </summary>
		/// <param name="rowId"></param>
		/// <returns>Status code - 0 if success</returns>
		private int DeleteAvailabilityHistory(int rowId)
		{
			int deleteStatus = 0;
			SqlHelper sqlHelper = new SqlHelper();
			Hashtable parameters = new Hashtable();
            parameters.Add(paramHistoryId, rowId);
		
			try
			{
				// Open DB connection 
				sqlHelper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);

				// Call Stored Procedure
                sqlHelper.Execute(SPDeleteAvailabilityHistory, parameters);
			}
			catch (SqlException ex)
			{
				// Error has occured in stored procedure - this needs to be logged.
				deleteStatus = (int)TDExceptionIdentifier.AEStoredProcedureError;
				Logger.Write( new OperationalEvent( TDEventCategory.Database, TDTraceLevel.Verbose, "Error in DeleteAvailabilityHistory stored procedure. Unable to delete records. " + ex.Message ));
			}
			finally
			{
				//Close DB connection
				sqlHelper.ConnClose();
			}
			
			return deleteStatus;
		}

		/// <summary>
		/// Performs the task of deleting the data in the Unavailable Products
		/// database table by calling the necessary stored proc and handling errors
		/// </summary>
		/// <returns>Status code - 0 if success</returns>
		private int DeleteUnavailableProducts()
		{
			int deleteStatus = 0;
			SqlHelper sqlHelper = new SqlHelper();
				
			try
			{
				// Open DB connection 
				sqlHelper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);

				// Call Stored Procedure
				sqlHelper.Execute(SPDeleteUnavailableProducts);
			}
			catch (SqlException ex)
			{
				// Error has occured in stored procedure - this needs to be logged.
				deleteStatus = (int)TDExceptionIdentifier.AEStoredProcedureError;
				Logger.Write( new OperationalEvent( TDEventCategory.Database, TDTraceLevel.Verbose, "Error in DeleteUnavailableProducts stored procedure. Unable to delete records. " + ex.Message ));
			}
			finally
			{
				//Close DB connection
				sqlHelper.ConnClose();
			}
			
			return deleteStatus;
		}
	

		/// <summary>
		///	Creates an xml file of the product profile data 
		///	and then emails it
		/// </summary>
		/// <returns>Status code - 0 if success</returns>
		private int ExportProductProfiles()
		{
			int exportStatus = 0;

			// Create the xml file attachment
			exportStatus = CreateAttachment();

			// If file created OK then send email
			if (exportStatus == 0)
			{
				try
				{
					string mailAddressFrom = Properties.Current["AvailabilityDataMaintenance.ProfilesExport.EMAIL.Sender"];					
					string mailAddressTo = Properties.Current["AvailabilityDataMaintenance.ProfilesExport.EmailAdddressTo"];	
					string subjectLine = Properties.Current["AvailabilityDataMaintenance.ProfilesExport.SubjectLine"];
					string attachmentFile = Properties.Current["AvailabilityDataMaintenance.ProfilesExport.AttachmentFile"];
					string attachmentName = Properties.Current["AvailabilityDataMaintenance.ProfilesExport.AttachmentName"];
					
					//Create and send email
					CustomEmailEvent ce = new CustomEmailEvent(mailAddressFrom, mailAddressTo, string.Empty, subjectLine, attachmentFile, attachmentName);
					Logger.Write(ce);	

					Logger.Write(new OperationalEvent(TDEventCategory.Business,TDTraceLevel.Info,
						"Availability Data Maintenance program has emailed user surveys to " + mailAddressTo.ToString(CultureInfo.InvariantCulture)));
				}
				catch (TDException tdex)
				{   
					// Set status code to show an error has occurred
					exportStatus = (int)TDExceptionIdentifier.AEEmailingFailure;

					// Log error
					string message = "Error emailing product profile data in ExportProductProfiles() method. TDException : " + tdex.Message;
					Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, message));				
				}
			}

			return exportStatus;
		}

		/// <summary>
		/// Performs the task of retreiving the data from the Product Profiles
		/// database table and writing it to an Xml file
		/// </summary>
		/// <returns>Status code - 0 if success</returns>
		private int CreateAttachment()
		{
			int statusCode = 0;
			SqlHelper sqlHelper = new SqlHelper();
			SqlDataReader drResults = null;
			string attachmentFile = Properties.Current["AvailabilityDataMaintenance.ProfilesExport.AttachmentFile"];

			#region XML String constants
			const string xmlDeclaration			= @"<?xml version=""1.0"" encoding=""utf-8"" ?>";
			const string openRootTag			= "<ProductProfiles>";
			const string closeRootTag			= "</ProductProfiles>";
			const string openProfileTag			= "<Profile>";
			const string closeProfileTag		= "</Profile>";
			const string elementTagMode			= "<Mode>{0}</Mode>";
			const string elementTagOrigin		= "<Origin>{0}</Origin>";
			const string elementTagDestination	= "<Destination>{0}</Destination>";
			const string elementTagCategory		= "<Category>{0}</Category>";
			const string elementTagDayType		= "<DayType>{0}</DayType>";
			const string elementTagDayRange		= "<DayRange>{0}</DayRange>";
			const string elementTagProbability	= "<Probability>{0}</Probability>";
			#endregion 

			// Open DB connection 
			sqlHelper.ConnOpen(SqlHelperDatabase.ProductAvailabilityDB);
			
			try
			{
				// Call Stored Procedure
				drResults = sqlHelper.GetReader(SPGetProductProfiles);
			
				// Create xml file and assign it a stream writer
				StreamWriter sw = File.CreateText(attachmentFile);
				
				sw.WriteLine(xmlDeclaration);
				sw.WriteLine(openRootTag);
				
				// Write out the product profile data in Xml format
				while (drResults.Read())
				{
					sw.WriteLine(openProfileTag);
					sw.WriteLine(string.Format(CultureInfo.CurrentCulture, elementTagMode, drResults.GetValue(0)));
					sw.WriteLine(string.Format(CultureInfo.CurrentCulture, elementTagOrigin, drResults.GetValue(1)));
					sw.WriteLine(string.Format(CultureInfo.CurrentCulture, elementTagDestination, drResults.GetValue(2)));
					sw.WriteLine(string.Format(CultureInfo.CurrentCulture, elementTagCategory, drResults.GetValue(3)));
					sw.WriteLine(string.Format(CultureInfo.CurrentCulture, elementTagDayType, drResults.GetValue(4)));
					sw.WriteLine(string.Format(CultureInfo.CurrentCulture, elementTagDayRange, drResults.GetValue(5)));
					sw.WriteLine(string.Format(CultureInfo.CurrentCulture, elementTagProbability, drResults.GetValue(6)));
					sw.WriteLine(closeProfileTag);
				}
			
				sw.WriteLine(closeRootTag);

				// Close the stream
				sw.Close();

			}
			catch(IOException ie)
			{
				// Error has occured writing to file - this needs to be logged
				Logger.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Error creating Product Profiles export file. " + ie.Message ));
				statusCode = (int)TDExceptionIdentifier.AEFileCreationError;
			}
			catch (SqlException se)
			{
				// Error has occured in stored procedure - this needs to be logged.
				Logger.Write( new OperationalEvent( TDEventCategory.Database, TDTraceLevel.Verbose, "Error in GetProductProfiles stored procedure. Unable to retrieve query. " + se.Message ));
				statusCode = (int)TDExceptionIdentifier.AEStoredProcedureError; 
			}
			finally
			{
				//Close DB connection
				sqlHelper.ConnClose();
			}
			return statusCode;
		}

		#endregion

	}

}
