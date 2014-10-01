///<header>
///Daily Reference Transaction Extract (16)
///Created 23/04/2004
///Author JP Scott
///
///Version	Date		Who	Reason
///1		05/01/2005	PS	Created 
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

using DailyReportFramework;

namespace DailyReportFramework
{
	/// <summary>
	/// Class to produce Daily ReferenceTransaction Extract shows retailer handoffs
	/// </summary>
	/// 
	
	public class DailyRefTransExtract
	{
		private string reportNo;
		private DateTime currentDateTime;
		private string reportStartDateString;

		private DateTime reportStartDate;
        private string RequestedDateString;
		
		private EventLog EventLogger;
		private DailyReportController controller;
		
		private string propertyConnectionString;
		private string reportingConnectionString;
		private SqlConnection reportingSqlConnection;
		private SqlDataReader myDataReader;
		private SqlCommand mySqlCommand;
		private StringBuilder tmpString;
		private string selectString;
		private System.IO.StreamWriter file;
		private string filePathString;
		private int statusCode;
		
		private int[] dayCount = new int[32];  //1-31
		private string myDate;
		private int myHour;
		private int myMin;
		private int myDay;
		private int myTid;
		private int mySuccessFlag;
		private int myOverran;
		private int myDuration;
        private string defaultInjectorString;
        private string backupInjectorString;

		public DailyRefTransExtract()
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
			reportNo ="16";
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

            // find correct injector transactions
            readOK = controller.ReadProperty("0", "DefaultInjector", out defaultInjectorString);
            if (readOK == false || defaultInjectorString.Length == 0)
            {
                statusCode = (int)StatusCode.ErrorReadingFilePathDefaultInjector; // Error Reading FilePath
                return statusCode;
            }
            // find correct injector transactions
            readOK = controller.ReadProperty("0", "BackupInjector", out backupInjectorString);
            if (readOK == false || backupInjectorString.Length == 0)
            {
                statusCode = (int)StatusCode.ErrorReadingFilePathBackupInjector; // Error Reading FilePath
                return statusCode;
            }
			//.............................			
			selectString = 
						"SELECT * FROM dbo.ReferenceTransactionEvents "
						+ "WHERE (RTEDate >= Convert(datetime,'"
						+ reportStartDateString 
						+ "',105) "
						+ "AND (RTERTTID >= 50)) "
                        + "and ( RTEMachineName not like '" + backupInjectorString + "' "
                        + "or RTEMachineName is NULL ) "
 						+ "order by rtedate,rtehour,rteminute ";
			myDataReader = null;


			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
			try
			{
				reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				while (myDataReader.Read())
				{


					myDate = myDataReader.GetDateTime(0).ToString();
					myHour = Convert.ToInt32(myDataReader.GetSqlByte(1).ToString());
					myMin  = Convert.ToInt32(myDataReader.GetSqlByte(2).ToString());
					myDay  = Convert.ToInt32(myDataReader.GetSqlByte(3).ToString());
					myTid = Convert.ToInt32(myDataReader.GetSqlByte(4).ToString());
     				mySuccessFlag = Convert.ToInt32(myDataReader.GetSqlByte(5).ToString());
					myOverran     = Convert.ToInt32(myDataReader.GetSqlByte(6).ToString());
					myDuration    = Convert.ToInt32(myDataReader.GetSqlInt32(7).ToString());

					tmpString = new StringBuilder();
					tmpString.Append(myDate);
					tmpString.Append(",");
					tmpString.Append(myHour);
					tmpString.Append(",");
					tmpString.Append(myMin);
					tmpString.Append(",");
					tmpString.Append(myDay);
					tmpString.Append(",");
					tmpString.Append(myTid);
					tmpString.Append(",");
					tmpString.Append(mySuccessFlag);
					tmpString.Append(",");
					tmpString.Append(myOverran);
					tmpString.Append(",");
					tmpString.Append(myDuration);
					this.file.WriteLine(tmpString.ToString());

				} // end of while
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
			myDataReader.Close();
			
		

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
