///<header>
///MonthlyUserFeedbackReport.cs
///Created 03/08/2004
///Author JP Scott
///
///Version	Date		Who	Reason
///1		03/08/2004	PS	Created (release 1.0.8)
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
	/// Class to produce Monthly User Feedback report
 	/// analyses all recorded use feedback transactions and reports average response by day
	/// </summary>
	/// 
	
	public class MonthlyUserFeedbackReport
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
		
		
		
		
		public MonthlyUserFeedbackReport()
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
			tmpString.Append("Day...,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28");
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


			bool readOK =  controller.ReadProperty("13", "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}
            filePathString = filePathString.Replace("%YY-MM%", RequestedDateString);


			// open report text file
			file = new StreamWriter( new FileStream(filePathString, System.IO.FileMode.Create ) );
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("TRANSPORT DIRECT USER FEEDBACK REPORT");
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("");
			this.file.WriteLine("For month starting : " + reportStartDate.ToShortDateString());
			this.file.WriteLine("");
			this.file.WriteLine("       User Feedback counts per day");
			this.file.WriteLine(daysString);
			this.file.Flush();
			reportingSqlConnection = new SqlConnection(reportingConnectionString);

			//.............................			
			transactionString = "Site Problems";
			selectString = "select day(UfeDate), isnull(sum(UfeCount),0)from dbo.UserFeedbackEvents "
				+ "where UfeDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and UfeDate <= convert(datetime, '" + reportEndDateString
				+ "',103) and UfeTypeID = 0 group by UfeDate order by UfeDate";
			DailyEventBreakDown();
			//.............................			
			transactionString = "Incorrect Information";
			selectString = "select day(UfeDate), isnull(sum(UfeCount),0)from dbo.UserFeedbackEvents "
				+ "where UfeDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and UfeDate <= convert(datetime, '" + reportEndDateString
				+ "',103) and UfeTypeID = 1 group by UfeDate order by UfeDate";
			DailyEventBreakDown();
			//.............................			
			transactionString = "Suggestion";
			selectString = "select day(UfeDate), isnull(sum(UfeCount),0)from dbo.UserFeedbackEvents "
				+ "where UfeDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and UfeDate <= convert(datetime, '" + reportEndDateString
				+ "',103) and UfeTypeID = 2 group by UfeDate order by UfeDate";
			DailyEventBreakDown();
			//.............................	
			this.file.WriteLine(" ");
			transactionString = "Total Feedback Emails";
			selectString = "select day(UfeDate), isnull(sum(UfeCount),0)from dbo.UserFeedbackEvents "
				+ "where UfeDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and UfeDate <= convert(datetime, '" + reportEndDateString
				+ "',103)  group by UfeDate order by UfeDate";
			DailyEventBreakDown();
			//.............................	
			
			this.file.WriteLine(" ");
			transactionString = "Emails Not Acknowledged";
			selectString = "select day(UfeDate), isnull(sum(UfeCount),0)from dbo.UserFeedbackEvents "
				+ "where UfeDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and UfeDate <= convert(datetime, '" + reportEndDateString
				+ "',103) and UfeAcknowledged = 0 group by UfeDate order by UfeDate";
			DailyEventBreakDown();
			
			//.............................	

			this.file.WriteLine(" ");
			this.file.WriteLine("Analysis of Acknowledgement response times (milliseconds)");
			this.file.WriteLine("Hour ");
			for (int selHour = 0;selHour <=23;selHour++)
			{
				transactionString = selHour.ToString();
				selectString = "select day(UfeDate), isnull(AVG(UfeMsToAcknowledge),0)from dbo.UserFeedbackEvents "
					+ "where UfeDate >= convert(datetime, '" + reportStartDateString 
					+ "',103) and UfeDate <= convert(datetime, '" + reportEndDateString
					+ "',103) and UfeAcknowledged = 1 and UfeHour = " + selHour
					+ " group by UfeDate order by UfeDate";
				DailyEventBreakDown();
			}
			transactionString = "Daily Average Response Time";
			selectString = "select day(UfeDate), isnull(AVG(UfeMsToAcknowledge),0)from dbo.UserFeedbackEvents "
			+ "where UfeDate >= convert(datetime, '" + reportStartDateString 
			+ "',103) and UfeDate <= convert(datetime, '" + reportEndDateString
			+ "',103) and UfeAcknowledged = 1 group by UfeDate order by UfeDate";
			DailyEventBreakDown();

			this.file.Flush();
			this.file.Close();

			string from = "";
			string to = "";
			string smtpServer = "";
			string subject = "";


			readOK =  controller.ReadProperty("13", "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}
            filePathString = filePathString.Replace("%YY-MM%", RequestedDateString);
            readOK = controller.ReadProperty("0", "MailAddress", out from);
			if (readOK == false || from.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingSenderEmailAddress; // Error Reading sender email address
				return statusCode;
			}
			readOK =  controller.ReadProperty("13", "MailRecipient", out to);
			if (readOK == false || to.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingRecipientEmailAddress; // Error Reading recipient email address
				return statusCode;
			}
			readOK =  controller.ReadProperty("13", "Title", out subject);
			if (readOK == false || subject.Length == 0)
			{
                subject = "Monthly Transactional Mix Report %YY-MM%";
			}
            subject = subject.Replace("%YY-MM%", RequestedDateString);

			readOK =  controller.ReadProperty("0", "smtpServer", out smtpServer);
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
