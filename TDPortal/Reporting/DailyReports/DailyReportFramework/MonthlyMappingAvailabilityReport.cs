///<header>
///Monthly Mapping Availability Report
///Created 14/11/2003
///Author JP Scott
///
///Version	Date		Who	Reason
///1		14/11/2003	PS	Created
///2		23/02/2004	PS	Availability comes from reference transactions
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
	/// Class to produce Monthly Mapping Availability
	/// analyses all mapping reference transacations and reports periods where absent
	/// </summary>
	/// 
	
	public class MonthlyMappingAvailabilityReport
	{
		private DateTime currentDateTime;
		private string reportStartDateString;
		private string reportEndDateString;
		private DateTime reportStartDate;
        private string RequestedDateString;
        private DateTime reportEndDate;
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
		private int day;
		private int hour;
		private int minute;
        private string defaultInjectorString;
        private string backupInjectorString;
        private string travelineInjectorString;


		
		public MonthlyMappingAvailabilityReport()
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
			int daysInMonth;
			int lineCount = 0;
			int missingCount = 0;
			int endDay = 0;
			int startHour = 0;
			int endHour = 0;
			int startDay = 0;
			int startMinute = 0;
			int endMinute = 0;
			int durDay = 0;
			int durHour = 0;
			int durMin = 0;
			int duration = 0;
			string strReportDate;
			string strStartHour; 
			string strStartMin; 
			string strEndHour; 
			string strEndMin;
			string strDurDay; 
			string strDurHour; 
			string strDurMin; 
			int transactionCount = 0;

			daysInMonth = reportEndDate.Day;
			
			tmpString = new StringBuilder();

			bool readOK =  controller.ReadProperty("7", "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}
            filePathString = filePathString.Replace("%YY-MM%", RequestedDateString);

			// open report text file
			file = new StreamWriter( new FileStream(filePathString, System.IO.FileMode.Create ) );
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("TRANSPORT DIRECT");
			this.file.WriteLine("DfT Monthly Mapping Availability Report");
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("");
			this.file.WriteLine("For month starting : " + reportStartDate.ToShortDateString());
			this.file.WriteLine("");

			this.file.WriteLine("");
			this.file.WriteLine("The periods detailed below indicate the time when injected mapping");
			this.file.WriteLine("transactions failed continuously for 15 minutes or more. ");
			this.file.WriteLine("It is likely that this was due to mapping unavailability.");
			this.file.WriteLine("");
			this.file.WriteLine("Date        Start   End    Duration");
			this.file.WriteLine("----------  -----   -----  ---------");
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

            /////////////////////////////////////////////////////////////////////////////////////////////////
            // find correct injector transactions
            readOK = controller.ReadProperty("0", "DefaultInjector", out defaultInjectorString);
            if (readOK == false || defaultInjectorString.Length == 0)
            {
                statusCode = (int)StatusCode.ErrorReadingFilePathDefaultInjector; // Error Reading FilePath
                return statusCode;
            }
            //.............................	
            readOK = controller.ReadProperty("0", "BackupInjector", out backupInjectorString);
            if (readOK == false || defaultInjectorString.Length == 0)
            {
                statusCode = (int)StatusCode.ErrorReadingFilePathDefaultInjector; // Error Reading FilePath
                return statusCode;
            }
            //.............................	
            readOK = controller.ReadProperty("0", "TravelineInjector", out travelineInjectorString);
            if (readOK == false || defaultInjectorString.Length == 0)
            {
                statusCode = (int)StatusCode.ErrorReadingFilePathDefaultInjector; // Error Reading FilePath
                return statusCode;
            }
            //.............................	
			// look for reference events-
			// load  array of reference transactions count by day / minute in day
			selectString = "select day(RTEDate), RTEHour, RTEMinute, isnull(count(*),0)from dbo.ReferenceTransactionEvents "
				+ "where RTEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and RTEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) "
				+ "and RTERTTID >= 8 and RTERTTID <= 10 "
				+ "and RTESuccessful = 1 "
                + "and (RTEMachineName not like '" + backupInjectorString + "' "
                + "and  RTEMachineName not like '" + travelineInjectorString + "' ) "
                + " group by RTEDate, RTEHour, RTEMinute order by RTEDate, RTEHour, RTEMinute";

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
					DayHrMinCount[day,hour,minute] = transactionCount;
				} // end of while
				myDataReader.Close();
			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure reading reference transaction table "+e.Message,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingTable; // Failed to read events table
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();
			}


			// convert array to minutes where no activity
			// i.e. make array 1 min where 0 and 0 where over 1 
			for (day = 1; day <= reportEndDate.Day;day++)
			{
				monthlyDayTot[day] = 0;
				for (hour = 0; hour <= 23;hour++)
				{

					for(minute = 0; minute<=59;minute++)
					{
						if (DayHrMinCount[day,hour,minute] == 0)
						{
							DayHrMinCount[day,hour,minute] = 1;
							missingCount = missingCount +1;
						}
						else
						{
							DayHrMinCount[day,hour,minute] = 0;
						}
					}
				}
			}



			for (day = 1; day <= reportEndDate.Day;day++)
			{
				for (hour = 0; hour <= 23;hour++)
				{
					for (minute = 0; minute <= 59;minute++)
					{
						transactionCount = DayHrMinCount[day,hour,minute];
						if (transactionCount > 0)
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
						}
						else
						{
							if (duration >=15)
							{
								// requires reporting

								endDay = day;
								endHour = hour;
								endMinute = minute; 

								reportDate = new DateTime(reportDate.Year,reportDate.Month,startDay,0,0,0);
								strReportDate = reportDate.ToShortDateString();
			
								strStartHour = startHour.ToString("D2");
								strStartMin  = startMinute.ToString("D2");
								strEndHour   = endHour.ToString("D2");
								strEndMin    = endMinute.ToString("D2");

								durHour = duration/60;
								if (durHour > 23)
								{
									durDay = durHour/24;
									durHour = durHour - (durDay*24);
									strDurDay = durDay.ToString("D2")+ "day(s) ";
								}
								else
								{
									strDurDay = " ";
								}
								strDurHour = durHour.ToString("D2");
								durMin = duration - ((duration/60)*60);
								strDurMin = durMin.ToString("D2");
	
								this.file.WriteLine(strReportDate+"  "+strStartHour+ ":"+strStartMin+"   "+
									strEndHour+ ":"+strEndMin+"   "+
									strDurDay+strDurHour+ ":"+strDurMin);
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
			//flush
			if (duration >=15)
			{
				// requires reporting

				endDay = day-1;
				endHour = hour-1;
				endMinute = minute-1; 

				reportDate = new DateTime(reportDate.Year,reportDate.Month,startDay,0,0,0);
				strReportDate = reportDate.ToShortDateString();
			
				strStartHour = startHour.ToString("D2");
				strStartMin  = startMinute.ToString("D2");
				strEndHour   = endHour.ToString("D2");
				strEndMin    = endMinute.ToString("D2");

				durHour = duration/60;
				if (durHour > 23)
				{
					durDay = durHour/24;
					durHour = durHour - (durDay*24);
					strDurDay = durDay.ToString("D2")+ "day(s) ";
				}
				else
				{
					strDurDay = " ";
				}
				strDurHour = durHour.ToString("D2");
				durMin = duration - ((duration/60)*60);
				strDurMin = durMin.ToString("D2");
	
				this.file.WriteLine(strReportDate+"  "+strStartHour+ ":"+strStartMin+"   "+
					strEndHour+ ":"+strEndMin+"   "+
					strDurDay+strDurHour+ ":"+strDurMin);
				lineCount = lineCount+1;
			}
			if (lineCount == 0)
			{
				this.file.WriteLine("");
				this.file.WriteLine("");
				this.file.WriteLine("THE SYSTEM WAS 100% AVAILAVBLE THIS MONTH");
			}
			this.file.WriteLine("");
			this.file.WriteLine("");
			this.file.WriteLine("TREND:-  "+missingCount+" minutes this month had no mapping Reference Transactions .");
			this.file.WriteLine("         This represents "+Math.Floor((double)missingCount/(double)(daysInMonth*14.4))+" percent of the time.");

		
			this.file.Flush();
		
			this.file.Close();

			string from = "";
			string to = "";
			string smtpServer = "";
			string subject = "";


			readOK =  controller.ReadProperty("7", "FilePath", out filePathString);
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
			readOK =  controller.ReadProperty("7", "MailRecipient", out to);
			if (readOK == false || to.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingRecipientEmailAddress; // Error Reading recipient email address
				return statusCode;
			}
			readOK =  controller.ReadProperty("7", "Title", out subject);
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
