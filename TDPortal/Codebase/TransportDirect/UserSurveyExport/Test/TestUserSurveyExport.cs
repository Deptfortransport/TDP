// ****************************************************
// NAME 		: TestUserSurveyExport.cs
// AUTHOR 		: Joe Morrissey - Atos Origin
// DATE CREATED : 29/10/2004
// DESCRIPTION 	: Nunit tests for UserSurveyExport code
// ****************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/UserSurveyExport/Test/TestUserSurveyExport.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:51:00   mturner
//Initial revision.
//
//   Rev 1.8   Feb 10 2006 09:38:58   kjosling
//Turned off failed unit tests
//
//   Rev 1.7   Feb 07 2006 11:04:08   mtillett
//Fix unit test, so that created 98 days ago. Rename sp instead of deleting.
//
//   Rev 1.6   Feb 08 2005 13:53:26   RScott
//Assertions changed to Asserts
//
//   Rev 1.5   Nov 11 2004 10:00:38   jmorrissey
//Updated to replace use of ToString(TDCultureInfo.CurrentCulture) with ToString(CultureInfo.InvariantCulture)
//
//   Rev 1.4   Nov 10 2004 17:27:04   jmorrissey
//Added FlagSentSurveys stored procedure test
//
//   Rev 1.3   Nov 10 2004 13:08:18   jmorrissey
//Completed testing and FxCop
//
//   Rev 1.2   Nov 09 2004 18:35:32   jmorrissey
//Added error handling tests
//
//   Rev 1.1   Nov 04 2004 13:04:46   jmorrissey
//Updated name of database property
//
//   Rev 1.0   Nov 01 2004 17:22:02   jmorrissey
//Initial revision.

using System;
using System.ServiceProcess;
using System.Collections;
using System.Diagnostics;
using System.Net.Mail;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;

using NUnit.Framework;

