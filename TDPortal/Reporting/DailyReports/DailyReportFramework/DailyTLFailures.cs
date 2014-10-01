///<header>
///Daily Traveline Failures (26)
///Created 20/06/2006
///Author J Frank
///
///Version	Date		Who	Reason
///1		20/06/2006	JF	Created 
///
///</header>
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections;

using DailyReportFramework;

namespace DailyReportFramework
{
	/// <summary>
	/// Class to produce Daily Traveline Failure Times
	/// </summary>
	/// 
	
	public class DailyTLFailures
	{
		private string reportNo;
		private DateTime currentDateTime;
		private string reportStartDateString;
        private string RequestedDateString;

		private DateTime reportStartDate;
		
		private EventLog EventLogger;
		private DailyReportController controller;
		
		private string propertyConnectionString;
		private string reportingConnectionString;
		private SqlConnection reportingSqlConnection;
		private SqlConnection reportingSqlConnection1;
		private SqlDataReader drTL;
		private SqlDataReader drCheck1;
		private SqlCommand sqlCommTL;
		private SqlCommand sqlCommCheck;
		private StringBuilder tmpString;
		private string strTLFailures;
		private string strCheck1Failures;
		private System.IO.StreamWriter file;
		private string filePathString;
		private int statusCode;
		
		private int[] dayCount = new int[32];  //1-31
		private string myType;
		private int myHour;
		private int myMin;
        private string defaultInjectorString;
        private string backupInjectorString;

        public DailyTLFailures()
        {
            EventLogger = new EventLog("Application");
            EventLogger.Source = "TD.Reporting";
            controller = new DailyReportController();

            ConfigurationManager.GetSection("appSettings");
            propertyConnectionString = ConfigurationManager.AppSettings["ReportProperties.connectionstring"];
            reportingConnectionString = ConfigurationManager.AppSettings["ReportDatabase.connectionstring"];
        }
		
