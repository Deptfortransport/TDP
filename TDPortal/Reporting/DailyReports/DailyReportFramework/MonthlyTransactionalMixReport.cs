///<header>
///DailyReportMain.cs
///Created 20/02/2004
///Author JP Scott
///
///Version	Date		Who	Reason
///1		20/02/2004	PS	Created
///</header>
///
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Diagnostics;

using DailyReportFramework;

namespace DailyReportFramework
{
	/// <summary>
	/// Class to produce Monthly Transactional Mix report for capacity planning
 	/// analyses all recorded event tables and reports total count per day
	/// </summary>
	/// 
	
	public class MonthlyTransactionalMixReport
	{
		private DateTime currentDateTime;
		private string reportStartDateString;
		private string reportEndDateString;
		private DateTime reportStartDate;
		private DateTime reportEndDate;
        private string RequestedDateString;

		private EventLog EventLogger;
		private DailyReportController controller;
		
		private string propertyConnectionString;
		private string reportingConnectionString;
		private SqlConnection reportingSqlConnection;
		private SqlDataReader myDataReader;
		private SqlCommand mySqlCommand;
		private StringBuilder tmpString;
		private string transactionString;
		private string selectString;
		private System.IO.StreamWriter file;
		private string filePathString;
		private int statusCode;
        private string defaultInjectorString;
        private string backupInjectorString;
	
		
		
		
		public MonthlyTransactionalMixReport()
		{
			EventLogger = new EventLog("Application");
			EventLogger.Source = "TD.Reporting";
			controller    = new DailyReportController();

            ConfigurationManager.GetSection("appSettings");
            propertyConnectionString = ConfigurationManager.AppSettings["ReportProperties.connectionstring"];
            reportingConnectionString = ConfigurationManager.AppSettings["ReportDatabase.connectionstring"];
        }
		
