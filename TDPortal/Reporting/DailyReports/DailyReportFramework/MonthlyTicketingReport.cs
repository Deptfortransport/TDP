///<header>
///Monthly Ticketing Report
///Created 23/04/2004
///Author JP Scott
///
///Version	Date		Who	Reason
///1		23/04/2004	PS	Created 
///2		01/06/2004	PS	Add totals for each vendor
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
	/// Class to produce Monthly Ticketing report shows retailer handoffs
	/// </summary>
	/// 
	
	public class MonthlyTicketingReport
	{
		private string reportNo;
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
		
		private int[] dayCount = new int[32];  //1-31
		private int retailtot = 0;

		
		
		public MonthlyTicketingReport()
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
			reportNo ="8";
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
			tmpString.Append("Retailer  ,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28");
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
			tmpString.Append(",Retailer Total");

			string daysString = tmpString.ToString();



			bool readOK =  controller.ReadProperty(reportNo, "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}
            filePathString = filePathString.Replace("%YY-MM%", RequestedDateString);
   

			// open report text file
			file = new StreamWriter( new FileStream(filePathString, System.IO.FileMode.Create ) );
			this.file.WriteLine("-----------------------------------------");
			this.file.WriteLine("TRANSPORT DIRECT MONTHLY TICKETING REPORT");
			this.file.WriteLine("-----------------------------------------");
			this.file.WriteLine("");
			this.file.WriteLine("For month starting : " + reportStartDate.ToShortDateString());
			this.file.WriteLine("");
			this.file.WriteLine("    hand off events by day");
			this.file.WriteLine(daysString);
			this.file.Flush();
			reportingSqlConnection = new SqlConnection(reportingConnectionString);

			//.............................			
			transactionString = "Retailer Handoff Events";
			selectString = 
				"select RHT.RHTDescription, day(RH.RHEDate), isnull(sum(RH.RHECount),0)"
				+ "from dbo.RetailerHandoffEvents RH, dbo.RetailerHandoffType RHT where RHT.RHTID = RH.RHERHTID"
                + " and RHT.RHTOnlineRetailer = 1"
				+ "  and RH.RHEDate >= convert(datetime, '" + reportStartDateString
				+ "',103) and RH.RHEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) group by RHT.RHTDescription,RH.RHEDate order by RHT.RHTDescription,RH.RHEDate";
			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
			int day;
			int transactionCount = 0;
			string description;
			string lastDescription;
			for (int i = 1;i<=reportEndDate.Day;i++)
			{
				dayCount[i] = 0;
			}
			retailtot = 0;
			try
			{
				lastDescription = "default";
				reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				while (myDataReader.Read())
				{

					description = myDataReader.GetString(0);
					day = Convert.ToInt32(myDataReader.GetInt32(1).ToString());
					transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(2).ToString());
					
					if(description != lastDescription)
					{
						if (lastDescription == "default")
						{
							lastDescription   = description;
						}
						else
						{
							tmpString = new StringBuilder();
							tmpString.Append(lastDescription);
							for (int i = 1;i<=reportEndDate.Day;i++)
							{
								tmpString.Append(","+dayCount[i]);
								retailtot=retailtot+dayCount[i];
								dayCount[i] = 0;
							}
							tmpString.Append(","+retailtot);
							retailtot = 0;
							this.file.WriteLine(tmpString.ToString());
							this.file.Flush();
							lastDescription   = description;
						}
					}
					dayCount[day] = transactionCount;

				} // end of while
				tmpString = new StringBuilder();
				tmpString.Append(lastDescription);
				for (int i = 1;i<=reportEndDate.Day;i++)
				{
					tmpString.Append(","+dayCount[i]);
					retailtot=retailtot+dayCount[i];
					dayCount[i] = 0;
				}
				tmpString.Append(","+retailtot);
				retailtot=0;
				this.file.WriteLine(tmpString.ToString());
				this.file.Flush();
				myDataReader.Close();

			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure reading table "+transactionString+"  "+e.Message,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Failed to reading page events table
				return statusCode;
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();
			}

			this.file.Flush();
			this.file.Close();

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
            filePathString = filePathString.Replace("%YY-MM%", RequestedDateString);

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
                subject = "Monthly Ticketing Report %YY-MM%";
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
	}
}