		public int RunReport(DateTime reportDate)
		{
			statusCode = (int)StatusCode.Success; // success
			reportNo ="20";
			string reportDateString;
            RequestedDateString = reportDate.Year.ToString().Substring(2, 2)
            + "-" + reportDate.Month.ToString().PadLeft(2, '0')
            + "-" + reportDate.Day.ToString().PadLeft(2, '0');
			reportDateString = reportDate.ToShortDateString();
			currentDateTime = System.DateTime.Now;
			reportStartDate = reportDate;
			reportStartDateString = reportStartDate.ToShortDateString();


			bool readOK =  controller.ReadProperty(reportNo, "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}
            filePathString = filePathString.Replace("%YY-MM-DD%", RequestedDateString);


			// open report text file
			file = new StreamWriter( new FileStream(filePathString, System.IO.FileMode.Create ) );
	

			reportingSqlConnection = new SqlConnection(reportingConnectionString);
			reportingSqlConnection1 = new SqlConnection(reportingConnectionString);


			//.............................			

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




			strCheck1Failures =
                "SELECT event.RTEHour, event.RTEMinute, type.RTTCode FROM dbo.ReferenceTransactionEvents event, dbo.ReferenceTransactionType type"
                + " WHERE RTERTTID IN (SELECT RTTID FROM dbo.ReferenceTransactionType WHERE RTTChannel = 'Direct')"
                + " AND (RTEDate >= CONVERT(DATETIME, '"
                + reportStartDateString
                + "', 105)) AND (RTEDate < CONVERT(DATETIME, '"
                + (reportStartDate.AddDays(1)).ToShortDateString()
                + "', 105)) AND event.RTERTTID = type.RTTID AND event.RTESuccessful = 0 "
                + " and ( event.RTEMachineName not like '" + backupInjectorString + "' "
                + " or event.RTEMachineName is NULL ) "
                + " ORDER BY type.RTTCode, event.RTEHour, event.RTEMinute";

			sqlCommCheck = new SqlCommand(strCheck1Failures, reportingSqlConnection);
			
			strTLFailures =
                "SELECT event.RTEHour, event.RTEMinute, type.RTTCode FROM dbo.ReferenceTransactionEvents event, dbo.ReferenceTransactionType type"
                + " WHERE RTERTTID IN (SELECT RTTID FROM dbo.ReferenceTransactionType WHERE RTTChannel = 'Portal')"
                + " AND (RTEDate >= CONVERT(DATETIME, '"
                + reportStartDateString
                + "', 105)) AND (RTEDate < CONVERT(DATETIME, '"
                + (reportStartDate.AddDays(1)).ToShortDateString()
                + "', 105)) AND event.RTERTTID = type.RTTID AND event.RTESuccessful = 0"
                + " and ( event.RTEMachineName not like '" + backupInjectorString + "' "
                + "or event.RTEMachineName is NULL ) "
                + " ORDER BY type.RTTCode, event.RTEHour, event.RTEMinute";

			sqlCommTL = new SqlCommand(strTLFailures, reportingSqlConnection1);

			try
			{

				reportingSqlConnection.Open();
				drCheck1 = sqlCommCheck.ExecuteReader();
					
				tmpString = new StringBuilder();
				tmpString.Append("Daily Traveline Failure Report: ");
				tmpString.Append(reportStartDateString);
				this.file.WriteLine(tmpString.ToString());
                tmpString = new StringBuilder();
                tmpString.Append("==========================================");
                this.file.WriteLine(tmpString.ToString());
                tmpString = new StringBuilder();
                tmpString.Append("( Note - times are GMT) ");
                this.file.WriteLine(tmpString.ToString());
                this.file.WriteLine();
				tmpString = new StringBuilder();
				tmpString.Append("Direct Injection Failures:");
				this.file.WriteLine(tmpString.ToString());
                this.file.WriteLine();
								
				//Loop for every row returned in the SQL query
                while (drCheck1.Read())
                {

                    myHour = Convert.ToInt32(drCheck1.GetSqlByte(0).ToString());
                    myMin = Convert.ToInt32(drCheck1.GetSqlByte(1).ToString());
                    myType = drCheck1.GetSqlString(2).ToString();

                    tmpString = new StringBuilder();
                    tmpString.Append(myHour);
                    tmpString.Append(":");
                    tmpString.Append(myMin);
                    tmpString.Append(",");
                    tmpString.Append(myType);
                    this.file.WriteLine(tmpString.ToString());

                }

				// If there are no Check failures
                if (drCheck1.HasRows == false)
				{
					tmpString = new StringBuilder();
					tmpString.Append("No Direct Failures");
					this.file.WriteLine(tmpString.ToString());
				}

                myHour = -1;
                myMin = -1;
                myType = string.Empty;

				reportingSqlConnection1.Open();

				drTL = null;
				drTL = sqlCommTL.ExecuteReader();

				tmpString = new StringBuilder();
				this.file.WriteLine();
				tmpString.Append("Through Portal Injection Failures:");
				this.file.WriteLine(tmpString.ToString());
                this.file.WriteLine();

				//While loop from TL failures
				while (drTL.Read())
				{

					myHour = Convert.ToInt32(drTL.GetSqlByte(0).ToString());
					myMin  = Convert.ToInt32(drTL.GetSqlByte(1).ToString());
					myType  = drTL.GetSqlString(2).ToString();

                    tmpString = new StringBuilder();
                    tmpString.Append(myHour);
                    tmpString.Append(":");
                    tmpString.Append(myMin);
                    tmpString.Append(",");
                    tmpString.Append(myType);
                    this.file.WriteLine(tmpString.ToString());

				} // end of TL while

                // If there are no Check failures
                if (drTL.HasRows == false)
                {
                    tmpString = new StringBuilder();
                    tmpString.Append("No Through Portal Failures");
                    this.file.WriteLine(tmpString.ToString());
                }

				this.file.Flush();

			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure reading Reference Transaction Event table - "+e.Message,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Failed to reading ref events table
				return statusCode;
			}
			

			this.file.Flush();
			this.file.Close();
			drCheck1.Close();
			drTL.Close();
			reportingSqlConnection.Close();
			reportingSqlConnection1.Close();
		

			string from = "";
			string to = "";
			string smtpServer = "";
			string subject = "";


			readOK =  controller.ReadProperty(reportNo, "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}
            filePathString = filePathString.Replace("%YY-MM-DD%", RequestedDateString);
            readOK = controller.ReadProperty("0", "MailAddress", out from);
			if (readOK == false || from.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingSenderEmailAddress; // Error Reading sender email address
				return statusCode;
			}
			readOK =  controller.ReadProperty(reportNo, "MailRecipient", out to);
			if (readOK == false || to.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingRecipientEmailAddress; // Error Reading recipient email address
				return statusCode;
			}
			readOK =  controller.ReadProperty(reportNo, "Title", out subject);
			if (readOK == false || subject.Length == 0)
			{
				subject = "Daily Reference Transaction Extract %YY-MM-DD%";
			}
            subject = subject.Replace("%YY-MM-DD%", RequestedDateString);
 
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
	}
}
