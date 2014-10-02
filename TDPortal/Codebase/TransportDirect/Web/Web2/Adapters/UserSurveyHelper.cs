// *********************************************** 
// NAME                 : UserSurveyHelper.cs 
// AUTHOR               : Joe Morrissey
// DATE CREATED         : 05/10/2004
// DESCRIPTION			: User Survey functionality
//
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/UserSurveyHelper.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 12:59:16   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:32   mturner
//Initial revision.
//
//   Rev 1.9   Feb 23 2006 19:16:14   build
//Automatically merged from branch for stream3129
//
//   Rev 1.8.1.0   Jan 10 2006 15:17:48   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.8   Nov 19 2004 11:35:54   jmorrissey
//Ensure random number generated is greater than zero
//
//   Rev 1.7   Nov 10 2004 13:00:44   jmorrissey
//Updated after Nunit testing and FxCop
//
//   Rev 1.6   Nov 10 2004 09:21:20   jmorrissey
//Updated logging
//
//   Rev 1.5   Nov 02 2004 17:23:26   jmorrissey
//Updated exception identifiers
//
//   Rev 1.4   Nov 02 2004 16:24:02   jmorrissey
//Updated after Nunit testing
//
//   Rev 1.3   Oct 14 2004 16:58:58   jmorrissey
//Updated SubmitSurvey method to use new stored procedure
//
//   Rev 1.2   Oct 11 2004 13:53:38   jmorrissey
//Added SubmitSurvey method stub. Needed by UserSurvey.aspx.
//
//   Rev 1.1   Oct 08 2004 12:34:24   jmorrissey
//Change for User Survey functionality.
//
//   Rev 1.0   Oct 06 2004 17:39:58   jmorrissey
//Initial revision.


using System;using TransportDirect.Common.ResourceManager;
using System.Globalization;
using System.Web;
using System.Collections;
using System.Data.SqlClient;

using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.SessionManager; 
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Web.Support;
using System.Diagnostics;
using Logger = System.Diagnostics.Trace;


namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>	
	/// Class is responsible for common functionality required by User Survey 
	/// </summary>	
	public sealed class UserSurveyHelper
	{

		/// <summary>
		/// private default constructor - public one not needed as only static members are defined in this class
		/// </summary>
		private UserSurveyHelper()
		{

		}

		/// <summary>
		/// Generates a random number and returns true if:
		/// 1. Random number matches the UserSurvey.UserSurveyTrigger
		/// 2. The user survey form has not already been displayed in the user's session
		/// </summary>
		/// <returns></returns>
		public static bool ShowUserSurvey()
		{
			//the 1 in n occurrences is configured through the UserSurvey.UserSurveyTrigger property			
			int surveyTrigger = Convert.ToInt32(Properties.Current["UserSurvey.UserSurveyTrigger"],CultureInfo.CurrentCulture.NumberFormat);
			
			//check if survey is not currently required, indicated by the surveyTrigger being set to zero
			switch (surveyTrigger)
			{
				//property set to zero indicates that survey is not currently required
				case 0:
					return false;

				//property set to one indicates that survey should always be shown e.g. for testing purposes
				case 1:
					return true;

				//property set to other value indicates that survey is in use and will be shown according to a random factor
				default:
			
					//generate random number between 1 and 50 inclusive
					Random randomGenerator = new Random(unchecked((int)DateTime.Now.Ticks)); 				
					int randomNumber = randomGenerator.Next(1, surveyTrigger + 1);	
		
					//log the number generated for informational purposes
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,TDTraceLevel.Info,
						"UserSurveyHelper.ShowUserSurvey has generated a random number of " + randomNumber.ToString(TDCultureInfo.CurrentCulture)));

					//method returns true if the random number matches the UserSurvey.UserSurveyTrigger property value
					//and the user survey has not already been shown in this session
					return ((randomNumber == surveyTrigger) && (!TDSessionManager.Current.UserSurveyAlreadyShown));	
			}
		}

		/// <summary>
		/// Writes the survey information to the UserSurvey table in the permanent portal database
		/// </summary>
		public static bool SubmitSurvey(Hashtable answers)
		{
			bool returnFlag = false;
			// Used for insert of user surveys into database
			SqlHelper sqlHelper = new SqlHelper();
			Hashtable parameters = new Hashtable();			

			//parameters for insert procedure			
			DateTime submittedDateTime;	
			bool surveyEmailed;
			bool userLoggedOn;			
			string sessionID;
			string spParamName;
			string spParamValue;
		
			//log Survey to permanent portal database						
			try
			{
				//populate parameters					
				submittedDateTime = DateTime.MinValue;
				submittedDateTime = DateTime.Now;
				sessionID  = TDSessionManager.Current.Session.SessionID;
				userLoggedOn = TDSessionManager.Current.Authenticated;
				//the next bool will always be false when inserted into the database, 
				//it only gets updated by the weekly batch run
				surveyEmailed = false;

				//open connection to properties database
				sqlHelper.ConnOpen(SqlHelperDatabase.DefaultDB);				

				// Add all the required parameters for the Stored Procedure to the hashtable
				parameters.Add( "@SubmittedDate", submittedDateTime );

				//get answer values from hashtable and use to populate answer parameters for the stored procedure
				IDictionaryEnumerator myEnumerator = answers.GetEnumerator();
				while ( myEnumerator.MoveNext() )
				{					
					spParamName = "@" + myEnumerator.Key.ToString();
					spParamValue = myEnumerator.Value.ToString();
					parameters.Add(spParamName,spParamValue);
				}

				parameters.Add( "@SessionId", sessionID);				
				parameters.Add( "@UserLoggedOn", userLoggedOn);	
				parameters.Add("@SurveyEmailed", surveyEmailed);					

				//use stored procedure "AddUserSurvey" to add user survey data to the database
				int rowsUpdated = sqlHelper.Execute("AddUserSurvey",parameters);

				//write submit event to the log file
				Logger.Write(new OperationalEvent(TDEventCategory.Database,TDTraceLevel.Info,
					"User Survey has been submitted to the UserSurvey table PermanentPortal database with Session ID : " + sessionID.ToString(TDCultureInfo.CurrentCulture)));

				//method returns true if one row has been successfully updated
				returnFlag = (rowsUpdated == 1);				
				
			}
			catch (SqlException sqlEx)
			{
				returnFlag = false;

				//log error and throw exception
				string message = "SqlException caught in UserSurveyHelper.SubmitSurvey method : " + sqlEx.Message;
				Logger.Write(new OperationalEvent(	TDEventCategory.Database,
					TDTraceLevel.Error, "SQLNUM["+sqlEx.Number+"] :"+sqlEx.Message+":"));

				throw new TDException(message, sqlEx,false, TDExceptionIdentifier.USESqlHelperError);
				
				
			}
			catch (TDException tdex)
			{   
				returnFlag = false;

				//log error and throw exception
				string message = "Error submitting a user survey in UserSurveyHelper.SubmitSurvey method : " + tdex.Message;
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, "TDException :" + tdex.Message + ":");				

				throw new TDException(message, tdex, false, TDExceptionIdentifier.USEAddUserSurveySPFailure);
				
			}
			finally
			{
				//close the database connection
				sqlHelper.ConnClose();				
			}	
	
			return returnFlag;			
		}
	}
}
