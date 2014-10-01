///<header>
///Monthly Surge Report
///Created 14/11/2003
///Author JP Scott
///
///Version	Date		Who	Reason
///1		14/11/2003	PS	Created
///2		23/02/2004	PS	Amend Surge threshold and method of display
///3        06/02/2004  PS  Add monthly total surge duration figure.
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
	/// Class to produce Monthly Transactional Mix report for capacity planning
 	/// analyses all recorded event tables and reports total count per day
	/// </summary>
	/// 
	
	public class MonthlySurgeReport
	{
		private DateTime currentDateTime;
		private string reportStartDateString;
		private string reportEndDateString;
		private DateTime reportStartDate;
		private DateTime reportEndDate;
        private string RequestedDateString;
        private EventLog EventLogger;
		private	DailyReportController controller;
		
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
		private int[] dayTot = new int[32];
		private int[] monthlyDayTot = new int[32];
		private int[,,] DayHrMinCount = new int[32,24,60];  //1-31 - 0-23,0-59
		private int CBMaxRequests;
		private int CBSurgeThreshold;
		private float CBSurgePercentage;

		
		
		public MonthlySurgeReport()
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
			string CapacityBandString;
			int CapacityBand;
			int daysInMonth;
			int lineCount = 0;
			int surgeMinCount = 0;
			int hourMinute = 0;
			int endDay = 0;
			int startHour = 0;
			int endHour = 0;
			int startDay = 0;
			int startMinute = 0;
			int endMinute = 0;
			int durHour = 0;
			int durMin = 0;
			int duration = 0;

						int monthDurMin = 0;

			string strReportDate;
			string strStartHour; 
			string strStartMin; 
			string strEndHour; 
			string strEndMin;
			string strDurHour; 
			string strDurMin; 
			int day;
			int hour;
			int minute;
			int transactionCount = 0;

			daysInMonth = reportEndDate.Day;
			
			tmpString = new StringBuilder();

			bool readOK =  controller.ReadProperty("6", "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}
            filePathString = filePathString.Replace("%YY-MM%", RequestedDateString);

			// open report properties DB and read in the current capacity band.
			
			readOK =  controller.ReadProperty("0", "CurrentCapacityBand", out CapacityBandString);
			if (readOK == false)
			{
				statusCode = (int)StatusCode.ErrorReadingCapacityBand; // Error Reading Capacity Band Property
				return statusCode;
			}
			CapacityBand = Convert.ToInt32(CapacityBandString, CultureInfo.CurrentCulture);


			// open report text file
			file = new StreamWriter( new FileStream(filePathString, System.IO.FileMode.Create ) );
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("TRANSPORT DIRECT");
			this.file.WriteLine("DfT Monthly Surge Report");
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("");
			this.file.WriteLine("For month starting : " + reportStartDate.ToShortDateString());
			this.file.WriteLine("");

			this.file.WriteLine("");
			this.file.WriteLine("The periods detailed below exceeded the surge threshold");
			this.file.WriteLine("for a period of 15 minutes or more. ");
			this.file.WriteLine("(Based on capacity band " + CapacityBandString + " values.)");
			this.file.WriteLine("");
			this.file.WriteLine("Date,Start,End,Duration");
			this.file.WriteLine("----------,-----,-----,---------");
			this.file.Flush();

			reportingSqlConnection = new SqlConnection(reportingConnectionString);

			
		

			for (day = 1; day <= reportEndDate.Day;day++)
			{
				for (hour = 0; hour <= 23;hour++)
				{
					for (minute = 0; minute <= 59;minute++)
					{
						DayHrMinCount[day,hour,minute] = 0;
					}
				}
			}
			

			// extract current capacity band CBMaxRequests
			// and CBSurgePercentage

			selectString = "select CBMAxRequests,CBSurgePercentage from CapacityBands where CBNumber = '" + CapacityBand + "'";
			reportingSqlConnection = new SqlConnection(reportingConnectionString);

			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);

			try
			{
				reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

				// Always call Read before accessing data.

				while (myDataReader.Read())
				{
					CBMaxRequests = Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
					CBSurgePercentage = (float) Convert.ToDecimal( myDataReader.GetSqlDecimal(1).ToString());
				}
			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Error Reading Capacity Band table"+e.Message,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingTable; // Failed to read from property table
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();

				// Close the connection when done with it.
				if (reportingSqlConnection.State == ConnectionState.Open)
					reportingSqlConnection.Close();
			}

			// work out threshold for 1 minute threshold figure for capacity band are per week
			CBSurgeThreshold = Convert.ToInt32(Math.Floor( ((CBMaxRequests/6)*0.2*2)/60));


			// look for workload events-
			selectString = "select day(WEDate), WEHour, WEMinute, isnull(sum(WECount),0)from dbo.WorkloadEvents "
				+ "where WEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and WEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) group by WEDate, WEHour, WEMinute order by WEDate, WEHour, WEMinute";

			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
			try
			{
				reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

				
				while (myDataReader.Read())
				{
					day = Convert.ToInt32(myDataReader.GetInt32(0).ToString());
					hour = Convert.ToInt32(myDataReader.GetSqlByte(1).ToString());
					minute = Convert.ToInt32(myDataReader.GetSqlByte(2).ToString());
					transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(3).ToString());
					hourMinute = hour*60+minute;					
					DayHrMinCount[day,hour,minute] = transactionCount;
				} // end of while
				myDataReader.Close();
			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure reading workload event table "+e.ToString(),EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Failed to read events table
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();
			}

			for (day = 1; day <= reportEndDate.Day;day++)
			{
				for (hour = 0; hour <= 23;hour++)
				{
					for (minute = 0; minute <= 59;minute++)
					{
						transactionCount = DayHrMinCount[day,hour,minute];
						if (transactionCount > CBSurgeThreshold)
						{
							if (duration == 0)
							{
								startDay    = day;
								startHour   = hour;
								startMinute = minute;
							}
							duration = duration+1;
							endDay = day;
							endHour = hour;
							endMinute = minute; 
							surgeMinCount = surgeMinCount +1;
						}
						else
						{
							if (duration >=15)
							{
								// requires reporting

								reportDate = new DateTime(reportDate.Year,reportDate.Month,day,0,0,0);
								strReportDate = reportDate.ToShortDateString();
			
								strStartHour = startHour.ToString("D2");
								strStartMin  = startMinute.ToString("D2");
								strEndHour   = endHour.ToString("D2");
								strEndMin    = endMinute.ToString("D2");



								monthDurMin = monthDurMin + duration;
								durHour = duration/60;
								strDurHour = durHour.ToString("D2");
								durMin = duration - ((duration/60)*60);
								strDurMin = durMin.ToString("D2");
	
								this.file.WriteLine(strReportDate+","+strStartHour+ ":"+strStartMin+","+
									strEndHour+ ":"+strEndMin+","+
									strDurHour+ ":"+strDurMin);
								lineCount = lineCount+1;

							}
							duration = 0;
							startDay    = day;
							startHour   = hour;
							startMinute = minute;
						}
					}	
				}
			}
			float percent = 0;
			string strPercent;
			if (lineCount == 0)
			{
				this.file.WriteLine("");
				this.file.WriteLine("");
				this.file.WriteLine("NO REPORTING PERIODS WERE IN SURGE THIS MONTH");
			}
			else
			{

				durHour = monthDurMin/60;
				strDurHour = durHour.ToString("D2");
				durMin = monthDurMin - ((monthDurMin/60)*60);
				strDurMin = durMin.ToString("D2");

				this.file.WriteLine("");
				this.file.WriteLine("TOTAL DURATION:- "+strDurHour+ "hrs  "+strDurMin+" mins");


				percent = (float)Math.Floor((float)monthDurMin*1000/(float)(daysInMonth*14.4))/1000;
				strPercent = percent.ToString();
				this.file.WriteLine("This represents "+strPercent+" percent of the time.");
			}

			this.file.WriteLine("");
			this.file.WriteLine("");
			this.file.WriteLine("TREND:-  "+surgeMinCount+" minutes exceeded surge threshold this month.");
			percent = (float)Math.Floor((float)surgeMinCount*1000/(float)(daysInMonth*14.4))/1000;
			strPercent = percent.ToString();
			
				this.file.WriteLine("This represents "+strPercent+" percent of the time.");

			this.file.WriteLine("         (Note this figure reports on individual minutes which");
			this.file.WriteLine("          may not be in a 15min SLA continuous surge period.)");
					
			this.file.Flush();
		

			this.file.Close();
			string from = "";
			string to = "";
			string smtpServer = "";
			string subject = "";


			readOK =  controller.ReadProperty("6", "FilePath", out filePathString);
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
			readOK =  controller.ReadProperty("6", "MailRecipient", out to);
			if (readOK == false || to.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingRecipientEmailAddress; // Error Reading recipient email address
				return statusCode;
			}
			readOK =  controller.ReadProperty("6", "Title", out subject);
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
	}
}
