// ************************************************************************************
// NAME 		: UserSurveyExport.cs
// AUTHOR 		: Joe Morrissey
// DATE CREATED : 14/10/2004
// DESCRIPTION 	: Main implementation class of this application.
// Creates and sends email with a text file attachment of user survey data
// ************************************************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/UserSurveyExport/ExportMain.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:50:54   mturner
//Initial revision.
//
//   Rev 1.5   Nov 11 2004 10:00:30   jmorrissey
//Updated to replace use of ToString(TDCultureInfo.CurrentCulture) with ToString(CultureInfo.InvariantCulture)
//
//   Rev 1.4   Nov 10 2004 17:40:14   jmorrissey
//Added code to flag email surveys as sent
//
//   Rev 1.3   Nov 09 2004 18:34:16   jmorrissey
//Updated error handling
//
//   Rev 1.2   Nov 04 2004 12:47:08   jmorrissey
//Improved logging
//
//   Rev 1.1   Nov 02 2004 16:27:58   jmorrissey
//Completed Nunit testing

using System;
using System.Web.Mail;
using System.Globalization;
using System.Collections;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using System.IO;

using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using System.Diagnostics;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserSurveyExport
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	class ExportMain
	{
		//private variables and objects
		private SqlHelper sqlHelper = new SqlHelper();	
		private string attachmentFile; 
		private TDDateTime timeSurveysRead;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static int Main()
		{
			//return code for this application
			int statusCode = 0;

			Console.WriteLine("Starting");
			try
			{
				TDServiceDiscovery.Init(new UserSurveyExportInitialisation());

				ExportMain app = new ExportMain();
				statusCode = app.Run();
			}
			catch (TDException tdEx)
			{
				if (!tdEx.Logged)
					Console.WriteLine(tdEx.ToString() + " has occured on Export Main initialisation.");
				statusCode = (int)tdEx.Identifier;
			}
			Console.WriteLine("Exiting with errorcode "+ statusCode);
			return statusCode;
		}

		/// <summary>
		/// This is the main method in the application. It uses private methods to do the following:
		/// 1. Delete surveys from User Survey table in PermanentPortal database 
		/// that have previously been emailed to the dft
		/// 2. Read latest surveys from database and streams them into a text file
		/// 3. Emails the text file to the dft
		/// </summary>
		/// <returns></returns>
		public int Run()
		{
			int statusCode = 0;

			//delete surveys marked as previously sent
			statusCode = DeleteOldSurveys();
			if (statusCode > 0)
			{
				//if error, return now
				return statusCode;
			}

			//get surveys not marked as sent
			statusCode = GetLatestSurveys();
			if (statusCode > 0)
			{
				//if error, return now
				return statusCode;
			}

			//email surveys 
			statusCode = EmailSurveys();
			if (statusCode > 0)
			{
				//if error, return now
				return statusCode;
			}
	
			//if no errors, statusCode should still be zero
			return statusCode;
		}
	
		/// <summary>
		/// Delete surveys marked as previously sent
		/// </summary>
		/// <returns></returns>
		public int DeleteOldSurveys()
		{
			//return code for this method
			int deleteStatusCode = 0;	
				
			try
			{
				//open connection to properties database							
				sqlHelper.ConnOpen(SqlHelperDatabase.DefaultDB);				

				//use stored procedure "DeleteSentSurveys" to delete user survey data from the database
				int rowsUpdated = sqlHelper.Execute("DeleteSentSurveys");

				Logger.Write(new OperationalEvent(TDEventCategory.Database,TDTraceLevel.Info,
					"User Survey Export program has deleted " + rowsUpdated.ToString(CultureInfo.InvariantCulture) + 
					" rows from the UserSurvey database table in PermanentPortal database."));

				//set status code to success indicator of zero
				deleteStatusCode = 0;							
			
			}
			catch (SqlException sqlEx)
			{
				//set status code to show an error has occurred
				deleteStatusCode = (int)TDExceptionIdentifier.USESqlHelperError;

				//log error 
				string message = "SQL Error in ExportMain.DeleteOldSurveys method: " + "SQLNUM["+sqlEx.Number+"] - " + sqlEx.Message;
				Logger.Write(new OperationalEvent(TDEventCategory.Database,
					TDTraceLevel.Error, message));			
				
			}
			catch (TDException tdex)
			{   
				//set status code to show an error has occurred
				deleteStatusCode = (int)TDExceptionIdentifier.USEDeleteSentSurveysSPFailure;

				//log error 
				string message = "Error in ExportMain.DeleteOldSurveys method. TDException : " + tdex.Message;
				Logger.Write(new OperationalEvent(TDEventCategory.Database,
					TDTraceLevel.Error, message));				
					
				
			}
			finally
			{
				//close the database connection
				if (sqlHelper.ConnIsOpen)
				{
					sqlHelper.ConnClose();
				}	
			}	

			//return status code
			return deleteStatusCode;	
		}

		/// <summary>
		/// Read surveys from the database that are not marked as sent
		/// </summary>
		/// <returns></returns>
		public int GetLatestSurveys()
		{
			//return code for this method
			int getLatestStatusCode = 0;
	
			//read surveys not previously sent						
			try
			{
				//open connection to properties database				
				sqlHelper.ConnOpen(SqlHelperDatabase.DefaultDB);
	
				//use stored procedure "GetUnsentSurveys" to read user survey data from the database
				SqlDataReader surveyReader = sqlHelper.GetReader("GetUnsentSurveys");

				//set time of read
				timeSurveysRead = TDDateTime.Now;
		
				Logger.Write(new OperationalEvent(TDEventCategory.Database,TDTraceLevel.Info,
					"User Survey Export program has read surveys from UserSurvey database table in PermanentPortal database."));
				
				//call method to stream the surveys into a text file
				getLatestStatusCode = CreateAttachment(surveyReader); 					
				
			}	
			catch (SqlException sqlEx)
			{
				//set status code to show an error has occurred
				getLatestStatusCode = (int)TDExceptionIdentifier.USESqlHelperError;

				//log error 
				string message = "SQL Error in ExportMain.GetLatestSurveys method: " + "SQLNUM["+sqlEx.Number+"] - " + sqlEx.Message;
				Logger.Write(new OperationalEvent(TDEventCategory.Database,
					TDTraceLevel.Error, message));		
			}
			catch (TDException tdex)
			{   
				//set status code to show an error has occurred
				getLatestStatusCode = (int)TDExceptionIdentifier.USEGetUnsentSurveysSPFailure;

				//log error 
				string message = "Error in ExportMain.GetLatestSurveys method. TDException : " + tdex.Message;
				Logger.Write(new OperationalEvent(TDEventCategory.Database,
					TDTraceLevel.Error, message));			
			}
			finally
			{
				//close the database connection
				if (sqlHelper.ConnIsOpen)
				{
					sqlHelper.ConnClose();
				}		
			}	

			//return status code
			return getLatestStatusCode;
		}

		/// <summary>
		/// Creates and sends the email 
		/// </summary>
		/// <returns></returns>
		public int EmailSurveys()
		{		
            //return code for this method
			int emailStatusCode = 0;

			//CustomEmailEvent used to send email
			CustomEmailEvent ce = null;		
		
			//today's date
			DateTime today = new DateTime();
			today = DateTime.Now;

			//properties
			string mailAddressFrom = string.Empty;					
			string mailAddressTo = string.Empty;
			string subjectLine = string.Empty;
			string attachmentName = string.Empty;

			//read properties	
			try
			{
				mailAddressFrom = ReadProperty("UserSurvey.EMAIL.Sender");					
				mailAddressTo = ReadProperty("UserSurvey.EmailAddressTo");	
				subjectLine = ReadProperty("UserSurvey.SubjectLine");
				attachmentName = ReadProperty("UserSurvey.AttachmentName"); 	
			}			
			catch (TDException tdex)
			{   
				//set status code to show an error has occurred
				emailStatusCode = (int)TDExceptionIdentifier.USEMissingProperty;

				//log error
				string message = "Error reading property in ExportMain.EmailLatestSurveys method. TDException : " + tdex.Message;
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,TDTraceLevel.Error, message);

				//return error code
				return emailStatusCode;				
			}
			
			//send email
			try
			{
				//add other parameters needed for the CustomEmailEvent
				string emptyBodyText = string.Empty;			
				
				//Create and send email
				ce = new CustomEmailEvent( mailAddressFrom, mailAddressTo, emptyBodyText, subjectLine, attachmentFile, attachmentName);
				Logger.Write(ce);	

				Logger.Write(new OperationalEvent(TDEventCategory.Business,TDTraceLevel.Info,
					"User Survey Export program has emailed user surveys to " + mailAddressTo.ToString(CultureInfo.InvariantCulture)));
				
			}
			catch (TDException tdex)
			{   
				//set status code to show an error has occurred
				emailStatusCode = (int)TDExceptionIdentifier.USEEmailingFailure;

				//log error
				string message = "Error emailing user surveys in ExportMain.EmailLatestSurveys method. TDException : " + tdex.Message;
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, message);				
			}

			//flag sent emails 
			try
			{
				//parameter for FlagSentSurveys stored procedure
				Hashtable parameters = new Hashtable();	
				parameters.Add("@TimeSurveysRead", timeSurveysRead.ToString());

				//open connection to properties database							
				sqlHelper.ConnOpen(SqlHelperDatabase.DefaultDB);				

				//execute stored procedure "FlagSentSurveys" 
				int rowsUpdated = sqlHelper.Execute("FlagSentSurveys",parameters);				

				Logger.Write(new OperationalEvent(TDEventCategory.Database,TDTraceLevel.Info,
					"User Survey Export program has updated 'SurveyEmailed' column in UserSurvey table in PermanentPortal database."));

			}
			catch (SqlException sqlEx)
			{
				//set status code to show an error has occurred
				emailStatusCode = (int)TDExceptionIdentifier.USESqlHelperError;

				//log error 
				string message = "SQL Error in ExportMain.EmailSurveys method: " + "SQLNUM["+sqlEx.Number+"] - " + sqlEx.Message;
				Logger.Write(new OperationalEvent(TDEventCategory.Database,
					TDTraceLevel.Error, message));		
			}
			catch (TDException tdex)
			{   
				//set status code to show an error has occurred
				emailStatusCode = (int)TDExceptionIdentifier.USEFlagSentSurveysSPFailure;

				//log error 
				string message = "Error in ExportMain.EmailSurveys method. TDException : " + tdex.Message;
				Logger.Write(new OperationalEvent(TDEventCategory.Database,
					TDTraceLevel.Error, message));			
			}
			finally
			{
				//close the database connection
				if (sqlHelper.ConnIsOpen)
				{
					sqlHelper.ConnClose();
				}		
			}	
			
			//return status code
			return emailStatusCode;
			
		}

		/// <summary>
		/// Creates a text file containing user survey data
		/// </summary>
		/// <returns></returns>
		private int CreateAttachment(SqlDataReader surveyReader)
		{
			 //return code for this method
			int createAttachmentStatusCode = 0;
			int numSurveys = 0;

			//variables used in craeting the text file
			string strLine = string.Empty;			
			char comma = ',';
			int i = 0;

			//create a text file which will be used as the email attachment
			attachmentFile = ReadProperty("UserSurvey.AttachmentFile");			
			
			try
			{
				//create text file and assign it a stream writer
				StreamWriter sw = File.CreateText(attachmentFile);

				//format column headers
				for (i = 0; i < surveyReader.FieldCount; i++)
				{
					strLine = strLine + surveyReader.GetName(i).ToString(CultureInfo.InvariantCulture) + comma;
				}

				//remove last comma and add a line break
				char[] trimArray = new char[1];
				trimArray[0] = comma;
				strLine.TrimEnd(trimArray);
				strLine = strLine + "\r"; 

				//write column headers 
				sw.WriteLine(strLine);									 

				//Reinitialize the string for data.
				strLine = string.Empty;

				//read the data records 
				while (surveyReader.Read())
				{
					for (i = 0; i < surveyReader.FieldCount; i++)
					{
						strLine = strLine + surveyReader.GetValue(i) + comma;
					}
					//remove last comma 
					strLine.TrimEnd(trimArray);					

					//write the data
					sw.WriteLine(strLine);
					strLine = string.Empty;		
				
					numSurveys++;
				}

				//close the stream 
				sw.Close();

				//close the reader
				surveyReader.Close();
				
				Logger.Write(new OperationalEvent(TDEventCategory.Business,TDTraceLevel.Info,
					"User Survey Export program has created " + attachmentFile.ToString(CultureInfo.InvariantCulture) + 
					" . This attachment contains " + numSurveys.ToString(CultureInfo.InvariantCulture) + " user surveys."));

			}
			catch (TDException tdex)
			{   
				//set status code to show an error has occurred
				createAttachmentStatusCode = (int)TDExceptionIdentifier.USECreateAttachmentFailure;

				//log error 
				string message = "Error creating attachment in ExportMain.CreateAttachment method " + tdex.Message;
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, message));
			}

			//return status code
			return createAttachmentStatusCode;			
		}

		/// <summary>
		/// Reads properties from the database
		/// </summary>
		/// <param name="identifier">string</param>
		/// <returns>string - propertyValue</returns>
		public static string ReadProperty(string identifier)
		{

			string propertyName = string.Empty;
			string propertyValue = string.Empty;

			//read property from Properties database
			propertyValue = Properties.Current[identifier];			
			if (propertyValue == null)
			{
				//log error and throw exception
				Logger.Write(new OperationalEvent(TDEventCategory.Database,TDTraceLevel.Error,
						"Missing property in Property database : " + identifier.ToString(CultureInfo.InvariantCulture)));
				
				throw new TDException(
					"Missing property in Property database : " + identifier.ToString(CultureInfo.InvariantCulture),
					true, TDExceptionIdentifier.USEMissingProperty);
			}	
			else
			{
				//return the property
				return propertyValue;
			}
		}
	}			

	
}