		public int RunReport(DateTime reportDate)
		{
			statusCode = (int)StatusCode.Success; // success
			string reportDateString;
            RequestedDateString = reportDate.Year.ToString().Substring(2, 2)
             + "-" + reportDate.Month.ToString().PadLeft(2, '0');
			reportDateString = reportDate.ToShortDateString();
			currentDateTime = System.DateTime.Now;
			reportStartDate = new DateTime(reportDate.Year,reportDate.Month,1,0,0,0);
			reportEndDate = reportStartDate.AddMonths(1);
			reportEndDate = reportEndDate.AddDays(-1);
			reportStartDateString = reportStartDate.ToShortDateString();
			reportEndDateString = reportEndDate.ToShortDateString();
			tmpString = new StringBuilder();
			tmpString.Append("Transaction,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28");
			if (reportEndDate.Day == 29)
			{
				tmpString.Append(",29");
			}
			if (reportEndDate.Day == 30)
			{
				tmpString.Append(",29,30");
			}
			if (reportEndDate.Day == 31)
			{
				tmpString.Append(",29,30,31");
			}
			string daysString = tmpString.ToString();


			bool readOK =  controller.ReadProperty("3", "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}
            filePathString = filePathString.Replace("%YY-MM%", RequestedDateString);


			// open report text file
			file = new StreamWriter( new FileStream(filePathString, System.IO.FileMode.Create ) );
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("TRANSPORT DIRECT TANSACTIONAL MIX REPORT");
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("");
			this.file.WriteLine("For month starting : " + reportStartDate.ToShortDateString());
			this.file.WriteLine("");
			this.file.WriteLine("       transaction count per day");
			this.file.WriteLine(daysString);
			this.file.Flush();
			reportingSqlConnection = new SqlConnection(reportingConnectionString);

			//.............................			
			transactionString = "Page Entry Events";
			selectString = "select day(PEEDate), isnull(sum(PEECount),0)from dbo.PageEntryEvents "
				+ "where PEEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and PEEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) group by PEEDate order by PEEDate";
			DailyEventBreakDown();
			//.............................			
			transactionString = "Login Events";
			selectString = "select day(LEDate), isnull(sum(LECount),0)from dbo.LoginEvents "
				+ "where LEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and LEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) group by LEDate order by LEDate";
			DailyEventBreakDown();
			//.............................			
			transactionString = "User Preference Save Events";
			selectString = "select day(UPSEDate), isnull(sum(UPSECount),0)from dbo.UserPreferenceSaveEvents "
				+ "where UPSEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and UPSEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) group by UPSEDate order by UPSEDate";
			DailyEventBreakDown();
			//.............................			
			transactionString = "Gazetteer Events";
			selectString = "select day(GEDate), isnull(sum(GECount),0)from dbo.GazetteerEvents "
				+ "where GEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and GEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) group by GEDate order by GEDate";
			DailyEventBreakDown();
			//.............................			
			transactionString = "Journey Plan Location Events";
			selectString = "select day(JPLEDate), isnull(sum(JPLECount),0)from dbo.JourneyPlanLocationEvents "
				+ "where JPLEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and JPLEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) group by JPLEDate order by JPLEDate";
			DailyEventBreakDown();
			//.............................			
			transactionString = "Map Events";
			selectString = "select day(MEDate), isnull(sum(MECount),0)from dbo.MapEvents "
				+ "where MEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and MEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) group by MEDate order by MEDate";
			DailyEventBreakDown();
			//.............................			
			transactionString = "Work Load Events";
			selectString = "select day(WEDate), isnull(sum(WECount),0)from dbo.WorkloadEvents "
				+ "where WEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and WEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) group by WEDate order by WEDate";
			DailyEventBreakDown();
			//.............................			
			transactionString = "Retailer Hand Off Events";
			selectString = "select day(RHEDate), isnull(sum(RHECount),0)from dbo.RetailerHandOffEvents "
				+ "where RHEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and RHEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) group by RHEDate order by RHEDate";
			DailyEventBreakDown();
			//.............................			
			transactionString = "Road Plan Events";
			selectString = "select day(RPEDate), isnull(sum(RPECount),0)from dbo.RoadPlanEvents "
				+ "where RPEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and RPEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) group by RPEDate order by RPEDate";
			DailyEventBreakDown();
			//.............................			
			transactionString = "JourneyWeb Request Events";
			selectString = "select day(JWREDate), isnull(sum(JWRECount),0)from dbo.JourneyWebRequestEvents "
				+ "where JWREDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and JWREDate <= convert(datetime, '" + reportEndDateString
				+ "',103) group by JWREDate order by JWREDate";
			DailyEventBreakDown();
			//.............................			
			transactionString = "Journey Plan Mode Events";
			selectString = "select day(JPMEDate), isnull(sum(JPMECount),0)from dbo.JourneyPlanModeEvents "
				+ "where JPMEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and JPMEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) group by JPMEDate order by JPMEDate";
			DailyEventBreakDown();
			//.............................			

            /////////////////////////////////////////////////////////////////////////////////////////////////
            // find correct injector transactions
            readOK = controller.ReadProperty("0", "DefaultInjector", out defaultInjectorString);
            if (readOK == false || defaultInjectorString.Length == 0)
            {
                statusCode = (int)StatusCode.ErrorReadingFilePathDefaultInjector; // Error Reading FilePath
                return statusCode;
            }
            readOK = controller.ReadProperty("0", "BackupInjector", out backupInjectorString);
            if (readOK == false || backupInjectorString.Length == 0)
            {
                statusCode = (int)StatusCode.ErrorReadingFilePathBackupInjector; // Error Reading FilePath
                return statusCode;
            }
           
            //.............................	

            
            transactionString = "Reference Transaction Events";
			selectString = "select day(RTEDate), isnull(count(*),0)from dbo.ReferenceTransactionEvents "
				+ "where RTEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and RTEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) "
                + "and ( RTEMachineName not like '" + backupInjectorString + "' "
                + "or RTEMachineName is NULL ) "
                + "group by RTEDate order by RTEDate";
			DailyEventBreakDown();
			//.............................			
			transactionString = "User Sessions";
			selectString = "select day(SEDate), isnull(sum(SECount),0)from dbo.SessionEvents "
				+ "where SEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and SEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) group by SEDate order by SEDate";
			DailyEventBreakDown();

            transactionString = "Single Page Sessions ";
            selectString = "select day(SDADate), isnull(sum(SDASinglePageSessions ),0)from dbo.SessionDailyAnalysis "
                + "where SDADate >= convert(datetime, '" + reportStartDateString
                + "',103) and SDADate <= convert(datetime, '" + reportEndDateString
                + "',103) group by SDADate order by SDADate";
            DailyEventBreakDown();


            transactionString = "Multi-Page Sessions ";
            selectString = "select day(SDADate), isnull(sum(SDAMultiplePageSessions ),0)from dbo.SessionDailyAnalysis "
                + "where SDADate >= convert(datetime, '" + reportStartDateString
                + "',103) and SDADate <= convert(datetime, '" + reportEndDateString
                + "',103) group by SDADate order by SDADate";
            DailyEventBreakDown();
            
            transactionString = "Average Session Duration (secs) ";
            selectString = "select day(SDADate), isnull(sum(SDAAverageDuration),0)from dbo.SessionDailyAnalysis "
                + "where SDADate >= convert(datetime, '" + reportStartDateString
                + "',103) and SDADate <= convert(datetime, '" + reportEndDateString
                + "',103) group by SDADate order by SDADate";
            DailyEventBreakDown();

            transactionString = "Average Multi-Page Session Duration (secs) ";
            selectString = "select day(SDADate), isnull(sum(SDAAverageDurationMultiplePageSessions),0)from dbo.SessionDailyAnalysis "
                + "where SDADate >= convert(datetime, '" + reportStartDateString
                + "',103) and SDADate <= convert(datetime, '" + reportEndDateString
                + "',103) group by SDADate order by SDADate";
            DailyEventBreakDown();

            transactionString = "New Visitors ";
            selectString = "select day(RVEDate), isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                + "where RVEDate >= convert(datetime, '" + reportStartDateString
                + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                + "',103) and RVERVTID = 2"
                + " group by RVEDate order by RVEDate";
            DailyEventBreakDown();


            transactionString = "Repeat Visitors ";
            selectString = "select day(RVEDate), isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                + "where RVEDate >= convert(datetime, '" + reportStartDateString
                + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                + "',103) and RVERVTID = 3"
                + " group by RVEDate order by RVEDate";
            DailyEventBreakDown();

            
			this.file.Flush();
			this.file.Close();

			string from = "";
			string to = "";
			string smtpServer = "";
			string subject = "";


			readOK =  controller.ReadProperty("3", "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}
            filePathString = filePathString.Replace("%YY-MM%", RequestedDateString);

			readOK =  controller.ReadProperty("0", "MailAddress", out from);
			if (readOK == false || from.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingSenderEmailAddress; // Error Reading sender email address
				return statusCode;
			}
			readOK =  controller.ReadProperty("3", "MailRecipient", out to);
			if (readOK == false || to.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingRecipientEmailAddress; // Error Reading recipient email address
				return statusCode;
			}
			readOK =  controller.ReadProperty("3", "Title", out subject);
			if (readOK == false || subject.Length == 0)
			{
                subject = "Monthly Transactional Mix Report %YY-MM%";
			}
            subject = subject.Replace("%YY-MM%", RequestedDateString);
            readOK = controller.ReadProperty("0", "smtpServer", out smtpServer);
			if (readOK == false || smtpServer.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingSmtpServer; // Error Reading smtpServer
				return statusCode;
			}

			string bodyText =  "Report is attached....";


			SendEmail mailObject = new SendEmail();
			int sendFileErrorCode = mailObject.SendFile(from, to, subject, bodyText, 
				filePathString, smtpServer);
			if (sendFileErrorCode != 0)
				statusCode = sendFileErrorCode;

			return statusCode;

		}
	



		void DailyEventBreakDown()
		{

			tmpString = new StringBuilder();
			tmpString.Append(transactionString);
			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
			int lastDay =1;
			int day;
			int transactionCount = 0;
			try
			{
                reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				while (myDataReader.Read())
				{
					day = Convert.ToInt32(myDataReader.GetInt32(0).ToString());
					transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(1).ToString());
					while(lastDay < day)
					{
						lastDay+=1;
						tmpString.Append(",0");
					}
					lastDay+=1;
					tmpString.Append(",");
					tmpString.Append(transactionCount.ToString());
				} // end of while

				while(lastDay <= reportEndDate.Day)
				{
					lastDay+=1;
					tmpString.Append(",0");
				}
				this.file.WriteLine(tmpString.ToString());
					
				this.file.Flush();
				myDataReader.Close();

			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure reading table "+transactionString+"  "+e.Message,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Failed to reading page events table
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
				myDataReader.Close();
			}
		}
	}
}
