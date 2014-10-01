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
    /// THIS CLASS IS OBSOLETE, replaced by MonthlyAvailabilityReport - (To Be Confirmed)
	public class MonthlyInUseReport
	{
		private DateTime currentDateTime;
		private string reportStartDateString;
		private string reportEndDateString;
		private DateTime reportStartDate;
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
		private int[,] DayMinuteCount = new int[32,1441];  //1-31 - 1-1440


        public MonthlyInUseReport()
		{
			EventLogger = new EventLog("Application");
			EventLogger.Source = "TD.Reporting";
			controller    = new DailyReportController();
            propertyConnectionString = ConfigurationManager.AppSettings["ReportProperties.connectionstring"];
            reportingConnectionString = ConfigurationManager.AppSettings["ReportDatabase.connectionstring"];
		}
		
		public int RunReport(DateTime reportDate)
		{
			statusCode = (int)StatusCode.Success; // success
			string reportDateString;
			reportDateString = reportDate.ToShortDateString();
			currentDateTime = System.DateTime.Now;
			reportStartDate = new DateTime(reportDate.Year,reportDate.Month,1,0,0,0);
			reportEndDate = reportStartDate.AddMonths(1);
			reportEndDate = reportEndDate.AddDays(-1);
			reportStartDateString = reportStartDate.ToShortDateString();
			reportEndDateString = reportEndDate.ToShortDateString();
			tmpString = new StringBuilder();
			tmpString.Append(",Day,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28");
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


			bool readOK =  controller.ReadProperty("5", "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}


			// open report text file
			file = new StreamWriter( new FileStream(filePathString, System.IO.FileMode.Create ) );
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("TRANSPORT DIRECT");
			this.file.WriteLine("DfT Monthly Availability Report");
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("");
			this.file.WriteLine("For month starting : " + reportStartDate.ToShortDateString());
			this.file.WriteLine("");
			this.file.WriteLine("(* = Unavailable for whole quarter hour period)");
			this.file.WriteLine("");
			this.file.WriteLine(daysString);
			this.file.WriteLine("Hour,Quarterhour");
			this.file.Flush();
			reportingSqlConnection = new SqlConnection(reportingConnectionString);

			
			int day;
			int hour;
			int minute;
			int hourminute;
			int transactionCount = 0;
			// look for workload events
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
					hourminute   = (hour * 60)+minute;
					transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(3).ToString());
					DayMinuteCount[day,hourminute] =DayMinuteCount[day,hourminute] + transactionCount;
				} // end of while
				myDataReader.Close();
			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure reading workload event table "+e.Message,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Failed to read events table
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();
			}


			// convert array to minutes where no activity
			// i.e. make array 1 min where 0 and 0 where over 1 
			int qhr=0;
			string dayString = ",";
			for (day=1;day<=reportEndDate.Day;day++)
			{
				monthlyDayTot[day] = 0;
			}
			for (day = 1; day <= reportEndDate.Day;day++)
			{
				for(hourminute = 0; hourminute<=1440;hourminute++)
				{
					if (DayMinuteCount[day,hourminute] == 0)
					{
						DayMinuteCount[day,hourminute] = 1;
					}
					else
					{
						DayMinuteCount[day,hourminute] = 0;
					}
				}
			}

			// now slot the unavailable times into 1/4 hr reporting periods
			int qhrBreak = -1;
			for (hourminute=0;hourminute<1440;hourminute++)
			{
				hour = Convert.ToInt32(Math.Floor((double)hourminute/60));
				qhrBreak = qhrBreak+1;
				if (qhrBreak==60)
					qhrBreak = 0;
				qhr=4;
				if (qhrBreak < 45)
				{
					qhr=3;
					if (qhrBreak < 30)
					{
						qhr = 2;
						if (qhrBreak < 15)
						{
							qhr= 1;
						}
					}
				}
				if (qhrBreak == 0 || qhrBreak == 15 || qhrBreak == 30 || qhrBreak ==45)
				{
					tmpString = new StringBuilder();
					tmpString.Append(hour.ToString()+",1");
					transactionCount = 0;
				}
				for (day=1;day<=reportEndDate.Day;day++)
				{
					if(DayMinuteCount[day,hourminute] == 1)
					{
						dayTot[day] = dayTot[day]+1;
						monthlyDayTot[day] = monthlyDayTot[day]+1;
						transactionCount=transactionCount+1;
					}
				}
				if (qhrBreak == 14 || qhrBreak == 29 || qhrBreak == 44 || qhrBreak ==59)
				{
					if (transactionCount > 0)
					{
						tmpString = new StringBuilder();
						tmpString.Append(hour.ToString()+","+qhr.ToString());
						for (day=1; day <= reportEndDate.Day;day++)
						{
							dayString = ",";
							if(DayMinuteCount[day,hourminute] == 1)
							{
								if(dayTot[day] >= 15)
								{
									dayString=",*";
								}
							}
							tmpString.Append(dayString);
						}
						this.file.WriteLine(tmpString.ToString());
						this.file.Flush();
						transactionCount = 0;
					}
					for(int i=0;i<=31;i++)
					{
					   dayTot[i]=0;
					}
				}
			}
			
			tmpString = new StringBuilder();
			tmpString.Append("Monthly Total (Hrs:Mins unavailable,");
			for (day=1; day <= reportEndDate.Day;day++)
			{
				if(monthlyDayTot[day] > 0)
				{
					hour = Convert.ToInt32(Math.Floor((double)monthlyDayTot[day]/60));
					minute = monthlyDayTot[day] - (hour*60);
					if(hour == 24)
					{
						tmpString.Append(",24:00");
					}
					else
					{
						tmpString.Append(","+hour.ToString()+":"+minute.ToString());
					}
				}
				else
				{
					tmpString.Append(",");
				}
			}
			this.file.WriteLine("");
			this.file.WriteLine(tmpString.ToString());

			this.file.Flush();
			this.file.Close();

			string from = "";
			string to = "";
			string smtpServer = "";
			string subject = "";


			readOK =  controller.ReadProperty("5", "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}
			readOK =  controller.ReadProperty("0", "MailAddress", out from);
			if (readOK == false || from.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingSenderEmailAddress; // Error Reading sender email address
				return statusCode;
			}
			readOK =  controller.ReadProperty("5", "MailRecipient", out to);
			if (readOK == false || to.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingRecipientEmailAddress; // Error Reading recipient email address
				return statusCode;
			}
			readOK =  controller.ReadProperty("5", "Title", out subject);
			if (readOK == false || subject.Length == 0)
			{
				subject = "Monthly Transactional Mix Report";
			}
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