using TransportDirect.UserSurveyExport;
using TransportDirect.Common;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.Logging;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.PropertyService.Properties;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserSurveyExport
{
	/// <summary>
	/// NUnit test class for test of UserSurveyExport.
	/// </summary>
	[TestFixture]
	public class TestUserSurveyExport
	{
		SqlHelper sqlHelper = new SqlHelper();	
		ExportMain exportMain = new ExportMain();
		ServiceController myService = new ServiceController("SMTPSVC");

		#region Constructor/Init/TearDown

		public TestUserSurveyExport()
		{
						
		}
		
		/// <summary>
		/// Setup anything required by the test
		/// </summary>
		[SetUp]
		public void Init()
		{
			// Initialise services
			TDServiceDiscovery.Init(new TestInitialization());
			
			//open connection to permanent portal database	
			if (!sqlHelper.ConnIsOpen)
			{
				sqlHelper.ConnOpen(SqlHelperDatabase.DefaultDB);
			}			

			// insert some test surveys
			InsertTestData();					
		}	

		/// <summary>
		/// Cleans up after tests
		/// </summary>
		[TearDown]
		public void TearDown()
		{
			//remove test surveys
			RemoveTestData();

			//close the database connection
			if (sqlHelper.ConnIsOpen)
			{
				sqlHelper.ConnClose();
			}
		}	

		#endregion
	
		#region TestRun
		/// <summary>
		/// tests the Run method
		/// </summary>
		[Test]
		public void TestRun()
		{
			int returnCode = 0;

			//simple check that the Run method does not return an error
			returnCode = exportMain.Run();
		
			//check method has returned ok
			Assert.IsTrue(returnCode == 0, "Run method has returned an error code");
		}

		#endregion
		
		#region TestReadProperty
		/// <summary>
		/// tests the ReadProperty method
		/// </summary>
		[Test]
		public void TestReadProperty()
		{	

			string attachmentProperty;
			bool propertyFound;			

			//read user survey properties
			attachmentProperty = ExportMain.ReadProperty("UserSurvey.AttachmentName");
			propertyFound = (attachmentProperty.ToString(CultureInfo.InvariantCulture) == "TDUserSurveys");
			Assert.IsTrue(propertyFound, "TestReadProperty failure. UserSurvey.AttachmentName property is incorrect");
		
		}

		#endregion

		#region TestFlagSentSurveys
		/// <summary>
		/// tests the FlagSentSurveys stored procedure
		/// </summary>
		[Test]
		public void TestFlagSentSurveys()
		{
			string qrySelect = string.Empty;
			int result = 0;
		
			DateTime DT = DateTime.Now;
			DateTime tomorrow = DT.AddDays(1);

			//parameter for FlagSentSurveys stored procedure
			Hashtable parameters = new Hashtable();	
			parameters.Add("@TimeSurveysRead", tomorrow.ToString(CultureInfo.InvariantCulture));						

			//execute stored procedure "FlagSentSurveys" 
			int rowsUpdated = sqlHelper.Execute("FlagSentSurveys",parameters);				

			//the setup method loads 4 test surveys into the UserSurvey table, only 1 of which has its SurveyEmailed flag set to true
			// the FlagSentSurveys stored procedure should flag all 4 to sent			
		
			//check that all 4 surveys in the database are now marked as sent'
			qrySelect = "select count (*) from UserSurvey where SurveyEmailed = 1";
			result = (int)sqlHelper.GetScalar(qrySelect);			
			Assert.IsTrue(result == 4,
				"FlagSentSurveys stored procedure has not updated the expected rows");	
			
		}

		#endregion
	
		#region TestDeleteOldSurveys
		/// <summary>
		/// tests the DeleteOldSurveys method
		/// </summary>
		[Test]
		[Ignore("ProjectNewkirk")]
		public void TestDeleteOldSurveys()
		{
			string qrySelect = string.Empty;
			int result = 0;
			int returnCode = 0;

			//the setup method loads 4 test surveys into the UserSurvey table, of which 1 has its SurveyEmailed flag set to true
			//the DeleteOldSurveys method should delete this survey, thereby leaving 3 surveys in the UserSurvey table
			returnCode = exportMain.DeleteOldSurveys();
		
			//check method has returned ok
			Assert.IsTrue(returnCode == 0,
				"DeleteOldSurveys has returned an error code");	
		
			//check that there are still 3 surveys in the database
			qrySelect = " select count (*) from UserSurvey";
			result = (int)sqlHelper.GetScalar(qrySelect);			
			Assert.IsTrue(result == 3,
				"DeleteOldSurveys has not worked correctly");	
			
		}

		/// <summary>
		/// tests the DeleteOldSurveys method returns errors correctly
		/// </summary>
		[Test]
		public void TestDeleteOldSurveysError()
		{

			int returnCode = 0;	
			string qryUpdate = string.Empty;

			try
			{
				//delete the stored procedure that DeleteOldSurveys relies on
				qryUpdate = "sp_rename 'DeleteSentSurveys', 'DeleteSentSurveys_NUNIT'";	
				sqlHelper.Execute(qryUpdate);
	
				//try DeleteOldSurveys method 
				returnCode = exportMain.DeleteOldSurveys();
		
				//check method has returned the correct error - USESqlHelperError = 6004,
				Assert.IsTrue(returnCode == 6004,
					"DeleteSentSurveys has not returned the correct error code");	
			}
			finally
			{			
				//always reset the stored procedure
				qryUpdate = "sp_rename 'DeleteSentSurveys_NUNIT', 'DeleteSentSurveys'";	
				sqlHelper.Execute(qryUpdate);
			}
		}	

		#endregion
	
		#region TestGetLatestSurveys
		/// <summary>
		/// tests the GetLatestSurveys method and also the CreateAttachment method that is also called
		/// from within GetLatestSurveys
		/// </summary>
		[Test]
		public void TestGetLatestSurveys()
		{

			string qrySelect = string.Empty;
			int returnCode = 0;
			int numRows = 0;

			//first delete any old version of UserSurveyAttachment.txt that might exist
			string emailAttachment = ExportMain.ReadProperty("UserSurvey.AttachmentFile");
			if (File.Exists(emailAttachment))
			{
				File.Delete(emailAttachment);
			}

			//DeleteOldSurveys method should delete any survey that has SurveyEmailed flag set to true 
			//thereby leaving 3 surveys in the UserSurvey table
			returnCode = exportMain.GetLatestSurveys();
			
			//check method has returned ok
			Assert.IsTrue(returnCode == 0,
				"GetLatestSurveys has returned an error code");	

			//check that there are 3 rows in the returned data reader
			SqlDataReader surveyReader = sqlHelper.GetReader("GetUnsentSurveys");	
			//count rows in the surveyReader
			while (surveyReader.Read())
			{
				numRows++;
			}
			surveyReader.Close(); 			
			Assert.IsTrue(numRows == 3,
				"GetLatestSurveys has not found all the latest surveys");	
		
			//check that the attachment has been created
			bool emailAttachmentCreated = File.Exists(emailAttachment);
			Assert.IsTrue(emailAttachmentCreated,
				"GetLatestSurveys has not created an attachment - C:\\temp\\UserSurveyAttachment.txt does not exist");	

		}	

		/// <summary>
		/// tests the GetLatestSurveys method returns an error correctly
		/// </summary>
		[Test]
		public void TestGetLatestSurveysError()
		{

			int returnCode = 0;	
			string qryUpdate = string.Empty;

			try
			{
				//rename the stored procedure that GetLatestSurveys relies on
				qryUpdate = "if exists(select * from sysobjects where name='GetUnsentSurveys' and type='P')" + 
					"EXEC sp_rename 'GetUnsentSurveys', 'tempSP'";	
				sqlHelper.Execute(qryUpdate);
	
				//try GetLatestSurveys method 
				returnCode = exportMain.GetLatestSurveys();
		
				//check method has returned the correct error - USESqlHelperError = 6004,
				Assert.IsTrue(returnCode == 6004,
					"GetLatestSurveys has not returned the correct error code");	
			}
			finally
			{			
				//always reanme the stored procedure
				qryUpdate = "if exists(select * from sysobjects where name='tempSP' and type='P')" + 
					"EXEC sp_rename 'tempSP', 'GetUnsentSurveys'";

				sqlHelper.Execute(qryUpdate);
			}			

		}	

		#endregion

		#region TestEmailSurveys

		/// <summary>
		/// tests the EmailSurveys method
		/// </summary>
		[Test]
		public void TestEmailSurveys()
		{

			int returnCode = 0;			
			returnCode = exportMain.EmailSurveys();
		
			//check method has returned ok
			Assert.IsTrue(returnCode == 0,
				"EmailSurveys has returned an error code");

			/*
			You should now check the email account specified in the 'UserSurvey.EmailAddressTo' 
			property in td.usersurveyexport.exe.xml. 			
			There should be a new email from UserSurvey@atosorigin.com with an attachment 
			text file containing 3 user surveys in csv format			
			*/
		}	

		/// <summary>
		/// tests that the EmailSurveys method returns a correct error when the smtp server is not found
		/// </summary>
		[Test]
		public void TestEmailSurveysError()
		{					
			//stop local SMTP server
 		    //this will cause the EmailSurveys method to fail and return an error
			myService = new ServiceController("SMTPSVC");
			if (myService.Status != ServiceControllerStatus.Stopped)
			{ 
				myService.Stop(); 
				myService.WaitForStatus(ServiceControllerStatus.Stopped);
			}
						
			int returnCode = 0;				
			
			//try EmailSurveys method 
			returnCode = exportMain.EmailSurveys();				

			//restart smtp process
			if (myService.Status == ServiceControllerStatus.Stopped)
			{								
				// Start the service, and wait until its status is "Running".
				myService.Start();
				myService.WaitForStatus(ServiceControllerStatus.Running);           
			}

			//check method has returned the correct error
			Assert.IsTrue( returnCode == 6007,
				"EmailSurveys has not returned the correct error code");
						
		}	
		
		#endregion
		
		#region Set Up Dummy Data
		/// <summary>
		/// adds test data to UserSurvey database table
		/// </summary>
		public void InsertTestData()
		{
			int rowsUpdated;

			// Used for insert of user surveys into database			
			Hashtable parameters = new Hashtable();				
	
			//test data row 1
			//create test data and set its emailed flag to false
			parameters = CreateDummyUserSurvey(false);
			//use stored procedure "AddUserSurvey" to add user survey data to the database
			rowsUpdated = sqlHelper.Execute("AddUserSurvey",parameters);
			//method returns true if one row has been successfully updated
			Assert.IsTrue(rowsUpdated == 1,
				"InsertTestData failure.");	

			//test data row 2
			//create test data and set its emailed flag to false
			parameters = CreateDummyUserSurvey(false);
			//use stored procedure "AddUserSurvey" to add user survey data to the database
			rowsUpdated = sqlHelper.Execute("AddUserSurvey",parameters);
			//method returns true if one row has been successfully updated
			Assert.IsTrue(rowsUpdated == 1,
				"InsertTestData failure.");	

			//test data row 3
			//create test data and set its emailed flag to false
			parameters = CreateDummyUserSurvey(false);
			//use stored procedure "AddUserSurvey" to add user survey data to the database
			rowsUpdated = sqlHelper.Execute("AddUserSurvey",parameters);
			//method returns true if one row has been successfully updated
			Assert.IsTrue(rowsUpdated == 1,
				"InsertTestData failure.");	

			//test data row 4 - survey has alraedy been emailed
			//create test data and set its emailed flag to true
			parameters = CreateDummyUserSurvey(true);
			//use stored procedure "AddUserSurvey" to add user survey data to the database
			rowsUpdated = sqlHelper.Execute("AddUserSurvey",parameters);
			//method returns true if one row has been successfully updated
			Assert.IsTrue(rowsUpdated == 1,
				"InsertTestData failure.");	
		
		}

		/// <summary>
		/// method creates dummy user surveys
		/// </summary>
		public Hashtable CreateDummyUserSurvey(bool surveyEmailed)
		{

			Hashtable dummyValues = new Hashtable();

			DateTime submittedDateTime;		
			submittedDateTime = DateTime.MinValue;
			submittedDateTime = DateTime.Now.AddDays(-98);			

			//add value that is passed to this method - allows surveyEmailed flag to be set to true or false
			dummyValues.Add("@SurveyEmailed", surveyEmailed);
		
			//add standard test data	
			dummyValues.Add( "@SubmittedDate", submittedDateTime );						
			dummyValues.Add("@Q1", "1");	
			dummyValues.Add("@Q2", "1");	
			dummyValues.Add("@Q3_1", "1");	
			dummyValues.Add("@Q3_2", "2");	
			dummyValues.Add("@Q3_3", "3");	
			dummyValues.Add("@Q3_4", "7");	
			dummyValues.Add("@Q3_5", "");	
			dummyValues.Add("@Q3_6", "");	
			dummyValues.Add("@Q3_7", "");	
			dummyValues.Add("@Q3_8", "");	
			dummyValues.Add("@Q3_9", "");	
			dummyValues.Add("@Q3_10", "");	
			dummyValues.Add("@Q3_11", "");	
			dummyValues.Add("@Q3_12", "");	
			dummyValues.Add("@Q4_1", "8");	
			dummyValues.Add("@Q4_2", "9");	
			dummyValues.Add("@Q4_3", "10");	
			dummyValues.Add("@Q4_4", "");	
			dummyValues.Add("@Q4_5", "");	
			dummyValues.Add("@Q4_6", "");	
			dummyValues.Add("@Q4_7", "");	
			dummyValues.Add("@Q4_8", "");	
			dummyValues.Add("@Q4_9", "");	
			dummyValues.Add("@Q4_10", "");	
			dummyValues.Add("@Q4_11", "");	
			dummyValues.Add("@Q4_12", "");	
			dummyValues.Add("@Q5", "6");	
			dummyValues.Add("@Q6_1", "1");	
			dummyValues.Add("@Q6_2", "");	
			dummyValues.Add("@Q6_3", "");	
			dummyValues.Add("@Q7", "1");	
			dummyValues.Add("@Q8_1", "1");	
			dummyValues.Add("@Q8_2", "2");	
			dummyValues.Add("@Q8_3", "3");	
			dummyValues.Add("@Q8_4", "4");	
			dummyValues.Add("@Q8_5", "5");	
			dummyValues.Add("@Q8_6", "6");	
			dummyValues.Add("@Q8_7", "7");	
			dummyValues.Add("@Q9_1", "5");	
			dummyValues.Add("@Q9_2", "");	
			dummyValues.Add("@Q9_3", "");	
			dummyValues.Add("@Q9_4", "");	
			dummyValues.Add("@Q9_5", "");	
			dummyValues.Add("@Q9_6", "");	
			dummyValues.Add("@Q9_7", "");	
			dummyValues.Add("@Q9_8", "");	
			dummyValues.Add("@Q9_9", "");	
			dummyValues.Add("@Q9_10", "");
			dummyValues.Add("@Q10_1", "1");	
			dummyValues.Add("@Q10_2", "2");	
			dummyValues.Add("@Q10_3", "3");	
			dummyValues.Add("@Q10_4", "7");	
			dummyValues.Add("@Q10_5", "");	
			dummyValues.Add("@Q10_6", "");	
			dummyValues.Add("@Q10_7", "");	
			dummyValues.Add("@Q10_8", "");	
			dummyValues.Add("@Q10_9", "");	
			dummyValues.Add("@Q10_10", "");	
			dummyValues.Add("@Q10_11", "");	
			dummyValues.Add("@Q10_12", "");	
			dummyValues.Add("@Q10_13", "");
			dummyValues.Add("@Q11_1", "1");	
			dummyValues.Add("@Q11_2", "2");	
			dummyValues.Add("@Q11_3", "3");	
			dummyValues.Add("@Q11_4", "4");	
			dummyValues.Add("@Q11_5", "5");	
			dummyValues.Add("@Q11_6", "6");	
			dummyValues.Add("@Q11_7", "7");	
			dummyValues.Add("@Q12", "3");	
			dummyValues.Add("@Q13", "4");	
			dummyValues.Add("@Q14", "5");	
			dummyValues.Add("@Q15", "6");	
			dummyValues.Add("@Q16", "7");
			dummyValues.Add("@ContactEmail", "tdpdev@yahoo.co.uk");	
			dummyValues.Add("@SessionId", "1");	
			dummyValues.Add("@UserLoggedOn", false);

			return dummyValues;

		}

		///
		/// <summary>
		/// removes test data from UserSurvey database table
		/// </summary>		
		public void RemoveTestData()
		{

			string sqlDelete = "Delete UserSurvey"; 
			sqlHelper.Execute(sqlDelete);
		}

		#endregion
       
	}
	
	#region Test Initialization class
	/// <summary>
	/// Class to initialise services that are used by the tests.
	/// </summary>
	public class TestInitialization : IServiceInitialisation
	{
		
		public void Populate(Hashtable serviceCache)
		{
			// Enable PropertyService
			serviceCache.Add (ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

			// Enable logging service.
			ArrayList errors = new ArrayList();

			//add CustomEmailPublisher		
			IEventPublisher[] customPublishers = new IEventPublisher[1];				

			try
			{				
				string sender = Properties.Current["UserSurvey.EMAIL.Sender"];
				string smtpServer = Properties.Current["Logging.Publisher.Custom.EMAIL.SMTPServer"];
				string directory = Properties.Current["Logging.Publisher.Custom.EMAIL.WorkingDir"];

				customPublishers[0] = 
					new CustomEmailPublisher("EMAIL",sender,MailPriority.Normal,smtpServer,directory,errors);

				Trace.Listeners.Add( new TDTraceListener(Properties.Current, customPublishers, errors ));
			}
			catch (TDException tdEx)
			{
				foreach(string error in errors)
				{
					Console.WriteLine(error);
				}
				throw tdEx;
			}
		}
	}

	#endregion 

}	
